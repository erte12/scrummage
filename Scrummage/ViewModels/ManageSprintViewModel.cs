using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Scrummage.Models;

namespace Scrummage.ViewModels
{
    public class ManageSprintViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public List<ApplicationUser> Users { get; set; }

        public List<ScrumTask> Tasks { get; set; }

        public IEnumerable<Estimation> Estimations { get; set; }
    }
}