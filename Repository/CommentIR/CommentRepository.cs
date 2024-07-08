using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Dtos.Comment;

namespace api.Repository.CommentIR
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async  Task<Comment> StoreAsync(Comment model)
        {
            await _context.Comments.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Comment?> UpdatewithMapperAsync(int id, Comment model)
        {
            var result = await _context.Comments.FindAsync(id);

            if(result == null)
            {
                return null;
            }
            
            result.Title = model.Title;
            result.Content = model.Content;
            
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequest request)
        {
           var result = await _context.Comments.FindAsync(id);

            if(result == null)
            {
                return null;
            }
            
            result.Title = request.Title;
            result.Content = request.Content;
            
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var model = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if(model == null)
            {
                return null;
            }
            _context.Comments.Remove(model);
            await _context.SaveChangesAsync();

            return model;
        }
    }
}