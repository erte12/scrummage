using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Scrummage.Core.Repositories;
using Scrummage.Models;

namespace Scrummage.Persistance.Repositories
{
    public class ScrumTaskRepository : Repository<ScrumTask>, IScrumTaskRepository
    {
        public ScrumTaskRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<ScrumTask> GetScrumTasksBySprintId(int sprintId)
        {
            return ApplicationDbContext.ScrumTasks
                .Where(s => s.SprintId == sprintId)
                .Include(s => s.User)
                .ToList();
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get
            {
                return Context as ApplicationDbContext;
            }
        }
    }
}