using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ShareView.DTO
{
    public class RegisterRequestModel
    {
        [Required(ErrorMessage = "Username is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, ErrorMessage = "Password length must be between 3 and 20 characters.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare("Password", ErrorMessage = "Password confirmation does not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [StringLength(10, ErrorMessage = "Phone number must be under 10 characters")]
        public string PhoneNumber { get; set; }
    }


    public class RegisterResponseModel
    {
        public object Value { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
