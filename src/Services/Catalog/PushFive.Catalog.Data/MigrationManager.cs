using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace PushFive.Catalog.Data
{
    /// <summary>
    /// It is not the best practice
    /// It is better to generate sql and run it in CD pipeline
    /// By the way, I used that because of limited time
    /// </summary>
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var dataContext = scope.ServiceProvider.GetRequiredService<CatalogContext>())
                {
                    try
                    {
                        dataContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        var connectionString = dataContext.Database.GetDbConnection().ConnectionString;
                        var logger = scope.ServiceProvider.GetRequiredService<ILogger<CatalogContext>>();
                        logger.LogError($"Connection String: {connectionString}");
                        logger.LogError(ex, "An error occurred migrating the DB.");
                        
                        throw;
                    }
                }
            }

            return host;
        }
    }
}
