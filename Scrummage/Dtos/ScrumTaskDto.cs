using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Scrummage.Models;

namespace Scrummage.Dtos
{
    public class ScrumTaskDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(400)]
        public string Content { get; set; }

        public byte? Estimation { get; set; }

        public byte? Took { get; set; }

        public TaskType TaskType { get; set; }

        public Sprint Sprint { get; set; }

        public int SprintId { get; set; }

        public string UserId { get; set; }
    }
}