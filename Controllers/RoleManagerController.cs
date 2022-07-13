using System;
using System.Diagnostics;
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

            IdentityRole role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }


        // POST: /RoleManager/Edit/role.ID
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ManageUserRolesViewModel model)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ManageUserRolesViewModel model)
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

        //// GET: /RoleManager/DeleteRole/role.ID
        //[Authorize]
        //public async Task<IActionResult> DeleteRole(string id)
        //{
        //    if (id == null || _roleManager.Roles == null)
        //    {
        //        return NotFound();
        //    }

        //    IdentityRole role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == id);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }

        //    return View();
        //}

        //// POST: ApplicationRoles/Delete/5


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
