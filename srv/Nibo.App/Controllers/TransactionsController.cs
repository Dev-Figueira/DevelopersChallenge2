using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nibo.App.ViewModels;
using Nibo.Business.Interfaces;

namespace Nibo.App.Controllers
{
    public class TransactionsController : BaseController
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionsController(ITransactionRepository transactionRepository,
                                      IMapper mapper,
                                     INotification notification) : base(notification)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(_mapper.Map<IEnumerable<TransactionViewModel>>(_transactionRepository.GetAllDistinct()));
        }
    }
}