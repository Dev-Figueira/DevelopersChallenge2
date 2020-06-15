using Nibo.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nibo.Business.Interfaces
{
    public interface ITransactionService : IDisposable
    {
        Task Add(Transaction transaction);
        Task AddList(List<Transaction> transactions);
        Task Update(Transaction transaction);
        Task Remove(Guid id);
    }
}
