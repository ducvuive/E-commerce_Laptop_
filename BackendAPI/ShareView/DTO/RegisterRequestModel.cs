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
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }


    public class RegisterResponseModel
    {
        public object Value { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
