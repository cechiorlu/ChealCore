using System;
using System.ComponentModel.DataAnnotations;

namespace ChealCore.Models.ViewModels
{
    public class RolesViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<string> Claims { get; set; } = new List<string>();
        public List<string> Users { get; set; } = new List<string>();
    }
}

