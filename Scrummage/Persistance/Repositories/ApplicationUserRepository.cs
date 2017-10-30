using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using AutoMapper.QueryableExtensions;
using Scrummage.Core.Repositories;
using Scrummage.Models;
using WebGrease.Css.Extensions;

namespace Scrummage.Persistance.Repositories
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(DbContext context) : base(context)
        {
        }

        public ApplicationUser Get(string id)
        {
            return ApplicationDbContext.Users
                .SingleOrDefault(a => a.Id.Equals(id));
        }

//        public IEnumerable<ApplicationUser> GetUsersWithActiveTasksBySprintId(int sprintId)
//        {
//            var users = ApplicationDbContext
//                .ScrumTasks
//                .Include(s => s.Estimation)
//                .Include(s => s.User)
//                .Where(s => s.SprintId == sprintId)
//                .Where(s => s.User != null && s.Estimation != null && s.Priority != null)
//                .GroupBy(s => s.User)
//                .AsEnumerable()
//                .Select(g => new ApplicationUser
//                {
//                    Id = g.Key.Id,
//                    Name = g.Key.Name,
//                    Surname = g.Key.Surname,
//                    ScrumTasks = g.ToList()
//                });
//
//            return users;
//        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}