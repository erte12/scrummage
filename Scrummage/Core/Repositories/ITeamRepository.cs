using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrummage.Models;

namespace Scrummage.Core.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        Team GetWithMembers(int id);
        Team GetWithMembersAndScrumMaster(int id);
        Team GetWithSprints(int id);
        IEnumerable<Team> GetMyTeams();
    }
}
