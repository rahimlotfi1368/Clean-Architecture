using GlobalTicketManagement.Application;
using GlobalTicketManagement.Infrastructure;
using GlobalTicketManagement.Persistence;

namespace GlobalTicketManagement.Api.Extensions
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder) 
        {
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddControllers();

            builder.Services.AddMyCorsPolicy();

            return builder.Build();
        }

        public static WebApplication ConfigurePipeLines(this WebApplication app)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Open");

            app.MapControllers();

            return app;
        }
    }
}
