using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Scrummage.Core.Domain;
using Scrummage.Core.Repositories;
using Scrummage.Models;

namespace Scrummage.Persistance.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Event> GetByTeamId(int teamId)
        {
            return ApplicationDbContext.Events.Where(e => e.TeamId == teamId).ToList();
        }

        public Event GetWithTeam(int id)
        {
            return ApplicationDbContext.Events
                .Include(e => e.Team)
                .Include(e => e.Team.Users)
                .SingleOrDefault(e => e.Id == id);
        }

        public ApplicationDbContext ApplicationDbContext => Context as ApplicationDbContext;
    }
}