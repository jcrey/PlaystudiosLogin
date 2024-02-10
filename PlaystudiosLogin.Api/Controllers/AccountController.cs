using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlaystudiosLogin.Api.Models;
using PlaystudiosLogin.Api.Providers;
using System.Security.Claims;

namespace PlaystudiosLogin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IEmailProvider _emailProvider;
        private readonly IAuthProvider _authProvider;

        public AccountController(IAuthProvider authProvider, IEmailProvider emailProvider)
        {
            _authProvider = authProvider;
            _emailProvider = emailProvider;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup(SignupRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _authProvider.SignUp(request);

            if (success)
            {
                return Ok(new SignupResponse
                {
                    Success = true,
                    Message = "Signup success"
                });                
            }

            return BadRequest(new SignupResponse
            {
                Success = false,
                Message = "User already exist"
            });

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var jwt = await _authProvider.Login(request);

            if (string.IsNullOrEmpty(jwt))
            {
                return Unauthorized(new LoginResult()
                {
                    Success = false,
                    Message = "Invalid Email or Password."
                });
            }

            return Ok(new LoginResult()
            {
                Success = true,
                Message = "Login successful",
                Token = jwt
            });
        }
      
        [HttpGet("Settings")]
        [Authorize(Roles = "RegisteredUser")]
        public async Task<IActionResult> Settings()
        {
            var claimIdentity = User.Identity as ClaimsIdentity;
            var email = claimIdentity?.Claims?.FirstOrDefault()?.Value;

            var result = await _authProvider.GetUserInfo(email);

            return Ok(result);
        }

        [HttpPost("SendResetPasswordEmail")]
        public async Task<IActionResult> SendResetPasswordEmail(ResetPasswordRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var token = await _authProvider.GetResetPassToken(request.Email);
            var success = await _emailProvider.SendResetPassword(request.Email, token);

            return Ok(new SignupResponse
            {
                Success = success,
                Message = "Email sent"
            });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _authProvider.ResetPassword(request);

            return Ok(new SignupResponse
            {
                Success = success,
                Message = success? "Reset done" : "Reset error"
            });
        }
    }
}
