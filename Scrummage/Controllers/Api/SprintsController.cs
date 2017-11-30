using System.Web.Http;
using Scrummage.Controllers.ApiActionFilters;
using Scrummage.Core;
using Scrummage.Core.Services;
using Scrummage.Dtos;
using Scrummage.Services.Validation;

namespace Scrummage.Controllers.Api
{
    [Authorize(Roles = RoleName.ScrumMaster)]
    public class SprintsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISprintService _sprintService;

        public SprintsController(IUnitOfWork unitOfWork, ISprintService sprintService)
        {
            _unitOfWork = unitOfWork;
            _sprintService = sprintService;
            _sprintService.Initialize(new ValidationDictionaryWebApi(ModelState));
        }

        [HttpPatch]
        [SprintUpdateAccessActionFilter]
        public IHttpActionResult UpdateSprint(int id, SprintDto sprintDto)
        {
            var sprint = _sprintService.Update(id, sprintDto);

            if (sprint == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }
    }
}
