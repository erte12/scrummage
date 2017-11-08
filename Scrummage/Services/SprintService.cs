using System;
using Scrummage.Core;
using Scrummage.Core.Services;
using Scrummage.Core.Services.Validation;
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

        public Sprint Create(Sprint sprintToCreate)
        {
            //TODO: Validation
            sprintToCreate.CreatedAt = DateTime.Now;

            _unitOfWork.Sprints.Add(sprintToCreate);
            _unitOfWork.Complate();

            return sprintToCreate;
        }
    }
}