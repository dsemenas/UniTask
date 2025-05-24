using UniTask_backend.DTO;
using UniTask_backend.Entities;

namespace UniTask_backend.Interfaces
{
    public interface IGroupService
    {
        Task CreateGroup(GroupDTO group);
        Task AddUserToGroup(GroupUser groupuser);
    }
}
