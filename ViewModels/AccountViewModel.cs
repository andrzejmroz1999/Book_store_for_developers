using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace book_store_for_developers.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Enter e-mail")]
        [EmailAddress]
        public string Email { get; set; }
         
        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [Display(Name = "remember me")]
        public bool RememberMe { get; set; }

    }
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "The email field is required")]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(30,ErrorMessage = "{0} must have at least {2} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "The password field is required")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",ErrorMessage = "Password and confirm password are different")]
        public string ConfirmPassword { get; set; }

    }
}