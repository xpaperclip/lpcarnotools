using System;
using System.Collections.Generic;
using System.Linq;

namespace LxTools.Carno
{
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
        public static Bag MergeGrouping<TKey, TElement>(this Bag bag, IGrouping<TKey, TElement> lookup, Func<TElement, string> key, Func<TElement, string> value)
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
        internal static WL CalcRaceStat(this IEnumerable<Record> g, Race r1, Race r2)
        {
            return new WL(g.Where(Predicates.RaceStat(r1, r2)).Count(),
                g.Where(Predicates.RaceStat(r2, r1)).Count());
        }
        internal static WL CalcWinRaceStat(this IEnumerable<P1P2Win> g, Race r1, Race r2)
        {
            return WL.Fill(g, (p) => p.Win, (p) => p.P1.Race == r1 && p.P2.Race == r2);
        }
    }
}
