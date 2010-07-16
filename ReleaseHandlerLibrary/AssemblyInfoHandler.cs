using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ReleaseHandlerLibrary
{
    public class AssemblyInfoHandler
    {
        #region Fields

        private string _fileName;
        private string _fileContents;
        private string _originalFileContents;

        #endregion

        #region Public properties

        public bool TextChanged
        {
            get
            {
                return _fileContents != _originalFileContents;
            }
        }

        public string Contents
        {
            get
            {
                return _fileContents;
            }
        }

        #endregion

        #region Constructor

        public AssemblyInfoHandler(string assemblyInfoPath)
        {
            _fileName = assemblyInfoPath;
            _originalFileContents = _fileContents = File.ReadAllText(assemblyInfoPath, Encoding.UTF8);
        }

        #endregion

        public void Save()
        {
            File.WriteAllText(_fileName, _fileContents, Encoding.UTF8);
        }

        public void SetVersion(Version version)
        {
            _fileContents = Regex.Replace(_fileContents, "AssemblyVersion\\(\"\\d+\\.\\d+\\.\\d+\\.\\d+\"\\)", String.Format("AssemblyVersion(\"{0}\")", version.ToFullStringNoV()));
            _fileContents = Regex.Replace(_fileContents, "AssemblyFileVersion\\(\"\\d+\\.\\d+\\.\\d+\\.\\d+\"\\)", String.Format("AssemblyFileVersion(\"{0}\")", version.ToFullStringNoV()));
        }
    }
}
