using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Scrummage.Dtos;
using Scrummage.Models;

namespace Scrummage.Controllers.Api
{
    public class UsersController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public UsersController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetUsers(string query = null)
        {
            var users = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                users = users.Where(u => u.Name.Contains(query) || u.Surname.Contains(query));
            }
         
            return Ok(users
                .ToList()
                .Select(Mapper.Map<ApplicationUser, UserDto>));
        }
    }
}
