using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Scrummage.Core;
using Scrummage.Core.Services;
using Scrummage.Dtos;
using Scrummage.Models;
using Scrummage.Persistance;
using Scrummage.Presentation.ViewModels;
using Scrummage.Services.Validation;
using Scrummage.ViewModels;

namespace Scrummage.Controllers
{
    public class SprintController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISprintService _sprintService;

        public SprintController(IUnitOfWork unitOfWork, ISprintService sprintService)
        {
            _unitOfWork = unitOfWork;
            _sprintService = sprintService;
            _sprintService.Initialize(new ValidationDictionaryMvc(ModelState));
        }
        
        public ActionResult Index(int id = 0, int teamId = 24)
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
        public ActionResult Save(NewSprintViewModel newSprintViewModel)
        {
            var newSprint = Mapper.Map<Sprint>(newSprintViewModel);

            _sprintService.Create(newSprint);

            if (!ModelState.IsValid)
                return HttpNotFound();

            return RedirectToAction("Manage", new {id = newSprint.Id});
        }

        public ActionResult New(int teamId)
        {
            var team = _unitOfWork.Teams.Get(teamId);

            if (team == null)
                return HttpNotFound();

            var viewModel = new NewSprintViewModel { TeamId = team.Id };

            return View(viewModel);
        }

        public ActionResult Manage(int id)
        {
            var sprint = _unitOfWork.Sprints.GetWithTeamAndTasks(id);

            if (sprint == null)
                return HttpNotFound();

            var estimations = _unitOfWork.Estimations.GetAll();

            var viewModel = new ManageSprintViewModel
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

            var sprintViewModel = Mapper.Map<StatisticsSprintViewModel>(sprint);

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