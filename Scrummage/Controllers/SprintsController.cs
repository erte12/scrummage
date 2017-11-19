using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;
using AutoMapper;
using Scrummage.Core;
using Scrummage.Core.Services;
using Scrummage.Dtos;
using Scrummage.Models;
using Scrummage.Presentation.ViewModels;
using Scrummage.Services.Validation;
using Scrummage.ViewModels;

namespace Scrummage.Controllers
{
    public class SprintsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISprintService _sprintService;

        public SprintsController(IUnitOfWork unitOfWork, ISprintService sprintService)
        {
            _unitOfWork = unitOfWork;
            _sprintService = sprintService;
            _sprintService.Initialize(new ValidationDictionaryMvc(ModelState));
        }

        [Route("Sprints/{id:regex(\\d)}")]
        public ActionResult Index(int id = 0, int teamId = 0)
        {
            Sprint sprint;

            if (id == 0)
            {
                sprint = _unitOfWork.Sprints.GetNewestForTeam(teamId);

                return sprint == null 
                    ? RedirectToAction("New", new { teamId }) 
                    : RedirectToAction("Index", new { sprint.Id });
            }

            sprint = _unitOfWork.Sprints.GetWithTeamAndUsers(id);

            if (sprint == null)
                return HttpNotFound();

            var estimations = _unitOfWork.Estimations.GetAll();
            var teamSprints = _unitOfWork.Sprints.GetByTeamId(sprint.TeamId);

            var viewModel = new SprintBoardViewModel
            {
                Id = sprint.Id,
                Team = sprint.Team,
                Users = sprint.Team.Users,
                TeamSprints = teamSprints,
                Estimations = estimations
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(SprintNewViewModel sprintNewViewModel)
        {
            var newSprint = Mapper.Map<Sprint>(sprintNewViewModel);
            _sprintService.Create(newSprint);

            if (!ModelState.IsValid)
                return View("New", sprintNewViewModel);

            return RedirectToAction("Manage", new {id = newSprint.Id});
        }

        public ActionResult New(int teamId)
        {
            var team = _unitOfWork.Teams.GetWithSprints(teamId);

            if (team == null)
                return HttpNotFound();
            if (!team.HasAnySprint)
                ViewBag.TeamHasNoSprints = true;

            var viewModel = new SprintNewViewModel { Team = Mapper.Map<TeamDto>(team) };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var sprint = _unitOfWork.Sprints.Get(id);

            if (sprint == null)
                return HttpNotFound();

            _unitOfWork.Sprints.Remove(sprint);
            _unitOfWork.Complate();

            return RedirectToAction("Index", new {id = 0, teamId = sprint.TeamId});
        }

        public ActionResult Manage(int id)
        {
            var sprint = _unitOfWork.Sprints.GetWithTeamAndTasks(id);

            if (sprint == null)
                return HttpNotFound();

            var estimations = _unitOfWork.Estimations.GetAll();

            var viewModel = new SprintManageViewModel
            {
                Id = sprint.Id,
                Name = sprint.Name,
                Description = sprint.Description,
                Users = sprint.Team.Users,
                Tasks = sprint.Tasks,
                Estimations = estimations,
                Team = sprint.Team,
                StartsAt = sprint.StartsAt.Date,
                EndsAt = sprint.EndsAt.Date
            };

            return View(viewModel);
        }

        public ActionResult Statistics(int id)
        {
            var sprint = _unitOfWork.Sprints.GetWithTeam(id);

            if (sprint == null)
                return null;

            var sprintViewModel = Mapper.Map<SprintStatisticsViewModel>(sprint);

            return View(sprintViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                _unitOfWork.Dispose();

            base.Dispose(disposing);
        }
    }
}