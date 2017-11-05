﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Scrummage.Core;
using Scrummage.Models;
using Scrummage.Persistance;
using Scrummage.ViewModels;

namespace Scrummage.Controllers
{
    public class SprintController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SprintController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //teamId temporarly hardcoded
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

            sprint = _unitOfWork.Sprints.GetWithTeamAndActiveTasks(id);

            if (sprint == null)
                return HttpNotFound();

            var estimations = _unitOfWork.Estimations.GetAll();

            var viewModel = new SprintBoardViewModel
            {
                SprintId = sprint.Id,
                Team = sprint.Team,
                Users = sprint.Team.Users,
                Estimations = estimations
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(NewSprintViewModel sprint)
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

            _unitOfWork.Sprints.Add(newSprint);
            _unitOfWork.Complate();

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
                Estimations = estimations
            };

            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                _unitOfWork.Dispose();

            base.Dispose(disposing);
        }
    }
}