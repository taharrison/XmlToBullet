using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XmlToBullet
{
    public class XmlConverter
    {
        private readonly string _attributeBullet;

        public XmlConverter(string attributeBullet = "*")
        {
            _attributeBullet = attributeBullet;
        }

        private class Element
        {
            public bool IsList { get; set; }
            public XElement Item { get; set; }
        }

        public string Convert(string xml)
        {
            var doc = XElement.Parse(xml);
            var builder = new StringBuilder();

            AddNodesRecursively(builder, 0, new List<Element> { new Element() { IsList = false, Item = doc }});

            return builder.ToString();
        }

        private void AddNodesRecursively(StringBuilder builder, int i, List<Element> elementsItems)
        {
            var elements = elementsItems.Select(x => x.Item);

            var uniqueChildren = elements
                .SelectMany(x => x.Elements().GroupBy(e => e.Name).Select(g => new { Group = g, IsList = g.Count() > 1 }))
                .SelectMany(g => g.Group.Select(gr => new Element() { Item = gr, IsList = g.IsList }))
                .GroupBy(x => x.Item.Name);

            var hasNoChildren = uniqueChildren.Count() == 0;
            var exampleValue = hasNoChildren && !String.IsNullOrWhiteSpace(elementsItems.First().Item.Value)
                ? " e.g. \"" + elementsItems.First().Item.Value + "\""
                : "";

            builder.AppendLine(new string(' ', i) + "* " + elementsItems.First().Item.Name.LocalName + (elementsItems.Max(e => e.IsList) ? " (...many...)" : "")
                + exampleValue);


            if (_attributeBullet != null)
            {
                var attributes = elements
                    .SelectMany(x => x.Attributes())
                    .GroupBy(x => x.Name)
                    .Select(x => new
                    {
                        x.OrderBy(a => a.Value == "" || a.Value == null).First().Name,
                        x.OrderBy(a => a.Value == "" || a.Value == null).First().Value
                    });
                foreach (var attribute in attributes)
                {
                    builder.AppendLine(new string(' ', i + 2) + _attributeBullet + " " + attribute.Name.LocalName +
                                       " e.g. \"" + attribute.Value + "\"");
                }
            }

            foreach (var child in uniqueChildren)
            {
                AddNodesRecursively(builder, i + 2, child.ToList());
            }
        }
    }
}
