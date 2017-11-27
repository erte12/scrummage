using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrummage.Models;

namespace Scrummage.Core.Repositories
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser Get(string id);
        IEnumerable<ApplicationUser> GetAllByQuery(string query, int? exceptTeamId);
    }
}
