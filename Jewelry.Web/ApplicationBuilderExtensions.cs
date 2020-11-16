using Microsoft.AspNetCore.Builder;

namespace Jewelry.Web
{
    internal static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomSwaggerUi(this IApplicationBuilder application) =>
            application.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", nameof(Jewelry));
            });
    }
}