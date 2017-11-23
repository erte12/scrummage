using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.Identity;
using Scrummage.Core.Repositories;
using Scrummage.Models;

namespace Scrummage.Persistance.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public Team GetWithMembers(int id)
        {
            return ApplicationDbContext.Teams
                .Include(t => t.Users)
                .SingleOrDefault(t => t.Id == id);
        }

        public Team GetWithMembersAndScrumMaster(int id)
        {
            return ApplicationDbContext.Teams
                .Include(t => t.Users)
                .Include(t => t.ScrumMaster)
                .SingleOrDefault(t => t.Id == id);
        }

        public Team GetWithSprints(int id)
        {
            return ApplicationDbContext.Teams
                .Include(t => t.Sprints)
                .SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<Team> GetMyTeams()
        {
            var currentUserId = HttpContext.Current.User.Identity.GetUserId();

            return ApplicationDbContext.Teams
                .Include(t => t.Users)
                .Include(t => t.ScrumMaster)
                .Where(t => t.Users.Select(u => u.Id).Contains(currentUserId) ||
                            t.ScrumMaster.Id.Equals(currentUserId))
                .ToList();
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}