using System.Text.Json.Serialization;

namespace TaskManagementAPI.Models.Dto
{
    public class TaskCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public PriorityType? PriorityOfTask { get; set; }
        public int? CategoryId { get; set; }
        public DateTime DueDate { get; set; }
    }
}
