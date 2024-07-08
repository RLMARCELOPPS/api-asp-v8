using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock model)
        {
            return new StockDto
            {
                Id = model.Id,
                Symbol = model.Symbol,
                CompanyName = model.CompanyName,
                Purchase = model.Purchase,
                LasDiv  = model.LasDiv,
                Industry = model.Industry,
                MarketCap = model.MarketCap,
                Comments = model.Comments.Select( c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockFromStoreDTO(this StoreStockRequest request)
        {
            return new Stock
            {
                Symbol = request.Symbol,
                CompanyName = request.CompanyName,
                Purchase = request.Purchase,
                LasDiv = request.LasDiv,
                Industry = request.Industry,
                MarketCap = request.MarketCap
            };
        }

    }
}