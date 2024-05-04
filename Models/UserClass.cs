using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; // Add the validation packages.
using System.Security.Cryptography;
using System.Web.Security;

namespace UserRegistration.Models
{
    public class UserClass
    {
        [Required(ErrorMessage = "Please enter a username!")] // This gives an error if the user name is not input.
        [Display(Name = "Enter a username:")] // This is the label for the user name.
        [StringLength(maximumLength: 15, MinimumLength = 3, 
            ErrorMessage = "Username should be longer than 3 characters, " +
            "up to a maximum of 15 characters.")] // This is the requirement criteria for the username input.
        public string Username { get; set; } // User ID/name Class property.

        [Required(ErrorMessage = "Please enter an email address!")] // Error if email is not input.
        // Email address validation.
        // REF: https://stackoverflow.com/questions/16712043/email-address-validation-using-asp-net-mvc-data-type-attributes
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$",
            ErrorMessage = "Email address is not valid. Please try again!")]
        [Display(Name = "Enter your email address:")] // Label for email.
        public string Useremail { get; set; } // User email Class property.

        [Required(ErrorMessage = "Please enter a password!")] // Error for password not input.
        // Password validation
        [MembershipPassword(ErrorMessage = "Minimum length of 7 characters: " +
            "Requires at least 1 special character and 1 number.")]
        [Display(Name = "Enter a password:")] // Label for password.
        [DataType(DataType.Password)] // Mask the password.
        public string Userpassword { get; set; } // User password Class property.

        [Required(ErrorMessage = "Please confirm your password!")] // Error for confirming password not input.
        [Display(Name = "Confirm your password:")] // Label for confirm password.
        [DataType(DataType.Password)] // Mask the password.
        [Compare("Userpassword")] // This checks that the confirm password matches the password input by the user.
        public string Confirmpassword { get; set; }

    }
}