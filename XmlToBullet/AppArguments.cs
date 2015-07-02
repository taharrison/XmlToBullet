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

            var showHelp = (nonSwitches.Length != 1 && nonSwitches.Length != 2) || args.Contains("-help");

            if (showHelp)
            {
                return new AppArguments {ShowHelp = true};
            }

            var noAttributesOption = switches.FirstOrDefault(a => a.StartsWith("-noAttributes"));
            var attributeOption = switches.FirstOrDefault(a => a.StartsWith("-a="));

            var attributeBullet = noAttributesOption != null
                ? null
                : (String.IsNullOrEmpty(attributeOption) ? "+" : attributeOption.Substring(3));

            return new AppArguments
            {
                ShowHelp = false,
                InPath = nonSwitches.First(),
                OutPath = nonSwitches.Skip(1).FirstOrDefault(),
                ShowAttributes = noAttributesOption == null,
                AttributeBullet = attributeBullet
            };
        }

        public bool ShowHelp { get; private set; }
        public string InPath { get; private set; }
        public string OutPath { get; private set; }
        public bool ShowAttributes { get; private set; }
        public string AttributeBullet { get; private set; }
    }
}