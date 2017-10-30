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

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Teams = new TeamRepository(context);
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