using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BackendAPI.Areas.Identity.Data;

// Add profile data for application users by adding properties to the UserIdentity class
public class UserIdentity : IdentityUser
{
    [MaxLength(100)]
    public string FullName { set; get; }

    [MaxLength(255)]
    public string DiaChi { set; get; }

    [DataType(DataType.Date)]
    public DateTime? NgaySinh { set; get; }

    public string GioiTinh { set; get; }
}

