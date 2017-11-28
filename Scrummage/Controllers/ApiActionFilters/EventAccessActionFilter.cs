using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.AspNet.Identity;
using Scrummage.Core;
using Scrummage.Models;
using Scrummage.Persistance;

namespace Scrummage.Controllers.ApiActionFilters
{
    public class EventAccessActionFilter : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventAccessActionFilter()
        {
            //TODO: Implement dependency injection
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var eventId = actionContext.ActionArguments.ContainsKey("eventId")
                ? (int)actionContext.ActionArguments["eventId"]
                : (int)actionContext.ActionArguments["id"];

            var eventFromDb = _unitOfWork.Events.GetWithTeam(eventId);

            if (eventFromDb?.Team == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return;
            }

            var currentUserId = HttpContext.Current.User.Identity.GetUserId();

            if (!(eventFromDb.Team.Users.Select(u => u.Id).Contains(currentUserId) 
               || eventFromDb.Team.ScrumMasterId.Equals(currentUserId)))
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);

            base.OnActionExecuting(actionContext);
        }
    }
}