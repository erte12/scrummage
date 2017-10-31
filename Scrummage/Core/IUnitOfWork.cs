using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrummage.Core.Repositories;

namespace Scrummage.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ITeamRepository Teams { get; }
        IApplicationUserRepository Users { get; }
        IScrumTaskRepository ScrumTasks { get; }
        IEstimationRepository Estimations { get; }
        ISprintRepository Sprints { get; }

        int Complate();
    }
}
