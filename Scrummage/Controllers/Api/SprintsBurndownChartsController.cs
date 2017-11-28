using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using Scrummage.Controllers.ApiActionFilters;
using Scrummage.Core;
using Scrummage.Models;

namespace Scrummage.Controllers.Api
{
    public class SprintsBurndownChartsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public SprintsBurndownChartsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [SprintAccessActionFilter]
        public IHttpActionResult Index(int id)
        {
            var sprint = _unitOfWork.Sprints.GetWithActiveTasks(id);

            if (sprint == null)
                return NotFound();

            var dates = GetDates(sprint.StartsAt, sprint.EndsAt);

            var tasksGroupedByDates = sprint.Tasks
                .Where(t => t.DoneAt != null)
                .GroupBy(t => t.DoneAt)
                .ToDictionary(g => g.Key, g => g.ToList());

            var csvData = GetCsv(sprint, dates, tasksGroupedByDates);

            return Ok(csvData);
        }

        private static int GetIdealBurndown(int estimationsSum, ICollection<DateTime> dates)
        {
            var daysNumber = dates.Count - 1;

            return (int)Math.Ceiling(1.0 * estimationsSum / daysNumber);
        }

        private static IList<DateTime> GetDates(DateTime startAt, DateTime endsAt)
        {
            var dates = new List<DateTime>();

            for (var dt = startAt.Date; dt <= endsAt.Date; dt = dt.AddDays(1))
                dates.Add(dt);

            return dates;
        }

        private static string GetCsv(Sprint sprint, IList<DateTime> dates, Dictionary<DateTime?, List<ScrumTask>> tasks)
        {
            var estimationsSum = sprint.Tasks.Sum(t => t.Estimation.Value);
            var idealBurndown = GetIdealBurndown(estimationsSum, dates);
            var doneSum = 0;

            var csvData = new StringBuilder("Day,Ideal,Left\r\n");
            for (var i = 0; i < dates.Count; i++)
            {
                if (tasks.ContainsKey(dates[i]) && tasks[dates[i]] != null)
                    doneSum += tasks[dates[i]].Sum(t => t.Estimation.Value);

                csvData.Append(dates[i]);
                csvData.Append(",");
                csvData.Append(Math.Max(0, estimationsSum - i * idealBurndown));
                csvData.Append(",");
                csvData.Append(estimationsSum - doneSum);
                if (i != dates.Count - 1)
                    csvData.Append("\r\n");
            }
            
            return csvData.ToString();
        }
    }
}
