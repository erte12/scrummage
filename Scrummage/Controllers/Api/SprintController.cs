﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Scrummage.Core;
using Scrummage.Dtos;
using Scrummage.Models;
using Scrummage.Persistance;

namespace Scrummage.Controllers.Api
{
    public class SprintController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public SprintController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPatch]
        public IHttpActionResult UpdateSprint(int id, SprintDto sprintDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var sprintFromDb = _unitOfWork.Sprints.Get(id);

            if (sprintFromDb == null)
                return NotFound();

            Mapper.Map(sprintDto, sprintFromDb);
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
