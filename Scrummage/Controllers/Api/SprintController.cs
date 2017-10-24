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
    public class SprintController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public SprintController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPatch]
        public IHttpActionResult UpdateSprint(int id, SprintDto sprintDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var sprintFromDb = _context.Sprints
                .SingleOrDefault(s => s.Id == id);

            if (sprintFromDb == null)
                return NotFound();

            Mapper.Map(sprintDto, sprintFromDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
