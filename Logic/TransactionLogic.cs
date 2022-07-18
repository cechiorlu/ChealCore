using ChealCore.Data;
using ChealCore.Enums;
using ChealCore.Models;

namespace ChealCore.Logic
{
    public class TransactionLogic
    {
        private readonly ApplicationDbContext _context;

        public TransactionLogic(ApplicationDbContext context)
        {
            _context = context; 
        }

        public void CreateTransaction(GLAccount account, decimal amount, TransactionType trnType)
        {
            //Record this transaction for Trial Balance generation
            Transaction transaction = new Transaction();
            transaction.Amount = amount;
            transaction.Date = DateTime.Now;
            transaction.AccountName = account.AccountName;
            transaction.SubCategory = account.GLCategory.CategoryName;
            transaction.mainAccountCategory = account.GLCategory.mainAccountCategory;
            transaction.TransactionType = trnType;

            _context.Transaction.Add(transaction);
            _context.SaveChanges();
        }

        public void CreateTransaction(CustomerAccount account, decimal amount, TransactionType trnType)
        {
            if (account.Accounttype == AccountType.Loan)
            {
                //Record this transaction for Trial Balance generation
                Transaction transaction = new Transaction();
                transaction.Amount = amount;
                transaction.Date = DateTime.Now;
                transaction.AccountName = account.AccountName;
                transaction.SubCategory = "Customer's Loan Account";
                transaction.mainAccountCategory = MainAccountCategory.Asset;
                transaction.TransactionType = trnType;

                _context.Transaction.Add(transaction);
                _context.SaveChanges();
            }
            else
            {
                //Record this transaction for Trial Balance generation
                Transaction transaction = new Transaction();
                transaction.Amount = amount;
                transaction.Date = DateTime.Now;
                transaction.AccountName = account.AccountName;
                transaction.SubCategory = "Customer Account";
                transaction.mainAccountCategory = MainAccountCategory.Liability;
                transaction.TransactionType = trnType;
                _context.Transaction.Add(transaction);
                _context.SaveChanges();
            }
        }
    }
}
