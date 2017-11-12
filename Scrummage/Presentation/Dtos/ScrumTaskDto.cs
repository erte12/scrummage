using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Scrummage.Models;

namespace Scrummage.Presentation.Dtos
{
    public class ScrumTaskDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public ApplicationUserDto User { get; set; }

        public EstimationDto Estimation { get; set; }

        public EstimationDto Took { get; set; }

        public byte? Priority { get; set; }

        public byte TaskType { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}