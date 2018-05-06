using System;
using System.Collections.Generic;
using System.Linq;
using Geoloc.Data;
using Geoloc.Data.Entities;
using Geoloc.Data.Repositories.Abstract;
using Geoloc.Services;
using Moq;
using NUnit.Framework;
using AutoMapper;
using Geoloc.Infrastructure;
using Geoloc.Models;
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

        [Test]
        public void AddMeeting_GivenValidModel_ReturnsTrue()
        {
            // Arrange
            var model = new MeetingModel();
            _repoMock.Setup(x => x.Add(It.IsAny<Meeting>()));

            // Act
            var result = _meetingService.AddMeeting(model);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AddMeeting_GivenValidModel_AddsMappedMeetingEntityToRepository()
        {
            // Arrange
            var model = new MeetingModel { Name = "Some meeting", Description = "Some description" };
            Meeting entity = null;
            _repoMock.Setup(x => x.Add(It.IsAny<Meeting>())).Callback<Meeting>(mappedModel => entity = mappedModel);

            // Act
            _meetingService.AddMeeting(model);

            // Assert
            Assert.AreEqual(model.Name, entity.Name);
            Assert.AreEqual(model.Description, entity.Description);
        }

        [Test]
        public void AddMeeting_GivenNull_ReturnsFalse()
        {
            // Act
            var result = _meetingService.AddMeeting(null);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetAllMeetings_GivenEmptyRepo_ReturnsEmptyList()
        {
            // Arrange
            _repoMock.Setup(x => x.GetAll()).Returns(new List<Meeting>());

            // Act
            var result = _meetingService.GetAllMeetings();

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetAllMeetings_ReturnsExpectedNumberOfMeetings()
        {
            // Arrange
            var meetings = new List<Meeting>
            {
                new Meeting {Name = "Meeting1"},
                new Meeting {Name = "Meeting2"}
            };
            _repoMock.Setup(x => x.GetAll()).Returns(meetings);

            // Act
            var result = _meetingService.GetAllMeetings();

            // Assert
            Assert.AreEqual(2, result.ToList().Count);
        }
    }
}
