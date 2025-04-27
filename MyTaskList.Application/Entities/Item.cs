namespace MyTaskList.Application.Entities;

public class Item
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool Done { get; set; } = false;
}