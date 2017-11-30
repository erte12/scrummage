using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using Microsoft.AspNet.Identity;
using Scrummage.Core;
using Scrummage.Models;
using Scrummage.Persistance;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace Scrummage.Controllers.ApiActionFilters
{
    public class ScrumTaskUpdateAccessActionFilter : ActionFilterAttribute
    {
        //        [Dependency]
        //        public IUnitOfWork UnitOfWork { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public ScrumTaskUpdateAccessActionFilter()
        {
            //TODO: Implement dependency injection
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var taskId = actionContext.ActionArguments.ContainsKey("taskId")
                ? (int)actionContext.ActionArguments["taskId"]
                : (int)actionContext.ActionArguments["id"];

            var scrumTask = _unitOfWork.ScrumTasks.GetWithScrumMaster(taskId);

            if (scrumTask == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return;
            }

            var currentUserId = HttpContext.Current.User.Identity.GetUserId();

            if (!scrumTask.Sprint.Team.ScrumMaster.Id.Equals(currentUserId))
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);

            base.OnActionExecuting(actionContext);
        }
    }
}