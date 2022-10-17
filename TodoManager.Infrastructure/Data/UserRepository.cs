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

    private IDbConnection Connection => new SqlConnection(_connString);

    public async Task<int> Create(CreateRequest request)
    {
        const string sql = $"DECLARE @InsertedRows AS TABLE (Id int);" +
            $"INSERT INTO {_table} (UserName, FirstName, LastName, Password, EmailAddress) " +
            $"OUTPUT INSERTED.UserId INTO @InsertedRows " +
            $"VALUES (@UserName, @FirstName, @LastName, @Password, @EmailAddress); " +
            $"SELECT Id FROM @InsertedRows";

        using var connection = Connection;

        return await connection.ExecuteScalarAsync<int>(sql, request);
    }

    public async Task<User?> GetById(int id)
    {
        const string sql = $"SELECT * FROM {_table} WHERE UserId = @UserId;";

        using var connection = Connection;

        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { UserId = id });
    }

    public async Task<User?> GetByUserName(string userName)
    {
        const string sql = $"SELECT * FROM {_table} WHERE UserName = @UserName;";

        using var connection = Connection;

        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { UserName = userName });
    }

    public async Task Update(User user)
    {
        const string sql = $"UPDATE {_table} " +
            $"SET FirstName = @FirstName, LastName = @LastName, Password = @Password " +
            $"WHERE UserId = @UserId;";

        using var connection = Connection;

        await connection.ExecuteAsync(sql, user);
    }

    public async Task Delete(int id)
    {
        const string sql = $"DELETE FROM {_table} WHERE UserId = @UserId;";

        IDbConnection connection = new SqlConnection(_connString);

        await connection.ExecuteAsync(sql, new { UserId = id});
    }

    public async Task<int> Count(string userName, string emailAddress)
    {
        const string sql = $"SELECT COUNT(*) " +
            $"FROM {_table} " +
            $"WHERE UserName = @UserName OR EmailAddress = @EmailAddress";

        using var connection = Connection;

        return await connection.ExecuteScalarAsync<int>(sql, new { UserName = userName, EmailAddress = emailAddress });
    }
}
