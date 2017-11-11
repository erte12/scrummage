using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Scrummage.Models;

namespace Scrummage.Dtos
{
    public class UpdateScrumTaskDto
    {
        [MinLength(1)]
        public string Content { get; set; }

        public int? EstimationId { get; set; }

        [Range(0, 5)]
        public byte? Priority { get; set; }

        [Range(0, 2)]
        public TaskType? TaskType { get; set; }

        public int? TookId { get; set; }

        public string UserId { get; set; }
    }
}