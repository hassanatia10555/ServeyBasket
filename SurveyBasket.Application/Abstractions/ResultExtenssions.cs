using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SurveyBasket.Application.Abstractions
{
    public static class ResultExtenssions
    {
        public static ObjectResult ToProblem(this Result result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("Cannot convert a successful result to a problem.");

            IResult problem = Results.Problem(statusCode : result.Error.Code);

            var problemDetails = 
                problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;

            problemDetails!.Extensions= new Dictionary<string, object?> 
            { 
                {
                    "Errors", new[] { result.Error } 
                } 
            };

            return new ObjectResult(problemDetails);
        }
    }
}
