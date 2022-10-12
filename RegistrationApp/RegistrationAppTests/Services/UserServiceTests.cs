using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RegistrationApp.Data.Entities;
using RegistrationApp.Services;
using RegistrationApp.Services.Abstraction;

namespace RegistrationAppTests.Services
{
    [TestClass()]
    public class UserServiceTests
    {
        private readonly UserService _sut;
        private readonly Mock<ISqliteDataAccess?> _dataAccessMock = new();

        public UserServiceTests()
        {
            _sut = new UserService(_dataAccessMock.Object!);
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
        public void CreateEntryNullConstructorTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new UserService(null!));
        }

        [TestMethod()]
        public void GetUsersListTest()
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

            _dataAccessMock.Setup(x =>
                x!.LoadUsers()).Returns(listUsers);

            var result = _sut.GetUsersList().ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(20, result[0].Age);
            Assert.AreEqual("+380111111111", result[0].PhoneNumber);
            Assert.AreEqual("TestOne TestTwo TestThree", result[1].FullName);
        }

        [TestMethod()]
        public void SaveUserNullTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _sut.SaveUser(null!));
        }

        [TestMethod()]
        public void SaveUserTest()
        {

            _dataAccessMock.Setup(x
                => x!.SaveUser(It.IsAny<User>())).Returns(true);

            var result = _sut.SaveUser(user);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, true);

        }

        [TestMethod()]
        public void IsUserExistTest()
        {
            _dataAccessMock.Setup(x
                => x!.CheckFullName(It.IsAny<string>())).Returns(true);

            var result = _sut.IsUserExist("user");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, true);

        }
    }
}