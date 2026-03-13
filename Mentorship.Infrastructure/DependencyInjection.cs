using System;
using Mentorship.Api.Data;
using Mentorship.Core.Entities;
using Mentorship.Core.Interfaces;
using Mentorship.Core.Interfaces.Repositories;
using Mentorship.Infrastructure.Persitence;
using Mentorship.Infrastructure.Persitence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mentorship.Infrastructure;

public static class DependencyInjection
{
     public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        
        // Register repositories
        services.AddScoped<IMentorshipProgramRepository, MentorshipProgramRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        
        // Register Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Register DbContext itself if needed directly
        services.AddScoped<AppDbContext>();
        
        return services;
    }
}
