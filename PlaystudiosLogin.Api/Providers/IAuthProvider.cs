using PlaystudiosLogin.Api.Models;

namespace PlaystudiosLogin.Api.Providers
{
    public interface IAuthProvider
    {
        Task<string> GetResetPassToken(string email);
        Task<UserInfo> GetUserInfo(string email);
        Task<string> Login(LoginRequest request);
        Task<bool> ResetPassword(ResetPasswordRequest request);
        Task<bool> SignUp(SignupRequest request);
    }

}