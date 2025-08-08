namespace SurveyBasket.Api.Controllers
{
    [Route("api/Polls/{PollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class ResultsController(IResultService resultService) : ControllerBase
    {
        private readonly IResultService _resultService = resultService;

        [HttpGet("row-data")]
        public async Task<IActionResult> PollVotes([FromRoute] int PollId, CancellationToken cancellationToken)
        {
            var result = await _resultService.GetPollVotesAsync(PollId, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status404NotFound,
                title: result.Error.code,
                detail: result.Error.Descriptions);
        }


        [HttpGet("Votes-Per-Day")]
        public async Task<IActionResult> VotesPerDay([FromRoute] int PollId, CancellationToken cancellationToken)
        {
            var result = await _resultService.GetPollVotesPerDayAsync(PollId, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status404NotFound,
                title: result.Error.code,
                detail: result.Error.Descriptions);
        }
        [HttpGet("votes-per-question")]
        public async Task<IActionResult> VotesPerQuestion([FromRoute] int pollId, CancellationToken cancellationToken)
        {
            var result = await _resultService.GetPollVotesPerQuestionAsync(pollId, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status404NotFound,
                title: result.Error.code,
                detail: result.Error.Descriptions);
        }
    }
}
