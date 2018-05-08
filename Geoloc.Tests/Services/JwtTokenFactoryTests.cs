using Geoloc.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace Geoloc.Tests.Services
{
    [TestFixture]
    public class JwtTokenFactoryTests
    {
        private Mock<IConfiguration> _configMock;
        private JwtTokenFactory _factory;

        [SetUp]
        public void Setup()
        {
            _configMock = new Mock<IConfiguration>();
            var sectionMock = new Mock<IConfigurationSection>();
            sectionMock.Setup(x => x[It.IsAny<string>()]).Returns("1");
            _configMock.Setup(x => x.GetSection(It.IsAny<string>())).Returns(sectionMock.Object);

            _factory = new JwtTokenFactory(_configMock.Object);
        }

        [Test]
        public void GetSecurityKey_GivenTheSameKeys_ReturnsIdenticalSecurityKeyId()
        {
            // Arrange
            const string key = "test_key";

            // Act
            var result1 = JwtTokenFactory.GetSecurityKey(key);
            var result2 = JwtTokenFactory.GetSecurityKey(key);

            // Assert
            Assert.AreEqual(result1.KeyId, result2.KeyId);
        }
    }
}
