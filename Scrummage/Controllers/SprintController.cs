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

        [HttpPost]

        public ActionResult Save(NewSprintViewModel sprint)
        {
            if (!ModelState.IsValid)
                return View("New", sprint);

            var newSprint = new Sprint
            {
                Id = sprint.Id,
                Name = sprint.Name,
                Description = sprint.Description,
                TeamId = sprint.TeamId,
                CreatedAt = DateTime.Now
            };

            _context.Sprints.Add(newSprint);
            _context.SaveChanges();

            return RedirectToAction("Manage", new {id = newSprint.Id});
        }

        public ActionResult New(int teamId)
        {
            var team = _context.Teams
                .SingleOrDefault(t => t.Id == teamId);

            if (team == null)
                return HttpNotFound();

            var viewModel = new NewSprintViewModel { TeamId = team.Id };

            return View(viewModel);
        }

        public ActionResult Manage(int id)
        {
            var sprint = _context.Sprints
                .SingleOrDefault(s => s.Id == id);

            var viewModel = new ManageSprintViewModel
            {
                Id = sprint.Id,
                Name = sprint.Name,
                Description = sprint.Description
            };

            return View(viewModel);
        }
    }
}