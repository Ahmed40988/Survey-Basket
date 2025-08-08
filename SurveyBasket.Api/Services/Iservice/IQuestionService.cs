using SurveyBasket.Api.Abstractions;
using SurveyBasket.Api.Contracts.Common;
using SurveyBasket.Api.ContractsDTO.Questions;

namespace SurveyBasket.Api.Services.Iservice
{
    public interface IQuestionService
    {
        Task<Result<PaginatedList<QuestionResponse>>> GetAllAsync(int PollId, RequestFilters filters, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<QuestionResponse>>> GetAvailableAsync(int PollId, string UserId, CancellationToken cancellationToken = default!);
        Task<Result<QuestionResponse>> GetAsync(int PollId, int id, CancellationToken cancellationToken = default!);
        Task<Result<QuestionResponse>> AddAsync(int PollId, QuestionRequest request, CancellationToken cancellationToken = default!);
        Task<Result> UpdateAsync(int pollid, int id, QuestionRequest request, CancellationToken cancellationToken = default);
        Task<Result> ToggleStatusAsync(int pollid, int id, CancellationToken cancellationToken = default);
    }
}
