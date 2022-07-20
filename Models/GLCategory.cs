using System;
using ChealCore.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChealCore.Models
{
    public class GLCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = ("Category Name is required")), MaxLength(40)]
        [Display(Name = "Category Name")]
        [DataType(DataType.Text)]
        public string CategoryName { get; set; } = "";

        [Required(ErrorMessage = ("Please enter a description")), MaxLength(150)]
        [DataType(DataType.Text)]
        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; } = "";


        [Display(Name = "Code")]
        public long? CodeNumber { get; set; }


        [Display(Name = "Main Account Category")]
        [Required(ErrorMessage = "Please select a main GL Category")]
        public MainAccountCategory mainAccountCategory { get; set; }

        public bool IsEnabled { get; set; }
    }
}

