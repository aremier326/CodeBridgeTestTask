using DAL.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDogRepository DogRepository { get; }
        Task<int> CommitAsync();


    }
}
