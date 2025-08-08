using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyBasket.Api.Services.Iservice
{
    public interface INotificationService
    {
        Task SendNewPollsNotification(int? pollid = null);
    }
}
