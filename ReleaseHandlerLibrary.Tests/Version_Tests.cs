using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ReleaseHandlerLibrary.Tests
{
    [TestFixture]
    public class Version_Tests
    {
        #region Constructor tests

        [Test]
        public void Constructor_WhenGivenValidVersionString_InitializesCorrectly()
        {
            // Arrange
            var vstring = "v1.2.3.4";

            // Act
            var ver = new Version(vstring);

            // Assert
            Assert.That(ver.Major, Is.EqualTo(1));
            Assert.That(ver.Minor, Is.EqualTo(2));
            Assert.That(ver.Feature, Is.EqualTo(3));
            Assert.That(ver.Build, Is.EqualTo(4));
        }

        [Test]
        public void Constructor_WhenGivenValidVersionStringWithNoVAtBeginning_InitializesCorrectly()
        {
            // Arrange
            var vstring = "1.2.3.4";

            // Act
            var ver = new Version(vstring);

            // Assert
            Assert.That(ver.Major, Is.EqualTo(1));
            Assert.That(ver.Minor, Is.EqualTo(2));
            Assert.That(ver.Feature, Is.EqualTo(3));
            Assert.That(ver.Build, Is.EqualTo(4));
        }

        [Test]
        public void Constructor_WhenGivenThreeFields_InitializesCorrectly()
        {
            // Arrange
            var vstring = "v7.8.9";

            // Act
            var ver = new Version(vstring);

            // Assert
            Assert.That(ver.Major, Is.EqualTo(7));
            Assert.That(ver.Minor, Is.EqualTo(8));
            Assert.That(ver.Feature, Is.EqualTo(9));
            Assert.That(ver.Build, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_WhenGivenTwoFields_InitializesCorrectly()
        {
            // Arrange
            var vstring = "v5.6";

            // Act
            var ver = new Version(vstring);

            // Assert
            Assert.That(ver.Major, Is.EqualTo(5));
            Assert.That(ver.Minor, Is.EqualTo(6));
            Assert.That(ver.Feature, Is.EqualTo(0));
            Assert.That(ver.Build, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_WhenGivenOneField_InitializesCorrectly()
        {
            // Arrange
            var vstring = "v12000";

            // Act
            var ver = new Version(vstring);

            // Assert
            Assert.That(ver.Major, Is.EqualTo(12000));
            Assert.That(ver.Minor, Is.EqualTo(0));
            Assert.That(ver.Feature, Is.EqualTo(0));
            Assert.That(ver.Build, Is.EqualTo(0));
        }
        
        [Test, ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithMalformedMajor_Throws()
        {
            // Arrange
            var ver = new Version("vHode.12.3.0");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithMalformedMinor_Throws()
        {
            // Arrange
            var ver = new Version("v5.Hode.3.0");
        }
        
        [Test, ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithMalformedFeature_Throws()
        {
            // Arrange
            var ver = new Version("v5.12.Saks.0");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithMalformedBuild_Throws()
        {
            // Arrange
            var ver = new Version("v1.12.3.papir");
        }

        #endregion

        #region Bump tests
        
        [Test]
        public void Bump_WhenBumpingMajor_BumpsProperly()
        {
            // Arrange
            var ver = new Version("v1.2.3.4");

            // Act
            ver.Bump(VersionField.Major);

            // Assert
            Assert.That(ver.Major, Is.EqualTo(2));
            Assert.That(ver.Minor, Is.EqualTo(0));
            Assert.That(ver.Feature, Is.EqualTo(0));
            Assert.That(ver.Build, Is.EqualTo(0));
        }

        [Test]
        public void Bump_WhenBumpingMinor_BumpsProperly()
        {
            // Arrange
            var ver = new Version("v1.2.3.4");

            // Act
            ver.Bump(VersionField.Minor);

            // Assert
            Assert.That(ver.Major, Is.EqualTo(1));
            Assert.That(ver.Minor, Is.EqualTo(3));
            Assert.That(ver.Feature, Is.EqualTo(0));
            Assert.That(ver.Build, Is.EqualTo(0));
        }

        [Test]
        public void Bump_WhenBumpingFeature_BumpsProperly()
        {
            // Arrange
            var ver = new Version("v1.2.3.4");

            // Act
            ver.Bump(VersionField.Feature);

            // Assert
            Assert.That(ver.Major, Is.EqualTo(1));
            Assert.That(ver.Minor, Is.EqualTo(2));
            Assert.That(ver.Feature, Is.EqualTo(4));
            Assert.That(ver.Build, Is.EqualTo(0));
        }

        [Test]
        public void Bump_WhenBumpingBuild_BumpsProperly()
        {
            // Arrange
            var ver = new Version("v1.2.3.4");

            // Act
            ver.Bump(VersionField.Build);

            // Assert
            Assert.That(ver.Major, Is.EqualTo(1));
            Assert.That(ver.Minor, Is.EqualTo(2));
            Assert.That(ver.Feature, Is.EqualTo(3));
            Assert.That(ver.Build, Is.EqualTo(5));
        }

        #endregion

        #region ToString tests

        [Test]
        public void ToString_WithFullVersionNumber_ReturnsExpectedResult()
        {
            // Arrange
            var vstring = "v12.20.88.9335";

            // Act
            var ver = new Version(vstring);
            var res = ver.ToString();

            // Assert
            Assert.That(res, Is.EqualTo(vstring));
        }

        [Test]
        public void ToString_WithThreeFirstValues_ReturnsExpectedResult()
        {
            // Arrange
            var vstring = "v12.20.88";

            // Act
            var ver = new Version(vstring);
            var res = ver.ToString();

            // Assert
            Assert.That(res, Is.EqualTo(vstring));
        }

        [Test]
        public void ToString_WithTwoFirstValues_ReturnsExpectedResult()
        {
            // Arrange
            var vstring = "v12.20";

            // Act
            var ver = new Version(vstring);
            var res = ver.ToString();

            // Assert
            Assert.That(res, Is.EqualTo(vstring));
        }

        [Test]
        public void ToString_WithFirstValue_ReturnsExpectedResult()
        {
            // Arrange
            var vstring = "v12";

            // Act
            var ver = new Version(vstring);
            var res = ver.ToString();

            // Assert
            Assert.That(res, Is.EqualTo(vstring));
        }

        [Test]
        public void ToString_StartValueWithoutVInFront_StillGivesVersionWithV()
        {
            // Arrange
            var vstring = "0.1.1.5";

            // Act
            var ver = new Version(vstring);
            var res = ver.ToString();

            // Assert
            Assert.That(res, Is.EqualTo("v0.1.1.5"));
        }

        #endregion

        #region Equals tests
        
        [Test]
        public void Equals_ComparedToNull_ReturnsFalse()
        {
            // Arrange
            var ver = new Version("v1.2.3.4");

            // Act
            var res = ver.Equals(null);

            // Assert
            Assert.That(res, Is.False);
        }
        
        [Test]
        public void Equals_WhenCalledWithDifferentObjectType_ReturnsFlase()
        {
            // Arrange
            var ver = new Version("v1.2.3.4");

            // Act
            var res = ver.Equals("v1.2.3.4");

            // Assert
            Assert.That(res, Is.False);
        }
        
        [Test]
        public void Equals_WhenCalledWithEqualVersionObject_ReturnsTrue()
        {
            // Arrange
            var ver = new Version("v1.2.3.4");
            var other = new Version("1.2.3.4");

            // Act
            var res = ver.Equals(other);

            // Assert
            Assert.That(res, Is.True);
        }
        
        [Test]
        public void Equals_WhenCalledWithDifferentVersionObject_ReturnsFalse()
        {
            // Arrange
            var ver = new Version("v1");
            var other = new Version("v2");

            // Act
            var res = ver.Equals(other);

            // Assert
            Assert.That(res, Is.False);
        }

        #endregion

        #region GetHashCode tests
        
        [Test]
        public void GetHashCode_ForTwoEqualObjects_ReturnsSameValue()
        {
            // Arrange
            var ver = new Version("v1.2.3.4");
            var other = new Version("1.2.3.4");

            // Act
            var res = ver.GetHashCode().Equals(other.GetHashCode());

            // Assert
            Assert.That(res, Is.True);
        }

        #endregion
    }
}
