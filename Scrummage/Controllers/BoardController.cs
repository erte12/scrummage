using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Scrummage.Models;
using System.Data.Entity;
using Newtonsoft.Json;
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
                .Include(s => s.Tasks.Select(t => t.User))
                .SingleOrDefault(s => s.Id == sprintId);

            if (sprint == null)
                return HttpNotFound();

            var viewModel = new BoardViewModel
            {
                Sprint = sprint,
                Team = sprint.Team,
                TeamSprints = sprint.Team.Sprints.OrderByDescending(s => s.CreatedAt),
                UsersWithTasks = sprint.Tasks
                    .Where(t => t.User != null)
                    .Where(t => t.IsActive)
                    .GroupBy(t => t.User)
                    .ToDictionary(g => g.Key, g => g.ToList())
            };

            return View(viewModel);
        }
    }
}