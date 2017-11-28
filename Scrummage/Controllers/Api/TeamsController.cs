﻿using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
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

        public IHttpActionResult GetTeams()
        {
            var teams = _unitOfWork.Teams.GetMyTeams();
            return Ok(Mapper.Map<IEnumerable<TeamDto>>(teams));
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.ScrumMaster)]
        [TeamAccessActionFilter]
        public IHttpActionResult DeleteTeam(int id)
        {
            var success = _teamsService.DeleteTeam(id);
            
            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = RoleName.ScrumMaster)]
        [TeamAccessActionFilter]
        public IHttpActionResult AddMember(int teamId, string memberId)
        {
            var success = _teamsService.AddMember(teamId, memberId);
            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.ScrumMaster)]
        [TeamAccessActionFilter]
        public IHttpActionResult RemoveMember(int teamId, string memberId)
        {
            var success = _teamsService.RemoveMember(teamId, memberId);
            if (!success)
                return BadRequest();

            return Ok();
        }
    }
}