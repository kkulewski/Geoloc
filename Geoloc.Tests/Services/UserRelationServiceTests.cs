using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Geoloc.Data;
using Geoloc.Data.Entities;
using Geoloc.Data.Repositories.Abstract;
using Geoloc.Infrastructure;
using Geoloc.Models;
using Geoloc.Services;
using Geoloc.Services.Abstract;
using Moq;
using NUnit.Framework;

namespace Geoloc.Tests.Services
{
    [TestFixture]
    public class UserRelationServiceTests
    {
        private Mock<IUnitOfWork> _uowMock;
        private Mock<IUserRelationRepository> _repoMock;
        private IUserRelationService _relationService;

        private AppUser _john;
        private AppUser _kate;
        private AppUser _dave;
        private AppUser _eric;
        private AppUser _matt;
        private AppUser _alex;
        private AppUser _anne;

        [OneTimeSetUp]
        public void InitialSetup()
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.AddProfile(new AutoMapperConfiguration()));
        }

        [SetUp]
        public void Setup()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repoMock = new Mock<IUserRelationRepository>();
            _relationService = new UserRelationService(_uowMock.Object, _repoMock.Object);

            _john = new AppUser { Id = Guid.NewGuid(), UserName = "john@test.com" };
            _kate = new AppUser { Id = Guid.NewGuid(), UserName = "kate@test.com" };
            _dave = new AppUser { Id = Guid.NewGuid(), UserName = "dave@test.com" };
            _eric = new AppUser { Id = Guid.NewGuid(), UserName = "eric@test.com" };
            _matt = new AppUser { Id = Guid.NewGuid(), UserName = "matt@test.com" };
            _alex = new AppUser { Id = Guid.NewGuid(), UserName = "alex@test.com" };
            _anne = new AppUser { Id = Guid.NewGuid(), UserName = "anne@test.com" };

            IList<UserRelation> relations = new List<UserRelation>
            {
                new UserRelation
                {
                    InvitingUser = _john,
                    InvitedUser = _kate,
                    UserRelationStatus = UserRelationStatus.Accepted
                },
                new UserRelation
                {
                    InvitingUser = _dave,
                    InvitedUser = _john,
                    UserRelationStatus = UserRelationStatus.Accepted
                },
                new UserRelation
                {
                    InvitingUser = _john,
                    InvitedUser = _eric,
                    UserRelationStatus = UserRelationStatus.Pending
                },
                new UserRelation
                {
                    InvitingUser = _matt,
                    InvitedUser = _john,
                    UserRelationStatus = UserRelationStatus.Pending
                },
                new UserRelation
                {
                    InvitingUser = _alex,
                    InvitedUser = _john,
                    UserRelationStatus = UserRelationStatus.Pending
                }
            };

            _repoMock.Setup(x => x.GetUserRelationsByUser(_john.Id)).Returns(relations);
        }

        [Test]
        public void GetUserSentRequests_ReturnsExpectedRelations()
        {
            // Act
            var relations = _relationService.GetUserSentRequests(_john.Id).ToList();

            // Assert
            bool containsEric = relations.Any(x => x.InvitedUser.UserName == _eric.UserName);
            bool containsOneRelation = relations.Count == 1;
            Assert.IsTrue(containsOneRelation && containsEric);
        }

        [Test]
        public void GetUserReceivedRequests_ReturnsExpectedRelations()
        {
            // Act
            var relations = _relationService.GetUserReceivedRequests(_john.Id).ToList();

            // Assert
            bool containsMatt = relations.Any(x => x.InvitingUser.UserName == _matt.UserName);
            bool containsAlex = relations.Any(x => x.InvitingUser.UserName == _alex.UserName);
            bool containsTwoRelations = relations.Count == 2;
            Assert.IsTrue(containsTwoRelations && containsMatt && containsAlex);
        }

        [Test]
        public void GetUserRelations_ReturnsExpectedRelations()
        {
            // Act
            var relations = _relationService.GetUserRelations(_john.Id).ToList();

            // Assert
            bool containsKate = relations.Any(x => x.InvitedUser.UserName == _kate.UserName);
            bool containsDave = relations.Any(x => x.InvitingUser.UserName == _dave.UserName);
            bool containsTwoRelations = relations.Count == 2;
            Assert.IsTrue(containsTwoRelations && containsKate && containsDave);
        }

        [Test]
        public void SendRelationRequests_GivenSelfInvitation_ReturnsFalse()
        {
            // Arrange
            var model = new UserRelationModel
            {
                InvitingUser = new UserModel {Id = _john.Id, UserName = _john.UserName},
                InvitedUser = new UserModel {Id = _john.Id, UserName = _john.UserName}
            };

            // Act
            var result = _relationService.SendRelationRequest(model);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
