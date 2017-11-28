using System.Linq;
using System.Web.Http;
using AutoMapper;
using Scrummage.Core;
using Scrummage.Models;
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
        public IHttpActionResult GetUsers(string query = null, int? exceptTeamId = null)
        {
            var users = _unitOfWork.Users.GetAllByQuery(query, exceptTeamId);
         
            return Ok(users
                .ToList()
                .Select(Mapper.Map<ApplicationUser, ApplicationUserDto>));
        }
    }
}
