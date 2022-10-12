﻿using Microsoft.AspNetCore.Mvc;
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

        private readonly User user = new User()
        {
            Age = 20,
            City = "Kyiv",
            Email = "test@test.com",
            FullName = "Test Test Test",
            Id = 1,
            PhoneNumber = "+380111111111"
        };

        [TestMethod()]
        public void IndexTest()
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

            _serviceMock.Setup(x => x!.GetUsersList()).Returns(listUsers.AsQueryable);

            var result = _sut.Index("") as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod()]
        public void CreateUserTest()
        {
            _serviceMock.Setup(x => x!.SaveUser(It.IsAny<User>())).Returns(true);
            var result = _sut.CreateUser(user);

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(RedirectToActionResult), result.GetType());
        }

        [TestMethod()]
        public void CheckFullNameTest()
        {
            _serviceMock.Setup(x
                => x!.IsUserExist(It.IsAny<string>())).Returns(true);

            var result = _sut.CheckFullName("user");

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, true);
        }
    }
}