using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LpCarno.Templates;

namespace LxTools.Carno
{
    public class IndividualPlayerStatisticsBlock : CarnoBlock
    {
        protected override void EmitInternal(TextWriter tw, DataStore data)
        {
            IEnumerable<Record> games = data.Records;

            var playerGames = (games.Select((r) => new { Player = r.Winner, r.Loser.Race, Win = true })).Concat(games.Select((r) => new { Player = r.Loser, r.Winner.Race, Win = false }));
            var playerStats = (from g in playerGames.GroupBy((p) => p.Player)
                               let wl = WL.Fill(g, (p) => p.Win)
                               let vT = WL.Fill(g, (p) => p.Win, (p) => p.Race == Race.Terran)
                               let vZ = WL.Fill(g, (p) => p.Win, (p) => p.Race == Race.Zerg)
                               let vP = WL.Fill(g, (p) => p.Win, (p) => p.Race == Race.Protoss)
                               orderby wl descending, g.Key.Id
                               select new { g.Key, wl, vT, vZ, vP }).ToDictionary((x) => x.Key.Identifier, (x) => new { x.Key, x.wl, x.vT, x.vZ, x.vP });

            var rows = (from pp in data.PlayerPlacements
                        where pp.Key != "TBD"
                        let stats = playerStats.GetValueOrDefault(pp.Key, null)
                        let playerInfo = (stats != null) ? stats.Key : data.PlayerInfoMap.GetValueOrDefault(pp.Key, Player.Empty)
                        let placement = data.PlayerPlacements.GetValueOrDefault(pp.Key, new Placement())
                        let pointsort = placement.Sort + ((placement.PlacementBg == "active") ? 1 : 0)
                        orderby pointsort descending, playerInfo.Identifier
                        select new
                        {
                            pointsort = pointsort,
                            bag = new Bag(
                                "ppKey", pp.Key,
                                "flag", data.PlayerInfoMap.GetValueOrDefault(pp.Key, Player.Empty).Flag,
                                "race", playerInfo.Race.ToString().MaxSubstring(1),
                                "player", playerInfo.IdWithLinkIfNeeded,
                                "team", data.PlayerInfoMap.GetValueOrDefault(pp.Key, Player.Empty).Team,
                                "placement", data.PlayerPlacements.GetValueOrDefault(pp.Key, new Placement()).ToString(),
                                "wl", ((stats != null) ? stats.wl : WL.Zero).ToString(),
                                "vT", ((stats != null) ? stats.vT : WL.Zero).ToString(),
                                "vZ", ((stats != null) ? stats.vZ : WL.Zero).ToString(),
                                "vP", ((stats != null) ? stats.vP : WL.Zero).ToString()
                                )
                        }).Index((a, b) => a.pointsort == b.pointsort).Select((r) => new Indexing<Bag>(r.Index, r.Object.bag));

            var template = new IndividualPlayerStatistics();
            template.Rows = rows;
            tw.Write(template.TransformText());
        }
    }
}
