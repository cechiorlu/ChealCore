using System;
using System.ComponentModel.DataAnnotations;
using ChealCore.Enums;

namespace ChealCore.Models
{
    public class TellerPosting
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Enter Amount")]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^[0-9.]+$", ErrorMessage = "Invalid amount"), Range(1, (double)Decimal.MaxValue, ErrorMessage = ("Amount must be between 1 and a reasonable maximum value"))]
        public decimal Amount { get; set; }

        [DataType(DataType.MultilineText)]
        public string Narration { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Posting type required")]
        public TellerPostingType PostingType { get; set; }

        [Display(Name = "Account")]
        public int CustomerAccountID { get; set; }
        public virtual CustomerAccount CustomerAccount { get; set; }


        //[Required(ErrorMessage = "Select a User")]
        //[Display(Name = "User")]
        //public string UserId { get; set; }
        //public ApplicationUser User { get; set; }


        [Required(ErrorMessage = "Select a User")]
        [Display(Name = "Post Initiator")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Display(Name = "Till Account")]
        public int? GLAccountID { get; set; }
        public virtual GLAccount TillAccount { get; set; }

        [Display(Name = "Post Status")]
        public PostStatus Status { get; set; }
    }
}

