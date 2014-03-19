using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
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
                        orderby pointsort descending, ((stats != null) ? stats.wl : WL.Zero).Percentage descending, playerInfo.Identifier
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

    public class PlayerMapStatisticsBlock : CarnoBlock
    {
        protected override void EmitInternal(TextWriter tw, DataStore data)
        {
            var records = data.Records.AsQueryable();

            var maps = from g in records.GroupBy((g) => g.Map)
                       orderby g.Key
                       select g.Key;

            var players = (records.Select((r) => new { map = r.Map, P1 = r.Winner, P2 = r.Loser, Win = true }))
                .Concat(records.Select((r) => new { map = r.Map, P1 = r.Loser, P2 = r.Winner, Win = false }));
            var tmlookup = (from g in players.GroupBy((p) => p.P1.Identifier)
                            from map in maps
                            select new
                            {
                                id = g.Key,
                                player = g.First().P1,
                                map,
                                wl = WL.Fill(g, (p) => p.Win, (p) => p.map == map)
                            }).ToLookup((p) => p.id);
            var table = from tm in tmlookup
                        orderby tm.Key

                        let totalplayed = (from x in tm
                                           select x.wl.Total).Sum()

                        let mostplayed = (from x in tm
                                          where x.wl.Total > 0
                                          orderby x.wl.Total descending
                                          select new
                                          {
                                              map = "[[" + x.map + "]]",
                                              count = x.wl.Total
                                          }).Index((x, y) => x.count == y.count).TakeTop(1)
                        let mostplayedmap = string.Join("<br />", from obj in mostplayed select obj.Object.map)
                        let mostplayedcount = mostplayed.First().Object.count.ToString()

                        let bestmap = (from x in tm
                                       where x.wl.Total > 0
                                       orderby x.wl.Difference descending
                                       select new
                                       {
                                           map = "[[" + x.map + "]]",
                                           wl = x.wl,
                                           gd = x.wl.Difference
                                       }).Index((x, y) => x.gd == y.gd).TakeTop(1)
                        let bestmapmap = string.Join("<br />", from obj in bestmap select obj.Object.map)
                        let bestmaprecord = string.Join("<br />", from obj in bestmap
                                                  select string.Format("{0}-{1}", obj.Object.wl.Wins, obj.Object.wl.Losses))
                        let bestmapcount = bestmap.First().Object.gd.ToStringWithSign()

                        let worstmap = (from x in tm
                                       where x.wl.Total > 0
                                       orderby x.wl.Difference
                                       select new
                                       {
                                           map = "[[" + x.map + "]]",
                                           wl = x.wl,
                                           gd = x.wl.Difference
                                       }).Index((x, y) => x.gd == y.gd).TakeTop(1)
                        let worstmapmap = string.Join("<br />", from obj in worstmap select obj.Object.map)
                        let worstmaprecord = string.Join("<br />", from obj in worstmap
                                                  select string.Format("{0}-{1}", obj.Object.wl.Wins, obj.Object.wl.Losses))
                        let worstmapcount = worstmap.First().Object.gd.ToStringWithSign()

                        let playerInfo = tm.First().player
                        //let playerInfo = data.PlayerInfoMap.GetValueOrDefault(tm.Key, Player.Empty)
                        select new Bag(
                            "flag", playerInfo.Flag,
                            "race", playerInfo.Race.ToString().MaxSubstring(1),
                            "player", playerInfo.IdWithLinkIfNeeded,
                            "team", playerInfo.Team,

                            "totalplayed", totalplayed.ToString(),
                            "mostplayed.map", mostplayedmap,
                            "mostplayed.count", mostplayedcount,
                            "bestmap.map", bestmapmap,
                            "bestmap.record", bestmaprecord,
                            "bestmap.count", bestmapcount,
                            "worstmap.map", worstmapmap,
                            "worstmap.record", worstmaprecord,
                            "worstmap.count", worstmapcount)
                            .MergeGrouping(tm, (x) => x.map, (x) => x.wl.ToString());

            var template = new PlayerMapStatistics();
            template.Maps = maps;
            template.Rows = table;
            tw.Write(template.TransformText());
        }
    }
}
