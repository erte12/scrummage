using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Scrummage.Models;

namespace Scrummage.Dtos
{
    public class ScrumTaskUpdateDto
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int? EstimationId { get; set; }

        public byte? Priority { get; set; }

        public TaskType? TaskType { get; set; }

        public int? TookId { get; set; }

        public string UserId { get; set; }
    }
}