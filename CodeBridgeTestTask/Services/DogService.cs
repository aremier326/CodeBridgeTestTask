using CodeBridgeTestTask.IServices;
using CodeBridgeTestTask.Models;
using DAL.Interfaces.IUnitOfWork;

namespace CodeBridgeTestTask.Services
{
    public class DogService : IDogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Dog>> GetDogs(string attribute = null, string order = null, int pageNumber = 1, int limit = 10)
        {
            return await _unitOfWork.DogRepository.GetDogsAsync(attribute, order, pageNumber, limit);
        }

        public async Task<Dog> GetDogById(int id)
        {
            return await _unitOfWork.DogRepository.GetDogByIdAsync(id);
        }

        public async Task CreateDog(Dog dog)
        {
            if(await _unitOfWork.DogRepository.DogExistsAsync(dog.Name))
            {
                throw new Exception("Dog with the same name already exists.");
            }

            await _unitOfWork.DogRepository.CreateDogAsync(dog);
            await _unitOfWork.CommitAsync();
        }
    }
}
