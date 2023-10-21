using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using TodoManager.Common.Contracts.Repositories;
using TodoManager.Common.Models.Groups;
using Dapper;
using TodoManager.Common.Models.Users;

namespace TodoManager.Infrastructure.Dapper.Repository;

public class GroupRepository : IGroupRepository
{
    private readonly string _connString;

    public GroupRepository(IConfiguration config)
    {
        _connString = config.GetConnectionString("Default");
    }

    private IDbConnection Connection => new SqlConnection(_connString);

    public async Task AddMember(int userId, int groupId)
    {
        const string sql = "INSERT INTO [UserGroupRelation] (UserId, GroupId) VALUES ( @UserId, @GroupId );";

        using var connection = Connection;

        await Connection.ExecuteAsync(sql, new { UserId = userId, GroupId = groupId });
    }

    public async Task RemoveMember(int userId, int groupId)
    {
        const string sql = "DELETE FROM [UserGroupRelation] WHERE UserId = @UserId AND GroupId = @GroupId";

        using var connection = Connection;

        await Connection.ExecuteAsync(sql, new { UserId = userId, GroupId = groupId });
    }

    public async Task<int> Create(GroupCreateRequest request, int requestedBy)
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

        return await Connection.ExecuteScalarAsync<int>(sql, new { Name = request.Name, OwnerId = requestedBy });
    }

    public async Task Delete(int groupId)
    {
        const string sql = "DELETE FROM [Group] WHERE GroupId = @GroupId;";

        using var connection = Connection;

        await Connection.ExecuteAsync(sql, new { GroupId = groupId });
    }

    public async Task<IEnumerable<Group>> GetAllByUser(int userId)
    {
        const string sql = "SELECT g.GroupId, g.[Name], g.OwnerId " +
            "FROM [UserGroupRelation] as ug INNER JOIN [Group] as g ON ug.GroupId = g.GroupId " +
            "WHERE UserId = @UserId;";

        using var connection = Connection;

        return await connection.QueryAsync<Group>(sql, new { UserId = userId });
    }

    public async Task<Group?> GetById(int groupId)
    {
        const string sql = "SELECT * FROM [Group] WHERE GroupId = @GroupId";

        using var connection = Connection;

        return await connection.QueryFirstOrDefaultAsync<Group>(sql, new { GroupId = groupId});
    }

    public async Task<IEnumerable<User>> GetGroupMembers(int groupId)
    {
        const string sql = "SELECT u.UserId, u.UserName, u.FirstName, u.LastName, u.[Password], u.EmailAddress " +
            "FROM [UserGroupRelation] AS ug " +
            "INNER JOIN [User] AS u ON ug.UserId = u.UserId " +
            "WHERE ug.GroupId = @GroupId;";

        using var connection = Connection;

        return await connection.QueryAsync<User>(sql, new { GroupId = groupId });
    }

    public async Task Update(GroupUpdateRequest request, int groupId)
    {
        const string sql = "UPDATE [Group] " +
            "SET Name = @Name " +
            "WHERE GroupId = @GroupId;";

        using var connection = Connection;

        await connection.ExecuteAsync(sql, new { Name = request.Name, GroupId = groupId });
    }
}
