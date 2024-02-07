using GlobalTicketManagement.Api.Middlewares;
using GlobalTicketManagement.Application;
using GlobalTicketManagement.Infrastructure;
using GlobalTicketManagement.Persistence;
using Microsoft.AspNetCore.HttpsPolicy;

namespace GlobalTicketManagement.Api.Extensions
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder) 
        {
            builder.Services.AddSwagger();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddControllers();

            builder.Services.AddMyCorsPolicy();
            builder.Services.AddTransient<ExceptionHandlerMiddleware>();


            return builder.Build();
        }

        public static WebApplication ConfigurePipeLines(this WebApplication app)
        {
            app.ConfigSwagger();

            app.UseHttpsRedirection();

            //app.UseRouting();

            app.UseCustomExceptionHandler();

            app.UseCors("Open");

            app.MapControllers();

            return app;
        }
    }
}
