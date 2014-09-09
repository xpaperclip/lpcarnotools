using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LxTools.Liquipedia;
using LxTools.Liquipedia.Parsing2;

namespace LxTools.Carno
{
    public static class CarnoService
    {
        static CarnoService()
        {
            fmtfolder = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            fmtfolder = Path.Combine(Path.GetDirectoryName(fmtfolder), "LPfmt");
        }
        private static string fmtfolder;

        public static void ProcessWikicode(string s, ICarnoServiceSink sink)
        {
            List<WikiNode> items = WikiParser.Parse(s).ToList();

            var templates = from item in items
                            where item is WikiTemplateNode
                            select item as WikiTemplateNode;
            foreach (WikiTemplateNode template in templates)
            {
                if (template.Name == "MatchList")
                {
                    int matchno = 1;
                    while (true)
                    {
                        List<WikiNode> contents;
                        if (!template.Params.TryGetValue("match" + matchno.ToString(), out contents))
                            break;

                        if (contents.Count == 1 && contents[0] is WikiTemplateNode)
                            TryProcessMatchMaps(sink, contents[0] as WikiTemplateNode);
                        matchno++;
                    }
                    //sink.ClearTemporaryIdMap();
                    continue;
                }

                if (TryProcessMatchMaps(sink, template))
                    continue;

                if (TryProcessBracket(sink, template))
                    continue;

                if (TryProcessTeamBracket(sink, template))
                    continue;

                if (TryProcessGroupTableSlot(sink, template))
                    continue;

                // if we ended up here, we don't support this template
                //sw.WriteLine("; -- Unsupported template: {0} --", template.Name);
                sink.UnknownTemplate(template);
            }
        }
        public static Dictionary<string, Player> GetPlayerInfoFromParticipants(ICarnoServiceSink sink, string s)
        {
            List<WikiNode> nodes = WikiParser.Parse(s).ToList();

            var participants = new Dictionary<string, Player>();
            bool taking = false;
            foreach (var node in nodes)
            {
                if (node.IsSection())
                {
                    taking = ((node as WikiTextNode).Text == "Participants");
                }
                if (taking && node is WikiTableNode)
                {
                    foreach (var cell in (node as WikiTableNode).Cells)
                    {
                        var cellnodes = (from cellnode in cell
                                         where cellnode is WikiTemplateNode
                                         let nodetemplate = cellnode as WikiTemplateNode
                                         orderby nodetemplate.Name
                                         select nodetemplate).ToList();
                        if (cellnodes.Count == 0 || cellnodes.Count > 2
                            || cellnodes[0].Name.ToLower() != "player")
                            continue;

                        WikiTemplateNode player = cellnodes[0];
                        string playerId = GetPlayerId(player);
                        string team = "noteam";

                        if (cellnodes.Count == 2)
                        {
                            WikiTemplateNode temppart = cellnodes[1];
                            team = temppart.Name.From("TeamPart/");
                            team = sink.ConformTeamId(team);
                        }
                        Player info = new Player();
                        info.Id = player.GetParamText("1");
                        info.Flag = player.GetParamText("flag");
                        info.Team = team;
                        info.Link = LiquipediaUtils.NormaliseLink(GetPlayerLink(player.GetParamText("link")));
                        participants[playerId] = info;
                        
                        if (!string.IsNullOrEmpty(info.Link))
                            sink.SetIdLinkMap(info.Id, info.Link);
                    }
                }
            }
            return participants;
        }

