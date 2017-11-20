using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Scrummage.Core;
using Scrummage.Dtos;
using Scrummage.Models;
using Scrummage.Persistance;
using Scrummage.Presentation.Dtos;

namespace Scrummage.Controllers.Api
{
    public class UsersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = RoleName.ScrumMaster)]
        public IHttpActionResult GetUsers(string query = null)
        {
            var users = _unitOfWork.Users.GetAllByQuery(query);
         
            return Ok(users
                .ToList()
                .Select(Mapper.Map<ApplicationUser, ApplicationUserDto>));
        }

        [HttpPatch]
        public IHttpActionResult UpdateUserDefaultTeam(int teamId)
        {
            var user = _unitOfWork.Users.Get(User.Identity.GetUserId());
            user.DefaultTeamId = teamId;
            _unitOfWork.Complate();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _unitOfWork.Dispose();

            base.Dispose(disposing);
        }
    }
}
