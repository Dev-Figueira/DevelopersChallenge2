using Nibo.Business.Interfaces;
using Nibo.Business.Models;
using Nibo.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nibo.Business.Service
{
    public class TransactionService : BaseService, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository,
                                  INotification notification) : base(notification)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task Add(Transaction transaction)
        {
            if (!ExecutionValidations(new TransactionValidation(), transaction)) return;

            if (_transactionRepository.Find(f => f.Id == transaction.Id).Result.Any())
            {
                Notify("A transaction with that ID already exists.");
                return;
            }

            await _transactionRepository.Add(transaction);
        }

        public async Task AddList(List<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                if (!ExecutionValidations(new TransactionValidation(), transaction)) return;

                if (_transactionRepository.Find(f => f.Id == transaction.Id).Result.Any())
                {
                    Notify("A transaction with that ID already exists.");
                    return;
                }

                await _transactionRepository.Add(transaction);
            }
        }

        public async Task Remove(Guid id)
        {
            if (_transactionRepository.GetForId(id).Result.Id.Equals(id))
            {
                Notify("There is no transaction!");
                return;
            }
           
            await _transactionRepository.Remove(id);
        }

        public async Task Update(Transaction transaction)
        {
            if (!ExecutionValidations(new TransactionValidation(), transaction)) return;

            if (_transactionRepository.Find(t => t.Id == transaction.Id).Result.Any())
            {
                Notify("A transaction with that ID already exists.");
                return;
            }

            await _transactionRepository.Updade(transaction);
        }

        public void Dispose()
        {
            _transactionRepository?.Dispose();
        }
    }
}
