using System;
using System.IO;
using NUnit.Framework;

namespace XmlToBullet.Tests
{
    [TestFixture]
    public class ConvertTest
    {
        [Test]
        [TestCase("<doc></doc>", "* doc\r\n")]
        [TestCase("<doc><item></item></doc>", "* doc\r\n"
                                            + "  * item\r\n")]
        [TestCase("<doc><stringitem>hello</stringitem></doc>", "* doc\r\n"
                                                             + "  * stringitem e.g. \"hello\"\r\n")]
        [TestCase("<doc><item attrib=\"123\"></item></doc>", "* doc\r\n"
                                                           + "  * item\r\n"
                                                           + "    * attrib e.g. \"123\"\r\n")]
        [TestCase("<doc><item attrib=\"123\"></item><item attrib=\"456\"></item></doc>", "* doc\r\n"
                                                                                       + "  * item (...many...)\r\n"
                                                                                       + "    * attrib e.g. \"123\"\r\n")]

        [TestCase("<doc><item><list /></item><item><list><listitem /></list></item></doc>", 
                                                                                         "* doc\r\n"
                                                                                       + "  * item (...many...)\r\n"
                                                                                       + "    * list\r\n"
                                                                                       + "      * listitem\r\n")]
        [TestCase("<doc><item></item><item attribute=\"hasattr\"></item></doc>",
                                                                                         "* doc\r\n"
                                                                                       + "  * item (...many...)\r\n"
                                                                                       + "    * attribute e.g. \"hasattr\"\r\n")]
        [TestCase("<doc><item attribute=\"\"></item><item attribute=\"hasattr\"></item></doc>",
                                                                                         "* doc\r\n"
                                                                                       + "  * item (...many...)\r\n"
                                                                                       + "    * attribute e.g. \"hasattr\"\r\n")]

        [TestCase("<doc><item><list><listitem /><listitem /><listitem /><listitem /></list></item></doc>",
                                                                                         "* doc\r\n"
                                                                                       + "  * item\r\n"
                                                                                       + "    * list\r\n"
                                                                                       + "      * listitem (...many...)\r\n")]
        [TestCase("<doc xmlns=\"http://www.foo.bar/Schemas/baz\" xmlns:vis=\"bazSchema\"><item /></doc>", "* doc\r\n"
                                                                                                        + "  * xmlns e.g. \"http://www.foo.bar/Schemas/baz\"\r\n"
                                                                                                        + "  * vis e.g. \"bazSchema\"\r\n"
                                                                                                        + "  * item\r\n")]
        public void ConvertNode(string input, string expected)
        {
            var sut = new XmlConverter();
            var output = sut.Convert(input);
            Assert.AreEqual(expected, output);
        }

        [Test]
        [TestCase("<doc></doc>", "* doc\r\n")]
        [TestCase("<doc><item></item></doc>", "* doc\r\n"
                                            + "  * item\r\n")]
        [TestCase("<doc><item attrib=\"123\"></item></doc>", "* doc\r\n"
                                                           + "  * item\r\n")]

        [TestCase("<doc><item><list /></item><item><list><listitem /></list></item></doc>",
                                                                                         "* doc\r\n"
                                                                                       + "  * item (...many...)\r\n"
                                                                                       + "    * list\r\n"
                                                                                       + "      * listitem\r\n")]
        [TestCase("<doc><item></item><item attribute=\"hasattr\"></item></doc>",
                                                                                         "* doc\r\n"
                                                                                       + "  * item (...many...)\r\n")]

        [TestCase("<doc xmlns=\"http://www.foo.bar/Schemas/baz\" xmlns:vis=\"bazSchema\"><item /></doc>", "* doc\r\n"
                                                                                                        + "  * item\r\n")]
        public void ConvertNodeWithoutAttributes(string input, string expected)
        {
            var sut = new XmlConverter(attributeBullet: null);
            var output = sut.Convert(input);
            Assert.AreEqual(expected, output);
        }

        [Test]
        [TestCase("<doc><item attrib=\"123\"></item></doc>", "* doc\r\n"
                                                           + "  * item\r\n"
                                                           + "    + attrib e.g. \"123\"\r\n")]
        [TestCase("<doc><item attrib=\"123\"></item><item attrib=\"456\"></item></doc>", "* doc\r\n"
                                                                                       + "  * item (...many...)\r\n"
                                                                                       + "    + attrib e.g. \"123\"\r\n")]
        [TestCase("<doc xmlns=\"http://www.foo.bar/Schemas/baz\" xmlns:vis=\"bazSchema\"><item /></doc>", "* doc\r\n"
                                                                                                        + "  + xmlns e.g. \"http://www.foo.bar/Schemas/baz\"\r\n"
                                                                                                        + "  + vis e.g. \"bazSchema\"\r\n"
                                                                                                        + "  * item\r\n")]
        public void ConvertNodeWithCustomAttributeBullet(string input, string expected)
        {
            var sut = new XmlConverter(attributeBullet: "+");
            var output = sut.Convert(input);
            Assert.AreEqual(expected, output);
        }
    }
}
