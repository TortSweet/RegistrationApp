using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RegistrationApp.Controllers;
using RegistrationApp.Data.Entities;
using RegistrationApp.Services.Abstraction;

namespace RegistrationAppTests.Controllers
{
    [TestClass()]
    public class HomeControllerTests
    {
        private readonly HomeController _sut;
        private readonly Mock<IUserService?> _serviceMock = new();

        public HomeControllerTests()
        {
            _sut = new HomeController(_serviceMock.Object!);
        }

        private static readonly User user = new ()
        {
            Age = 20,
            City = "Kyiv",
            Email = "test@test.com",
            FullName = "Test Test Test",
            Id = 1,
            PhoneNumber = "+380111111111"
        };

        [TestMethod()]
        public async Task IndexTest()
        {
            var listUsers = new List<User>()
            {
                user,
                new User()
                {
                    Age = 30,
                    City = "Lviv",
                    Email = "test123@test.com",
                    FullName = "TestOne TestTwo TestThree",
                    Id = 1,
                    PhoneNumber = "+380222222222"
                }
            };

            _serviceMock.Setup(x => x!.GetUsersListAsync(String.Empty)).ReturnsAsync(listUsers.AsQueryable);

            var result = await _sut.Index("") as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod()]
        public async Task CreateUserTest()
        {
            var result = await _sut.CreateUserAsync(user);

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(RedirectToActionResult), result.GetType());
        }

        [TestMethod()]
        public async Task CheckFullNameTest()
        {
            _serviceMock.Setup(x
                => x!.IsUserExistAsync(It.IsAny<string>())).ReturnsAsync(true);

            var result = await _sut.CheckFullNameAsync("user");

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, true);
        }
    }
}