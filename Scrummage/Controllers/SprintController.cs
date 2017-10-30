﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Scrummage.Models;
using Scrummage.Persistance;
using Scrummage.ViewModels;

namespace Scrummage.Controllers
{
    public class SprintController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public SprintController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
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
    }
}