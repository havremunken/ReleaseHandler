using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ReleaseHandlerLibrary
{
    public class VersionFile
    {
        #region Fields

        private List<ReleaseBundle> _versions;

        #endregion

        #region Public properties

        public IEnumerable<ReleaseBundle> Versions
        {
            get
            {
                return _versions;
            }
        }

        #endregion

        #region Constructors

        public VersionFile()
        {
            _versions = new List<ReleaseBundle>();
        }

        #endregion

        #region Public methdos

        public void AddVersion(string name, Version version, VersionField fieldToBump)
        {
            _versions.Add(new ReleaseBundle { Name = name, Version = version, Field = fieldToBump });
        }

        public bool Bump(string releaseName)
        {
            var bundle = _versions.SingleOrDefault(t => String.Compare(t.Name, releaseName, true) == 0);
            if (bundle == null)
                return false;

            bundle.Version.Bump(bundle.Field);

            // Sett alle som er "under" denne på rangstigen til samme versjon
            var toUpgrade = from t in _versions
                            where t.Field > bundle.Field
                            select t;

            foreach (var t in toUpgrade)
            {
                t.Version = bundle.Version;
            }

            return true;
        }

        public Version GetVersionByName(string versionName)
        {
            var bundle = _versions.SingleOrDefault(v => string.Compare(v.Name, versionName, true) == 0);
            return bundle == null ? null : bundle.Version;
        }

        public void Save(string filename)
        {
            var file = new StreamWriter(filename);

            foreach (var bundle in _versions)
            {
                file.WriteLine("{0}: {1},{2}", bundle.Name, bundle.Version.ToString(), (int)bundle.Field);
            }

            file.Close();
        }

        public void Load(string filename)
        {
            _versions = new List<ReleaseBundle>();

            var file = new StreamReader(filename);
            string line;

            while((line = file.ReadLine()) != null)
            {
                var namesplit = line.Split(":".ToCharArray());
                var versplit = namesplit[1].Trim().Split(",".ToCharArray());
                int field=0;
                if(!int.TryParse(versplit[1], out field))
                    continue;

                AddVersion(namesplit[0], new Version(versplit[0]), (VersionField)field); 
            }

            file.Close();
        }

        #endregion
    }
}
