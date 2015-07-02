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

        [Test]
        public void Two_arguments_defaults()
        {
            AppArguments args = AppArguments.From(new[] {"In.xml", "out.txt"});
            Assert.IsFalse(args.ShowHelp);
            Assert.AreEqual("In.xml", args.InPath);
            Assert.AreEqual("out.txt", args.OutPath);
            Assert.IsTrue(args.ShowAttributes);
            Assert.AreEqual("+", args.AttibuteBullet);
        }

        [Test]
        public void Disable_attributes()
        {
            AppArguments args = AppArguments.From(new[] {"In.xml", "out.txt", "-noAttributes"});
            Assert.IsFalse(args.ShowAttributes);
            Assert.IsNull(args.AttibuteBullet);
        }

        [Test]
        public void Specify_bullet()
        {
            AppArguments args = AppArguments.From(new[] {"In.xml", "out.txt", "-a=#"});
            Assert.IsTrue(args.ShowAttributes);
            Assert.AreEqual("#", args.AttibuteBullet);
        }
    }
}