﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scrummage.Models
{
    public class Estimation
    {
        public int Id { get; set; }

        public byte Value { get; set; }

        public ICollection<ScrumTask> ScrumTasksEstimated { get; set; }

        public ICollection<ScrumTask> ScrumTasksTaken { get; set; }
    }
}