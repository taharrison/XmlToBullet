using NUnit.Framework;

namespace XmlToBullet.Tests
{
    [TestFixture]
    public sealed class AppArgumentTests
    {
        [TestCase("")]
        [TestCase("-a=# -noAttribute")]
        [TestCase("In.xml out.txt -help")]
        [TestCase("In.xml out.txt thirdarg")]
        public void Should_show_help(string commandLine)
        {
            AppArguments args = AppArguments.From(commandLine.Split(' '));
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

        [TestCase("In.xml out.txt")]
        [TestCase("In.xml out.txt -a=#")]
        [TestCase("-a# In.xml out.txt")]
        [TestCase("In.xml -a=# out.txt")]
        public void Paths(string commandLine)
        {
            AppArguments args = AppArguments.From(commandLine.Split(' '));
            Assert.AreEqual("In.xml", args.InPath);
            Assert.AreEqual("out.txt", args.OutPath);
        }

        [TestCase("In.xml out.txt -noAttributes")]
        [TestCase("-noAttributes In.xml out.txt")]
        [TestCase("In.xml -noAttributes out.txt")]
        public void Disable_attributes(string commandLine)
        {
            AppArguments args = AppArguments.From(commandLine.Split(' '));
            Assert.IsFalse(args.ShowAttributes);
            Assert.IsNull(args.AttibuteBullet);
        }

        [TestCase("In.xml out.txt -a=#")]
        [TestCase("-a=# In.xml out.txt")]
        [TestCase("In.xml -a=# out.txt")]
        public void Specify_bullet(string commandLine)
        {
            AppArguments args = AppArguments.From(commandLine.Split(' '));
            Assert.IsTrue(args.ShowAttributes);
            Assert.AreEqual("#", args.AttibuteBullet);
        }
    }
}