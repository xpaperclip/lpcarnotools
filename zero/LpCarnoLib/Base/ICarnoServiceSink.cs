using System;
using System.Collections.Generic;
using LxTools.Liquipedia.Parsing2;

namespace LxTools.Carno
{
    public interface ICarnoServiceSink
    {
        void TeamMatchBegin(string winner, string loser);
        void Record(int set, Player winner, Player loser, string map);
        void TeamMatchEnd();

        void PlayerPlacement(string player, string finish, bool determined);

        void UnknownTemplate(WikiTemplateNode template);

        void SetTemporaryIdMap(string id, string link);
        void ClearTemporaryIdMap();
        string GetTemporaryPlayerLink(string id);

        void UpdatePlayerRace(string id, Race race);

        string ConformPlayerId(string id);
        string ConformTeamId(string id);
        string ConformMap(string map);
    }
}
