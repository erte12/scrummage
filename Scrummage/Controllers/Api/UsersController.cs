using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Scrummage.Dtos;
using Scrummage.Models;
using Scrummage.Persistance;

namespace Scrummage.Controllers.Api
{
    public class UsersController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;

        public UsersController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        public IHttpActionResult GetUsers(string query = null)
        {
            var users = _unitOfWork.Users.GetAllByQuery(query);
         
            return Ok(users
                .ToList()
                .Select(Mapper.Map<ApplicationUser, UserDto>));
        }
    }
}
