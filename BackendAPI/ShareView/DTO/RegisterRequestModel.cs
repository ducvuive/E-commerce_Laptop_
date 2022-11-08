using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ShareView.DTO
{
    public class RegisterRequestModel
    {
        [Required(ErrorMessage = "Username không được để trống")]
        [EmailAddress(ErrorMessage = "Không đúng định dạng email")]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(20, ErrorMessage = "Độ dài mật khẩu phải từ 3 đến 20 kí tự.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Mật khẩu xác nhận không được để trống")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng")]
        [StringLength(10, ErrorMessage = "Số điện thoại phải dưới 10 ký tự")]
        public string PhoneNumber { get; set; }
    }


    public class RegisterResponseModel
    {
        public object Value { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
