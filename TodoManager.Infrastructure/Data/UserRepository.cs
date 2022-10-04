using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using TodoManager.Common.Entities;
using TodoManager.Common.Contracts;
using TodoManager.Common.Models.Users;

namespace TodoManager.Infrastructure.Data;

public class UserRepository : IUserRepository
{
    private readonly IConfiguration _config;
    private readonly string _connString;
    private const string _table = "[User]";

    public UserRepository(IConfiguration config)
    {
        _config = config;
        _connString = _config.GetConnectionString("Default");
    }
    public void Create(CreateRequest model)
    {
        const string sql = $"INSERT INTO {_table} (UserName, FirstName, LastName, Password) " +
            $"VALUES (@UserName, @FirstName, @LastName, @Password);";

        IDbConnection connection = new SqlConnection(_connString);

        connection.Execute(sql, model);
    }

    public User? GetById(int id)
    {
        const string sql = $"SELECT * FROM {_table} WHERE UserID = @UserId;";

        IDbConnection connection = new SqlConnection(_connString);

        return connection.QueryFirstOrDefault<User>(sql, new { UserId = id });
    }

    public User? GetByUserName(string userName)
    {
        const string sql = $"SELECT * FROM {_table} WHERE UserName = @UserName;";

        IDbConnection connection = new SqlConnection(_connString);

        return connection.QueryFirstOrDefault<User>(sql, new { UserName = userName });
    }

    public void Update(User user)
    {
        const string sql = $"UPDATE {_table} SET FirstName = @FirstName, LastName = @LastName, Password = @Password;";

        IDbConnection connection = new SqlConnection(_connString);

        connection.Execute(sql, user);
    }

    public void Delete(int id)
    {
        const string sql = $"DELETE FROM {_table} WHERE UserID = @UserId;";

        IDbConnection connection = new SqlConnection(_connString);

        connection.Execute(sql, new { UserId = id});
    }
}
