using System;
using System.Collections.Generic;
using System.Text;

namespace SurveyBasket.Application.Errors
{
    public static class PollErrors
    {
        public static readonly Error PollNotFound
            = new Error("Poll Not Found",
                   "Poll With entered Id Not Found !..");

        public static readonly Error PollAlreadyExists
            = new Error("Poll Already Exists",
                   "Poll With entered Title Already Exists !..");
    }
}
