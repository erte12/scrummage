using System;
using System.Collections.Generic;
using System.Data;
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

        private void Validate(ScrumTask task)
        {
            if (task.Title == null || task.Title.Trim().Length == 0)
                _validationDictionary.AddError("Content", "Title is required.");
            if (task.Title != null && task.Title.Trim().Length < 3)
                _validationDictionary.AddError("Content", "Title must contain at least 3 characters.");
            if (task.Title != null && task.Title.Trim().Length > 40)
                _validationDictionary.AddError("Content", "Title must contain less than 40 characters.");
            if (task.Content == null || task.Content.Trim().Length == 0)
                _validationDictionary.AddError("Content", "Content is required.");
            if (task.Content != null && task.Content.Trim().Length < 3)
                _validationDictionary.AddError("Content", "Content must contain at least 3 characters.");
            if (task.Content != null && task.Content.Trim().Length > 400)
                _validationDictionary.AddError("Content", "Content must contain less than 400 characters.");
            if (_unitOfWork.Sprints.Get(task.SprintId) == null)
                _validationDictionary.AddError("SprintId", "Sprint with this id does not exist.");
        }

        public ScrumTask Create(ScrumTask newScrumTask)
        {
            newScrumTask.CreatedAt = DateTime.Now;

            Validate(newScrumTask);

            if (_validationDictionary.IsValid)
            {
                _unitOfWork.ScrumTasks.Add(newScrumTask);
                _unitOfWork.Complate();
            }

            return newScrumTask;
        }

        public ScrumTask Update(int taskId, ScrumTaskUpdateDto taskDto)
        {
            var taskFromDb = _unitOfWork.ScrumTasks.Get(taskId);

            if (taskFromDb == null)
                return null;

            if (!string.IsNullOrWhiteSpace(taskDto.Title))
                UpdateTitle(taskFromDb, taskDto.Title);
            if (!string.IsNullOrWhiteSpace(taskDto.Content))
                UpdateContent(taskFromDb, taskDto.Content);
            if (!string.IsNullOrWhiteSpace(taskDto.UserId))
                UpdateUser(taskFromDb, taskDto.UserId);
            if (taskDto.EstimationId != null)
                UpdateEstimation(taskFromDb, taskDto.EstimationId.Value);
            if (taskDto.Priority != null)
                UpdatePriority(taskFromDb, taskDto.Priority.Value);
            if (taskDto.TaskType != null)
                UpdateTaskType(taskFromDb, taskDto.TaskType.Value, taskDto.TookId);

            Validate(taskFromDb);

            if(_validationDictionary.IsValid)
                _unitOfWork.Complate();

            return taskFromDb;
        }

        private static void UpdateTitle(ScrumTask task, string title)
        {
            task.Title = title;
        }

        private static void UpdateContent(ScrumTask task, string content)
        {
            task.Content = content;
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

        private static void UpdatePriority(ScrumTask task, byte priority)
        {
            if (!Enumerable.Range(1, 5).Contains(priority))
                task.Priority = null;

            task.Priority = priority;
        }

        private void UpdateTaskType(ScrumTask task, TaskType taskType, int? tookId = null)
        {
            if (task.TaskType == TaskType.Done) return;

            if (tookId != null)
            {
                var took = _unitOfWork.Estimations.Get(tookId.Value);
                if (took == null) return;
                task.Took = took;
                task.DoneAt = DateTime.Today;
            }
            else
            {
                task.TookId = null;
                task.DoneAt = null;
            }

            task.TaskType = taskType;
        }
    }
}