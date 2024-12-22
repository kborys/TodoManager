using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using TodoManager.Application.Interfaces.Repositories;
using TodoManager.Application.Models.Authentication;
using TodoManager.Application.Models.Users;

namespace TodoManager.Infrastructure.Dapper.Repository;

internal class UserRepository : IUserRepository
{
    private readonly string _connString;

    public UserRepository(IConfiguration config)
    {
        _connString = config.GetConnectionString("Default")
            ?? throw new ArgumentNullException(nameof(config));
    }

    private IDbConnection Connection => new SqlConnection(_connString);

    public async Task<int> Create(User user)
    {
        const string sql = @"
        INSERT INTO [User] (UserName, FirstName, LastName, PasswordHash, EmailAddress)
        OUTPUT INSERTED.UserId
        VALUES (@UserName, @FirstName, @LastName, @PasswordHash, @EmailAddress);";

        using var connection = Connection;

        return await connection.ExecuteScalarAsync<int>(sql, user);
    }

    public async Task<User?> GetById(int id)
    {
        const string sql = "SELECT * FROM [User] WHERE UserId = @UserId;";

        using var connection = Connection;

        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { UserId = id });
    }

    public async Task<User?> GetByUserName(string userName)
    {
        const string sql = "SELECT * FROM [User] WHERE UserName = @UserName;";

        using var connection = Connection;

        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { UserName = userName });
    }

    public async Task Update(User user)
    {
        const string sql = "UPDATE [User] " +
            "SET FirstName = @FirstName, LastName = @LastName, PasswordHash = @PasswordHash " +
            "WHERE UserId = @UserId;";

        using var connection = Connection;

        await connection.ExecuteAsync(sql, user);
    }

    public async Task Delete(int id)
    {
        const string sql = "DELETE FROM [User] WHERE UserId = @UserId;";

        IDbConnection connection = new SqlConnection(_connString);

        await connection.ExecuteAsync(sql, new { UserId = id});
    }

    public async Task<int> Count(string userName, string emailAddress)
    {
        const string sql = "SELECT COUNT(*) " +
            "FROM [User] " +
            "WHERE UserName = @UserName OR EmailAddress = @EmailAddress";

        using var connection = Connection;

        return await connection.ExecuteScalarAsync<int>(sql, new { UserName = userName, EmailAddress = emailAddress });
    }
}
