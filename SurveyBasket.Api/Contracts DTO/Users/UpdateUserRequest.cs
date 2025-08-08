namespace SurveyBasket.Api.Contracts.Users;

public record UpdateUserRequest(
    string Fname,
    string Lname,
    string Email,
    IList<string> Roles
);