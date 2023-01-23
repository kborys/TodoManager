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
        const string sql = 
            "DECLARE @InsertedRows AS TABLE (Id int);" +
            "INSERT INTO [Todo] (Title, Description, GroupId, OwnerId, Status) " +
            "OUTPUT INSERTED.TodoId INTO @InsertedRows " +
            "VALUES (@Title, @Description, @GroupId, @OwnerId, @Status); " +
            "SELECT Id FROM @InsertedRows";
        using var connection = Connection;

        return await Connection.ExecuteScalarAsync<int>(sql,
            new
            {
                Title = request.Title, Description = request.Description, GroupId = request.GroupId,
                OwnerId = request.OwnerId, Status = request.Status
            });
    }

    public async Task<IEnumerable<Todo>> GetAllByGroup(int groupId)
    {
        const string sql = "SELECT * FROM [Todo] WHERE GroupId = @GroupId;";
        using var connection = Connection;

        return await connection.QueryAsync<Todo>(sql, new { GroupId = groupId});
    }

    public async Task<Todo?> GetById(int todoId)
    {
        const string sql = "SELECT * FROM [Todo] WHERE TodoId = @TodoId;";
        using var connection = Connection;

        return await connection.QueryFirstOrDefaultAsync<Todo>(sql, new { TodoId = todoId });
    }

    public async Task Delete(int todoId)
    {
        throw new NotImplementedException();
    }
}
