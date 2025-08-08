using SurveyBasket.Api.Contracts.Roles;
using SurveyBasket.Api.ContractsDTO.Roles;

namespace SurveyBasket.Api.Services.Iservice
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponse>> GetAllAsync(bool includeDisabled = false, CancellationToken cancellationToken = default);
        Task<Result<RoleDetailResponse>> GetAsync(string Id, CancellationToken cancellationToken = default);
        Task<Result<RoleDetailResponse>> AddAsync(RoleRequest request, CancellationToken cancellationToken = default);
        Task<Result<RoleDetailResponse>> UpdateAsync(string Id, RoleRequest request, CancellationToken cancellationToken = default);
        Task<Result> ToggleStatusAsync(string Id, CancellationToken cancellationToken = default);

    }
}
