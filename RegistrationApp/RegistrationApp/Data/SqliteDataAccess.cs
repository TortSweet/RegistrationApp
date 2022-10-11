using System.Data;
using System.Data.SQLite;
using Dapper;
using RegistrationApp.Data.Entities;

namespace RegistrationApp.Data
{
    public class SqliteDataAccess
    {
        public static List<User> LoadUsers()
        {
            using IDbConnection connection = new SQLiteConnection(LoadConnectionString());
            var outPut = connection.Query<User>("select * from Users", new DynamicParameters());
            return outPut.ToList();
        }

        public static void SaveUser(User newUser)
        {
            using IDbConnection connection = new SQLiteConnection(LoadConnectionString());
            connection.Execute(
                "insert into Users (FullName, Age, City, Email, PhoneNumber) values (@FullName, @Age, @City, @Email, @PhoneNumber)",
                newUser);
        }

        private static string LoadConnectionString()
        {
            return
                @"Data Source = UsersData.db";
        }
    }
}

