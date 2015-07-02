using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var showHelp = args.Count() < 2 | args.Contains("-help");

            if (showHelp)
            {
                Console.WriteLine(helptext);
                return;
            }


            string filePath = args[0];
            string fileOutPath = args[1];
            var otherargs = args.Skip(2).ToArray();

            var noAttributesOption = otherargs.FirstOrDefault(a => a.StartsWith("-noAttributes"));
            var attributeOption = otherargs.FirstOrDefault(a => a.StartsWith("-a="));

            var attibuteBullet = noAttributesOption != null ? (string) null
                : (String.IsNullOrEmpty(attributeOption) ? "+" : attributeOption.Substring(3));

            var text = File.ReadAllText(filePath);

            var asBullets = new XmlConverter(attibuteBullet).Convert(text);

            File.WriteAllText(fileOutPath, asBullets);
        }
    }
}
