using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Scrummage.Core;
using Unity.Attributes;

namespace Scrummage.Controllers.MvcActionFilters
{
    public class TeamAccessActionFilter: ActionFilterAttribute
    {
        [Dependency]
        public IUnitOfWork UnitOfWork { private get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var teamId = filterContext.ActionParameters.ContainsKey("teamId")
                ? (int) filterContext.ActionParameters["teamId"]
                : (int) filterContext.ActionParameters["id"];

            var team = UnitOfWork.Teams.GetWithMembersAndScrumMaster(teamId);

            if (team == null)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.NotFound);
                return;
            }

            var currentUserId = HttpContext.Current.User.Identity.GetUserId();

            if (!(team.Users.Select(u => u.Id).Contains(currentUserId) || team.ScrumMasterId.Equals(currentUserId)))
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            base.OnActionExecuting(filterContext);
        }
    }
}