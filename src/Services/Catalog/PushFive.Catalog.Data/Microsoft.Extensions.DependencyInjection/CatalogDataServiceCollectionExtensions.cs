using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PushFive.Catalog.Data.Repository;
using PushFive.Catalog.Domain.Repository;
using System;

namespace PushFive.Catalog.Data.Microsoft.Extensions.DependencyInjection
{
    public static class CatalogDataServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogData(this IServiceCollection services, CatalogDataConfiguration catalogDataConfiguration)
        {
            catalogDataConfiguration = catalogDataConfiguration ?? throw new ArgumentNullException(nameof(catalogDataConfiguration));

            services.AddDbContext<CatalogContext>(options => 
                options.UseSqlServer(catalogDataConfiguration.SqlConnectionString));

            services.AddScoped<ISongRepository, SongRepository>();

            return services;
        }
    }
}
