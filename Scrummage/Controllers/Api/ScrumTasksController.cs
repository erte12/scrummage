using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Scrummage.Controllers.ApiActionFilters;
using Scrummage.Core;
using Scrummage.Core.Services;
using Scrummage.Dtos;
using Scrummage.Models;
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
            _scrumTasksService.Initialize(new ValidationDictionaryWebApi(ModelState));
        }

        [HttpPost]
        [Authorize(Roles = RoleName.ScrumMaster)]
        [SprintAccessActionFilter]
        public IHttpActionResult CreateScrumTask(int sprintId, ScrumTaskNewDto taskDto)
        {
            var newScrumTask = Mapper.Map<ScrumTask>(taskDto);
            newScrumTask.SprintId = sprintId;

            _scrumTasksService.Create(newScrumTask);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newScrumTaskDto = Mapper.Map<ScrumTaskDto>(newScrumTask);
            return Ok(newScrumTaskDto);
        }

        //Todo: Devide into two methods: UpdateType and UpdateData
        [HttpPatch]
        [ScrumTaskAccessActionFilter]
        public IHttpActionResult UpdateScrumTask(int id, ScrumTaskUpdateDto taskDto)
        {
            var task = _scrumTasksService.Update(id, taskDto);
            if (task == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(new { took = task.Took?.Value });
        }

        [HttpGet]
        [SprintAccessActionFilter]
        public IHttpActionResult GetScrumTasksForSprint(int sprintId, bool onlyActive = false)
        {
            var tasks = onlyActive
                ? _unitOfWork.ScrumTasks.GetActiveScrumTasksBySprintId(sprintId)
                : _unitOfWork.ScrumTasks.GetScrumTasksBySprintId(sprintId);

            var tasksDto = Mapper.Map<IEnumerable<ScrumTaskDto>>(tasks);
            return Ok(tasksDto);
        }

        [HttpGet]
        [ScrumTaskAccessActionFilter]
        public IHttpActionResult GetScrumTask(int id)
        {
            var scrumTask = _unitOfWork.ScrumTasks.GetWithDetails(id);
            if (scrumTask == null)
                return NotFound();

            var scrumTaskDto = Mapper.Map<ScrumTaskDto>(scrumTask);
            return Ok(scrumTaskDto);
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.ScrumMaster)]
        [ScrumTaskAccessActionFilter]
        public IHttpActionResult DeleteScrumTask(int id)
        {
            var task = _unitOfWork.ScrumTasks.Get(id);
            if (task == null)
                return NotFound();

            _unitOfWork.ScrumTasks.Remove(task);
            _unitOfWork.Complate();
            return Ok();
        }
    }
}
