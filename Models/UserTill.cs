using System;
using System.ComponentModel.DataAnnotations;

namespace ChealCore.Models
{
    public class UserTill
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Select a User")]
        [Display(Name = "User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Select a Till Account")]
        [Display(Name = "Till")]
        public int GlAccountID { get; set; }
        public virtual GLAccount GLAccount { get; set; }
    }
}

