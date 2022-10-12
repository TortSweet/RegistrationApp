﻿using RegistrationApp.Data.Entities;

namespace RegistrationApp.Services.Abstraction
{
    public interface ISqliteDataAccess
    {
        public List<User> LoadUsers();
        public bool SaveUser(User newUser);
        public bool CheckFullName(string fullName);
    }
}
