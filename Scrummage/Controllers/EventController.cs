using System.Web.Mvc;
using AutoMapper;
using Scrummage.Core;
using Scrummage.Dtos;
using Scrummage.Models;
using Scrummage.Presentation.ViewModels;
using Scrummage.ViewModels;

namespace Scrummage.Controllers
{
    public class EventController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(int teamId)
        {
            var team = _unitOfWork.Teams.Get(teamId);
            if (team == null)
                return HttpNotFound();

            var viewModel = new EventViewModel {Team = Mapper.Map<TeamDto>(team)};

            return View(viewModel);
        }
    }
}