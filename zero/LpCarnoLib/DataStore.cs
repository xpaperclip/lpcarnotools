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

        private readonly Dictionary<string, Player> playerInfoMap = new Dictionary<string, Player>();
        private readonly Dictionary<string, Placement> playerPlacements = new Dictionary<string, Placement>();
        private Dictionary<string, Placement> placementMap = null;

        public List<Record> Records { get { return records; } }
        public List<Match> Matches { get { return matches; } }
        public Dictionary<string, Player> PlayerInfoMap { get { return playerInfoMap; } }
        public Dictionary<string, Placement> PlayerPlacements { get { return playerPlacements; } }
        public Dictionary<string, Placement> PlacementMap
        {
            get { return placementMap; }
            set { placementMap = value; }
        }

        public void Accumulate(string page)
        {
            string wikicode = LiquipediaClient.GetWikicode(page);
            CarnoService.ProcessWikicode(wikicode, this);
        }
        public void AccumulateParticipants(string page)
        {
            string wikicode = LiquipediaClient.GetWikicode(page);
            playerInfoMap.Merge(CarnoService.GetPlayerInfoFromParticipants(this, wikicode));
        }

        void ICarnoServiceSink.TeamMatchBegin(string winner, string loser)
        {
            currentMatch = new Match() { TeamWinner = winner, TeamLoser = loser };
            matches.Add(currentMatch);
        }
        void ICarnoServiceSink.TeamMatchEnd()
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

        void ICarnoServiceSink.UnknownTemplate(LxTools.Liquipedia.Parsing2.WikiTemplateNode template)
        {
            // just ignore it
        }

        void ICarnoServiceSink.PlayerPlacement(string player, string finish, bool determined)
        {
            if (placementMap == null || string.IsNullOrEmpty(player))
                return;
            
            Placement placement;
            if (placementMap.TryGetValue(finish, out placement))
            {
                if (!determined)
                {
                    placement.PlacementBg = "active";
                }
                playerPlacements[player] = placement;
                //System.Diagnostics.Debug.WriteLine("{0} -> {1}", player, placement.ToString());
            }
        }

        void ICarnoServiceSink.UpdatePlayerRace(string player, Race race)
        {
            if (this.playerInfoMap.ContainsKey(player))
            {
                Player pl = this.playerInfoMap[player];
                pl.Race = race;
                this.playerInfoMap[player] = pl;
            }
        }

        private readonly Dictionary<string, string> idLinkMap = new Dictionary<string, string>();
        void ICarnoServiceSink.SetIdLinkMap(string id, string link)
        {
            idLinkMap[id] = LiquipediaUtils.NormaliseLink(link);
        }
        string ICarnoServiceSink.GetPlayerLink(string id)
        {
            return idLinkMap.GetValueOrDefault(id, null);
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

            return idRewriter.GetValueOrDefault(id);
        }
        string ICarnoServiceSink.ConformTeamId(string id)
        {
            if (id == null) return string.Empty;
            return idRewriter.GetValueOrDefault(id);
        }
        string ICarnoServiceSink.ConformMap(string map)
        {
            if (map == null) return string.Empty;
            return mapRewriter.GetValueOrDefault(map);
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

    public struct Placement
    {
        public Placement(string PlacementBg, string Points)
        {
            this.PlacementBg = PlacementBg;
            this.Points = Points;
            this.Sort = Points.TryParseAsInt(int.MaxValue - 1);
        }
        public Placement(string PlacementBg, string Points, int Sort)
        {
            this.PlacementBg = PlacementBg;
            this.Points = Points;
            this.Sort = Sort;
        }

        public string PlacementBg;
        public string Points;
        public int Sort;

        public override string ToString()
        {
            return string.Format("{{{{PlacementBG|{0}}}}}{1}", PlacementBg, Points);
        }
    }
}
