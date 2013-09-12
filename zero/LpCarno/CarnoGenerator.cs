﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LpCarno.Templates;

namespace LxTools.CarnoZ
{
    public static class CarnoGenerator
    {
        public static string Generate(CarnoServiceEventSink service, bool teamStats, bool aceMatches, bool allKills)
        {
            using (var sw = new StringWriter())
            {
                if (teamStats)
                {
                    sw.WriteLine("==Team Statistics==");
                    TeamStatistics(sw, service, allKills);
                    sw.WriteLine("===Match-ups===");
                    TeamMatchupStatistics(sw, service.Records);
                    sw.WriteLine("===Racial Statistics===");
                    TeamRacialStatistics(sw, service.Records, includeRaces: true, includeVs: true);
                    sw.WriteLine("===Map Statistics===");
                    TeamMapStatistics(sw, service);

                    sw.WriteLine("==Player Statistics==");
                    PlayerStatistics(sw, service, service.Records, allKills);

                    if (aceMatches)
                    {
                        sw.WriteLine("==Ace Matches==");
                        PlayerStatistics(sw, service, service.Records.Where((r) => r.IsAce), false);
                        sw.WriteLine("===Match-ups===");
                        TeamMatchupStatistics(sw, service.Records.Where((r) => r.IsAce));
                        sw.WriteLine("===Racial Statistics===");
                        TeamRacialStatistics(sw, service.Records.Where((r) => r.IsAce), includeRaces: true, includeVs: false);
                    }
                }

                sw.WriteLine("==Map Statistics==");
                MapStatistics(sw, service);

                return sw.ToString();
            }
        }

        public static void LoadRewriter(string filename, Dictionary<string, string> rewriter)
        {
            foreach (string line in File.ReadAllLines(filename))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith(";")) continue;
                int idx = line.IndexOf(",");

                // invalid line, ignore
                if (idx == -1)
                    continue;

