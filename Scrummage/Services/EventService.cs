using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Scrummage.Core;
using Scrummage.Core.Domain;
using Scrummage.Core.Services;
using Scrummage.Core.Services.Validation;

namespace Scrummage.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IValidationDictionary _validationDictionary;

        public EventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Initialize(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
        }

        private void Validate(Event eventToSave)
        {
            if (eventToSave.Content == null || eventToSave.Content.Trim().Length == 0)
                _validationDictionary.AddError("Content", "Content is required.");
            if (eventToSave.Content != null && eventToSave.Content.Trim().Length < 3)
                _validationDictionary.AddError("Content", "Content must contain at least 3 characters.");
            if (eventToSave.Content != null && eventToSave.Content.Trim().Length > 200)
                _validationDictionary.AddError("Content", "Content must contain less than 200 characters.");
            if (eventToSave.StartsAt > eventToSave.EndsAt)
                _validationDictionary.AddError("StartsAt", "Starting date must earlier than ending date.");
            if (_unitOfWork.Teams.Get(eventToSave.TeamId) == null)
                _validationDictionary.AddError("TeamId", "Team with this id does not exist.");
        }

        public Event Create(Event newEvent)
        {
            newEvent.CreatedAt = DateTime.Now;
            Validate(newEvent);

            if (_validationDictionary.IsValid)
            {
                _unitOfWork.Events.Add(newEvent);
                _unitOfWork.Complate();
            }

            return newEvent;
        }
    }
}