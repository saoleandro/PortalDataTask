using PortalDataTask.Domain.Enums;

namespace PortalDataTask.Domain.Entities
{
    public class DataTask : BaseEntity
    {
        public string? Description { get; private set; }
        public DateTime ValidateDate { get; private set; }
        public StatusEnum Status { get; private set; }
                
        public DataTask(long id, string? description, DateTime validateDate, StatusEnum status, DateTime createdAt, DateTime? updatedAt)
            : base(id, createdAt, updatedAt)
        {
            Description = description;
            ValidateDate = validateDate;
            Status = status;
        }

        public void SetStatus(StatusEnum status)
        {
            Status = status;
        }
    }
}
