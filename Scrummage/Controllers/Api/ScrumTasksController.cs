using System;
using System.Linq;
using System.Web.Http;
using Scrummage.Models;
using AutoMapper;
using Scrummage.Core;
using Scrummage.Dtos;
using Scrummage.Persistance;
using Scrummage.Services;

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

            var newScrumTask = Mapper.Map<ScrumTask>(taskDto);

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

            var task = _scrumTasksService.Update(id, taskDto);

            if (task == null)
                return NotFound();

            return Ok(new { took = task.Took?.Value });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _unitOfWork.Dispose();

            base.Dispose(disposing);
        }
    }
}
