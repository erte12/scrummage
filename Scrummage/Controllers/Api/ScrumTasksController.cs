using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Scrummage.Core;
using Scrummage.Core.Services;
using Scrummage.Dtos;
using Scrummage.Presentation.Dtos;
using Scrummage.Services.Validation;

namespace Scrummage.Controllers.Api
{
    public class ScrumTasksController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IScrumTasksService _scrumTasksService;

        public ScrumTasksController(IUnitOfWork unitOfWork, IScrumTasksService scrumTasksService)
        {
            _unitOfWork = unitOfWork;
            _scrumTasksService = scrumTasksService;
        }

        [HttpPost]
        public IHttpActionResult CreateScrumTask(NewScrumTaskDto taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newScrumTask = _scrumTasksService.Create(taskDto);

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

            var task = _scrumTasksService.Update(id, taskDto);

            if (task == null)
                return NotFound();

            return Ok(new { took = task.Took?.Value });
        }

        [HttpGet]
        public IHttpActionResult GetScrumTasksForSprint(int sprintId)
        {
            var tasks = _unitOfWork.ScrumTasks.GetScrumTasksBySprintId(sprintId);
            var tasksDto = Mapper.Map<IEnumerable<ManageSprintScrumTaskDto>>(tasks);
            return Ok(tasksDto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _unitOfWork.Dispose();

            base.Dispose(disposing);
        }
    }
}
