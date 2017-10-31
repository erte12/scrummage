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
        public IHttpActionResult UpdateScrumTask(int id, UpdateScrumTaskDto updateScrumTaskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var scrumTaskFromDb = _unitOfWork.ScrumTasks.Get(id);

            if (scrumTaskFromDb == null)
                return NotFound();

            if (!string.IsNullOrWhiteSpace(updateScrumTaskDto.UserId))
            {
                var user = _unitOfWork.Users.Get(updateScrumTaskDto.UserId);

                scrumTaskFromDb.UserId = user?.Id;
            }
            else if (updateScrumTaskDto.EstimationId != null)
            {
                var estimation = _unitOfWork.Estimations.Get(updateScrumTaskDto.EstimationId.Value);

                scrumTaskFromDb.EstimationId = estimation?.Id;
            }
            else if (updateScrumTaskDto.Priority != null)
            {
                if (updateScrumTaskDto.Priority.Value == 0)
                    updateScrumTaskDto.Priority = null;

                scrumTaskFromDb.Priority = updateScrumTaskDto.Priority;
            }
            else if (updateScrumTaskDto.TaskType != null)
                scrumTaskFromDb.TaskType = updateScrumTaskDto.TaskType.Value;

            _unitOfWork.Complate();

            return Ok();
        }
    }
}
