using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sveikata.DTOs;
using sveikata.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace sveikata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: comments/<CommentsController>
        [HttpGet]
        public Task<IEnumerable<CommentDTO>> GetAll()
        {
            return _commentService.GetAll();
        }

        // GET comments/<CommentsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CommentDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentDTO>> GetById( int id)
        {
            var result = await _commentService.GetById(id);

            if (!result.Success)
            {
                return NotFound(result.Messages);
            }

            return result.comment;
        }

        [HttpGet("users/{userId}")]
        public async Task<IEnumerable<CommentDTO>> GetByUser(int userId)
        {
            return await _commentService.GetByUser(userId);
        }

        // POST comments/<CommentsController>
        [HttpPost]
        [Authorize(Roles = "Common,Worker,Admin")]
        public async Task<IActionResult> Create([FromBody] CommentDTO item)
        {
            var result = await _commentService.Create(item);
            if (!result.Success)
            {
                return BadRequest(result.Messages);
            }
            return CreatedAtAction("GetAll", result.comment);
        }

        // PUT comments/<CommentsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Common,Worker,Admin")]
        public async Task<ActionResult> Update(int id, [FromBody] CommentDTO item)
        {
            try
            {
                var result = await _commentService.Update(id, item, User.Identity.Name, User.IsInRole("Admin"));
                if (!result.Success)
                {
                    return BadRequest(result.Messages);
                }
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE comments/<CommentsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Common,Worker,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _commentService.Delete(id, User.Identity.Name, User.IsInRole("Admin"));
                if (!result.Success)
                {
                    return BadRequest(result.Messages);
                }
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
