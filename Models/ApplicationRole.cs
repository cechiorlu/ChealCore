using System;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ChealCore.Data;

namespace ChealCore.Models
{
    public class ApplicationRole : IdentityRole
    {
        public bool IsEnabled { get; set; }
    }

}
