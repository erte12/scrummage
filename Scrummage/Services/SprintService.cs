using System;
using System.Linq;
using AutoMapper;
using Scrummage.Core;
using Scrummage.Core.Services;
using Scrummage.Core.Services.Validation;
using Scrummage.Dtos;
using Scrummage.Models;

namespace Scrummage.Services
{
    public class SprintService : ISprintService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IValidationDictionary _validationDictionary;

        public SprintService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Initialize(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
        }

        private void Validate(Sprint sprint)
        {
            if (sprint.Name == null || sprint.Name.Trim().Length == 0)
                _validationDictionary.AddError("Name", "Name is required.");
            if (sprint.Name != null && sprint.Name.Trim().Length < 3)
                _validationDictionary.AddError("Name", "Name must contain at least 3 characters.");
            if (sprint.Name != null && sprint.Name.Length > 60)
                _validationDictionary.AddError("Name", "Name must contain less than 60 characters.");
            if (sprint.Description != null && sprint.Description.Length > 1000)
                _validationDictionary.AddError("Description", "Description must contain less than 1000 characters.");
            if (sprint.StartsAt > sprint.EndsAt)
                _validationDictionary.AddError("StartsAt", "Starting date must earlier than ending date.");
        }

        public Sprint Create(Sprint sprintToCreate)
        {
            sprintToCreate.CreatedAt = DateTime.Now;

            Validate(sprintToCreate);

            if (_validationDictionary.IsValid)
            {
                _unitOfWork.Sprints.Add(sprintToCreate);
                _unitOfWork.Complate();
            }

            return sprintToCreate;
        }

        public Sprint Update(int id, SprintDto sprintDto)
        {
            var sprintFromDb = _unitOfWork.Sprints.Get(id);
            if (sprintFromDb == null)
                return null;

            Mapper.Map(sprintDto, sprintFromDb);

            Validate(sprintFromDb);

            if(_validationDictionary.IsValid)
                _unitOfWork.Complate();

            return sprintFromDb;
        }
    }
}