using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Mappers;
using api.Repository.StockIR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDbContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context;
        }

        //GET ALL RECORD
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var data =  await _stockRepo.GetAllAsync(query);
            var results = data.Select( s => s.ToStockDto());
            return Ok(results);
        }

        //GET SPECIFIC RECORD
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _stockRepo.GetByIdAsync(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result.ToStockDto());
        }

        //STORE RECORD
       [HttpPost]
       public async Task<IActionResult> Store([FromBody] StoreStockRequest request)
       {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = request.ToStockFromStoreDTO();

            await _stockRepo.StoreAsync(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Id}, result.ToStockDto());
       }

       //UPDATE RECORD
       [HttpPut]
       [Route("{id:int}")]
       public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequest request)
       {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var result = await  _stockRepo.UpdateAsync(id, request);

            if(result == null)
            {
                return NotFound();
            }
           
            return Ok(result.ToStockDto());
       }
        
        //DELETE RECORD
        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _stockRepo.Delete(id);

            if(result == null)
            {
                return NotFound();
            }
            return  NoContent();
        }
    }
}