namespace UniTask_backend.DTO;

public class GetAllTasksDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Description { get; set; }
    
    public string AssignedTo { get; set; }

    public int Version { get; set; }

    public TaskStatus Status { get; set; }
}