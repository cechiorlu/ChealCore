using ChealCore.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChealCore.Models
{
    public class LoanAccount
    {
        //public LoanAccount()
        //{
        //    RepaymentFrequencyMonths = 1;
        //    StartDate = DateTime.Now;
        //}
        public int Id { get; set; }

        [DisplayName("Linked Account")]
        public int CustomerAccountId { get; set; }
        public virtual CustomerAccount CustomerAccount { get; set; }



        [Required(ErrorMessage = "Account name is required")]
        [RegularExpression(@"^[ a-zA-Z]+$", ErrorMessage = "Account name should only contain characters and white spaces"), MaxLength(40)]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1000.00, (double)decimal.MaxValue, ErrorMessage = "Loan amount must be between #1,000 and a maximum reasonable amount")]
        [Display(Name = "Loan Amount")]
        public decimal LoanAmount { get; set; }

        [Required]
        [Display(Name = "Terms of loan")]
        public LoanTerms LoanTerms { get; set; }

        [Required(ErrorMessage = "Number of years is required")]
        [Range(0.084, 1000.0)]
        [RegularExpression(@"^[.0-9]+$", ErrorMessage = "Invalid format")]
        [Display(Name = "Number of years")]
        public double NumberOfYears { get; set; }

        [Required(ErrorMessage = "Interest rate is required")]
        [Display(Name = "Interest Rate")]
        [Range(0, 100)]
        [RegularExpression(@"^[.0-9]+$", ErrorMessage = "Invalid format")]
        public double? InterestRate { get; set; }


        [Required]
        [Display(Name = "Servicing Account Number")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Account number should only contain numbers"), MinLength(10), MaxLength(11)]
        public string? ServicingAccountNumber { get; set; }


        [Required(ErrorMessage = "Please select a branch")]
        [Display(Name = "Branch")]
        public int BranchID { get; set; }
        public virtual Branch branch { get; set; }


    }
}
