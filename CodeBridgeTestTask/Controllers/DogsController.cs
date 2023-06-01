using CodeBridgeTestTask.Data;
using CodeBridgeTestTask.IServices;
using CodeBridgeTestTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeBridgeTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IDogService _dogService;

        public DogsController(IDogService dogService)
        {
            _dogService = dogService;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Dogs house service. Version 1.0.1");
        }

        [HttpGet("dogs")]
        public async Task<IActionResult> GetDogs(string attribute = null, string order = null,
            int pageNumber = 1, int limit = 10)
        {
            var dogs = await _dogService.GetDogs(attribute, order, pageNumber, limit);
            return Ok(dogs);
        }

        [HttpPost("dog")]
        public async Task<IActionResult> CreateDog(Dog dog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _dogService.CreateDog(dog);
                return CreatedAtAction(nameof(GetDogs), new { id = dog.Id }, dog);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
