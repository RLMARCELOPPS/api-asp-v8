using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Helpers.Filter;
using api.Models;

namespace api.Repository.CommentIR
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(CommentQuery query);

        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> StoreAsync(Comment model);
        //with mapper request
        Task<Comment?> UpdatewithMapperAsync(int id, Comment model);
        //with no mapper request
        Task<Comment?> UpdateAsync(int id, UpdateCommentRequest request);
        Task<Comment?> DeleteAsync(int id);
    }
}