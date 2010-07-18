using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReleaseHandlerLibrary;

namespace CheckReleaseName
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length!=1)
            {
                Console.WriteLine("Need release type name to check for");
                Environment.ExitCode = 1;
                return;
            }

            var versionFile = new VersionFile();
            versionFile.Load("Version.ini");

            var ver = versionFile.GetVersionByName(args[0]);
            if (ver == null)
                Console.WriteLine("Unknown");
            else
                Console.WriteLine("OK");
            return;
        }
    }
}
