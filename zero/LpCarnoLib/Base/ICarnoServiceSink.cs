using System;
using System.Collections.Generic;

namespace LxTools.Carno
{
    public interface ICarnoServiceSink
    {
        void MatchBegin(string winner, string loser);
        void Record(int set, Player winner, Player loser, string map);
        void MatchEnd();

        void UnknownTemplate(string template);

        string ConformPlayerId(string id);
        string ConformTeamId(string id);
        string ConformMap(string map);
    }
}
