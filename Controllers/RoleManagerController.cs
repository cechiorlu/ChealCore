using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Claims;
using ChealCore.Enums;
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
            var rolesModel = new CreateRolesViewModel();
            var roles = await _roleManager.Roles.ToListAsync();
            rolesModel.Roles = roles;
            return View(rolesModel);
        }

        // POST: /RoleManager
        // create new role
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(CreateRolesViewModel createRolesViewModel)
        {
            if (String.IsNullOrEmpty(createRolesViewModel.InputName))
            {
                TempData["ErrorMessage"] = "Rolename cannot be blank";
                return RedirectToAction("Index");
            }

            var roles = await _roleManager.Roles.ToListAsync();
            string inputName = createRolesViewModel.InputName.Trim().ToUpper();
            List<string> rolenames = new();

            roles.ForEach(role => rolenames.Add(role.NormalizedName));

            bool roleExists = rolenames.Contains(inputName);

            if (roleExists)
            {
                TempData["ErrorMessage"] = "Rolename already in use";
            }
            else if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new IdentityRole(createRolesViewModel.InputName.Trim()));
                TempData["ErrorMessage"] = "";
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


        // GET: /RoleManager/ManageRoleClaims/role.Id
        [HttpGet]
        public async Task<IActionResult> ManageRoleClaims(string roleId)
        {
            var superAdmin = await _roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role cannot be found";
                return View("NotFound");
            }

            var model = new RoleClaimsViewModel();
            model.RoleId = roleId;

            var allClaims = await _roleManager.GetClaimsAsync(superAdmin);
            var roleClaims = await _roleManager.GetClaimsAsync(role);

            foreach (var claim in allClaims)
            {
                bool hasClaim = roleClaims.Contains(claim);

                var roleClaimsModel = new RoleClaims
                {
                    ClaimType = claim.Type
                };

                if (hasClaim)
                {
                    roleClaimsModel.IsSelected = true;
                }
                else
                {
                    roleClaimsModel.IsSelected = false;
                }

                model.Claims.Add(roleClaimsModel);
            }

            return View(model);
        }

        // POST: /UserManager/ManageRoleClaims/
        [HttpPost]
        public async Task<IActionResult> ManageRoleClaims([Bind("RoleId", "Claims")] RoleClaimsViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role cannot be found";
                return View("NotFound");
            }

            var claims = await _roleManager.GetClaimsAsync(role);

            // remove existing claims
            foreach (var claim in claims)
            {
                var result = await _roleManager.RemoveClaimAsync(role, claim);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot remove existing claims");
                    return View(model);
                }
            }

            // add claims
            foreach (var roleClaim in model.Claims)
            {
                var claim = new Claim(roleClaim.ClaimType.Trim(), roleClaim.ClaimType.Trim());
                if (roleClaim.IsSelected)
                {
                    var result = await _roleManager.AddClaimAsync(role, claim);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Cannot add selected claims to role");
                        return View(model);
                    }
                }
            }

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
