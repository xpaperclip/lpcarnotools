using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LpCarno.Templates;

namespace LxTools.Carno
{
    public class TeamStatisticsBlock : CarnoBlock
    {
        public bool IncludeAllKillsColumn { get; set; }

        protected override void EmitInternal(TextWriter tw, DataStore data)
        {
            var matches = (data.Matches.Select((m) => new { Team = m.TeamWinner, Win = true })).Concat(data.Matches.Select((m) => new { Team = m.TeamLoser, Win = false }));
            var players = (data.Records.Select((r) => new { Player = r.Winner, Win = true })).Concat(data.Records.Select((r) => new { Player = r.Loser, Win = false }));
            var table = from g in matches.GroupBy((m) => m.Team)
                        let allKills = (from m in data.Matches
                                        where m.Games.Count > 0 &&
                                            m.TeamWinner == g.Key &&
                                            m.Games.All((s) => s.Winner.Equals(m.Games.Last().Winner) || s.Winner.Team != g.Key)
                                        select m).Count()
                        let matchCount = WL.Fill(g, (p) => p.Win)
                        let games = (from r in data.Records
                                     where r.Winner.Team == g.Key || r.Loser.Team == g.Key
                                     select r.Winner.Team == g.Key)
                        let gameCount = WL.Fill(games, (p) => p)
                        let diff = gameCount.Wins - gameCount.Losses
                        orderby matchCount.Wins descending, matchCount.Losses, diff descending
                        select new Bag(
                            "team", g.Key,
                            "allkills", "{{AllKillIcon}}".Repeat(allKills),
                            "matchCount", matchCount.Total.ToString(),
                            "matches", matchCount.ToString(),
                            "gameCount", gameCount.Total.ToString(),
                            "games", gameCount.ToString(),
                            "gameDiff", (diff >= 0 ? "+" : "") + diff.ToString()
                        );

            var template = new TeamStatistics();
            template.IncludeAllKills = this.IncludeAllKillsColumn;
            template.Rows = table.Index((a, b) => false);
            tw.Write(template.TransformText());
        }
    }

    public class TeamRacialStatisticsBlock : CarnoBlock
    {
        public bool Ace { get; set; }
        public bool IncludeRacesPlayedColumns { get; set; }
        public bool IncludeRacesAgainstColumns { get; set; }

