using Microsoft.Identity.Client;

namespace WebAPI
{
    public static class CorsConfigurations
    {
        private const string CorsPolicyName = "CorsPolicy";
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });
        }
        public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
        {
            return app.UseCors(CorsPolicyName);
        }
    }
}
