using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Scrummage.Models;
using System.Data.Entity;
using Newtonsoft.Json;
using Scrummage.Persistance;
using Scrummage.ViewModels;

namespace Scrummage.Controllers
{
    public class BoardController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public BoardController()
        {;
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        //teamId temporarly hardcoded
        public ActionResult Index(int teamId = 24, int sprintId = 0)
        {
            Sprint sprint;

            if (sprintId == 0)
            {
                sprint = _unitOfWork.Sprints.GetNewestForTeam(teamId);
                return RedirectToAction("Index", new { sprintId = sprint?.Id});
            }

            sprint = _unitOfWork.Sprints.GetWithTeamAndTasks(sprintId);

            if (sprint == null)
                return HttpNotFound();

            var viewModel = new BoardViewModel
            {
                Sprint = sprint,
                Team = sprint.Team,
                Users = sprint.Team.Users
            };

            return View(viewModel);
        }
    }
}