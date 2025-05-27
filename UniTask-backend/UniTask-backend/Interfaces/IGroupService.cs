using UniTask_backend.DTO;
using UniTask_backend.Entities;

namespace UniTask_backend.Interfaces
{
    public interface IGroupService
    {

        Task<(bool Success, string? ErrorMessage, Guid? GroupId)> CreateGroup(string name, Guid ownerId);

        Task<(bool Success, string? ErrorMessage)> AddMemberToGroup(Guid userId, Guid groupId);

        Task<(bool Success, string? ErrorMessage, List<GroupDTO> Groups)> GetGroupsByUserId(Guid userId);

        
    }
}
