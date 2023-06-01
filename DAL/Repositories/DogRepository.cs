using CodeBridgeTestTask.Data;
using CodeBridgeTestTask.Models;
using DAL.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly DogDbContext _dbContext;

        public DogRepository(DogDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task CreateDogAsync(Dog dog)
        {
            await _dbContext.Dogs.AddAsync(dog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DogExistsAsync(string name)
        {
            return await _dbContext.Dogs.AnyAsync(d => d.Name == name);

        }

        public async Task<Dog> GetDogByIdAsync(int id)
        {
            return await _dbContext.Dogs.FindAsync(id);
        }

        public async Task<List<Dog>> GetDogsAsync(string attribute = null, string order = "desc", int pageNumber = 1, int limit = 10)
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
                        throw new ArgumentException("Invalid sorting attribute.");
                }
            }

            query = query.Skip((pageNumber - 1) * limit).Take(limit);

            return await query.ToListAsync();
        }
    }
}
