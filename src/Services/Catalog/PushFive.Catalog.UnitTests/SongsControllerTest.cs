using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PushFive.Catalog.Domain.Models;
using PushFive.Catalog.Domain.Repository;
using PushFive.Catalog.WebApi.Controllers;
using PushFive.Catalog.WebApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PushFive.Catalog.UnitTests
{
    public class SongsControllerTest
    {
        private readonly Mock<ISongRepository> songRepositoryMock;
        private readonly Mock<IMapper> mapperMock;

        private readonly SongsController songsController;

        public SongsControllerTest()
        {
            songRepositoryMock = new Mock<ISongRepository>();
            mapperMock = new Mock<IMapper>();

            songsController = new SongsController(songRepositoryMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task GetSongs_Success()
        {
            // Arrange
            songRepositoryMock.Setup(x => x.CountSongs()).Returns(Task.FromResult(2L));
            songRepositoryMock.Setup(x => x.GetSongs(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(new List<Song>() as IEnumerable<Song>));

            // Act
            var songsGet = new SongsGet { PageIndex = 1, PageSize = 1 };
            var actionResult = await songsController.GetSongs(songsGet);

            // Assert
            Assert.Equal((actionResult as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetSongsByIds_Success()
        {
            // Arrange
            songRepositoryMock.Setup(x => x.GetSongsByIds(It.IsAny<Guid>())).Returns(Task.FromResult(new List<Song>() as IEnumerable<Song>));

            // Act
            var ids = GetGuids(3).ToArray();
            var actionResult = await songsController.GetSongsById(ids);

            // Assert
            Assert.Equal((actionResult as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetSongsByIds_BadRequest()
        {
            // Arrange
            songRepositoryMock.Setup(x => x.GetSongsByIds(It.IsAny<Guid>())).Returns(Task.FromResult(new List<Song>() as IEnumerable<Song>));

            // Act
            var ids = GetGuids(50).ToArray();
            var actionResult = await songsController.GetSongsById(ids);

            // Assert
            Assert.Equal((actionResult as BadRequestResult).StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        private IEnumerable<Guid> GetGuids(int count)
        {
            for (int i = 0; i < count; i++)
                yield return Guid.NewGuid();
        }
    }
}
