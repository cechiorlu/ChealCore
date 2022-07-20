using System.ComponentModel.DataAnnotations;
using ChealCore.Models;
using ChealCore.Models.ViewModels;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ChealCore.Data;

namespace ChealCore.Validation
{
    public class Unique : ValidationAttribute
    {
        public Type ObjectType { get; private set; }
        public Unique(Type type)
        {
            ObjectType = type;
        }



        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // role input
            if (ObjectType == typeof(RoleInput))
            {
                //DbContext db = new ApplicationDbContext();

                var roles = new List<string>();

                var role = roles.FirstOrDefault(u => u.Contains(((RoleInput)value).RoleName));

                if (String.IsNullOrEmpty(role))
                    return ValidationResult.Success;
                else
                    return new ValidationResult("Role already exists");
            }

            //// gl category input
            //if (ObjectType == typeof(RoleInput))
            //{
            //    DbContext db = new ApplicationDbContext();

            //    var glCategories = new List<string>();
            //    //.Add("ra@ra.com");
            //    //emails.Add("ve@ve.com");

            //    var role = roles.FirstOrDefault(u => u.Contains(((RoleInput)value).RoleName));

            //    if (String.IsNullOrEmpty(role))
            //        return ValidationResult.Success;
            //    else
            //        return new ValidationResult("GL Category already exists");
            //}

            return new ValidationResult("Generic Validation Fail");
        }
    }
}




