using System.Data;
using System.Data.SQLite;
using Dapper;
using RegistrationApp.Data.Entities;
using RegistrationApp.Services.Abstraction;

namespace RegistrationApp.Data
{
    public class SqliteDataAccess : ISqliteDataAccess
    {
        public List<User> LoadUsers()
        {
            using IDbConnection connection = new SQLiteConnection(LoadConnectionString());
            var outPut = connection.Query<User>("select * from Users", new DynamicParameters());
            connection.Close();
            return outPut.ToList();
        }

        public void SaveUser(User newUser)
        {
            using IDbConnection connection = new SQLiteConnection(LoadConnectionString());
            connection.Execute(
                "insert into Users (FullName, Age, City, Email, PhoneNumber) values (@FullName, @Age, @City, @Email, @PhoneNumber)",
                newUser);
            connection.Close();
        }

        public bool CheckFullName(string fullName)
        {
            using IDbConnection connection = new SQLiteConnection(LoadConnectionString());
            var outPut = connection.QueryFirstOrDefault<User>("select * from Users where FullName == @FullName", new {FullName = fullName});
            connection.Close();
            return outPut != null;
        }

        private static string LoadConnectionString()
        {
            return
                @"Data Source = UsersData.db";
        }
    }
}

