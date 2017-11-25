using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Scrummage.Core;
using Scrummage.Persistance;
using Unity.Attributes;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace Scrummage.Controllers.ApiActionFilters
{
    public class SprintActionFilter : ActionFilterAttribute
    {
        [Dependency]
        public IUnitOfWork UnitOfWork { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var sprintId = actionContext.ActionArguments.ContainsKey("sprintId")
                ? (int)actionContext.ActionArguments["sprintId"]
                : (int)actionContext.ActionArguments["id"];

            var sprint = UnitOfWork.Sprints.GetWithTeamAndUsers(sprintId);

            if (sprint == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return;
            }

            var currentUserId = HttpContext.Current.User.Identity.GetUserId();

            if (!(sprint.Team.Users.Select(u => u.Id).Contains(currentUserId) || sprint.Team.ScrumMasterId.Equals(currentUserId)))
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);

            base.OnActionExecuting(actionContext);
        }
    }
}