        private static bool TryProcessMatchMaps(ICarnoServiceSink sink, WikiTemplateNode template)
        {
            string fmtfile = Path.Combine(fmtfolder, template.Name + ".matchfmt");
            if (!File.Exists(fmtfile))
                return false;

            string[] xs = File.ReadAllLines(fmtfile);
            if (xs.Length != 4)
            {
                throw new Exception("Unrecognised match formatting file.");
            }

            string mapname = xs[2];

            if (template.Name == "TeamMatch")
            {
                string team1 = sink.ConformTeamId(template.GetParamText("team1"));
                string team2 = sink.ConformTeamId(template.GetParamText("team2"));
                string teamwin = template.GetParamText("teamwin");

                if (teamwin == "1")
                {
                    sink.TeamMatchBegin(team1, team2);
                }
                else if (teamwin == "2")
                {
                    sink.TeamMatchBegin(team2, team1);
                }
            }

            if (template.Name == "MatchMaps")
            {
                string winner = template.GetParamText("winner");
                string playerleft = GetPlayerIdentifier(sink, template.GetParamText(xs[0]).Replace(" ", "_"));
                string playerright = GetPlayerIdentifier(sink, template.GetParamText(xs[1]).Replace(" ", "_"));
                if (winner == "1")
                {
                    sink.PlayerPlacement(playerleft, "win", true);
                    sink.PlayerPlacement(playerright, "loss", true);
                }
                else if (winner == "2")
                {
                    sink.PlayerPlacement(playerleft, "loss", true);
                    sink.PlayerPlacement(playerright, "win", true);
                }
                else
                {
                    sink.PlayerPlacement(playerleft, "active", false);
                    sink.PlayerPlacement(playerright, "active", false);
                }
            }

            int mapno = 1;
            while (true)
            {
                if (TryProcessMatch(sink, template, xs[0], xs[1], xs[2], xs[3], mapno))
                    mapno++;
                else
                    break;

                if (!mapname.Contains("{0}")) break;
            }
            // just for TeamMatch really
            TryProcessMatch(sink, template, "acep1", "acep2", "acemap", "acewin", "-1");

            sink.TeamMatchEnd();

            return true;
        }
        private static bool TryProcessBracket(ICarnoServiceSink sink, WikiTemplateNode template)
        {
            string fmtfile = Path.Combine(fmtfolder, template.Name + ".bracketfmt");
            if (!File.Exists(fmtfile))
                return false;

            using (var fmtsr = new StreamReader(fmtfile))
            {
                string fmtstring;
                while ((fmtstring = fmtsr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(fmtstring) || fmtstring.StartsWith(";"))
                    {
                        continue;
                    }

                    string[] xs = fmtstring.Split(' ');
                    switch (xs.Length)
                    {
                        case 2:
                            ProcessBracketGame(sink, template, xs[0], xs[1], "", null, null);
                            break;
                        case 3:
                            ProcessBracketGame(sink, template, xs[0], xs[1], xs[2], null, null);
                            break;
                        case 4:
                            ProcessBracketGame(sink, template, xs[0], xs[1], xs[2], xs[3], null);
                            break;
                        case 5:
                            ProcessBracketGame(sink, template, xs[0], xs[1], xs[2], xs[3], xs[4]);
                            break;
                        default:
                            throw new Exception("Unrecognised format string: " + fmtstring);
                    }
                }
                return true;
            }
        }

        private static bool TryProcessGroupTableSlot(ICarnoServiceSink sink, WikiTemplateNode template)
        {
            if (template.Name != "GroupTableSlot")
                return false;

            var playerTemplate = template.GetParamTemplate("1", "Player");
            if (playerTemplate == null)
                return false;

            var playerIdentifier = GetPlayerId(playerTemplate);
            sink.SetIdLinkMap(playerTemplate.GetParamText("1"), GetPlayerLink(playerTemplate.GetParamText("link")));
            sink.UpdatePlayerRaceFlag(playerIdentifier, GetRaceFromString(playerTemplate.GetParamText("race")), playerTemplate.GetParamText("flag"));

            // if bg exists, placement is determined
            if (template.HasParam("bg"))
            {
                string finish = template.GetParamText("place");
                sink.PlayerPlacement(playerIdentifier, finish, true);
            }
            else
            {
                sink.PlayerPlacement(playerIdentifier, "active", false);
            }
            return true;
        }

