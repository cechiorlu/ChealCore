using System;
using ChealCore.Enums;

namespace ChealCore.Models
{
    public class Transaction
    {
        public int ID { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string AccountName { get; set; }

        public string SubCategory { get; set; }
        //eg customerAccount, CashAsset etc

        public MainAccountCategory mainAccountCategory { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}

