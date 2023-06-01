using CodeBridgeTestTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.IRepositories
{
    public interface IDogRepository
    {
        Task<List<Dog>> GetDogsAsync(string attribute = null, string order = null,
            int pageNumber = 1, int limit = 10);
        Task<Dog> GetDogByIdAsync(int id);
        Task CreateDogAsync(Dog dog);
        Task<bool> DogExistsAsync(string name);
    }
}
