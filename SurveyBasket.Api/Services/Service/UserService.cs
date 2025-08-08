using SurveyBasket.Api.Contracts.Users;
using SurveyBasket.Api.ContractsDTO.Users;
using SurveyBasket.Api.Error;
using System.Data;

namespace SurveyBasket.Api.Services.Service
{
    public class UserService(UserManager<ApplicationUser> userManager
        , ApplicationDbcontext context
        , IRoleService roleService) : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ApplicationDbcontext _context = context;
        private readonly IRoleService _roleService = roleService;

        public async Task<IEnumerable<UserResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {

            var users = await (
                               from u in _context.Users
                               join ur in _context.UserRoles
                               on u.Id equals ur.UserId
                               join r in _context.Roles
                               on ur.RoleId equals r.Id into roles
                               where !roles.Any(x => x.Name == DefaultRoles.Member)
                               select new
                               {
                                   u.Id,
                                   u.Fname,
                                   u.Lname,
                                   u.Email,
                                   u.IsDisabled,
                                   Roles = roles.Select(x => x.Name).ToList()
                               })
                               .GroupBy(u => new { u.Id, u.Fname, u.Lname, u.Email, u.IsDisabled })
                               .Select(x => new UserResponse(
                                   x.Key.Id,
                                   x.Key.Fname,
                                   x.Key.Lname,
                                   x.Key.Email,
                                   x.Key.IsDisabled,
                                   x.SelectMany(x => x.Roles)
                                   )).ToListAsync(cancellationToken);
            return users;

        }
        public async Task<Result<UserResponse>> GetAsync(string Id, CancellationToken cancellationToken = default)
        {
            #region  First Way
            //    var user = await (
            //    from u in _context.Users
            //    where u.Id == Id
            //    join ur in _context.UserRoles on u.Id equals ur.UserId
            //    join r in _context.Roles on ur.RoleId equals r.Id into roles
            //    select new
            //    {
            //        u.Id,
            //        u.Fname,
            //        u.Lname,
            //        u.Email,
            //        u.IsDisabled,
            //        Roles = roles
            //            .Where(x => x.Name != DefaultRoles.Member)
            //            .Select(x => x.Name!)
            //            .ToList()
            //    }
            //)
            //.GroupBy(u => new { u.Id, u.Fname, u.Lname, u.Email, u.IsDisabled })
            //.Select(g => new UserResponse(
            //    g.Key.Id,
            //    g.Key.Fname,
            //    g.Key.Lname,
            //    g.Key.Email,
            //    g.Key.IsDisabled,
            //    g.SelectMany(x => x.Roles).Distinct()
            //))
            //.FirstOrDefaultAsync(cancellationToken);
            //            return Result.Success(user!);
            #endregion

            #region 2nd way
            //if (await _userManager.FindByIdAsync(Id) is not { } user)
            //    return Result.Failure<UserResponse>(UserErrors.UserNotFound);
            //var roles=await _userManager.GetRolesAsync(user);
            //var response = new UserResponse
            //    (
            //    user.Id,
            //    user.Fname,
            //    user.Lname,
            //    user.Email,
            //    user.IsDisabled,
            //    roles
            //    );
            //return Result.Success(response);
            #endregion

            #region 3nd using Mapster
            if (await _userManager.FindByIdAsync(Id) is not { } user)
                return Result.Failure<UserResponse>(UserErrors.UserNotFound);
            var roles = await _userManager.GetRolesAsync(user);
            var response = (user, roles).Adapt<UserResponse>();
            return Result.Success(response);
            #endregion

        }
        public async Task<Result<UserResponse>> AddAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
        {
            if (await _context.Users.AnyAsync(x => x.Email == request.Email))
                return Result.Failure<UserResponse>(UserErrors.DuplicatedEmail);

            var allowedRoles = await _roleService.GetAllAsync(cancellationToken: cancellationToken);

            if (request.Roles.Except(allowedRoles.Select(r => r.Name)).Any())
                return Result.Failure<UserResponse>(UserErrors.InvalidRoles);

            var user = request.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, request.Roles);

                var response = (user, request.Roles).Adapt<UserResponse>();
                return Result.Success(response);
            }
            var error = result.Errors.First();

            return Result.Failure<UserResponse>(new Abstractions.Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        public async Task<Result> UpdateAsync(string Id, UpdateUserRequest request, CancellationToken cancellationToken = default)
        {
            if (await _context.Users.AnyAsync(x => x.Email == request.Email && x.Id != Id))
                return Result.Failure(UserErrors.DuplicatedEmail);

            var allowedRoles = await _roleService.GetAllAsync(cancellationToken: cancellationToken);

            if (request.Roles.Except(allowedRoles.Select(r => r.Name)).Any())
                return Result.Failure(UserErrors.InvalidRoles);

            if (await _userManager.FindByIdAsync(Id) is not { } user)
                return Result.Failure(UserErrors.UserNotFound);
            user = request.Adapt(user);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                await _context.UserRoles.Where(r => r.UserId == Id).ExecuteDeleteAsync(cancellationToken);
                await _userManager.AddToRolesAsync(user, request.Roles);
                return Result.Success();
            }
            var error = result.Errors.First();

            return Result.Failure<UserResponse>(new Abstractions.Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        public async Task<Result> ToggleStatus(string Id, CancellationToken cancellationToken = default)
        {
            if (await _userManager.FindByIdAsync(Id) is not { } user)
                return Result.Failure<UserResponse>(UserErrors.UserNotFound);
            user.IsDisabled = !user.IsDisabled;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();

            return Result.Failure(new Abstractions.Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        public async Task<Result> Unlock(string id, CancellationToken cancellationToken = default)
        {
            if (await _userManager.FindByIdAsync(id) is not { } user)
                return Result.Failure(UserErrors.UserNotFound);

            var result = await _userManager.SetLockoutEndDateAsync(user, null);

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();

            return Result.Failure(new Abstractions.Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        public async Task<Result<UserProfileResponse>> GetProfileAsync(string Id)
        {
            var user = await _userManager.Users
                  .Where(u => u.Id == Id)
                  .ProjectToType<UserProfileResponse>()
                  .SingleAsync();
            return Result<UserProfileResponse>.Success(user);
        }

        public async Task<Result> UpdateProfileAsync(string Id, UpdatedProfileRequest request)
        {
            var user = await _userManager.FindByIdAsync(Id);
            user = request.Adapt(user);
            await _userManager.UpdateAsync(user!);
            return Result.Success();
        }

        public async Task<Result> ChangePasswordAsync(string Id, ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var result = await _userManager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassword);
            if (result.Succeeded)
                return Result.Success();

            var FirstErorr = result.Errors.First();
            return Result.Failure(new(FirstErorr.Code, FirstErorr.Description, StatusCodes.Status400BadRequest));

        }
    }
}
