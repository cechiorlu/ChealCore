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

        //public async Task<CreateRolesViewModel> CreateRoleModel()
        //{

        //    return await r.GetRolesViewModel();
        //}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // role input
            if (ObjectType == typeof(RoleInput))
            {

                //var model = CreateRoleModel();
                //var roles = model.Result.Roles ?? new List<IdentityRole>();

                //var role = roles.FirstOrDefault(u => u.NormalizedName == value.ToString().ToUpper());

                //if (role == null)
                //    return ValidationResult.Success;
                //else
                //    return new ValidationResult("Role already exists");
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




