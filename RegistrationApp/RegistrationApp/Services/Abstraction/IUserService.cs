using RegistrationApp.Data.Entities;

namespace RegistrationApp.Services.Abstraction
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetUsersListAsync(string sortingProperty);
        public Task SaveUserAsync(User newUser);
        public Task<bool> IsUserExistAsync(string fullName);
    }
}
