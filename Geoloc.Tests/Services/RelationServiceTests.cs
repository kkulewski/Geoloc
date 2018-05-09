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
    public class RelationServiceTests
    {
        private Mock<IUnitOfWork> _uowMock;
        private Mock<IRelationRepository> _repoMock;
        private IRelationService _relationService;

        private User _john;
        private User _kate;
        private User _dave;
        private User _eric;
        private User _matt;
        private User _alex;
        private User _anne;

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
            _repoMock = new Mock<IRelationRepository>();
            _relationService = new RelationService(_uowMock.Object, _repoMock.Object);

            _john = new User { Id = Guid.NewGuid(), UserName = "john@test.com" };
            _kate = new User { Id = Guid.NewGuid(), UserName = "kate@test.com" };
            _dave = new User { Id = Guid.NewGuid(), UserName = "dave@test.com" };
            _eric = new User { Id = Guid.NewGuid(), UserName = "eric@test.com" };
            _matt = new User { Id = Guid.NewGuid(), UserName = "matt@test.com" };
            _alex = new User { Id = Guid.NewGuid(), UserName = "alex@test.com" };
            _anne = new User { Id = Guid.NewGuid(), UserName = "anne@test.com" };

            IList<Relation> relations = new List<Relation>
            {
                new Relation
                {
                    InvitingUser = _john,
                    InvitedUser = _kate,
                    RelationStatus = RelationStatus.Accepted
                },
                new Relation
                {
                    InvitingUser = _dave,
                    InvitedUser = _john,
                    RelationStatus = RelationStatus.Accepted
                },
                new Relation
                {
                    InvitingUser = _john,
                    InvitedUser = _eric,
                    RelationStatus = RelationStatus.Pending
                },
                new Relation
                {
                    InvitingUser = _matt,
                    InvitedUser = _john,
                    RelationStatus = RelationStatus.Pending
                },
                new Relation
                {
                    InvitingUser = _alex,
                    InvitedUser = _john,
                    RelationStatus = RelationStatus.Pending
                }
            };

            _repoMock.Setup(x => x.GetRelationsByUserId(_john.Id)).Returns(relations);
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
        public void SendRequests_GivenSelfInvitation_ReturnsFalse()
        {
            // Arrange
            var model = new RelationModel
            {
                InvitingUser = new UserModel {Id = _john.Id, UserName = _john.UserName},
                InvitedUser = new UserModel {Id = _john.Id, UserName = _john.UserName}
            };

            // Act
            var result = _relationService.SendRequest(model);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SendRequests_GivenExistingRelation_ReturnsFalse()
        {
            // Arrange
            var model = new RelationModel
            {
                InvitingUser = new UserModel { Id = _john.Id, UserName = _john.UserName },
                InvitedUser = new UserModel { Id = _kate.Id, UserName = _kate.UserName }
            };

            // Act
            var result = _relationService.SendRequest(model);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SendRequests_GivenNull_ReturnsFalse()
        {
            // Act
            var result = _relationService.SendRequest(null);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SendRequests_GivenValidModel_ReturnsTrue()
        {
            // Arrange
            var model = new RelationModel
            {
                InvitingUser = new UserModel { Id = _john.Id, UserName = _john.UserName },
                InvitedUser = new UserModel { Id = _anne.Id, UserName = _anne.UserName }
            };

            // Act
            var result = _relationService.SendRequest(model);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AcceptRequest_GivenNonExistingRelationId_ReturnsFalse()
        {
            // Act
            var result = _relationService.AcceptRequest(Guid.NewGuid());

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void AcceptRequest_GivenAcceptedRelationId_ReturnsFalse()
        {
            // Arrange
            var relation = new Relation { RelationStatus = RelationStatus.Accepted};
            _repoMock.Setup(x => x.GetRelationById(It.IsAny<Guid>())).Returns(relation);

            // Act
            var result = _relationService.AcceptRequest(Guid.NewGuid());

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void AcceptRequest_GivenPendingRelationId_ReturnsTrue()
        {
            // Arrange
            var relation = new Relation { RelationStatus = RelationStatus.Pending };
            _repoMock.Setup(x => x.GetRelationById(It.IsAny<Guid>())).Returns(relation);

            // Act
            var result = _relationService.AcceptRequest(Guid.NewGuid());

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GetRelationById_GivenNonExistingRelationId_ReturnsNull()
        {
            // Act
            var result = _relationService.GetRelationById(Guid.NewGuid());

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetRelationById_GivenExistingRelationId_ReturnsExpectedRelation()
        {
            // Arrange
            var relationId = Guid.NewGuid();
            var relation = new Relation { Id = relationId };
            _repoMock.Setup(x => x.GetRelationById(It.IsAny<Guid>())).Returns(relation);

            // Act
            var result = _relationService.GetRelationById(Guid.NewGuid());

            // Assert
            Assert.AreEqual(relationId, result.Id);
        }
    }
}
