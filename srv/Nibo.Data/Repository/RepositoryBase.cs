using Microsoft.EntityFrameworkCore;
using Nibo.Business.Interfaces;
using Nibo.Business.Models;
using Nibo.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.Data.Repository
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MyDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected RepositoryBase(MyDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public List<Transaction> GetAllDistinct()
        {
            var transactions = (from a in Db.Transactions
                                orderby 1 descending
                                select new Transaction
                                {
                                    DTPOSTED = a.DTPOSTED,
                                    MEMO = a.MEMO,
                                    TRNAMT = a.TRNAMT,
                                    TRNTYPE = a.TRNTYPE
                                });
            return transactions.Distinct().ToList();
        }

        public virtual async Task<TEntity> GetForId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task Remove(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id});
            await SaveChanges();
        }

        public virtual async Task RemoveAll() 
        {
            DbSet.RemoveRange(DbSet);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public virtual async Task Updade(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }       
    }
}
