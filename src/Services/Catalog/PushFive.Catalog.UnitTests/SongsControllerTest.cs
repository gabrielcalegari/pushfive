using Microsoft.AspNetCore.Mvc;
using Moq;
using PushFive.Catalog.Domain.Models;
using PushFive.Catalog.Domain.Repository;
using PushFive.Catalog.WebApi.Controllers;
using PushFive.Catalog.WebApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PushFive.Catalog.UnitTests
{
    public class SongsControllerTest
    {
        private readonly Mock<ISongRepository> songRepositoryMock;

        public SongsControllerTest()
        {
            songRepositoryMock = new Mock<ISongRepository>();
        }

        [Fact]
        public async Task GetSongs_Success()
        {
            // Arrange
            songRepositoryMock.Setup(x => x.CountSongs()).Returns(Task.FromResult(2L));
            songRepositoryMock.Setup(x => x.GetSongs(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(new List<Song>() as IEnumerable<Song>));

            // Act
            var songsController = new SongsController(songRepositoryMock.Object);
            var songsGet = new SongsGet { PageIndex = 1, PageSize = 1 };
            var actionResult = await songsController.GetSongs(songsGet);

            // Assert
            Assert.Equal((actionResult as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        }
    }
}
