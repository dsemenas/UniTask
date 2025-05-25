namespace UniTask_backend.DTO
{
    public class AddMembersToGroupRequest
    {
        public Guid UserId { get; set; }

        public Guid GroupId { get; set; }
    }
}
