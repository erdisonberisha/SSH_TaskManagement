namespace TaskManagementAPI.Models
{
    public class SearchModel
    {
        public string? Query { get; set; }
        public int? CategoryId { get; set; }
        public PriorityType? Priority { get; set; }
        public StatusType? Status { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}