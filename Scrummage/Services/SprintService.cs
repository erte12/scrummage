using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Scrummage.Core;
using Scrummage.Core.Services;
using Scrummage.Models;

namespace Scrummage.Services
{
    public class SprintService : ISprintService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SprintService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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