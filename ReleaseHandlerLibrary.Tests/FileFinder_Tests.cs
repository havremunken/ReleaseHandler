using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ReleaseHandlerLibrary.Tests
{
    [TestFixture]
    public class FileFinder_Tests
    {
        #region FindFiles tests

        [Test]
        public void FindFiles_WhenSearchingTheCurrentDirectory_FindsExpectedFile()
        {
            // Arrange
            var ff = new FileFinder();

            // Act
            var res = ff.FindFiles(Environment.CurrentDirectory, "ReleaseHandlerLibrary.dll").ToList();

            // Assert
            Assert.That(res.Count, Is.EqualTo(1));
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void FindFiles_WhenGivenNonExistingPath_ThrowsArgumentException()
        {
            // Arrange
            var ff = new FileFinder();

            // Act
            ff.FindFiles("C:\\Bull\\Crap\\Non-existant\\Probably\\", "*.mp3");

            // Assert
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void FindFiles_WhenGivenEmptyString_ThrowsArgumentException()
        {
            // Arrange
            var ff = new FileFinder();

            // Act
            ff.FindFiles(String.Empty, "*.mp3");

            // Assert
        }

        [Test]
        public void FindFiles_WhenSearchingInaccessibleFolders_ReturnsPartialList()
        {
            // Arrange
            var ff = new FileFinder();

            // Act
            var res = ff.FindFiles("C:\\", "*.mp3");

            // Assert
            Assert.That(res, Is.InstanceOf(typeof(List<string>)));
        }

        #endregion
    }
}
