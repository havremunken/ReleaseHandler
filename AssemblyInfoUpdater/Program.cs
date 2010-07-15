using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReleaseHandlerLibrary;
using System.IO;
using System.Text.RegularExpressions;

namespace AssemblyInfoUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            var vf = new VersionFile();
            vf.Load("Version.ini");
            var ver = vf.Versions.SingleOrDefault(v => string.Compare(v.Name, args[0], true) == 0);
            if(ver==null)
            {
                Console.WriteLine("Not happy with release name {0}", args[0]);
                return;
            }
            Console.WriteLine("Updating all files to {0}", ver.Version.ToFullString());
            
            var ff = new FileFinder();

            Console.WriteLine("AssemblyInfo.cs files found in {0}:", Environment.CurrentDirectory);
            foreach (var file in ff.FindFiles(Environment.CurrentDirectory, "AssemblyInfo.cs"))
            {
                Console.WriteLine(" {0}", file);
                var fileContents = File.ReadAllText(file, Encoding.UTF8);
                // Replace stuff
                //var regex = new Regex("AssemblyVersion\\(\"\\d*\\.\\d*\\.\\d*\\.\\d*\\)");
                var newString = Regex.Replace(fileContents, "AssemblyVersion\\(\"\\d+\\.\\d+\\.\\d+\\.\\d+\"\\)", String.Format("AssemblyVersion(\"{0}\")", ver.Version.ToFullStringNoV()));
                newString = Regex.Replace(newString, "AssemblyFileVersion\\(\"\\d+\\.\\d+\\.\\d+\\.\\d+\"\\)", String.Format("AssemblyFileVersion(\"{0}\")", ver.Version.ToFullStringNoV()));
                if (newString == fileContents)
                    Console.WriteLine("Bah, humbug!");
                File.WriteAllText(file, newString, Encoding.UTF8);
            }
        }
    }
}
