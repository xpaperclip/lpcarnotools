using System;
using System.Collections.Generic;

namespace LxTools.CarnoZ
{
    public class CarnoServiceEventSink : CarnoServiceEventSinkBase
    {
        private readonly List<Record> records = new List<Record>();
        private readonly List<Match> matches = new List<Match>();
        private Match currentMatch = null;

        public List<Record> Records
        {
            get { return records; }
        }
        public List<Match> Matches
        {
            get { return matches; }
        }

        public override void MatchBegin(string winner, string loser)
        {
            currentMatch = new Match() { TeamWinner = winner, TeamLoser = loser };
            matches.Add(currentMatch);
        }
        public override void MatchEnd()
        {
            currentMatch = null;
        }
        public override void Record(int set, Player winner, Player loser, string map)
        {
            Record record = new Record() { Set = set, Winner = winner, Loser = loser, Map = map };
            records.Add(record);

            if (currentMatch != null)
                currentMatch.Games.Add(record);
        }

        #region Id Conforming

        private readonly Dictionary<string, string> idRewriter = new Dictionary<string, string>();
        private readonly Dictionary<string, string> mapRewriter = new Dictionary<string, string>();

        public Dictionary<string, string> IdRewriter
        {
            get { return idRewriter; }
        }
        public Dictionary<string, string> MapRewriter
        {
            get { return mapRewriter; }
        }

        public override string ConformPlayerId(string id)
        {
            if (id == null) return string.Empty;

            string value;
            if (idRewriter.TryGetValue(id, out value))
                return value;
            else
                return id;
        }
        public override string ConformTeamId(string id)
        {
            if (id == null) return string.Empty;

            string value;
            if (idRewriter.TryGetValue(id, out value))
                return value;
            else
                return id;
        }
        public override string ConformMap(string map)
        {
            if (map == null) return string.Empty;

            string value;
            if (mapRewriter.TryGetValue(map, out value))
                return value;
            else
                return map;
        }
    
        #endregion
    }

    public struct Record
    {
        public int Set;
        public Player Winner;
        public Player Loser;
        public string Map;

        public bool IsAce
        {
            get { return this.Set == -1; }
        }
    }

    public class Match
    {
        public string TeamWinner;
        public string TeamLoser;
        public readonly List<Record> Games = new List<Record>();
    }
}
