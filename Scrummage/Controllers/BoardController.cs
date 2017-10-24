using System.Linq;
using System.Web.Mvc;
using Scrummage.Models;
using System.Data.Entity;

namespace Scrummage.Controllers
{
    public class BoardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoardController()
        {
            _context = new ApplicationDbContext();
        }

        //teamId temporarly hardcoded
        [Route("board/{teamId?}/{sprintId?}")]
        public ActionResult Index(int teamId = 24, int sprintId = 0)
        {
            var team = _context.Teams
                .Include(t => t.Sprints)
                .SingleOrDefault(t => t.Id == teamId);

            return View(team);
        }
    }
}
