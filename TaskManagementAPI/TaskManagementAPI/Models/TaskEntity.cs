using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManagementAPI.Models
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public StatusType Status { get; set; } = StatusType.INPROGRESS;
        public PriorityType? PriorityOfTask { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        public DateTime DueDate { get; set; }
    }

    public enum StatusType
    {
        INPROGRESS = 1,
        COMPLETED = 2,
        OVERDUE = 3
    }

    public enum PriorityType
    {
        LOW = 1,
        MEDIUM = 2,
        HIGH = 3
    }
}