using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Scrummage.Models;

namespace Scrummage.ViewModels
{
    public class SprintManageTaskRowViewModel
    {
        public ScrumTask Task { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }

        public IEnumerable<Estimation> Estimations { get; set; }
    }
}