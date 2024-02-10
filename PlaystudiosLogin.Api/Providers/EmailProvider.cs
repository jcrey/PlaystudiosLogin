namespace PlaystudiosLogin.Api.Providers
{
    public class EmailProvider : IEmailProvider
    {
        public EmailProvider()
        {

        }

        public async Task<bool> SendResetPassword(string email, string token)
        {


            return false;
        }
    }
}