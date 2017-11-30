using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Scrummage.Core;
using Unity.Attributes;

namespace Scrummage.Controllers.MvcActionFilters
{
    public class SprintUpdateAccessActionFilter : ActionFilterAttribute
    {
        [Dependency]
        public IUnitOfWork UnitOfWork { private get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sprintId = filterContext.ActionParameters.ContainsKey("sprintId")
                ? (int)filterContext.ActionParameters["sprintId"]
                : (int)filterContext.ActionParameters["id"];

            var sprint = UnitOfWork.Sprints.GetWithTeamAndUsers(sprintId);

            if (sprint == null)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.NotFound);
                return;
            }

            var currentUserId = HttpContext.Current.User.Identity.GetUserId();

            if (!sprint.Team.ScrumMasterId.Equals(currentUserId))
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            base.OnActionExecuting(filterContext);
        }
    }
}