using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LpCarno.Templates;

namespace LxTools.Carno
{
    public class MapStatisticsBlock : CarnoBlock
    {
        protected override void EmitInternal(TextWriter tw, DataStore data)
        {
            var games = data.Records;
            var table = from g in games.GroupBy((g) => g.Map)
                        let total = g.Count()
                        let TvZ = g.CalcRaceStat(Race.Terran, Race.Zerg)
                        let ZvP = g.CalcRaceStat(Race.Zerg, Race.Protoss)
                        let PvT = g.CalcRaceStat(Race.Protoss, Race.Terran)
                        let TvT = g.Where(Predicates.Matchup(Race.Terran)).Count()
                        let ZvZ = g.Where(Predicates.Matchup(Race.Zerg)).Count()
                        let PvP = g.Where(Predicates.Matchup(Race.Protoss)).Count()
                        orderby g.Key
                        select new { g.Key, total, TvZ, ZvP, PvT, TvT, ZvZ, PvP };

            var ov = new
            {
                total = games.Count(),
                TvZ = games.CalcRaceStat(Race.Terran, Race.Zerg),
                ZvP = games.CalcRaceStat(Race.Zerg, Race.Protoss),
                PvT = games.CalcRaceStat(Race.Protoss, Race.Terran),
                TvT = games.Where(Predicates.Matchup(Race.Terran)).Count(),
                ZvZ = games.Where(Predicates.Matchup(Race.Zerg)).Count(),
                PvP = games.Where(Predicates.Matchup(Race.Protoss)).Count(),
            };

            var template = new MapStatistics();
            template.Params = new Bag(
                    "total", ov.total.ToString(),
                    "TvT", ov.TvT != 0 ? ov.TvT.ToString() : "-",
                    "ZvZ", ov.ZvZ != 0 ? ov.ZvZ.ToString() : "-",
                    "PvP", ov.PvP != 0 ? ov.PvP.ToString() : "-")
                .TotalWinLossPercentage("TvZ", ov.TvZ)
                .TotalWinLossPercentage("ZvP", ov.ZvP)
                .TotalWinLossPercentage("PvT", ov.PvT);
            template.Rows = table.Select((r) => new Bag(
                    "map", r.Key,
                    "total", r.total.ToString(),
                    "TvT", r.TvT != 0 ? r.TvT.ToString() : "-",
                    "ZvZ", r.ZvZ != 0 ? r.ZvZ.ToString() : "-",
                    "PvP", r.PvP != 0 ? r.PvP.ToString() : "-")
                .TotalWinLossPercentage("TvZ", r.TvZ)
                .TotalWinLossPercentage("ZvP", r.ZvP)
                .TotalWinLossPercentage("PvT", r.PvT));
            tw.Write(template.TransformText());
        }
    }
}
