using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ReleaseHandlerLibrary
{
    public class FileFinder
    {
        public IEnumerable<string> FindFiles(string path, string fileSpecification)
        {
            if (String.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
                throw new ArgumentException("Bah!");

            var filelist = new List<string>();

            try
            {
                var files = Directory.GetFileSystemEntries(path, fileSpecification, SearchOption.AllDirectories);
                foreach (var file in files)
                    filelist.Add(file);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return filelist;
        }
    }
}
