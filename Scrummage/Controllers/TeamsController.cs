using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using AutoMapper;
using Scrummage.Core;
using Scrummage.Dtos;
using Scrummage.Models;
using Scrummage.Persistance;
using Scrummage.Presentation.ViewModels;

namespace Scrummage.Controllers
{
    [Authorize(Roles = RoleName.ScrumMaster)]
    public class TeamsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }
   
        [Route("Teams/{id:regex(\\d)}")]
        public ActionResult Details(int id)
        {
            var team = _unitOfWork.Teams.GetWithMembers(id);

            if (team == null)
                return HttpNotFound();

            return View(team);
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                _unitOfWork.Dispose();

            base.Dispose(disposing);
        }
    }
}