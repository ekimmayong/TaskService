using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskService.Domain.Interfaces.BaseRepository;
using TaskService.Domain.Interfaces.Context;
using TaskService.Domain.Interfaces.IServices;
using TaskService.Domain.Models;
using TaskService.Domain.Services;
using TaskService.Infrastructure.Context;
using TaskService.Infrastructure.Repositories;

namespace TaskService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        // Extends IServiceCollection by adding Db Context Configurations
        public static void AddDbContextConfiguration(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("TaskServiceDb");
            });
        }

        // Extends IServiceCollection by Adding Authentication and Authorization configuration
        public static void AddAuthorizationExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
                };
            });
        }

        // Configure and register Dependency injection
        public static IServiceCollection AddApplicationDependencyInjection(this IServiceCollection services)
        {
            // Context DI
            services.AddScoped<IApplicationContext, ApplicationDbContext>();

            // Base Repository DI
            services.AddScoped<IBaseRepository<TaskModel>, TaskRepository>();
            services.AddScoped<IIdentityRepository, IdentityRepository>();

            //Services DI
            services.AddScoped<ITasksService, TasksService>();
            services.AddScoped<IIdentityService, IdentityService>();


            return services;
        }
    }
}
