using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> DeleteComment(int id, int userId)
        {
            var comment = await GetCommentById(id);
            if (comment != null && comment.UserId == userId)
            {
                _unitOfWork.CommentRepository.Delete(comment);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }

        public async Task<Comment?> EditComment(int id, JsonPatchDocument<Comment> task, int userId)
        {
            var commentEntity = await GetCommentById(userId);
            if (commentEntity is null)
            {
                throw new InvalidOperationException($"Commend with id {id} and user id {userId} not found.");
            }
            task.ApplyTo(commentEntity);
            await _unitOfWork.CompleteAsync();
            return commentEntity;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsForTask(int taskId)
        {
            var comments = await _unitOfWork.CommentRepository.GetByCondition(x => x.TaskId == taskId).ToListAsync();
            return comments;
        }

        public async Task<Comment?> GetCommentById(int id)
        {
            var comment = await _unitOfWork.CommentRepository.GetById(x => x.Id == id).FirstOrDefaultAsync();
            if (comment is null)
            {
                throw new InvalidOperationException($"Commend with id {id} was not found.");
            }
            return comment;
        }

        public async Task WriteComment(CommentDto commentDto, int userId)
        {
            var commentToInsert = new Comment
            {
                Content = commentDto.Content,
                UserId = userId,
                TaskId= commentDto.TaskId
            };
            await _unitOfWork.CommentRepository.CreateAsync(commentToInsert);
            await _unitOfWork.CompleteAsync();
        }
    }
}
