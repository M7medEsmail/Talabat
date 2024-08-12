using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;

namespace Talabat.Domain.IRepositories
{
    public interface IUniteOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository <TEntity> () where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
