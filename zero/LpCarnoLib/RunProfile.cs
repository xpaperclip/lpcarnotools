using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace LxTools.Carno
{
    public class RunProfile
    {
        public static string Execute(string profilePath)
        {
            XDocument xml = XDocument.Load(profilePath);
            
            DataStore data = new DataStore();
            DataStore.LoadRewriter("playerpka.dict", data.IdRewriter);
            DataStore.LoadRewriter("mapakas.dict", data.MapRewriter);

            // load additional conformance rules
            foreach (XElement xe in xml.Root.Elements("Map"))
            {
                string target = xe.Attribute("To").Value;
                if (xe.Attribute("Map") != null)
                    data.MapRewriter[xe.Attribute("Map").Value] = target;
                if (xe.Attribute("Player") != null)
                    data.IdRewriter[xe.Attribute("Player").Value] = target;
                if (xe.Attribute("Team") != null)
                    data.IdRewriter[xe.Attribute("Team").Value] = target;
            }

            // read participant lists if they exist
            foreach (XElement xe in xml.Root.Elements("Participants"))
            {
                string page = xe.Attribute("Page").Value;
                data.AccumulateParticipants(page);
            }

            // process pages
            foreach (XElement xe in xml.Root.Elements("Matches"))
            {
                string page = xe.Attribute("Page").Value;

                // read placements map
                var placementMap = new Dictionary<string, Placement>();
                foreach (XElement xeMap in xe.Elements("Map"))
                {
                    string finish = xeMap.Attribute("Finish").Value;
                    string pbg = xeMap.Attribute("Placement").Value;
                    string points = xeMap.Attribute("Points").Value;
                    var sort = xeMap.Attribute("Sort");

                    if (sort == null)
                        placementMap[finish] = new Placement(pbg, points);
                    else
                        placementMap[finish] = new Placement(pbg, points, int.Parse(sort.Value));
                }

                // process data
                try
                {
                    data.PlacementMap = placementMap;
                    data.Accumulate(page);
                }
                catch
                {
                    // just swallow errors for now
                }
            }

            // emit
            string pageGenTemplate = xml.Root.Element("PageGenTemplate").Attribute("Template").Value;
            PageGenerator pagegen = PageGenerator.FromXml(XDocument.Load("pages/" + pageGenTemplate));
            return pagegen.Emit(data);
        }
    }
}
