using System;
using System.Collections.Generic;
using System.Linq;

namespace LxTools.Carno
{
    public struct WL : IComparable, IComparable<WL>
    {
        public static WL Fill<TElement>(IEnumerable<TElement> games, Func<TElement, bool> winCondition)
        {
            int wins = games.Where(winCondition).Count();
            int total = games.Count();
            return WL.FromWT(wins, total);
        }
        public static WL Fill<TElement>(IEnumerable<TElement> games, Func<TElement, bool> winCondition, Func<TElement, bool> filter)
        {
            var filtered = games.Where(filter);
            int wins = filtered.Where(winCondition).Count();
            int total = filtered.Count();
            return WL.FromWT(wins, total);
        }

        public WL(int wins, int losses)
        {
            this.Wins = wins;
            this.Losses = losses;
        }
        public static WL FromWT(int wins, int total)
        {
            return new WL(wins, total - wins);
        }

        public int Wins;
        public int Losses;
        public int Total
        {
            get { return this.Wins + this.Losses; }
        }
        public float Percentage
        {
            get { return 100.0f * Wins / Total; }
        }

        public override string ToString()
        {
            return "{{WL%|" + Wins.ToString() + "|" + Losses.ToString() + "}}";
        }

        public static bool operator ==(WL a, WL b)
        {
            return a.Wins == b.Wins && a.Losses == b.Losses;
        }
        public static bool operator !=(WL a, WL b)
        {
            return a.Wins != b.Wins || a.Losses != b.Losses;
        }

        public override bool Equals(object obj)
        {
            if (obj is WL) return (WL)obj == this;
            return false;
        }
        public override int GetHashCode()
        {
            return this.Wins * this.Losses;
        }

        public int CompareTo(WL other)
        {
            if (this.Wins > other.Wins)
            {
                return 1;
            }
            else if (this.Wins == other.Wins)
            {
                if (this.Losses < other.Losses)
                {
                    return 1;
                }
                else if (this.Losses == other.Losses)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }
        public int CompareTo(object obj)
        {
            if (!(obj is WL)) return 0;
            return this.CompareTo((WL)obj);
        }
    }
}
