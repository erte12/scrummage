using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Scrummage.Core;
using Scrummage.Core.Services;
using Scrummage.Dtos;
using Scrummage.Models;
using Scrummage.Persistance;
using Scrummage.Services.Validation;

namespace Scrummage.Controllers.Api
{
    public class SprintsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISprintService _sprintService;

        public SprintsController(IUnitOfWork unitOfWork, ISprintService sprintService)
        {
            _unitOfWork = unitOfWork;
            _sprintService = sprintService;
            _sprintService.Initialize(new ValidationDictionaryWebApi(ModelState));
        }

        [HttpPatch]
        public IHttpActionResult UpdateSprint(int id, SprintDto sprintDto)
        {
            var sprint = _sprintService.Update(id, sprintDto);

            if (sprint == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
