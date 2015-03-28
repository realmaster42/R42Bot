﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using PlayerIOClient;

namespace R42Bot
{
    public partial class CallsSettings
    {
        public static bool WinSystem = false;
        public static bool AllowJoiners = true,
            KickBots = false;
        public static bool Welcome_Upper = true,
            Welcome = true;
        public static bool Goodbye_Upper = true,
            Goodbye = false;
        public static bool FreeEdit = false;
        public static string Welcome_Text = "",
            Welcome_Text_2 = "";
        public static string Goodbye_Text = "",
            Goodbye_Text_2 = "";
        public static List<string> Bans = new List<string> { };
    }
    public partial class Calls
    {
        public partial class UpdateMeta
        {
            public static void Setup(PlayerIOClient.Message m)
            {
                Variables.worldowner = m.GetString(0);
                Variables.worldtitle = m.GetString(1);
                Variables.plays = m.GetInt(2);
                Variables.woots = m.GetInt(3);

                Variables.currentOwner = Variables.worldowner;
                Variables.currentTitle = Variables.worldtitle;
                Variables.currentPlays = Variables.plays;
                Variables.currentWoots = Variables.woots;
            }
        }
        public partial class Crown
        {
            public static void Setup(PlayerIOClient.Message m)
            {
                if (Variables.botFullyConnected)
                {
                    if (CallsSettings.WinSystem)
                    {
                        Variables.player[m.GetInt(0)].wins = Variables.player[m.GetInt(0)].wins + 1;
                        Thread.Sleep(250);
                        if (Variables.names.ContainsKey(m.GetInt(0)))
                        {
                            Variables.con.Send("say", string.Concat(Variables.names[m.GetInt(0)].ToString() + Voids.GetLangFile(Variables.CurrentLang, 103).Replace("(W)", Variables.player[m.GetInt(0)].wins.ToString())));
                        }
                    }
                }
            }
        }
        public partial class PlayersInGame
        {
            public partial class New
            {
                public static void Setup(PlayerIOClient.Message m)
                {
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        if (CallsSettings.AllowJoiners)
                        {
                            Variables.player[m.GetInt(0)].isBot = (m.GetString(1).ToString().Contains("bot")) ? true : false;
                            if (!Variables.names.ContainsKey(m.GetInt(0)))
                                Variables.names.Add(m.GetInt(0), m.GetString(1));
                            else
                                if (CallsSettings.KickBots) { Thread.Sleep(200); Variables.con.Send("say", "/kick " + m.GetString(1) + " Bots dissallowed!"); }else { Variables.player[m.GetInt(0)].isBot = true; }
                            Variables.player[m.GetInt(0)].isGuest = (m.GetString(1).ToString().StartsWith("guest-")) ? true : false;
                            if (CallsSettings.FreeEdit)
                            {
                                if (Variables.names[m.GetInt(0)] != Variables.botName)
                                {
                                    Thread.Sleep(355);
                                    Variables.con.Send("say", "/giveedit " + Variables.names[m.GetInt(0)].ToString());
                                    Thread.Sleep(355);
                                }
                            }
                            if (CallsSettings.Bans.Contains(Variables.names[m.GetInt(0)]))
                            {
                                Variables.con.Send("say", "/kick " + Variables.names[m.GetInt(0)] + " [R42Bot++] You have been banned by world owner.");
                            }
                            Variables.player[m.GetInt(0)].username = Variables.names[m.GetInt(0)].ToString();

                            if (CallsSettings.Welcome)
                            {
                                if (Variables.names[m.GetInt(0)] != Variables.botName && !CallsSettings.Bans.Contains(Variables.names[m.GetInt(0)]))
                                {
                                    if (!CallsSettings.Welcome_Upper)
                                    {
                                        Thread.Sleep(200);
                                        Variables.con.Send("say", "[R42Bot++] " + Voids.GetLangFile(Variables.CurrentLang, 3) + " " + Variables.names[m.GetInt(0)].ToString().ToLower() + CallsSettings.Welcome_Text_2);
                                        Thread.Sleep(200);
                                    }
                                    else
                                    {
                                        Thread.Sleep(200);
                                        Variables.con.Send("say", "[R42Bot++] " + Voids.GetLangFile(Variables.CurrentLang, 3) + " " + Variables.names[m.GetInt(0)].ToString().ToUpper() + CallsSettings.Welcome_Text_2);
                                        Thread.Sleep(200);
                                    }
                                }
                            }
                            Variables.botFullyConnected = true;

                            Variables.players++;
                        }
                        else
                        {
                            Variables.con.Send("say", "/kick " + m.GetString(1) + " [R42Bot++] Joining disabled.");
                        }
                    });
                }
            }

