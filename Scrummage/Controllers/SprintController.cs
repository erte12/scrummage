using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Scrummage.Models;
using Scrummage.ViewModels;

namespace Scrummage.Controllers
{
    public class SprintController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SprintController()
        {
            _context = new ApplicationDbContext();
        }

        [Route("sprint/{sprintId}")]
        public ActionResult Index(int sprintId)
        {
            return View();
        }

        [Route("sprint/new/{teamId}")]
        public ActionResult New(int teamId)
        {
            var team = _context.Teams
                .SingleOrDefault(t => t.Id == teamId);

            if (team == null)
                return HttpNotFound();

            var viewModel = new SprintViewModel { TeamId = team.Id };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(SprintViewModel sprint)
        {
            if (!ModelState.IsValid)
                return View("New", sprint);

            var newSprint = new Sprint
            {
                Name = sprint.Name,
                Description = sprint.Description,
                TeamId = sprint.TeamId,
                CreatedAt = DateTime.Now
            };

            _context.Sprints.Add(newSprint);
            _context.SaveChanges();

            return RedirectToAction("Index", "Board",
                new {teamId = newSprint.TeamId, sprintId = newSprint.Id});
        }
    }
}