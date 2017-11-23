using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Scrummage.Core;
using Scrummage.Core.Services;
using Scrummage.Dtos;
using Scrummage.Models;
using Scrummage.Persistance;
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

        public IHttpActionResult GetTeams()
        {
            var teams = _unitOfWork.Teams.GetMyTeams();
            return Ok(Mapper.Map<IEnumerable<TeamDto>>(teams));
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.ScrumMaster)]
        public IHttpActionResult DeleteTeam(int id)
        {
            var success = _teamsService.DeleteTeam(id);
            
            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = RoleName.ScrumMaster)]
        public IHttpActionResult AddMember(MemberTeamDto memberTeam)
        {
            var success = _teamsService.AddMember(memberTeam.TeamId, memberTeam.MemberId);
            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.ScrumMaster)]
        public IHttpActionResult RemoveMember(MemberTeamDto memberTeam)
        {
            var success = _teamsService.RemoveMember(memberTeam.TeamId, memberTeam.MemberId);
            if (!success)
                return BadRequest();

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