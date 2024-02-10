namespace PlaystudiosLogin.Api.Providers
{
    public interface IEmailProvider
    {
        Task<bool> SendResetPassword(string email, string token);
    }
}