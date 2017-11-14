using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using Scrummage.Core;
using Scrummage.Models;
using Scrummage.Persistance;

namespace Scrummage.Controllers
{
    public class TeamController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }
   
        [Route("teams/{id:regex(\\d)}")]
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