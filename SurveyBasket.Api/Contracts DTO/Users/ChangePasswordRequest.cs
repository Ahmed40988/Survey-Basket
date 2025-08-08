namespace SurveyBasket.Api.ContractsDTO.Users
{
    public record ChangePasswordRequest(
        string CurrentPassword,
        string NewPassword

        );
}
