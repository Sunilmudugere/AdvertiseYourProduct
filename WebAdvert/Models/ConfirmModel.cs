using System;
using System.ComponentModel.DataAnnotations;

namespace WebAdvert.Models
{
    public class ConfirmModel
    {
        [Required(ErrorMessage="Email is required")]
        [EmailAddress]
        [Display(Name="Email")]
        public string Email { get; set; }

        [Required(ErrorMessage="Code is required")]
        public string Code { get; set; }

    }
}