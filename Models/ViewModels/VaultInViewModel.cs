using System;
using System.ComponentModel.DataAnnotations;

namespace ChealCore.Models.ViewModels
{
    public class VaultInViewModel
    {
        [Required]
        public decimal Amount { get; set; }

        public int CodeNumber { get; set; }
    }
}

