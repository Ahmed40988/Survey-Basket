namespace SurveyBasket.Api.ContractsDTO.Users
{
    public record UserProfileResponse
    (
        string Email,
        string UserName,
        string Fname,
        string Lname
        );
}