        private static bool TryProcessMatch(ICarnoServiceSink sink, WikiTemplateNode template, string p1, string p2, string mapnameparam, string mapwinparam)
        {
            return TryProcessMatch(sink, template, p1, p2, mapnameparam, mapwinparam, null);
        }
        private static bool TryProcessMatch(ICarnoServiceSink sink, WikiTemplateNode template, string p1, string p2, string mapname, string mapwin, object param)
        {
            string playerleft = template.GetParamText(string.Format(p1, param));
            if (playerleft == null) return false;
            playerleft = playerleft.Replace(" ", "_");
            string playerright = template.GetParamText(string.Format(p2, param));
            if (playerright == null) return false;
            playerright = playerright.Replace(" ", "_");

            string mapnameparam = string.Format(mapname, param);
            string mapwinparam = string.Format(mapwin, param);

            if (!template.Params.ContainsKey(mapwinparam))
                return false;

            string map = "Unknown";
            if (template.Params.ContainsKey(mapnameparam))
            {
                map = template.GetParamText(mapnameparam) ?? "Unknown";
            }

            string win = template.GetParamText(mapwinparam);
            if (string.IsNullOrEmpty(win) || win == "skip")
                return false;

            Player pl1 = TemplateGetPlayer(sink, template, p1, "team1", param);
            Player pl2 = TemplateGetPlayer(sink, template, p2, "team2", param);

            Player winner, loser;
            if (win == "1")
            {
                winner = pl1;
                loser = pl2;
            }
            else if (win == "2")
            {
                winner = pl2;
                loser = pl1;
            }
            else
            {
                return true;
            }

            int set = 0;
            if (param != null) int.TryParse(param.ToString(), out set);

            sink.Record(set, winner, loser, sink.ConformMap(map));
            return true;
        }
        private static void ProcessBracketGame(ICarnoServiceSink sink, WikiTemplateNode template, string left, string right, string game, string loserplacement, string winnerplacement)
        {
            if (!template.Params.ContainsKey(left)) return;
            if (!template.Params.ContainsKey(right)) return;

            string playerleft = template.GetParamText(left);
            if (playerleft == null) return;
            playerleft = GetPlayerIdentifier(sink, playerleft.Replace(" ", "_"));
            string playerright = template.GetParamText(right);
            if (playerright == null) return;
            playerright = GetPlayerIdentifier(sink, playerright.Replace(" ", "_"));

            // set player placement
            if (template.GetParamText(left + "win") == "1")
            {
                if (loserplacement != null)
                    sink.PlayerPlacement(playerright, loserplacement, true);
                if (winnerplacement != null)
                    sink.PlayerPlacement(playerleft, winnerplacement, true);
            }
            else if (template.GetParamText(right + "win") == "1")
            {
                if (loserplacement != null)
                    sink.PlayerPlacement(playerleft, loserplacement, true);
                if (winnerplacement != null)
                    sink.PlayerPlacement(playerright, winnerplacement, true);
            }
            else
            {
                sink.PlayerPlacement(playerleft, loserplacement, false);
                sink.PlayerPlacement(playerright, loserplacement, false);
            }

            int scoreleft, scoreright;
            if (!int.TryParse(template.GetParamText(left + "score"), out scoreleft)
                | !int.TryParse(template.GetParamText(right + "score"), out scoreright))
            {
                //sw.WriteLine(";{0}-{1} {2} {3}", template.GetParam(left + "score"),
                //    template.GetParam(right + "score"), playerleft, playerright);
                return;
            }

            Player p1 = TemplateGetPlayer(sink, template, left, null, null);
            Player p2 = TemplateGetPlayer(sink, template, right, null, null);

            if (template.Params.ContainsKey(game + "details"))
            {
                // game has details - use them
                WikiTemplateNode details = template.GetParamTemplate(game + "details", "BracketMatchSummary");
                for (int i = 1; i <= scoreleft + scoreright; i++)
                {
                    string map = "Unknown";
                    if (details.Params.ContainsKey("map" + i.ToString()))
                    {
                        map = details.GetParamText("map" + i.ToString()) ?? "Unknown";
                    }
                    map = sink.ConformMap(map);

                    string win = details.GetParamText(string.Format("map{0}win", i));
                    if (string.IsNullOrEmpty(win) || win == "skip")
                        continue;

                    if (win == "1")
                        sink.Record(0, p1, p2, map);
                    else if (win == "2")
                        sink.Record(0, p2, p1, map);
                }
            }
            else
            {
                string map = sink.ConformMap("Unknown");
                // just output games
                for (int i = 0; i < scoreleft; i++)
                    sink.Record(0, p1, p2, map);
                for (int i = 0; i < scoreright; i++)
                    sink.Record(0, p2, p1, map);
                
                //sw.WriteLine("{0}-{1} {2} {3}", scoreleft, scoreright, playerleft, playerright);

                // TODO- extended series
                //|R5W1score2, |R5W2score2=
            }

        }


