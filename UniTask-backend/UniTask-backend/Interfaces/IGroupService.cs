using UniTask_backend.DTO;

namespace UniTask_backend.Interfaces
{
    public interface IGroupService
    {
        (bool Success, string? ErrorMessage, Guid? GroupId) CreateGroup(string name, Guid ownerId);

        (bool Success, string? ErrorMessage) AddMemberToGroup(Guid userId, Guid groupId);

        (bool Success, string? ErrorMessage, List<GroupDTO> Groups) GetGroupsByUserId(Guid userId);
    }
}
