namespace SurveyBasket.Api.Contracts.Users;

public record UserResponse(
    string Id,
    string Fname,
    string Lname,
    string Email,
    bool IsDisabled,
    IEnumerable<string> Roles
);