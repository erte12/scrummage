using System.Collections.Generic;
using Scrummage.Core.Domain;

namespace Scrummage.Core.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        IEnumerable<Event> GetByTeamId(int teamId);
        Event GetWithTeam(int id);
    }
}
