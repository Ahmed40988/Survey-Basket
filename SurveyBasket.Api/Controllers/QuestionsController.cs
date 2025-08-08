using SurveyBasket.Api.Contracts.Common;
using SurveyBasket.Api.ContractsDTO.Questions;
using SurveyBasket.Api.Error;

namespace SurveyBasket.Api.Controllers
{
    [Route("api/Polls/{PollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController(IQuestionService questionService) : ControllerBase
    {
        private readonly IQuestionService _questionService = questionService;

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromRoute] int PollId, [FromQuery] RequestFilters filters, CancellationToken cancellationToken)
        {
            var result = await _questionService.GetAllAsync(PollId, filters, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status404NotFound,
                 title: result.Error.code, detail: result.Error.Descriptions);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get([FromRoute] int PollId, [FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _questionService.GetAsync(PollId, id, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status404NotFound,
           title: result.Error.code, detail: result.Error.Descriptions);
        }



        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromRoute] int PollId, [FromBody] QuestionRequest request, CancellationToken cancellationToken)
        {
            var result = await _questionService.AddAsync(PollId, request, cancellationToken);

            return result.IsSuccess
            ? base.CreatedAtAction(nameof(Get), new { result.Value.Id, PollId }, result.Value)
            : result.Error.Equals(QuestionErrors.DuplicatedQuestionContent)
            ? base.Problem(statusCode: StatusCodes.Status409Conflict,
                title: result.Error.code, detail: result.Error.Descriptions)
                : base.Problem(statusCode: StatusCodes.Status400BadRequest,
                title: result.Error.code, detail: result.Error.Descriptions);


        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int PollId, [FromRoute] int id, [FromBody] QuestionRequest request, CancellationToken cancellationToken)
        {
            var result = await _questionService.UpdateAsync(PollId, id, request, cancellationToken);


            return result.IsSuccess
            ? base.NoContent()
            : result.Error.Equals(global::SurveyBasket.Api.Error.QuestionErrors.DuplicatedQuestionContent)
            ? base.Problem(statusCode: global::Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict,
                title: result.Error.code, detail: result.Error.Descriptions)
                : base.Problem(statusCode: global::Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest,
                title: result.Error.code, detail: result.Error.Descriptions);

        }

        [HttpPut("{id}/ToggleStatus")]
        public async Task<IActionResult> ToggleStatus([FromRoute] int PollId, [FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _questionService.ToggleStatusAsync(PollId, id, cancellationToken);


            return result.IsSuccess ? NoContent()
                    : Problem(statusCode: StatusCodes.Status400BadRequest,
                    title: result.Error.code,
                    detail: result.Error.Descriptions);

        }

    }
}
