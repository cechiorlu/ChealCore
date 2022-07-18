using System;
using System.Diagnostics;
using System.Security.Claims;
using ChealCore.Enums;
using ChealCore.Models;
using ChealCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChealCore.Controllers
{
    [Authorize]
    public class ClaimsManagerController : Controller
    {
        private readonly ILogger<ClaimsManagerController> _logger;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<ApplicationUser> _userManager;

        public ClaimsManagerController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ILogger<ClaimsManagerController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: /ClaimsManager
        // list all claims on super admin

        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.FindByEmailAsync("cchimzi@appzonegroup.com");
            if (user == null)
            {
                return NotFound();
            }
            var role = await _roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Super Admin role not found";
                return View("NotFound");
            }
            var claims = await _roleManager.GetClaimsAsync(role);
            return View(claims);
        }

        // POST: /ClaimsManager/AddFunction
        // Create new function
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddFunction(string claimType)
        {
            var role = await _roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Super Admin role not found";
                return View("NotFound");
            }
            if (claimType != null)
            {
                ViewBag.ClaimType = claimType;
                var claim = new Claim(claimType.Trim(), claimType.Trim());
                await _roleManager.AddClaimAsync(role, claim);
            }
            return RedirectToAction("Index");
        }

        ////delete from super admin and all users
        ////POST: /ClaimsManager/Delete/claim.ID
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(string claimType)
        //{
        //    var claim = new Claim(claimType.Trim(), claimType.Trim());

        //    // get all roles
        //    var roles = _roleManager.Roles.ToList();

        //    if(roles == null)
        //    {
        //        ModelState.AddModelError("", "no roles found");
        //        return View("NotFound");
        //    }

        //    foreach (var role in roles)
        //    {
        //        var claimsInRole = await _roleManager.GetClaimsAsync(role);
        //        if (claimsInRole.Contains(claim))
        //        {
        //            var result = await _roleManager.RemoveClaimAsync(role, claim);
        //            if (!result.Succeeded)
        //            {
        //                ModelState.AddModelError("", "claim does not exist in role");
        //                return NotFound();
        //            }
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}