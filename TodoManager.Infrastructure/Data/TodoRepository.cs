using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using TodoManager.Common.Contracts.Repositories;
using TodoManager.Common.Models.Todos;

namespace TodoManager.Infrastructure.Data;

public class TodoRepository : ITodoRepository
{
    private readonly string _connString;

    public TodoRepository(IConfiguration config)
    {
        _connString = config.GetConnectionString("Default");
    }

    private IDbConnection Connection => new SqlConnection(_connString);

    public async Task<int> Create(TodoCreateRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Todo>> GetAllByGroup(int groupId)
    {
        const string sql = $"SELECT * FROM [Todo] WHERE GroupId = @GroupId;";
        using var connection = Connection;

        return await connection.QueryAsync<Todo>(sql, new { GroupId = groupId});
    }

    public async Task Delete(int todoId)
    {
        throw new NotImplementedException();
    }
}
