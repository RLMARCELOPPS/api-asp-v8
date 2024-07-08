using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        //Response Mapper
        public static CommentDto ToCommentDto(this Comment model)
        {
            return new CommentDto 
            {
                Id = model.Id,
                Title = model.Title,
                Content = model.Content,
                CreateOn = model.CreateOn,
                StockId = model.StockId
            };
        }

        //Store Mapper
        public static Comment ToCommentFromStore(this StoreCommentRequest request, int stockId)
        {
            return new Comment 
            {
                Title = request.Title,
                Content = request.Content,
                StockId = stockId
            };
        }

        //Update Mapper
        public static Comment ToCommentFromUpdate(this UpdateCommentRequest request)
        {
            return new Comment 
            {
                Title = request.Title,
                Content = request.Content,
            };
        }
    }
}