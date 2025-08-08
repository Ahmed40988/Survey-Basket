using Microsoft.AspNetCore.Identity.UI.Services;
using SurveyBasket.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyBasket.Api.Services.Service
{
    public class NotificationService(
        UserManager<ApplicationUser> userManager
        ,ApplicationDbcontext context
        , IHttpContextAccessor httpContextAccessor
        ,IEmailSender emailSender) : INotificationService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ApplicationDbcontext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IEmailSender _emailSender = emailSender;

        public async Task SendNewPollsNotification(int? pollid = null)
        {
            IEnumerable<Poll> polls = [];

            if(pollid.HasValue)
            {
                var poll=await _context.polls.SingleOrDefaultAsync(x=>x.Id== pollid&&x.IsPubliched);
                polls = [poll!];

            }
            else
            {
                polls=await _context.polls.Where(x => x.IsPubliched && x.Startsat == DateOnly.FromDateTime(DateTime.UtcNow))
                .AsNoTracking()
                .ToListAsync();
            }
            //var members = await _userManager.GetUsersInRoleAsync(DefaultRoles.Member);
            var members = await _userManager.Users.ToListAsync();
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            foreach (var poll in polls)
            {
                foreach (var member in members)
                {
                    var placeholders = new Dictionary<string, string>
                    {
                        { "{{name}}", member.Fname },
                        { "{{pollTill}}", poll.Title },
                        { "{{endDate}}", poll.Endsat.ToString() },
                        { "{{url}}", $"{origin}/polls/start/{poll.Id}" }
                    };
                    var body = EmailBodyBuilder.GenerateEmailBody("PollNotification", placeholders);
                    await _emailSender.SendEmailAsync(member.Email!, $"📣 Survey Basket: New Poll - {poll.Title}", body);

                }
            }

        }
    }
}
