using System;
using System.ComponentModel.DataAnnotations;
using ChealCore.Enums;

namespace ChealCore.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required]
        [RegularExpression(@"^[ a-zA-Z]+$", ErrorMessage = "Full name should only contain characters and white spaces")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(225, MinimumLength = 4)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(11, ErrorMessage = "Telephone Number cannot be less than 11 letters", MinimumLength = 11)]
        [Display(Name = "Telephone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        public bool IsActivated { get; set; }
    }
}

