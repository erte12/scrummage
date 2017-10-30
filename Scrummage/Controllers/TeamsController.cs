using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using Scrummage.Models;
using Scrummage.Persistance;

namespace Scrummage.Controllers
{
    public class TeamsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public TeamsController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        public ActionResult Index()
        {
            return View();
        }
   
        [Route("teams/{id:regex(\\d)}")]
        public ActionResult Details(int id)
        {
            var team = _unitOfWork.Teams.GetTeamWithMembers(id);

            if (team == null)
                return HttpNotFound();

            return View(team);
        }
    }
}