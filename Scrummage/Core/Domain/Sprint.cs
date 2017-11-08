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

        public List<ScrumTask> Tasks { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime StartsAt { get; set; }

        public DateTime EndsAt { get; set; }

        public Sprint()
        {
            Tasks = new List<ScrumTask>();
        }
    }
}