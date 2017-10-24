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
        public ActionResult Index(int teamId = 24, int sprintId = 0)
        {
            Sprint sprint;

            if (sprintId == 0)
                sprint = _context.Sprints
                    .OrderByDescending(s => s.CreatedAt)
                    .FirstOrDefault(s => s.TeamId == teamId);
            else
            {
                sprint = _context.Sprints.SingleOrDefault(s => s.Id == sprintId);
                if (sprint == null)
                    return HttpNotFound();
            }

            var teamQuery = _context.Teams
                .Include(t => t.Sprints);

            if (sprint != null)
                teamQuery = teamQuery
                    .Include(t => t.Users
                        .Select(u => u.ScrumTasks));

            var team = teamQuery.SingleOrDefault(t => t.Id == teamId);

            if (team == null)
                return HttpNotFound();

            var viewModel = new BoardViewModel
            {
                Sprint = sprint,
                Team = team
            };

            return View(viewModel);
        }
    }
}