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
using Scrummage.Models;
using Scrummage.Persistance;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace Scrummage.Controllers.ApiActionFilters
{
    public class TeamAccessActionFilter : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamAccessActionFilter()
        {
            //TODO: Implement dependency injection
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var teamId = actionContext.ActionArguments.ContainsKey("teamId")
                ? (int)actionContext.ActionArguments["teamId"]
                : (int)actionContext.ActionArguments["id"];

            var team = _unitOfWork.Teams.GetWithMembersAndScrumMaster(teamId);

            if (team == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return;
            }

            var currentUserId = HttpContext.Current.User.Identity.GetUserId();

            if (!(team.Users.Select(u => u.Id).Contains(currentUserId) || team.ScrumMasterId.Equals(currentUserId)))
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);

            base.OnActionExecuting(actionContext);
        }
    }
}