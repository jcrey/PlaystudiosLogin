using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using PlaystudiosLogin.Api.Controllers;
using PlaystudiosLogin.Api.Models;
using PlaystudiosLogin.Api.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaystudiosLogin.Api.Tests.Controllers
{
    public class AccountControllerTest
    {
        private readonly AccountController _accountController;
        private readonly IAuthProvider _authProvider;
        private readonly IEmailProvider _emailProvider;

        public AccountControllerTest() 
        {
            _authProvider = Substitute.For<IAuthProvider>();
            _emailProvider = Substitute.For<IEmailProvider>();

            _accountController = new AccountController(_authProvider, _emailProvider);
        }

        [Fact]
        public async Task Signup_should_return_success() 
        {
            var request = new SignupRequest { 
                Email = "test@email.com",
                Name = "Test",
                Password = "Sampl3Pa$$"
            };
            _authProvider.SignUp(request).Returns(true);
            
            var result = await _accountController.Signup(request) as OkObjectResult;
            var response = result?.Value as SignupResponse;

            Assert.True(response?.Success);
            await _authProvider.Received(1).SignUp(request);
        }

        [Fact]
        public async Task Login_should_return_a_token()
        {
            var request = new LoginRequest
            {
                Email = "test@email.com",
                Password = "Sampl3Pa$$"
            };
            _authProvider.Login(request).Returns("ASF3$%DFG");

            var result = await _accountController.Login(request) as OkObjectResult;
            var response = result?.Value as LoginResult;

            Assert.NotEmpty(response?.Token);
            await _authProvider.Received(1).Login(request);
        }

        [Fact]
        public async Task ResetPassword_should_return_success()
        {
            var request = new ResetPasswordRequest
            {
                Email = "test@email.com",
                Password = "Sampl3Pa$$"
            };
            _authProvider.ResetPassword(request).Returns(true);

            var result = await _accountController.ResetPassword(request) as OkObjectResult;
            var response = result?.Value as SignupResponse;

            Assert.True(response?.Success);
            await _authProvider.Received(1).ResetPassword(request);
        }

    }
}
