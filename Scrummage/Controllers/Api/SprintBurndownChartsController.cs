using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Scrummage.Controllers.Api
{
    public class SprintBurndownChartsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Index()
        {
            var csvData = new StringBuilder("Day,Ideal,Left\r\n");

            csvData.Append("3/9/13,5691,4346\r\n");
            csvData.Append("4/9/13,7086,2300\r\n");
            csvData.Append("5/9/13,3575,8735\r\n");

            return Ok(csvData.ToString());
        }
    }
}
