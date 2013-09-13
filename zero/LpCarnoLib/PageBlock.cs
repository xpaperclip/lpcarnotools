using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace LxTools.Carno
{
    public abstract class PageBlock
    {
        private readonly Dictionary<string, string> _params = new Dictionary<string,string>();

        public string GetParam(string param)
        {
            return GetParam(param, "");
        }
        public string GetParam(string param, string defaultValue)
        {
            return _params.GetValueOrDefault(param, defaultValue);
        }
        public bool GetParamBoolean(string param)
        {
            return GetParamBoolean(param, false);
        }
        public bool GetParamBoolean(string param, bool defaultValue)
        {
            bool result;
            if (bool.TryParse(GetParam(param), out result))
                return result;
            else
                return defaultValue;
        }
        public int GetParamInt(string param)
        {
            return GetParamInt(param, 0);
        }
        public int GetParamInt(string param, int defaultValue)
        {
            int result;
            if (int.TryParse(GetParam(param), out result))
                return result;
            else
                return defaultValue;
        }

        public abstract XNode Save();
        public abstract void Emit(TextWriter tw, DataStore data);
    }

    public class UnknownXNodeBlock : PageBlock
    {
        public UnknownXNodeBlock(XNode node)
        {
            this._node = node;
        }

        private XNode _node;
        public XNode Node
        {
            get { return _node; }
        }

        public override XNode Save()
        {
            return this.Node;
        }
        public override void Emit(TextWriter tw, DataStore data)
        {
            tw.Write(this.Node.ToString());
        }
    }

    public class TextBlock : PageBlock
    {
        public TextBlock() : this(string.Empty) { }
        public TextBlock(string text)
        {
            this.Text = text;
        }

        public string Text { get; set; }

        public override XNode Save()
        {
            return new XText(this.Text);
        }

        public override void Emit(TextWriter tw, DataStore data)
        {
            tw.Write(this.Text);
        }
    }

    public abstract class CarnoBlock : PageBlock
    {
        public string TypeName
        {
            get { return this.GetType().Name; }
        }

        public override XNode Save()
        {
            return new XElement(this.TypeName);
        }

        public override sealed void Emit(TextWriter tw, DataStore data)
        {
            //tw.WriteLine("<!--[carno:{0}]-->", this.TypeName);
            EmitInternal(tw, data);
            //tw.Write("<!--[/carno:{0}]-->", this.TypeName);
        }
        protected abstract void EmitInternal(TextWriter tw, DataStore data);
    }
}
