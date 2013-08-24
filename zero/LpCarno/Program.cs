using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarnoZ;

class Program
{
    static void Main(string[] args)
    {
        CarnoService service = new CarnoService();
        InitialiseIdRewriter(service);

        //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2012-2013_Proleague/Round_1", "round1");
        //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2012-2013_Proleague/Round_2", "round2");
        //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2012-2013_Proleague/Round_3", "round3");
        //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2012-2013_Proleague/Round_4", "round4");
        //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2012-2013_Proleague/Round_5", "round5");
        service.Accumulate("http://wiki.teamliquid.net/starcraft2/2012-2013_Proleague/Round_6", "round6");
        //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2012-2013_Proleague/Playoffs", "po");

        //service.Accumulate("2013 WCS Season 2 Korea OSL/Premier/Ro32", "ro32");
        //service.Accumulate("2013 WCS Season 2 Korea OSL/Premier/Ro16", "ro16");
        //service.Accumulate("http://wiki.teamliquid.net/starcraft2/index.php?title=2013_WCS_Season_2_Korea_OSL/Premier&action=edit&section=6", "po");
        //service.Accumulate("http://wiki.teamliquid.net/starcraft2/index.php?title=2013_WCS_Season_2_Korea_OSL/Premier&action=edit&section=7", "pm");
        //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2013_WCS_Season_2_Korea_OSL/Challenger", "ch");
        //service.Accumulate("http://wiki.teamliquid.net/starcraft2/2013_WCS_Season_3_Korea_GSL/Up_and_Down", "ud");

        

        Console.WriteLine();

        using (StreamWriter sw = new StreamWriter("output.txt"))
        {
            sw.WriteLine("==Player Statistics==");
            Matches(service, sw, service.Records, "Match-up");
            Matches(service, sw, service.Records.Where((r) => r.Set == -1), "Ace Matches");

            sw.WriteLine("==Racial Statistics==");
            Racial(service, sw, service.Records, "All Matches");
            Racial(service, sw, service.Records.Where((r) => r.Set == -1), "Ace Matches");

            MapStats(service, sw);
        }

        Console.ReadLine();
    }

