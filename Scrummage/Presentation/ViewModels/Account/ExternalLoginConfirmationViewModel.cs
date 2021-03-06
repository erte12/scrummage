﻿using System.ComponentModel.DataAnnotations;

namespace Scrummage.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}