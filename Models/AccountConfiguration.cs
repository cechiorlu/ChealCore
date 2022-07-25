using System;
using System.ComponentModel.DataAnnotations;

namespace ChealCore.Models
{
    public class AccountConfiguration
    {
        public int ID { get; set; }

        [Display(Name = "Business Status (Open)")]
        public bool IsBusinessOpen { get; set; }

        [Display(Name = "Financial Date")]
        public DateTime FinancialDate { get; set; }

        [Display(Name = "Credit Interest Rate")]
        [Range(0, 100)]
        [RegularExpression(@"^[.0-9]+$", ErrorMessage = "Invalid input")]
        public double SavingsCreditInterestRate { get; set; }

        [Display(Name = "Minimum Balance")]
        [Range(0, (double)decimal.MaxValue)]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^[.0-9]+$", ErrorMessage = "Invalid input for minimum balance")]
        public decimal SavingsMinimumBalance { get; set; }

        [Display(Name = "Select Interest Expense GL")]
        public int? SavingsInterestExpenseGlID { get; set; }
        public virtual GLAccount SavingsInterestExpenseGl { get; set; }

        [Display(Name = "Select Interest Payable GL")]
        public int? SavingsInterestPayableGlID { get; set; }
        public virtual GLAccount SavingsInterestPayableGl { get; set; }


        //current account 
        [Display(Name = "Credit Interest Rate")]
        [Range(0, 100)]
        [RegularExpression(@"^[.0-9]+$", ErrorMessage = "Invalid format")]
        public double CurrentCreditInterestRate { get; set; }

        [Display(Name = "Minimum Balance")]
        [Range(0, (double)decimal.MaxValue)]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^[.0-9]+$", ErrorMessage = "Invalid format")]
        public decimal CurrentMinimumBalance { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Commission on Turnover")]
        [Range(0.00, 1000.00)]
        [RegularExpression(@"^[.0-9]+$", ErrorMessage = "Invalid format")]
        public decimal CurrentCot { get; set; }        //Commission On Turnover

        [Display(Name = "Select Interest Expense GL")]
        public int? CurrentInterestExpenseGlID { get; set; }
        public virtual GLAccount CurrentInterestExpenseGl { get; set; }

        [Display(Name = "Select COT Income GL")]
        public int? CurrentCotIncomeGlID { get; set; }
        public virtual GLAccount CurrentCotIncomeGl { get; set; }

        [Display(Name = "Select Interest Payable GL")]
        public int? CurrentInterestPayableGlID { get; set; }
        public virtual GLAccount CurrentInterestPayableGl { get; set; }


        //Loan account
        [Display(Name = "Debit Interest Rate")]
        [Range(0, 100)]
        [RegularExpression(@"^[.0-9]+$", ErrorMessage = "Invalid format")]
        public double LoanDebitInterestRate { get; set; }

        [Display(Name = "Select Interest Income GL")]
        public int? LoanInterestIncomeGlID { get; set; }
        public virtual GLAccount LoanInterestIncomeGl { get; set; }

        [Display(Name = "Select Interest Expense GL")]
        public int? LoanInterestExpenseGLID { get; set; }
        public virtual GLAccount LoanInterestExpenseGl { get; set; }        //Expense: from where the loan is disbursed

        [Display(Name = "Select Interest Receivable GL")]
        public int? LoanInterestReceivableGlID { get; set; }
        public virtual GLAccount LoanInterestReceivableGl { get; set; }     //Asset

    }
}

