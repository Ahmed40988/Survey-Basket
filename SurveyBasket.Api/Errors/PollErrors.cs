namespace SurveyBasket.Api.Error;

public static class PollErrors
{
    public static readonly Abstractions.Error PollNotFound =
        new("Poll.NotFound", "No poll was found with the given ID", StatusCodes.Status404NotFound);

    public static readonly Abstractions.Error DuplicatedPollTitle =
        new("Poll.DuplicatedTitle", "Another poll with the same title is already exists", StatusCodes.Status409Conflict);

}