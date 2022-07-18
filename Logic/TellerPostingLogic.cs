using ChealCore.Data;
using ChealCore.Enums;
using ChealCore.Models;

namespace ChealCore.Logic
{
    public class TellerPostingLogic
    {
        BusinessLogic busLogic = new BusinessLogic();

        private readonly ApplicationDbContext _context;


        public TellerPostingLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        public string PostTeller(CustomerAccount account, GLAccount till, float amt, TellerPostingType pType)
        {
            string output = "";
            MainAccountCategory mainAccountCategory = new();
            switch (pType)
            {
                case TellerPostingType.Deposit:
                    busLogic.CreditCustomerAccount(account, (decimal)amt);
                    busLogic.DebitGl(till, amt, mainAccountCategory);

                    output = "success";
                    break;
                //break;
                case TellerPostingType.Withdrawal:
                    //Transfer the money from the user's till and reflect the changes in the customer account balance
                    //check withdrawal limit

                    var config = _context.AccountConfiguration.First();

                    //till = user.TillAccount;
                    if (account.Accounttype == AccountType.Savings)
                    {
                        if (account.AccountBalance >= config.SavingsMinimumBalance + (decimal)amt)
                        {
                            if (till.AccountBalance >= amt)
                            {
                                busLogic.CreditGl(till, amt, mainAccountCategory);
                                busLogic.DebitCustomerAccount(account, (decimal)amt);

                                output = "success";
                                
                                //account.SavingsWithdrawalCount++;
                            }
                            else
                            {
                                output = "Insufficient fund in the Till account";
                            }
                        }
                        else
                        {
                            output = "insufficient Balance in Customer's account, cannot withdraw!";
                        }

                    }//end if savings


                    else if (account.Accounttype == AccountType.Current)
                    {
                        if (account.AccountBalance >= config.CurrentMinimumBalance + (decimal)amt)
                        {
                            //REVISIT!!!
                            if (till.AccountBalance >= amt)
                            {
                                busLogic.CreditGl(till, amt, mainAccountCategory);
                                busLogic.DebitCustomerAccount(account, (decimal)amt);

                                output = "success";
                                //decimal x = (amt + account.CurrentLien) / 1000;
                                decimal x = ((decimal)amt) / 1000;
                                decimal charge = (int)x * config.CurrentCot;
                               // account.dailyCOTAccrued += charge;
                                //account.CurrentLien = (x - (int)x) * 1000;
                            }
                            else
                            {
                                output = "Insufficient fund in the Till account";
                            }
                        }
                        else
                        {
                            output = "insufficient Balance in Customer's account, cannot withdraw!";
                        }

                    }
                    else //for loan
                    {
                        output = "Please select a valid account";
                    }
                    break;
                //break;
                default:
                    break;
            }//end switch
            return output;
        }//end method PostTeller
    
    }
}
