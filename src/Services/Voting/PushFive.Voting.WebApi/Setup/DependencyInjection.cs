using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PushFive.Core.Communication;
using PushFive.Core.Messages;
using PushFive.Voting.Data;
using PushFive.Voting.Data.Microsoft.Extensions.DependencyInjection;
using PushFive.Voting.Domain.Command;

namespace PushFive.Voting.WebApi.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Data base
            var votingDataConfiguration = new VotingDataConfiguration();
            configuration.Bind("VotingDataConfiguration", votingDataConfiguration);
            services.AddVotingData(votingDataConfiguration);

            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Handlers
            services.AddScoped<IRequestHandler<AddVotingCommand, bool>, VotingCommandHandler>();
        }
    }
}