        protected override void EmitInternal(TextWriter tw, DataStore data)
        {
            IEnumerable<Record> games = data.Records;
            if (this.Ace)
                games = games.Where((r) => r.IsAce);
            
            var players = (games.Select((r) => new { P1 = r.Winner, P2 = r.Loser, Win = true })).Concat(games.Select((r) => new { P1 = r.Loser, P2 = r.Winner, Win = false }));
            var table = from g in players.GroupBy((p) => p.P1.Team)
                        let wl = WL.Fill(g, (p) => p.Win)
                        let T = WL.Fill(g, (p) => p.Win, (p) => p.P1.Race == Race.Terran)
                        let Z = WL.Fill(g, (p) => p.Win, (p) => p.P1.Race == Race.Zerg)
                        let P = WL.Fill(g, (p) => p.Win, (p) => p.P1.Race == Race.Protoss)
                        let vT = WL.Fill(g, (p) => p.Win, (p) => p.P2.Race == Race.Terran)
                        let vZ = WL.Fill(g, (p) => p.Win, (p) => p.P2.Race == Race.Zerg)
                        let vP = WL.Fill(g, (p) => p.Win, (p) => p.P2.Race == Race.Protoss)
                        orderby wl.Percentage descending, g.Key
                        select new { g.Key, wl, T, Z, P, vT, vZ, vP };

            var ov = new
            {
                T = WL.Fill(players, (p) => p.Win, (p) => p.P1.Race == Race.Terran),
                Z = WL.Fill(players, (p) => p.Win, (p) => p.P1.Race == Race.Zerg),
                P = WL.Fill(players, (p) => p.Win, (p) => p.P1.Race == Race.Protoss),
                vT = WL.Fill(players, (p) => p.Win, (p) => p.P2.Race == Race.Terran),
                vZ = WL.Fill(players, (p) => p.Win, (p) => p.P2.Race == Race.Zerg),
                vP = WL.Fill(players, (p) => p.Win, (p) => p.P2.Race == Race.Protoss),
            };

            var template = new TeamRacialStatistics();
            template.Params = new Bag()
                .TotalWinLossPercentage("T", ov.T)
                .TotalWinLossPercentage("Z", ov.Z)
                .TotalWinLossPercentage("P", ov.P)
                .TotalWinLossPercentage("vT", ov.vT)
                .TotalWinLossPercentage("vZ", ov.vZ)
                .TotalWinLossPercentage("vP", ov.vP);
            template.Rows = table.Index((a, b) => a.wl == b.wl).Select((r) => new Bag(
                    "index", r.Index.ToString(),
                    "team", r.Object.Key)
                    .TotalWinLossPercentage("T", r.Object.T)
                    .TotalWinLossPercentage("Z", r.Object.Z)
                    .TotalWinLossPercentage("P", r.Object.P)
                    .TotalWinLossPercentage("vT", r.Object.vT)
                    .TotalWinLossPercentage("vZ", r.Object.vZ)
                    .TotalWinLossPercentage("vP", r.Object.vP)
                );
            template.IncludeRaces = this.IncludeRacesPlayedColumns;
            template.IncludeVs = this.IncludeRacesAgainstColumns;
            tw.Write(template.TransformText());
        }
    }

    public class TeamMatchupStatisticsBlock : CarnoBlock
    {
        public bool Ace { get; set; }

        protected override void EmitInternal(TextWriter tw, DataStore data)
        {
            IEnumerable<Record> games = data.Records;
            if (this.Ace)
                games = games.Where((r) => r.IsAce);
            
            var players = (games.Select((r) => new P1P2Win() { P1 = r.Winner, P2 = r.Loser, Win = true })).Concat(games.Select((r) => new P1P2Win() { P1 = r.Loser, P2 = r.Winner, Win = false }));
            var table = from g in players.GroupBy((p) => p.P1.Team)
                        orderby g.Key
                        select new Bag(
                            "team", g.Key,
                            "TvT", g.CalcWinRaceStat(Race.Terran, Race.Terran).ToString(),
                            "TvZ", g.CalcWinRaceStat(Race.Terran, Race.Zerg).ToString(),
                            "TvP", g.CalcWinRaceStat(Race.Terran, Race.Protoss).ToString(),
                            "ZvT", g.CalcWinRaceStat(Race.Zerg, Race.Terran).ToString(),
                            "ZvZ", g.CalcWinRaceStat(Race.Zerg, Race.Zerg).ToString(),
                            "ZvP", g.CalcWinRaceStat(Race.Zerg, Race.Protoss).ToString(),
                            "PvT", g.CalcWinRaceStat(Race.Protoss, Race.Terran).ToString(),
                            "PvZ", g.CalcWinRaceStat(Race.Protoss, Race.Zerg).ToString(),
                            "PvP", g.CalcWinRaceStat(Race.Protoss, Race.Protoss).ToString()
                        );

            var template = new TeamMatchupStatistics();
            template.Rows = table;
            tw.Write(template.TransformText());
        }
    }

