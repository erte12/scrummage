using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
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
        public IHttpActionResult CreateScrumTask(ScrumTaskNewDto taskDto)
        {
            var newScrumTask = Mapper.Map<ScrumTask>(taskDto);
            _scrumTasksService.Create(newScrumTask);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newScrumTaskDto = Mapper.Map<ScrumTaskDto>(newScrumTask);
            return Ok(newScrumTaskDto);
        }

        //Todo: Devide into two methods: UpdateType and UpdateData
        [HttpPatch]
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
        public IHttpActionResult GetScrumTasksForSprint(int sprintId, bool onlyActive = false)
        {
            var tasks = onlyActive
                ? _unitOfWork.ScrumTasks.GetActiveScrumTasksBySprintId(sprintId)
                : _unitOfWork.ScrumTasks.GetScrumTasksBySprintId(sprintId);

            var tasksDto = Mapper.Map<IEnumerable<ScrumTaskDto>>(tasks);
            return Ok(tasksDto);
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.ScrumMaster)]
        public IHttpActionResult DeleteScrumTask(int id)
        {
            var task = _unitOfWork.ScrumTasks.Get(id);
            if (task == null)
                return NotFound();

            _unitOfWork.ScrumTasks.Remove(task);
            _unitOfWork.Complate();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _unitOfWork.Dispose();

            base.Dispose(disposing);
        }
    }
}
