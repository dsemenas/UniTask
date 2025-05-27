using UniTask_backend.DTO;
using UniTask_backend.Entities;

namespace UniTask_backend.Interfaces
{
    public interface IGroupService
    {
        (bool Success, string? ErrorMessage, Guid? GroupId) CreateGroup(string name, string ownerName);

        (bool Success, string? ErrorMessage) AddMemberToGroup(string username, Guid groupId);

        (bool Success, string? ErrorMessage, List<GroupDTO> Groups) GetGroupsByUserId(Guid userId);
        
        (bool Success, string? ErrorMessage, List<GetUsersDTO> users) GetMembers(Guid groupId);
    }
}
