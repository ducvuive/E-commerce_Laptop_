using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class LoginRequestModel
    {

        [Required]
        [EmailAddress(ErrorMessage = "Không đúng định dạng email")]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
