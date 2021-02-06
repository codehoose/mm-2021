using ManicMiner.Converter.Lib;
using System;
using System.IO;

namespace ManicMiner.Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            string map = args[0];
            string output = args[1];

            var mapParser = new MapParser(map);
            var json = mapParser.Parse();

            if (File.Exists(output))
            {
                File.Delete(output);
            }

            File.WriteAllText(output, json);
            Console.WriteLine("Job's done!");
        }
    }
}
