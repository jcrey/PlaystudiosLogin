using System.ComponentModel.DataAnnotations;

namespace PlaystudiosLogin.Api.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression("^[a-zA-Z0-9@#$%&*()-_+]{8,15}$")]
        public string Password { get; set; } = null!;
    }
}
