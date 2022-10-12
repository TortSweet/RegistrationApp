using RegistrationApp.Data;
using RegistrationApp.Data.Entities;
using RegistrationApp.Services.Abstraction;

namespace RegistrationApp.Services
{
    public class UserService : IUserService
    {
        private readonly ISqliteDataAccess _dataAccess;

        public UserService(ISqliteDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IQueryable<User> GetUsersList()
        {
            var users = _dataAccess.LoadUsers().AsQueryable();

            return users;
        }

        public void SaveUser(User newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException(nameof(newUser), "New user must exist");
            }
            _dataAccess.SaveUser(newUser);
        }

        public bool IsUserExist(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName), "fullName must exist");
            }
            return _dataAccess.CheckFullName(fullName);
        }
    }
}