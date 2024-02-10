using Microsoft.AspNetCore.Identity;
using PlaystudiosLogin.Api.Common;
using PlaystudiosLogin.Api.Data;
using PlaystudiosLogin.Api.Models;
using System.IdentityModel.Tokens.Jwt;

namespace PlaystudiosLogin.Api.Providers
{
    public class AuthProvier : IAuthProvider
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly JwtHandler _jwtHandler;

        public AuthProvier(AppDbContext context, UserManager<User> userManager, JwtHandler jwtHandler)
        {
            _context = context;
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        public async Task<string> GetResetPassToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        }

        public async Task<UserInfo> GetUserInfo(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return new UserInfo
            {
                Name = user.UserName,
                Email = user.Email,
                ResetPasswordToken = token
            };
        }

        public async Task<string> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return string.Empty;

            var secToken = await _jwtHandler.GetTokenAsync(user);
            var jwt = new JwtSecurityTokenHandler().WriteToken(secToken);

            return jwt;
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            return result.Succeeded;
        }

        public async Task<bool> SignUp(SignupRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return false;

            string role = "RegisteredUser";

            var user = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Name,
                Email = request.Email
            };

            await _userManager.CreateAsync(user, request.Password);
            await _userManager.AddToRoleAsync(user, role);

            user.EmailConfirmed = true;
            user.LockoutEnabled = false;


            await _context.SaveChangesAsync();

            return true;
        }
    }

}