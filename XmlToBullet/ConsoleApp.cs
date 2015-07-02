using System;
using System.IO;

namespace XmlToBullet
{
    public class ConsoleApp
    {
        public static void Main(string[] args)
        {
            const string helptext = @"Usage:
	XmlToBullet.exe <inputpath> <outputpath>

Options:
	-a=<attribute bullet point symbol>      specify the bullet point used for attributes (default '+')
	-noAttributes                           do not show attributes at all
	-help                                   show helptext";

            var appArgs = AppArguments.From(args);

            if (appArgs.ShowHelp)
            {
                Console.WriteLine(helptext);
                return;
            }

            var text = File.ReadAllText(appArgs.InPath);

            var asBullets = new XmlConverter(appArgs.AttibuteBullet).Convert(text);

            File.WriteAllText(appArgs.OutPath, asBullets);
        }
    }
}