using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GetFileVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Need exactly two arguments - the file to extract the version from, and the output file");
                return;
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("File does not exist");
                return;
            }
            if (String.IsNullOrWhiteSpace(args[1]))
            {
                Console.WriteLine("Destination file must be a file path");
                return;
            }
            Console.WriteLine("File is ", args[0]);
            var info = System.Diagnostics.FileVersionInfo.GetVersionInfo(args[0]);
            var file = new StreamWriter(args[1]);
            file.WriteLine("!define Version \"v{0}\"", info.ProductVersion);
            file.Close();
        }
    }
}
