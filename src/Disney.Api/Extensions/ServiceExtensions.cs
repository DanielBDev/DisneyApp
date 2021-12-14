using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Disney.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(configuration =>
            {
                configuration.DefaultApiVersion = new ApiVersion(1, 0);

                configuration.AssumeDefaultVersionWhenUnspecified = true;

                configuration.ReportApiVersions = true;
            });
        }
    }
}
