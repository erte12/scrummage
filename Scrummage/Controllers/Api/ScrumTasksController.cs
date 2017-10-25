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
        public IHttpActionResult CreateScrumTask(ScrumTaskDto scrumTaskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newScrumTask = Mapper.Map<ScrumTask>(scrumTaskDto);

            _context.ScrumTasks.Add(newScrumTask);
            _context.SaveChanges();

            return Ok(new {id = newScrumTask.Id, content = newScrumTask.Content});
        }

        [HttpPatch]
        public IHttpActionResult UpdateScrumTask(int id, ScrumTaskDto scrumTaskDto)
        {
            var scrumTaskFromDb = _context.ScrumTasks
                .SingleOrDefault(s => s.Id == id);

            if (scrumTaskFromDb == null)
                return NotFound();

            if (!string.IsNullOrWhiteSpace(scrumTaskDto.UserId))
            {
                var user = _context.Users.SingleOrDefault(u => u.Id.Equals(scrumTaskDto.UserId));
                scrumTaskFromDb.UserId = user?.Id;
            }
            
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult ChangeScrumTaskType(int id, ScrumTaskDto scrumTaskDto)
        {
            var scrumTaskFromDb = _context.ScrumTasks
                .SingleOrDefault(s => s.Id == id);

            if (scrumTaskFromDb == null)
                return NotFound();

            scrumTaskFromDb.TaskType = scrumTaskDto.TaskType;
            _context.SaveChanges();

            return Ok();
        }
    }
}
