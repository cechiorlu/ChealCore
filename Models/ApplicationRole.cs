using System;
using Microsoft.AspNetCore.Identity;

namespace ChealCore.Models
{
    public class ApplicationRole : IdentityRole
    {
        public bool IsEnabled { get; set; }
    }
}

