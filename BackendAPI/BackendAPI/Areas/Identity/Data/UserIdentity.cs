using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Areas.Identity.Data;

// Add profile data for application users by adding properties to the UserIdentity class
public class UserIdentity : IdentityUser
{
    [MaxLength(255)]
    public string? Adress { set; get; }

    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { set; get; }

    public string? Gender { set; get; }
}

