using Microsoft.Identity.Client;

namespace WebAPI
{
    public static class CorsConfigurations
    {
        private const string CorsPolicyName = "CorsPolicy";
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            var corsOrigins = new string[]
            {
                "http://localhost:5173",
                "http://localhost:3000",
                "http://localhost:5247"
            };

            return services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins(corsOrigins);
                });
            });
        }
        public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
        {
            return app.UseCors(CorsPolicyName);
        }
    }
}
