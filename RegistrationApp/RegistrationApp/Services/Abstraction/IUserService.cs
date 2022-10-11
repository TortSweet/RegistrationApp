using RegistrationApp.Data.Entities;

namespace RegistrationApp.Services.Abstraction
{
    public interface IUserService
    {
        public IQueryable<User> GetUsersList();
        public void SaveUser(string fullName, int age, string city, string email, string phoneNumber);
    }
}
