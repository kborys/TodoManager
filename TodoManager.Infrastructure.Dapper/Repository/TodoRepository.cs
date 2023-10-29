using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using TodoManager.Application.Interfaces.Repositories;
using TodoManager.Application.Models.Todos;

namespace TodoManager.Infrastructure.Dapper.Repository;

internal class TodoRepository : ITodoRepository
{
    private readonly string _connString;

    public TodoRepository(IConfiguration config)
    {
        _connString = config.GetConnectionString("Default")
            ?? throw new ArgumentNullException(nameof(config));
    }

    private IDbConnection Connection => new SqlConnection(_connString);

    public async Task<int> Create(TodoCreateRequest request)
    {
        const string sql = 
            "DECLARE @InsertedRows AS TABLE (Id int);" +
            "INSERT INTO [Todo] (Title, Description, GroupId, OwnerId, Status) " +
            "OUTPUT INSERTED.TodoId INTO @InsertedRows " +
            "VALUES (@Title, @Description, @GroupId, @OwnerId, @Status); " +
            "SELECT Id FROM @InsertedRows";
        using var connection = Connection;

        return await Connection.ExecuteScalarAsync<int>(sql, request);
    }

    public async Task<IEnumerable<Todo>> GetAllByGroup(int groupId)
    {
        const string sql = "SELECT * FROM [Todo] WHERE GroupId = @GroupId AND Status != 1;";
        using var connection = Connection;

        return await connection.QueryAsync<Todo>(sql, new { GroupId = groupId});
    }

    public async Task<Todo?> GetById(int todoId)
    {
        const string sql = "SELECT * FROM [Todo] WHERE TodoId = @TodoId;";
        using var connection = Connection;

        return await connection.QueryFirstOrDefaultAsync<Todo?>(sql, new { TodoId = todoId });
    }

    public async Task Update(Todo todo)
    {
        const string sql = "UPDATE [Todo] " +
            "SET Title = @Title, Description = @Description, GroupId = @GroupId, OwnerId = @OwnerId, Status = @Status " +
            "WHERE TodoId = @TodoId;";
        using var connection = Connection;

        await connection.ExecuteAsync(sql, todo);
    }

    public async Task Delete(int todoId)
    {
        const string sql = "DELETE FROM [Todo] WHERE TodoId = @TodoId;";
        using var connection = Connection;

        await connection.ExecuteAsync(sql, new {TodoId = todoId});
    }
}
