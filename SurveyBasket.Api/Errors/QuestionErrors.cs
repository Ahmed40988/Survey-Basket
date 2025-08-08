namespace SurveyBasket.Api.Error;

public static class QuestionErrors
{
    public static readonly Abstractions.Error QuestionNotFound =
        new("Question.NotFound", "No question was found with the given ID", StatusCodes.Status404NotFound);

    public static readonly Abstractions.Error DuplicatedQuestionContent =
        new("Question.DuplicatedContent", "Another question with the same content is already exists", StatusCodes.Status409Conflict);
}