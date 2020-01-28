using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PushFive.Core.Communication;
using PushFive.Core.Messages;
using PushFive.Voting.Domain.Command;
using PushFive.Voting.Domain.Repository;
using PushFive.Voting.WebApi.Controllers;
using PushFive.Voting.WebApi.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PushFive.Voting.UnitTests.WebApi
{
    public class VotingsControllerTest
    {
        private readonly Mock<IMediatorHandler> mediatorHandlerMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<DomainNotificationHandler> domainNotificationHandlerMock;
        private readonly Mock<IVotingRepository> votingRepositoryMock;

        private readonly VotingsController votingsController;

        public VotingsControllerTest()
        {
            mediatorHandlerMock = new Mock<IMediatorHandler>();
            mapperMock = new Mock<IMapper>();
            domainNotificationHandlerMock = new Mock<DomainNotificationHandler>();
            votingRepositoryMock = new Mock<IVotingRepository>();

            votingsController = new VotingsController(mediatorHandlerMock.Object, mapperMock.Object, domainNotificationHandlerMock.Object, votingRepositoryMock.Object);
        }

        [Fact]
        public async Task PostVoting_Success()
        {
            //Arrange
            mapperMock.Setup(x => x.Map<AddVotingCommand>(It.IsAny<object>()))
                .Returns(new AddVotingCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IEnumerable<AddVotingCommand.VotingItem>>()));

            domainNotificationHandlerMock.Setup(x => x.HasNotifications()).Returns(false);

            mediatorHandlerMock.Setup(x => x.SendCommand(It.IsAny<AddVotingCommand>()))
                 .Returns(Task.FromResult(true));

            //Act
            var actionResult = await votingsController.PostVoting(new VotingPost());

            //Assert
            Assert.Equal((actionResult as StatusCodeResult).StatusCode, (int)System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task PostVoting_BadRequest()
        {
            //Arrange
            mapperMock.Setup(x => x.Map<AddVotingCommand>(It.IsAny<object>()))
                .Returns(new AddVotingCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IEnumerable<AddVotingCommand.VotingItem>>()));

            domainNotificationHandlerMock.Setup(x => x.HasNotifications()).Returns(true);
            domainNotificationHandlerMock.Setup(x => x.GetNotifications()).Returns(new List<DomainNotification>());

            mediatorHandlerMock.Setup(x => x.SendCommand(It.IsAny<AddVotingCommand>()))
                 .Returns(Task.FromResult(true));

            //Act
            var actionResult = await votingsController.PostVoting(new VotingPost());

            //Assert
            Assert.Equal((actionResult as BadRequestObjectResult).StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetVotingResult_Success()
        {
            //Arrange
            votingRepositoryMock.Setup(x => x.GetFiveMostVotedSongs()).Returns(Task.FromResult(new List<Guid>() as IEnumerable<Guid>));

            //Act
            var actionResult = await votingsController.GetVotingResult();

            //Assert
            Assert.Equal((actionResult as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        }
    }
}
