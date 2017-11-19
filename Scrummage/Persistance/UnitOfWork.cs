using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Scrummage.Core;
using Scrummage.Core.Repositories;
using Scrummage.Models;
using Scrummage.Persistance.Repositories;

namespace Scrummage.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ITeamRepository Teams { get; }
        public IApplicationUserRepository Users { get; set; }
        public ISprintRepository Sprints { get; set; }
        public IEstimationRepository Estimations { get; set; }
        public IScrumTaskRepository ScrumTasks { get; set; }
        public IEventRepository Events { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Teams = new TeamRepository(context);
            Users = new ApplicationUserRepository(context);
            Sprints = new SprintRepository(context);
            Estimations = new EstimationRepository(context);
            ScrumTasks = new ScrumTaskRepository(context);
            Events = new EventRepository(context);
        }

        public int Complate()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}