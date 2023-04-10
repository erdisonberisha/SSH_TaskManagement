using System.Text.Json.Serialization;

namespace TaskManagementAPI.Models.Dto
{
    public class TaskCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusType Status { get; set; } = StatusType.INPROGRESS;
        public PriorityType? PriorityOfTask { get; set; }
        public int? CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        public DateTime DueDate { get; set; }
    }
}
