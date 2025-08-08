using SurveyBasket.Api.Contracts.Users;
using SurveyBasket.Api.ContractsDTO.Users;

namespace SurveyBasket.Api.Services.Iservice
{
    public interface IUserService
    {
        Task<Result<UserProfileResponse>> GetProfileAsync(string Id);
        Task<Result> UpdateProfileAsync(string Id, UpdatedProfileRequest request);
        Task<Result> ChangePasswordAsync(string Id, ChangePasswordRequest request);
        Task<IEnumerable<UserResponse>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Result<UserResponse>> GetAsync(string Id, CancellationToken cancellationToken = default);
        Task<Result<UserResponse>> AddAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(string Id, UpdateUserRequest request, CancellationToken cancellationToken = default);
        Task<Result> ToggleStatus(string Id, CancellationToken cancellationToken = default);
        Task<Result> Unlock(string id, CancellationToken cancellationToken = default);




    }
}
