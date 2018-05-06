using System;
using Geoloc.Data;
using Geoloc.Data.Entities;
using Geoloc.Data.Repositories.Abstract;
using Geoloc.Services;
using Moq;
using NUnit.Framework;
using AutoMapper;
using Geoloc.Infrastructure;
using Geoloc.Services.Abstract;

namespace Geoloc.Tests.Services
{
    [TestFixture]
    public class MeetingServiceTests
    {
        private Mock<IUnitOfWork> _uowMock;
        private Mock<IMeetingRepository> _repoMock;
        private IMeetingService _meetingService;

        [OneTimeSetUp]
        public void InitialSetup()
        {
            Mapper.Initialize(c => c.AddProfile(new AutoMapperConfiguration()));
        }

        [SetUp]
        public void Setup()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repoMock = new Mock<IMeetingRepository>();
            _meetingService = new MeetingService(_uowMock.Object, _repoMock.Object);
        }

        [Test]
        public void GetById_GivenExistingId_ReturnsExpectedMeeting()
        {
            // Arrange
            var id = new Guid();
            var meeting = new Meeting {Id = id, Name = "Test meeting"};
            _repoMock.Setup(x => x.Get(id)).Returns(meeting);

            // Act
            var result = _meetingService.GetById(id);

            // Assert
            Assert.AreEqual(meeting.Name, result.Name);
        }

        [Test]
        public void GetById_GivenNonExistingId_ReturnsNull()
        {
            // Arrange
            _repoMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns((Meeting) null);

            // Act
            var result = _meetingService.GetById(new Guid());

            // Assert
            Assert.IsNull(result);
        }
    }
}
