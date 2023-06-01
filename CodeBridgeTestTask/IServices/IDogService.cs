using CodeBridgeTestTask.Models;

namespace CodeBridgeTestTask.IServices
{
    public interface IDogService
    {
        Task<List<Dog>> GetDogs(string attribute = null, string order = "desc",
            int pageNumber = 1, int limit = 10);
        
        Task<Dog> GetDogById(int id);

        Task CreateDog(Dog dog);
    }
}
