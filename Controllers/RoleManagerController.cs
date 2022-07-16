using System;
using System.Diagnostics;
using System.Security.Claims;
using ChealCore.Models;
using ChealCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChealCore.Controllers
{
    [Authorize]
    public class RoleManagerController : Controller
    {
        private readonly ILogger<RoleManagerController> _logger;

        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagerController(RoleManager<IdentityRole> roleManager, ILogger<RoleManagerController> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        // GET: /RoleManager
        // list all roles

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        // POST: /RoleManager
        // create new role
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return RedirectToAction("Index");
        }

        // GET: /RoleManager/Manage/role.ID
        public async Task<IActionResult> Manage(string id)
        {
            if (id == null || _roleManager.Roles == null)
            {
                return NotFound();
            }

            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if (role == null)
            {
                return NotFound();
            }

            else
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                var model = new RolesViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                    Claims = roleClaims.Select(c => c.Value).ToList(),
                };

                return View(model);
            }
        }


        // POST: /RoleManager/Edit/role.ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RolesViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.Name;

                // Update the Role using UpdateAsync
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }


        // POST: /RoleManager/Delete/role.ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RolesViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                // Update the Role using UpdateAsync
                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }



        //////
        ///

        //[HttpGet]
        //public async Task<IActionResult> ManageRoleClaims(string roleId)
        //{
        //    //ViewBag.RoleId = roleId;    

        //    var role = await _roleManager.FindByIdAsync(roleId);


        //    if (role == null)
        //    {
        //        ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
        //        return View("NotFound");
        //    }

        //    var existingRoleClaims = await _roleManager.GetClaimsAsync(role);

        //    var model = new RoleClaimViewModel
        //    {
        //        RoleId = roleId
        //    };

        //    //var data = claimsstore.allclaims;
        //    //var data_ = _context.Claims.ToListAsync();

        //    foreach (Claim claim in ClaimsStore.GetClaims(_context))
        //    {
        //        RoleClaims roleClaims = new RoleClaims
        //        {
        //            ClaimType = claim.Type
        //        };

        //        if (existingRoleClaims.Any(c => c.Type == claim.Type))
        //        {
        //            roleClaims.IsSelected = true;
        //        }
        //        model.Claims.Add(roleClaims);
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> ManageRoleClaims(RoleClaimViewModel model, string roleId)
        //{
        //    var role = await roleManager.FindByIdAsync(roleId);

        //    if (role == null)
        //    {
        //        ViewBag.ErrorMessage = $"User with Id = {roleId} cannot be found";
        //        return View("NotFound");
        //    }

        //    // Get all the user existing claims and delete them
        //    var claims = await roleManager.GetClaimsAsync(role);

        //    foreach (var claim in claims)
        //    {
        //        var result = await roleManager.RemoveClaimAsync(role, claim);
        //        if (!result.Succeeded)
        //        {
        //            ModelState.AddModelError("", "Cannot remove user existing claims");
        //            return View(model);
        //        }


        //    }
        //    var data_ = model.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType));

        //    foreach (var data in data_)
        //    {
        //        var result_ = await roleManager.AddClaimAsync(role, data);

        //        if (!result_.Succeeded)
        //        {
        //            ModelState.AddModelError("", "Cannot add selected claims to user");
        //            return View(model);
        //        }
        //    }
        //    return RedirectToAction("EditRole", new { Id = model.RoleId });


        //}
        //////





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
