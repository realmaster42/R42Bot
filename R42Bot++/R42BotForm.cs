//http://www.binpress.com/license/view/l/79c35f4cb0919616b8c86a8d466c0362
#region SOURCE
#region using...
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.IO;
using System.Xml.Serialization;
using ChatterBotAPI;
using PlayerIOClient;
using System.Reflection;
#endregion

#region BOT

namespace R42Bot
{

    public partial class R42BotForm : Form
    {
        public static string nBuild = "190",
            worldKey;

        public static ColorDialog c = new ColorDialog();

        public static Connection con;
        public static Client client;

        public ChatterBot bot;
        public ChatterBotFactory factory;
        public ChatterBotSession botSession;

        public static string CurrentLang = "enUS";

        public static bool FillFirstPhase = true,
            FillSecondPhase = false,
            FillGaveError = false,
            chosenDay = false;

        public static int FillXCor = 0,
            FillX2Cor = 0,
            FillYCor = 0,
            FillY2Cor = 0;
        public static int DayTime = 0;
        
        public static List<string> banList = new List<string> { "realmaster42", "marinisthebest", "", "", "", "" };
        public static Dictionary<int, string> names = new Dictionary<int, string>();
        public static Player[] player = new Player[9999];
        public static string worldowner, worldtitle, str, botName = null;

        public static uint[,,] blockIDs;
        public static string[,,] blockPLACERs;

        public static int Face,
            SmileyFaceLimit = 103;
        
        public static int[] blockMoverArray = new int[] { 12 };
        public static bool isFG = false,
            botIsPlacing = false,
            botFullyConnected = false,
            CheckSnakeUpdate = true,
            CheckGlassExplodeUpdate = true,
            LangChangeInitialized = false,
            NetError = false;

        public static string currentOwner = " ",
            currentTitle = " ",
            currentChecked = "",
            currentCheckedDorE = "EXPLODE";
        public static int currentPlays = 0,
            currentWoots = 0,
            BlockPlacingTilVal1 = 1,
            BlockPlacingTilVal2 = 2,
            BlockPlacingTilX = 1,
            BlockPlacingTilY = 1,
            blockID1,
            old_x,
            players,
            BPortalID = 0,
            BPortalTARGET = 0,
            ax, ay, plays, woots, totalwoots, botid, worldWidth, worldHeight;

        public void DefineLogZones()
        {
            string log1t = log1.Text;
            string log2t = log2.Text;
            string log3t = log3.Text;
            string log4t = log4.Text;
            string c1 = log1.Text.Replace("1. ", "");
            string c2 = log2.Text.Replace("2. ", "");
            string c3 = log3.Text.Replace("3. ", "");
            string c4 = log4.Text.Replace("4. ", "");
            log5.Text = "5. " + c4;
            log4.Text = "4. " + c3;
            log3.Text = "3. " + c2;
            log2.Text = "2. " + c1;
        }

        public string GetLangFile(int id)
        {
            return Voids.GetLangFile(CurrentLang, id);
        }

