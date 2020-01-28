using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PushFive.Voting.Data.Repository;
using PushFive.Voting.Domain.Repository;
using System;

namespace PushFive.Voting.Data.Microsoft.Extensions.DependencyInjection
{
    public static class VotingDataServiceCollectionExtensions
    {
        public static IServiceCollection AddVotingData(this IServiceCollection services, VotingDataConfiguration votingDataConfiguration)
        {
            votingDataConfiguration = votingDataConfiguration ?? throw new ArgumentNullException(nameof(votingDataConfiguration));

            services.AddDbContext<VotingContext>(options =>
                options.UseSqlServer(votingDataConfiguration.SqlConnectionString));

            services.AddScoped<IVotingRepository, VotingRepository>();

            return services;
        }
    }
}
