using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrummage.Core.Domain;
using Scrummage.Models;

namespace Scrummage.Core.Services
{
    public interface ITeamsService : IService
    {
        Team Create(Team newTeam);
        bool AddMember(int teamId, string newMemberId);
        bool RemoveMember(int teamId, string memberId);
        bool DeleteTeam(int teamId);
    }
}
