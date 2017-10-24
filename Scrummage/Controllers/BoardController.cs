using System.Linq;
using System.Web.Mvc;
using Scrummage.Models;
using System.Data.Entity;
using Scrummage.ViewModels;

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

            if (team == null)
                return HttpNotFound();

            var sprint = new Sprint();

            sprint = sprintId == 0 
                ? team.Sprints.OrderByDescending(s => s.CreatedAt).FirstOrDefault() 
                : team.Sprints.SingleOrDefault(s => s.Id == sprintId);

            var viewModel = new BoardViewModel
            {
                Sprint = sprint,
                Team = team
            };

            return View(viewModel);
        }
    }
}
