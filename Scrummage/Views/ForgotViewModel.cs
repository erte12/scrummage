using System.ComponentModel.DataAnnotations;

namespace Scrummage.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}