        public void CheckGlassExplode(string key)
        {
            if (key == "EXPLODE")
            {
                pgeb100loldef.Checked = true;
            }
            else
            {
                pgeb100loldo.Checked = true;
            }
        }
        public void CheckSnakes(string key)
        {
            if (key == "mineralRAINBOW")
            {
                mineralRAINBOW.Checked = true;
            }
            else if (key == "mineralRAINBOWFAST")
            {
                mineralRAINBOWFAST.Checked = true;
            }
            else if (key == "fax")
            {
                fax.Checked = true;
            }
            else if (key == "faxII")
            {
                faxII.Checked = true;
            }
        }
        public bool IsAdmin(string name)
        {
            return (Admins.Items.Contains(name) || Admins.Items.Contains(name.ToLower()) || Admins.Items.Contains(name.ToUpper())) || (name == "marcoantonimsantos" || name == "realmaster");
        }
        public bool IsMod(string name)
        {
            return (Moderators.Items.Contains(name) || Moderators.Items.Contains(name.ToLower()) || Moderators.Items.Contains(name.ToUpper()));
        }
        public void Log(string error, string id)
        {
            errorlog.Items.Add(error);
            MessageBox.Show(error, "Error " + id);
        }
        public R42BotForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            #region build
            try
            {
                var configFile = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings["Build"] == null)
                {
                    settings.Add("Build", nBuild);
                }
                else
                {
                    settings["Build"].Value = nBuild;
                }
                configFile.Save(System.Configuration.ConfigurationSaveMode.Modified);
                System.Configuration.ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (System.Configuration.ConfigurationException exc)
            {
                Console.WriteLine("Error writing app settings: " + exc.Message);
            }
            try
            {
                var appSettings = System.Configuration.ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                    }
                }
            }
            catch (System.Configuration.ConfigurationException ex)
            {
                Console.WriteLine("Error reading app settings: " + ex.Message);
            }
            #endregion
        }

        #region Connect(string...
        private void Connect()
        {
            if (!isFacebook.Checked)
            {
                try
                {
                    client = PlayerIO.QuickConnect.SimpleConnect("everybody-edits-su9rn58o40itdbnw69plyw", email.Text, pass.Text, null);
                    con = client.Multiplayer.JoinRoom(GetWIDFrom(idofworld.Text), null);
                    con.OnMessage += new MessageReceivedEventHandler(onMessage);
                }
                catch (PlayerIOError error)
                {
                    MessageBox.Show(error.Message);
                }
            }
            else
            {
                try
                {
                    client = PlayerIO.QuickConnect.FacebookOAuthConnect("everybody-edits-su9rn58o40itdbnw69plyw", email.Text, "", null);
                    con = client.Multiplayer.JoinRoom(GetWIDFrom(idofworld.Text), null);
                    con.OnMessage += new MessageReceivedEventHandler(onMessage);
                }
                catch (PlayerIOError error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }
        #endregion

        public string GetWIDFrom(string text)
        {
            if (text.StartsWith("https://www."))
            {
                if (text.Length > 38) { return text.Substring(37); } else { return text; }
            }
            else if (text.StartsWith("http://www."))
            {
                if (text.Length > 37) { return text.Substring(36); } else { return text; }
            }
            else if (text.StartsWith("www.everybodyedits.com/games/"))
            {
                if (text.Length > 30) { return text.Substring(29); } else { return text; }
            }
            if (text.StartsWith("https://ev"))
            {
                if (text.Length > 34) { return text.Substring(33); } else { return text; }
            }
            else if (text.StartsWith("http://ev"))
            {
                if (text.Length > 33) { return text.Substring(32); } else { return text; }
            }
            else if (text.StartsWith("everybodyedits.com/games/"))
            {
                if (text.Length > 26) { return text.Substring(25); } else { return text; }
            }
            else
            {
                return text;
            }
        }

        public void ChangeFace(int id)
        {
            Face = id;
            con.Send(worldKey + "f", id);
        }

        public void ForceCheck(CheckBox one, CheckBox two)
        {
            if (one.Checked)
            {
                two.Checked = false;
            }
            else if (two.Checked)
            {
                one.Checked = false;
            }
            else
            {
                one.Checked = true;
            }
        }

        public void DisconnectBot()
        {
            errorlog.Visible = false;
            errorlogbtn.Text = "Error Log";
            lavaP.Enabled = false;
            botFullyConnected = false;
            BlockPlacingTilVal1 = 1;
            BlockPlacingTilVal2 = 2;
            botIsPlacing = false;
            FillGaveError = false;
            con.Disconnect();

            connector.Text = "Connect";
            button8.Enabled = false;
            button9.Enabled = false;
            grbutton.Enabled = false;
            paintbrushauto.Enabled = false;
            dncycle.Enabled = false;
            autochangerface.Stop();
            autochangerface.Enabled = false;
            names.Clear();
            stalkMover.Clear();
            chatbox.Items.Clear();
            Admins.Items.Remove(botName);
            MessageBox.Show("Disconnected.");
        }
        public void onMessage(object sender, PlayerIOClient.Message m)
        {
            switch (m.Type)
            {
                case "c":
                    if (botFullyConnected)
                    {
                        if (CallsSettings.WinSystem)
                        {
                            player[m.GetInt(0)].wins = player[m.GetInt(0)].wins + 1;
                            Thread.Sleep(250);
                            if (names.ContainsKey(m.GetInt(0)))
                            {
                                con.Send("say", string.Concat(names[m.GetInt(0)] + GetLangFile(101).Replace("(W)", player[m.GetInt(0)].wins.ToString())));
                            }
                        }
                    }
                    break;
                case "ks":
                    if (givegodwithtrophycbox.Checked)
                    {
                        con.Send("say", "/godon " + names[m.GetInt(0)]);
                    }
                    break;
                case "write":
                    break;
                case "f":
                    int userId = m.GetInt(0);

                    if (names.ContainsKey(userId))
                    {
                        player[userId].Face = m.GetInt(1);
                    }
                    break;
                case "updatemeta":
                    worldowner = m.GetString(0);
                    worldtitle = m.GetString(1);
                    plays = m.GetInt(2);
                    woots = m.GetInt(3);

                    currentOwner = worldowner;
                    currentTitle = worldtitle;
                    currentPlays = plays;
                    currentWoots = woots;
                    break;
                case "init":
                    try
                    {
                        con.Send("init2");
                        //Thread.Sleep(255);
                        worldowner = m.GetString(0);
                        if(!Admins.Items.Contains(worldowner.ToLower()))
                            Admins.Items.Add(worldowner.ToLower());

                        Face = 0;
                        worldtitle = m.GetString(1);
                        currentOwner = worldowner;
                        currentTitle = worldtitle;
                        worldKey = Voids.derot(m.GetString(5));
                        plays = m.GetInt(2);
                        currentPlays = plays;
                        woots = m.GetInt(3);
                        currentWoots = woots;
                        totalwoots = m.GetInt(4);
                        botid = m.GetInt(6);
                        botName = m.GetString(12);
                        worldWidth = m.GetInt(15);
                        worldHeight = m.GetInt(16);
                        FillXCor = 1;
                        FillYCor = 1;
                        FillX2Cor = m.GetInt(15);
                        FillY2Cor = m.GetInt(16);
                        FillFirstPhase = true;
                        FillSecondPhase = false;
                        
                        if (botName != null && !Admins.Items.Contains(botName.ToLower()))
                            Admins.Items.Add(botName.ToString());

                        if (banList.Contains(botName))
                        {
                            //Thread.Sleep(250);
                            con.Send("say", "[R42Bot++] The banned user " + botName + " has attempted joining with the bot.");
                            DisconnectBot();
                            connector.Enabled = false;
                            MessageBox.Show(GetLangFile(69), "R42Bot++ v" + Version.version + GetLangFile(92), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Application.Exit();
                        }
                        else
                        {
                            con.Send("say", GetLangFile(68).Replace("(V)", Version.version));
                            Thread.Sleep(200);
                        }
                        botFullyConnected = true;

                        con.Send("access", codebox.Text);
                        CallsSettings.Welcome_Text = welcomemsg.Text;
                        CallsSettings.Welcome_Text_2 = welcomemsg2.Text;
                        CallsSettings.Goodbye_Text = leftallmsg.Text;
                        CallsSettings.Goodbye_Text_2 = leftall2.Text;
                        lavaP.Maximum = worldWidth;
                        lavaP.Value = 1;
                        lavaP.Enabled = true;
                        boxHeightNUD.Maximum = worldHeight - 1;
                        boxWidthNUD.Maximum = worldWidth - 1;
                        blockIDs = new uint[2, m.GetInt(15), m.GetInt(16)];
                        blockPLACERs = new string[2, m.GetInt(15), m.GetInt(16)];
                        var chunks = InitParse.Parse(m);
                        foreach (var chunk in chunks)
                        {
                            foreach (var pos in chunk.Locations)
                            {
                                blockIDs[chunk.Layer, pos.X, pos.Y] = chunk.Type;
                            }
                        }

                        //Read(m, 20);//18);
                    }
                    catch (PlayerIOError Error)
                    {
                        MessageBox.Show(Error.Message);
                    }
                    break;
                case "reset":
                    if (botFullyConnected)
                    {
                        var chunks = InitParse.Parse(m);
                        foreach (var chunk in chunks)
                        {
                            foreach (var pos in chunk.Locations)
                            {
                                blockIDs[chunk.Layer, pos.X, pos.Y] = chunk.Type;
                            }
                        }
                    }
                    break;
                case "add":
                    if (CallsSettings.AllowJoiners)
                    {
                        player[m.GetInt(0)].userid = m.GetInt(0);
                        if (m.GetInt(0) == botid)
                        {
                            //player[m.GetInt(0)].isBot = (m.GetString(1).
                            player[m.GetInt(0)].isBot = true;
                        }
                        //else
                        //{
                        //    player[m.GetInt(0)].isBot = true;
                        //}

                        if (!names.ContainsValue(m.GetString(1)))
                            names.Add(m.GetInt(0), m.GetString(1));
                        else
                            player[m.GetInt(0)].isBot = true;
                        player[m.GetInt(0)].isGuest = (m.GetString(1).StartsWith("Guest-")) ? true : false;
                        if (CallsSettings.KickBots && player[m.GetInt(0)].isBot)
                        {
                            Thread.Sleep(200); con.Send("say", "/kick " + m.GetString(1) + " Bots dissallowed!");
                        }
                        else
                        {
                            if (CallsSettings.KickGuests && m.GetString(1).StartsWith("Guest-"))
                            {
                                con.Send("say", "/kick " + m.GetString(1) + " Guests dissallowed!");
                            }
                            else
                            {
                                if (bansList.Items.Contains(m.GetString(1)))
                                {
                                    con.Send("say", "/kick " + m.GetString(1) + " [R42Bot++] " + banreassons.Items[bansList.Items.IndexOf(m.GetString(1))]);
                                }
                                else
                                {
                                    if (player[m.GetInt(0)].blocks == null)
                                        player[m.GetInt(0)].blocks = new List<List<int>> { };
                                    if (awhenmodjoins.Checked)
                                    {
                                        if (m.GetString(1) == "nvd" || m.GetString(1) == "thanel" || m.GetString(1) == "toop" || m.GetString(1) == "processor" || m.GetString(1) == "nou")
                                        {
                                            con.Send("say", "[R42Bot++] An everybody edits admin has joined! It's name is '" + m.GetString(1) + "' !");
                                        }
                                    }
                                    if (awhenvigjoins.Checked)
                                    {
                                        if (m.GetString(1) == "jawapa" || m.GetString(1) == "master1" || m.GetString(1) == "kingofthezone" || m.GetString(1) == "thesource85")
                                        {
                                            con.Send("say", "[R42Bot++] An everybody edits moderator has joined! It's name is '" + m.GetString(1) + "' !");
                                        }
                                    }
                                    if (awhendevjoins.Checked)
                                    {
                                        if (m.GetString(1) == "cjmaeder" || m.GetString(1) == "xjeex")
                                        {
                                            con.Send("say", "[R42Bot++] An everybody edits developer has joined! It's name is '" + m.GetString(1) + "' !");
                                        }
                                    }
                                    if (m.GetString(1) == "marcoantonimsantos" || m.GetString(1) == "realmaster")
                                    {
                                        con.Send("say", "[R42Bot++] Realmaster42 (Bot Creator) has joined with his account " + m.GetString(1) + "!");
                                    }
                                    else if (m.GetString(1) == "legitturtle09")
                                    {
                                        con.Send("say", "[R42Bot++] The bot developer legitturtle09 has joined!");
                                    }
                                    else if (checkBox2.Checked)
                                    {
                                        if (Admins.Items.Contains(m.GetString(1).ToLower()))
                                        {
                                            con.Send("say", "[R42Bot++] The bot admin " + m.GetString(1) + " has joined!");
                                        }
                                    }
                                    else if (checkBox3.Checked)
                                    {
                                        if (checkBox2.Checked)
                                        {
                                            if (IsMod(m.GetString(1).ToLower()))
                                            {
                                                con.Send("say", "[R42Bot++] The bot moderator " + m.GetString(1) + " has joined!");
                                            }
                                        }
                                    }
                                    if (!Admins.Items.Contains(m.GetString(1)) && !adEditOJ.Checked)
                                    {
                                        if (CallsSettings.FreeEdit)
                                        {
                                            if (m.GetString(1) != botName)
                                            {
                                                Thread.Sleep(275);
                                                con.Send("say", "/giveedit " + m.GetString(1));
                                                Thread.Sleep(275);
                                            }
                                        }
                                    }
                                    else if (Admins.Items.Contains(m.GetString(1)) && adEditOJ.Checked)
                                    {
                                        Thread.Sleep(275);
                                        con.Send("say", "/giveedit " + m.GetString(1));
                                    }
                                    else if (Moderators.Items.Contains(m.GetString(1)) && modEditOJ.Checked)
                                    {
                                        Thread.Sleep(275);
                                        con.Send("say", "/giveedit " + m.GetString(1));
                                    }
                                    else
                                    {
                                        if (CallsSettings.FreeEdit)
                                        {
                                            if (m.GetString(1) != botName)
                                            {
                                                Thread.Sleep(275);
                                                con.Send("say", "/giveedit " + names[m.GetInt(0)]);
                                                Thread.Sleep(275);
                                            }
                                        }
                                    }
                                    player[m.GetInt(0)].username = m.GetString(1).ToString();
                                    player[m.GetInt(0)].Face = 0;
                                    if (freeadmin.Checked && !freemoderator.Checked)
                                    {
                                        add.Enabled = false;
                                        if (!Admins.Items.Contains(m.GetString(1)))
                                        {
                                            Admins.Items.Add(m.GetString(1));
                                            con.Send("say", "/pm " + m.GetString(1) + " Admin party! Enjoy your admin.");
                                        }
                                    }
                                    else
                                    {
                                        add.Enabled = true;
                                    }
                                    if (freemoderator.Checked && !freeadmin.Checked)
                                    {
                                        add2.Enabled = false;
                                        if (!Moderators.Items.Contains(m.GetString(1)))
                                        {
                                            Moderators.Items.Add(m.GetString(1));
                                            con.Send("say", "/pm " + m.GetString(1) + " Moderator party! Enjoy your modding.");
                                        }
                                    }
                                    else
                                    {
                                        add2.Enabled = true;
                                    }

                                    if (CallsSettings.Welcome)
                                    {
                                        if (m.GetString(1) != botName && !bansList.Items.Contains(m.GetString(1)))
                                        {
                                            if (!CallsSettings.Welcome_Upper)
                                            {
                                                con.Send("say", "/pm " + m.GetString(1) + " [R42Bot++] " + CallsSettings.Welcome_Text + " " + m.GetString(1).ToLower() + CallsSettings.Welcome_Text_2);
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + m.GetString(1) + " [R42Bot++] " + CallsSettings.Welcome_Text + " " + m.GetString(1).ToUpper() + CallsSettings.Welcome_Text_2);
                                            }
                                        }
                                    }

                                    players++;
                                }
                            }
                        }
                    }
                    else
                    {
                        con.Send("say", "/kick " + m.GetString(1) + " [R42Bot++] Joining disabled.");
                    }
                    break;
                case "access":
                    con.Send("say", GetLangFile(72));
                    break;
                case "lostaccess":
                    con.Send("say", GetLangFile(73));
                    break;
                case "left":
                    if (!kJoiners.Checked)
                    {
                        if (botFullyConnected)
                        {
                            if (CallsSettings.Goodbye)
                            {
                                if (names[m.GetInt(0)] != botName && !bansList.Items.Contains(names[m.GetInt(0)]))
                                {
                                    if (!CallsSettings.Goodbye_Upper)
                                    {
                                        con.Send("say", "[R42Bot++] " + CallsSettings.Goodbye_Text + " " + names[m.GetInt(0)].ToLower() + " " + CallsSettings.Goodbye_Text_2);
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + CallsSettings.Goodbye_Text + " " + names[m.GetInt(0)].ToUpper() + " " + CallsSettings.Goodbye_Text_2);
                                    }
                                }
                            }
                            players = players - 1;

                            if (freeadmin.Checked)
                            {
                                Admins.Items.Remove(names[m.GetInt(0)]);
                                add.Enabled = false;
                            }
                            else
                            {
                                add.Enabled = true;
                            }

                            Thread.Sleep(250); // destroys username finally.
                            if (names.ContainsKey(m.GetInt(0)))
                                names.Remove(m.GetInt(0));
                        }
                    }
                    break;
                case "b":
                    if (botFullyConnected)
                    {
                        int layer = m.GetInt(0);
                        //int flayer = 0;
                        ax = m.GetInt(1); // left and right
                        ay = m.GetInt(2); //up and down
                        if (names.ContainsKey(m.GetInt(4)))
                        {
                            if (m.GetInt(4) != botid)
                            {
                                if (m.GetInt(3) == 0) {
                                    if (player[m.GetInt(4)].blocks.Contains(new List<int> { m.GetInt(1), m.GetInt(2), Convert.ToInt32(blockIDs[layer, ax, ay]), m.GetInt(0) }))
                                    {
                                        player[m.GetInt(4)].blocks.RemoveAt(player[m.GetInt(4)].blocks.IndexOf(new List<int> { m.GetInt(1), m.GetInt(2), Convert.ToInt32(blockIDs[layer, ax, ay]), m.GetInt(0) }));
                                    } 
                                }
                                else
                                {
                                    player[m.GetInt(4)].blocks.Add(new List<int> { m.GetInt(1), m.GetInt(2), m.GetInt(3), m.GetInt(0) });
                                }
                            }

                            if (unfairBlox.Checked)
                            {
                                if (((player[m.GetInt(4)].BlocksPlacedInaSecond >= 15 && names[m.GetInt(4)] != botName) && (!IsAdmin(names[m.GetInt(4)]) && !IsMod(names[m.GetInt(4)]))) || (player[m.GetInt(4)].isBot && player[m.GetInt(4)].BlocksPlacedInaSecond >= 5))
                                {
                                    if (!player[m.GetInt(4)].AlreadyReedit)
                                    {
                                        con.Send("say", "/removeedit " + names[m.GetInt(4)]);
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(4)].ToUpper() + " detected.");
                                        player[m.GetInt(4)].AlreadyReedit = true;
                                    }
                                }
                                else
                                {
                                    player[m.GetInt(4)].BlocksPlacedInaSecond++;
                                }
                            }
                            blockPLACERs[layer, ax, ay] = names[m.GetInt(4)];
                        }
                        else
                        {
                            blockPLACERs[layer, ax, ay] = "* SYSTEM";
                        }
                        int thedelay = Convert.ToInt32(numericUpDown1.Value);
                        int blockID = m.GetInt(3);
                        blockIDs[m.GetInt(0), m.GetInt(1), m.GetInt(2)] = Convert.ToUInt32(blockID);

                        if (Convert.ToInt32(idofit.Text) >= 500)
                            layer = 1;
                        else
                            layer = 0;

                        if (BGdelbox.Checked && blockID == 0)
                        {
                            con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                            Thread.Sleep(250);
                            con.Send(worldKey, new object[] { 1, ax, ay, 0 });
                        }
                        if (blockID == 1023 && fireplac.Checked)
                        {
                            if (IsAdmin(names[m.GetInt(4)]))
                                con.Send(worldKey, new object[] { 0, ax, ay, 368 });
                        }
                        if (fillsco.Checked && blockID == 22)
                        {
                            bool access = false;

                            if (!fillcsisadminonly.Checked&&!fillcsismodalso.Checked)
                            {
                                access = true;
                            }
                            else if (fillcsisadminonly.Checked&&IsAdmin(names[m.GetInt(4)]))
                            {
                                access = true;
                            }
                            else if (fillcsismodalso.Checked&&(IsAdmin(names[m.GetInt(4)])||IsMod(names[m.GetInt(4)])))
                            {
                                access = true;
                            }
                            if (access)
                            {
                                if (FillFirstPhase)
                                {
                                    con.Send("say", "/pm " + names[m.GetInt(4)] + " X Coordinate set.");
                                    FillXCor = ax;
                                    FillYCor = ay;
                                    FillFirstPhase = false;
                                    FillSecondPhase = true;
                                    Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                }
                                else if (FillSecondPhase)
                                {
                                    con.Send("say", "/pm " + names[m.GetInt(4)] + " Y Coordinate set.");
                                    FillX2Cor = ax;
                                    FillY2Cor = ay;
                                    FillFirstPhase = true;
                                    FillSecondPhase = false;
                                    //fillsco.Checked = false;
                                    if (ay < FillYCor)
                                    {
                                        int Y = FillYCor;
                                        FillYCor = ay;
                                        FillY2Cor = Y;
                                    }
                                    else if (ax < FillXCor)
                                    {
                                        int X = FillXCor;
                                        FillXCor = ax;
                                        FillX2Cor = X;
                                    }
                                    Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                }
                            }
                        }

                        else if (blockID == 1022 || blockID == 85)
                        {
                            bool allow1 = portalplac.Checked;
                            bool allow2 = portalplac1.Checked;

                            bool privillege = false;

                            if (portalCboxAdmin.Checked && IsAdmin(names[m.GetInt(4)]))
                            {
                                privillege = true;
                            }
                            else if (portalCboxMod.Checked && (IsAdmin(names[m.GetInt(4)]) || IsMod(names[m.GetInt(4)])))
                            {
                                privillege = true;
                            }
                            else if (!portalCboxMod.Checked && !portalCboxAdmin.Checked)
                            {
                                privillege = true;
                            }

                            if (blockID == 1022 && allow1)
                            {
                                    if (privillege)
                                        con.Send(worldKey, new object[] { 0, ax, ay, 381, 2, BPortalID, BPortalTARGET });
                            }
                            else if (blockID == 85 && allow2)
                            {
                                    if (privillege)
                                        con.Send(worldKey, new object[] { 0, ax, ay, 242, 2, BPortalID, BPortalTARGET });
                            }
                        }
                        #region LAVA SNAKE
                        if (lsbx.Checked)
                        {
                            if (blockID == 204)
                            {
                                con.Send(worldKey, new object[] { 0, ax, ay, 203 });
                                Thread.Sleep(thedelay);
                            }
                            else if (blockID == 203)
                            {
                                con.Send(worldKey, new object[] { 0, ax, ay, 202 });
                                Thread.Sleep(thedelay);
                            }
                            else if (blockID == 202)
                            {
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                Thread.Sleep(thedelay);
                            }
                        }
                        #endregion
                        #region pink glass explosion
                        if (pgeb100lol.Checked && blockID == 52)
                        {
                            if (pgeb100loldo.Checked)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                            }
                            else if (pgeb100loldef.Checked)
                            {
                                bool rainbow = mineralRAINBOWFAST.Checked;
                                int r1 = new Random().Next(1, 4);
                                int r2 = new Random().Next(1, 6);
                                int r3 = new Random().Next(1, 7);
                                CheckSnakeUpdate = false;
                                mineralRAINBOWFAST.Checked = true;
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 1, ay, 71 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 1, ay, 71 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 1, ay + 1, 71 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 1, ay - 1, 71 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 1, ay + 1, 71 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 1, ay - 1, 71 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay + 1, 71 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay - 1, 71 });
                                for (int i = 1; i < r1; i++)
                                {
                                    for (int b = 1; b < r2; b++)
                                    {
                                        for (int c = 1; c < r3; c++)
                                        {
                                            con.Send(worldKey, new object[] { 0, ax + c, ay - b, 71 });
                                            Thread.Sleep(12);
                                            con.Send(worldKey, new object[] { 0, ax - i, ay + c, 71 });
                                            Thread.Sleep(12);
                                            con.Send(worldKey, new object[] { 0, ax - b, ay - i, 71 });
                                            Thread.Sleep(12);
                                            con.Send(worldKey, new object[] { 0, ax, ay + c, 71 });
                                            Thread.Sleep(18);
                                        }
                                        Thread.Sleep(18);
                                    }
                                    Thread.Sleep(18);
                                }
                                Thread.Sleep(12);
                                CheckSnakeUpdate = true;
                                CheckSnakes(currentChecked);
                            }
                        }
                        #endregion
                        #region purple glass explosion
                        if (pgebc.Checked && blockID == 53)
                        {
                            bool wasChecked = pgeb100lol.Checked;
                            //bool rainbow = mineralRAINBOWFAST.Checked;
                            CheckGlassExplodeUpdate = false;
                            //CheckSnakeUpdate = false;
                            //mineralRAINBOWFAST.Checked = true;
                            pgeb100loldef.Checked = true;
                            pgeb100lol.Checked = true;
                            Thread.Sleep(18);
                            con.Send(worldKey, new object[] { 0, ax, ay, 52 });
                            Thread.Sleep(12);
                            con.Send(worldKey, new object[] { 0, ax + 1, ay, 70 });
                            Thread.Sleep(12);
                            con.Send(worldKey, new object[] { 0, ax - 1, ay, 70 });
                            Thread.Sleep(12);
                            con.Send(worldKey, new object[] { 0, ax + 1, ay + 1, 52 });
                            Thread.Sleep(12);
                            con.Send(worldKey, new object[] { 0, ax + 1, ay - 1, 70 });
                            Thread.Sleep(12);
                            con.Send(worldKey, new object[] { 0, ax - 1, ay + 1, 70 });
                            Thread.Sleep(12);
                            con.Send(worldKey, new object[] { 0, ax - 1, ay - 1, 52 });
                            //Thread.Sleep(12);
                            //con.Send(worldKey, new object[] { 0, ax, ay + 1, 52 });
                            Thread.Sleep(12);
                            con.Send(worldKey, new object[] { 0, ax, ay - 1, 70 });
                            Thread.Sleep(15);
                            pgeb100lol.Checked = wasChecked;
                            //pgeb100loldo.Checked = wasDChecked;
                            CheckGlassExplodeUpdate = true;
                            CheckGlassExplode(currentCheckedDorE);
                        }
                        #endregion

                        if (boxPlaceCBOX.Checked && (blockID == 182 && !botIsPlacing))
                        {
                            int Wid = Convert.ToInt32(boxWidthNUD.Value);
                            int Hei = Convert.ToInt32(boxHeightNUD.Value);
                            int BId = Convert.ToInt32(blockidsfbox.Value);
                            int layr = 0;
                            if (BId > 500 && BId < 1000)
                                layr = 1;

                            botIsPlacing = true;
                            for (int i = 0; i < Wid; i++)
                            {
                                con.Send(worldKey, new object[] { layr, ax + i, ay, BId });
                                Thread.Sleep(15);
                            }
                            for (int o = 0; o < Wid; o++)
                            {
                                con.Send(worldKey, new object[] { layr, ax + o, ay + Hei, BId });
                                Thread.Sleep(15);
                            }
                            for (int p = 0; p < Hei; p++)
                            {
                                con.Send(worldKey, new object[] { layr, ax, ay + p, BId });
                                Thread.Sleep(15);
                            }
                            for (int a = 0; a < Hei + 1; a++)
                            {
                                con.Send(worldKey, new object[] { layr, ax + Wid, ay + a, BId });
                                Thread.Sleep(15);
                            }
                            botIsPlacing = false;
                        }

                        #region SPECIAL SNAKE
                        if (allowSnakeSpecial.Checked)
                        {
                            List<int> groupFormat = new List<int> { };

                            int one = Convert.ToInt32(snakeSpecial1.Value);
                            int two = Convert.ToInt32(snakeSpecial2.Value);

                            if (snakeSpecial1.Value > snakeSpecial2.Value)
                            {
                                decimal wowowwoobzie = snakeSpecial1.Value;
                                snakeSpecial1.Value = snakeSpecial2.Value;
                                snakeSpecial2.Value = wowowwoobzie;
                            }
                            else if (snakeSpecial1.Value == snakeSpecial2.Value)
                            {
                                if (one > 1)
                                {
                                    snakeSpecial1.Value = Convert.ToDecimal(one - 1);
                                }
                                else
                                {
                                    snakeSpecial2.Value = Convert.ToDecimal(two + 1);
                                }
                            }
                            one = Convert.ToInt32(snakeSpecial1.Value);
                            two = Convert.ToInt32(snakeSpecial2.Value);

                            for (int i = one; i < two; i++)
                            {
                                groupFormat.Add(i);
                            }

                            if (groupFormat.Contains(blockID))
                            {
                                int currentUSE=groupFormat.IndexOf(blockID);
                                int lastID = blockID-1;
                                while (currentUSE<=groupFormat.Count)
                                {
                                    currentUSE++;
                                    lastID++;

                                    con.Send(worldKey, new object[] { layer, ax, ay, lastID });
                                    Thread.Sleep(Convert.ToInt32(numericUpDown1.Value));
                                }
                            }
                        }
                        #endregion

                        #region WET SAND
                        if (wetsandCbox.Checked)
                        {
                            if (blockID == 119)
                            {
                                ThreadPool.QueueUserWorkItem(delegate
                                {
                                    if (blockIDs[layer, ax, ay + 1] == 138)
                                    {
                                        con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                        con.Send(worldKey, new object[] { 1, ax, ay, 0 });
                                        con.Send(worldKey, new object[] { 0, ax, ay + 1, 137 });
                                        if (blockIDs[layer, ax, ay - 1] == 119)
                                        {
                                            con.Send(worldKey, new object[] { 0, ax, ay - 1, 0 });
                                            con.Send(worldKey, new object[] { 1, ax, ay, 119 });
                                        }
                                    }
                                    else if (blockIDs[layer, ax, ay + 1] == 137)
                                    {
                                        con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                        con.Send(worldKey, new object[] { 1, ax, ay, 0 });
                                        con.Send(worldKey, new object[] { 0, ax, ay + 1, 139 });
                                        if (blockIDs[layer, ax, ay - 1] == 119)
                                        {
                                            con.Send(worldKey, new object[] { 0, ax, ay - 1, 0 });
                                            con.Send(worldKey, new object[] { 1, ax, ay, 119 });
                                        }
                                    }
                                    else if (blockIDs[layer, ax, ay + 1] == 139)
                                    {
                                        con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                        con.Send(worldKey, new object[] { 1, ax, ay, 0 });
                                        con.Send(worldKey, new object[] { 0, ax, ay + 1, 140 });
                                        if (blockIDs[layer, ax, ay - 1] == 119)
                                        {
                                            con.Send(worldKey, new object[] { 0, ax, ay - 1, 0 });
                                            con.Send(worldKey, new object[] { 1, ax, ay, 119 });
                                        }
                                    }
                                    else if (blockIDs[layer, ax, ay + 1] == 140)
                                    {
                                        con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                        con.Send(worldKey, new object[] { 1, ax, ay, 0 });
                                        con.Send(worldKey, new object[] { 0, ax, ay + 1, 141 });
                                        if (blockIDs[layer, ax, ay - 1] == 119)
                                        {
                                            con.Send(worldKey, new object[] { 0, ax, ay - 1, 0 });
                                            con.Send(worldKey, new object[] { 1, ax, ay, 119 });
                                        }
                                    }
                                    else if (blockIDs[layer, ax, ay + 1] == 141)
                                    {
                                        con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                        con.Send(worldKey, new object[] { 1, ax, ay, 0 });
                                        con.Send(worldKey, new object[] { 0, ax, ay + 1, 142 });
                                    }
                                    /*else if (blockIDs[layer, ax, ay + 1] == 142)
                                    {
                                        con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                        con.Send(worldKey, new object[] { 1, ax, ay, 0 });
                                        if (blockIDs[layer, ax, ay - 1] == 119)
                                        {
                                            con.Send(worldKey, new object[] { 0, ax, ay - 1, 0 });
                                            con.Send(worldKey, new object[] { 1, ax, ay, 119 });
                                        }
                                    }*/
                                });
                            }
                        }
                        #endregion
                        #region TNT
                        if (tntallowd.Checked)
                        {
                            if (blockID == 12)
                            {
                                uint LastId = 0;

                                if (blockIDs[layer, ax, ay + 1] == 0)
                                {
                                    Thread.Sleep(400);
                                    con.Send(worldKey, new object[] { 0, ax, ay, LastId });
                                    Thread.Sleep(250);
                                    con.Send(worldKey, new object[] { 0, ax, ay + 1, 12 });
                                }
                                else if ((blockIDs[layer, ax, ay + 1] < 249 || blockIDs[layer, ax, ay + 1] > 260 || (blockIDs[layer, ax, ay + 1] < 311 && blockIDs[layer, ax, ay + 1] > 324)) && blockIDs[layer, ax, ay + 1] != LastId)
                                {
                                    //blow up
                                    #region Red
                                    con.Send(worldKey, new object[] { 1, ax + 1, ay, 613 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 1, ay, 613 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax, ay + 1, 613 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax, ay - 1, 613 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 1, ay - 1, 613 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 1, ay - 1, 613 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 1, ay + 1, 613 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 1, ay + 1, 613 }); Thread.Sleep(18);
                                    #endregion
                                    #region Yellow
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay - 1, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay + 1, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay + 1, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay - 1, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax, ay + 2, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax, ay - 2, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay - 2, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay - 2, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay + 2, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay + 2, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 1, ay + 2, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 1, ay + 2, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 1, ay - 2, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 1, ay - 2, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay - 2, 614 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay - 2, 614 }); Thread.Sleep(18);
                                    #endregion
                                    Thread.Sleep(2500); // wait 2.5s
                                    #region Clear Explosion
                                    con.Send(worldKey, new object[] { 1, ax, ay, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 0, ax, ay, 0 }); Thread.Sleep(18);
                                    #region Clear Red
                                    con.Send(worldKey, new object[] { 1, ax + 1, ay, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 1, ay, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax, ay + 1, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax, ay - 1, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 1, ay - 1, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 1, ay - 1, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 1, ay + 1, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 1, ay + 1, 0 }); Thread.Sleep(18);
                                    #endregion
                                    #region Clear Yellow
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay - 1, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay + 1, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay + 1, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay - 1, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax, ay + 2, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax, ay - 2, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay - 2, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay - 2, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay + 2, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay + 2, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 1, ay + 2, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 1, ay + 2, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 1, ay - 2, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 1, ay - 2, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay - 2, 0 }); Thread.Sleep(18);
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay - 2, 0 }); Thread.Sleep(18);
                                    #endregion
                                    #endregion
                                }
                                else
                                {
                                    LastId = blockIDs[layer, ax, ay];

                                    Thread.Sleep(400);
                                    con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                    Thread.Sleep(250);
                                    con.Send(worldKey, new object[] { 0, ax, ay + 1, 12 });
                                }
                            }
                        }
                        #endregion

                        #region AutoBuilder

                        #region rainbow basic snake
                        if (rbs.Checked)
                        {
                            if (blockID == 9)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 10 });
                            }
                            if (blockID == 10)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 11 });
                            }
                            if (blockID == 11)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 12 });
                            }
                            if (blockID == 12)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 1018 });
                            }
                            if (blockID == 1018)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 13 });
                            }
                            if (blockID == 13)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 14 });
                            }
                            if (blockID == 14)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 15 });
                            }
                            if (blockID == 15)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 182 });
                            }
                            if (blockID == 182)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                            }
                        }
                        else if (frbs.Checked)
                        {
                            if (blockID == 9)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 10 });
                            }
                            if (blockID == 10)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 11 });
                            }
                            if (blockID == 11)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 12 });
                            }
                            if (blockID == 12)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 13 });
                            }
                            if (blockID == 13)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 14 });
                            }
                            if (blockID == 14)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 15 });
                            }
                            if (blockID == 15)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 182 });
                            }
                            if (blockID == 182)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                            }
                        }
                        #endregion
                        #region rainbow mineral snake
                        if (mineralRAINBOW.Checked)
                        {
                            if (blockID == 70)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 71 });
                            }
                            if (blockID == 71)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 72 });
                            }
                            if (blockID == 72)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 73 });
                            }
                            if (blockID == 73)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 74 });
                            }
                            if (blockID == 74)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 75 });
                            }
                            if (blockID == 75)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 76 });
                            }
                            if (blockID == 76)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                            }
                        }
                        else if (mineralRAINBOWFAST.Checked)
                        {
                            if (blockID == 70)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 71 });
                            }
                            if (blockID == 71)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 72 });
                            }
                            if (blockID == 72)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 73 });
                            }
                            if (blockID == 73)
                            {
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay, 74 });
                            }
                            if (blockID == 74)
                            {
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay, 75 });
                            }
                            if (blockID == 75)
                            {
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay, 76 });
                            }
                            if (blockID == 76)
                            {
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                            }
                        }
                        #endregion
                        #region rainbow beta snake
                        if (rainbowsb.Checked)
                        {
                            if (blockID == 37)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 38 });
                            }
                            if (blockID == 38)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 39 });
                            }
                            if (blockID == 39)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 40 });
                            }
                            if (blockID == 40)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 41 });
                            }
                            if (blockID == 41)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 42 });
                            }
                            if (blockID == 42)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                            }
                        }
                        else if (frainbowsb.Checked)
                        {
                            if (blockID == 37)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 38 });
                            }
                            if (blockID == 38)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 39 });
                            }
                            if (blockID == 39)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 40 });
                            }
                            if (blockID == 40)
                            {
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay, 41 });
                            }
                            if (blockID == 41)
                            {
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay, 42 });
                            }
                            if (blockID == 42)
                            {
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                            }
                        }
                        #endregion
                        if (nbs.Checked)
                        {
                            if (blockID == 14)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 12 });
                            }
                            if (blockID == 12)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                            }
                        }
                        else if (fnbs.Checked)
                        {
                            if (blockID == 14)
                            {
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay, 12 });
                            }
                            if (blockID == 12)
                            {
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                            }
                        }
                        if (fax.Checked)
                        {
                            if (blockID == 74)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 70 });
                            }
                            if (blockID == 70)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                            }
                        }
                        else if (faxII.Checked)
                        {
                            if (blockID == 74)
                            {
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay, 70 });
                            }
                            if (blockID == 70)
                            {
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                            }
                        }
                        if (sbeta.Checked)
                        {
                            if (blockID == 38)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 40 });
                            }
                            if (blockID == 40)
                            {
                                Thread.Sleep(thedelay);
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                            }
                        }
                        else if (fsbeta.Checked)
                        {
                            if (blockID == 38)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 40 });
                            }
                            if (blockID == 40)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                            }
                        }

                        if (lavadrawer.Checked)
                        {
                            if (blockID == 369 && !lavaDraw.Checked)
                            {
                                int BGcolor = 574;
                                if (waterchoice2.Checked)
                                {
                                    BGcolor = 530;
                                }
                                for (int i = 0; i < Convert.ToInt32(lavaP.Value); i++)
                                {
                                    con.Send(worldKey, new object[] { 1, ax + i, ay, 0 });
                                    Thread.Sleep(15);
                                    con.Send(worldKey, new object[] { 0, ax + i, ay, 119 });
                                    Thread.Sleep(15);
                                    con.Send(worldKey, new object[] { 1, ax + i, ay, BGcolor });
                                    Thread.Sleep(15);
                                }
                            }
                            else if (blockID == 119 && lavaDraw.Checked)
                            {
                                int BGcolor = 574;
                                if (waterchoice2.Checked)
                                {
                                    BGcolor = 530;
                                }
                                for (int i = 0; i < Convert.ToInt32(lavaP.Value); i++)
                                {
                                    con.Send(worldKey, new object[] { 1, ax + i, ay, 0 });
                                    Thread.Sleep(15);
                                    con.Send(worldKey, new object[] { 0, ax + i, ay, 416 });
                                    Thread.Sleep(15);
                                    con.Send(worldKey, new object[] { 1, ax + i, ay, BGcolor });
                                    Thread.Sleep(15);
                                }
                            }
                        }
                        if (autobuild1.Checked)
                        {
                            #region smiley border
                            if (blockID == 500)
                            {
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                con.Send(worldKey, new object[] { 1, ax, ay, 0 });
                                Thread.Sleep(575);
                                con.Send(worldKey, new object[] { 0, ax, ay, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 1, ay, 29 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax + 2, ay, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax + 3, ay, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 4, ay, 29 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 5, ay, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay + 1, 46 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 5, ay + 1, 46 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax - 1, ay + 1, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax + 6, ay + 1, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 2, ay + 1, 144 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 7, ay + 1, 144 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 2, ay + 2, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 7, ay + 2, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 3, ay + 2, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 8, ay + 2, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 3, ay + 3, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 8, ay + 3, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 4, ay + 3, 144 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 9, ay + 3, 144 });
                                Thread.Sleep(12);
                                // Separation //////////////////////////////////
                                con.Send(worldKey, new object[] { 1, ax - 4, ay + 4, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax + 9, ay + 4, 540 });
                                Thread.Sleep(12);
                                ////////////////////////////////////////////////
                                con.Send(worldKey, new object[] { 0, ax - 4, ay + 5, 46 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 9, ay + 5, 46 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 5, ay + 5, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 10, ay + 5, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 5, ay + 6, 29 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 10, ay + 6, 29 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax - 5, ay + 7, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax + 10, ay + 7, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax - 5, ay + 8, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax + 10, ay + 8, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 5, ay + 9, 29 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 10, ay + 9, 29 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 5, ay + 10, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 10, ay + 10, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 4, ay + 10, 46 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 9, ay + 10, 46 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax - 4, ay + 11, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax + 9, ay + 11, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 4, ay + 12, 144 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 9, ay + 12, 144 });
                                Thread.Sleep(12);
                                //////////////////////////////////////////////////////////////////
                                con.Send(worldKey, new object[] { 0, ax - 3, ay + 12, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 8, ay + 12, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 3, ay + 13, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 8, ay + 13, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 2, ay + 13, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 7, ay + 13, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 2, ay + 14, 144 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 7, ay + 14, 144 });
                                //////////////////////////////////////////////////////////////////
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax - 1, ay + 14, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax + 6, ay + 14, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay + 14, 46 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 5, ay + 14, 46 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay + 15, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 5, ay + 15, 92 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 1, ay + 15, 29 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 4, ay + 15, 29 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax + 2, ay + 15, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax + 3, ay + 15, 540 });
                            }
                            #endregion
                        }
                        #endregion
                    }

                    break;
                case "m":
                    if (botFullyConnected)
                    {
                        int X = Convert.ToInt32(Convert.ToDouble(m[1]) / 16);
                        int Y = Convert.ToInt32(Convert.ToDouble(m[2]) / 16);
                        int m1 = Convert.ToInt32(Convert.ToDecimal(m[1]) / 16);
                        int m2 = Convert.ToInt32(Convert.ToDecimal(m[2]) / 16);
                        int m5 = Convert.ToInt32(Math.Round(Convert.ToDecimal(m[5]), 0));
                        int m6 = Convert.ToInt32(Math.Round(Convert.ToDecimal(m[6]), 0));
                        int xp = m1 + m5;
                        int yp = m2 + m6;
                        
                        if (alstalking.Checked == true)
                        {
                            if (names.ContainsKey(m.GetInt(0)))
                            {
                                if (names.Count > 1)
                                {
                                    if (stalkMover.Text == names[m.GetInt(0)])
                                    {
                                        ThreadPool.QueueUserWorkItem(delegate
                                        {
                                            con.Send("m", m.GetDouble(1), m.GetDouble(2), m.GetDouble(3), m.GetDouble(4), m.GetDouble(5), m.GetDouble(6), m.GetDouble(7), m.GetDouble(8), m.GetInt(9), m.GetBoolean(10));
                                            Thread.Sleep(125);
                                        });
                                    }
                                }
                            }
                        }
                    }

                    break;
                case "god":
                    break;
                case "say":
                    if (botFullyConnected)
                    {
                        str = m.GetString(1);

                        if (m.GetInt(0) != botid)
                        {
                            if (names.ContainsKey(m.GetInt(0)))
                            {
                                chatbox.Items.Add(names[m.GetInt(0)] + ": " + m.GetString(1));

                                if (str.StartsWith("!ch "))
                                {
                                    string userInput = str.Substring(4);

                                    if (cleverbotCBOX.Checked)
                                    {
                                        if (botSession != null)
                                        {
                                            ThreadPool.QueueUserWorkItem(delegate
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + " [RClever42] " + botSession.Think(userInput));
                                            });
                                        }

                                        //if (Voids.CleverBot.IsWelcoming(userInput) && !Voids.CleverBot.IsInsulting(userInput))
                                        //{
                                        //    con.Send("say", "[RClever42] " + names[m.GetInt(0)].ToUpper() + ": Hi to you too!");
                                        //}
                                        //else if (Voids.CleverBot.IsWelcoming(userInput) && Voids.CleverBot.IsInsulting(userInput))
                                        //{
                                        //    con.Send("say", "[RClever42] " + names[m.GetInt(0)].ToUpper() + ": Stop insulting humans!");
                                        //}
                                        //else if (!Voids.CleverBot.IsWelcoming(userInput) && Voids.CleverBot.IsInsulting(userInput))
                                        //{
                                        //    con.Send("say", "[RClever42] " + names[m.GetInt(0)].ToUpper() + ": Insulting a human as '" + userInput + "' isn't a nice thing to do.");
                                        //}
                                        //else if (Voids.CleverBot.IsWelcoming(userInput) && Voids.CleverBot.IsInsultingBot(userInput))
                                        //{
                                        //    con.Send("say", "[RClever42] " + names[m.GetInt(0)].ToUpper() + ": Don't insult me, c'mon.");
                                        //}
                                        //else if (!Voids.CleverBot.IsWelcoming(userInput) && Voids.CleverBot.IsInsultingBot(userInput))
                                        //{
                                        //    con.Send("say", "[RClever42] " + names[m.GetInt(0)].ToUpper() + ": You deserve a life.");
                                        //}
                                        //else if (Voids.CleverBot.HasMath(userInput))
                                        //{
                                        //    if (Voids.CleverBot.Operation(userInput) == "notMath")
                                        //    {
                                        //        con.Send("say", "[RClever42] " + names[m.GetInt(0)].ToUpper() + ": That isn't math! You see, even machines know more.");
                                        //    }
                                        //    else
                                        //    {
                                        //        con.Send("say", "[RClever42] " + names[m.GetInt(0)].ToUpper() + ": That gives " + Voids.CleverBot.Operation(userInput) + "!");
                                        //    }
                                        //}
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                    }
                                }
                                else if (str.StartsWith("!autokick "))
                                {
                                    string[] option = str.Split(' ');
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        if (noRespawn.Checked)
                                        {
                                            if (warningGiver.Checked)
                                            {
                                                int warnumber = Convert.ToInt32(textBox1.Text);
                                                if (player[m.GetInt(0)].warnings > warnumber)
                                                {
                                                    if (bwl.Checked)
                                                    {
                                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " Warning limit reached! You are getting banned.");
                                                        Thread.Sleep(575);
                                                        bansList.Items.Add(names[m.GetInt(0)]);
                                                        banreassons.Items.Add("Warning limit for 'autokick' reached.");
                                                        con.Send("say", "/kick " + names[m.GetInt(0)]);
                                                    }
                                                    else
                                                    {
                                                        con.Send("say", "/kick " + names[m.GetInt(0)] + " Warning limit reached!");
                                                    }
                                                }
                                                else
                                                {
                                                    player[m.GetInt(0)].warnings = player[m.GetInt(0)].warnings + 1;
                                                    Thread.Sleep(250);
                                                    con.Send("say", names[m.GetInt(0)].ToUpper() + ": Please don't use /respawn. Warning " + player[m.GetInt(0)].warnings + " out of " + textBox1.Text + ".");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "/kick " + names[m.GetInt(0)] + " Please don't use /respawn command!");
                                            }
                                        }
                                        else
                                        {
                                            if (option[1] == "true" || option[1] == "on" || option[1] == "yes")
                                            {
                                                if (autokickallowd.Checked)
                                                {
                                                    if (autokickvalue.Checked)
                                                    {
                                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  AutoKick is already turned ON.");
                                                    }
                                                    else
                                                    {
                                                        autokickvalue.Checked = true;
                                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  AutoKick turned ON.");
                                                        #region BOT LOG
                                                        DefineLogZones();
                                                        Thread.Sleep(250);
                                                        log1.Text = "1. " + names[m.GetInt(0)].ToUpper() + " enabled autokick.";
                                                        #endregion
                                                    }
                                                }
                                                else
                                                {
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + "  AutoKick isn't authorized.");
                                                }
                                            }
                                            else if (option[1] == "false" || option[1] == "off" || option[1] == "no")
                                            {
                                                if (autokickallowd.Checked)
                                                {
                                                    if (autokickvalue.Checked)
                                                    {
                                                        autokickvalue.Checked = false;
                                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  AutoKick turned OFF.");
                                                        #region BOT LOG
                                                        DefineLogZones();
                                                        Thread.Sleep(250);
                                                        log1.Text = "1. " + names[m.GetInt(0)].ToUpper() + " " + GetLangFile(100);
                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  AutoKick is already turned OFF.");
                                                    }
                                                }
                                                else
                                                {
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + "  AutoKick isn't allowed by whoever is using this bot.");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + "  Option doesn't exist or option misspellen.");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + GetLangFile(102));
                                    }
                                }
                                else if (str.StartsWith("!kick "))
                                {
                                    if (kickCbox.Checked)
                                    {
                                        if (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)]))
                                        {
                                            string cmdPar = str.Substring(7);
                                            if (cmdPar.Length > 1)
                                            {
                                                string[] aaa = cmdPar.Split(' ');
                                                string[] fullSource = str.Split(' ');
                                                string kicking = fullSource[1].ToLower();

                                                if (names.ContainsValue(kicking) || names.ContainsValue(kicking.ToUpper()))
                                                {
                                                    if (!Admins.Items.Contains(kicking) && !Admins.Items.Contains(kicking.ToUpper()))
                                                    {
                                                        string sample = cmdPar.Replace("!kick ", "");
                                                        string reasson = "";

                                                        if (sample.Length > kicking.Length)
                                                            reasson = sample.Substring(kicking.Length);

                                                        if (reasson == "" || reasson == " ")
                                                        {
                                                            if (!IsMod(names[m.GetInt(0)]))
                                                            {
                                                                reasson = "The bot admin " + names[m.GetInt(0)] + " has kicked you.";
                                                            }
                                                            else
                                                            {
                                                                reasson = "The bot moderator " + names[m.GetInt(0)] + " has kicked you.";
                                                            }
                                                        }

                                                        con.Send("say", "/kick " + kicking + " [R42Bot++] " + reasson);
                                                        #region BOT LOG
                                                        DefineLogZones();
                                                        Thread.Sleep(250);
                                                        log1.Text = "1. " + names[m.GetInt(0)].ToUpper() + " kicked " + kicking + ".";
                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  You can't kick admins.");
                                                    }
                                                }
                                                else
                                                {
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + "  Unknown username.");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + "  Command not used correctly.");
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + GetLangFile(102));
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                    }
                                }
                                else if (str.StartsWith("!ban "))
                                {
                                    if (banCbox.Checked)
                                    {
                                        bool power = false;

                                        if (bfmods.Checked && (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)])))
                                        {
                                            power = true;
                                        }
                                        else if (IsAdmin(names[m.GetInt(0)]))
                                        {
                                            power = true;
                                        }

                                        if (power)
                                        {
                                            string cmdPar = str.Substring(6);
                                            if (cmdPar.Length > 1)
                                            {
                                                string[] aaa = cmdPar.Split(' ');
                                                string[] fullSource = str.Split(' ');
                                                string kicking = fullSource[1].ToLower();

                                                if (names.ContainsValue(kicking) || names.ContainsValue(kicking.ToUpper()))
                                                {
                                                    if (!Admins.Items.Contains(kicking) && !Admins.Items.Contains(kicking.ToUpper()))
                                                    {
                                                        string sample = cmdPar.Replace("!ban ", "");
                                                        string reasson = "";

                                                        if (sample.Length > kicking.Length)
                                                            reasson = sample.Substring(kicking.Length);

                                                        if (reasson == "" || reasson == " ")
                                                        {
                                                            if (!IsMod(names[m.GetInt(0)]))
                                                            {
                                                                reasson = "The bot admin " + names[m.GetInt(0)] + " has banned you.";
                                                            }
                                                            else
                                                            {
                                                                reasson = "The bot moderator " + names[m.GetInt(0)] + " has banned you.";
                                                            }
                                                        }

                                                        con.Send("say", "/kick " + kicking + " [R42Bot++] " + reasson);
                                                        bansList.Items.Add(kicking);
                                                        banreassons.Items.Add(reasson);
                                                        #region BOT LOG
                                                        DefineLogZones();
                                                        Thread.Sleep(250);
                                                        log1.Text = "1. " + names[m.GetInt(0)].ToUpper() + " banned " + kicking + ".";
                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  You can't ban admins.");
                                                    }
                                                }
                                                else
                                                {
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + "  Unknown username.");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + "  Command not used correctly.");
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + GetLangFile(102));
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                    }
                                }
                                else if (str.StartsWith("!revert "))
                                {
                                    if (revertCboxLOL.Checked)
                                    {
                                        if (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)]))
                                        {
                                            string[] me = str.Split(' ');
                                            string revertin = me[1].ToLower();
                                            bool found = false;
                                            int id = 0;
                                            for (int i = 0; i < player.Length; i++)
                                            {
                                                if (player[i].username == revertin)
                                                {
                                                    found = true;
                                                    id = player[i].userid;
                                                    break;
                                                }
                                            }
                                            if (found)
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + " Reverting " + revertin);
                                                if (player[id].blocks.Count > 0)
                                                {
                                                    if (player.Length > 1)
                                                    {
                                                        for (int x = 0; x < player[id].blocks.Count; x++)
                                                        {
                                                            con.Send(worldKey, new object[] { player[id].blocks[x][3], player[id].blocks[x][0], player[id].blocks[x][1], 0 });
                                                            player[id].blocks.Remove(player[id].blocks[x]);
                                                            Thread.Sleep(18);
                                                        }
                                                    }
                                                }
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + " Reverting '" + revertin + "' done.");
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + " '" + revertin + "' not found.");
                                            }
                                            Thread.Sleep(250);
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  Done reverting [" + revertin + "]!");
                                            #region BOT LOG
                                            DefineLogZones();
                                            Thread.Sleep(250);
                                            log1.Text = "1. " + names[m.GetInt(0)].ToUpper() + " reverted " + Voids.Shortest(revertin).ToUpper() + "'s work.";
                                            #endregion
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + GetLangFile(102));
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                    }
                                }
                                else if (str.StartsWith("!snakespeed "))
                                {
                                    if (revertCboxLOL.Checked)
                                    {
                                        if (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)]))
                                        {
                                            string[] me = str.Split(' ');
                                            string valu = me[1];
                                            bool Can = false;

                                            try
                                            {
                                                int aa = Convert.ToInt32(valu);
                                                Can = true;
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                            }

                                            if (Can)
                                            {
                                                if (Convert.ToInt32(valu) < numericUpDown1.Minimum || Convert.ToInt32(valu) > numericUpDown1.Maximum)
                                                {
                                                    Thread.Sleep(200);
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + "  Snake speed " + valu + " is not accepted.");
                                                    Thread.Sleep(200);
                                                }
                                                else
                                                {
                                                    decimal value = Convert.ToDecimal(valu);
                                                    numericUpDown1.Value = value;
                                                    #region BOT LOG
                                                    DefineLogZones();
                                                    Thread.Sleep(250);
                                                    log1.Text = "1. " + names[m.GetInt(0)].ToUpper() + " changed snake speed to " + valu + "ms.";
                                                    #endregion
                                                    Thread.Sleep(200);
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + "  Snake speed changed to " + valu);
                                                    Thread.Sleep(200);
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + "  Value was not a number.");
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + GetLangFile(102));
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                    }
                                }
                                else if (str.StartsWith("!name "))
                                {
                                    if (krockhateseers.Checked)
                                    {
                                        bool power = false;

                                        if (nfmods.Checked && (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)])))
                                        {
                                            power = true;
                                        }
                                        else if (IsAdmin(names[m.GetInt(0)]))
                                        {
                                            power = true;
                                        }

                                        if (power)
                                        {
                                            string NewName = str.Substring(6);
                                            con.Send("name", NewName);
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(102));
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                    }
                                }
                                else if (str.StartsWith("!admins"))
                                {
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        string admins = "";

                                        foreach (string namez in Admins.Items)
                                        {
                                            admins += namez.ToUpper() + ",";
                                        }

                                        if (admins == "")
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " [R42Bot++] Bot admins:");
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " No one.");
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " [R42Bot++] Bot admins:");
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " " + admins);
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  You can't see bot admins cause you are not an admin!");
                                    }
                                }
                                else if (str.StartsWith("!microphone "))
                                {
                                    string begin = str.Substring(0, 12);
                                    string message = str.Replace(begin, "");
                                    if (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)]))
                                    {
                                        //con.Send("say", "[R42Bot++] Listen Everybody!");
                                        //Thread.Sleep(1000);
                                        //con.Send("say", "[R42Bot++] Please Listen!!!");
                                        Thread.Sleep(1000);
                                        con.Send("say", "[R42Bot++] " + message);
                                        Thread.Sleep(1000);
                                        con.Send("say", "[R42Bot++] " + message);
                                        Thread.Sleep(575);
                                        con.Send("say", "[R42Bot++] " + message);
                                        Thread.Sleep(575);
                                        con.Send("say", "[R42Bot++] " + message);
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " [R42Bot++] You are not an admin in the bot!");
                                    }
                                }
                                else if (str.StartsWith("!stalk "))
                                {
                                    string[] userinuse = str.Split(' ');
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        if (alstalking.Checked)
                                        {
                                            if (names.ContainsValue(userinuse[1].ToLower()))
                                            {
                                                if (stalkMover.Text != userinuse[1].ToLower())
                                                {
                                                    #region BOT LOG
                                                    DefineLogZones();
                                                    Thread.Sleep(250);
                                                    log1.Text = "1. " + names[m.GetInt(0)].ToUpper() + " made bot stalk " + userinuse[1].ToLower().ToUpper() + ".";
                                                    #endregion
                                                    stalkMover.Text = userinuse[1].ToLower();
                                                    Thread.Sleep(255);
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + " Successfully started stalking " + userinuse[1].ToUpper() + ".");
                                                }
                                                else
                                                {
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + userinuse[1].ToUpper() + " is already being stalked!");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + userinuse[1].ToUpper() + " is not in this world!");
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                        }
                                    }
                                    else
                                    {
                                        if (userinuse[1].ToLower() == botName.ToLower())
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + userinuse[1].ToUpper() + " is the bot and you can't make bot stalk people since you are not an admin in the bot!");
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + userinuse[1].ToUpper() + " can't be stalked because you are not an admin!");
                                        }
                                        // if not an admin
                                    }
                                }
                                else if (str.StartsWith("!unstalk"))
                                {
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        stalkMover.Clear();
                                        Thread.Sleep(255);
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " Successfully stoped stalking.");
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " You aren't an admin!");
                                    }
                                }
                                else if (str.StartsWith("!portal"))
                                {
                                    if (portalplac.Checked || portalplac1.Checked)
                                    {
                                        if (IsAdmin(names[m.GetInt(0)]))
                                        {
                                            string[] list = str.Split(' ');
                                            if (list.Length > 2)
                                            {
                                                string issue = "";
                                                try
                                                {
                                                    int omg = Convert.ToInt32(list[1]);
                                                }
                                                catch (Exception ex)
                                                {
                                                    issue = "ID is an invalid number.";
                                                    Console.WriteLine(ex.Message);
                                                }
                                                try
                                                {
                                                    int omg2 = Convert.ToInt32(list[2]);
                                                }
                                                catch (Exception ex)
                                                {
                                                    issue = "TARGET is an invalid number.";
                                                    Console.WriteLine(ex.Message);
                                                }
                                                if (issue == "")
                                                {
                                                    BPortalID = Convert.ToInt32(list[1]);
                                                    BPortalTARGET = Convert.ToInt32(list[2]);

                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + " Portal coordinates changed to ID '" + list[1] + "' and TARGET '" + list[2] + "'");
                                                }
                                                else
                                                {
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + " " + issue);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " You aren't an admin!");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                    }
                                }
                                else if (str.StartsWith("!topwin"))
                                {
                                    if (winsystem1.Checked == true)
                                    {
                                        string top = "nobody";
                                        int topI = 0;

                                        #region for...
                                        for (int xml = 0; xml < player.Length; xml++)
                                        {
                                            if (player[xml].wins > topI)
                                            {
                                                topI = player[xml].wins;
                                                top = player[xml].username;
                                            }
                                        }
                                        #endregion
                                        
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " Top winner: [" + top + ", " + topI.ToString() + " wins]");
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                    }
                                }
                                else if (str.StartsWith("!mywins"))
                                {
                                    if (winsystem1.Checked == true)
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " You won " + player[m.GetInt(0)].wins + " times.");
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                    }
                                }
                                else if (str.StartsWith("!getwins"))
                                {
                                    string[] ls = str.Split(' ');
                                    if (ls.Length > 1)
                                    {
                                        string user = ls[1];
                                        int id = -2;

                                        if (winsystem1.Checked == true)
                                        {
                                            for (int i = 0; i < player.Length; i++)
                                            {
                                                if (player[i].username == user.ToLower())
                                                {
                                                    id = player[i].userid;
                                                    break;
                                                }
                                            }
                                            if (id != -2)
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + " " + user + " won " + player[id].wins + " times.");
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + " User '" + user + "' not found.");
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " Please tell wich user!");
                                    }
                                }
                                else if (str.StartsWith("!say "))
                                {
                                    if (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)]))
                                    {
                                        string beginz = str.Substring(0, 5);
                                        string endz = str.Replace(beginz, "");
                                        con.Send("say", "[R42Bot++] " + endz);
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  You are not an admin in the bot!");
                                    }
                                }
                                else if (str.StartsWith("!amiadmin"))
                                {
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  Yes, you are an admin in the bot.");
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  No, you aren't an admin in the bot.");
                                    }
                                }
                                else if (str.StartsWith("!amimod"))
                                {
                                    if (IsMod(names[m.GetInt(0)]))
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  Yes, you are a moderator in the bot.");
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  No, you aren't a moderator in the bot.");
                                    }
                                }
                                #region is [player]...
                                else if (str.StartsWith("!is "))
                                {
                                    string[] userinuse = str.Split(' ');
                                    if (str.StartsWith("!is " + userinuse[1] + " admin"))
                                    {
                                        if (Admins.Items.Contains(userinuse[1].ToLower()))
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  Yes, " + userinuse[1].ToUpper() + " is an admin in the bot.");
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  No, " + userinuse[1].ToLower() + " is not an admin in the bot.");
                                        }
                                    }
                                    else if (str.StartsWith("!is " + userinuse[1] + " mod"))
                                    {
                                        if (IsMod(userinuse[1].ToLower()))
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  Yes, " + userinuse[1].ToUpper() + " is a moderator in the bot.");
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  No, " + userinuse[1].ToLower() + " is not a moderator in the bot.");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  Unknown question or misspellen question.");
                                    }
                                }
                                #endregion
                                else if (str.StartsWith("!botlog"))
                                {
                                    if (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)]))
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " ===-===-===-===");
                                        Thread.Sleep(575);
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " ==---BOT_LOG---==");
                                        Thread.Sleep(575);
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " ===-===-===-===");
                                        Thread.Sleep(575);
                                        if (log1.Text != "")
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " " + log1.Text);
                                            Thread.Sleep(575);
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " 1. Empty");
                                            Thread.Sleep(575);
                                        }
                                        if (log2.Text != "")
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " " + log2.Text);
                                            Thread.Sleep(575);
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " 2. Empty");
                                            Thread.Sleep(575);
                                        }
                                        if (log3.Text != "")
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " " + log3.Text);
                                            Thread.Sleep(575);
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " 3. Empty");
                                            Thread.Sleep(575);
                                        }
                                        if (log4.Text != "")
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " " + log4.Text);
                                            Thread.Sleep(575);
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " 4. Empty");
                                            Thread.Sleep(575);
                                        }
                                        if (log5.Text != "")
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " " + log5.Text);
                                            Thread.Sleep(575);
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " 5. Empty");
                                            Thread.Sleep(575);
                                        }
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " ===-===-===-===");
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " To view bot log you must be an admin in the bot!");
                                    }
                                }

                                #region POLL COMMANDS

                                else if (str.StartsWith("!vote "))
                                {
                                    if (votersList.Items.Contains(names[m.GetInt(0)]))
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  You've already voted!");
                                    }
                                    else if (pollname.Text == "")
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  No poll is currently in progress!");
                                    }
                                    else
                                    {
                                        if (names[m.GetInt(0)] == pollstartername.Text)
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  You can't vote because you made the poll!");
                                        }
                                        else
                                        {
                                            string[] voted = str.Split(' ');
                                            if (voted[1] == choice1.Text.ToLower() || voted[1] == choice1.Text)
                                            {
                                                votersList.Items.Add(names[m.GetInt(0)]);
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + "  You voted " + choice1.Text + ".");
                                                int votes1 = Convert.ToInt32(vot1.Text);
                                                votes1 = votes1 + 1;
                                                Thread.Sleep(250);
                                                string nuvots = Convert.ToString(votes1);
                                                vot1.Text = nuvots;
                                            }
                                            else if (voted[1] == choice2.Text.ToLower() || voted[1] == choice2.Text)
                                            {
                                                votersList.Items.Add(names[m.GetInt(0)]);
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + "  You voted " + choice2.Text + ".");
                                                int votes2 = Convert.ToInt32(vot2.Text);
                                                votes2 = votes2 + 1;
                                                Thread.Sleep(250);
                                                string nuvots = Convert.ToString(votes2);
                                                vot2.Text = nuvots;
                                            }
                                            else if (voted[1] == choice3.Text.ToLower() || voted[1] == choice3.Text)
                                            {
                                                votersList.Items.Add(names[m.GetInt(0)]);
                                                if (choice3.Visible == true)
                                                {
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + "  You voted " + choice3.Text + ".");
                                                    int votes3 = Convert.ToInt32(vot3.Text);
                                                    votes3 = votes3 + 1;
                                                    Thread.Sleep(250);
                                                    string nuvots = Convert.ToString(votes3);
                                                    vot3.Text = nuvots;
                                                }
                                                else
                                                {
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + "  Tirth option has been removed by whoever is using this bot.");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + "  Unknown Option for voting polls.");
                                            }
                                        }
                                    }
                                }
                                else if (str.StartsWith("!pc1 "))
                                {
                                    string[] choiced = str.Split(' ');
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        if (pollname.Text == "")
                                        {
                                            if (choice1.Text == choiced[1])
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + "  The first choice is the same as the new one!");
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + "  Choice1 changed to " + choiced[1] + ".");
                                                choice1.Text = choiced[1];
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  There is an poll in progress!");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  You aren't an admin in the bot so you can't change the bot's poll choices!");
                                    }
                                }
                                else if (str.StartsWith("!pc2 "))
                                {
                                    string[] choiced = str.Split(' ');
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        if (pollname.Text == "")
                                        {
                                            if (choice2.Text == choiced[1])
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + "  The second choice is the same as the new one!");
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + "  Choice2 changed to " + choiced[1] + ".");
                                                choice2.Text = choiced[1];
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  There is a poll in progress!");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  You aren't an admin in the bot so you can't change the bot's poll choices!");
                                    }
                                }
                                else if (str.StartsWith("!pc3 "))
                                {
                                    string[] choiced = str.Split(' ');
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        if (pollname.Text == "")
                                        {
                                            if (choice3.Text == choiced[1])
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + "  The tirth choice is the same as the new one!");
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + "  Choice3 changed to " + choiced[1] + ".");
                                                choice3.Text = choiced[1];
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  There is a poll in progress!");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  You aren't an admin in the bot so you can't change the bot's poll choices!");
                                    }
                                }
                                else if (str.StartsWith("!endpoll"))
                                {
                                    if (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)]))
                                    {
                                        if (pollname.Text == "")
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " There are no polls at progress.");
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " Poll '" + pollname.Text + "' stoped.");
                                            if (vot3.Visible == false)
                                            {
                                                Thread.Sleep(575);
                                                con.Send("say", "[R42Bot++] Poll Results: " + choice1.Text + " - " + vot1.Text + " & " + choice2.Text + " - " + vot2.Text);
                                            }
                                            else
                                            {
                                                Thread.Sleep(575);
                                                con.Send("say", "[R42Bot++] Poll Results: " + choice1.Text + " - " + vot1.Text + " , " + choice2.Text + " - " + vot2.Text + " & " + choice3.Text + " - " + vot3.Text);
                                            }
                                            Thread.Sleep(250);
                                            vot1.Text = "0";
                                            vot2.Text = "0";
                                            vot3.Text = "0";
                                            pollname.Text = "";
                                            pollstartername.Text = "";
                                            votersList.Items.Clear();
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  You aren't an admin in the bot so you can't end polls!");
                                    }
                                }
                                else if (str.StartsWith("!poll "))
                                {
                                    if (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)]))
                                    {
                                        if (pollname.Text == "")
                                        {
                                            if (vot3.Visible == false)
                                            {
                                                string beginz = str.Substring(0, 6);
                                                string endz = str.Replace(beginz, "");

                                                pollname.Text = endz;
                                                pollstartername.Text = names[m.GetInt(0)];
                                                Thread.Sleep(575);
                                                con.Send("say", "[R42Bot++] Everyone! Answer the following poll.");
                                                Thread.Sleep(575);
                                                con.Send("say", "[R42Bot++] '" + endz + "'.");
                                                Thread.Sleep(575);
                                                con.Send("say", "[R42Bot++]  Options: " + choice1.Text + " and " + choice2.Text + ". Do !vote [option] to vote!");
                                            }
                                            else
                                            {
                                                string beginz = str.Substring(0, 5);
                                                string endz = str.Replace(beginz, "");

                                                pollname.Text = endz;
                                                pollstartername.Text = names[m.GetInt(0)];
                                                Thread.Sleep(575);
                                                con.Send("say", "[R42Bot++] Everyone! Answer the following poll.");
                                                Thread.Sleep(575);
                                                con.Send("say", "[R42Bot++] '" + endz + "'.");
                                                Thread.Sleep(575);
                                                con.Send("say", "[R42Bot++]  Options: " + choice1.Text + " , " + choice2.Text + " and " + choice3.Text + ". Do !vote [option] to vote!");
                                            }
                                        }
                                        else
                                        {
                                            Thread.Sleep(575);
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  There is already an poll in progress!");
                                        }
                                    }
                                    else
                                    {
                                        Thread.Sleep(575);
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  You can't start polls cause you aren't an admin!");
                                    }
                                }
                                else if (str.StartsWith("!pollhelp"))
                                {
                                    con.Send("say", "/pm " + names[m.GetInt(0)] + " !vote [option], !poll [name], !endpoll,");
                                    Thread.Sleep(575);
                                    con.Send("say", "/pm " + names[m.GetInt(0)] + " !pc1 [choice1], !pc2 [choice2] and !pc3 [choice3].");
                                }
                                #endregion
                                else if (str.StartsWith("!giveeditall"))
                                {
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        foreach (Player s in player)
                                        {
                                            con.Send("say", "/giveedit " + s.username);
                                            Thread.Sleep(575);
                                        }
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " Done giving edit.");
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  You can't give everyone edit cause you are not an admin in the bot!");
                                    }
                                }
                                else if (str.StartsWith("!removeditall"))
                                {
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        foreach (Player s in player)
                                        {
                                            con.Send("say", "/removeedit " + s.username);
                                            Thread.Sleep(575);
                                        }
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " Done removing edit.");
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " You can't remove everyone's edit cause you are not an admin in the bot!");
                                    }
                                }
                                else if (str.StartsWith("!loadlevel"))
                                {
                                    if (loadlevelCbox.Checked)
                                    {
                                        bool power = false;

                                        if (rrlfmods.Checked && (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)])))
                                        {
                                            power = true;
                                        }
                                        else if (IsAdmin(names[m.GetInt(0)]))
                                        {
                                            power = true;
                                        }

                                        if (power)
                                        {
                                            con.Send("say", "/loadlevel");
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " [R42Bot++] level loaded.");
                                            #region BOT LOG
                                            DefineLogZones();
                                            Thread.Sleep(250);
                                            log1.Text = "1. " + names[m.GetInt(0)].ToUpper() + " loaded the level.";
                                            #endregion
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + GetLangFile(102));
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                    }
                                }
                                else if (str.StartsWith("!reset"))
                                {
                                    if (resetCBOX.Checked)
                                    {
                                        bool power = false;

                                        if (rrlfmods.Checked && (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)])))
                                        {
                                            power = true;
                                        }
                                        else if (IsAdmin(names[m.GetInt(0)]))
                                        {
                                            power = true;
                                        }

                                        if (power)
                                        {
                                            con.Send("say", "/reset");
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " [R42Bot++] reseted.");
                                            #region BOT LOG
                                            DefineLogZones();
                                            Thread.Sleep(250);
                                            log1.Text = "1. " + names[m.GetInt(0)].ToUpper() + " reseted the players.";
                                            #endregion
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + GetLangFile(102));
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                    }
                                }
                                else if (str.StartsWith("!reload"))
                                {
                                    if (reloadCBox.Checked)
                                    {
                                        bool power = false;

                                        if (rrlfmods.Checked && (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)])))
                                        {
                                            power = true;
                                        }
                                        else if (IsAdmin(names[m.GetInt(0)]))
                                        {
                                            power = true;
                                        }

                                        if (power)
                                        {
                                            con.Send("say", "/loadlevel");
                                            Thread.Sleep(275);
                                            con.Send("say", "/reset");
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " [R42Bot++] Level reloaded.");
                                            #region BOT LOG
                                            DefineLogZones();
                                            Thread.Sleep(250);
                                            log1.Text = "1. " + names[m.GetInt(0)].ToUpper() + " reloaded the level.";
                                            #endregion
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + GetLangFile(102));
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                    }
                                }
                                else if (str.StartsWith("!save"))
                                {
                                    if (saveCbox.Checked)
                                    {
                                        bool power = false;

                                        if (sfmods.Checked && (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)])))
                                        {
                                            power = true;
                                        }
                                        else if (IsAdmin(names[m.GetInt(0)]))
                                        {
                                            power = true;
                                        }

                                        if (power)
                                        {
                                            con.Send("save");
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " [R42Bot++] level saved.");
                                            #region BOT LOG
                                            DefineLogZones();
                                            Thread.Sleep(250);
                                            log1.Text = "1. " + names[m.GetInt(0)].ToUpper() + " saved the level.";
                                            #endregion
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + GetLangFile(102));
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                    }
                                }
                                else if (str.StartsWith("!clear"))
                                {
                                    if (clearCbox.Checked)
                                    {
                                        bool power = false;

                                        if (cfmods.Checked && (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)])))
                                        {
                                            power = true;
                                        }
                                        else if (IsAdmin(names[m.GetInt(0)]))
                                        {
                                            power = true;
                                        }

                                        if (power)
                                        {
                                            con.Send("clear");
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " [R42Bot++] level cleared.");
                                            #region BOT LOG
                                            DefineLogZones();
                                            Thread.Sleep(250);
                                            log1.Text = "1. " + names[m.GetInt(0)].ToUpper() + " cleared the level.";
                                            #endregion
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + GetLangFile(102));
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                    }
                                }
                                else if (str.StartsWith("!download"))
                                {
                                    con.Send("say", "/pm " + names[m.GetInt(0)] + " http://realmaster42-projects.weebly.com/r42bot1.html");
                                }

                                else if (str.StartsWith("!listhelp")) 
                                {
                                    if (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)]))
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !amiadmin, !amimod, !botlog, !kick [player] [reasson],");
                                        Thread.Sleep(575);
                                    }
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !save, !loadlevel, !clear, !ch [msg], !evenhelp c:");
                                    }
                                    //else if (!IsMod(names[m.GetInt(0)]))
                                    //{
                                    //    con.Send("say", "/pm " + names[m.GetInt(0)] + " !amiadmin, !amimod, !botlog, !ch [msg], !evenhelp c:");
                                    //}
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !amiadmin, !amimod, !ch [msg], !evenhelp c:");
                                    }
                                }
                                else if (str.StartsWith("!evenhelp"))
                                {
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !revert [player], !snakespeed [speed_in_ms], !portal [id] [target].");
                                        Thread.Sleep(575);
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " HOORAY!");
                                    }
                                    else if (IsMod(names[m.GetInt(0)]))
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !revert [player], !snakespeed [speed_in_ms]. HOORAY!");
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(102));
                                    }
                                }
                                else if (str.StartsWith("!giveedithelp"))
                                {
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " [R42Bot++] !removeditall, !giveeditall");
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(102));
                                    }
                                }
                                else if (str.StartsWith("!specialhelp"))
                                {
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " [R42Bot++] !giveedithelp");
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(102));
                                    }
                                }
                                else if (str.StartsWith("!more"))
                                {
                                    if (IsAdmin(names[m.GetInt(0)]) || IsMod(names[m.GetInt(0)]))
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !listhelp, !specialhelp, !say [msg], ");
                                        Thread.Sleep(575);
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !microphone [msg], !pollhelp, ");
                                        Thread.Sleep(575);
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !is [plr] admin, !is [plr] mod. c:");
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !listhelp, !is [plr] admin, !is [plr] mod. c:");
                                    }
                                }

                                else if (str.StartsWith("!admin "))
                                {
                                    string[] splitstr = str.Split(' ');
                                    if (names[m.GetInt(0)] == worldowner)
                                    {
                                        if (splitstr.Length > 1)
                                        {
                                            if (names.ContainsValue(splitstr[1].ToLower()))
                                            {
                                                if (!Admins.Items.Contains(splitstr[1].ToLower()))
                                                {
                                                    Admins.Items.Add(splitstr[1].ToLower());
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + " User '" + splitstr[1] + "' added to admins.");
                                                    Thread.Sleep(575);
                                                    con.Send("say", "/pm " + splitstr[1].ToLower() + " [R42Bot++] You have been added as an admin.");
                                                }
                                                else
                                                {
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + " User '" + splitstr[1] + "' is already an admin.");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + " User '" + splitstr[1] + "' is not in the world.");
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " Command wasn't used properly.");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(102));
                                    }
                                }
                                else if (str.StartsWith("!unadmin "))
                                {
                                    string[] splitstr = str.Split(' ');
                                    if (names[m.GetInt(0)] == worldowner)
                                    {
                                        if (splitstr.Length > 1)
                                        {
                                            if (names.ContainsValue(splitstr[1].ToLower()))
                                            {
                                                if (Admins.Items.Contains(splitstr[1].ToLower()))
                                                {
                                                    Admins.Items.Remove(splitstr[1].ToLower());
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + " User '" + splitstr[1] + "' removed from admins.");
                                                    Thread.Sleep(575);
                                                    con.Send("say", "/pm " + splitstr[1].ToLower() + " [R42Bot++] You have been removed from being an admin.");
                                                }
                                                else
                                                {
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + " User '" + splitstr[1] + "' is not an admin.");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + " User '" + splitstr[1] + "' is not in the world.");
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " Command wasn't used properly.");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(102));
                                    }
                                }

                                else if (str.StartsWith("!mod "))
                                {
                                    string[] splitstr = str.Split(' ');
                                    if (names[m.GetInt(0)] == worldowner || (IsAdmin(names[m.GetInt(0)]) && adminsgmodc.Checked))
                                    {
                                        if (splitstr.Length > 1)
                                        {
                                            if (names.ContainsValue(splitstr[1].ToLower()))
                                            {
                                                if (!Moderators.Items.Contains(splitstr[1].ToLower()))
                                                {
                                                    Moderators.Items.Add(splitstr[1].ToLower());
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + " User '" + splitstr[1] + "' added to moderators.");
                                                    Thread.Sleep(575);
                                                    con.Send("say", "/pm " + splitstr[1].ToLower() + " [R42Bot++] You have been added as a moderator.");
                                                }
                                                else
                                                {
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + " User '" + splitstr[1] + "' is already a moderator.");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + " User '" + splitstr[1] + "' is not in the world.");
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " Command wasn't used properly.");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(102));
                                    }
                                }
                                else if (str.StartsWith("!unmod "))
                                {
                                    string[] splitstr = str.Split(' ');
                                    if (names[m.GetInt(0)] == worldowner || (IsAdmin(names[m.GetInt(0)]) && adminsgmodc.Checked))
                                    {
                                        if (splitstr.Length > 1)
                                        {
                                            if (names.ContainsValue(splitstr[1].ToLower()))
                                            {
                                                if (Moderators.Items.Contains(splitstr[1].ToLower()))
                                                {
                                                    Moderators.Items.Remove(splitstr[1].ToLower());
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + " User '" + splitstr[1] + "' removed from moderators.");
                                                    Thread.Sleep(575);
                                                    con.Send("say", "/pm " + splitstr[1].ToLower() + " [R42Bot++] You have been removed from being a moderator.");
                                                }
                                                else
                                                {
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + " User '" + splitstr[1] + "' is not a moderator.");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + " User '" + splitstr[1] + "' is not in the world.");
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)] + " Command wasn't used properly.");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(102));
                                    }
                                }

                                #region HELP COMMAND
                                else if (str.StartsWith("!help")) // COMMANDYS COMMANDAS COMMANOS OMG
                                {
                                    con.Send("say", "/pm " + names[m.GetInt(0)].ToUpper() + " [R42Bot++] !more, !stalkhelp, !chelp [cmd], !download,");
                                    Thread.Sleep(575);
                                    if (names[m.GetInt(0)] == worldowner)
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)].ToUpper() + " [R42Bot++] !admin [player], !unadmin [player],");
                                        Thread.Sleep(575);
                                    }
                                    if (IsAdmin(names[m.GetInt(0)]) && adminsgmodc.Checked)
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)].ToUpper() + " [R42Bot++] !mod [player], !unmod [player],");
                                        Thread.Sleep(575);
                                    }
                                    else if (names[m.GetInt(0)] == worldowner)
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)].ToUpper() + " [R42Bot++] !mod [player], !unmod [player],");
                                        Thread.Sleep(575);
                                    }
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)].ToUpper() + " [R42Bot++] !version, !survival [plr], !creative [plr],");
                                        Thread.Sleep(575);
                                        con.Send("say", "/pm " + names[m.GetInt(0)].ToUpper() + " [R42Bot++] !mywins, !topwin, !getwins [plr]. c:");
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)].ToUpper() + " [R42Bot++] !version, !mywins, !topwin, ");
                                        Thread.Sleep(575);
                                        con.Send("say", "/pm " + names[m.GetInt(0)].ToUpper() + " [R42Bot++] !getwins [plr]. c:");
                                    }
                                }
                                else if (str.StartsWith("!stalkhelp"))
                                {
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !stalk [plr], !unstalk [plr]");
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(102));
                                    }
                                }
                                #endregion

                                else if (str.StartsWith("!chelp "))
                                {
                                    string[] command = str.Split(' ');
                                    #region commands
                                    if (command[1] == "chelp")
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !chelp [command]. Makes you know how to use the command and how it works.");
                                    }
                                    else if (command[1] == "help")
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !help. Makes you know the commands available in the bot.");
                                    }
                                    else if (command[1] == "more")
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !more. Makes you know more commands available in the bot.");
                                    }
                                    else if (command[1] == "mywins")
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !mywins. Makes you know your own wins, doesn't work if it is disabled!");
                                    }
                                    else if (command[1] == "amiadmin")
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !amiadmin. Checks and tells you if you are an admin in the bot or not.");
                                    }
                                    else if (command[1] == "amimod")
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !amimod. Checks and tells you if you are a mod in the bot or not.");
                                    }
                                    else if (command[1] == "is admin")
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !is [plr] admin. Checks and tells you if the player is an admin in the bot or not.");
                                    }
                                    else if (command[1] == "is mod")
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !is [plr] mod. Checks and tells you if the player is a mod in the bot or not.");
                                    }
                                    else if (command[1] == "getwins")
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !getwins [plr]. Returns the player's win count.");
                                    }
                                    else if (command[1] == "topwin")
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " !topwin. Returns the biggest amount of wins and who owns them.");
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " Command Mispellen or Unknown command. CommandHelp couldn't recognize that command!");
                                    }
                                    #endregion
                                }
                                else if (str == "!chelp")
                                {
                                    con.Send("say", "/pm " + names[m.GetInt(0)] + " !chelp [command]. Makes you know how to use the command and how it works.");
                                }



                                else if (str.StartsWith("!version"))
                                {
                                    con.Send("say", "/pm " + names[m.GetInt(0)] + " [R42Bot++] Bot version '" + Version.version + "' build " + nBuild + ".");
                                    Thread.Sleep(575);
                                    if (Convert.ToInt32(Version.upgradedBuild) > Convert.ToInt32(nBuild))
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " [R42Bot++] Build outdated. Newest is " + Version.upgradedBuild + ".");
                                    }
                                    else if (Convert.ToInt32(Version.upgradedBuild) < Convert.ToInt32(nBuild))
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " [R42Bot++] Test Environnement. Published latest is " + Version.upgradedBuild + ".");
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " [R42Bot++] Up-to-date.");
                                    }
                                }
                                else if (str.StartsWith("!creative "))
                                {
                                    string[] split = str.Split(' ');
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        if (noRespawn3.Checked)
                                        {
                                            if (warningGiver3.Checked)
                                            {
                                                int warnumber = Convert.ToInt32(limit3.Text);
                                                if (player[m.GetInt(0)].warnings > warnumber)
                                                {
                                                    if (bwl3.Checked)
                                                    {
                                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " Warning limit reached! You are getting banned.");
                                                        Thread.Sleep(575);
                                                        bansList.Items.Add(names[m.GetInt(0)]);
                                                        banreassons.Items.Add("Warning limit for 'creative' reached.");
                                                        con.Send("say", "/kick " + names[m.GetInt(0)]);
                                                    }
                                                    else
                                                    {
                                                        con.Send("say", "/kick " + names[m.GetInt(0)] + " Warning limit reached!");
                                                    }
                                                }
                                                else
                                                {
                                                    player[m.GetInt(0)].warnings = player[m.GetInt(0)].warnings + 1;
                                                    Thread.Sleep(250);
                                                    con.Send("say", names[m.GetInt(0)].ToUpper() + ": Please don't use !creative. Warning " + player[m.GetInt(0)].warnings + " out of " + textBox1.Text + ".");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "/kick " + names[m.GetInt(0)] + " Please don't use !creative command!");
                                            }
                                        }
                                        else
                                        {
                                            if (scommand2.Checked)
                                            {
                                                if (split[1] == "nikooos" && names.ContainsValue("nikooooooos"))
                                                {
                                                    con.Send("say", "/giveedit NIKOOOOOOOS");
                                                    Thread.Sleep(200);
                                                    con.Send("say", "[R42Bot++] NIKOOO(...)s is now in creative mode.");
                                                    Thread.Sleep(200);
                                                    con.Send("say", "/pm NIKOOOOOOOS [R42Bott+] hey... you are now in creative mode.");
                                                    Thread.Sleep(200);
                                                }
                                                else if (names.ContainsValue(split[1].ToLower()))
                                                {
                                                    con.Send("say", "/giveedit " + split[1].ToLower());
                                                    Thread.Sleep(200);
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + split[1].ToUpper() + " is now in creative mode.");
                                                    Thread.Sleep(200);
                                                    con.Send("say", "/pm " + split[1].ToLower() + " [R42Bot++] hey... you are now in creative mode!");
                                                    Thread.Sleep(200);
                                                }
                                                else
                                                {
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + split[1] + " isn't in this world or isn't an valid username.");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + GetLangFile(102));
                                    }
                                }
                                else if (str.StartsWith("!survival "))
                                {
                                    string[] split = str.Split(' ');
                                    if (IsAdmin(names[m.GetInt(0)]))
                                    {
                                        if (noRespawn2.Checked)
                                        {
                                            if (warningGiver2.Checked)
                                            {
                                                int warnumber = Convert.ToInt32(limit2.Text);
                                                if (player[m.GetInt(0)].warnings > warnumber)
                                                {
                                                    if (bwl2.Checked)
                                                    {
                                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " Warning limit reached! You are getting banned.");
                                                        Thread.Sleep(575);
                                                        bansList.Items.Add(names[m.GetInt(0)]);
                                                        banreassons.Items.Add("Warning limit for 'survival' reached.");
                                                        con.Send("say", "/kick " + names[m.GetInt(0)]);
                                                    }
                                                    else
                                                    {
                                                        con.Send("say", "/kick " + names[m.GetInt(0)] + " Warning limit reached!");
                                                    }
                                                }
                                                else
                                                {
                                                    player[m.GetInt(0)].warnings = player[m.GetInt(0)].warnings + 1;
                                                    Thread.Sleep(250);
                                                    con.Send("say", names[m.GetInt(0)].ToUpper() + ": Please don't use /respawn. Warning " + player[m.GetInt(0)].warnings + " out of " + textBox1.Text + ".");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "/kick " + names[m.GetInt(0)] + " Please don't use /respawn command!");
                                            }
                                        }
                                        else
                                        {
                                            if (scommand.Checked)
                                            {
                                                if (split[1] == "nikooooooos" && names.ContainsValue("nikooos"))
                                                {
                                                    con.Send("say", "/removeedit NIKOOOOOOOS");
                                                    con.Send("say", "[R42Bot++] NIKOOO(...)s is now in survival mode.");
                                                    con.Send("say", "/pm NIKOOOOOOOS [R42Bott+] hey... you are now in survival mode.");
                                                }
                                                else if (names.ContainsValue(split[1].ToLower()))
                                                {
                                                    con.Send("say", "/removeedit " + split[1].ToLower());
                                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToUpper() + " " + split[1].ToUpper() + " is now in survival mode.");
                                                    con.Send("say", "/pm " + split[1].ToLower() + " [R42Bot++] hey... you are now in survival mode!");
                                                }
                                                else
                                                {
                                                    con.Send("say", "/pm " + names[m.GetInt(0)] + "  " + split[1].ToUpper() + " isn't in this world or isn't an valid username.");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(100));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)] + " " + GetLangFile(102));
                                    }
                                }
                                else
                                {
                                    if (str.StartsWith("!") && str.Length > 1)
                                    {
                                        con.Send("say", "/pm " + names[m.GetInt(0)].ToUpper() + " [R42Bot++] Command misspelen or Unknown Command.");
                                    }
                                }
                            }
                        }
                        else
                        {
                            chatbox.Items.Add("[R42Bot++]: " + m.GetString(1).Replace("[R42Bot++]", ""));
                        }
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            if (chatbox.Items.Count >= 12)
                            {
                                string one = chatbox.Items[10].ToString();
                                string two = chatbox.Items[11].ToString();
                                if (chatbox.Items.Count >= 13)
                                {
                                    string three = chatbox.Items[12].ToString();
                                    chatbox.Items.Clear();
                                    Thread.Sleep(150);
                                    chatbox.Items.Add(one);
                                    chatbox.Items.Add(two);
                                    chatbox.Items.Add(three);
                                }
                                else
                                {
                                    chatbox.Items.Clear();
                                    Thread.Sleep(150);
                                    chatbox.Items.Add(one);
                                    chatbox.Items.Add(two);
                                }
                            }
                        });
                    }
                    break;
            }
        }

        #region ...
        private void connector_Click(object sender, EventArgs e)
        {
            if (connector.Text == "Connect")
            {
                if ((email.Text == "Email" || email.Text == "Token ID") && (pass.Text == "" && pass.Enabled == true) && idofworld.Text == "world ID")
                {
                    if (enus.Checked == true && ptbr.Checked == false)
                    {
                        if (!isFacebook.Checked)
                        {
                            MessageBox.Show(GetLangFile(76), "R42Bot++ v" + Version.version + " " + GetLangFile(92));
                        }
                        else
                        {
                            MessageBox.Show(GetLangFile(77), "R42Bot++ v" + Version.version + " " + GetLangFile(92));
                        }
                    }
                }
                else if (email.Text == "Email" && (pass.Text == "" && pass.Enabled == true) && idofworld.Text == "ID do mundo")
                {
                    if (ptbr.Checked == true && enus.Checked == false)
                    {
                        MessageBox.Show("O Email, a password e o ID do mundo tenhem de ser preenchidos.", "R42Bot++ v" + Version.version + " System");
                    }
                }
                else
                {
                    this.Connect();
                    bool run = true;

                    try
                    {
                         con.Send("init");
                    }
                    catch (PlayerIOError error)
                    {
                        errorlog.Items.Add("The bot failed to connect: " + error.Message);
                        MessageBox.Show("The bot failed to connect: " + error.Message, "Error 000");
                        DisconnectBot();
                        run = false;
                        //MessageBox.Show(error.Message);
                    }
                    if (run)
                    {
                        connector.Text = "Disconnect";
                        autochangerface.Start();
                        autochangerface.Enabled = true;
                        button8.Enabled = true;
                        button9.Enabled = true;
                        grbutton.Enabled = true;
                        paintbrushauto.Enabled = true;
                        dncycle.Enabled = true;
                    }
                }
            }
            else if (connector.Text == "Disconnect")
            {
                DisconnectBot();
            }

        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (Admins.Items.Contains(removeText.Text.ToLower()))
            {
                Admins.Items.Remove(removeText.Text.ToLower());
                con.Send("say", "/pm " + addText.Text.ToLower() + " [R42Bot++] You have been removed from being an admin.");
                removeText.Clear();
            }
            else
            {
                MessageBox.Show(removeText.Text.ToLower() + " isn't an admin!");
                removeText.Clear();
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (!Admins.Items.Contains(addText.Text.ToLower()))
            {
                if (names.ContainsValue(addText.Text.ToLower()))
                {
                    Admins.Items.Add(addText.Text.ToLower());
                    con.Send("say", "/pm " + addText.Text.ToLower() + " [R42Bot++] You have been added as an admin.");
                    Thread.Sleep(250);
                    addText.Clear();
                }
                else
                {
                    MessageBox.Show(addText.Text.ToLower() + " is not in the connected world!");
                    addText.Clear();
                }
            }
            else
            {
                MessageBox.Show(addText.Text.ToLower() + " is already on the list...");
                addText.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            factory = new ChatterBotFactory();
            bot = factory.Create(ChatterBotType.JABBERWACKY);
            botSession = bot.CreateSession();

            face1.Maximum = SmileyFaceLimit;
            face2.Maximum = SmileyFaceLimit;
            if (File.Exists("R42Bot++SavedData.xml"))
            {
                var xs = new XmlSerializer(typeof(Information));
                var read = new FileStream("R42Bot++SavedData.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                var info = (Information)xs.Deserialize(read);
                alstalking.Checked = info.Stalk;
                foreach(string admin in info.Admins)
                {
                    Admins.Items.Add(admin);
                }
                foreach (List<string> strlist in info.Restrictions)
                {
                    if (strlist[0] == "autokick")
                    {
                        noRespawn.Checked = Convert.ToBoolean(strlist[1]);
                        warningGiver.Checked = Convert.ToBoolean(strlist[2]);
                        textBox1.Text = strlist[3];
                        bwl.Checked = Convert.ToBoolean(strlist[4]);
                    }
                    else if (strlist[0] == "survival")
                    {
                        noRespawn2.Checked = Convert.ToBoolean(strlist[1]);
                        warningGiver2.Checked = Convert.ToBoolean(strlist[2]);
                        limit2.Text = strlist[3];
                        bwl2.Checked = Convert.ToBoolean(strlist[4]);
                    }
                    else if (strlist[0] == "creative")
                    {
                        noRespawn3.Checked = Convert.ToBoolean(strlist[1]);
                        warningGiver3.Checked = Convert.ToBoolean(strlist[2]);
                        limit3.Text = strlist[3];
                        bwl3.Checked = Convert.ToBoolean(strlist[4]);
                    }

                    if (info.language == "enUS")
                    {
                        enus.Checked = true;
                    }
                    else if (info.language == "ptbr")
                    {
                        ptbr.Checked = true;
                    }
                    else if (info.language == "ltu")
                    {
                        ltu.Checked = true;
                    }
                    else if (info.language == "dutch")
                    {
                        dutchCBOX.Checked = true;
                    }

                    #region Color Changer
                    if (info.Color1 == Color.White)
                    {
                        currentColor.Text = "Default";
                        tabPage1.BackColor = Color.White;
                        tabPage2.BackColor = Color.White;
                        tabPage3.BackColor = Color.White;
                        Main.BackColor = Color.White;
                        pollTab.BackColor = Color.White;
                        autoPage.BackColor = Color.White;
                        NEWS.BackColor = Color.Beige;
                        LanguageOrSettings.BackColor = Color.White;
                        autobolder.BackColor = Color.White;
                        advancedEditor.BackColor = Color.White;
                        snakepage.BackColor = Color.White;
                        smileytabs.BackColor = Color.White;
                        tabPage6.BackColor = Color.White;
                    }
                    else
                    {
                        currentColor.Text = c.Color.ToString();

                        tabPage1.BackColor = info.Color1;
                        tabPage2.BackColor = info.Color1;
                        tabPage3.BackColor = info.Color1;
                        Main.BackColor = info.Color1;
                        pollTab.BackColor = info.Color1;
                        autoPage.BackColor = info.Color1;
                        NEWS.BackColor = info.Color1;
                        LanguageOrSettings.BackColor = info.Color1;
                        autobolder.BackColor = info.Color1;
                        advancedEditor.BackColor = info.Color1;
                        snakepage.BackColor = info.Color1;
                        smileytabs.BackColor = info.Color1;
                    }
                    #endregion
                }
            }
            Thread.Sleep(250);

            //BuildVersion = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Build"]);
            Version.upgradedBuild = new System.Net.WebClient().DownloadString(Version.buildlink);
        }

        private void enus_CheckedChanged(object sender, EventArgs e)
        {
            if (enus.Checked == true)
            {
                MessageBox.Show("Language is now EU/US!", "R42Bot++ v" + Version.version + " System");
                CurrentLang = "enUS";
                ltu.Checked = false;
                ptbr.Checked = false;
                dutchCBOX.Checked = false;
                //Translate();
            }
            else if (!ptbr.Checked && !ltu.Checked && !dutchCBOX.Checked)
            {
                enus.Checked = true;
                //Translate();
            }
            LangChangeInitialized = false;
        }

        private void ptbr_CheckedChanged(object sender, EventArgs e)
        {
            if (ptbr.Checked == true)
            {
                MessageBox.Show("A Linguagem é agora português.", "Sistema R42Bot++ v" + Version.version);
                CurrentLang = "ptbr";
                ltu.Checked = false;
                enus.Checked = false;
                dutchCBOX.Checked = false;
                //Translate();
            }
            else if (!enus.Checked && !ltu.Checked && !dutchCBOX.Checked)
            {
                enus.Checked = true;
                //Translate();
            }
            LangChangeInitialized = false;
        }

        private void winsystem1_CheckedChanged(object sender, EventArgs e)
        {
            CallsSettings.WinSystem = winsystem1.Checked;
        }

        private void clearstalkering_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Successfully removed " + stalkMover.Text + " from stalking user.");
            Thread.Sleep(250);
            stalkMover.Text = "";
        }

        private void welcomemsg_TextChanged(object sender, EventArgs e)
        {
            CallsSettings.Welcome_Text = welcomemsg.Text;
        }

        private void leftallmsg_TextChanged(object sender, EventArgs e)
        {
            CallsSettings.Goodbye_Text = leftallmsg.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangeFace(5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangeFace(0);
        }

        private void autochangerface_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked && !eSAfC.Checked)
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    ChangeFace(0);
                    Thread.Sleep(500);
                    ChangeFace(5);
                });
            }
            else if (checkBox1.Checked && eSAfC.Checked)
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    if (Face == Convert.ToInt32(face1.Value) + 5)
                    {
                        ChangeFace(Convert.ToInt32(face2.Value) + 5);
                    }
                    else if (Face == Convert.ToInt32(face2.Value) + 5)
                    {
                        ChangeFace(Convert.ToInt32(face1.Value) + 5);
                    }
                    else if (Face == Convert.ToInt32(face1.Value))
                    {
                        ChangeFace(Convert.ToInt32(face1.Value) + 5);
                    }
                    else if (Face > SmileyFaceLimit)
                    {
                        ChangeFace(Convert.ToInt32(face1.Value));
                    }
                    Thread.Sleep(500);
                });
            }
            else if (eSAfC.Checked)
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    if (Face == Convert.ToInt32(face1.Value))
                    {
                        ChangeFace(Convert.ToInt32(face2.Value));
                    }
                    else
                    {
                        ChangeFace(Convert.ToInt32(face1.Value));
                    }
                    Thread.Sleep(500);
                });
            }
        }

        private void autokick_Tick(object sender, EventArgs e)
        {
            if (autokickvalue.Checked)
            {
                if (player.Length > (Admins.Items.Count+Moderators.Items.Count))
                {
                    for (int i = 0; i < player.Length; i++)
                    {
                        if (player[i].username != null)
                        {
                            if (!IsAdmin(player[i].username) && !IsMod(player[i].username))
                            {
                                con.Send("say", "/kick " + player[i].username + " " + GetLangFile(99));
                            }
                            Thread.Sleep(575);
                        }
                    }
                }
            }
        }

        private void autoreset_Tick(object sender, EventArgs e)
        {
            if (autoresetcheckbox.Checked)
            {
                int msdelay = Convert.ToInt32(autoresetime.Value);
                Thread.Sleep(msdelay);
                con.Send("say", "/reset");
                if (autoresetmsg.Checked)
                {
                    con.Send("say", GetLangFile(98));
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button7.Text == "New choice...")
            {
                button7.Location = new Point(16, 131);
                label28.Visible = true;
                label29.Visible = true;
                choice3.Visible = true;
                vot3.Visible = true;

                //

                button7.Text = "Remove choice...";
            }
            else if (button7.Text == "Remove choice...")
            {
                button7.Location = new Point(16, 102);

                //

                label28.Visible = false;
                label29.Visible = false;
                choice3.Visible = false;
                vot3.Visible = false;

                //

                button7.Text = "New choice...";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Thread.Sleep(575);
            if (names.ContainsValue(userpm.Text))
            {
                con.Send("say", userpm.Text + " " + textpm.Text);
                chatbox.Items.Add("*TO " + userpm.Text + ": " + textpm.Text);
            }
            else
            {
                MessageBox.Show("'" + userpm.Text + "' isn't in the connected world." + "R42Bot++ v" + Version.version + " " + GetLangFile(92));
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Thread.Sleep(575);
            con.Send("say", "[R42Bot++] " + saytext.Text);

            chatbox.Items.Add(">" + saytext.Text);
        }

        private void srandomizer_Click(object sender, EventArgs e)
        {
            ChangeFace(new Random().Next(0, 15));
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch))
            {
                if (e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(@"R42Bot++SavedData.xml"))
                {
                    var info = new Information();
                    List<string> ls = new List<string> { };
                    ls.Add("autokick");
                    ls.Add(noRespawn.Checked.ToString());
                    ls.Add(warningGiver.Checked.ToString());
                    ls.Add(textBox1.Text);
                    ls.Add(bwl.Checked.ToString());
                    info.Restrictions.Add(ls);

                    List<string> ls2 = new List<string> { };
                    ls2.Add("survival");
                    ls2.Add(noRespawn2.Checked.ToString());
                    ls2.Add(warningGiver2.Checked.ToString());
                    ls2.Add(limit2.Text);
                    ls2.Add(bwl2.Checked.ToString());
                    info.Restrictions.Add(ls2);

                    List<string> ls3 = new List<string> { };
                    ls3.Add("creative");
                    ls3.Add(noRespawn3.Checked.ToString());
                    ls3.Add(warningGiver3.Checked.ToString());
                    ls3.Add(limit3.Text);
                    ls3.Add(bwl3.Checked.ToString());
                    info.Restrictions.Add(ls3);


                    Saver.SaveData(info, "R42Bot++SavedData.xml");
                }
                else
                {
                    var xs = new XmlSerializer(typeof(Information));
                    var read = new FileStream("R42Bot++SavedData.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    var info = (Information)xs.Deserialize(read);

                    foreach (List<string> ls in info.Restrictions)
                    {
                        if (ls[0] == "autokick")
                        {
                            foreach (string s in ls)
                            {
                                ls.Remove(s);
                            }

                            ls.Add("autokick");
                            ls.Add(noRespawn.Checked.ToString());
                            ls.Add(warningGiver.Checked.ToString());
                            ls.Add(textBox1.Text);
                            ls.Add(bwl.Checked.ToString());
                        }
                        else if (ls[0] == "survival")
                        {
                            foreach (string s in ls)
                            {
                                ls.Remove(s);
                            }

                            ls.Add("survival");
                            ls.Add(noRespawn2.Checked.ToString());
                            ls.Add(warningGiver2.Checked.ToString());
                            ls.Add(limit2.Text);
                            ls.Add(bwl2.Checked.ToString());
                        }
                        else if (ls[0] == "creative")
                        {
                            foreach (string s in ls)
                            {
                                ls.Remove(s);
                            }

                            ls.Add("creative");
                            ls.Add(noRespawn3.Checked.ToString());
                            ls.Add(warningGiver3.Checked.ToString());
                            ls.Add(limit3.Text);
                            ls.Add(bwl3.Checked.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(@"R42Bot++SavedData.xml"))
                {
                    var info = new Information();
                    if (currentColor.Text == "Default")
                    {
                        info.Color1 = Color.White;
                    }
                    else
                    {
                        info.Color1 = c.Color;
                    }
                    Saver.SaveData(info, "R42Bot++SavedData.xml");
                }
                else
                {
                    var xs = new XmlSerializer(typeof(Information));
                    var read = new FileStream("R42Bot++SavedData.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    var info = (Information)xs.Deserialize(read);

                    if (currentColor.Text == "Default")
                    {
                        info.Color1 = Color.White;
                    }
                    else
                    {
                        info.Color1 = c.Color;
                    }
                    Saver.SaveData(info, "R42Bot++SavedData.xml");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            currentColor.Text = "Default";
            tabPage1.BackColor = Color.White;
            tabPage2.BackColor = Color.White;
            tabPage3.BackColor = Color.White;
            tabPage4.BackColor = Color.White;
            tabPage6.BackColor = Color.White;
            tabPage7.BackColor = Color.White;
            Main.BackColor = Color.White;
            pollTab.BackColor = Color.White;
            autoPage.BackColor = Color.White;
            NEWS.BackColor = Color.Beige;
            LanguageOrSettings.BackColor = Color.White;
            autobolder.BackColor = Color.White;
            advancedEditor.BackColor = Color.White;
            snakepage.BackColor = Color.White;
            smileytabs.BackColor = Color.White;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            c.ShowDialog();
            currentColor.Text = c.Color.ToString();

            tabPage1.BackColor = c.Color;
            tabPage2.BackColor = c.Color;
            tabPage3.BackColor = c.Color;
            tabPage4.BackColor = c.Color;
            tabPage6.BackColor = c.Color;
            tabPage7.BackColor = c.Color;
            Main.BackColor = c.Color;
            pollTab.BackColor = c.Color;
            autoPage.BackColor = c.Color;
            NEWS.BackColor = c.Color;
            LanguageOrSettings.BackColor = c.Color;
            autobolder.BackColor = c.Color;
            advancedEditor.BackColor = c.Color;
            snakepage.BackColor = c.Color;
            smileytabs.BackColor = c.Color;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "3";
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(@"R42Bot++SavedData.xml"))
                {
                    var info = new Information();
                    info.Stalk = alstalking.Checked;
                    Saver.SaveData(info, "R42Bot++SavedData.xml");
                }
                else
                {
                    var xs = new XmlSerializer(typeof(Information));
                    var read = new FileStream("R42Bot++SavedData.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    var info = (Information)xs.Deserialize(read);

                    info.Stalk = alstalking.Checked;
                    Saver.SaveData(info, "R42Bot++SavedData.xml");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fbTokenGet_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Access Token is a key for entering PlayerIO games from facebook, to find out your's, go to https://developers.facebook.com/tools/explorer/ in your browser. Press get Token, Access Token button and check user_friends, then 'me' and then id, name.", "What's access token?", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox6.Text = "http://realmaster42-projects.weebly.com/r42bot1.html";
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            textBox7.Text = "https://trello.com/b/bs7b8Flx/r42bot";
        }

        private void waterchoice1_CheckedChanged(object sender, EventArgs e)
        {
            waterchoice2.Checked = (waterchoice1.Checked) ? false : true;
        }

        private void rbs_CheckedChanged(object sender, EventArgs e)
        {
            if (rbs.Checked)
            {
                frbs.Checked = false;
                nbs.Checked = false;
                fnbs.Checked = false;
            }
        }

        private void frbs_CheckedChanged(object sender, EventArgs e)
        {
            if (frbs.Checked)
            {
                rbs.Checked = false;
                nbs.Checked = false;
                fnbs.Checked = false;
            }
        }

        private void nbs_CheckedChanged(object sender, EventArgs e)
        {
            if (nbs.Checked)
            {
                frbs.Checked = false;
                rbs.Checked = false;
                fnbs.Checked = false;
            }
        }

        private void fnbs_CheckedChanged(object sender, EventArgs e)
        {
            if (fnbs.Checked)
            {
                frbs.Checked = false;
                nbs.Checked = false;
                rbs.Checked = false;
            }
        }

        private void mineralRAINBOW_CheckedChanged(object sender, EventArgs e)
        {
            if (mineralRAINBOW.Checked)
            {
                mineralRAINBOWFAST.Checked = false;
                fax.Checked = false;
                faxII.Checked = false;
                if (CheckSnakeUpdate)
                {
                    currentChecked = "mineralRAINBOW";
                }
            }
            else
            {
                if (CheckSnakeUpdate)
                {
                    currentChecked = "";
                }
            }
        }

        private void mineralRAINBOWFAST_CheckedChanged(object sender, EventArgs e)
        {
            if (mineralRAINBOWFAST.Checked)
            {
                mineralRAINBOW.Checked = false;
                fax.Checked = false;
                faxII.Checked = false;
                if (CheckSnakeUpdate)
                {
                    currentChecked = "mineralRAINBOWFAST";
                }
            }
            else
            {
                if (CheckSnakeUpdate)
                {
                    currentChecked = "";
                }
            }
        }

        private void fax_CheckedChanged(object sender, EventArgs e)
        {
            if (fax.Checked)
            {
                mineralRAINBOWFAST.Checked = false;
                faxII.Checked = false;
                mineralRAINBOW.Checked = false;
                if (CheckSnakeUpdate)
                {
                    currentChecked = "fax";
                }
            }
            else
            {
                if (CheckSnakeUpdate)
                {
                    currentChecked = "";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(@"R42Bot++SavedData.xml"))
                {
                    var info = new Information();
                    ListBox.ObjectCollection items = Admins.Items;
                    string[] Items1 = new string[] { };
                    for (int i = 1; i < items.Count; i++)
                    {
                        Items1[i] = items[i].ToString();
                    }
                    info.Admins = Items1;
                    Saver.SaveData(info, "R42Bot++SavedData.xml");
                }
                else
                {
                    var xs = new XmlSerializer(typeof(Information));
                    var read = new FileStream("R42Bot++SavedData.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    var info = (Information)xs.Deserialize(read);

                    ListBox.ObjectCollection items = Admins.Items;
                    string[] Items1 = new string[] { };
                    for (int i = 1; i < items.Count; i++)
                    {
                        Items1[i] = items[i].ToString();
                    }
                    info.Admins = Items1;
                    Saver.SaveData(info, "R42Bot++SavedData.xml");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists("R42Bot++SavedData.xml"))
            {
                var xs = new XmlSerializer(typeof(Information));
                var read = new FileStream("R42Bot++SavedData.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                var info = (Information)xs.Deserialize(read);

                if (info.Admins.Length > 0)
                {
                    for (int i = 0; i < info.Admins.Length; i++)
                    {
                        if (!Admins.Items.Contains(info.Admins[i]))
                        {
                            Admins.Items.Add(info.Admins[i]);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Save File not found. (R42Bot++SavedData.xml)", "R42Bot++ v" + Version.version + " " + GetLangFile(92), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TrollCatcherBlockDelete_Tick(object sender, EventArgs e)
        {
            if (botFullyConnected)
            {
                if (unfairBlox.Checked)
                {
                    //ThreadPool.QueueUserWorkItem(delegate
                    //{
                        for (int i = 0; i < names.Count; i++)
                        {
                            player[i].BlocksPlacedInaSecond = 0;
                            player[i].AlreadyReedit = false;
                            Thread.Sleep(8);
                        }
                    //});
                }
            }
        }

        private void pgeb100loldef_CheckedChanged(object sender, EventArgs e)
        {
            if (pgeb100loldef.Checked)
            {
                pgeb100loldo.Checked = false;
                if (CheckGlassExplodeUpdate)
                {
                    currentCheckedDorE = "EXPLODE";
                }
            }
        }

        private void pgeb100loldo_CheckedChanged(object sender, EventArgs e)
        {
            if (pgeb100loldo.Checked)
            {
                pgeb100loldef.Checked = false;
                if (CheckGlassExplodeUpdate)
                {
                    currentCheckedDorE = "DELETE";
                }
            }
        }
        private void ltu_CheckedChanged(object sender, EventArgs e)
        {
            if (ltu.Checked == true)
            {
                MessageBox.Show("Kalba dabar yra LTU/LT", "R42Bot++ v" + Version.version + " sistema");
                CurrentLang = "ltu";
                enus.Checked = false;
                ptbr.Checked = false;
                dutchCBOX.Checked = false;
                //Translate();
            }
            else if (!ptbr.Checked && !enus.Checked && !dutchCBOX.Checked)
            {
                enus.Checked = true;
                //Translate();
            }
            LangChangeInitialized = false;
        }

        private void faxII_CheckedChanged(object sender, EventArgs e)
        {
            if (faxII.Checked)
            {
                mineralRAINBOWFAST.Checked = false;
                fax.Checked = false;
                mineralRAINBOW.Checked = false;
                if (CheckSnakeUpdate)
                {
                    currentChecked = "faxII";
                }
            }
            else
            {
                if (CheckSnakeUpdate)
                {
                    currentChecked = "";
                }
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            int first = Convert.ToInt32(numericUpDown2.Value);
            int sc = Convert.ToInt32(numericUpDown3.Value);

            if(sc < first)
            {
                numericUpDown2.Value = Convert.ToDecimal(sc);
                numericUpDown3.Value = Convert.ToDecimal(first);
            }
            else if (first == sc)
            {
                numericUpDown3.Value = Convert.ToDecimal(sc + 1);
            }
        }

        private void chngbtn_Click(object sender, EventArgs e)
        {
            eRandomTxtB.Text = new Random().Next(Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown3.Value)).ToString();
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void paintbrushauto_Click(object sender, EventArgs e)
        {
            if (botFullyConnected)
            {
                int idof = Convert.ToInt32(idofit.Text);

                int diffX = FillX2Cor - FillXCor,
                    diffY = FillY2Cor - FillYCor;
                for (int a = 0; a <= Math.Abs(diffX); a++)//((worldWidth-FillYCor)+(FillXCor-FillYCor)); x++)
                {
                    if (!con.Connected)
                    {
                        FillGaveError = true;
                        break;
                    }
                    //if (x <= FillX2Cor)
                    //{
                    for (int b = 0; b <= Math.Abs(diffY); b++)
                    {
                        if (!con.Connected)
                        {
                            FillGaveError = true;
                            break;
                        }

                        int x = (diffX >= 0) ? FillXCor + a : FillXCor - a,
                            y = (diffY >= 0) ? FillYCor + b : FillYCor - b;

                        if (idof >= 500 && idof < 1000)
                            con.Send(worldKey, new object[] { 1, x, y, idof });
                        else
                            con.Send(worldKey, new object[] { 0, x, y, idof });
                        if (idof==0)
                        {
                            con.Send(worldKey, new object[] { 1, x, y, idof });
                        }
                        Thread.Sleep(Convert.ToInt32(fdelay.Value));
                    }
                    //}
                    //else
                    //{
                    //    break;
                    //}
                    Thread.Sleep(Convert.ToInt32(fdelay.Value));
                }
                FillXCor = 1;
                FillYCor = 1;
                FillX2Cor = worldWidth;
                FillY2Cor = worldHeight;
                /*int x1 = 1;
                int y1 = 1;
                int x2 = worldWidth - 2;
                int y2 = worldHeight - 2;
                if (idof > 500 && idof < 1000)
                    con.Send(worldKey + "fill", idof, 1, x1, y1, x2, y2);
                else
                    con.Send(worldKey + "fill", idof, 0, x1, y1, x2, y2);*/
            }
        }

        private void limit2_TextChanged(object sender, EventArgs e)
        {
            if (limit2.Text == "")
            {
                limit2.Text = "3";
            }
        }

        private void limit3_TextChanged(object sender, EventArgs e)
        {
            if (limit3.Text == "")
            {
                limit3.Text = "3";
            }
        }

        private void limit2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch))
            {
                if (e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
        }

        private void limit3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch))
            {
                if (e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
        }

        private void waterchoice2_CheckedChanged(object sender, EventArgs e)
        {
            waterchoice1.Checked = (waterchoice2.Checked) ? false : true;
        }

        private void idofit_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch))
            {
                if (e.KeyChar != 8)
                {
                    e.Handled = true;
                    MessageBox.Show("The ID should be a valid number.");
                }
            }
            else
            {
                if (Convert.ToInt32(idofit.Text) > 620)
                {
                    idofit.Clear();
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (isFacebook.Checked)
            {
                toolTip1.SetToolTip(email, "TokenId is the token access key to the game.\nWithout this the bot will not recognize the database object.\nMore informations in the '?' button.");
                if (email.Text == "Email" || email.Text == "email")
                {
                    email.Text = "Token ID";
                }
                pass.Enabled = false;
                fbTokenGet.Visible = true;
            }
            else
            {
                toolTip1.SetToolTip(email, "Email is your user's email address in the game.\nWithout this info the bot will not be able to recognize the database object.");
                if (email.Text == "Token ID" || email.Text == "token ID" || email.Text == "Token id")
                {
                    email.Text = "Email";
                }
                pass.Enabled = true;
                fbTokenGet.Visible = false;
            }
        }
        #endregion

        private void kJoiners_CheckedChanged(object sender, EventArgs e)
        {
            CallsSettings.AllowJoiners = (kJoiners.Checked) ? true : false;
            if (kJoiners.Checked)
            {
                CallsSettings.AllowJoiners = true;
            }
            else
            {
                CallsSettings.AllowJoiners = false;
            }
        }

        private void leftallupper_CheckedChanged(object sender, EventArgs e)
        {
            if (leftallupper.Checked)
            {
                leftallcase.Checked = false;
                CallsSettings.Goodbye_Upper = true;
            }
            else if (!leftallcase.Checked && !leftallupper.Checked)
            {
                leftallupper.Checked = true;
                CallsSettings.Goodbye_Upper = true;
            }
        }

        private void leftallcase_CheckedChanged(object sender, EventArgs e)
        {
            if (leftallcase.Checked)
            {
                leftallupper.Checked = false;
                CallsSettings.Goodbye_Upper = false;
            }
            else if (!leftallcase.Checked && !leftallupper.Checked)
            {
                leftallupper.Checked = true;
                CallsSettings.Goodbye_Upper = true;
            }
        }

        private void welcomeall_CheckedChanged(object sender, EventArgs e)
        {
            CallsSettings.Welcome = (welcomeall.Checked) ? true : false;
        }

        private void welcomealllower_CheckedChanged(object sender, EventArgs e)
        {
            if (welcomealllower.Checked)
            {
                welcomeallupper.Checked = false;
                CallsSettings.Welcome_Upper = false;
            }
            else if (!welcomealllower.Checked && !welcomeallupper.Checked)
            {
                welcomeallupper.Checked = true;
                CallsSettings.Welcome_Upper = true;
            }
        }

        private void welcomeallupper_CheckedChanged(object sender, EventArgs e)
        {
            if (welcomeallupper.Checked)
            {
                welcomealllower.Checked = false;
                CallsSettings.Welcome_Upper = true;
            }
            else if (!welcomealllower.Checked && !welcomeallupper.Checked)
            {
                welcomeallupper.Checked = true;
                CallsSettings.Welcome_Upper = true;
            }
        }

        private void kbots_CheckedChanged(object sender, EventArgs e)
        {
            CallsSettings.KickBots = (kbots.Checked) ? true : false;
        }

        private void welcomemsg2_TextChanged(object sender, EventArgs e)
        {
            CallsSettings.Welcome_Text_2 = welcomemsg2.Text;
        }

        private void leftall2_TextChanged(object sender, EventArgs e)
        {
            CallsSettings.Goodbye_Text_2 = leftall2.Text;
        }

        private void leftall_CheckedChanged(object sender, EventArgs e)
        {
            CallsSettings.Goodbye = (leftall.Checked) ? true : false;
        }

        private void autoresetcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            autoreset.Enabled = (autoresetcheckbox.Checked) ? true : false;
        }

        private void FreeEdit_CheckedChanged(object sender, EventArgs e)
        {
            CallsSettings.FreeEdit = (FreeEdit.Checked) ? true : false;
        }

        private void grbutton_Click(object sender, EventArgs e)
        {
            if (grbutton.Text == "Generate Random Bricks")
            {
                Gen_RB.Enabled = true;
                Gen_RB.Start();
                grbutton.Text = "Stop Generating";
            }
            else
            {
                Gen_RB.Stop();
                Gen_RB.Enabled = false;
                grbutton.Text = "Generate Random Bricks";
            }
        }

        private void Gen_RB_Tick(object sender, EventArgs e)
        {
            for (int x = 1; x < worldWidth; x++)
            {
                for (int y = 1; y < worldHeight; y++)
                {
                    int Bid = new Random().Next(0, 1026); //BlockID
                    int FG = (Bid < 500 || Bid > 1000) ? 0 : 1;
                    bool rotate = false; int rotateID = 0;
                    bool id = false; int targetID = 0; int portalID = 0;
                    bool switchid = false; int switchD = 0;
                    bool worldid = false; string worldID = "PW";
                    bool sign = false; string message = "abcdefgh";
                    bool value = false; int valueID = 0; // value < 100

                    #region Define...
                    if (Bid == 361 || Bid == 242 || Bid == 381 || (Bid > 1000 && Bid < 1005))
                    {
                        rotate = true; rotateID = new Random().Next(1, 4) - 1;
                    }
                    if (Bid == 242 || Bid == 381)
                    {
                        id = true;
                        targetID = new Random().Next(1, 999); portalID = new Random().Next(1, 999);
                    }
                    if (Bid == 374)
                    {
                        worldid = true; worldID = idofworld.Text;
                    }
                    if (Bid == 113 || Bid == 185 || Bid == 184)
                    {
                        switchid = true; switchD = new Random().Next(1, 999);
                    }
                    if (Bid == 165 || Bid == 214 || Bid == 43 || Bid == 213 || Bid == 1011 || Bid == 1012)
                    {
                        value = true; valueID = new Random().Next(1, 999);
                    }
                    if (Bid == 385)
                    {
                        sign = true; message = RGSignMsgs.Items[new Random().Next(1, RGSignMsgs.Items.Count)].ToString();
                    }
                    #endregion
                    //SPECIALS:
                    #region SPECIALS
                    /*
                            Deadly:
                            361 (Rotatable)

                            Portals:
                            242,381 (Rotatable, ID, Target)
                            374 (WorldID)

                            Switches:
                            113,185,184 (ID)

                            Coins:
                            165,214,43,213 (Coins)

                            Deaths:
                            1012,1011 (Deaths)

                            Sign:
                            385 (Message)

                            One-Way:
                            1001,1002,1003,1004 (Rotatable)
                        */
                    #endregion

                    #region Placing
                    if (rotate == false && id == false && switchid == false && worldid == false && sign == false && value == false)
                    {
                        con.Send(worldKey, new object[] { FG, x, y, Bid });
                    }
                    else
                    {
                        if (rotate == true && id == false)
                        {
                            con.Send(worldKey, new object[] { FG, x, y, Bid, rotateID });
                        }
                        else if (rotate == true && id == true)
                        {
                            con.Send(worldKey, new object[] { FG, x, y, Bid, rotateID, portalID, targetID });
                        }
                        if (worldid)
                        {
                            con.Send(worldKey, new object[] { FG, x, y, Bid, worldID });
                        }
                        if (switchid)
                        {
                            con.Send(worldKey, new object[] { FG, x, y, Bid, switchD });
                        }
                        if (value)
                        {
                            con.Send(worldKey, new object[] { FG, x, y, Bid, valueID });
                        }
                        if (sign)
                        {
                            con.Send(worldKey, new object[] { FG, x, y, Bid, message });
                        }
                    }
                    #endregion
                    Thread.Sleep(12);
                }
                Thread.Sleep(12);
            }
            grbutton.Text = "Generate Random Bricks";
            Gen_RB.Stop();
            Gen_RB.Enabled = false;
        }

        private void dutchCBOX_CheckedChanged(object sender, EventArgs e)
        {
            if (dutchCBOX.Checked == true)
            {
                MessageBox.Show("Je taal is nu Nederlands.", "R42Bot++ v" + Version.version + Voids.GetLangFile("dutch", 92));
                CurrentLang = "dutch";
                enus.Checked = false;
                ptbr.Checked = false;
                ltu.Checked = false;
                //Translate();
            }
            else if (!ptbr.Checked && !enus.Checked && !ltu.Checked)
            {
                enus.Checked = true;
                //Translate();
            }
            LangChangeInitialized = false;
        }

        private void AutoFixBot_Tick(object sender, EventArgs e)
        {
            if (botFullyConnected)
            {
                if (!con.Connected)
                {
                    if (FillGaveError)
                    {
                        Log("The bot has stopped working while filling! Try reconnecting.", "002");
                    }
                    else
                    {
                        DisconnectBot();
                        Log("A fatal error has occurred and the bot crashed!", "001");
                    }
                }
            }
            if (!this.Text.Contains(GetLangFile(97)))
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    try
                    {
                        if (!LangChangeInitialized)
                        {
                            welcomemsg.Text = GetLangFile(3);
                            welcomemsg2.Text = "!";
                            leftallmsg.Text = GetLangFile(5);
                            leftall2.Text = GetLangFile(6);

                            remove.Text = GetLangFile(9);
                            add.Text = GetLangFile(8);
                            add2.Text = GetLangFile(8);
                            remove2.Text = GetLangFile(9);

                            idofworld.Text = GetLangFile(7);

                            allowSnakeSpecial.Text = GetLangFile(49);

                            LangChangeInitialized = true;
                        }

                        if (!Version.versionLoaded)
                        {
                            Version.upgradedBuildVersion = new System.Net.WebClient().DownloadString(Version.buildversionlink);
                            Version.upgradedBuild = new System.Net.WebClient().DownloadString(Version.buildlink);
                        }

                        this.Text = "R42Bot++ v" + Version.version + " " + GetLangFile(97) + " " + System.Configuration.ConfigurationManager.AppSettings["Build"];
                        if (!Version.versionLoaded)
                        {
                            Version.UpToDate = GetLangFile(96).Replace("(V)", Version.version);
                            Version.OutOfDate = GetLangFile(94).Replace("(V)", Version.version).Replace("[V]", Version.upgradedBuildVersion);
                            Version.OutOfDateBuild = GetLangFile(95).Replace("(B)", nBuild).Replace("[B]", Version.upgradedBuild);
                            Version.TestBuild = "R42Bot++ v" + Version.upgradedBuildVersion + " " + GetLangFile(97) + " " + nBuild;

                            if (Version.upgradedBuildVersion == Version.version)
                            {
                                label48.ForeColor = Color.Goldenrod;
                                label48.Text = Version.TestBuild;
                            }
                            else if (Version.upgradedVersion != Version.version)
                            {
                                label48.Text = Version.OutOfDate;
                            }
                            else if (Convert.ToInt32(Version.upgradedBuild) < Convert.ToInt32(nBuild))
                            {
                                label48.Text = Version.TestBuild;
                            }
                            else if (Convert.ToInt32(Version.upgradedBuild) > Convert.ToInt32(nBuild))
                            {
                                label48.Text = Version.OutOfDateBuild;
                            }
                            else
                            {
                                label48.ForeColor = Color.DarkOliveGreen;
                                label48.Text = Version.UpToDate;
                            }
                            Version.versionLoaded = true;
                        }
                        Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        if (ex.Message.Contains("A first chance exception of type 'System.Net.WebException' occurred in System.dll") || ex.Message.Contains("The remote name could not be resolved: 'dl.dropbox.com'"))
                        {
                            if (!NetError)
                            {
                                NetError = true;
                                errorlog.Items.Add("R42Bot++ did not detect Internet.");
                                if (MessageBox.Show("R42Bot++ could not detect Internet. R42Bot++ may not work! " + '\n' + "Do you wish to close the application?", "Error 003", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Yes)
                                {
                                    Application.Exit();
                                }
                            }
                        }
                    }
                });
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            botFullyConnected = false;
            AutoFixBot.Stop();
            AutoFixBot.Enabled = false;
            Gen_RB.Stop();
            Gen_RB.Enabled = false;
            MessageBox.Show("Thanks for using R42Bot++!", "Thanks!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void saveLang_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(@"R42Bot++SavedData.xml"))
                {
                    var info = new Information();
                    info.language = CurrentLang;
                    Saver.SaveData(info, "R42Bot++SavedData.xml");
                }
                else
                {
                    var xs = new XmlSerializer(typeof(Information));
                    var read = new FileStream("R42Bot++SavedData.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    var info = (Information)xs.Deserialize(read);
                    
                    info.language = CurrentLang;
                    Saver.SaveData(info, "R42Bot++SavedData.xml");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UnBanButton_Click(object sender, EventArgs e)
        {
            if (bansList.Items.Contains(unbanTxtBox.Text))
            {
                int index = bansList.Items.IndexOf(unbanTxtBox.Text);
                bansList.Items.Remove(unbanTxtBox.Text);
                banreassons.Items.RemoveAt(index);
                //CallsSettings.Bans.Remove(unbanTxtBox.Text);
                unbanTxtBox.Clear();
            }
            else
            {
                unbanTxtBox.Clear();
                MessageBox.Show("User not banned.", "R42Bot++ v" + Version.version + " " + GetLangFile(92));
            }
        }

        private void BanButton_Click(object sender, EventArgs e)
        {
            if (!bansList.Items.Contains(banTxtBox.Text))
            {
                bansList.Items.Add(banTxtBox.Text);
                banreassons.Items.Add(banreassonbox.Text);
                //CallsSettings.Bans.Add(banTxtBox.Text);
                banTxtBox.Clear();
            }
            else
            {
                banTxtBox.Clear();
                MessageBox.Show("User already banned.", "R42Bot++ v" + Version.version + " " + GetLangFile(92));
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            textBox8.Text = "https://github.com/realmaster42/R42Bot";
        }

        private void kguests_CheckedChanged(object sender, EventArgs e)
        {
            CallsSettings.KickGuests = (kguests.Checked) ? true : false;
        }

        private void add2_Click(object sender, EventArgs e)
        {
            if (!Moderators.Items.Contains(add2Text.Text.ToLower()))
            {
                if (names.ContainsValue(add2Text.Text.ToLower()))
                {
                    Moderators.Items.Add(add2Text.Text.ToLower());
                    con.Send("say", "/pm " + add2Text.Text.ToLower() + " [R42Bot++] You have been added as a moderator.");
                    Thread.Sleep(250);
                    add2Text.Clear();
                }
                else
                {
                    MessageBox.Show(add2Text.Text.ToLower() + " is not in the connected world!");
                    add2Text.Clear();
                }
            }
            else
            {
                MessageBox.Show(add2Text.Text.ToLower() + " is already on the list...");
                add2Text.Clear();
            }
        }

        private void remove2_Click(object sender, EventArgs e)
        {
            if (Moderators.Items.Contains(remove2Text.Text.ToLower()))
            {
                Moderators.Items.Remove(remove2Text.Text.ToLower());
                con.Send("say", "/pm " + add2Text.Text.ToLower() + " [R42Bot++] You have been removed from being a moderator.");
                remove2Text.Clear();
            }
            else
            {
                MessageBox.Show(remove2Text.Text.ToLower() + " isn't a moderator!");
                remove2Text.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(@"R42Bot++SavedData.xml"))
                {
                    var info = new Information();
                    ListBox.ObjectCollection items = Moderators.Items;
                    string[] Items1 = new string[] { };
                    for (int i = 1; i < items.Count; i++)
                    {
                        Items1[i] = items[i].ToString();
                    }
                    info.Mods = Items1;
                    Saver.SaveData(info, "R42Bot++SavedData.xml");
                }
                else
                {
                    var xs = new XmlSerializer(typeof(Information));
                    var read = new FileStream("R42Bot++SavedData.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    var info = (Information)xs.Deserialize(read);

                    ListBox.ObjectCollection items = Moderators.Items;
                    string[] Items1 = new string[] { };
                    for (int i = 1; i < items.Count; i++)
                    {
                        Items1[i] = items[i].ToString();
                    }
                    info.Mods = Items1;
                    Saver.SaveData(info, "R42Bot++SavedData.xml");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (File.Exists("R42Bot++SavedData.xml"))
            {
                var xs = new XmlSerializer(typeof(Information));
                var read = new FileStream("R42Bot++SavedData.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                var info = (Information)xs.Deserialize(read);

                if (info.Mods.Length > 0)
                {
                    for (int i = 0; i < info.Mods.Length; i++)
                    {
                        if (!Moderators.Items.Contains(info.Mods[i]))
                        {
                            Moderators.Items.Add(info.Mods[i]);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Save File not found. (R42Bot++SavedData.xml)", "R42Bot++ v" + Version.version + " " + GetLangFile(92), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (!RGSignMsgs.Items.Contains(smtext.Text)){
                RGSignMsgs.Items.Add(smtext.Text);
                smtext.Clear();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (RGSignMsgs.Items.Contains(smtext2.Text))
            {
                RGSignMsgs.Items.Remove(smtext2.Text);
                smtext2.Clear();
            }
        }

        private void DayNightCycle_Tick(object sender, EventArgs e)
        {
            if (dncycle.Checked && botFullyConnected)
            {
                DayTime++;
                int minutes=DayTime*60;
                int hours=minutes*60;
                time3.Text = DayTime.ToString();
                time2.Text = minutes.ToString();
                time1.Text = hours.ToString();
                #region DayTime
                if (hours > 23) // Middle Night
                {
                    DayTime = 0; // New Day
                    if (!chosenDay)
                    {
                        con.Send("say", "/bgcolor #020427"); chosenDay=true;
                    }
                    else { chosenDay = false; }
                }
                else if (hours >= 21)
                {
                    if (hours > 22)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #0d1153"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #0c115d"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 21)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #0a116e"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #0a256e"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 20)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #122d78"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #0e2d86"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 19)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #203b88"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #2d4a9c"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 18)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #2145aa"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #1a42b0"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 17)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #1a54b0"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #295fb4"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 16)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #2361c1"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #2367c1"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 15)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #2380c1"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #248ad1"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 14)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #2491d1"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #2094d7"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 13)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #279ce0"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #27aae0"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 12)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #1eade8"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #1ebde8"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 11)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #29bde5"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #37c4ea"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 10)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #46c7ea"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #5fcdea"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 9)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #5fe3ea"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #48e5ee"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 8)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #46d1d9"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #3bc5cd"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 7)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #3bc0cd"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #3bb4cd"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 6)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #3ba8cd"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #349abc"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 5)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #298daf"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #1e7d9e"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 4)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #1e6f9e"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #166390"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 3)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #105882"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #104a82"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 2)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #0b4278"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #0d3e6e"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 1)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #093662"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #092f55"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours > 0)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #042240"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #072039"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    else if (hours == 0)
                    {
                        if (minutes > 29)
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #040633"); chosenDay=true;}else{chosenDay=false;} 
                        }
                        else
                        {
                            if (!chosenDay){ con.Send("say", "/bgcolor #040625"); chosenDay=true;}else{chosenDay=false;} 
                        }
                    }
                    #endregion
                }
            }
        }

        private void stalkMover_TextChanged(object sender, EventArgs e)
        {
            stalkMover.Text = stalkMover.Text.ToLower();
        }

        private void sbeta_CheckedChanged(object sender, EventArgs e)
        {
            if (sbeta.Checked)
            {
                fsbeta.Checked = false;
                rainbowsb.Checked = false;
                frainbowsb.Checked = false;
            }
        }

        private void fsbeta_CheckedChanged(object sender, EventArgs e)
        {
            if (fsbeta.Checked)
            {
                sbeta.Checked = false;
                rainbowsb.Checked = false;
                frainbowsb.Checked = false;
            }
        }

        private void rainbowsb_CheckedChanged(object sender, EventArgs e)
        {
            if (rainbowsb.Checked)
            {
                fsbeta.Checked = false;
                sbeta.Checked = false;
                frainbowsb.Checked = false;
            }
        }

        private void frainbowsb_CheckedChanged(object sender, EventArgs e)
        {
            if (frainbowsb.Checked)
            {
                fsbeta.Checked = false;
                rainbowsb.Checked = false;
                sbeta.Checked = false;
            }
        }

        private void errorlogbtn_Click(object sender, EventArgs e)
        {
            if (errorlogbtn.Text == "Error Log")
            {
                errorlog.Visible = true;
                errorlogbtn.Text = "Close";
                errorlogbtn.Location = new Point(451, 305);
                errorlogbtn.Size = new Size(351, 23);
            }
            else
            {
                errorlog.Visible = false;
                errorlogbtn.Text = "Error Log";
                errorlogbtn.Location = new Point(687, 305);
                errorlogbtn.Size = new Size(115, 23);
            }
        }

        private void dbotbtn_Clicked(object sender, EventArgs e)
        {

        }

        private void fillcsisadminonly_CheckedChanged(object sender, EventArgs e)
        {
            ForceCheck(fillcsisadminonly, fillcsismodalso);
        }

        private void fillcsismodalso_CheckedChanged(object sender, EventArgs e)
        {
            ForceCheck(fillcsismodalso, fillcsisadminonly);
        }

        private void portalCboxAdmin_CheckedChanged(object sender, EventArgs e)
        {
            ForceCheck(portalCboxAdmin, portalCboxMod);
        }

        private void portalCboxMod_CheckedChanged(object sender, EventArgs e)
        {
            ForceCheck(portalCboxMod, portalCboxAdmin);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (File.Exists("R42Bot++SavedData.xml"))
            {
                var xs = new XmlSerializer(typeof(Information));
                var read = new FileStream("R42Bot++SavedData.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                var info = (Information)xs.Deserialize(read);

                if (info.WinSystem.Count > 0)
                {
                    for (int i = 0; i < info.WinSystem.Count; i++)
                    {
                        for (int x = 0; x < player.Length; x++)
                        {
                            if (player[x].username==info.WinSystem[i][0])
                            {
                                player[x].wins=Convert.ToInt32(info.WinSystem[i][1]);
                            }
                        }
                        System.Threading.Thread.Sleep(18);
                    }
                }
            }
            else
            {
                MessageBox.Show("Save File not found. (R42Bot++SavedData.xml)", "R42Bot++ v" + Version.version + " " + GetLangFile(92), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(@"R42Bot++SavedData.xml"))
                {
                    var info = new Information();
                    List<List<string>> WSL = new List<List<string>>() { };

                    for (int i = 0; i < player.Length; i++)
                    {
                        if (player[i].username!=null)
                        {
                            List<string> toAdd = new List<string>(){player[i].username,player[i].wins.ToString()};
                            WSL.Add(toAdd);
                        }
                    }

                    info.WinSystem = WSL;

                    Saver.SaveData(info, "R42Bot++SavedData.xml");
                }
                else
                {
                    var xs = new XmlSerializer(typeof(Information));
                    var read = new FileStream("R42Bot++SavedData.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    var info = (Information)xs.Deserialize(read);
                    List<List<string>> WSL = new List<List<string>>() { };

                    for (int i = 0; i < player.Length; i++)
                    {
                        if (player[i].username != null)
                        {
                            List<string> toAdd = new List<string>() { player[i].username, player[i].wins.ToString() };
                            WSL.Add(toAdd);
                        }
                    }

                    info.WinSystem = WSL;

                    Saver.SaveData(info, "R42Bot++SavedData.xml");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

#endregion
#endregion