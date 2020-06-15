using Nibo.Business.Interfaces;
using Nibo.Business.Models;
using Nibo.Data.Context;

namespace Nibo.Data.Repository
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(MyDbContext context) : base(context) { }
    }
}
