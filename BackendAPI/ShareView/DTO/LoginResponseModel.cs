using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ShareView.DTO
{
    public class LoginResponseModel
    {
        public string Value { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
