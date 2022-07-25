using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ChealCore.Validation;

namespace ChealCore.Models.ViewModels
{
    public class CreateRolesViewModel
    {
        [Required]
        [Display(Name = "Role Name")]
        //[Unique(typeof(RoleInput))]
        public string InputName { get; set; } = "";

        public List<IdentityRole> Roles { get; set; } = new List<IdentityRole>();

        //public string ErrorMessage { get; set; } = "";
    }

    public class RoleInput
    {
        public string RoleName { get; set; } = "";
    }

}
