using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class TeamsController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;

        public TeamsController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        [HttpPost]
        public IHttpActionResult CreateTeam(TeamDto teamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var team = Mapper.Map<Team>(teamDto);
            team.CreatedAt = DateTime.Now;

            _unitOfWork.Teams.Add(team);
            _unitOfWork.Complate();

            teamDto.Id = team.Id;

            return Ok(teamDto);
        }

        public IHttpActionResult GetTeams()
        {
            var teams = _unitOfWork.Teams.GetAll();

            return Ok(teams.Select(Mapper.Map<Team, TeamDto>));
        }

        [HttpDelete]
        public IHttpActionResult DeleteTeam(int id)
        {
            var team = _unitOfWork.Teams.Get(id);

            if (team == null)
                return NotFound();

            _unitOfWork.Teams.Remove(team);
            _unitOfWork.Complate();

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult AddMember(MemberTeamDto memberTeam)
        {
            var user = _unitOfWork.Users.Get(memberTeam.MemberId);

            if (user == null)
                return NotFound();

            var team = _unitOfWork.Teams.Get(memberTeam.TeamId);

            if (team == null)
                return NotFound();

            team.Users.Add(user);
            _unitOfWork.Complate();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult RemoveMember(MemberTeamDto memberTeam)
        {
            var user = _unitOfWork.Users.Get(memberTeam.MemberId);

            if (user == null)
                return BadRequest();

            var team = _unitOfWork.Teams
                .GetTeamWithMembers(memberTeam.TeamId);

            if (team == null)
                return BadRequest();

            team.Users.Remove(user);
            _unitOfWork.Complate();

            return Ok();
        }
    }
}
