using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace CarnoZ
{
    public class CarnoService
    {
        public readonly List<Record> Records = new List<Record>();
        public readonly Dictionary<string, string> IdRewriter = new Dictionary<string, string>();

        private string fmtfolder;
        private object tag;

        public void Accumulate(string page, object tag)
        {
            Console.WriteLine("{0} [{1}]", page, tag);
            this.tag = tag;

            if (page.StartsWith("http://wiki.teamliquid.net/starcraft2/index.php?title=") && page.Contains("action=edit"))
            {
                using (WebClient wc = new WebClient())
                {
                    string text = wc.DownloadString(page);
                    text = text.TrimBetween("<textarea", "</textarea>").From(">").Replace("&lt;", "<").Replace("&amp;", "&");
                    ProcessWikicode(text);
                }
            }
            else
            {
                Uri uri;
                if (Uri.TryCreate(page, UriKind.Absolute, out uri) && uri.Host.ToLower() == "wiki.teamliquid.net" && uri.AbsolutePath.StartsWith("/starcraft2/"))
                {
                    page = uri.AbsolutePath.Substring("/starcraft2/".Length);
                }

                string url = "http://wiki.teamliquid.net/starcraft2/api.php?format=xml&action=query&titles=" +
                    Uri.EscapeUriString(page.Replace("%20", " ").Replace(" ", "_")) + "&prop=revisions&rvprop=content";
                XDocument xml = XDocument.Load(url);
                XElement xpage = xml.Elements("api").Elements("query").Elements("pages").Elements("page").First();
                string data = xpage.Elements("revisions").Elements("rev").First().Value;
                ProcessWikicode(data);
            }
        }
        public void ProcessWikicode(string s)
        {
            fmtfolder = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            fmtfolder = Path.Combine(Path.GetDirectoryName(fmtfolder), "LPfmt");

            int length;
            List<LpItem> items = LpParser.Parse(s, 0, out length);

            var templates = from item in items
                            where item is LpTemplate
                            select item as LpTemplate;
            foreach (LpTemplate template in templates)
            {
                if (template.Name == "MatchList")
                {
                    int matchno = 1;
                    while (true)
                    {
                        List<LpItem> contents;
                        if (!template.Params.TryGetValue("match" + matchno.ToString(), out contents))
                            break;

                        if (contents.Count == 1 && contents[0] is LpTemplate)
                            TryProcessMatchMaps(contents[0] as LpTemplate);
                        matchno++;
                    }
                    continue;
                }

                if (TryProcessMatchMaps(template))
                    continue;

                if (TryProcessBracket(template))
                    continue;

                // if we ended up here, we don't support this template
                //sw.WriteLine("; -- Unsupported template: {0} --", template.Name);
            }
        }

        private bool TryProcessMatchMaps(LpTemplate template)
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
                if (TryProcessMatch(template, xs[0], xs[1], xs[2], xs[3], mapno))
                    mapno++;
                else
                    break;

                if (!mapname.Contains("{0}")) break;
            }
            // just for TeamMatch really
            TryProcessMatch(template, "acep1", "acep2", "acemap", "acewin", "-1");

            return true;
        }
        private bool TryProcessBracket(LpTemplate template)
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
                        ProcessBracketGame(template, xs[0], xs[1], "");
                    }
                    else if (xs.Length == 3)
                    {
                        ProcessBracketGame(template, xs[0], xs[1], xs[2]);
                    }
                    else
                    {
                        throw new Exception("Unrecognised format string: " + fmtstring);
                    }
                }
                return true;
            }
        }

        private bool TryProcessMatch(LpTemplate template, string p1, string p2, string mapnameparam, string mapwinparam)
        {
            return TryProcessMatch(template, p1, p2, mapnameparam, mapwinparam, null);
        }
        private bool TryProcessMatch(LpTemplate template, string p1, string p2, string mapname, string mapwin, object param)
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
                winner = TemplateGetPlayer(template, p1, "team1", param);
                loser = TemplateGetPlayer(template, p2, "team2", param);
            }
            else if (win == "2")
            {
                winner = TemplateGetPlayer(template, p2, "team2", param);
                loser = TemplateGetPlayer(template, p1, "team1", param);
            }
            else
            {
                return true;
            }

            int set = 0;
            if (param != null) int.TryParse(param.ToString(), out set);

            Records.Add(new Record() { Tag = tag, Set = set, Map = map, Winner = winner, Loser = loser });
            return true;
        }
        private void ProcessBracketGame(LpTemplate template, string left, string right, string game)
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

            Player p1 = TemplateGetPlayer(template, left, null, null);
            Player p2 = TemplateGetPlayer(template, right, null, null);

            if (template.Params.ContainsKey(game + "details"))
            {
                // game has details - use them
                LpTemplate details = template.GetParamTemplate(game + "details", "BracketMatchSummary");
                for (int i = 1; i <= scoreleft + scoreright; i++)
                {
                    string map = "Unknown";
                    if (details.Params.ContainsKey("map" + i.ToString()))
                    {
                        map = details.GetParamText("map" + i.ToString()) ?? "Unknown";
                    }

                    string win = details.GetParamText(string.Format("map{0}win", i));
                    if (string.IsNullOrEmpty(win) || win == "skip")
                        continue;

                    if (win == "1")
                        Records.Add(new Record() { Tag = tag, Map = map, Winner = p1, Loser = p2 });
                    else if (win == "2")
                        Records.Add(new Record() { Tag = tag, Map = map, Winner = p2, Loser = p1 });
                }
            }
            else
            {
                // just output games
                for (int i = 0; i < scoreleft; i++)
                    Records.Add(new Record() { Tag = tag, Map = "Unknown", Winner = p1, Loser = p2 });
                for (int i = 0; i < scoreright; i++)
                    Records.Add(new Record() { Tag = tag, Map = "Unknown", Winner = p2, Loser = p1 });
                
                //sw.WriteLine("{0}-{1} {2} {3}", scoreleft, scoreright, playerleft, playerright);

                // TODO- extended series
                //|R5W1score2, |R5W2score2=
            }

        }

        private Player TemplateGetPlayer(LpTemplate template, string p1, string team, object param)
        {
            Player pl = new Player();
            pl.Id = template.GetParamText(string.Format(p1, param));
            if (IdRewriter.ContainsKey(pl.Id)) pl.Id = IdRewriter[pl.Id];
            pl.Flag = template.GetParamText(string.Format(p1, param) + "flag");
            pl.Link = template.GetParamText(string.Format(p1, param) + "link");
            pl.Team = template.GetParamText(team);
            if (IdRewriter.ContainsKey(pl.Team)) pl.Team = IdRewriter[pl.Team];

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