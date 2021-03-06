﻿using System;
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

        public Sprint GetWithTeam(int sprintId)
        {
            var sprint = ApplicationDbContext.Sprints
                .Include(s => s.Team)
                .SingleOrDefault(s => s.Id == sprintId);

            return sprint;
        }

        public Sprint GetWithTeamAndUsers(int sprintId)
        {
            var sprint = ApplicationDbContext.Sprints
                .Include(s => s.Team)
                .Include(s => s.Team.Users)
                .Include(s => s.Team.ScrumMaster)
                .SingleOrDefault(s => s.Id == sprintId);

            return sprint;
        }

        public Sprint GetWithActiveTasks(int sprintId)
        {
            var sprint = ApplicationDbContext.Sprints
                .SingleOrDefault(s => s.Id == sprintId);

            ApplicationDbContext.ScrumTasks
                .Where(s => s.SprintId == sprintId)
                .Where(s => s.Estimation != null && s.UserId != null && s.Priority != null)
                .Include(s => s.Estimation)
                .Load();

            return sprint;
        }

        public Sprint GetWithTeamAndTasks(int sprintId)
        {
            var sprint = ApplicationDbContext.Sprints
                .Include(s => s.Team)
                .Include(s => s.Team.Users)
                .SingleOrDefault(s => s.Id == sprintId);

            ApplicationDbContext.ScrumTasks
                .Where(s => s.SprintId == sprintId)
                .Include(s => s.Estimation)
                .Load();

            return sprint;
        }

        public IEnumerable<Sprint> GetByTeamId(int teamId)
        {
            var sprints = ApplicationDbContext.Sprints
                .Where(s => s.TeamId == teamId)
                .OrderByDescending(s => s.CreatedAt)
                .ToList();

            return sprints;
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