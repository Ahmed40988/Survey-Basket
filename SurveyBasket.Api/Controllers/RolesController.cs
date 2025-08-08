
using SurveyBasket.Api.Authentication.Filters;
using SurveyBasket.Api.Contracts.Roles;

namespace SurveyBasket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRoleService roleService) : ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        [HttpGet("")]
        [HasPermission(Permissions.GetRoles)]
        public async Task<IActionResult> GetAll([FromQuery] bool includeDisabled, CancellationToken cancellationToken)
        {
            var result = await _roleService.GetAllAsync(includeDisabled, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        [HasPermission(Permissions.GetRoles)]
        public async Task<IActionResult> Get([FromRoute] string Id, CancellationToken cancellationToken)
        {
            var result = await _roleService.GetAsync(Id, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status404NotFound,
                title: result.Error.code,
                detail: result.Error.Descriptions);
        }

        [HttpPost("")]
        [HasPermission(Permissions.AddRoles)]
        public async Task<IActionResult> Add([FromBody] RoleRequest request, CancellationToken cancellationToken)
        {
            var result = await _roleService.AddAsync(request, cancellationToken);

            return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value)
            : Problem(
                statusCode: result.Error.StatusCode,
                title: result.Error.code,
                detail: result.Error.Descriptions
            );

        }
        [HttpPut("{id}")]
        [HasPermission(Permissions.UpdateRoles)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] RoleRequest request, CancellationToken cancellationToken)
        {
            var result = await _roleService.UpdateAsync(id, request, cancellationToken);

            return result.IsSuccess ? NoContent() : Problem(
                statusCode: result.Error.StatusCode,
                title: result.Error.code,
                detail: result.Error.Descriptions
                );
        }
        [HttpPut("{id}/toggle-status")]
        [HasPermission(Permissions.UpdateRoles)]
        public async Task<IActionResult> ToggleStatus([FromRoute] string id)
        {
            var result = await _roleService.ToggleStatusAsync(id);

            return result.IsSuccess ? NoContent() : Problem(
      statusCode: result.Error.StatusCode,
      title: result.Error.code,
      detail: result.Error.Descriptions);
        }

    }
}
