﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrummage.Models;

namespace Scrummage.Core.Repositories
{
    public interface IScrumTaskRepository : IRepository<ScrumTask>
    {
        IEnumerable<ScrumTask> GetScrumTasksBySprintId(int sprintId);
    }
}
