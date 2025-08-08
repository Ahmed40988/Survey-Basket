using SurveyBasket.Api.Abstractions;

public static class VoteErrors
{
    public static readonly Error InvalidQuestions =
        new("Vote.InvalidQuestions", "Invalid questions, and some questions is not Voted in the same Poll ", StatusCodes.Status400BadRequest);

    public static readonly Error DuplicatedVote =
        new("Vote.DuplicatedVote", "This user already voted before for this poll", StatusCodes.Status409Conflict);
}