    public class TeamMapStatisticsBlock : CarnoBlock
    {
        protected override void EmitInternal(TextWriter tw, DataStore data)
        {
            var maps = from g in data.Records.GroupBy((g) => g.Map)
                       orderby g.Key
                       select g.Key;

            var players = (data.Records.Select((r) => new { map = r.Map, P1 = r.Winner.Team, P2 = r.Loser.Team, Win = true })).Concat(data.Records.Select((r) => new { map = r.Map, P1 = r.Loser.Team, P2 = r.Winner.Team, Win = false }));
            var table = from tm in
                            (from g in players.GroupBy((p) => p.P1)
                             from map in maps
                             select new
                             {
                                 team = g.Key,
                                 map,
                                 wl = WL.Fill(g, (p) => p.Win, (p) => p.map == map)
                             }).ToLookup((p) => p.team)
                        orderby tm.Key
                        select new Bag("team", tm.Key).MergeGrouping(tm, (x) => x.map, (x) => x.wl.ToString());

            var template = new TeamMapStatistics();
            template.Maps = maps;
            template.Rows = table;
            tw.Write(template.TransformText());
        }
    }

    public class PlayerStatisticsBlock : CarnoBlock
    {
        public bool Ace { get; set; }
        public bool IncludeAllKillsColumn { get; set; }

        protected override void EmitInternal(TextWriter tw, DataStore data)
        {
            IEnumerable<Record> games = data.Records;
            if (this.Ace)
                games = games.Where((r) => r.IsAce);

            var players = (games.Select((r) => new { Player = r.Winner, r.Loser.Race, Win = true })).Concat(games.Select((r) => new { Player = r.Loser, r.Winner.Race, Win = false }));
            var table = from g in players.GroupBy((p) => p.Player)
                        let wl = WL.Fill(g, (p) => p.Win)
                        let vT = WL.Fill(g, (p) => p.Win, (p) => p.Race == Race.Terran)
                        let vZ = WL.Fill(g, (p) => p.Win, (p) => p.Race == Race.Zerg)
                        let vP = WL.Fill(g, (p) => p.Win, (p) => p.Race == Race.Protoss)
                        let allKills = (from m in data.Matches
                                        where m.Games.Count > 0 &&
                                            m.TeamWinner == g.Key.Team &&
                                            m.Games.All((s) => s.Winner.Equals(g.Key) || s.Winner.Team != g.Key.Team)
                                        select m).Count()
                        orderby wl descending, g.Key.Id
                        select new { g.Key, allKills, wl, vT, vZ, vP };

            var rows = table.Index((a, b) => a.wl == b.wl).Select((r) => new Indexing<Bag>(r.Index, new Bag(
                       "flag", r.Object.Key.Flag ?? "",
                       "race", r.Object.Key.Race.ToString().MaxSubstring(1),
                       "player", r.Object.Key.IdWithLinkIfNeeded,
                       "allkills", "{{AllKillIcon}}".Repeat(r.Object.allKills),
                       "team", r.Object.Key.Team,
                       "wl", r.Object.wl.ToString(),
                       "vT", r.Object.vT.ToString(),
                       "vZ", r.Object.vZ.ToString(),
                       "vP", r.Object.vP.ToString()
                   )));

            var template = new PlayerStatistics();
            if (table.Count() > 15)
            {
                var top10 = new PlayerStatistics();
                top10.HeaderType = "top10";
                top10.IncludeAllKills = this.IncludeAllKillsColumn;
                top10.Rows = rows.TakeTop(10);
                tw.WriteLine(top10.TransformText());

                template.HeaderType = "top10-complete";
            }
            else
            {
                template.HeaderType = "all";
            }
            template.IncludeAllKills = this.IncludeAllKillsColumn;
            template.Rows = rows;
            tw.Write(template.TransformText());
        }
    }

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
                        let points = placement.Points.TryParseAsInt(int.MaxValue) + ((placement.PlacementBg == "active") ? 1 : 0)
                        orderby points descending, playerInfo.Identifier
                        select new
                        {
                            points = points,
                            bag = new Bag(
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
                        }).Index((a, b) => a.points == b.points).Select((r) => new Indexing<Bag>(r.Index, r.Object.bag));

            var template = new IndividualPlayerStatistics();
            template.Rows = rows;
            tw.Write(template.TransformText());
        }
    }
}
