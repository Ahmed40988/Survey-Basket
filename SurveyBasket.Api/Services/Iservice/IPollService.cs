namespace SurveyBasket.Api.Services.Iservice
{
    public interface IPollservice
    {
        Task<IEnumerable<PollResponse>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<PollResponse>> GetCurrentAsync(CancellationToken cancellationToken = default);
        Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<PollResponse>> AddAsync(PollRequest request, CancellationToken cancellationToken = default);
        Task<Result> updateAsync(int id, PollRequest request, CancellationToken cancellationToken = default);
        Task<Result> deleteAsync(int id, CancellationToken cancellationToken = default);
        Task<Result> TogglePublishedStatusAsync(int id, CancellationToken cancellationToken = default);

    }
}
