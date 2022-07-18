using ChealCore.Enums;
using ChealCore.Models;

namespace ChealCore.Logic
{
    public class BusinessLogic
    {
        public bool CreditGl(GLAccount account, float amount, MainAccountCategory mainAccountCategory)
        {
            try
            {
                switch (mainAccountCategory)
                {
                    case MainAccountCategory.Asset:
                        account.AccountBalance -= amount;
                        break;
                    case MainAccountCategory.Capital:
                        account.AccountBalance += amount;
                        break;
                    case MainAccountCategory.Expenses:
                        account.AccountBalance -= amount;
                        break;
                    case MainAccountCategory.Income:
                        account.AccountBalance += amount;
                        break;
                    case MainAccountCategory.Liability:
                        account.AccountBalance += amount;
                        break;
                    default:
                        break;
                }//end switch

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }//end creditGl

        public bool DebitGl(GLAccount account, float amount, MainAccountCategory mainAccountCategory)
        {
            try
            {
                switch (mainAccountCategory)
                {
                    case MainAccountCategory.Asset:
                        account.AccountBalance += amount;
                        break;
                    case MainAccountCategory.Capital:
                        account.AccountBalance -= amount;
                        break;
                    case MainAccountCategory.Expenses:
                        account.AccountBalance += amount;
                        break;
                    case MainAccountCategory.Income:
                        account.AccountBalance -= amount;
                        break;
                    case MainAccountCategory.Liability:
                        account.AccountBalance -= amount;
                        break;
                    default:
                        break;
                }//end switch
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }//end DebitGl

        public bool CreditCustomerAccount(CustomerAccount account, decimal amount)
        {
            try
            {
                if (account.Accounttype == AccountType.Loan)
                {
                    account.AccountBalance -= amount;       //Loan accounts are assets to the bank
                }
                else
                {
                    account.AccountBalance += amount;       //Savings and current accounts are liabilities to the bank
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DebitCustomerAccount(CustomerAccount account, decimal amount)
        {
            try
            {
                if (account.Accounttype == AccountType.Loan)
                {
                    account.AccountBalance += amount;   //because its an asset to the bank.
                }
                else
                {
                    account.AccountBalance -= amount;   //its a liability, hence a debit reduces it.
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
