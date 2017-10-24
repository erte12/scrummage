using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scrummage.Models
{
    public class ScrumTask
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public byte? Estimation { get; set; }

        public byte? Took { get; set; }

        public TaskType TaskType { get; set; }

        public Sprint Sprint { get; set; }

        public int SprintId { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }

    public enum TaskType : byte
    {
        Awaiting,
        Ongoing,
        Done,
        Cancelled
    }
}