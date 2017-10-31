using System.Linq;
using System.Web.Http;
using Scrummage.Models;
using AutoMapper;
using Scrummage.Core;
using Scrummage.Dtos;
using Scrummage.Persistance;

namespace Scrummage.Controllers.Api
{
    public class ScrumTasksController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScrumTasksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult CreateScrumTask(NewScrumTaskDto newScrumTaskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newScrumTask = Mapper.Map<ScrumTask>(newScrumTaskDto);

            _unitOfWork.ScrumTasks.Add(newScrumTask);
            _unitOfWork.Complate();

            return Ok(new
            {
                id = newScrumTask.Id,
                content = newScrumTask.Content
            });
        }

        [HttpPatch]
        public IHttpActionResult UpdateScrumTask(int id, UpdateScrumTaskDto taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var taskFromDb = _unitOfWork.ScrumTasks.Get(id);

            if (taskFromDb == null)
                return NotFound();

            var took = new Estimation();

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
                    took = _unitOfWork.Estimations.Get(taskDto.TookId.Value);
                    taskFromDb.TookId = took?.Id;
                }
            }

            _unitOfWork.Complate();

            return Ok(new {took = took?.Value});
        }
    }
}