                string newid = line.Substring(0, idx);
                string[] oldids = line.Substring(idx + 1).Split(',');
                foreach (string oldid in oldids)
                {
                    rewriter.Add(oldid.Trim(), newid.Trim());
                }
            }
        }

        private static void TeamStatistics(TextWriter sw, CarnoServiceEventSink service, bool includeAllKills)
        {
            var matches = (service.Matches.Select((m) => new { Team = m.TeamWinner, Win = true })).Concat(service.Matches.Select((m) => new { Team = m.TeamLoser, Win = false }));
            var players = (service.Records.Select((r) => new { Player = r.Winner, Win = true })).Concat(service.Records.Select((r) => new { Player = r.Loser, Win = false }));
            var table = from g in matches.GroupBy((m) => m.Team)
                        let allKills = (from m in service.Matches
                                        where m.Games.Count > 0 &&
                                            m.TeamWinner == g.Key &&
                                            m.Games.All((s) => s.Winner.Equals(m.Games.Last().Winner) || s.Winner.Team != g.Key)
                                        select m).Count()
                        let matchCount = WL.Fill(g, (p) => p.Win)
                        let games = (from r in service.Records
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
            template.IncludeAllKills = includeAllKills;
            template.Rows = table.Index((a, b) => false);
            sw.WriteLine(template.TransformText());
        }
        private static void TeamRacialStatistics(TextWriter sw, IEnumerable<Record> games, bool includeRaces, bool includeVs)
        {
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
            template.IncludeRaces = includeRaces;
            template.IncludeVs = includeVs;
            sw.WriteLine(template.TransformText());
        }
        private static void TeamMatchupStatistics(TextWriter sw, IEnumerable<Record> games)
        {
            var players = (games.Select((r) => new P1P2Win() { P1 = r.Winner, P2 = r.Loser, Win = true })).Concat(games.Select((r) => new P1P2Win() { P1 = r.Loser, P2 = r.Winner, Win = false }));
            var table = from g in players.GroupBy((p) => p.P1.Team)
                        orderby g.Key
                        select new Bag(
                            "team", g.Key,
                            "TvT", WinRaceStat(g, Race.Terran, Race.Terran).ToString(),
                            "TvZ", WinRaceStat(g, Race.Terran, Race.Zerg).ToString(),
                            "TvP", WinRaceStat(g, Race.Terran, Race.Protoss).ToString(),
                            "ZvT", WinRaceStat(g, Race.Zerg, Race.Terran).ToString(),
                            "ZvZ", WinRaceStat(g, Race.Zerg, Race.Zerg).ToString(),
                            "ZvP", WinRaceStat(g, Race.Zerg, Race.Protoss).ToString(),
                            "PvT", WinRaceStat(g, Race.Protoss, Race.Terran).ToString(),
                            "PvZ", WinRaceStat(g, Race.Protoss, Race.Zerg).ToString(),
                            "PvP", WinRaceStat(g, Race.Protoss, Race.Protoss).ToString()
                        );

            var template = new TeamMatchupStatistics();
            template.Rows = table;
            sw.WriteLine(template.TransformText());
        }
        private static void TeamMapStatistics(TextWriter sw, CarnoServiceEventSink service)
        {
            var maps = from g in service.Records.GroupBy((g) => g.Map)
                       orderby g.Key
                       select g.Key;

            var players = (service.Records.Select((r) => new { map = r.Map, P1 = r.Winner.Team, P2 = r.Loser.Team, Win = true })).Concat(service.Records.Select((r) => new { map = r.Map, P1 = r.Loser.Team, P2 = r.Winner.Team, Win = false }));
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
            sw.WriteLine(template.TransformText());
        }

        private static void PlayerStatistics(TextWriter sw, CarnoServiceEventSink service, IEnumerable<Record> games, bool includeAllkills)
        {
            var players = (games.Select((r) => new { Player = r.Winner, r.Loser.Race, Win = true })).Concat(games.Select((r) => new { Player = r.Loser, r.Winner.Race, Win = false }));
            var table = from g in players.GroupBy((p) => p.Player)
                        let wl = WL.Fill(g, (p) => p.Win)
                        let vT = WL.Fill(g, (p) => p.Win, (p) => p.Race == Race.Terran)
                        let vZ = WL.Fill(g, (p) => p.Win, (p) => p.Race == Race.Zerg)
                        let vP = WL.Fill(g, (p) => p.Win, (p) => p.Race == Race.Protoss)
                        let allKills = (from m in service.Matches
                                        where m.Games.Count > 0 &&
                                            m.TeamWinner == g.Key.Team &&
                                            m.Games.All((s) => s.Winner.Equals(g.Key) || s.Winner.Team != g.Key.Team)
                                        select m).Count()
                        orderby wl descending, g.Key.Id
                        select new { g.Key, allKills, wl, vT, vZ, vP };

            var rows = table.Index((a, b) => a.wl == b.wl).Select((r) => new Indexing<Bag>(r.Index, new Bag(
                       "flag", r.Object.Key.Flag ?? "",
                       "race", r.Object.Key.Race.ToString().MaxSubstring(1),
                       "player", r.Object.Key.IdWithLinkIfNeeded(),
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
                top10.IncludeAllKills = includeAllkills;
                top10.Rows = rows.TakeTop(10);
                sw.WriteLine(top10.TransformText());

                template.HeaderType = "top10-complete";
            }
            else
            {
                template.HeaderType = "all";
            }
            template.IncludeAllKills = includeAllkills;
            template.Rows = rows;
            sw.WriteLine(template.TransformText());
        }
        private static void MapStatistics(TextWriter sw, CarnoServiceEventSink service)
        {
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
            sw.WriteLine(template.TransformText());
        }

        private static WL RaceStat(IEnumerable<Record> g, Race r1, Race r2)
        {
            return new WL(g.Where(Predicates.RaceStat(r1, r2)).Count(),
                g.Where(Predicates.RaceStat(r2, r1)).Count());
        }
        private static WL WinRaceStat(IEnumerable<P1P2Win> g, Race r1, Race r2)
        {
            return WL.Fill(g, (p) => p.Win, (p) => p.P1.Race == r1 && p.P2.Race == r2);
        }
    }

    public struct P1P2Win
    {
        public Player P1;
        public Player P2;
        public bool Win;
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

    public static class MoreExtensionMethods
    {
        public static string Repeat(this string str, int count)
        {
            return string.Concat(Enumerable.Repeat(str, count));
        }

        internal static Bag MergeGrouping<TKey, TElement>(this Bag bag, IGrouping<TKey, TElement> lookup, Func<TElement, string> key, Func<TElement, string> value)
        {
            foreach (var item in lookup)
            {
                bag[key(item)] = value(item);
            }
            return bag;
        }

        internal static Bag TotalWinLossPercentage(this Bag bag, string id, WL wl)
        {
            bag[id + "total"] = wl.Total != 0 ? wl.Total.ToString() : "-";
            bag[id + "win"] = wl.Total != 0 ? wl.Wins.ToString() : "-";
            bag[id + "loss"] = wl.Total != 0 ? wl.Losses.ToString() : "-";
            bag[id + "pc"] = wl.Total != 0 ? wl.Percentage.ToString("0") + "%" : "-";
            return bag;
        }
    }
}