
using SurveyBasket.Api.ContractsDTO.Users;

namespace SurveyBasket.Api.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("")]
        public async Task<IActionResult> Info()
        {
            var result = await _userService.GetProfileAsync(User.GetUserId());
            return result.IsSuccess ? Ok(result.Value) : BadRequest();
        }

        [HttpPut("")]
        public async Task<IActionResult> Info([FromBody] UpdatedProfileRequest request)
        {
            await _userService.UpdateProfileAsync(User.GetUserId(), request);
            return NoContent();
        }
        [HttpPut("Change_password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var result = await _userService.ChangePasswordAsync(User.GetUserId(), request);
            return result!.IsSuccess ? Ok() : BadRequest(result.Error);
        }

    }
}
