using System.Web.Mvc;
using Scrummage.Controllers.MvcActionFilters;
using Scrummage.Core;

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
        [TeamAccessActionFilter]
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