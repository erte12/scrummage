using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrummage.Core.Repositories;

namespace Scrummage.Core
{
    interface IUnitOfWork : IDisposable
    {
        ITeamRepository Teams { get; }
        int Complate();
    }
}
