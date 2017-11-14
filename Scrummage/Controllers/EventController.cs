using System.Web.Mvc;
using Scrummage.Core;
using Scrummage.Models;
using Scrummage.ViewModels;

namespace Scrummage.Controllers
{
    public class EventController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}