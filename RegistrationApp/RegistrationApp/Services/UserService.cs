using RegistrationApp.Data;
using RegistrationApp.Data.Entities;
using RegistrationApp.Services.Abstraction;

namespace RegistrationApp.Services
{
    public class UserService : IUserService
    {
        public IQueryable<User> GetUsersList()
        {
            var users = SqliteDataAccess.LoadUsers().AsQueryable();
            return users;
        }

        public void SaveUser(string fullName, int age, string city, string email, string phoneNumber)
        {
            var newUser = new User()
            {
                FullName = fullName,
                Age = age,
                City = city,
                Email = email,
                PhoneNumber = phoneNumber,

            };
            SqliteDataAccess.SaveUser(newUser);
        }
    }
}