using System;
using System.Collections.Generic;
using System.IO;
using LxTools.Liquipedia;

namespace LxTools.Carno
{
    public class DataStore : ICarnoServiceSink
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

        public void Accumulate(string page)
        {
            string wikicode = LiquipediaClient.GetWikicode(page);
            CarnoService.ProcessWikicode(wikicode, this);
        }

        void ICarnoServiceSink.MatchBegin(string winner, string loser)
        {
            currentMatch = new Match() { TeamWinner = winner, TeamLoser = loser };
            matches.Add(currentMatch);
        }
        void ICarnoServiceSink.MatchEnd()
        {
            currentMatch = null;
        }
        void ICarnoServiceSink.Record(int set, Player winner, Player loser, string map)
        {
            Record record = new Record() { Set = set, Winner = winner, Loser = loser, Map = map };
            records.Add(record);

            if (currentMatch != null)
                currentMatch.Games.Add(record);
        }

        void ICarnoServiceSink.UnknownTemplate(string template)
        {
            // just ignore it
        }

        #region Id Conforming

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

        string ICarnoServiceSink.ConformPlayerId(string id)
        {
            if (id == null) return string.Empty;

            string value;
            if (idRewriter.TryGetValue(id, out value))
                return value;
            else
                return id;
        }
        string ICarnoServiceSink.ConformTeamId(string id)
        {
            if (id == null) return string.Empty;

            string value;
            if (idRewriter.TryGetValue(id, out value))
                return value;
            else
                return id;
        }
        string ICarnoServiceSink.ConformMap(string map)
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
