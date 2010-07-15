using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReleaseHandlerLibrary
{
    public enum VersionField
    {
        Major = 1,
        Minor = 2,
        Feature = 3,
        Build = 4
    }

    public class Version
    {
        #region Public properties

        public int Major { get; private set; }
        public int Minor { get; private set; }
        public int Feature { get; private set; }
        public int Build { get; private set; }

        #endregion

        #region Constructors

        public Version()
        {
            // I'm a pedant.

            Major = 0;
            Minor = 0;
            Feature = 0;
            Build = 0;
        }

        /// <summary>
        /// Create a new Version object from a string
        /// </summary>
        /// <param name="versionString">The version in this format: [v]M[.m[.f[.b]]] - stuff in brackets optional</param>
        public Version(string versionString)
            : this()
        {
            ReadVersionString(versionString);
        }

        #endregion

        #region Public methods

        public void Bump(VersionField versionField)
        {
            switch (versionField)
            { 
                case VersionField.Major:
                    Major++;
                    Minor = 0;
                    Feature = 0;
                    Build = 0;
                    break;
                case VersionField.Minor:
                    Minor++;
                    Feature = 0;
                    Build = 0;
                    break;
                case VersionField.Feature:
                    Feature++;
                    Build = 0;
                    break;
                case VersionField.Build:
                    Build++;
                    break;
            }
        }

        public string ToFullString()
        {
            return "v" + Major + "." + Minor + "." + Feature + "." + Build;
        }

        public string ToFullStringNoV()
        {
            return Major + "." + Minor + "." + Feature + "." + Build;
        }

        public override string ToString()
        {
            if(Build!=0)
                return "v" + Major + "." + Minor + "." + Feature + "." + Build;
            if(Feature!=0)
                return "v" + Major + "." + Minor + "." + Feature;
            if(Minor!=0)
                return "v" + Major + "." + Minor;
            return "v" + Major;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Version;
            if (other == null)
                return false;

            return ToString() == other.ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        #endregion

        #region Private methods

        private void ReadVersionString(string versionString)
        {
            var vstring = versionString[0] == 'v' ? versionString.Substring(1) : versionString;

            var split = vstring.Split(".".ToCharArray());
            int storage = 0;

            if (!int.TryParse(split[0], out storage))
                throw new ArgumentException("Malformed version string");
            Major = storage;

            if (split.Length == 1)
                return;

            if (!int.TryParse(split[1], out storage))
                throw new ArgumentException("Malformed version string");
            Minor = storage;

            if (split.Length == 2)
                return;

            if (!int.TryParse(split[2], out storage))
                throw new ArgumentException("Malformed version string");
            Feature = storage;

            if (split.Length == 3)
                return;

            if (!int.TryParse(split[3], out storage))
                throw new ArgumentException("Malformed version string");
            Build = storage;
        }

        #endregion
    }
}