    private static void InitialiseIdRewriter(CarnoService service)
    {
        foreach (string line in File.ReadAllLines("playerpka.dict"))
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith(";")) continue;
            int idx = line.IndexOf(",");
            string newid = line.Substring(0, idx);
            string[] oldids = line.Substring(idx + 1).Split(',');
            foreach (string oldid in oldids)
            {
                service.IdRewriter.Add(oldid, newid);
            }
        }
    }

    private static void Matches(CarnoService service, StreamWriter sw, IEnumerable<Record> games, string title)
    {
        var players = (games.Select((r) => new { Player = r.Winner, r.Loser.Race, Win = true })).Concat(games.Select((r) => new { Player = r.Loser, r.Winner.Race, Win = false }));
        var table = from g in players.GroupBy((p) => p.Player)
                    let wl = WL.Fill(g, (p) => p.Win)
                    let vT = WL.Fill(g, (p) => p.Win, (p) => p.Race == Race.Terran)
                    let vZ = WL.Fill(g, (p) => p.Win, (p) => p.Race == Race.Zerg)
                    let vP = WL.Fill(g, (p) => p.Win, (p) => p.Race == Race.Protoss)
                    orderby wl descending, g.Key.Id
                    select new { g.Key, wl, vT, vZ, vP };

        string template = File.ReadAllText("match.wiki");
        string template_top10 = File.ReadAllText("matchtop10.wiki");
        string template_complete = File.ReadAllText("matchcomplete.wiki");
        string row = File.ReadAllText("match-row.wiki");

        if (table.Count() > 15)
        {
            sw.WriteLine(TableWriter.WriteTable(template_top10, row,
               new
               {
                   title = title
               },
               table.Index((a, b) => a.wl == b.wl).TakeTop(10).Select((r) => new
               {
                   index = r.Index,
                   flag = r.Object.Key.Flag,
                   race = r.Object.Key.Race.ToString().MaxSubstring(1),
                   player = r.Object.Key.IdWithLinkIfNeeded(),
                   team = r.Object.Key.Team,
                   wl = r.Object.wl,
                   vT = r.Object.vT,
                   vZ = r.Object.vZ,
                   vP = r.Object.vP,
               })));
            template = template_complete;
        }
        sw.WriteLine(TableWriter.WriteTable(template, row,
           new
           {
               title = title,
           },
           table.Index((a, b) => a.wl == b.wl).Select((r) => new
           {
               index = r.Index,
               flag = r.Object.Key.Flag,
               race = r.Object.Key.Race.ToString().MaxSubstring(1),
               player = r.Object.Key.IdWithLinkIfNeeded(),
               team = r.Object.Key.Team,
               wl = r.Object.wl,
               vT = r.Object.vT,
               vZ = r.Object.vZ,
               vP = r.Object.vP,
           })));

        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Matches ({0} players in {1} games)", table.Count(), games.Count());
        Console.WriteLine("{0,-15} | {1,-12} | {2,-12} | {3,-12} | {4,-12}", "ID", "Overall", "vT", "vZ", "vP");
        Console.ResetColor();
        foreach (var xidx in table.Index((a, b) => a.wl == b.wl))
        {
            var x = xidx.Object;
            Console.WriteLine("{5,2} {0,-12} | {1} | {2} | {3} | {4}", x.Key.Id, xWL(x.wl), xWL(x.vT), xWL(x.vZ), xWL(x.vP), xidx.Index);
        }
        Console.WriteLine();
    }
    private static void Racial(CarnoService service, StreamWriter sw, IEnumerable<Record> games, string title)
    {
        string template = File.ReadAllText("racial.wiki");
        string row = File.ReadAllText("racial-row.wiki");

        var players = (games.Select((r) => new { P1 = r.Winner, P2 = r.Loser, Win = true })).Concat(games.Select((r) => new { P1 = r.Loser, P2 = r.Winner, Win = false }));
        var table = from g in players.GroupBy((p) => p.P1.Team)
                    let wl = WL.Fill(g, (p) => p.Win)
                    let T = WL.Fill(g, (p) => p.Win, (p) => p.P1.Race == Race.Terran)
                    let Z = WL.Fill(g, (p) => p.Win, (p) => p.P1.Race == Race.Zerg)
                    let P = WL.Fill(g, (p) => p.Win, (p) => p.P1.Race == Race.Protoss)
                    orderby wl.Percentage descending, g.Key
                    select new { g.Key, wl, T, Z, P };

        var ov = new
        {
            T = WL.Fill(players, (p) => p.Win, (p) => p.P1.Race == Race.Terran),
            Z = WL.Fill(players, (p) => p.Win, (p) => p.P1.Race == Race.Zerg),
            P = WL.Fill(players, (p) => p.Win, (p) => p.P1.Race == Race.Protoss),
        };

        sw.WriteLine(TableWriter.WriteTable(template, row,
            new
            {
                title = title,
                Ttotal = ov.T.Total != 0 ? ov.T.Total.ToString() : "-",
                Twin = ov.T.Total != 0 ? ov.T.Wins.ToString() : "-",
                Tloss = ov.T.Total != 0 ? ov.T.Losses.ToString() : "-",
                Tpc = ov.T.Total != 0 ? ov.T.Percentage.ToString("0") + "%" : "-",
                Ztotal = ov.Z.Total != 0 ? ov.Z.Total.ToString() : "-",
                Zwin = ov.Z.Total != 0 ? ov.Z.Wins.ToString() : "-",
                Zloss = ov.Z.Total != 0 ? ov.Z.Losses.ToString() : "-",
                Zpc = ov.Z.Total != 0 ? ov.Z.Percentage.ToString("0") + "%" : "-",
                Ptotal = ov.P.Total != 0 ? ov.P.Total.ToString() : "-",
                Pwin = ov.P.Total != 0 ? ov.P.Wins.ToString() : "-",
                Ploss = ov.P.Total != 0 ? ov.P.Losses.ToString() : "-",
                Ppc = ov.P.Total != 0 ? ov.P.Percentage.ToString("0") + "%" : "-",
            },
            table.Index((a, b) => a.wl == b.wl).Select((r) => new
            {
                index = r.Index,
                team = r.Object.Key,
                Ttotal = r.Object.T.Total != 0 ? r.Object.T.Total.ToString() : "-",
                Twin = r.Object.T.Total != 0 ? r.Object.T.Wins.ToString() : "-",
                Tloss = r.Object.T.Total != 0 ? r.Object.T.Losses.ToString() : "-",
                Tpc = r.Object.T.Total != 0 ? r.Object.T.Percentage.ToString("0") + "%" : "-",
                Ztotal = r.Object.Z.Total != 0 ? r.Object.Z.Total.ToString() : "-",
                Zwin = r.Object.Z.Total != 0 ? r.Object.Z.Wins.ToString() : "-",
                Zloss = r.Object.Z.Total != 0 ? r.Object.Z.Losses.ToString() : "-",
                Zpc = r.Object.Z.Total != 0 ? r.Object.Z.Percentage.ToString("0") + "%" : "-",
                Ptotal = r.Object.P.Total != 0 ? r.Object.P.Total.ToString() : "-",
                Pwin = r.Object.P.Total != 0 ? r.Object.P.Wins.ToString() : "-",
                Ploss = r.Object.P.Total != 0 ? r.Object.P.Losses.ToString() : "-",
                Ppc = r.Object.P.Total != 0 ? r.Object.P.Percentage.ToString("0") + "%" : "-",
            })));
    }
    private static void MapStats(CarnoService service, StreamWriter sw)
    {
        string template = File.ReadAllText("map.wiki");
        string row = File.ReadAllText("map-row.wiki");

        var games = service.Records;
        var table = from g in games.GroupBy((g) => g.Map)
                    let total = g.Count()
                    let TvZ = RaceStat(g, Race.Terran, Race.Zerg)
                    let ZvP = RaceStat(g, Race.Zerg, Race.Protoss)
                    let PvT = RaceStat(g, Race.Protoss, Race.Terran)
                    let TvT = g.Where(Predicates.Matchup(Race.Terran)).Count()
                    let ZvZ = g.Where(Predicates.Matchup(Race.Zerg)).Count()
                    let PvP = g.Where(Predicates.Matchup(Race.Protoss)).Count()
                    orderby g.Key
                    select new { g.Key, total, TvZ, ZvP, PvT, TvT, ZvZ, PvP };

        var ov = new
        {
            total = games.Count(),
            TvZ = RaceStat(games, Race.Terran, Race.Zerg),
            ZvP = RaceStat(games, Race.Zerg, Race.Protoss),
            PvT = RaceStat(games, Race.Protoss, Race.Terran),
            TvT = games.Where(Predicates.Matchup(Race.Terran)).Count(),
            ZvZ = games.Where(Predicates.Matchup(Race.Zerg)).Count(),
            PvP = games.Where(Predicates.Matchup(Race.Protoss)).Count(),
        };

        sw.WriteLine(TableWriter.WriteTable(template, row,
            new
            {
                total = ov.total,
                TvZtotal = ov.TvZ.Total != 0 ? ov.TvZ.Total.ToString() : "-",
                TvZwin = ov.TvZ.Total != 0 ? ov.TvZ.Wins.ToString() : "-",
                TvZloss = ov.TvZ.Total != 0 ? ov.TvZ.Losses.ToString() : "-",
                TvZpc = ov.TvZ.Total != 0 ? ov.TvZ.Percentage.ToString("0") + "%" : "-",
                ZvPtotal = ov.ZvP.Total != 0 ? ov.ZvP.Total.ToString() : "-",
                ZvPwin = ov.ZvP.Total != 0 ? ov.ZvP.Wins.ToString() : "-",
                ZvPloss = ov.ZvP.Total != 0 ? ov.ZvP.Losses.ToString() : "-",
                ZvPpc = ov.ZvP.Total != 0 ? ov.ZvP.Percentage.ToString("0") + "%" : "-",
                PvTtotal = ov.PvT.Total != 0 ? ov.PvT.Total.ToString() : "-",
                PvTwin = ov.PvT.Total != 0 ? ov.PvT.Wins.ToString() : "-",
                PvTloss = ov.PvT.Total != 0 ? ov.PvT.Losses.ToString() : "-",
                PvTpc = ov.PvT.Total != 0 ? ov.PvT.Percentage.ToString("0") + "%" : "-",
                TvT = ov.TvT != 0 ? ov.TvT.ToString() : "-",
                ZvZ = ov.ZvZ != 0 ? ov.ZvZ.ToString() : "-",
                PvP = ov.PvP != 0 ? ov.PvP.ToString() : "-",
            },
            table.Select((r) => new
            {
                map = r.Key,
                total = r.total,
                TvZtotal = r.TvZ.Total != 0 ? r.TvZ.Total.ToString() : "-",
                TvZwin = r.TvZ.Total != 0 ? r.TvZ.Wins.ToString() : "-",
                TvZloss = r.TvZ.Total != 0 ? r.TvZ.Losses.ToString() : "-",
                TvZpc = r.TvZ.Total != 0 ? r.TvZ.Percentage.ToString("0") : "-",
                ZvPtotal = r.ZvP.Total != 0 ? r.ZvP.Total.ToString() : "-",
                ZvPwin = r.ZvP.Total != 0 ? r.ZvP.Wins.ToString() : "-",
                ZvPloss = r.ZvP.Total != 0 ? r.ZvP.Losses.ToString() : "-",
                ZvPpc = r.ZvP.Total != 0 ? r.ZvP.Percentage.ToString("0") : "-",
                PvTtotal = r.PvT.Total != 0 ? r.PvT.Total.ToString() : "-",
                PvTwin = r.PvT.Total != 0 ? r.PvT.Wins.ToString() : "-",
                PvTloss = r.PvT.Total != 0 ? r.PvT.Losses.ToString() : "-",
                PvTpc = r.PvT.Total != 0 ? r.PvT.Percentage.ToString("0") : "-",
                TvT = r.TvT != 0 ? r.TvT.ToString() : "-",
                ZvZ = r.ZvZ != 0 ? r.ZvZ.ToString() : "-",
                PvP = r.PvP != 0 ? r.PvP.ToString() : "-",
            })));

        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("Maps ({0})", table.Count());
        Console.WriteLine("{0,-20}| {1,-12} | {2,-12} | {3,-12} |{4,-2} |{5,-2} |{6,-2}", "Map", "TvZ", "ZvP", "PvT", "TvT", "ZvZ", "PvP");
        Console.ResetColor();
        foreach (var x in table)
        {
            Console.WriteLine("({7,2}) {0,-15}| {1} | {2} | {3} | {4,2} | {5,2} | {6,2}", x.Key.MaxSubstring(15), xWL(x.TvZ.Wins, x.TvZ.Losses), xWL(x.ZvP.Wins, x.ZvP.Losses), xWL(x.PvT.Wins, x.PvT.Losses), x.TvT, x.ZvZ, x.PvP, x.total);
        }
        Console.WriteLine();
    }

    private static WL RaceStat(IEnumerable<Record> g, Race r1, Race r2)
    {
        return new WL(g.Where(Predicates.RaceStat(r1, r2)).Count(),
            g.Where(Predicates.RaceStat(r2, r1)).Count());
    }

    static string xWL(WL wl)
    {
        if (wl.Total == 0) return "     -      ";
        return string.Format("{0,2}-{1,-2} ({2,3:0}%)", wl.Wins, wl.Losses, wl.Percentage);
    }
    static string xWT(int win, int total)
    {
        int loss = total - win;
        float pc = 0;
        if (total == 0) return "     -      ";
        pc = win / (float)total;
        return string.Format("{0,2}-{1,-2} ({2,4:0%})", win, loss, pc);
    }
    static string xWL(int win, int loss)
    {
        int total = win + loss;
        float pc = 0;
        if (total == 0) return "     -      ";
        pc = win / (float)total;
        return string.Format("{0,2}-{1,-2} ({2,4:0%})", win, loss, pc);
    }
}

public static class Predicates
{
    public static Func<Record, bool> Matchup(Race mirror)
    {
        return (r) => (r.Winner.Race == mirror && r.Loser.Race == mirror);
    }
    public static Func<Record, bool> Matchup(Race r1, Race r2)
    {
        return (r) => (r.Winner.Race == r1 && r.Loser.Race == r2) || (r.Winner.Race == r2 && r.Loser.Race == r1);
    }

    public static Func<Record, bool> RaceStat(Race win, Race loss)
    {
        return (r) => (r.Winner.Race == win && r.Loser.Race == loss);
    }
}
