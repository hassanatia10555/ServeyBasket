
namespace SurveyBasket.Application.Abstractions;

public static class QuestionErrors
{
    public static readonly Error QuestionNotFound = new(
        StatusCodes.Status404NotFound,
        "Question with the entered ID was not found.");

    public static readonly Error PollNotFound = new(
        StatusCodes.Status404NotFound,
        "The poll with the entered ID was not found.");

    public static readonly Error AnswerNotFound = new(
        StatusCodes.Status404NotFound,
        "An answer ID in the request does not belong to this question.");

    public static readonly Error AnswerInUse = new(
        StatusCodes.Status400BadRequest,
        "An answer with recorded votes cannot be removed.");

    public static readonly Error QuestionCreationFailed = new(
        StatusCodes.Status400BadRequest,
        "Failed to create the question with the entered details.");

    public static readonly Error DublicatedQuestion = new(
        StatusCodes.Status400BadRequest,
        "Question with the entered content already exists in this poll.");
    public static readonly Error DeleteQuestionConistrants = new(
        StatusCodes.Status400BadRequest,
        "Deleting the question violates constraints the most save solution is to deactivate it.");

}
