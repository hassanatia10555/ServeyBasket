namespace SurveyBasket.Application.Errors;

public static class QuestionErrors
{
    public static readonly Error QuestionNotFound = new(
        "Question Not Found",
        "Question with the entered ID was not found.");

    public static readonly Error PollNotFound = new(
        "Poll Not Found",
        "The poll with the entered ID was not found.");

    public static readonly Error AnswerNotFound = new(
        "Answer Not Found",
        "An answer ID in the request does not belong to this question.");

    public static readonly Error AnswerInUse = new(
        "Answer In Use",
        "An answer with recorded votes cannot be removed.");

    public static readonly Error QuestionCreationFailed = new(
        "Question Creation Failed",
        "Failed to create the question with the entered details.");

    public static readonly Error DublicatedQuestion = new(
        "Dublicated Question",
        "Question with the entered content already exists in this poll.");
    public static readonly Error DeleteQuestionConistrants = new(
        "Delete Question Constraints",
        "Deleting the question violates constraints the most save solution is to deactivate it.");

}
