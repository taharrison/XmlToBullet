using System;
using System.Linq;

namespace XmlToBullet
{
    public sealed class AppArguments
    {
        private AppArguments()
        {
        }

        public static AppArguments From(string[] args)
        {
            var nonSwitches = args.Where(a => !a.StartsWith("-")).ToArray();
            var switches = args.Where(a => a.StartsWith("-")).ToArray();

            var showHelp = nonSwitches.Count() != 2 || args.Contains("-help");

            if (showHelp)
            {
                return new AppArguments {ShowHelp = true};
            }

            string filePath = nonSwitches[0];
            string fileOutPath = nonSwitches[1];

            var noAttributesOption = switches.FirstOrDefault(a => a.StartsWith("-noAttributes"));
            var attributeOption = switches.FirstOrDefault(a => a.StartsWith("-a="));

            var attibuteBullet = noAttributesOption != null
                ? null
                : (String.IsNullOrEmpty(attributeOption) ? "+" : attributeOption.Substring(3));

            return new AppArguments
            {
                ShowHelp = false,
                InPath = filePath,
                OutPath = fileOutPath,
                ShowAttributes = noAttributesOption == null,
                AttibuteBullet = attibuteBullet
            };
        }

        public bool ShowHelp { get; private set; }
        public string InPath { get; private set; }
        public string OutPath { get; private set; }
        public bool ShowAttributes { get; private set; }
        public string AttibuteBullet { get; private set; }
    }
}