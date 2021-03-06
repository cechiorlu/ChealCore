// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using ChealCore.Enums;
using ChealCore.Models;
using EmailService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
//using ChealCore.Enum;

namespace ChealCore.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            //[Required]
            //[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            //[DataType(DataType.Password)]
            //[Display(Name = "Password")]
            //public string Password { get; set; }

            //[DataType(DataType.Password)]
            //[Display(Name = "Confirm password")]
            //[Compare("Password", ErrorMessage = "Passwords do not match.")]
            //public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                MailAddress address = new MailAddress(Input.Email);
                string userName = address.User;

                var user = CreateUser();

                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.UserName = userName;
                string initials = user.FirstName[0].ToString() + user.LastName[0].ToString();
                string generatedPassword = CreateRandomPassword();

                // create placeholder profile image
                using (WebClient webClient = new WebClient())
                {
                    try
                    {
                        byte[] profilePicture = webClient.DownloadData($"https://fakeimg.pl/250x250/?retina=1&text={initials}&font=noto");
                        user.ProfilePicture = profilePicture;
                    }
                    catch
                    {
                        throw new InvalidOperationException($"Can't create placeholder image for new user '{nameof(ApplicationUser)}'");
                    }
                }

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, generatedPassword);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // add basic role by default
                    await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl, password = generatedPassword });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }

        private string CreateRandomPassword()
        {
            try
            {
                byte[] specialCharacters = new byte[2];
                byte[] numbers = new byte[2];
                byte[] upperCaseLetters = new byte[2];
                byte[] lowercaseLetters = new byte[2];
                string result = "";

                for (int i = 0; i < 2; i++)
                {
                    specialCharacters[i] = (byte)new Random().Next(33, 47);
                }

                for (int i = 0; i < 2; i++)
                {
                    numbers[i] = (byte)new Random().Next(48, 57);
                }

                for (int i = 0; i < 2; i++)
                {
                    upperCaseLetters[i] = (byte)new Random().Next(65, 90);
                }

                for (int i = 0; i < 2; i++)
                {
                    lowercaseLetters[i] = (byte)new Random().Next(97, 122);
                }

                string s = Encoding.ASCII.GetString(specialCharacters)
                    + Encoding.ASCII.GetString(numbers)
                    + Encoding.ASCII.GetString(upperCaseLetters)
                    + Encoding.ASCII.GetString(lowercaseLetters);

                result = new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());

                return result;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
