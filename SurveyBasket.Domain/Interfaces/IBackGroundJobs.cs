using System;
using System.Collections.Generic;
using System.Text;

namespace SurveyBasket.Domain.Interfaces
{
    public interface IBackGroundJobs<T> where T : class
    {
        Task EnqueJob();
        Task RecuiringJob();
    }
}
