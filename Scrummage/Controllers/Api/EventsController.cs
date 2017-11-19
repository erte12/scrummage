using Scrummage.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Scrummage.Core.Domain;
using Scrummage.Models;
using Scrummage.Presentation.Dtos;

namespace Scrummage.Controllers.Api
{
    public class EventsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult CreateEvent(EventDto eventDto)
        {
            var newEvent = Mapper.Map<Event>(eventDto);
            newEvent.CreatedAt = DateTime.Now;

            _unitOfWork.Events.Add(newEvent);
            _unitOfWork.Complate();

            eventDto = Mapper.Map<EventDto>(newEvent);

            return Ok(eventDto);
        }

        [HttpGet]
        public IHttpActionResult GetEventsForTeam(int teamId)
        {
            var events = _unitOfWork.Events.GetByTeamId(teamId);

            var eventDto = Mapper.Map<IEnumerable<EventDto>>(events);
            return Ok(eventDto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteEvent(int id)
        {
            var eventToDelete = _unitOfWork.Events.Get(id);

            if (eventToDelete == null)
                return NotFound();

            _unitOfWork.Events.Remove(eventToDelete);
            _unitOfWork.Complate();

            return Ok();
        }
    }
}
