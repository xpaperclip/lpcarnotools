using System;
using System.IO;
using System.Net;
using System.Xml.Linq;
using LxTools;
using LxTools.Carno;
using LxTools.Liquipedia;

namespace LpCarno
{
    static class Program
    {
        static void MainTest()
        {
            DataStore data = new DataStore();
            DataStore.LoadRewriter("playerpka.dict", data.IdRewriter);
            DataStore.LoadRewriter("mapakas.dict", data.MapRewriter);
            
            data.Accumulate("http://wiki.teamliquid.net/starcraft2/2013_Global_StarCraft_II_Team_League_Season_2/Round_1");

            PageGenerator pagegen = PageGenerator.FromXml(System.Xml.Linq.XDocument.Load("pages/Team League (Winners League).xml"));
            string emit = pagegen.Emit(data);

            // push update

            UI.ShowDialog(new UIDocument("emit", emit),
                new UIDocument("html", LiquipediaClientEx.RequestParse(emit)),
                new UIDocument("xml", pagegen.Save().ToString()));
        }

        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new MainForm());
        }
    }
}
