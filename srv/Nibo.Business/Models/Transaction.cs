using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.Business.Models
{
    public class Transaction : Entity
    {
        public TransactionType TRNTYPE { get; set; }

        public string TRNAMT { get; set; }

        public string MEMO { get; set; }

        public DateTime DTPOSTED { get; set; }
    }
}
