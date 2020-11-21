using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sveikata.DTOs;
using sveikata.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sveikata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseasesController : ControllerBase
    {
        private readonly IDiseaseService _diseaseService;

        public DiseasesController(IDiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
        }

        // GET: diseases/<DiseasesController>
        [HttpGet]
        public Task<IEnumerable<DiseaseDTO>> GetAll()
        {
            return _diseaseService.GetAll();
        }

        // GET diseases/<DiseasesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DiseaseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DiseaseDTO>> GetById( int id)
        {
            var result = await _diseaseService.GetById(id);

            if (!result.Success)
            {
                return NotFound(result.Messages);
            }

            return result.disease;
        }

            // POST diseases/<DiseasesController>
            [HttpPost]
        public async Task<IActionResult> Create([FromBody] DiseaseDTO item)
        {
            var result = await _diseaseService.Create(item);
            if (!result.Success)
            {
                return BadRequest(result.Messages);
            }
            return CreatedAtAction("GetAll", result.disease);
        }

        // PUT diseases/<DiseasesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] DiseaseDTO disease)
        {
            try
            {
                var result = await _diseaseService.Update(id, disease);
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

        // DELETE diseases/<DiseasesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _diseaseService.Delete(id);
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
