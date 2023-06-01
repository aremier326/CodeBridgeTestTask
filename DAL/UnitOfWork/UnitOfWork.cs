using CodeBridgeTestTask.Data;
using DAL.Interfaces.IRepositories;
using DAL.Interfaces.IUnitOfWork;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DogDbContext _dbContext;
        public IDogRepository DogRepository { get; }

        public UnitOfWork(DogDbContext dbContext)
        {
            _dbContext = dbContext;
            DogRepository = new DogRepository(_dbContext);
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
