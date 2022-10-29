using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ShareView.DTO
{
    public class RegisterRequestModel
    {
        [Required(ErrorMessage = "Name should not be empty")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "Password should not be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }


    public class RegisterResponseModel
    {
        public object Value { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
