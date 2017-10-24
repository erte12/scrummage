using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Scrummage.Models;

namespace Scrummage.ViewModels
{
    public class SprintViewModel
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public int TeamId { get; set; }
    }
}