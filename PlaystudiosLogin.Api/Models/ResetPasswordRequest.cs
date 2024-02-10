using System.ComponentModel.DataAnnotations;

namespace PlaystudiosLogin.Api.Models
{
    public class ResetPasswordRequest: LoginRequest
    {
        [Required(ErrorMessage = "Token is required.")]
        public string Token { get; set; }
    }
}