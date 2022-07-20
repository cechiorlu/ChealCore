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
        [Unique(typeof(RoleInput))]
        public RoleInput InputName { get; set; } = new RoleInput();

        public List<IdentityRole> Roles { get; set; } = new List<IdentityRole>();
    }

    public class RoleInput
    {
        public string RoleName { get; set; } = "";
    }

}
