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

            var tuple = versionFile.Versions.SingleOrDefault(t => String.Compare(t.Name,args[0], true) == 0);
            if (tuple == null)
            {
                Console.WriteLine("Unknown version '{0}'", args[0]);
                return;
            }

            tuple.Version.Bump(tuple.Field);

            // Sett alle som er "under" denne på rangstigen til samme versjon
            var toUpgrade = from t in versionFile.Versions
                            where t.Field > tuple.Field
                            select t;

            foreach (var t in toUpgrade)
            {
                t.Version = tuple.Version;
            }

            versionFile.Save("Version.ini");
        }
    }
}
