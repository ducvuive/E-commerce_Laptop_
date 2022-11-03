using BackendAPI.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShareView.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendAPI.Controllers
{
    /*[route("[controller]")]
    [apicontroller]*/
    public class AuthController : Controller
    {
        private readonly IConfiguration config;
        private readonly SignInManager<UserIdentity> signInManager;
        private readonly UserManager<UserIdentity> userManager;

        public AuthController(IConfiguration config, SignInManager<UserIdentity> signInManager, UserManager<UserIdentity> userManager)
        {
            this.config = config;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IResult> Register([FromBody] RegisterRequestModel registerRequestModel)
        {
            var a = 1;
            // khong bi loi require,...
            if (ModelState.IsValid)
            {
                var user = new UserIdentity
                {
                    UserName = registerRequestModel.UserName,
                    Email = registerRequestModel.UserName,
                    PhoneNumber = registerRequestModel.PhoneNumber,
                };

                // tao user trong database
                var createUserResult = await userManager.CreateAsync(user, registerRequestModel.Password);
                a = 1;

                if (createUserResult.Succeeded)
                {
                    return Results.Ok();
                }
                else
                {
                    foreach (var error in createUserResult.Errors)
                    {
                        ModelState.AddModelError("name", error.Description);
                    }
                    return Results.BadRequest(createUserResult.Errors);
                }
            }

            return Results.BadRequest(ModelState.Values.SelectMany(x => x.Errors));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IResult> Login([FromBody] LoginRequestModel loginRequestModel)
        {
            var result = await signInManager.PasswordSignInAsync(loginRequestModel.UserName, loginRequestModel.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var issuer = config["Jwt:Issuer"];
                var audience = config["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes
                (config["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, loginRequestModel.UserName),
                new Claim(JwtRegisteredClaimNames.Email, loginRequestModel.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };

                var identityUser = await userManager.FindByEmailAsync(loginRequestModel.UserName);
                var roles = await userManager.GetRolesAsync(identityUser);

                foreach (var role in roles)
                {
                    tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
                }

                // generate token with the above information
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);

                return Results.Ok(stringToken);
            }

            //return Results.Unauthorized();
            throw new Exception("wrong password");
            return Results.BadRequest(new { message = "wrong password" });
        }


    }
}