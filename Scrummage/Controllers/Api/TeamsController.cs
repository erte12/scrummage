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
    public class TeamsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public TeamsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateTeam(TeamDto teamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var team = Mapper.Map<Team>(teamDto);

            team.CreatedAt = DateTime.Now;

            _context.Teams.Add(team);
            _context.SaveChanges();

            teamDto.Id = team.Id;

            return Ok(teamDto);
        }

        public IHttpActionResult GetTeams()
        {
            var teams = _context.Teams
                .OrderBy(t => t.Name)
                .ToList()
                .Select(Mapper.Map<Team, TeamDto>);

            return Ok(teams);
        }

        [HttpDelete]
        public IHttpActionResult DeleteTeam(int id)
        {
            var team = _context.Teams.SingleOrDefault(t => t.Id == id);

            if (team == null)
                return NotFound();

            _context.Teams.Remove(team);
            _context.SaveChanges();

            return Ok();
        }
    }
}
