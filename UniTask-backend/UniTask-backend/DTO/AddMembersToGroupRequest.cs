namespace UniTask_backend.DTO
{
    public class AddMembersToGroupRequest
    {
        public string Username { get; set; }

        public Guid GroupId { get; set; }
    }
}
