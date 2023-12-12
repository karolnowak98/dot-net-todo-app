using TodoApp.API.Core.Data.Identity;
using TodoApp.API.Core.Data.Repositories;
using TodoApp.API.Interfaces;
using TodoApp.API.Services;

namespace TodoApp.API.Core.Other.Extensions;

public static class ServicesExtensions
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<ITasksRepository, TasksRepository>();
        services.AddScoped<ICategoryRepository, CategoriesRepository>();

        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITasksService, TasksService>();
        services.AddScoped<ICategoriesService, CategoriesService>();
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter the JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["Jwt:ValidIssuer"],
                    ValidAudience = configuration["Jwt:ValidAudience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"] ?? string.Empty))
                };
            });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options => { options.Password.RequiredLength = 10; });
    }

    public static void ConfigureSqlDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("TodoAppConnectionString"));
        });
    }

    public static void ConfigureMediatR(this IServiceCollection services)
    {
        var config = new MediatRServiceConfiguration();
        services.AddMediatR(config);
    }
}