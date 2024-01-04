namespace GlobalTicketManagement.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddMyCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Open",
                    builder => builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
             
            });
        }
    }
}
