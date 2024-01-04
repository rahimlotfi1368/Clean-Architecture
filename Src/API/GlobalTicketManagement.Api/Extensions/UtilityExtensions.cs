using GlobalTicketManagement.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GlobalTicketManagement.Api.Extensions
{
    public static class UtilityExtensions
    {
        /// <summary>
        /// Every time That The Application is run The database will be reset entirely
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static async Task ResetDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetService<DatabaseContext>();
                if (context != null)
                {
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                //var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
                //logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
    }
}
