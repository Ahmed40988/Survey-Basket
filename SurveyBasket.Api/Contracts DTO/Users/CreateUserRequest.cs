namespace SurveyBasket.Api.Contracts.Users;

public record CreateUserRequest(
    string Fname,
    string Lname,
    string Email,
    string Password,
    IList<string> Roles
);