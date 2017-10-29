using System.Linq;
using System.Web.Http;
using Scrummage.Models;
using AutoMapper;
using Scrummage.Dtos;

namespace Scrummage.Controllers.Api
{
    public class ScrumTasksController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public ScrumTasksController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateScrumTask(NewScrumTaskDto newScrumTaskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newScrumTask = Mapper.Map<ScrumTask>(newScrumTaskDto);

            _context.ScrumTasks.Add(newScrumTask);
            _context.SaveChanges();

            return Ok(new {id = newScrumTask.Id, content = newScrumTask.Content});
        }

        [HttpPatch]
        public IHttpActionResult UpdateScrumTask(int id, UpdateScrumTaskDto updateScrumTaskDto)
        {
            var scrumTaskFromDb = _context.ScrumTasks
                .SingleOrDefault(s => s.Id == id);

            if (scrumTaskFromDb == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!string.IsNullOrWhiteSpace(updateScrumTaskDto.UserId))
            {
                var user = _context.Users
                    .SingleOrDefault(u => u.Id.Equals(updateScrumTaskDto.UserId));

                scrumTaskFromDb.UserId = user?.Id;
            }
            else if (updateScrumTaskDto.EstimationId != null)
            {
                var estimation = _context.Estimations
                    .SingleOrDefault(u => u.Id == updateScrumTaskDto.EstimationId.Value);

                scrumTaskFromDb.EstimationId = estimation?.Id;
            }
            else if (updateScrumTaskDto.Priority != null)
            {
                if (updateScrumTaskDto.Priority.Value == 0)
                    updateScrumTaskDto.Priority = null;

                scrumTaskFromDb.Priority = updateScrumTaskDto.Priority;
            }
            else if (updateScrumTaskDto.TaskType != null)
            {
                scrumTaskFromDb.TaskType = updateScrumTaskDto.TaskType.Value;
            }
            
            _context.SaveChanges();

            return Ok();
        }
    }
}
