
using Microsoft.AspNetCore.RateLimiting;
using SurveyBasket.Api.Contracts.Authentication;
using SurveyBasket.ContractsDTO.Authentication;

namespace SurveyBasket.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableRateLimiting(RateLimiters.IpLimiter)]
    public class AuthController(IAuthService authService, ILogger<AuthController> logger) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly ILogger<AuthController> _logger = logger;

        [HttpPost("")]
        public async Task<IActionResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Logging with email: {email} and password: {password}", request.Email, request.Password);
            var ResultRequest = await _authService
                .GetTokenAsync(request.Email, request.Password, cancellationToken);

            return ResultRequest!.IsSuccess ? Ok(ResultRequest.Value) : BadRequest(ResultRequest.Error);
        }


        [HttpPost("register")]
        [DisableRateLimiting]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            var ResultRequest = await _authService
                .RegisterAsync(request, cancellationToken);

            return ResultRequest!.IsSuccess ? Ok()
                : Problem(statusCode: StatusCodes.Status400BadRequest,
                title: ResultRequest.Error.code, detail: ResultRequest.Error.Descriptions);
        }


        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var ResultRequest = await _authService.ConfirmEmailAsync(request);

            return ResultRequest!.IsSuccess ? Ok()
               : Problem(statusCode: StatusCodes.Status400BadRequest,
               title: ResultRequest.Error.code, detail: ResultRequest.Error.Descriptions);
        }

        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailRequest request, CancellationToken cancellationToken)
        {
            var ResultRequest = await _authService.ResendConfirmationEmailAsync(request);

            return ResultRequest!.IsSuccess ? Ok()
               : Problem(statusCode: StatusCodes.Status400BadRequest,
               title: ResultRequest.Error.code, detail: ResultRequest.Error.Descriptions);
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request)
        {
            var ResultRequest = await _authService.SendResetPasswordCodeAsync(request.Email);
            return ResultRequest!.IsSuccess ? Ok()
                : Problem(statusCode: StatusCodes.Status400BadRequest,
                title: ResultRequest.Error.code, detail: ResultRequest.Error.Descriptions);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var ResultRequest = await _authService.ResetPasswordAsync(request);

            return ResultRequest!.IsSuccess ? Ok()
    : Problem(statusCode: StatusCodes.Status400BadRequest,
    title: ResultRequest.Error.code, detail: ResultRequest.Error.Descriptions);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var authResult = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

            return authResult!.IsSuccess ? Ok(authResult.Value) : BadRequest(authResult.Error);
        }

        [HttpPost("revoke-refresh-token")]
        public async Task<IActionResult> RevokeRefreshTokenAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

            return result.IsSuccess ? Ok() : Problem(statusCode: StatusCodes.Status400BadRequest, title: result.Error.code, detail: result.Error.Descriptions);
        }


    }
}
