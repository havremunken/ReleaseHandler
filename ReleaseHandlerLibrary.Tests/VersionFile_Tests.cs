using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ReleaseHandlerLibrary;
using System.IO;

namespace ReleaseHandlerLibrary.Tests
{
    [TestFixture]
    public class VersionFile_Tests
    {
        private string testfilename = "TestVersionFile.ini";

        #region Setup and teardown

        [SetUp]
        public void Setup()
        {
            if (File.Exists(testfilename))
                File.Delete(testfilename);
        }

        [TearDown]
        public void Teardown()
        {
            if (File.Exists(testfilename))
                File.Delete(testfilename);
        }

        #endregion

        #region Versions tests

        [Test]
        public void Versions_WithNewlyConstructedObject_IsEmpty()
        {
            // Arrange
            var v = new VersionFile();

            // Act
            var res = v.Versions.Count();

            // Assert
            Assert.That(res, Is.EqualTo(0));
        }
        
        [Test]
        public void AddVersion_WhenAddingSimpleVersion_ActuallyAddsVersion()
        {
            // Arrange
            var v = new VersionFile();
            var name = "Alpha";
            var ver = new Version("v1.2.3.4");
            var field = VersionField.Build;
            
            // Act
            v.AddVersion(name, ver, field);

            // Assert
            var tuple = v.Versions.Single();
            Assert.That(tuple.Name, Is.EqualTo(name));
            Assert.That(tuple.Version, Is.EqualTo(ver));
            Assert.That(tuple.Field, Is.EqualTo(field));
        }

        #endregion

        #region Load and save tests
        
        [Test]
        public void Save_WhenSaving_PutsSomethingOnDisk()
        {
            // Arrange
            var vf = new VersionFile();
            vf.AddVersion("Alpha", new Version("v1.2"), VersionField.Minor);

            // Act
            vf.Save(testfilename);

            // Assert
            Assert.That(File.Exists(testfilename), Is.True);
        }

        
        [Test]
        public void SaveAndLoad_WhenCalledWithRealObject_GivesExpectedObject()
        {
            // Arrange
            var vf = new VersionFile();
            vf.AddVersion("Alpha", new Version("v0.1.1.5"), VersionField.Build);
            vf.AddVersion("Beta", new Version("v0.1.1.0"), VersionField.Feature);
            vf.AddVersion("Public", new Version("v0.1.0.0"), VersionField.Minor);
            var other = new VersionFile();

            // Act
            vf.Save(testfilename);
            other.Load(testfilename);

            // Assert
            Assert.That(vf.Versions, Is.EquivalentTo(other.Versions));
        }
        
        [Test]
        public void Load_WithMalformedLine_SkipsLine()
        {
            // Arrange
            var sw = new StreamWriter(testfilename);
            sw.WriteLine("Alpha: v1,1");
            sw.WriteLine("Beta: v2,egg");
            sw.WriteLine("Public: v2,3");
            sw.Close();
            var vf = new VersionFile();

            // Act
            vf.Load(testfilename);

            // Assert
            Assert.That(vf.Versions.Count(), Is.EqualTo(2));
        }

        #endregion

        #region Bump tests
        
        [Test]
        public void Bump_WhenBumpingSingleVersion_BumpsAsExpected()
        {
            // Arrange
            var vf = new VersionFile();
            vf.AddVersion("Test", new Version("v1.0.0.0"), VersionField.Build);

            // Act
            vf.Bump("Test");

            // Assert
            Assert.That(vf.Versions.First().Version.Build, Is.EqualTo(1));
        }

        [Test]
        public void Bump_WhenBumpingReleaseBundleAboveSomeOthers_OthersAreResetAlongWithIt()
        {
            // Arrange
            var vf = new VersionFile();
            vf.AddVersion("Master", new Version("v1.0.0.0"), VersionField.Major);
            vf.AddVersion("Public", new Version("v1.4.0.0"), VersionField.Minor);
            vf.AddVersion("Beta", new Version("v1.4.12.0"), VersionField.Feature);
            vf.AddVersion("Alpha", new Version("v1.4.12.28"), VersionField.Build);

            // Act
            var res = vf.Bump("Master");

            // Assert
            Assert.That(res, Is.True);
            Assert.That(vf.Versions.Single(v => v.Name == "Master").Version.ToString(), Is.EqualTo("v2"), "Master not bumped");
            Assert.That(vf.Versions.Single(v => v.Name == "Public").Version.ToString(), Is.EqualTo("v2"), "Public not bumped");
            Assert.That(vf.Versions.Single(v => v.Name == "Beta").Version.ToString(), Is.EqualTo("v2"), "Beta not bumped");
            Assert.That(vf.Versions.Single(v => v.Name == "Alpha").Version.ToString(), Is.EqualTo("v2"), "Alpha not bumped");
        }
        
        [Test]
        public void Bump_WhenBundleNotfound_ReturnsFalse()
        {
            // Arrange
            var vf = new VersionFile();
            vf.AddVersion("Master", new Version("v1.0.0.0"), VersionField.Major);
            vf.AddVersion("Public", new Version("v1.4.0.0"), VersionField.Minor);
            vf.AddVersion("Beta", new Version("v1.4.12.0"), VersionField.Feature);
            vf.AddVersion("Alpha", new Version("v1.4.12.28"), VersionField.Build);

            // Act
            var res = vf.Bump("Frogger");

            // Assert
            Assert.That(res, Is.False);
        }

        #endregion
    }
}
