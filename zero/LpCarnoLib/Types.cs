using System;
using System.Collections.Generic;
using System.Linq;

namespace LxTools.CarnoZ
{
    public struct Record
    {
        public int Set;
        public Player Winner;
        public Player Loser;
        public string Map;
        public object Tag;
    }
    public class Match
    {
        public string TeamWinner;
        public string TeamLoser;
        public readonly List<Record> Games = new List<Record>();
    }

    public struct Player
    {
        public string Id;
        public string Link;
        public string Team;
        public Race Race;
        public string Flag;

        public string IdWithLinkIfNeeded()
        {
            if (string.IsNullOrEmpty(Link)) return Id;
            return Link + "|" + Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is Player)
            {
                Player pl = (Player)obj;
                return (pl.Id == this.Id) && (pl.Team == this.Team);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }

    public enum Race
    {
        Unknown,
        Terran,
        Zerg,
        Protoss,
        Random
    }
}
