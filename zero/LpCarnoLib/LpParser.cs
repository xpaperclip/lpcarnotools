using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LxTools.CarnoZ
{
    class LpParser
    {
        public static List<LpItem> Parse(string s, int start, out int idx)
        {
            return ParseTempl(s, start, out idx, true).Children;
        }
        public static LpComment ParseComment(string s, int start, out int idx)
        {
            idx = start;
            int closingidx = s.IndexOf("-->", idx);
            if (closingidx < 0)
            {
                LpComment comment = new LpComment(s.Substring(idx));
                idx = s.Length;
                return comment;
            }
            else
            {
                LpComment comment = new LpComment(s.Substring(idx, closingidx - idx));
                idx = closingidx + 3;
                return comment;
            }
        }
        public static LpTemplate ParseTempl(string s, int start, out int idx, bool rootlevel)
        {
            idx = start;
            LpTemplate template = new LpTemplate();
            List<LpItem> items = template.Children;
            while (idx < s.Length)
            {
                int commentopenindex = s.IndexOf("<!--", idx);
                int templatecloseindex = rootlevel ? -1 : s.IndexOf("}}", idx);
                int templateopenindex = s.IndexOf("{{", idx);
                if ((commentopenindex >= 0) && ((templatecloseindex < 0) || (commentopenindex < templatecloseindex)) && ((templateopenindex < 0) || (commentopenindex < templateopenindex)))
                {
                    string text = s.Substring(idx, commentopenindex - idx).TrimWhitespace();
                    if (!string.IsNullOrEmpty(text))
                        items.Add(new LpText(text));
                    items.Add(ParseComment(s, commentopenindex + 4, out idx));
                }
                else if ((templateopenindex < 0) || ((templatecloseindex >= 0) && (templatecloseindex < templateopenindex)))
                {
                    if (templatecloseindex < 0)
                    {
                        string text = s.Substring(idx).TrimWhitespace();
                        if (!string.IsNullOrEmpty(text))
                            items.Add(new LpText(text));
                        idx = s.Length;
                    }
                    else
                    {
                        string text = s.Substring(idx, templatecloseindex - idx).TrimWhitespace();
                        if (!string.IsNullOrEmpty(text))
                            items.Add(new LpText(text));
                        idx = templatecloseindex + 2;
                        break;
                    }
                }
                else if (templateopenindex == idx)
                {
                    items.Add(ParseTempl(s, templateopenindex + 2, out idx, false));
                }
                else
                {
                    string text = s.Substring(idx, templateopenindex - idx);
                    if (!string.IsNullOrEmpty(text))
                        items.Add(new LpText(text));
                    items.Add(ParseTempl(s, templateopenindex + 2, out idx, false));
                }
            }

            // now process into params
            if (!rootlevel)
            {
                List<LpItem> currentParam = new List<LpItem>();
                int unnamed = 0;
                string paramName = "0";
                Queue<LpItem> remainingItems = new Queue<LpItem>(template.Children);
                while (remainingItems.Count > 0)
                {
                    LpItem item = remainingItems.Dequeue();
                    if (item is LpText)
                    {
                        string text = (item as LpText).Text;
                        int baridx;
                        while ((baridx = text.IndexOf('|')) >= 0)
                        {
                            // add up to the |
                            string tt = text.Substring(0, baridx).TrimWhitespace();
                            if (!string.IsNullOrEmpty(tt))
                                currentParam.Add(new LpText(tt));
                            template.Params[paramName] = currentParam;
                            currentParam = new List<LpItem>();

                            text = text.Substring(baridx + 1);
                            int nextbar = text.IndexOf('|');
                            int equalsidx;
                            if (nextbar > 0)
                                equalsidx = text.IndexOf('=', 0, nextbar);
                            else
                                equalsidx = text.IndexOf('=');
                            if (equalsidx < 0)
                            {
                                unnamed++;
                                paramName = unnamed.ToString();
                            }
                            else
                            {
                                paramName = text.Substring(0, equalsidx).TrimWhitespace();
                                text = text.Substring(equalsidx + 1);
                            }
                        }
                        text = text.TrimWhitespace();
                        if (text.Length > 0) currentParam.Add(new LpText(text));
                    }
                    else
                    {
                        currentParam.Add(item);
                    }
                }
                if (currentParam.Count != 0)
                {
                    template.Params[paramName] = currentParam;
                }
            }
            return template;
        }
    }

    class LpItem { }
    class LpText : LpItem
    {
        public LpText(string Text)
        {
            this.Text = Text;
        }
        public string Text;

        public override string ToString()
        {
            return this.Text;
        }
    }
    class LpComment : LpItem
    {
        public LpComment(string Text)
        {
            this.Text = Text;
        }
        public string Text;

        public override string ToString()
        {
            return this.Text;
        }
    }
    class LpTemplate : LpItem
    {
        public readonly List<LpItem> Children = new List<LpItem>();
        public readonly Dictionary<string, List<LpItem>> Params = new Dictionary<string, List<LpItem>>();

        public string Name
        {
            get
            {
                if (Params.ContainsKey("0"))
                {
                    var p = Params["0"];
                    if ((p.Count > 0) && (p[0] is LpText))
                        return (p[0] as LpText).Text;
                }
                return "";
            }
        }

        public string GetParamText(string label)
        {
            if (label == null || !Params.ContainsKey(label)) return null;
            return (from item in Params[label]
                    where item is LpText
                    select (item as LpText).Text).FirstOrDefault();
        }
        public LpTemplate GetParamTemplate(string label, string template)
        {
            if (!Params.ContainsKey(label)) return null;
            return (from item in Params[label]
                    where item is LpTemplate
                    let templ = item as LpTemplate
                    where templ.Name == template
                    select templ).First();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}