        private static bool TryProcessTeamBracket(ICarnoServiceSink sink, WikiTemplateNode template)
        {
            string fmtfile = Path.Combine(fmtfolder, template.Name + ".teambracketfmt");
            if (!File.Exists(fmtfile))
                return false;

            using (var fmtsr = new StreamReader(fmtfile))
            {
                string fmtstring;
                while ((fmtstring = fmtsr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(fmtstring) || fmtstring.StartsWith(";"))
                    {
                        continue;
                    }

                    string[] xs = fmtstring.Split(' ');
                    switch (xs.Length)
                    {
                        case 3:
                            var detailsSets = template.GetParamTemplates(xs[2] + "details", "BracketTeamMatch").ToList();
                            for (int set = 0; set < detailsSets.Count; set++)
                            {
                                var details = detailsSets[set];
                                string team1 = sink.ConformTeamId(template.GetParamText(xs[0] + "team"));
                                string team2 = sink.ConformTeamId(template.GetParamText(xs[1] + "team"));

                                if (detailsSets.Count == 1)
                                {
                                    if (template.GetParamText(xs[0] + "win") == "1")
                                        sink.TeamMatchBegin(team1, team2);
                                    else if (template.GetParamText(xs[1] + "win") == "1")
                                        sink.TeamMatchBegin(team2, team1);
                                }
                                else
                                {
                                    string count = (set == 0) ? "" : (set + 1).ToString();
                                    int p1score, p2score;
                                    if (int.TryParse(template.GetParamText(xs[0] + "score" + count), out p1score)
                                        && int.TryParse(template.GetParamText(xs[1] + "score" + count), out p2score))
                                    {
                                        if (p1score > p2score)
                                            sink.TeamMatchBegin(team1, team2);
                                        else if (p2score > p1score)
                                            sink.TeamMatchBegin(team2, team1);
                                    }
                                }

                                ProcessTeamMatchDetails(sink, details, team1, team2);

                                sink.TeamMatchEnd();
                            }
                            break;
                        default:
                            throw new Exception("Unrecognised format string: " + fmtstring);
                    }
                }
                return true;
            }
        }
        private static bool ProcessTeamMatchDetails(ICarnoServiceSink sink, WikiTemplateNode template, string team1, string team2)
        {
            string fmtfile = Path.Combine(fmtfolder, template.Name + ".matchfmt");
            if (!File.Exists(fmtfile))
                return false;

            string[] xs = File.ReadAllLines(fmtfile);
            if (xs.Length != 4)
            {
                throw new Exception("Unrecognised match formatting file.");
            }

            string mapname = xs[2];

            int mapno = 1;
            while (true)
            {
                if (TryProcessMatchDetail(sink, template, xs[0], xs[1], team1, team2, xs[2], xs[3], mapno))
                    mapno++;
                else
                    break;
            }
            // just for TeamMatch really
            TryProcessMatchDetail(sink, template, "acep1", "acep2", team1, team2, "acemap", "acewin", "-1");
            
            return true;
        }
        private static bool TryProcessMatchDetail(ICarnoServiceSink sink, WikiTemplateNode template,
            string p1, string p2, string team1, string team2, string mapname, string mapwin, object param)
        {
            string playerleft = template.GetParamText(string.Format(p1, param));
            if (playerleft == null) return false;
            playerleft = playerleft.Replace(" ", "_");
            string playerright = template.GetParamText(string.Format(p2, param));
            if (playerright == null) return false;
            playerright = playerright.Replace(" ", "_");

            string mapnameparam = string.Format(mapname, param);
            string mapwinparam = string.Format(mapwin, param);

            if (!template.Params.ContainsKey(mapwinparam))
                return false;

            string map = "Unknown";
            if (template.Params.ContainsKey(mapnameparam))
            {
                map = template.GetParamText(mapnameparam) ?? "Unknown";
            }

            string win = template.GetParamText(mapwinparam);
            if (string.IsNullOrEmpty(win) || win == "skip")
                return false;

            Player pl1 = TemplateGetPlayerBracketTeamMatch(sink, template, p1, team1, param);
            Player pl2 = TemplateGetPlayerBracketTeamMatch(sink, template, p2, team2, param);

            Player winner, loser;
            if (win == "1")
            {
                winner = pl1;
                loser = pl2;
            }
            else if (win == "2")
            {
                winner = pl2;
                loser = pl1;
            }
            else
            {
                return true;
            }

            int set = 0;
            if (param != null) int.TryParse(param.ToString(), out set);

            sink.Record(set, winner, loser, sink.ConformMap(map));
            return true;
        }


