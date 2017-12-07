using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Scrummage.Controllers.ApiActionFilters;
using Scrummage.Core;
using Scrummage.Core.Services;
using Scrummage.Dtos;
using Scrummage.Models;
using Scrummage.Services.Validation;

namespace Scrummage.Controllers.Api
{
    public class TeamsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITeamsService _teamsService;

        public TeamsController(IUnitOfWork unitOfWork, ITeamsService teamsService)
        {
            _unitOfWork = unitOfWork;
            _teamsService = teamsService;
            _teamsService.Initialize(new ValidationDictionaryWebApi(ModelState));
        }

        [HttpPost]
        [Authorize(Roles = RoleName.ScrumMaster)]
        public IHttpActionResult CreateTeam(TeamDto teamDto)
        {
            var team = Mapper.Map<Team>(teamDto);
            _teamsService.Create(team);

            if (!ModelState.IsValid)
                return BadRequest();

            teamDto = Mapper.Map<TeamDto>(team);
            return Ok(teamDto);
        }

        [HttpGet]
        public IHttpActionResult GetMyTeams()
        {
            var teams = _unitOfWork.Teams.GetMyTeams();
            return Ok(Mapper.Map<IEnumerable<TeamDto>>(teams));
        }

        [HttpGet]
        [Route("Api/Teams/GetTeamsByQuery/{query}")]
        public IHttpActionResult GetTeamsByQuery(string query = null)
        {
            var teams = _unitOfWork.Teams.GetTeamsByQuery(query);
            return Ok(Mapper.Map<IEnumerable<TeamDto>>(teams));
        }

        [HttpGet]
        [TeamReadAccessActionFilter]
        [Route("Api/Teams/IsScrumMasterOfTheTeam/{id}")]
        public IHttpActionResult IsLoggedUserScrumMasterOfTheTeam(int id)
        {
            var team = _unitOfWork.Teams.Get(id);
            var loggedUserId = User.Identity.GetUserId();

            return Ok(team.ScrumMasterId.Equals(loggedUserId));
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.ScrumMaster)]
        [TeamUpdateAccessActionFilter]
        public IHttpActionResult DeleteTeam(int id)
        {
            var success = _teamsService.DeleteTeam(id);
            
            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = RoleName.ScrumMaster)]
        [TeamUpdateAccessActionFilter]
        public IHttpActionResult AddMember(int teamId, string memberId)
        {
            var success = _teamsService.AddMember(teamId, memberId);
            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult SendRequest(int id)
        {
            var success = _teamsService.AddUserToWaitingList(id);
            if (!success)
                return BadRequest();
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.ScrumMaster)]
        [TeamUpdateAccessActionFilter]
        public IHttpActionResult RemoveMember(int teamId, string memberId)
        {
            var success = _teamsService.RemoveMember(teamId, memberId);
            if (!success)
                return BadRequest();

            return Ok();
        }
    }
}