using System;
using System.Collections.Generic;
using System.Text;

namespace SurveyBasket.Application.Errors
{
    public static class PollErrors
    {
        public static readonly Error PollNotFound
                 = new Error("Poll Not Found", "Poll With entered Id Not Found !..");
    }
}
