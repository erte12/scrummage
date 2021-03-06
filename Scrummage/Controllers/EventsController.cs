﻿using System.Web.Mvc;
using AutoMapper;
using Scrummage.Controllers.MvcActionFilters;
using Scrummage.Core;
using Scrummage.Dtos;
using Scrummage.Presentation.ViewModels;

namespace Scrummage.Controllers
{
    public class EventsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [TeamReadAccessActionFilter]
        public ActionResult Index(int teamId)
        {
            var team = _unitOfWork.Teams.Get(teamId);

            var viewModel = new EventViewModel {Team = Mapper.Map<TeamDto>(team)};
            return View(viewModel);
        }
    }
}