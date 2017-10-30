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

        public IEnumerable<ApplicationUser> GetAllByQuery(string query)
        {
            var users = ApplicationDbContext.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
                users = users.Where(u => u.Name.Contains(query) || u.Surname.Contains(query));

            return users.ToList();
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}