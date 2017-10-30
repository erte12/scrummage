using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Scrummage.Models;

namespace Scrummage.Dtos
{
    public class SprintDto
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }
    }
}