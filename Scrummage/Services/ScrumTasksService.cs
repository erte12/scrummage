using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Scrummage.Core;
using Scrummage.Dtos;
using Scrummage.Models;

namespace Scrummage.Services
{
    public class ScrumTasksService : IScrumTasksService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScrumTasksService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ScrumTask Update(ScrumTask taskFromDb, UpdateScrumTaskDto taskDto)
        {
            if (!string.IsNullOrWhiteSpace(taskDto.UserId))
            {
                var user = _unitOfWork.Users.Get(taskDto.UserId);

                taskFromDb.UserId = user?.Id;
            }
            else if (taskDto.EstimationId != null)
            {
                var estimation = _unitOfWork.Estimations.Get(taskDto.EstimationId.Value);

                taskFromDb.EstimationId = estimation?.Id;
            }
            else if (taskDto.Priority != null)
            {
                if (taskDto.Priority.Value == 0)
                    taskDto.Priority = null;

                taskFromDb.Priority = taskDto.Priority;
            }
            else if (taskDto.TaskType != null)
            {
                taskFromDb.TaskType = taskDto.TaskType.Value;
                if (taskDto.TookId != null)
                {
                    var took = _unitOfWork.Estimations.Get(taskDto.TookId.Value);

                    if (took != null)
                    {
                        taskFromDb.TookId = took.Id;
                        taskFromDb.DoneAt = DateTime.Now;
                    }
                }
                else
                    taskFromDb.DoneAt = null;
            }

            _unitOfWork.Complate();

            return taskFromDb;
        }
    }
}