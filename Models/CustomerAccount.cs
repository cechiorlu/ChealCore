using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ChealCore.Enums;

namespace ChealCore.Models
{
    public class CustomerAccount
    {
        public int Id { get; set; }

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Account name is required")]
        [RegularExpression(@"^[ a-zA-Z]+$", ErrorMessage = "Account name should contain characters and white spaces only"), MaxLength(40)]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [DisplayName("Account Number")]
        public long? AccountNumber { get; set; }

        [Display(Name = "Account Balance")]
        [DataType(DataType.Currency)]
        public decimal AccountBalance { get; set; }

        [DisplayName("Account Type")]
        public AccountType Accounttype { get; set; }

        [DisplayName("Date Opened")]
        public DateTime DateOpened { get; set; }

        //[DisplayName("Loan Account")]
        //public LoanAccount LoanAccount { get; set; }

        public bool IsActivated { get; set; }
    }
}

