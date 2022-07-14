using System;
using System.ComponentModel.DataAnnotations;

namespace ChealCore.Models.ViewModels
{
    public class ManageUserViewModel
    {
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> Claims { get; set; } = new List<string>();

        public IList<string> Roles { get; set; } = new List<string>();

    }
}