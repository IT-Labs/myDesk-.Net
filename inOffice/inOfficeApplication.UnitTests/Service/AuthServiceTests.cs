﻿using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using inOffice.BusinessLogicLayer;
using inOfficeApplication.Data.Interfaces.BusinessLogic;
using inOfficeApplication.Data.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;

namespace inOfficeApplication.UnitTests.Service
{
    public class AuthServiceTests
    {
        private IAuthService _authService;
        private IApplicationParmeters _applicationParmeters;
        private IWebHostEnvironment _webHostEnvironment;

        private string signingKey;
        private string adminEmail;
        private string adminPassword;

        [OneTimeSetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables()
                .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
                .Build();


            _applicationParmeters = Substitute.For<IApplicationParmeters>();
            _webHostEnvironment = Substitute.For<IWebHostEnvironment>();

            adminEmail = config.GetValue<string>("AdminEmail");
            adminPassword = config.GetValue<string>("AdminPassword");
            signingKey = config.GetValue<string>("Authentication:Local:CustomBearerTokenSigningKey");

            _applicationParmeters.GetCustomBearerTokenSigningKey(false).Returns(signingKey);

            _authService = new AuthService(_applicationParmeters, _webHostEnvironment);

        }

        [TestCase(true)]
        [Order(1)]
        public void GetToken_ValidateToken_Custom_Success(bool isAdmin)
        {

            // Arrange
            var adminPasswordArray = System.Text.Encoding.UTF8.GetBytes(adminPassword);
            var employeeDto = new EmployeeDto()
            {
                Email = adminEmail,
                Password = System.Convert.ToBase64String(adminPasswordArray),
            };

            // Act
            string token = _authService.GetToken(employeeDto, string.Empty)[7..];

            // Assert
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(signingKey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var roles = jwtToken.Claims.First(x => x.Type == "roles").Value;

            Assert.That(roles, Is.EqualTo("EMPLOYEE"));
        }
    }
}