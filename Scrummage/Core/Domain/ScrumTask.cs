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

        public Estimation Estimation { get; set; }

        public int? EstimationId { get; set; }

        public Estimation Took { get; set; }

        public int? TookId { get; set; }

        public byte? Priority { get; set; }

        public TaskType TaskType { get; set; }

        public Sprint Sprint { get; set; }

        public int SprintId { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public DateTime? DoneAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public enum TaskType : byte
    {
        Awaiting,
        Ongoing,
        Done,
        Cancelled
    }
}