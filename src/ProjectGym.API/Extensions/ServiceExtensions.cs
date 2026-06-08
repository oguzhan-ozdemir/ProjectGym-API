using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectGym.Application.Interfaces;
using ProjectGym.Domain.Interfaces;
using ProjectGym.Infrastructure.Data;
using ProjectGym.Infrastructure.Identity;
using ProjectGym.Infrastructure.Repositories;

namespace ProjectGym.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddDatabese(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string(Default Connection) bulunamadı!");

        services.AddDbContext<ProjectGymDbContext>(options=>options.UseNpgsql(connectionString));

        return services;
    }

    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<AppIdentityUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit=true;
            options.Password.RequiredLength=8;
            options.Password.RequireNonAlphanumeric=true;
            options.Password.RequireUppercase=true; 
            options.Password.RequireLowercase=true; 
            options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts=5;
            options.User.RequireUniqueEmail=true;
        })
        .AddEntityFrameworkStores<ProjectGymDbContext>()
        .AddDefaultTokenProviders();

        // services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
        services.AddScoped<IMemberRepository,MemberRepository>();
        services.AddScoped<IMembershipPlanRepository, MembershipPlanRepository>();
        services.AddScoped<IMembershipRepository, MembershipRepository>();
        services.AddScoped<ITrainerRepository, TrainerRepository>();
        services.AddScoped<IWorkoutSessionRepository, WorkoutSessionRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

}
