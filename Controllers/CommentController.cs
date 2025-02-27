using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Helpers.Filter;
using api.Mappers;
using api.Models;
using api.Repository.CommentIR;
using api.Repository.StockIR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;


        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll([FromQuery] CommentQuery query)
        {
            //No Need for get
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _commentRepo.GetAllAsync(query);
            var results = data.Select( s => s.ToCommentDto());
            return Ok(results);
        }

        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {

            //No Need for get
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _commentRepo.GetByIdAsync(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result.ToCommentDto());
        }

        [HttpPost("{stockId}")]

        public async Task<IActionResult> Store([FromRoute] int stockId, [FromBody] StoreCommentRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

           if(!await _stockRepo.StockExists(stockId))
           {
            return BadRequest("Stock does not exist");
           }

            var model =  request.ToCommentFromStore(stockId); //Validation
            
            await _commentRepo.StoreAsync(model);

            return CreatedAtAction(nameof(GetById), new { id = model.Id}, model.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequest request)
        {
            //If you want to format the request use this pag gusto mo format yung request before save use mapper
            // var result = await _commentRepo.UpdatewithMapperAsync(id, request.ToCommentFromUpdate());

            //kung gusto mo direct sa request and save na agad ito gamitin
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _commentRepo.UpdateAsync(id, request);

            if(result == null)
            {
              return  NotFound("Comment not found");
            }

            return Ok(result.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _commentRepo.DeleteAsync(id);

            if(result == null)
            {
                return NotFound("Comment not found!");
            }
            return NoContent();
            
        }
    }
    

}