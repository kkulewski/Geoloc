using AutoMapper;
using Geoloc.Infrastructure;
using NUnit.Framework;

namespace Geoloc.Tests.Infrastructure
{
    [TestFixture]
    public class AutoMapperConfigurationTests
    {
        [Test]
        public void CheckAutoMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperConfiguration()));
            config.AssertConfigurationIsValid();
        }
    }
}