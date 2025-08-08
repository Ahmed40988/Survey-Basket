namespace SurveyBasket.Api.Error;

public static class UserErrors
{
    public static readonly Abstractions.Error InvalidCredentials =
        new("User.InvalidCredentials", "Invalid email/password", StatusCodes.Status401Unauthorized);
    public static readonly Abstractions.Error UserNotFound =
    new("User.NotFound", "No User was found with the given ID", StatusCodes.Status404NotFound);

    public static readonly Abstractions.Error DisabledUser =
        new("User.DisabledUser", "Disabled User ,Please Contact your Administrator ", StatusCodes.Status401Unauthorized);

    public static readonly Abstractions.Error LockedUser =
        new("User.LockedUser", "Locked User ,Please Contact your Administrator ", StatusCodes.Status401Unauthorized);

    public static readonly Abstractions.Error InvalidJwtToken =
        new("User.InvalidJwtToken", "Invalid Jwt token", StatusCodes.Status401Unauthorized);

    public static readonly Abstractions.Error InvalidRefreshToken =
        new("User.InvalidRefreshToken", "Invalid refresh token", StatusCodes.Status401Unauthorized);

    public static readonly Abstractions.Error DuplicatedEmail =
        new("User.DuplicatedEmail", "Another user with the same email is already exists", StatusCodes.Status409Conflict);

    public static readonly Abstractions.Error EmailNotConfirmed =
        new("User.EmailNotConfirmed", "Email is not confirmed", StatusCodes.Status401Unauthorized);

    public static readonly Abstractions.Error InvalidCode =
        new("User.InvalidCode", "Invalid code", StatusCodes.Status401Unauthorized);

    public static readonly Abstractions.Error DuplicatedConfirmation =
        new("User.DuplicatedConfirmation", "Email already confirmed", StatusCodes.Status400BadRequest);
    public static readonly Abstractions.Error InvalidRoles =
    new("User.InvalidRoles", "Invalid roles", StatusCodes.Status400BadRequest);

}