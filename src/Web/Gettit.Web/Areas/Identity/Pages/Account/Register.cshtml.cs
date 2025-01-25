// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using Gettit.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gettit.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private const string AdministratorRole = "Administrator";

        private const string UserRole = "User";

        private readonly SignInManager<GettitUser> _signInManager;
        private readonly UserManager<GettitUser> _userManager;
        private readonly IUserStore<GettitUser> _userStore;
        private readonly IUserEmailStore<GettitUser> _emailStore;

        public RegisterModel(
            UserManager<GettitUser> userManager,
            IUserStore<GettitUser> userStore,
            SignInManager<GettitUser> signInManager)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Username, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    if (_userManager.Users.Count() == 1)
                    {
                        await _userManager.AddToRoleAsync(user, AdministratorRole);
                    } 
                    else
                    {
                        await _userManager.AddToRoleAsync(user, UserRole);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect("/");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private GettitUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<GettitUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(GettitUser)}'. " +
                    $"Ensure that '{nameof(GettitUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<GettitUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<GettitUser>)_userStore;
        }
    }
}
