using System;
using System.ComponentModel.DataAnnotations;

namespace WebAdvert.Models
{
    public class SignUpModel
    {
        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string Email { get; set; }
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        public string Password{get;set;}
        [Display(Name="ConfirmPassword")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password doesnt match")]
        public string ConfirmPassword{get;set;}
    }
}