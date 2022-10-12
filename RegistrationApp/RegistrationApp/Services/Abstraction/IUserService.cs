using RegistrationApp.Data.Entities;

namespace RegistrationApp.Services.Abstraction
{
    public interface IUserService
    {
        public IQueryable<User> GetUsersList();
        public void SaveUser(User user);
        public bool IsUserExist(string fullName);
    }
}
