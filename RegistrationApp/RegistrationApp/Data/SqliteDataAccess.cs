using System.Data;
using System.Data.SQLite;
using Dapper;
using Microsoft.Extensions.Options;
using RegistrationApp.Data.Entities;
using RegistrationApp.Services.Abstraction;
using RegistrationApp.Settings;

namespace RegistrationApp.Data
{
    public class SqliteDataAccess : ISqliteDataAccess
    {
        private readonly SqlLiteConnection _sqlLiteConnection;
        public SqliteDataAccess(IOptions<SqlLiteConnection> options)
        {
            _sqlLiteConnection = options.Value;
        }

        public async Task<IEnumerable<User>> LoadUsersAsync(string sortingProperty)
        {
            using var connection = new SQLiteConnection(_sqlLiteConnection.ConnectionString);
            var outPut = (await connection.QueryAsync<User>("select * from Users", new DynamicParameters())).AsQueryable();

            outPut = SortUsers(outPut, sortingProperty);

            await connection.CloseAsync();
            return outPut.ToArray();
        }

        public async Task SaveUserAsync(User newUser)
        {
            using var connection = new SQLiteConnection(_sqlLiteConnection.ConnectionString);
            var savedUser = await connection.ExecuteAsync(
                "insert into Users (FullName, Age, City, Email, PhoneNumber) values (@FullName, @Age, @City, @Email, @PhoneNumber)",
                newUser);
            connection.Close();
        }

        public async Task<bool> IsFullNameExistsAsync(string fullName)
        {
            using var connection = new SQLiteConnection(_sqlLiteConnection.ConnectionString);
            var outPut = await connection.QueryFirstOrDefaultAsync<User>("select * from Users where FullName == @FullName", new {FullName = fullName});
            connection.Close();
            return outPut != null;
        }

        private static IQueryable<User> SortUsers(IQueryable<User> userList, string property)
        {
            userList = property switch
            {
                "FullName" => userList.OrderBy(item => item.FullName),
                "Id" => userList.OrderBy(item => item.Id),
                "Age" => userList.OrderBy(item => item.Age),
                "City" => userList.OrderBy(item => item.City),
                "Email" => userList.OrderBy(item => item.Email),
                "PhoneNumber" => userList.OrderBy(item => item.PhoneNumber),
                _ => userList
            };

            return userList;
        }
    }
}

