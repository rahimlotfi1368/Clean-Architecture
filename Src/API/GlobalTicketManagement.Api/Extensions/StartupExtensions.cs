using GlobalTicketManagement.Api.Middlewares;
using GlobalTicketManagement.Api.Services;
using GlobalTicketManagement.Application;
using GlobalTicketManagement.Application.Contracts;
using GlobalTicketManagement.Infrastructure;
using GlobalTicketManagement.Persistence;
using GlobalTicketManagement.Identity;
using Microsoft.AspNetCore.HttpsPolicy;
using Serilog;

namespace GlobalTicketManagement.Api.Extensions
{
    public static class StartupExtensions
    {
        public static void ConfigureSerilog(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration.WriteTo.Console().ReadFrom.Configuration(context.Configuration));
        }
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder) 
        {
            builder.ConfigureSerilog();
            builder.Services.AddSwagger();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
            builder.Services.AddHttpContextAccessor();

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

            app.UseAuthentication();

            app.UseCustomExceptionHandler();

            app.UseCors("Open");

            app.UseAuthorization();

            app.MapControllers();

            app.UseSerilogRequestLogging();

            return app;
        }
    }
}
