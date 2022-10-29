using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class LoginRequestModel
    {

        [Required(ErrorMessage = "Name should not be empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password should not be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
