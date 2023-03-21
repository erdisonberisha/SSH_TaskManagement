using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManagementAPI.Models
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(15)]
        public StatusType Status { get; set; }
        public PriorityType PriorityOfTask { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string ProjectId { get; set; }
        public DateTime DueDate { get; set; }
    }

    public enum StatusType
    {
        In_Progress = 1,
        Completed = 2,
        Overdue = 3
    }

    public enum PriorityType
    {
        LOW = 1,
        MEDIUM = 2,
        HIGH = 3
    }
}