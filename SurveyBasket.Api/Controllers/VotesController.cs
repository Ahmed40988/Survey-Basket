
using Microsoft.AspNetCore.RateLimiting;
using SurveyBasket.Api.Contracts.Votes;

namespace SurveyBasket.Api.Controllers
{
    [Route("api/Polls/{PollId}/Vote")]
    [ApiController]
    [Authorize(Roles = DefaultRoles.Member)]
    [EnableRateLimiting(RateLimiters.Concurrency)]
    public class VotesController(IQuestionService questionService, IVoteService voteService) : ControllerBase
    {
        private readonly IQuestionService _questionService = questionService;
        private readonly IVoteService _voteService = voteService;

        [HttpGet("")]
        public async Task<IActionResult> StartVote(int PollId, CancellationToken cancellationToken)
        {
            var userid = User.GetUserId();

            var result = await _questionService.GetAvailableAsync(PollId, userid, cancellationToken);

            return result.IsSuccess
    ? Ok(result.Value)
    : result.Error.Equals(VoteErrors.DuplicatedVote)
    ? Problem(statusCode: StatusCodes.Status409Conflict,
        title: result.Error.code, detail: result.Error.Descriptions)
        : Problem(statusCode: StatusCodes.Status400BadRequest,
        title: result.Error.code, detail: result.Error.Descriptions);


        }

        [HttpPost("")]
        public async Task<IActionResult> Vote([FromRoute] int pollId, [FromBody] VoteRequest request, CancellationToken cancellationToken)
        {
            var result = await _voteService.AddAsync(pollId, User.GetUserId()!, request, cancellationToken);

            return result.IsSuccess ? Created() : Problem(statusCode: StatusCodes.Status400BadRequest,
        title: result.Error.code, detail: result.Error.Descriptions);
        }
    }
}
