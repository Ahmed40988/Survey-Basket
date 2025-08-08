using Hangfire;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using SurveyBasket.Api.Authentication;
using SurveyBasket.Api.Contracts.Authentication;
using SurveyBasket.Api.Error;
using SurveyBasket.Api.Helpers;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace SurveyBasket.Api.Services.Service
{
    public class AuthService(
        SignInManager<ApplicationUser> signInManager
        , UserManager<ApplicationUser> userManager
        , IjwtProvider ijwtProvider
        , ApplicationDbcontext context
        ,ILogger<AuthService> logger
        ,IEmailSender emailSender,
    IHttpContextAccessor httpContextAccessor) : IAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IjwtProvider _ijwtProvider = ijwtProvider;
        private readonly ApplicationDbcontext _context = context;
        private readonly ILogger<AuthService> _logger = logger;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly int _refreshTokenExpiryDays = 14;
        public async Task<Result<AuthResponse>?> GetTokenAsync(string Email, string Password, CancellationToken cancellationToken = default)
        {

            if (await _userManager.FindByEmailAsync(Email) is not { } user)
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

            if (user.IsDisabled)
                return Result.Failure<AuthResponse>(UserErrors.DisabledUser);

            var result = await _signInManager.PasswordSignInAsync(user, Password, false, true);

            if (result.Succeeded)
            {
                var (userRoles, userPermissions) = await GetUserRolesAndPermissions(user, cancellationToken);

                var (token, expiresIn) = _ijwtProvider.GenerateToken(user, userRoles, userPermissions);
                var refreshToken = GenerateRefreshToken();
                var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

                user.RefreshTokens.Add(new RefreshToken
                {
                    Token = refreshToken,
                    Expiereson = refreshTokenExpiration
                });

                await _userManager.UpdateAsync(user);

                var response = new AuthResponse(user.Id, user.Email, user.Fname, user.Lname, token, expiresIn, refreshToken, refreshTokenExpiration);

                return Result.Success(response);
            }

            var error = result.IsNotAllowed
                ? UserErrors.EmailNotConfirmed
                : result.IsLockedOut
                ? UserErrors.LockedUser
                : UserErrors.InvalidCredentials;

            return Result.Failure<AuthResponse>(error);
        }


        public async Task<Result<AuthResponse>?> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _ijwtProvider.ValidateToken(token);

            if (userId is null)
                return Result<AuthResponse>.Failure<AuthResponse>((UserErrors.InvalidJwtToken));

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result<AuthResponse>.Failure<AuthResponse>((UserErrors.InvalidCredentials));

            if (user.IsDisabled)
                return Result<AuthResponse>.Failure<AuthResponse>(UserErrors.DisabledUser);

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.isActive);

            if (userRefreshToken is null)
                return Result<AuthResponse>.Failure<AuthResponse>((UserErrors.InvalidRefreshToken));

            userRefreshToken.Revokedon = DateTime.UtcNow;
            var (UserRoles, UserPermissions) = await GetUserRolesAndPermissions(user, cancellationToken);
            var (newToken, expiresIn) = _ijwtProvider.GenerateToken(user, UserRoles, UserPermissions);
            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                Expiereson = refreshTokenExpiration
            });

            await _userManager.UpdateAsync(user);

            var response = new AuthResponse(user.Id, user.Email, user.Fname, user.Lname, newToken, expiresIn, newRefreshToken, refreshTokenExpiration);
            return Result<AuthResponse>.Success(response);
        }

        public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _ijwtProvider.ValidateToken(token);


            if (userId is null)
                return Result.Failure((UserErrors.InvalidJwtToken));

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure((UserErrors.InvalidCredentials));


            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.isActive);

            if (userRefreshToken is null)
                return Result.Failure((UserErrors.InvalidRefreshToken));

            userRefreshToken.Revokedon = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            return Result.Success();
        }


        public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var EmailExist = await _userManager.Users.AnyAsync(x => x.Email == request.Email);

            if (EmailExist)
                return Result<AuthResponse>.Failure<AuthResponse>(UserErrors.DuplicatedEmail);

            var user = request.Adapt<ApplicationUser>();

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var code =await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));


                await SendConfirmationEmail(user, code);


                return Result.Success();
            }

            var error = result.Errors.First();

            return Result.Failure<AuthResponse>(new Abstractions.Error(code: error.Code
                , Descriptions: error.Description
                , StatusCodes.Status400BadRequest));
        }


        public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            if (await _userManager.FindByIdAsync(request.UserId) is not { } user)
                return Result.Failure(UserErrors.UserNotFound);

            if(user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);
            var code = request.Code;
            try
            {
                code=Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            }
            catch(FormatException)
            {
                return Result.Failure(UserErrors.InvalidCode);

            }
            var result=await _userManager.ConfirmEmailAsync(user, code); 
            if(result.Succeeded)
            return Result.Success();

            var error = result.Errors.First();
            return Result.Failure(new Abstractions.Error(code:error.Code,Descriptions:error.Description,StatusCodes.Status400BadRequest));
        }

        public async Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Success();

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Confirmation code: {code}", code);

            await SendConfirmationEmail(user, code);

            return Result.Success();
        }

        public async Task<Result> SendResetPasswordCodeAsync(string email)
        {
            if (await _userManager.FindByEmailAsync(email) is not { } user)
                return Result.Success();

            if (!user.EmailConfirmed)
                return Result.Failure(UserErrors.EmailNotConfirmed with { StatusCode = StatusCodes.Status400BadRequest });

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Reset code: {code}", code);

            await SendResetPasswordEmail(user, code);

            return Result.Success();
        }

        public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || !user.EmailConfirmed)
                return Result.Failure(UserErrors.InvalidCode);

            IdentityResult result;

            try
            {
                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
                result = await _userManager.ResetPasswordAsync(user, code, request.NewPassword);
            }
            catch (FormatException)
            {
                result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());
            }

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();

            return Result.Failure(new Abstractions.Error (error.Code, error.Description, StatusCodes.Status401Unauthorized));
        }
        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        private async Task<(IEnumerable<string> roles, IEnumerable<string> permissions)> GetUserRolesAndPermissions(ApplicationUser user, CancellationToken cancellationToken)
        {
            var UserRoles = await _userManager.GetRolesAsync(user);
            #region use ExtintionMethod Join
            //var UserPermissions = await _context.Roles
            //    .Join(_context.RoleClaims,
            //    role => role.Id,
            //    claim => claim.RoleId,
            //    (role, claim) => new { role, claim })
            //    .Where(x => UserRoles.Contains(x.role.Name))
            //    .Select(x => x.claim.ClaimValue)
            //    .Distinct()
            //    .ToListAsync(cancellationToken);
            #endregion
            var UserPermissions = await (
                from r in _context.Roles
                join p in _context.RoleClaims
                on r.Id equals p.RoleId
                where UserRoles.Contains(r.Name!)
                select p.ClaimValue!)
                .Distinct()
                .ToListAsync();
            return (UserRoles, UserPermissions);
        }

        private async Task SendResetPasswordEmail(ApplicationUser user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody("ForgetPassword",
                templateModel: new Dictionary<string, string>
                {
                { "{{name}}", user.Fname },
                { "{{action_url}}", $"{origin}/auth/forgetPassword?email={user.Email}&code={code}" }
                }
            );

            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "✅ Survey Basket: Change Password", emailBody));

            await Task.CompletedTask;
        }

        private async Task SendConfirmationEmail(ApplicationUser user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
                templateModel: new Dictionary<string, string>
                {
                { "{{name}}", user.Fname },
                    { "{{action_url}}", $"{origin}/auth/emailConfirmation?userId={user.Id}&code={code}" }
                }
            );
            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "✅ Survey Basket: Email Confirmation", emailBody));

           await  Task.CompletedTask;    
        }
    }
}
