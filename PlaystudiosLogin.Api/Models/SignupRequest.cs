using System.ComponentModel.DataAnnotations;

namespace PlaystudiosLogin.Api.Models
{
    public class SignupRequest: LoginRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }
    }
}
