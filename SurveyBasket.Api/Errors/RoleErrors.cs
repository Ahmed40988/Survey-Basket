
namespace SurveyBasket.Api.Errors;


public static class RoleErrors
{
    public static readonly Abstractions.Error RoleNotFound =
        new("Role.RoleNotFound", "Role is not found", StatusCodes.Status404NotFound);

    public static readonly Abstractions.Error InvalidPermissions =
        new("Role.InvalidPermissions", "Invalid permissions", StatusCodes.Status400BadRequest);

    public static readonly Abstractions.Error DuplicatedRole =
        new("Role.DuplicatedRole", "Another role with the same name is already exists", StatusCodes.Status409Conflict);
}