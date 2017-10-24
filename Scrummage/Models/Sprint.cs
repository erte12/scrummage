using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scrummage.Models
{
    public class Sprint
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Team Team { get; set; }

        public int TeamId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}