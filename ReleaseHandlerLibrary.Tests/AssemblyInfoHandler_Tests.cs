using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ReleaseHandlerLibrary.Tests
{
    [TestFixture]
    public class AssemblyInfoHandler_Tests
    {
        #region Setup and teardown

        [SetUp]
        public void Setup()
        {
            var clean = new AssemblyInfoHandler("AssemblyInfo.cs");
            clean.SetVersion(new Version("0.0.0.0"));
            clean.Save();
        }

        [TearDown]
        public void TearDown()
        {
            Setup();
        }

        #endregion

        #region Simple test for full code coverage - bad stuff!

        [Test]
        public void AssemblyInfo_DoesStuff_LikeWeWantItTo()
        {
            // Arrange
            var ai = new AssemblyInfoHandler("AssemblyInfo.cs");

            // Act
            Assert.That(ai.Contents, Is.Not.Empty);
            Assert.That(ai.TextChanged, Is.EqualTo(false));
            ai.SetVersion(new Version("v1.2.3.4"));
            Assert.That(ai.TextChanged, Is.EqualTo(true));

            ai.Save();

            // Assert
        }

        #endregion
    }
}
