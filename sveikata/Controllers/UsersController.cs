using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sveikata.DTOs.User;
using sveikata.DTOs;
using sveikata.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using sveikata.Models;

namespace sveikata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: users/<UsersController>
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public Task<IEnumerable<UserDTO>> GetAll()
        {
            return _userService.GetAll();
        }

        // GET users/<UsersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> GetById( int id)
        {
            var result = await _userService.GetById(id);

            if (!result.Success)
            {
                return NotFound(result.Messages);
            }

            return result.user;
        }

        [HttpGet("{id}/comments")]
        public async Task<IEnumerable<CommentDTO>> GetUserCommentsList(int id)
        {
            return await _userService.GetUserCommentsList(id);
        }

        [HttpGet("{id}/comments/{commentId}")]
        public async Task<IEnumerable<CommentDTO>> GetUserComment(int id, int commentId)
        {
            return await _userService.GetByComment(id, commentId);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(AuthenticatedUserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Authenticate([FromBody] PostUserDTO userCredentials)
        {
            var repsonse = await _userService.AuthenticateUserAsync(userCredentials);
            if (repsonse.Success == false)
            {
                return BadRequest(repsonse.Message);
            }
            return Ok(repsonse.Data);
        }

        // POST users/<UsersController>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDTO item)
        {
            var result = await _userService.Create(item, ERole.Common);
            if (!result.Success)
            {
                return BadRequest(result.Messages);
            }
            return CreatedAtAction("GetAll", result.user);
        }

        // PUT users/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UserDTO item)
        {
            try
            {
                var result = await _userService.Update(id, item);
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

        // DELETE users/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _userService.Delete(id);
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
