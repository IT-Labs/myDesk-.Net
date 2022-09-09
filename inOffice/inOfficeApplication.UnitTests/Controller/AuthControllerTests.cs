﻿using inOffice.BusinessLogicLayer.Interface;
using inOfficeApplication.Controllers;
using inOfficeApplication.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System.Text;

namespace inOfficeApplication.UnitTests.Controller
{
    public class AuthControllerTests
    {
        private AuthController _authController;
        private IEmployeeService _employeeService;
        private IAuthService _authService;
        private IApplicationParmeters _applicationParmeters;

        [OneTimeSetUp]
        public void Setup()
        {
            _employeeService = Substitute.For<IEmployeeService>();
            _authService = Substitute.For<IAuthService>();
            _applicationParmeters = Substitute.For<IApplicationParmeters>();

            _authController = new AuthController(_employeeService, _authService, _applicationParmeters);
        }

        [Test]
        [Order(1)]
        public void GetToken_Success()
        {
            // Arrange
            string token = "jwt token";
            string password = "new_test";
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(password);
            string encodedPassword =  Convert.ToBase64String(plainTextBytes);

            _applicationParmeters.GetSettingsUseCustomBearerToken().Returns("true");
            EmployeeDto employeeDto = new EmployeeDto() { Email = "test", Password = encodedPassword };

            _employeeService.GetByEmailAndPassword(employeeDto.Email, password).Returns(employeeDto);
            _authService.GetToken(employeeDto).Returns(token);

            // Act
            IActionResult result = _authController.GetToken(employeeDto);

            // Assert
            Assert.IsTrue(result is OkObjectResult);
            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.IsTrue(objectResult.Value.ToString() == token);
        }

        [TestCase(false, "email", "pass")]
        [TestCase(true, "", "pass")]
        [TestCase(true, "email", "")]
        [Order(2)]
        public void GetToken_BadRequest(bool useCustomBearerToken, string email, string password)
        {
            // Arrange
            EmployeeDto employeeDto = new EmployeeDto() { Email = email, Password = password };
            _applicationParmeters.GetSettingsUseCustomBearerToken().Returns(useCustomBearerToken.ToString());

            // Act
            IActionResult result = _authController.GetToken(employeeDto);

            // Assert
            Assert.IsTrue(result is BadRequestResult);
        }

        [TestCase(true, true)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(false, false)]
        [Order(3)]
        public void Authentication_Register_Success(bool hasPassword, bool useRegister)
        {
            // Arrange
            string password = "Passvord!23";
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(password);
            string encodedPassword = Convert.ToBase64String(plainTextBytes);

            EmployeeDto employeeDto = new EmployeeDto() 
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@it-labs.com",
                JobTitle = "employee"
            };

            if (hasPassword)
            {
                employeeDto.Password = encodedPassword;
            }

            _authController.ControllerContext = new ControllerContext() { HttpContext = ControllerTestHelper.GetMockedHttpContext() };

            // Act
            IActionResult result;

            if (useRegister)
            {
                result = _authController.Register(employeeDto);
            }
            else
            {
                result = _authController.Authentication(employeeDto);
            }

            // Assert
            Assert.IsTrue(result is OkObjectResult);
            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.IsTrue(objectResult.Value.ToString() == "User created, redirect depending on the role");
            _employeeService.Received(1).Create(Arg.Is<EmployeeDto>(x => IsValidResponse(x, employeeDto, hasPassword)));
            _employeeService.ClearReceivedCalls();
        }

        [TestCase(null)]
        [TestCase(" ab ")]
        [TestCase(" test@test.com ")]
        [Order(4)]
        public void Register_ValidationFailed(string email)
        {
            // Arrange
            EmployeeDto employeeDto = new EmployeeDto() { Email = email };

            // Act
            IActionResult result = _authController.Register(employeeDto);

            // Assert
            Assert.IsTrue(result is BadRequestObjectResult);
            BadRequestObjectResult objectResult = (BadRequestObjectResult)result;
            Assert.NotNull(objectResult.Value);
            Assert.IsTrue(objectResult.Value is IEnumerable<string>);
            IEnumerable<string> values = (IEnumerable<string>)objectResult.Value;
            Assert.IsTrue(values.Any(x => x == "Employee must have an email address.") || 
                values.Any(x => x == "Email length should be between 3 and 254.") || values.Any(x => x == "Invalid email adress."));
            _employeeService.DidNotReceive().GetByEmail(Arg.Any<string>());
            _employeeService.DidNotReceive().Create(Arg.Any<EmployeeDto>());
        }

        [Test]
        [Order(5)]
        public void Register_EmployeeAlreadyExists()
        {
            // Arrange
            EmployeeDto employeeDto = new EmployeeDto() { Email = "test@it-labs.com" };
            _employeeService.GetByEmail(employeeDto.Email).Returns(employeeDto);

            // Act
            IActionResult result = _authController.Register(employeeDto);

            // Assert
            Assert.IsTrue(result is OkObjectResult);
            OkObjectResult objectResult = (OkObjectResult)result;
            Assert.IsTrue(objectResult.Value.ToString() == "User allready exists, redirect depending on the role");
            _employeeService.DidNotReceive().Create(Arg.Any<EmployeeDto>());
        }

        private bool IsValidResponse(EmployeeDto result, EmployeeDto employeeDto, bool hasPassword)
        {
            bool validBasicData = result.FirstName == employeeDto.FirstName && result.LastName == employeeDto.LastName &&
                result.Email == employeeDto.Email && result.JobTitle == employeeDto.JobTitle;

            if (hasPassword)
            {
                byte[] data = Convert.FromBase64String(employeeDto.Password);
                string decodedPassword = Encoding.UTF8.GetString(data);

                return validBasicData && BCrypt.Net.BCrypt.Verify(decodedPassword, result.Password);
            }
            else
            {
                return validBasicData && result.IsAdmin == false;
            }
        }
    }
}