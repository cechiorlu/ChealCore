using System;
using System.ComponentModel.DataAnnotations;

namespace ChealCore.Models.ViewModels
{
    public class ManageUserRolesViewModel
    {
        public List<string> Functions { get; set; } = new List<string>();
        public List<string> Users { get; set; } = new List<string>();
        public string Id { get; set; }
        [Required(ErrorMessage = "Role name is required")]
        public string Name { get; set; }
    }
}

