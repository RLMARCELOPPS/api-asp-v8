using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Repository.StockIR
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> StoreAsync(Stock model);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequest request);
        Task<Stock?> Delete(int id);
        Task<bool> StockExists(int id);
    }
}