        private static Player TemplateGetPlayerBracketTeamMatch(ICarnoServiceSink sink, WikiTemplateNode template, string p1, string team, object param)
        {
            Player pl = new Player();
            pl.Id = sink.ConformPlayerId(template.GetParamText(string.Format(p1, param)));
            pl.Flag = template.GetParamText(string.Format(p1, param) + "flag");
            pl.Link = GetPlayerLink(template.GetParamText(string.Format(p1, param) + "link")) ?? sink.GetPlayerLink(pl.Id);
            pl.Team = sink.ConformTeamId(team);
            pl.Race = GetRaceFromString(template.GetParamText(string.Format(p1, param) + "race").ToLower());

            sink.UpdatePlayerRaceFlag(pl.Identifier, pl.Race, pl.Flag);

            return pl;
        }
        private static Player TemplateGetPlayer(ICarnoServiceSink sink, WikiTemplateNode template, string p1, string team, object param)
        {
            Player pl = new Player();
            pl.Id = sink.ConformPlayerId(template.GetParamText(string.Format(p1, param)));
            pl.Flag = template.GetParamText(string.Format(p1, param) + "flag");
            pl.Link = GetPlayerLink(template.GetParamText(string.Format(p1, param) + "link")) ?? sink.GetPlayerLink(pl.Id);
            pl.Team = sink.ConformTeamId(template.GetParamText(team));
            pl.Race = GetRaceFromString(template.GetParamText(string.Format(p1, param) + "race").ToLower());

            sink.UpdatePlayerRaceFlag(pl.Identifier, pl.Race, pl.Flag);

            return pl;
        }
        private static Race GetRaceFromString(string s)
        {
            switch (s.ToLower())
            {
                case "t":
                case "terran":
                    return Race.Terran;
                case "z":
                case "zerg":
                    return Race.Zerg;
                case "p":
                case "protoss":
                    return Race.Protoss;
                case "r":
                case "random":
                    return Race.Random;
                default:
                    return Race.Unknown;
            }
        }
        private static string GetPlayerLink(string link)
        {
            if (link == "false") return null;
            return link;
        }
        private static string GetPlayerId(WikiTemplateNode template)
        {
            if (template.Name != "Player")
                throw new ArgumentOutOfRangeException("template");

            string playerId = template.GetParamText("1");
            if (!string.IsNullOrEmpty(GetPlayerLink(template.GetParamText("link"))))
                playerId = LiquipediaUtils.NormaliseLink(template.GetParamText("link"));
            return playerId;
        }
        private static string GetPlayerIdentifier(ICarnoServiceSink sink, string player)
        {
            string link = sink.GetPlayerLink(player);
            if (link != null) return link;

            return player;
        }
    }
}