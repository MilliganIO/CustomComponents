namespace FluentUI.Models;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CreatedBy { get; set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public Guid? UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
}
