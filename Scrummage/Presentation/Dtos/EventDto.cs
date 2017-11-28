using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Scrummage.Presentation.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        [Required(ErrorMessage = "Starting date is required.")]
        public DateTime StartsAt { get; set; }

        [Required(ErrorMessage = "Ending date is required.")]
        public DateTime EndsAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}