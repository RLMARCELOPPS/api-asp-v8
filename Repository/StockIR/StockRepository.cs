using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.StockIR
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<Stock>> GetAllAsync()
        {
            return  await _context.Stocks.Include( c => c.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include( c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Stock> StoreAsync(Stock model)
        {
            await _context.Stocks.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequest request)
        {
           var model = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

           if(model == null)
           {
                return null;
           }
            model.Symbol = request.Symbol;
            model.CompanyName = request.CompanyName;
            model.Purchase = request.Purchase;
            model.LasDiv = request.LasDiv;
            model.Industry = request.Industry;
            model.MarketCap = request.MarketCap;
            
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<Stock?> Delete(int id)
        {
            var model = await _context.Stocks.FirstOrDefaultAsync( x => x.Id == id);

            if(model == null)
            {
                return null;
            }
            _context.Stocks.Remove(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> StockExists(int id)
        {
            return await  _context.Stocks.AnyAsync(s => s.Id == id);
        }
    }
}