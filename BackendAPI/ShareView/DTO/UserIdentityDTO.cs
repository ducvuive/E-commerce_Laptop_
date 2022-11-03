using Microsoft.AspNetCore.Identity;

namespace ShareView.DTO
{
    public class UserIdentityDTO : IdentityUser
    {
        public string? Email { set; get; }
        public string? UserName { set; get; }
        /*        public string? FullName { set; get; }*/
        public string? DiaChi { set; get; }
        public DateTime? NgaySinh { set; get; }
        public int? PhoneNumber { set; get; }
    }
}
