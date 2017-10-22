using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;
using System.Web.Mvc;
using System.Web.Routing;
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
            var team = _context.Teams.SingleOrDefault(t => t.Id == id);

            if (team == null)
                return HttpNotFound();

            return View(team);
        }
    }
}