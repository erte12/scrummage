using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Scrummage.Models;

namespace Scrummage.Presentation.Dtos
{
    public class ManageSprintScrumTaskDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public ApplicationUserDto User { get; set; }

        public int? EstimationId { get; set; }

        public byte? Priority { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}