using Nibo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Nibo.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);
        Task<TEntity> GetForId(Guid id);
        Task<List<TEntity>> GetAll();
        List<Transaction> GetAllDistinct();
        Task Updade(TEntity entity);
        Task Remove(Guid id);
        Task RemoveAll();
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
