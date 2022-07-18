using System;
using System.ComponentModel.DataAnnotations;

namespace ChealCore.Models
{
    public class Branch
    {
        public enum BranchStatus
        {
            Closed, Open
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(pattern: @"^[A-Z][a-zA-Z]*$", ErrorMessage = "Branch name cannot contain special characters")]
        public string BranchName { get; set; }


        [Required]
        public string Address { get; set; }

        public long SortCode { get; set; }
        public BranchStatus Status { get; set; }
    }
}

