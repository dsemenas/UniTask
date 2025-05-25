namespace UniTask_backend.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
    public string PasswordHash { get; set; }

    public List<GroupUser> Members { get; set; } = new List<GroupUser>();
}