            public partial class Leave
            {
                public static void Fix(PlayerIOClient.Message m)
                {
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        if (Variables.names.ContainsKey(m.GetInt(0)))
                            Variables.names.Remove(m.GetInt(0));
                    });
                }
                public static void Setup(PlayerIOClient.Message m)
                {
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        if (CallsSettings.Goodbye)
                        {
                            if (Variables.names[m.GetInt(0)] != Variables.botName && !CallsSettings.Bans.Contains(Variables.names[m.GetInt(0)]))
                            {
                                if (!CallsSettings.Goodbye_Upper)
                                {
                                    Thread.Sleep(200);
                                    Variables.con.Send("say", "[R42Bot++] " + Voids.GetLangFile(Variables.CurrentLang, 5) + " " + Variables.names[m.GetInt(0)].ToString().ToLower() + " " + Voids.GetLangFile(Variables.CurrentLang, 6));
                                    Thread.Sleep(200);
                                }
                                else
                                {
                                    Thread.Sleep(200);
                                    Variables.con.Send("say", "[R42Bot++] " + Voids.GetLangFile(Variables.CurrentLang, 5) + " " + Variables.names[m.GetInt(0)].ToString().ToUpper() + " " + Voids.GetLangFile(Variables.CurrentLang, 6));
                                    Thread.Sleep(200);
                                }
                            }
                        }
                        Variables.players = Variables.players - 1;
                    });
                }
            }
        }
        public partial class Init
        {
            public static void Init2()
            {
                Variables.con.Send("init2");
            }
            public static void Setup(PlayerIOClient.Message m)
            {
                Variables.worldowner = m.GetString(0);
                Variables.worldtitle = m.GetString(1);
                Variables.currentOwner = Variables.worldowner;
                Variables.currentTitle = Variables.worldtitle;
                Variables.worldKey = Voids.derot(m.GetString(5));
                Variables.plays = m.GetInt(2);
                Variables.currentPlays = Variables.plays;
                Variables.woots = m.GetInt(3);
                Variables.currentWoots = Variables.woots;
                Variables.totalwoots = m.GetInt(4);
                Variables.botid = m.GetInt(6);
                Variables.botName = m.GetString(9);
                Variables.worldWidth = m.GetInt(12);
                Variables.worldHeight = m.GetInt(13);
                if (!Variables.names.ContainsValue(Variables.botName))
                {
                    Variables.names.Add(m.GetInt(6), Variables.botName);
                }

                Variables.block = new GetBlock[Variables.worldWidth, Variables.worldHeight];
                if (Variables.banList.Contains(Variables.botName))
                {
                    MessageBox.Show(Voids.GetLangFile(Variables.CurrentLang, 71), "R42Bot++ v" + Version.version + " System");
                    Thread.Sleep(250);
                    MessageBox.Show("YES = WAH, NO = NUUUUU!", "R42Bot++ v" + Version.version + " System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    Variables.con.Send("say", "[R42Bot++] Goodbye, the user using me is banned! :D");
                    MessageBox.Show("Suprise, banned!");
                    Variables.con.Disconnect();
                    Application.Exit();
                }
                else
                {
                    Variables.con.Send("say", Voids.GetLangFile(Variables.CurrentLang, 70));
                    Thread.Sleep(200);
                }
            }
        }
    }
}
