using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Scrummage.Models;

namespace Scrummage.Presentation.ViewModels
{
    public class StatisticsSprintViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //TODO: Implement DTO
        public Team Team { get; set; }

        public DateTime StartsAt { get; set; }

        public DateTime EndsAt { get; set; }
    }
}