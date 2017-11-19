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
    public class EventController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventController(IUnitOfWork unitOfWork)
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
    }
}
