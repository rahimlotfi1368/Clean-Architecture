using GlobalTicketManagement.Api.Extensions;

namespace GlobalTicketManagement.Api
{
    public class Program
    {
        public static async void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder
                .ConfigureServices()
                .ConfigurePipeLines();

            await app.ResetDatabaseAsync();

            app.Run();
        }
    }
}
