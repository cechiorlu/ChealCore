//using System;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace ChealCore.Controllers
//{
//    public class ClaimsController : Controller
//    {
//        private readonly ILogger<ClaimsController> _logger;
//        //private readonly ClaimsManager<IdentityRoleClaim<string>> _claimsManager;


//        //public ClaimsController(ILogger<ClaimsController> logger, ClaimsManager<IdentityRoleClaim<string>> claimsManager)
//        //{
//        //    _claimsManager = claimsManager;
//        //    _logger = logger;
//        //}

//        //public async Task<IActionResult> Index()
//        //{
//        //    var roles = await _roleManager.Roles.ToListAsync();
//        //    return View(roles);
//        //}
//    }
//}


   

//    // GET: /RoleManager
//    // list all roleŽ

  

//    // POST: /RoleManager
//    // create new role
//    [Authorize]
//    [HttpPost]
//    public async Task<IActionResult> AddRole(string roleName)
//    {
//        if (roleName != null)
//        {
//            await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
//        }
//        return RedirectToAction("Index");
//    }

//    // GET: /RoleManager/Manage/role.ID
//    public async Task<IActionResult> Manage(string id)
//    {
//        if (id == null || _roleManager.Roles == null)
//        {
//            return NotFound();
//        }

//        var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == id);

//        if (role == null)
//        {
//            return NotFound();
//        }

//        return View(role);
//    }


//    // POST: /RoleManager/Edit/role.ID
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Edit(ManageUserRolesViewModel model)
//    {
//        var role = await _roleManager.FindByIdAsync(model.Id);

//        if (role == null)
//        {
//            ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
//            return View("NotFound");
//        }
//        else
//        {
//            role.Name = model.Name;

//            // Update the Role using UpdateAsync
//            var result = await _roleManager.UpdateAsync(role);

//            if (result.Succeeded)
//            {
//                return RedirectToAction("Index");
//            }

//            foreach (var error in result.Errors)
//            {
//                ModelState.AddModelError("", error.Description);
//            }

//            return View(model);
//        }
//    }


//    // POST: /RoleManager/Delete/role.ID
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Delete(ManageUserRolesViewModel model)
//    {
//        var role = await _roleManager.FindByIdAsync(model.Id);

//        if (role == null)
//        {
//            ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
//            return View("NotFound");
//        }
//        else
//        {
//            // Update the Role using UpdateAsync
//            var result = await _roleManager.DeleteAsync(role);

//            if (result.Succeeded)
//            {
//                return RedirectToAction("Index");
//            }

//            foreach (var error in result.Errors)
//            {
//                ModelState.AddModelError("", error.Description);
//            }

//            return View(model);
//        }
//    }