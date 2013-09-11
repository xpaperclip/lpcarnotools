using System;
using LxTools;
using LxTools.CarnoZ;

namespace LpCarno
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.Run(new MainForm());
        }

        static void Console()
        {
            CarnoServiceEventSink sink = new CarnoServiceEventSink();
            CarnoGenerator.LoadRewriter("playerpka.dict", sink.IdRewriter);
            CarnoGenerator.LoadRewriter("mapakas.dict", sink.MapRewriter);

            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2012-2013_Proleague/Round_1", "round1");
            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2012-2013_Proleague/Round_2", "round2");
            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2012-2013_Proleague/Round_3", "round3");
            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2012-2013_Proleague/Round_4", "round4");
            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2012-2013_Proleague/Round_5", "round5");
            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2012-2013_Proleague/Round_6", "round6");
            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2012-2013_Proleague/Playoffs", "po");

            //service.Accumulate("2013 WCS Season 2 Korea OSL/Premier/Ro32", "ro32");
            //service.Accumulate("2013 WCS Season 2 Korea OSL/Premier/Ro16", "ro16");
            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/index.php?title=2013_WCS_Season_2_Korea_OSL/Premier&action=edit&section=6", "po");
            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/index.php?title=2013_WCS_Season_2_Korea_OSL/Premier&action=edit&section=7", "pm");
            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2013_WCS_Season_2_Korea_OSL/Challenger", "ch");
            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2013_WCS_Season_3_Korea_GSL/Up_and_Down", "ud");

            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2013_WCS_Season_3_Korea_GSL/Premier/Ro32", "");

            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/Acer_TeamStory_Cup_Season_1/Group_Stage", "");
            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/Acer_TeamStory_Cup_Season_1/Playoffs", "");

            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2013_Global_StarCraft_II_Team_League_Season_1/Group_Stage", "gs");
            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2013_Global_StarCraft_II_Team_League_Season_1/Playoffs", "po");

            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2013_Global_StarCraft_II_Team_League_Season_2/Round_1", "r1");
            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2013_Global_StarCraft_II_Team_League_Season_2/Round_2", "r2");

            //service.Accumulate("http://wiki.teamliquid.net/starcraft2/StarCraft_2_League/Group_Stage", "");

            string result = CarnoGenerator.Generate(sink, teamStats: true, aceMatches: false, allKills: true);
            UI.ShowDialog(new UIDocument("Statistics", result));
        }
    }
}
