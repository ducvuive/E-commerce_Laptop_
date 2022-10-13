using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Test.Models {
    public class AppUser : IdentityUser {
        [MaxLength (100)]
        public string FullName { set; get; }
        
        [MaxLength (255)]
        public string DiaChi { set; get; }

        [DataType (DataType.Date)]
        public DateTime? NgaySinh { set; get; }

        public string GioiTinh { set; get; }
    }
}