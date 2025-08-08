using SurveyBasket.Api.Contracts.Roles;
using SurveyBasket.Api.ContractsDTO.Roles;


namespace SurveyBasket.Api.Services.Service
{
    public class RoleService(RoleManager<ApplicationRole> roleManager, ApplicationDbcontext context) : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
        private readonly ApplicationDbcontext _context = context;

        public async Task<IEnumerable<RoleResponse>> GetAllAsync(bool includeDisabled = false, CancellationToken cancellationToken = default) =>
            await _roleManager.Roles
              .Where(x => !x.IsDefault && (!x.IsDeleted || (includeDisabled)))
            .ProjectToType<RoleResponse>()
            .ToListAsync(cancellationToken);

        public async Task<Result<RoleDetailResponse>> GetAsync(string Id, CancellationToken cancellationToken = default)
        {
            if (await _roleManager.FindByIdAsync(Id) is not { } role)
                return Result.Failure<RoleDetailResponse>(RoleErrors.RoleNotFound);

            var permetions = await _roleManager.GetClaimsAsync(role!);

            var response = new RoleDetailResponse(role.Id, role.Name!, role.IsDeleted, permetions.Select(x => x.Value));

            return Result.Success(response);
        }

        public async Task<Result<RoleDetailResponse>> AddAsync(RoleRequest request, CancellationToken cancellationToken = default)
        {
            var isExisted = await _roleManager.RoleExistsAsync(request.Name);
            if (isExisted)
                return Result.Failure<RoleDetailResponse>(RoleErrors.DuplicatedRole);

            var allowedPermissions = Permissions.GetAllPermissions();

            if (request.Permissions.Except(allowedPermissions).Any())
                return Result.Failure<RoleDetailResponse>(RoleErrors.InvalidPermissions);

            var role = new ApplicationRole
            {
                Name = request.Name,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                var permissions = request.Permissions
                    .Select(x => new IdentityRoleClaim<string>
                    {
                        ClaimType = Permissions.Type,
                        ClaimValue = x,
                        RoleId = role.Id
                    });

                await _context.AddRangeAsync(permissions);
                await _context.SaveChangesAsync();

                var response = new RoleDetailResponse(role.Id, role.Name, role.IsDeleted, request.Permissions);

                return Result.Success(response);
            }
            var error = result.Errors.First();
            return Result.Failure<RoleDetailResponse>(new Abstractions.Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        public async Task<Result<RoleDetailResponse>> UpdateAsync(string Id, RoleRequest request, CancellationToken cancellationToken = default)
        {
            var isExisted = await _roleManager.Roles.AnyAsync(x => x.Name == request.Name && x.Id != Id);
            if (isExisted)
                return Result.Failure<RoleDetailResponse>(RoleErrors.DuplicatedRole);

            if (await _roleManager.FindByIdAsync(Id) is not { } role)
                return Result.Failure<RoleDetailResponse>(RoleErrors.RoleNotFound);

            var allowedPermissions = Permissions.GetAllPermissions();

            if (request.Permissions.Except(allowedPermissions).Any())
                return Result.Failure<RoleDetailResponse>(RoleErrors.InvalidPermissions);

            role.Name = request.Name;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                var CurrentPermissions = await _context.RoleClaims
                    .Where(x => x.RoleId == Id && x.ClaimType == Permissions.Type)
                    .Select(x => x.ClaimValue)
                    .ToListAsync(cancellationToken);

                var newPermissions = request.Permissions.Except(CurrentPermissions)
                      .Select(x => new IdentityRoleClaim<string>
                      {
                          ClaimType = Permissions.Type,
                          ClaimValue = x,
                          RoleId = role.Id
                      });
                var removedPermissions = CurrentPermissions.Except(request.Permissions);

                await _context.RoleClaims
                    .Where(x => x.RoleId == Id && removedPermissions.Contains(x.ClaimValue))
                    .ExecuteDeleteAsync();

                await _context.AddRangeAsync(newPermissions);
                await _context.SaveChangesAsync();

                var response = new RoleDetailResponse(role.Id, role.Name, role.IsDeleted, request.Permissions);

                return Result.Success(response);
            }
            var error = result.Errors.First();
            return Result.Failure<RoleDetailResponse>(new Abstractions.Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        public async Task<Result> ToggleStatusAsync(string Id, CancellationToken cancellationToken = default)
        {
            if (await _roleManager.FindByIdAsync(Id) is not { } role)
                return Result.Failure<RoleDetailResponse>(RoleErrors.RoleNotFound);
            role.IsDeleted = !role.IsDeleted;
            await _roleManager.UpdateAsync(role);
            return Result.Success();
        }
    }
}
