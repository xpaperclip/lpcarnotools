using System;
using LxTools.Liquipedia;

namespace LxTools.Carno
{
    public struct Player
    {
        public static Player Empty
        {
            get { return new Player() { Team = "", Flag = "" }; }
        }

        public string Id;
        public string Link;
        public string Team;
        public Race Race;
        public string Flag;

        public string IdWithLinkIfNeeded
        {
            get
            {
                if (string.IsNullOrEmpty(Link)) return Id;
                return LiquipediaUtils.NormaliseLink(this.Link) + "|" + Id;
            }
        }

        public string Identifier
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Link))
                    return LiquipediaUtils.NormaliseLink(this.Link);
                return this.Id;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Player)
            {
                Player pl = (Player)obj;
                return (pl.Identifier == this.Identifier) && (pl.Team == this.Team);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        public override string ToString()
        {
            return this.Identifier;
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
