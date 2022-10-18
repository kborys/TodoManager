using TodoManager.Common.Models.Groups;

namespace TodoManager.Common.Contracts.Services;

public interface IGroupService
{
    Task<Group> Create(UpdateRequest request);
    Task<Group?> GetById(int id);
    Task Update(int id, UpdateRequest request);
    Task Delete(int id);
}