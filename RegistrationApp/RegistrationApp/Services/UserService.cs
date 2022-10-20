using RegistrationApp.Data.Entities;
using RegistrationApp.Services.Abstraction;

namespace RegistrationApp.Services
{
    public class UserService : IUserService
    {
        private readonly ISqliteDataAccess _dataAccess;

        public UserService(ISqliteDataAccess dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess), "Access to Db must exist");
        }

        public async Task<IEnumerable<User>> GetUsersListAsync(string sortingProperty)
        {
            return await _dataAccess.LoadUsersAsync(sortingProperty);
        }

        public Task SaveUserAsync(User newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException(nameof(newUser), "New user must exist");
            }

            return _dataAccess.SaveUserAsync(newUser);
        }

        public Task<bool> IsUserExistAsync(string fullName)
        {
            return _dataAccess.IsFullNameExistsAsync(fullName);
        }
    }
}