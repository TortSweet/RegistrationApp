using RegistrationApp.Data.Entities;
using RegistrationApp.Services.Abstraction;

namespace RegistrationApp.Services
{
    public class UserService : IUserService
    {
        private readonly ISqliteDataAccess _dataAccess;

        public UserService(ISqliteDataAccess dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException("Access to Db must exist", nameof(dataAccess));
        }

        public IQueryable<User> GetUsersList()
        {
            var users = _dataAccess.LoadUsers().AsQueryable();

            return users;
        }

        public bool SaveUser(User newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException(nameof(newUser), "New user must exist");
            }
            var result = _dataAccess.SaveUser(newUser);
            return result;
        }

        public bool IsUserExist(string fullName)
        {
            return _dataAccess.CheckFullName(fullName);
        }
    }
}