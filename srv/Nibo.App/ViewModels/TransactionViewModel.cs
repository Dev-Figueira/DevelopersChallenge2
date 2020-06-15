using Nibo.Business.Models;
using System;

namespace Nibo.App.ViewModels
{
    public class TransactionViewModel
    {
        public Guid Id { get; set; }

        public TransactionType TRNTYPE { get; set; }

        public string TRNAMT { get; set; }

        public string MEMO { get; set; }

        public DateTime DTPOSTED { get; set; }
    }
}
