using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Scrummage.Models;

namespace Scrummage.ViewModels
{
    public class SprintBoardViewModel
    {
        public int Id { get; set; }

        public Team Team { get; set; }

        public IEnumerable<Sprint> TeamSprints { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }

        public IEnumerable<Estimation> Estimations { get; set; }
    }
}