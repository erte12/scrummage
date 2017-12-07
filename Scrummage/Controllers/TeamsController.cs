using System.Web.Mvc;
using Scrummage.Controllers.MvcActionFilters;
using Scrummage.Core;

namespace Scrummage.Controllers
{
    public class TeamsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = RoleName.ScrumMaster)]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Teams/{id:regex(\\d)}")]
        [Authorize(Roles = RoleName.ScrumMaster)]
        [TeamUpdateAccessActionFilter]
        public ActionResult Details(int id)
        {
            var team = _unitOfWork.Teams.GetWithMembersAndScrumMaster(id);
            return View(team);
        }

        public ActionResult Join()
        {
            return View();
        }
    }
}