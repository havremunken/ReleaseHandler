using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReleaseHandlerLibrary;

namespace VersionBumper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Need name of version to bump");
                return;
            }

            var versionFile = new VersionFile();
            versionFile.Load("Version.ini");

            versionFile.Bump(args[0]);

            versionFile.Save("Version.ini");
        }
    }
}
