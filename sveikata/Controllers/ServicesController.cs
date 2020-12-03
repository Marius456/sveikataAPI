using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sveikata.DTOs;
using sveikata.DTOs.Errors;
using sveikata.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sveikata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        // GET: services/<ServicesController>
        [HttpGet]
        public Task<IEnumerable<ServiceDTO>> GetAll()
        {
            return _serviceService.GetAll();
        }

        // GET services/<ServicesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceDTO>> GetById( int id)
        {
            var result = await _serviceService.GetById(id);

            if (!result.Success)
            {
                return NotFound(result.Messages);
            }

            return result.service;
        }

        // POST services/<ServicesController>
        [HttpPost]
        [Authorize(Roles = "Worker,Admin")]
        public async Task<IActionResult> Create([FromBody] ServiceDTO item)
        {
            var result = await _serviceService.Create(item);
            if (!result.Success)
            {
                return BadRequest(result.Messages);
            }
            return CreatedAtAction("GetAll", result.service);
        }

        // PUT services/<ServicesController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Worker,Admin")]
        public async Task<ActionResult> Update(int id, [FromBody] ServiceDTO service)
        {
            try
            {
                var result = await _serviceService.Update(id, service);
                if (!result.Success)
                {
                    return BadRequest(result.Messages);
                }
            }
            catch (KeyNotFoundException)
            {
                Error e = new Error();
                e.Message = "Service not found.";
                return NotFound(e);
            }
            return NoContent();
        }

        // DELETE services/<ServicesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Worker,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _serviceService.Delete(id);
                if (!result.Success)
                {
                    return BadRequest(result.Messages);
                }
            }
            catch (KeyNotFoundException)
            {
                Error e = new Error();
                e.Message = "Service not found.";
                return NotFound(e);
            }
            return NoContent();
        }
    }
}
