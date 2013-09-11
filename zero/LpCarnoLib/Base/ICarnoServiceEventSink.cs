using System;
using System.Collections.Generic;

namespace LxTools.CarnoZ
{
    public interface ICarnoServiceEventSink
    {
        void MatchBegin(string winner, string loser);
        void Record(int set, Player winner, Player loser, string map);
        void MatchEnd();

        void UnknownTemplate(string template);

        string ConformPlayerId(string id);
        string ConformTeamId(string id);
        string ConformMap(string map);
    }
    public class CarnoServiceEventSinkBase : ICarnoServiceEventSink
    {
        public virtual void MatchBegin(string winner, string loser) { }
        public virtual void Record(int set, Player winner, Player loser, string map) { }
        public virtual void MatchEnd() { }

        public virtual void UnknownTemplate(string template)
        {
            // just ignore it
        }

        public virtual string ConformPlayerId(string id)
        {
            return id;
        }
        public virtual string ConformTeamId(string id)
        {
            return id;
        }
        public virtual string ConformMap(string map)
        {
            return map;
        }
    }
}
