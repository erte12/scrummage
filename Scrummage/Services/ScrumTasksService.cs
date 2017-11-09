using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Scrummage.Core;
using Scrummage.Core.Services;
using Scrummage.Core.Services.Validation;
using Scrummage.Dtos;
using Scrummage.Models;

namespace Scrummage.Services
{
    public class ScrumTasksService : IScrumTasksService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IValidationDictionary _validationDictionary;

        public ScrumTasksService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Initialize(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
        }

        public ScrumTask Create(NewScrumTaskDto taskDto)
        {
            var newScrumTask = Mapper.Map<ScrumTask>(taskDto);

            _unitOfWork.ScrumTasks.Add(newScrumTask);
            _unitOfWork.Complate();

            return newScrumTask;
        }

        public ScrumTask Update(int taskId, UpdateScrumTaskDto taskDto)
        {
            var taskFromDb = _unitOfWork.ScrumTasks.Get(taskId);

            if (taskFromDb == null)
                return null;

            if (!string.IsNullOrWhiteSpace(taskDto.UserId))
                UpdateUser(taskFromDb, taskDto.UserId);
            if (taskDto.EstimationId != null)
                UpdateEstimation(taskFromDb, taskDto.EstimationId.Value);
            if (taskDto.Priority != null)
                UpdatePriority(taskFromDb, taskDto.Priority.Value);
            if (taskDto.TaskType != null)
                UpdateTaskType(taskFromDb, taskDto.TaskType.Value, taskDto.TookId);

            _unitOfWork.Complate();

            return taskFromDb;
        }

        private void UpdateUser(ScrumTask task, string userId)
        {
            var user = _unitOfWork.Users.Get(userId);
            task.UserId = user?.Id;
        }

        private void UpdateEstimation(ScrumTask task, int estimationId)
        {
            var estimation = _unitOfWork.Estimations.Get(estimationId);
            task.EstimationId = estimation?.Id;
        }

        private void UpdatePriority(ScrumTask task, byte priority)
        {
            if (!Enumerable.Range(1, 5).Contains(priority))
                task.Priority = null;

            task.Priority = priority;
        }

        private void UpdateTaskType(ScrumTask task, TaskType taskType, int? tookId = null)
        {
            task.TaskType = taskType;
            if (tookId != null)
            {
                var took = _unitOfWork.Estimations.Get(tookId.Value);

                if (took == null) return;

                task.Took = took;
                task.DoneAt = DateTime.Now;
            }
            else
                task.DoneAt = null;
        }
    }
}