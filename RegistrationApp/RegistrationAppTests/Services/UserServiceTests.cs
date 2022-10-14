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
        public async Task GetUsersListTest()
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
                x!.LoadUsersAsync(String.Empty)).ReturnsAsync(listUsers);

            var resultUsers = await _sut.GetUsersListAsync(String.Empty);
            var result = resultUsers.ToArray();

            Assert.IsNotNull(resultUsers);
            Assert.AreEqual(2, resultUsers.Count());
            Assert.AreEqual(20, result[0].Age);
            Assert.AreEqual("+380111111111", result[0].PhoneNumber);
            Assert.AreEqual("TestOne TestTwo TestThree", result[1].FullName);
        }

        [TestMethod()]
        public async Task SaveUserNullTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _sut.SaveUserAsync(null!));
        }


        //TODO : Review test
        [TestMethod()]
        public async Task SaveUserTest()
        {
           await _sut.SaveUserAsync(user);

        }

        [TestMethod()]
        public async Task IsUserExistTest()
        {
            _dataAccessMock.Setup(x
                => x!.IsFullNameExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var result = await _sut.IsUserExistAsync("user");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, true);

        }
    }
}