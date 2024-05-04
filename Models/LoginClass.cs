using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; // To validate the data.

namespace UserRegistration.Models
{
    public class LoginClass
    {
        [Required(ErrorMessage = "Please enter your username!")] // Error message if Username is not input.
        [Display(Name = "Username:")] // Label for Username.
        public string Username { get; set; } // Attribute for username.

        [Required(ErrorMessage = "Please enter your email address to login!")] // Error message if Useremail is not input.
        [Display(Name = "Email Address:")] // Label for Useremail.
        public string Useremail { get; set; } // Attribute for Useremail.

        [Required(ErrorMessage = "Please enter your password!")] // Error message if Userpassword is not input.
        [Display(Name = "Password:")] // Label for User password.
        [DataType(DataType.Password)] // Masks the password.
        public string Userpassword { get; set; } // Attribute for User password.
    }
}