using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using TodoManager.Common.Contracts.Repositories;
using TodoManager.Common.Models.Groups;
using Dapper;

namespace TodoManager.Infrastructure.Data;

public class GroupRepository : IGroupRepository
{
    private readonly string _connString;

    public GroupRepository(IConfiguration config)
    {
        _connString = config.GetConnectionString("Default");
    }

    private IDbConnection Connection => new SqlConnection(_connString);
    public async Task<int> Create(GroupCreateRequest request)
    {
        const string sql = 
            "DECLARE @InsertedRows AS TABLE (Id int);" +
            "INSERT INTO [Group] (Name, OwnerId)" +
            "OUTPUT INSERTED.GroupId INTO @InsertedRows " +
            "VALUES (@Name, @OwnerId);" +

            "INSERT INTO [UserGroupRelation] (UserId, GroupId)" +
            "VALUES (@OwnerId, (SELECT Id FROM @InsertedRows) );" +

            "SELECT Id FROM @InsertedRows; ";

        using var connection = Connection;

        return await Connection.ExecuteScalarAsync<int>(sql, request);
    }

    public async Task Delete(int groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Group>> GetAllByUser(int userId)
    {
        const string sql = "SELECT g.GroupId, g.[Name], g.OwnerId " +
            "FROM [UserGroupRelation] as ug INNER JOIN [Group] as g ON ug.GroupId = g.GroupId " +
            "WHERE UserId = @UserId;";

        using var connection = Connection;

        return await connection.QueryAsync<Group>(sql, new { UserId = userId });
    }
}
