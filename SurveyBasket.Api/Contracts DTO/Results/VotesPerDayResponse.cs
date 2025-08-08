namespace SurveyBasket.Api.Contracts_DTO.Results
{
    public record VotesPerDayResponse(
     DateOnly Date,
     int NumberOfVotes
 );
}
