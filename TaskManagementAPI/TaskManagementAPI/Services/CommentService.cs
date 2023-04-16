using Microsoft.AspNetCore.JsonPatch;
using TaskManagementAPI.Data.UnitOfWork;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.Dto;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task EditComment(int id, JsonPatchDocument<Comment> task, int userId)
        {
           throw new NotImplementedException();
        }

        public Task WriteComment(CommentDto commentDto, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
