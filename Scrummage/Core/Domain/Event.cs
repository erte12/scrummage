using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Scrummage.Models;

namespace Scrummage.Core.Domain
{
    public class Event
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public Team Team { get; set; }

        public int TeamId { get; set; }

        public DateTime StartsAt { get; set; }

        public DateTime? EndsAt { get; set; }
    }
}