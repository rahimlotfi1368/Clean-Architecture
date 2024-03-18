using GlobalTicketManagement.Api.Extensions;
using GlobalTicketManagement.Identity.SeedData;
using Serilog;

namespace GlobalTicketManagement.Api
{
    public class Program
    {
        public static async  Task Main(string[] args)
        {
            Log.Logger=new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            Log.Information("GloboTicket API starting");

            var builder = WebApplication.CreateBuilder(args);

            var app = builder
                .ConfigureServices()
                .ConfigurePipeLines();

            await app.ResetDatabaseAsync();

            app.Run();
        }
    }
}
