using ChealCore.Data;
using ChealCore.Enums;
using ChealCore.Models;

namespace ChealCore.Logic
{
    public class CustomerAccountLogic
    {
        private readonly ApplicationDbContext _context;

        public CustomerAccountLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GenerateCustomerAccountNumber(AccountType accountType)
        {
            string codeNumber = "0";

            Random random = new();

            switch (accountType)
            {
                case AccountType.Current:
                    codeNumber = "11";
                    break;

                case AccountType.Savings:
                    codeNumber = "22";
                    break;

                case AccountType.Loan:
                    codeNumber = "33";
                    break;

                default:
                    break;
            }
            return codeNumber;
        }
        public bool CustomerAccountHasSufficientBalance(CustomerAccount account, decimal amountToDebit)
        {
            var config = _context.AccountConfiguration.First();

            if (account.AccountBalance >= amountToDebit + config.SavingsMinimumBalance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
