using RegistrationApp.Data.Entities;

namespace RegistrationApp.Services.Abstraction
{
    public interface ISqliteDataAccess
    {
        public Task<IEnumerable<User>> LoadUsersAsync(string sortingProperty);
        public Task SaveUserAsync(User newUser);
        public Task<bool> IsFullNameExistsAsync(string fullName);
    }
}
