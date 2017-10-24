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

            return Ok(newScrumTask.Content);
        }
    }
}
