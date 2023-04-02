using TaskManagementAPI.Data.Repository.Intefaces;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data.Repository
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(TaskManagementDbContext context) : base(context) { }
    }
}
