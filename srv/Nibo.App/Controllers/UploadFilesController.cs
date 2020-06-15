using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nibo.App.Controllers;
using Nibo.App.ViewModels;
using Nibo.Business.Interfaces;
using Nibo.Business.Models;
using ServiceStack.Host;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Projeto_Nibo.Controllers
{
    public class UploadFilesController : BaseController
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public UploadFilesController(IMapper mapper,
                                     ITransactionService transactionService,
                                     ITransactionRepository transactionRepository,
                                     INotification notification) : base(notification)
        {
            _transactionService = transactionService;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                string extensao = Path.GetExtension(file.FileName);
                string[] extensoesValidas = new string[] { "ofx" };

                if (!extensoesValidas.Contains(extensao))
                {
                    new HttpException(string.Format("Extensão de arquivo *.{0} não suportada", extensao));
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\docsOFX\\" + file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            List<Transaction> transactionsList = _mapper.Map<List<Transaction>>(CreateListTransactionViewModel(files));
            await _transactionRepository.RemoveAll();
            await _transactionService.AddList(transactionsList);

            return RedirectToAction("Index", "Transactions");
        }

        private List<TransactionViewModel> CreateListTransactionViewModel(List<IFormFile> files)
        {
            var transactions = new List<TransactionViewModel>();
            foreach (var file in files)
            {
                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    string line = "";
                    string lineValues = "";
                    int count = 0;
                    while (!stream.EndOfStream || line.Contains("</STMTTRN>"))
                    {
                        line = stream.ReadLine();
                        if (line.Contains("<TRNTYPE>") || line.Contains("<DTPOSTED>") || line.Contains("<TRNAMT>") || line.Contains("<MEMO>"))
                        {
                            lineValues += line.Split('>')[1] + ",";
                            count++;
                        }
                        if (count == 4)
                        {
                            var arrayValues = lineValues.Split(',');
                            TransactionType type = 0;
                            if (arrayValues[0].ToLower().Equals("debit")) type = TransactionType.Debit;

                            if (arrayValues[0].ToLower().Equals("credit")) type = TransactionType.Credit;

                            transactions.Add(new TransactionViewModel()
                            {
                                TRNTYPE = type,
                                DTPOSTED = DateTime.ParseExact(arrayValues[1].Substring(0, 8), "yyyyMMdd", null),
                                MEMO = arrayValues[2].ToString(CultureInfo.CurrentCulture),
                                TRNAMT = arrayValues[3]
                            });

                            count = 0;
                            lineValues = "";
                        }
                    }
                }
            }

            return transactions;
        }
    }
}

