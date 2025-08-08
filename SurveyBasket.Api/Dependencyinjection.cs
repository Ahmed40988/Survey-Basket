
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SurveyBasket.Api.Authentication;
using SurveyBasket.Api.Authentication.Filters;
using SurveyBasket.Api.Error;
using SurveyBasket.Api.Services;
using SurveyBasket.Api.Settings;
using System.Text;
using System.Threading.RateLimiting;
namespace SurveyBasket.Api
{
    public static class Dependencyinjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services,
             IConfiguration configuration)
        {
            services.AddControllers();
            services.AddDistributedMemoryCache();

            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>()!)
                )
            );

            services.AddAuthConfig(configuration);

            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbcontext>(options =>
                options.UseSqlServer(connectionString));

            services
                .AddSwaggerServices()
                .AddMapsterConfig()
                .AddFluentValidationConfig();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPollservice, PollService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IVoteService, VoteService>();
            services.AddScoped<IResultService, ResultService>();
            services.AddScoped<INotificationService,NotificationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IEmailSender, EmailService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddBackgroundJobsConfig(configuration);
            services.AddHttpContextAccessor();

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            services.AddHealthChecks()
                .AddSqlServer(connectionString: configuration.GetConnectionString("DefaultConnection")!, name: "Database");
            services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));

            services.AddRateLimitingConfig();

            return services;
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
        {
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton<IMapper>(new Mapper(mappingConfig));

            return services;
        }

        private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
        {
            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }

        private static IServiceCollection AddAuthConfig(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
              .AddEntityFrameworkStores<ApplicationDbcontext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            services.AddSingleton<IjwtProvider, jwtProvider>();

            //services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            services.AddOptions<JWToptions>()
                .BindConfiguration(JWToptions.Sectionname)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            var jwtSettings = configuration.GetSection(JWToptions.Sectionname).Get<JWToptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience
                };
            });
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
            }
            );

            return services;
        }
        private static IServiceCollection AddBackgroundJobsConfig(this IServiceCollection services,
         IConfiguration configuration)
        {
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));

            services.AddHangfireServer();

            return services;
        }
        private static IServiceCollection AddRateLimitingConfig(this IServiceCollection services)
        {
            services.AddRateLimiter(rateLimiterOptions =>
            {
                rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                rateLimiterOptions.AddPolicy(RateLimiters.IpLimiter, httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 2,
                            Window = TimeSpan.FromSeconds(20)
                        }
                    )
                );

                rateLimiterOptions.AddPolicy(RateLimiters.UserLimiter, httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User.GetUserId(),
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 2,
                            Window = TimeSpan.FromSeconds(20)
                        }
                    )
                );

                rateLimiterOptions.AddConcurrencyLimiter(RateLimiters.Concurrency, options =>
                {
                    options.PermitLimit = 1000;
                    options.QueueLimit = 100;
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                //rateLimiterOptions.AddTokenBucketLimiter("token", options =>
                //{
                //    options.TokenLimit = 2;
                //    options.QueueLimit = 1;
                //    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                //    options.ReplenishmentPeriod = TimeSpan.FromSeconds(30);
                //    options.TokensPerPeriod = 2;
                //    options.AutoReplenishment = true;
                //});

                //rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
                //{
                //    options.PermitLimit = 2;
                //    options.Window = TimeSpan.FromSeconds(20);
                //    options.QueueLimit = 1;
                //    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                //});

                //rateLimiterOptions.AddSlidingWindowLimiter("sliding", options =>
                //{
                //    options.PermitLimit = 2;
                //    options.Window = TimeSpan.FromSeconds(20);
                //    options.SegmentsPerWindow = 2;
                //    options.QueueLimit = 1;
                //    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                //});
            });

            return services;
        }
    }
}




