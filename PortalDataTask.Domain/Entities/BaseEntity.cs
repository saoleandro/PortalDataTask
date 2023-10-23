namespace PortalDataTask.Domain.Entities;

public class BaseEntity
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public BaseEntity(DateTime createdAt, DateTime? updatedAt)
    {
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public BaseEntity(long id, DateTime createdAt, DateTime? updatedAt)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
