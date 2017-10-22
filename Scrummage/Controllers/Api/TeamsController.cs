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

            teamDto.CreatedAt = DateTime.Now;

            var team = Mapper.Map<Team>(teamDto);

            _context.Teams.Add(team);
            _context.SaveChanges();

            teamDto.Id = team.Id;

            return Ok(teamDto.Id);
        }
    }
}
