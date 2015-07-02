using NUnit.Framework;

namespace XmlToBullet.Tests
{
    [TestFixture]
    public sealed class AppArgumentTests
    {
        [Test]
        public void Empty_should_show_help()
        {
            AppArguments args = AppArguments.From(new string[0]);
            Assert.IsTrue(args.ShowHelp);
        }

        [TestCase("In.xml out.txt")]
        public void Two_arguments_defaults(string commandLine)
        {
            AppArguments args = AppArguments.From(commandLine.Split(' '));
            Assert.IsFalse(args.ShowHelp);
            Assert.AreEqual("In.xml", args.InPath);
            Assert.AreEqual("out.txt", args.OutPath);
            Assert.IsTrue(args.ShowAttributes);
            Assert.AreEqual("+", args.AttibuteBullet);
        }

        [TestCase("In.xml out.txt -noAttributes")]
        public void Disable_attributes(string commandLine)
        {
            AppArguments args = AppArguments.From(commandLine.Split(' '));
            Assert.IsFalse(args.ShowAttributes);
            Assert.IsNull(args.AttibuteBullet);
        }

        [TestCase("In.xml out.txt -a=#")]
        public void Specify_bullet(string commandLine)
        {
            AppArguments args = AppArguments.From(commandLine.Split(' '));
            Assert.IsTrue(args.ShowAttributes);
            Assert.AreEqual("#", args.AttibuteBullet);
        }
    }
}