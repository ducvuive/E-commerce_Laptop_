using BackendAPI.Persistence.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<UserIdentity> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserManagementDTO>>> GetUser()
        {
            var users = await _userManager.Users.OrderBy(u => u.UserName).ToListAsync();
            var result = new List<UserManagementDTO>();

            foreach (var user in users)
            {
                result.Add(await ToUserManagementDTO(user));
            }

            return Ok(result);
        }

        [HttpGet("{idOrEmail}")]
        public async Task<ActionResult<UserManagementDTO>> Get(string idOrEmail)
        {
            var user = await _userManager.FindByIdAsync(idOrEmail)
                ?? await _userManager.FindByEmailAsync(idOrEmail);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(await ToUserManagementDTO(user));
        }

        [HttpPost]
        public async Task<ActionResult<UserManagementDTO>> CreateUser(UserUpsertRequest request)
        {
            var user = new UserIdentity
            {
                UserName = string.IsNullOrWhiteSpace(request.UserName) ? request.Email : request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(user, request.Password ?? string.Empty);
            if (!createResult.Succeeded)
            {
                return BadRequest(createResult.Errors.Select(e => e.Description));
            }

            var roleResult = await ReplaceUserRoles(user, request.Roles);
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return BadRequest(roleResult.Errors.Select(e => e.Description));
            }

            return CreatedAtAction(nameof(Get), new { idOrEmail = user.Id }, await ToUserManagementDTO(user));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UserUpsertRequest request)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = string.IsNullOrWhiteSpace(request.UserName) ? request.Email : request.UserName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return BadRequest(updateResult.Errors.Select(e => e.Description));
            }

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, request.Password);
                if (!passwordResult.Succeeded)
                {
                    return BadRequest(passwordResult.Errors.Select(e => e.Description));
                }
            }

            var roleResult = await ReplaceUserRoles(user, request.Roles);
            if (!roleResult.Succeeded)
            {
                return BadRequest(roleResult.Errors.Select(e => e.Description));
            }

            return Ok(await ToUserManagementDTO(user));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (await IsLastAdmin(user))
            {
                return BadRequest("Cannot delete the last admin user.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(e => e.Description));
            }

            return NoContent();
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<RoleDTO>>> GetRoles()
        {
            var roles = await _roleManager.Roles
                .OrderBy(r => r.Name)
                .Select(r => new RoleDTO { Id = r.Id, Name = r.Name })
                .ToListAsync();

            return Ok(roles);
        }

        [HttpPost("roles")]
        public async Task<ActionResult<RoleDTO>> CreateRole(RoleUpsertRequest request)
        {
            var roleName = request.Name?.Trim();
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name is required.");
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(e => e.Description));
            }

            var role = await _roleManager.FindByNameAsync(roleName);
            return CreatedAtAction(nameof(GetRoles), new RoleDTO { Id = role!.Id, Name = role.Name });
        }

        [HttpPut("roles/{id}")]
        public async Task<IActionResult> UpdateRole(string id, RoleUpsertRequest request)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            role.Name = request.Name?.Trim();
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(e => e.Description));
            }

            return Ok(new RoleDTO { Id = role.Id, Name = role.Name });
        }

        [HttpDelete("roles/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var assignedUsers = await _userManager.GetUsersInRoleAsync(role.Name!);
            if (assignedUsers.Any())
            {
                return BadRequest("Cannot delete a role that is assigned to users.");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(e => e.Description));
            }

            return NoContent();
        }

        private async Task<UserManagementDTO> ToUserManagementDTO(UserIdentity user)
        {
            return new UserManagementDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = (await _userManager.GetRolesAsync(user)).ToList()
            };
        }

        private async Task<IdentityResult> ReplaceUserRoles(UserIdentity user, List<string>? roles)
        {
            var newRoles = roles?
                .Where(r => !string.IsNullOrWhiteSpace(r))
                .Select(r => r.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList() ?? new List<string>();

            foreach (var role in newRoles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    return IdentityResult.Failed(new IdentityError { Description = $"Role '{role}' does not exist." });
                }
            }

            if (await IsLastAdmin(user) && !newRoles.Contains("Admin", StringComparer.OrdinalIgnoreCase))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Cannot remove Admin from the last admin user." });
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                return removeResult;
            }

            return newRoles.Any()
                ? await _userManager.AddToRolesAsync(user, newRoles)
                : IdentityResult.Success;
        }

        private async Task<bool> IsLastAdmin(UserIdentity user)
        {
            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return false;
            }

            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            return admins.Count == 1 && admins[0].Id == user.Id;
        }
    }

    public class UserManagementDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string> Roles { get; set; } = new();
    }

    public class UserUpsertRequest
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public List<string>? Roles { get; set; }
    }

    public class RoleDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? Name { get; set; }
    }

    public class RoleUpsertRequest
    {
        public string? Name { get; set; }
    }
}
