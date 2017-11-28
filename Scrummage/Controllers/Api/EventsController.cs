using Scrummage.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Scrummage.Controllers.ApiActionFilters;
using Scrummage.Core.Domain;
using Scrummage.Core.Services;
using Scrummage.Models;
using Scrummage.Presentation.Dtos;
using Scrummage.Services.Validation;

namespace Scrummage.Controllers.Api
{
    public class EventsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventService _eventService;

        public EventsController(IUnitOfWork unitOfWork, IEventService eventService)
        {
            _unitOfWork = unitOfWork;
            _eventService = eventService;
            _eventService.Initialize(new ValidationDictionaryWebApi(ModelState));
        }

        [HttpPost]
        [TeamAccessActionFilter]
        public IHttpActionResult CreateEvent(int teamId, EventDto eventDto)
        {
            var newEvent = Mapper.Map<Event>(eventDto);
            newEvent.TeamId = teamId;

            _eventService.Create(newEvent);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            eventDto = Mapper.Map<EventDto>(newEvent);

            return Ok(eventDto);
        }

        [HttpGet]
        [TeamAccessActionFilter]
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
