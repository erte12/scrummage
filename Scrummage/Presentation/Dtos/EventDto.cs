using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scrummage.Presentation.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int TeamId { get; set; }

        public DateTime StartsAt { get; set; }

        public DateTime EndsAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}