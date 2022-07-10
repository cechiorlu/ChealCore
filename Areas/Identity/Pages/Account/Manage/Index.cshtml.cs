// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ChealCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChealCore.Data;
using Microsoft.EntityFrameworkCore;

namespace ChealCore.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public string Username { get; set; }

        // TODO: view assigned roles
        //public IEnumerable<string> Roles { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            [Display(Name = "Profile Picture")]
            public byte[] ProfilePicture { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            // TODO: view assigned roles
            //var roles = await _userManager.GetRolesAsync(user);
            var firstName = user.FirstName;
            var lastName = user.LastName;
            var profilePicture = user.ProfilePicture;

            Username = userName;
            // TODO: view assigned roles
            //Roles = roles;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                LastName = lastName,
                FirstName = firstName,
                ProfilePicture = profilePicture
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            // update phoneNumber
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            // update firstname
            var firstName = user.FirstName;
            if (Input.FirstName != firstName)
            {
                user.FirstName = Input.FirstName;
                IdentityResult setFirstNameResult = await _userManager.UpdateAsync(user);

                if (!setFirstNameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set first name.";
                    return RedirectToPage();
                }
            }

            // update lastname
            var lastName = user.LastName;
            if (Input.LastName != lastName)
            {
                user.LastName = Input.LastName;
                IdentityResult setLastNameResult = await _userManager.UpdateAsync(user);

                if (!setLastNameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set last name.";
                    return RedirectToPage();
                }
            }

            // update profile picture
            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.ProfilePicture = dataStream.ToArray();
                }
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
