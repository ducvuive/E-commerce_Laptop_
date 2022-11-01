using Microsoft.AspNetCore.Identity;

namespace ShareView.DTO
{
    public class UserIdentityDTO : IdentityUser
    {
        public string? FullName { set; get; }
        public string? DiaChi { set; get; }
        public DateTime? NgaySinh { set; get; }
        public string? GioiTinh { set; get; }
    }
}
