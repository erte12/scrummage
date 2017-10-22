using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using Scrummage.Models;

namespace Scrummage.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }
   
        [Route("teams/{id:regex(\\d)}")]
        public ActionResult Details(int id)
        {
            var team = _context.Teams
                .Include(t => t.Users)
                .SingleOrDefault(t => t.Id == id);

            if (team == null)
                return HttpNotFound();

            return View(team);
        }
    }
}