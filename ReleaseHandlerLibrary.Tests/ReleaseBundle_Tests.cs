using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ReleaseHandlerLibrary.Tests
{
    [TestFixture]
    public class ReleaseBundle_Tests
    {
        #region Equals tests
        
        [Test]
        public void Equals_WhenObjectsAreEqual_ReturnsTrue()
        {
            // Arrange
            var name = "Alpha";
            var ver = new Version("v1.2.3.4");
            var field = VersionField.Build;

            var rb1 = new ReleaseBundle { Name = name, Version = ver, Field = field };
            var rb2 = new ReleaseBundle { Name = name, Version = ver, Field = field };

            // Act
            var res = rb1.Equals(rb2);

            // Assert
            Assert.That(res, Is.True);
        }

        [Test]
        public void Equals_WhenNamesDiffer_ReturnsFalse()
        {
            // Arrange
            var name = "Alpha";
            var ver = new Version("v1.2.3.4");
            var field = VersionField.Build;

            var rb1 = new ReleaseBundle { Name = name, Version = ver, Field = field };
            var rb2 = new ReleaseBundle { Name = name+"2", Version = ver, Field = field };

            // Act
            var res = rb1.Equals(rb2);

            // Assert
            Assert.That(res, Is.False);
        }

        [Test]
        public void Equals_WhenVersionsDiffer_ReturnsFalse()
        {
            // Arrange
            var name = "Alpha";
            var ver = new Version("v1.2.3.4");
            var ver2 = new Version("v2.0");
            var field = VersionField.Build;

            var rb1 = new ReleaseBundle { Name = name, Version = ver, Field = field };
            var rb2 = new ReleaseBundle { Name = name, Version = ver2, Field = field };

            // Act
            var res = rb1.Equals(rb2);

            // Assert
            Assert.That(res, Is.False);
        }

        [Test]
        public void Equals_WhenFieldsDiffer_ReturnsFalse()
        {
            // Arrange
            var name = "Alpha";
            var ver = new Version("v1.2.3.4");
            var field = VersionField.Build;
            var field2 = VersionField.Feature;

            var rb1 = new ReleaseBundle { Name = name, Version = ver, Field = field };
            var rb2 = new ReleaseBundle { Name = name, Version = ver, Field = field2 };

            // Act
            var res = rb1.Equals(rb2);

            // Assert
            Assert.That(res, Is.False);
        }

        [Test]
        public void Equals_WhenOtherIsDifferentObjectType_ReturnFalse()
        {
            // Arrange
            var name = "Alpha";
            var ver = new Version("v1.2.3.4");
            var field = VersionField.Build;

            var rb = new ReleaseBundle { Name = name, Version = ver, Field = field };

            // Act
            var res = rb.Equals(name);

            // Assert
            Assert.That(res, Is.False);
        }
        #endregion
    }
}
