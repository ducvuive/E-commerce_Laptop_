using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class LoginRequestModel
    {

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
