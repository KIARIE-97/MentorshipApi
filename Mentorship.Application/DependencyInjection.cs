using System;
using Microsoft.Extensions.DependencyInjection;

namespace Mentorship.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication( this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
        cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        //register automapper
        services.AddAutoMapper(typeof(EnrollmentProfile));

           // Add validators if using FluentValidation
        // services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
    return services;
    }
}
