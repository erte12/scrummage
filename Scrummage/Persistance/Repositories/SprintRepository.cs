using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Scrummage.Core.Repositories;
using Scrummage.Models;

namespace Scrummage.Persistance.Repositories
{
    public class SprintRepository : Repository<Sprint>, ISprintRepository
    {
        public SprintRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Sprint GetNewestForTeam(int teamId)
        {
            return ApplicationDbContext.Sprints
                .OrderByDescending(s => s.CreatedAt)
                .FirstOrDefault(s => s.TeamId == teamId);
        }

        public Sprint GetWithTeamAndTasks(int sprintId)
        {
            var sprint = ApplicationDbContext.Sprints
                .Include(s => s.Team)
                .Include(s => s.Team.Users)
                .SingleOrDefault(s => s.Id == sprintId);

            ApplicationDbContext.Sprints
                .OrderByDescending(s => s.CreatedAt)
                .Where(s => s.TeamId == sprint.TeamId)
                .Load();

            ApplicationDbContext.ScrumTasks
                .Where(s => s.SprintId == sprintId)
                .Include(s => s.Estimation)
                .Load();

            return sprint;
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