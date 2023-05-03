using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TaskManagementAPI.Data;
using TaskManagementAPI.Data.UnitOfWork;
using TaskManagementAPI.Services;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Helpers
{
    public static class StartupHelper
    {
        public static void AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskManagementDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"))
                       .UseSnakeCaseNamingConvention();
            });

        }
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = configuration["Jwt:Issuer"],
                       ValidAudience = configuration["Jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
                   };
               });
        }

        public static void AddControllerServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICommentService,CommentService>();
            services.AddTransient<IUserService,UserService>();
        }
        public static void AddSwaggerWithAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Management API", Version = "v1" });

                // JWT Bearer Authorization
                c.AddSecurityDefinition("Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
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

    }
}
