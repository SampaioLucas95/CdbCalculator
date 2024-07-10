using CalculoCdbApi.Application.Commands;
using CDBCalculator.Application.Shared;
using CDBCalculator.Domain.Service;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CDBCalculator.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<ICdbService, CdbService>();
            services.AddScoped<IValidator<CalcularCdbCommand>, CalcularCdbCommandValidator>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddScoped<IRequestHandler<CalcularCdbCommand, GenericCommandResult<CalcularCdbCommandResult>>>(
                cfg => {
                    return new CalcularCdbCommandHandler(
                        cfg.GetRequiredService<IValidator<CalcularCdbCommand>>(),
                         cfg.GetRequiredService<ICdbService>());
                });

        }
    }
}
