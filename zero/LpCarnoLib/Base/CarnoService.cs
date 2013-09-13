using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LxTools.Liquipedia;
using LxTools.Liquipedia.Parsing;

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
            int length;
            List<IWikiItem> items = WikiParser.Parse(s, 0, out length);

            var templates = from item in items
                            where item is WikiTemplate
                            select item as WikiTemplate;
            foreach (WikiTemplate template in templates)
            {
                if (template.Name == "MatchList")
                {
                    int matchno = 1;
                    while (true)
                    {
                        List<IWikiItem> contents;
                        if (!template.Params.TryGetValue("match" + matchno.ToString(), out contents))
                            break;

                        if (contents.Count == 1 && contents[0] is WikiTemplate)
                            TryProcessMatchMaps(sink, contents[0] as WikiTemplate);
                        matchno++;
                    }
                    continue;
                }

                if (TryProcessMatchMaps(sink, template))
                    continue;

                if (TryProcessBracket(sink, template))
                    continue;

                // if we ended up here, we don't support this template
                //sw.WriteLine("; -- Unsupported template: {0} --", template.Name);
                sink.UnknownTemplate(template.Name);
            }
        }

        private static bool TryProcessMatchMaps(ICarnoServiceSink sink, WikiTemplate template)
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
                    sink.MatchBegin(team1, team2);
                }
                else if (teamwin == "2")
                {
                    sink.MatchBegin(team2, team1);
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

            sink.MatchEnd();

            return true;
        }
        private static bool TryProcessBracket(ICarnoServiceSink sink, WikiTemplate template)
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

                    if (xs.Length == 2)
                    {
                        ProcessBracketGame(sink, template, xs[0], xs[1], "");
                    }
                    else if (xs.Length == 3)
                    {
                        ProcessBracketGame(sink, template, xs[0], xs[1], xs[2]);
                    }
                    else
                    {
                        throw new Exception("Unrecognised format string: " + fmtstring);
                    }
                }
                return true;
            }
        }

        private static bool TryProcessMatch(ICarnoServiceSink sink, WikiTemplate template, string p1, string p2, string mapnameparam, string mapwinparam)
        {
            return TryProcessMatch(sink, template, p1, p2, mapnameparam, mapwinparam, null);
        }
        private static bool TryProcessMatch(ICarnoServiceSink sink, WikiTemplate template, string p1, string p2, string mapname, string mapwin, object param)
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

            Player winner, loser;
            if (win == "1")
            {
                winner = TemplateGetPlayer(sink, template, p1, "team1", param);
                loser = TemplateGetPlayer(sink, template, p2, "team2", param);
            }
            else if (win == "2")
            {
                winner = TemplateGetPlayer(sink, template, p2, "team2", param);
                loser = TemplateGetPlayer(sink, template, p1, "team1", param);
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
        private static void ProcessBracketGame(ICarnoServiceSink sink, WikiTemplate template, string left, string right, string game)
        {
            if (!template.Params.ContainsKey(left)) return;
            if (!template.Params.ContainsKey(right)) return;

            string playerleft = template.GetParamText(left);
            if (playerleft == null) return;
            playerleft = playerleft.Replace(" ", "_");
            string playerright = template.GetParamText(right);
            if (playerright == null) return;
            playerright = playerright.Replace(" ", "_");

            int scoreleft, scoreright;
            if (!int.TryParse(template.GetParamText(left + "score"), out scoreleft)
                | !int.TryParse(template.GetParamText(right + "score"), out scoreright))
            {
                //sw.WriteLine(";{0}-{1} {2} {3}", template.GetParamText(left + "score"),
                //    template.GetParamText(right + "score"), playerleft, playerright);
                return;
            }

            Player p1 = TemplateGetPlayer(sink, template, left, null, null);
            Player p2 = TemplateGetPlayer(sink, template, right, null, null);

            if (template.Params.ContainsKey(game + "details"))
            {
                // game has details - use them
                WikiTemplate details = template.GetParamTemplate(game + "details", "BracketMatchSummary");
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

        private static Player TemplateGetPlayer(ICarnoServiceSink sink, WikiTemplate template, string p1, string team, object param)
        {
            Player pl = new Player();
            pl.Id = sink.ConformPlayerId(template.GetParamText(string.Format(p1, param)));
            pl.Flag = template.GetParamText(string.Format(p1, param) + "flag");
            pl.Link = template.GetParamText(string.Format(p1, param) + "link");
            pl.Team = sink.ConformTeamId(template.GetParamText(team));

            switch (template.GetParamText(string.Format(p1, param) + "race").ToLower())
            {
                case "t":
                case "terran":
                    pl.Race = Race.Terran;
                    break;
                case "z":
                case "zerg":
                    pl.Race = Race.Zerg;
                    break;
                case "p":
                case "protoss":
                    pl.Race = Race.Protoss;
                    break;
                case "r":
                case "random":
                    pl.Race = Race.Random;
                    break;
                default:
                    pl.Race = Race.Unknown;
                    break;
            }

            return pl;
        }
    }
}