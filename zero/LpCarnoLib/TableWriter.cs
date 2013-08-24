using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CarnoZ
{
    public static class TableWriter
    {
        public static string WriteTable<T>(string template, string rowtemplate, IEnumerable<T> table)
        {
            return WriteTable(template, rowtemplate, new object(), table);
        }
        public static string WriteTable<S, T>(string template, string rowtemplate, S val, IEnumerable<T> table)
        {
            StringBuilder sb = new StringBuilder();
            
            var fields = new Dictionary<string, PropertyInfo>();
            foreach (var fi in typeof(T).GetProperties())
            {
                fields.Add(fi.Name, fi);
            }

            foreach (var rowdata in table)
            {
                string row = rowtemplate;
                foreach (var kvp in fields)
                {
                    row = row.Replace("<? " + kvp.Key + " ?>", kvp.Value.GetValue(rowdata, null).ToString());
                }
                sb.AppendLine(row);
            }

            string body = template;
            body = template.Replace("<? rows ?>", sb.ToString());

            foreach (var fi in typeof(S).GetProperties())
            {
                body = body.Replace("<? " + fi.Name + " ?>", fi.GetValue(val, null).ToString());
            }

            return body;
        }
    }
}
