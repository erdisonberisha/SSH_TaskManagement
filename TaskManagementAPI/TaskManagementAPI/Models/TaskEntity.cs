using Newtonsoft.Json;

namespace TaskManagementAPI.Models
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public StatusType Status { get; set; } = StatusType.TODO;
        public PriorityType? PriorityOfTask { get; set; } = PriorityType.MEDIUM;
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int? CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        public DateTime DueDate { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }

}
public enum StatusType
{
    TODO = 0,
    INPROGRESS = 1,
    TESTING = 2,
    COMPLETED = 3,
    OVERDUE = 4
}

public enum PriorityType
{
    LOW = 1,
    MEDIUM = 2,
    HIGH = 3
}