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
            var ver = vf.GetVersionByName(args[0]);
            if(ver==null)
            {
                Console.WriteLine("Not happy with release name {0}", args[0]);
                return;
            }
            Console.WriteLine("Updating all files to version {0}", ver.ToFullStringNoV());
            
            var ff = new FileFinder();

            Console.WriteLine("Updating AssemblyInfo.cs files found in {0}:", Environment.CurrentDirectory);

            foreach (var file in ff.FindFiles(Environment.CurrentDirectory, "AssemblyInfo.cs"))
            {
                Console.WriteLine(" {0}", file);
                var assemblyInfo = new AssemblyInfoHandler(file);
                
                assemblyInfo.SetVersion(ver);

                if (!assemblyInfo.TextChanged)
                    Console.WriteLine("No change performed on file '{0}'", file);
                else
                    assemblyInfo.Save();
            }
        }
    }
}
