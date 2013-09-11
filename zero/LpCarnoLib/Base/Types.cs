using System;
using System.Collections.Generic;

namespace LxTools.CarnoZ
{
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
