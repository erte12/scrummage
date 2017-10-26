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
            {
                sprint = _context.Sprints
                    .OrderByDescending(s => s.CreatedAt)
                    .FirstOrDefault(s => s.TeamId == teamId);

                return RedirectToAction("Index", new { sprintId = sprint?.Id});
            }

            sprint = _context.Sprints
                .Include(s => s.Team)
                .Include(s => s.Team.Sprints)
                .Include(s => s.Team.Users)
                .Include(s => s.Team.Users.Select(u => u.ScrumTasks))
                .SingleOrDefault(s => s.Id == sprintId);

            return View(sprint);
        }
    }
}