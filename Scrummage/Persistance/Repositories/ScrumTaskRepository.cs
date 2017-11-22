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
            return GetTasksBySprintIdQuery(sprintId).ToList();
        }

        public IEnumerable<ScrumTask> GetActiveScrumTasksBySprintId(int sprintId)
        {
            return GetTasksBySprintIdQuery(sprintId)
                .Where(s => s.Estimation != null && s.UserId != null && s.Priority != null)
                .ToList();
        }

        public ScrumTask GetWithDetails(int id)
        {
            return ApplicationDbContext.ScrumTasks
                .Include(s => s.User)
                .Include(s => s.Estimation)
                .Include(s => s.Took)
                .SingleOrDefault(s => s.Id == id);
        }

        private IQueryable<ScrumTask> GetTasksBySprintIdQuery(int sprintId)
        {
            return ApplicationDbContext.ScrumTasks
                .Where(s => s.SprintId == sprintId)
                .Include(s => s.User)
                .Include(s => s.Estimation)
                .Include(s => s.Took);
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