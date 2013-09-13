using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace LxTools.Carno
{
    public class PageGenerator
    {
        private PageGenerator() { }
        public static PageGenerator FromXml(XDocument xml)
        {
            var pagegen = new PageGenerator();
            foreach (XAttribute attr in xml.Root.Attributes())
            {
                pagegen._params.Add("$" + attr.Name.LocalName, attr.Value);
            }
            foreach (XNode node in xml.Root.Nodes())
            {
                if (node is XText)
                {
                    pagegen.Blocks.Add(new TextBlock((node as XText).Value));
                }
                else if (node is XElement)
                {
                    XElement xe = node as XElement;
                    
                    // try to instantiate the PageSegment
                    try
                    {
                        // find the type
                        string xmlns = xe.Name.NamespaceName;
                        var xs = xmlns.SplitAt(";asm=");
                        string typename = xs.Item1 + "." + xe.Name.LocalName;
                        if (xs.Item2.Length > 0)
                            typename += ", " + xs.Item2;
                        Type type = Type.GetType(typename);

                        // try to instantiate it
                        PageBlock obj = Activator.CreateInstance(type) as PageBlock;
                        foreach (XAttribute xa in xe.Attributes())
                        {
                            PropertyInfo property = type.GetProperty(xa.Name.LocalName);
                            string value = xa.Value;
                            if (value.StartsWith("$"))
                                value = pagegen._params.GetValueOrDefault(xa.Value);
                            if (property != null)
                            {
                                // try and set it
                                if (property.PropertyType == typeof(int))
                                    property.SetValue(obj, int.Parse(value), null);
                                else if (property.PropertyType == typeof(bool))
                                    property.SetValue(obj, bool.Parse(value), null);
                                else
                                    property.SetValue(obj, value, null);
                            }
                            else
                            {
                                // ignore it?
                            }
                        }
                        pagegen.Blocks.Add(obj);
                    }
                    catch
                    {
                        pagegen.Blocks.Add(new UnknownXNodeBlock(node));
                    }
                }
                else
                {
                    // ignore it
                }
            }
            return pagegen;
        }

        private Dictionary<string, string> _params = new Dictionary<string, string>();
        private List<PageBlock> _blocks = new List<PageBlock>();
        public List<PageBlock> Blocks
        {
            get { return _blocks; }
        }

        public XDocument Save()
        {
            return new XDocument(
                new XElement(XName.Get("PageGenTemplate", "LxTools.Carno;asm=LpCarno"),
                    from segment in Blocks
                    select segment.Save()));
        }
        public string Emit(DataStore data)
        {
            using (StringWriter sw = new StringWriter())
            {
                foreach (var segment in Blocks)
                    segment.Emit(sw, data);

                return sw.ToString();
            }
        }
    }
}
