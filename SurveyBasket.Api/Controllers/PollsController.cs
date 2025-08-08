using Microsoft.AspNetCore.RateLimiting;
using SurveyBasket.Api.Authentication.Filters;

namespace SurveyBasket.Api.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PollsController : ControllerBase
    {
        private readonly IPollservice _pollservice;
        public PollsController(IPollservice pollservice)
        {
            _pollservice = pollservice;
        }

        [HttpGet("")]
        [HasPermission(Permissions.GetPolls)]
        public async Task<IActionResult> Getall(CancellationToken cancellationToken)
        {
            var result = await _pollservice.GetAllAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet("GetCurrent")]
        [Authorize(Roles = DefaultRoles.Member)]
        [EnableRateLimiting(RateLimiters.UserLimiter)]
        public async Task<IActionResult> GetCurrent(CancellationToken cancellationToken)
        {
            var result = await _pollservice.GetAllAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [HasPermission(Permissions.GetPolls)]
        public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _pollservice.GetAsync(id, cancellationToken);

            return result.IsSuccess ? Ok(result.Value)
                : Problem(statusCode: StatusCodes.Status404NotFound,
                title: result.Error.code,
                detail: result.Error.Descriptions);
        }
        [HttpPost("")]
        [HasPermission(Permissions.AddPolls)]
        public async Task<IActionResult> Add([FromBody] PollRequest request, CancellationToken cancellationToken)
        {
            var result = await _pollservice.AddAsync(request, cancellationToken);
            return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value)
            : Problem(statusCode: StatusCodes.Status400BadRequest,
            title: result.Error.code,
            detail: result.Error.Descriptions);
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.UpdatePolls)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PollRequest request, CancellationToken cancellationToken)
        {
            var result = await _pollservice.updateAsync(id, request, cancellationToken);

            return result.IsSuccess ? NoContent()
                : Problem(statusCode: StatusCodes.Status400BadRequest,
                title: result.Error.code,
                detail: result.Error.Descriptions);
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.DeletePolls)]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _pollservice.deleteAsync(id);

            return result.IsSuccess ? NoContent()
        : Problem(statusCode: StatusCodes.Status400BadRequest,
        title: result.Error.code,
        detail: result.Error.Descriptions);

        }

        [HttpPut("{id}/TogglePublished")]
        [HasPermission(Permissions.UpdatePolls)]
        public async Task<IActionResult> TogglePublished([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _pollservice.TogglePublishedStatusAsync(id, cancellationToken);

            return result.IsSuccess ? NoContent()
                    : Problem(statusCode: StatusCodes.Status400BadRequest,
                    title: result.Error.code,
                    detail: result.Error.Descriptions);
        }

    }
}
