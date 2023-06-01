using CodeBridgeTestTask.Data;
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
        private readonly DogDbContext _dbContext;

        public DogsController(DogDbContext dbContext)
        {
            _dbContext = dbContext;
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
            var query = _dbContext.Dogs.AsQueryable();

            if (!string.IsNullOrEmpty(attribute) && !string.IsNullOrEmpty(order))
            {
                switch (attribute.ToLower())
                {
                    case "name":
                        query = (order.ToLower() == "desc") ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                        break;
                    case "color":
                        query = (order.ToLower() == "desc") ? query.OrderByDescending(x => x.Color) : query.OrderBy(x => x.Color);
                        break;
                    case "tail_length":
                        query = (order.ToLower() == "desc") ? query.OrderByDescending(x => x.TailLength) : query.OrderBy(x => x.TailLength);
                        break;
                    case "weight":
                        query = (order.ToLower() == "desc") ? query.OrderByDescending(x => x.Weight) : query.OrderBy(x => x.Weight);
                        break;
                    default:
                        return BadRequest("Invalid sorting attribute!");
                }
            }

            // Pagination
            query = query.Skip((pageNumber - 1) * limit).Take(limit);

            var dogs = await query.ToListAsync();
            return Ok(dogs);
        }

        [HttpPost("dog")]
        public async Task<IActionResult> CreateDog(Dog dog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(_dbContext.Dogs.Any(d =>d.Name == dog.Name))
            {
                return Conflict("Dog with the same name exists already!");
            }

            _dbContext.Dogs.Add(dog);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDogs), new {id = dog.Id}, dog);
        }
    }
}
