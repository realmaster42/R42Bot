//http://www.binpress.com/license/view/l/79c35f4cb0919616b8c86a8d466c0362
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using PlayerIOClient;
using System.IO;
using System.Xml.Serialization;

#region BOT

namespace R42Bot
{

    public partial class Form1 : Form
    {
        #region UPDATER
        string versionlink = "https://dl.dropbox.com/s/6x0q6qmhd4afu40/version.txt";
        string version = "1 - A new reborn!";
        string upgradedVersion = "1 - A new reborn!";
        #endregion

        #region VARIABLES

        Connection con;
        Client client;
        public List<string> banList = new List<string> { "realmaster42", "", "", "", "", "" };
        public Dictionary<int, string> names = new Dictionary<int, string>();
        public Player[] player = new Player[9999];
        public int players;
        public string worldKey;
        string worldowner, worldtitle, str;
        public string botName = null;
        public int ax, ay, plays, woots, totalwoots, botid, worldWidth, worldHeight;
        public GetBlock[,] block;
        public int[] blockMoverArray = new int[] { 12 };
        public ColorDialog c = new ColorDialog();
        public int blockID1 = 0;
        public bool isFG = false,
            botIsPlacing = false,
            botFullyConnected = false,
            CheckSnakeUpdate = true;
        public int old_x = 0;
        public string currentOwner = " ";
        public string currentTitle = " ";
        public string currentChecked = "";
        public int currentPlays = 0;
        public int currentWoots = 0;
        public int BlockPlacingTilVal1 = 1,
            BlockPlacingTilVal2 = 2,
            BlockPlacingTilX = 1,
            BlockPlacingTilY = 1;

        #region [Face Randomizer]
        public int[] faceslist = new int[] { 0, 1, 2, 3, 4, 5, 12, 13, 14, 15 };
        #endregion

        #region HELP SECTION
        string[] possible_causes_botconnect_pt = new string[] { "Uma coisa que pode ter acontecido é que pôs um ID do mundo inválido, verifique.", "Pode ter escrito o email mal, a password mal ou os dois mal (conta que não existente)", "Se não tem o PlayerIO confirmado no seu firewall, confirma-o." };
        string[] possible_causes_botconnect_eu = new string[] { "One thing that could have happend is an wrong WorldID, please check it again.", "You may writed the email wrong, writed the password wrong or both wrong (account doesnt exist)", "Please, in your firewall, enable PlayerIO if it isn't enabled." };
        #endregion

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

        #endregion

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

        public void Translate()
        {
            #region language-changer
            if (enus.Checked == true)
            {
                welcomeall.Text = "Welcome everyone who comes to the world.";
                welcomemsg.Text = "Welcome to this world, ";
                leftall.Text = "Tell something when anybody leaves the world.";
                leftallmsg.Text = "Ow... ";
                leftall2.Text = " left US behind...";
                idofworld.Text = "world ID";
                add.Text = "Add";
                remove.Text = "Remove";
                leftallcase.Text = "non-caps";
                leftallupper.Text = "caps";
                welcomealllower.Text = "non-caps";
                welcomeallupper.Text = "caps";
                codebox.Text = "Code";
                //texts
                label1.Text = "hmm... the bot needs admin to work the best.";
                label4.Text = "Welcome Message";
                label6.Text = "text after username.";
                label7.Text = "Message";
                label9.Text = "text after username.";
                label13.Text = "Language:";
                label11.Text = "leaving username:";
                label12.Text = "welcoming username:";
                label10.Text = "Only choose one option for leaving and welcoming msg. (of username)";
                winsystem1.Text = "WINS SYSTEM";
                Main.Text = "Index";
                LanguageOrSettings.Text = "Options";
                NEWS.Text = "News";
                advancedEditor.Text = "Advanced Options";
                autobolder.Text = "AutoBuilder";
                autobuild1.Text = "Smiley Border";
            }
            else if (ptbr.Checked == true)
            {
                welcomeall.Text = "Dizer bem vindo a quem entrar no mundo.";
                welcomemsg.Text = "Bem vindo, ";
                leftall.Text = "Dizer algo quando alguém sai do mundo.";
                leftallmsg.Text = "Ó, que pena... ";
                leftall2.Text = " deixou-nos para trás...";
                idofworld.Text = "ID do mundo";
                add.Text = "Adicionar";
                remove.Text = "Remover";
                leftallcase.Text = "letra pequena";
                leftallupper.Text = "letra grande";
                welcomealllower.Text = "letra pequena";
                welcomeallupper.Text = "letra grande";
                codebox.Text = "Código";
                winsystem1.Text = "Sistema De Ganhos";
                //texts
                label1.Text = "hmm... o bot precisa de admin para funcionar melhor.";
                label4.Text = "Mensagem 'Bem Vindo'";
                label6.Text = "texto seguindo do nome.";
                label7.Text = "Mensagem 'Adeus'";
                label9.Text = "texto seguindo do nome.";
                label13.Text = "Linguagem:";
                label11.Text = "nome ao sair:";
                label12.Text = "nome ao entrar:";
                label10.Text = "Apenas escolha uma opção (letra pequena ou grande).";
                Main.Text = "Casa";
                LanguageOrSettings.Text = "Opções";
                NEWS.Text = "Novo";
                advancedEditor.Text = "Opções Avançadas";
                autobolder.Text = "Auto-Construtor";
                autobuild1.Text = "Smiley 10%";
            }
            else if (ltu.Checked)
            {
                welcomeall.Text = "visiems labas kurie atejo i si pasauli";
                welcomemsg.Text = "Sveikas atvikes i si pasauli, ";
                leftall.Text = "Pasakik kazka kai kazas iseina is pasaulio";
                leftallmsg.Text = "Au... ";
                leftall2.Text = " Paliko mus...";
                idofworld.Text = "pasaulio ID";
                add.Text = "prideti";
                remove.Text = "atimti";
                leftallcase.Text = "ne didziosios";
                leftallupper.Text = "didziosios";
                welcomealllower.Text = "ne didziosios";
                welcomeallupper.Text = "didziosios";
                codebox.Text = "Kodas";

                label1.Text = "hmm... botui reikia admino kad veiktu geriausiai";
                label4.Text = "Sveikinimo zinute";
                label6.Text = "tekstas to vardo";
                label7.Text = "zinute";
                label9.Text = "tekstas po vardo.";
                label13.Text = "kalba:";
                label11.Text = "iseinantis vardas:";
                label12.Text = "sveikinantis vardas:";
                label10.Text = "Tiktai pasiring viena pasirinkima isejimo ir atejimo zinutei (naudotojui)";
                label5.Text = "->";

                winsystem1.Text = "laimejimo sistema";
                kJoiners.Text = "ispirti atvikelius";
                kbots.Text = "ispirti botus";
                freeadmin.Text = "nemokamas adminas";
                FreeEdit.Text = "nemokamas redaugavimas";
                autobuild1.Text = "veido krastines";
                lavadrawer.Text = "vandens padejejas";
                paintbrushauto.Text = "pripilditi";
                wetsandCbox.Text = "slapias smelis";
                BGdelbox.Text = "panaikinti tapetus";
                pmresult.Text = "PM rezultatas";
            }
            #endregion
        }

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        #region Position Database Information <click to see>
        public string derot(string arg1)
        {
            int num = 0;
            string str = "";
            for (int i = 0; i < arg1.Length; i++)
            {
                num = arg1[i];
                if ((num >= 0x61) && (num <= 0x7a))
                {
                    if (num > 0x6d)
                    {
                        num -= 13;
                    }
                    else
                    {
                        num += 13;
                    }
                }
                else if ((num >= 0x41) && (num <= 90))
                {
                    if (num > 0x4d)
                    {
                        num -= 13;
                    }
                    else
                    {
                        num += 13;
                    }
                }
                str = str + ((char)num);
            }
            return str;
        }
        #endregion

        public struct GetBlock
        {
            public int BlockID { get; set; }
            public string placer { get; set; }
        }

        private void Connect(string email, string pass, string idofworld, bool isFB)
        {
            if (isFB == false)
            {
                try
                {
                    this.client = PlayerIO.QuickConnect.SimpleConnect("everybody-edits-su9rn58o40itdbnw69plyw", email, pass);
                    this.con = this.client.Multiplayer.JoinRoom(idofworld, null);
                    this.con.OnMessage += new MessageReceivedEventHandler(onMessage);
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
                    this.client = PlayerIO.QuickConnect.FacebookOAuthConnect("everybody-edits-su9rn58o40itdbnw69plyw", email, "");
                    this.con = this.client.Multiplayer.JoinRoom(idofworld, null);
                    this.con.OnMessage += new MessageReceivedEventHandler(onMessage);
                }
                catch (PlayerIOError error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        public string Shortest(string name)
        {
            if (name.Length >= 5)
            {
                return name.Substring(0, 5);
            }
            else if (name.Length >= 3)
            {
                return name.Substring(0, 3);
            }
            return name;
        }

        public void Read(PlayerIOClient.Message m, uint c)
        {
            DeserializeBar.Visible = true;
            con.Send("say", "[R42Bot++] Loading final assets. . .");
            
            int total = 0;
            //for (int y = 1; y<worldHeight;y++)
            //{
            //    for (int x = 1; x<worldWidth;x++)
            //    {
            //        block[x, y].BlockID = 0;
            //        total = ((worldHeight - y + worldWidth - x) / 100) * 10;
            //        DeserializeBar.Value = total;
            //    }
            //}
            Thread.Sleep(575);
            while (c < m.Count && !(m[c].ToString().StartsWith("PW") || m[c].ToString().StartsWith("BW")))
            {
                if (m[c].ToString() != "we")
                {
                    try
                    {
                        //MessageBox.Show(m.Count.ToString());
                        int bid = m.GetInt(c);
                        for (int n = 0; n < m.GetByteArray(c + 2).Length; n += 2)
                        {
                            int x = m.GetByteArray(c + 1)[n] << 8 | m.GetByteArray(c + 1)[n + 1];
                            int y = m.GetByteArray(c + 2)[n] << 8 | m.GetByteArray(c + 2)[n + 1];
                            total = 100-((worldHeight - y + worldWidth - x) / 100) * 10;
                            DeserializeBar.Value = total;
                            Console.WriteLine("aaa");

                            if (m.GetInt(c + 3) == 0) // FG
                            {
                                block[x, y].BlockID = bid;
                            }
                            else { block[x, y].BlockID = bid; } // BG
                            Thread.Sleep(10);
                        }
                        Console.WriteLine("lloo");
                        if (bid == 43 || bid == 77 || bid == 83 || bid == 1000 || bid == 165 || bid == 361 || bid >= 375 && bid <= 380 || bid == 385)
                        { c += 4; }
                        else if (bid == 242 || bid == 381)
                        { c += 6; }
                        else
                        { c += 3; }
                        Console.WriteLine("yay");
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message.ToString());
                    }
                }
                else
                {
                    break;
                }
            }
            DeserializeBar.Value = 0;
            DeserializeBar.Visible = false;
            //uint loc4 = 21;
            //var loc5 = m.Count;
            //byte[] loc6 = m.GetByteArray(loc4 + 2);
            //int loc7 = loc6.Length;
            //int bid = m.GetInt(loc4);
            //while (loc4 < loc5)
            //{
            //    if (m[loc4].ToString() == "we")
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        bid = m.GetInt(loc4);
            //        for (int n = 0; n < loc7; n += 2)
            //        {
            //            try
            //            {

            //                int x = m.GetByteArray(loc4 + 2)[n] << 8 | m.GetByteArray(loc4 + 2)[n + 1];
            //                int y = m.GetByteArray(loc4 + 2)[n] << 8 | m.GetByteArray(loc4 + 2)[n + 1];

            //                if (bid == 242 || bid == 381) // Portal (0 down, 1 left, 2 up, 3 right)
            //                {
            //                    var rotation = m.GetInt(loc4 + 4);
            //                    var sid = m.GetInt(loc4 + 5);
            //                    var eid = m.GetInt(loc4 + 6);
            //                }

            //                if (bid == 43 || bid == 165 || bid == 214 || bid == 213) //Coin door and gate
            //                {
            //                    var coins = m.GetInt(loc4 + 4);
            //                }

            //                if (bid == 361) //Spikes
            //                {
            //                    var rotation = m.GetInt(loc4 + 4); // (0 left, 1 up, 2 right, 3 down)
            //                }

            //                if (bid == 77 || bid == 83) //Piano or Drums
            //                {
            //                    var note = m.GetInt(loc4 + 4);
            //                }

            //                if (bid == 1000 || bid == 385 || bid == 374) // Text, Sign, World Portal
            //                {
            //                    var text = m.GetString(loc4 + 4);
            //                }

            //            }
            //            catch (Exception ee)
            //            {
            //                Console.WriteLine(bid + " " + ee.Message + "\n\n");
            //            }
            //        }


            //        if (bid == 43 || bid == 77 || bid == 83 || bid == 214 || bid == 213 || bid == 1000 || bid == 165 || bid == 361 || bid >= 374 && bid <= 380 || bid == 385) { loc4 += 5; }
            //        else if (bid == 242 || bid == 381) { loc4 += 7; }
            //        else { loc4 += 4; }


            //    }
            //}
            con.Send("say", "[R42Bot++] R42Bot++ Version " + version + " has been connected successfully! :)");
            con.Send("access", codebox.Text);
        }

        public void onMessage(object sender, PlayerIOClient.Message m)
        {
            switch (m.Type)
            {

                case "k":
                    if (botFullyConnected)
                    {
                        if (winsystem1.Checked == true)
                        {
                            player[m.GetInt(0)].wins = player[m.GetInt(0)].wins + 1;
                            Thread.Sleep(250);
                            if (names.ContainsKey(m.GetInt(0)))
                            {
                                if (enus.Checked == true && ptbr.Checked == false)
                                {
                                    con.Send("say", string.Concat(names[m.GetInt(0)].ToString() + " won! Now he/she has " + player[m.GetInt(0)].wins + " wins!"));
                                }
                                else if (ptbr.Checked == true && enus.Checked == false)
                                {
                                    con.Send("say", string.Concat(names[m.GetInt(0)].ToString() + " ganhou! Agora ele/ela ganhou " + player[m.GetInt(0)].wins + " vezes!"));
                                }
                            }
                        }
                    }

                    return;
                case "write":
                    return;
                case "updatemeta":
                    worldowner = m.GetString(0);
                    worldtitle = m.GetString(1);
                    plays = m.GetInt(2);
                    woots = m.GetInt(3);

                    currentOwner = worldowner;
                    currentTitle = worldtitle;
                    currentPlays = plays;
                    currentWoots = woots;
                    return;
                case "init":
                    try
                    {
                        con.Send("init2");
                        Thread.Sleep(575);
                        worldowner = m.GetString(0);
                        worldtitle = m.GetString(1);
                        currentOwner = worldowner;
                        currentTitle = worldtitle;
                        worldKey = derot(m.GetString(5));
                        plays = m.GetInt(2);
                        currentPlays = plays;
                        woots = m.GetInt(3);
                        currentWoots = woots;
                        totalwoots = m.GetInt(4);
                        botid = m.GetInt(6);
                        botName = m.GetString(9);
                        worldWidth = m.GetInt(12);
                        worldHeight = m.GetInt(13);
                        lavaP.Maximum = worldWidth;
                        lavaP.Value = 1;
                        lavaP.Enabled = true;
                        if (!names.ContainsValue(botName))
                        {
                            names.Add(m.GetInt(6), botName);
                        }
                        block = new GetBlock[worldWidth, worldHeight];
                        if (banList.Contains(botName))
                        {
                            MessageBox.Show("You have been banned from this bot!!!", "R42Bot++ v" + version + " System");
                            Thread.Sleep(250);
                            MessageBox.Show("YES = WAH, NO = NUUUUU!", "R42Bot++ v" + version + " System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            con.Send("say", "[R42Bot++] Goodbye, the user using me is banned! :D");
                            MessageBox.Show("Suprise, banned!");
                            con.Disconnect();
                            Application.Exit();
                        }
                        Read(m, 21);//18);
                    }
                    catch (PlayerIOError Error)
                    {
                        MessageBox.Show(Error.Message);
                    }
                    return;
                case "add":
                    if (!kJoiners.Checked)
                    {
                        if (!names.ContainsKey(m.GetInt(0)))
                            names.Add(m.GetInt(0), m.GetString(1));
                        else
                            if (kbots.Checked) { Thread.Sleep(200); con.Send("say", "/kick " + m.GetString(1) + " Bots dissallowed!"); }
                        if (m.GetString(1).ToString().StartsWith("guest-"))
                        {
                            player[m.GetInt(0)].isGuest = true;
                        }
                        else
                        {
                            player[m.GetInt(0)].isGuest = false;
                        }
                        Thread.Sleep(575);
                        if (freeadmin.Checked)
                        {
                            if (!Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                            {
                                Admins.Items.Add(names[m.GetInt(0)].ToString());
                                add.Enabled = false;
                            }
                        }
                        else
                        {
                            add.Enabled = true;
                        }
                        if (FreeEdit.Checked)
                        {
                            Thread.Sleep(200);
                            con.Send("say", "/giveedit " + names[m.GetInt(0)].ToString());
                            Thread.Sleep(200);
                        }
                        player[m.GetInt(0)].username = names[m.GetInt(0)].ToString();

                        if (welcomeall.Checked)
                        {
                            if (welcomealllower.Checked && !welcomeallupper.Checked)
                            {
                                Thread.Sleep(200);
                                con.Send("say", "[R42Bot++] " + welcomemsg.Text + " " + names[m.GetInt(0)].ToString().ToLower() + welcomemsg2.Text);
                                Thread.Sleep(200);
                            }
                            if (!welcomealllower.Checked && welcomeallupper.Checked)
                            {
                                Thread.Sleep(200);
                                con.Send("say", "[R42Bot++] " + welcomemsg.Text + " " + names[m.GetInt(0)].ToString().ToUpper() + welcomemsg2.Text);
                                Thread.Sleep(200);
                            }
                        }
                        botFullyConnected = true;

                        players++;
                    }
                    else
                    {
                        con.Send("say", "/kick " + m.GetString(1) + " [R42Bot++] Joining disabled.");
                    }
                    return;
                case "access":
                    con.Send("say", "[R42Bot++] Got Edit.");
                    return;
                case "lostaccess":
                    con.Send("say", "[R42Bot++] Lost Edit.");
                    return;
                case "left":
                    if (!kJoiners.Checked)
                    {
                        if (botFullyConnected)
                        {
                            #region case "left" code
                            if (leftall.Checked)
                            {
                                if (leftallcase.Checked && !leftallupper.Checked)
                                {
                                    Thread.Sleep(200);
                                    con.Send("say", "[R42Bot++] " + leftallmsg.Text + " " + names[m.GetInt(0)].ToString().ToLower() + " " + leftall2.Text);
                                    Thread.Sleep(200);
                                }
                                else if (!leftallcase.Checked && leftallupper.Checked)
                                {
                                    Thread.Sleep(200);
                                    con.Send("say", "[R42Bot++] " + leftallmsg.Text + " " + names[m.GetInt(0)].ToString().ToUpper() + " " + leftall2.Text);
                                    Thread.Sleep(200);
                                }
                            }
                            #endregion


                            players = players - 1;

                            if (freeadmin.Checked)
                            {
                                Admins.Items.Remove(names[m.GetInt(0)].ToString());
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
                    return;
                case "b":
                    if (botFullyConnected)
                    {

                        block[m.GetInt(1), m.GetInt(2)].BlockID = m.GetInt(3);
                        int layer = m.GetInt(0);
                        int flayer = 0;
                        ax = m.GetInt(1); // left and right
                        ay = m.GetInt(2); //up and down
                        if (names.ContainsKey(m.GetInt(4)))
                        {
                            block[ax, ay].placer = names[m.GetInt(4)];
                        }
                        int thedelay = Convert.ToInt32(numericUpDown1.Value);
                        int blockID = m.GetInt(3);

                        if (Convert.ToInt32(idofit.Text) >= 500)
                        {
                            layer = 1;
                        }
                        else
                        {
                            layer = 0;
                        }

                        if (BGdelbox.Checked)
                        {
                            if (blockID == 0)
                            {
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                Thread.Sleep(250);
                                con.Send(worldKey, new object[] { 1, ax, ay, 0 });
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
                        if (pgeb100lol.Checked)
                        {
                            if (blockID == 52)
                            {
                                if (pgeb100loldo.Checked)
                                {
                                    Thread.Sleep(thedelay);
                                    con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                }
                                else if (pgeb100loldef.Checked)
                                {
                                    bool RainbowMineral = mineralRAINBOWFAST.Checked;
                                    con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                    Thread.Sleep(12);
                                    CheckSnakeUpdate = false;
                                    mineralRAINBOWFAST.Checked = true;
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
                                    Thread.Sleep(12);
                                    CheckSnakeUpdate = true;
                                    CheckSnakes(currentChecked);
                                }
                            }
                        }
                        #endregion

                        #region SPECIAL SNAKE
                        if (allowSnakeSpecial.Checked)
                        {
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

                            BlockPlacingTilVal1 = one;
                            BlockPlacingTilVal2 = two;
                            BlockPlacingTilX = ax;
                            BlockPlacingTilY = ay;
                        }
                        #endregion

                        #region WET SAND
                        if (wetsandCbox.Checked)
                        {
                            if (blockID == 119)
                            {
                                if (block[ax, ay + 1].BlockID == 138)
                                {
                                    con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                    con.Send(worldKey, new object[] { 1, ax, ay, 0 });
                                    con.Send(worldKey, new object[] { 0, ax, ay + 1, 137 });
                                    if (block[ax, ay - 1].BlockID == 119)
                                    {
                                        con.Send(worldKey, new object[] { 0, ax, ay - 1, 0 });
                                        con.Send(worldKey, new object[] { 1, ax, ay, 119 });
                                    }
                                }
                                else if (block[ax, ay + 1].BlockID == 137)
                                {
                                    con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                    con.Send(worldKey, new object[] { 1, ax, ay, 0 });
                                    con.Send(worldKey, new object[] { 0, ax, ay + 1, 139 });
                                    if (block[ax, ay - 1].BlockID == 119)
                                    {
                                        con.Send(worldKey, new object[] { 0, ax, ay - 1, 0 });
                                        con.Send(worldKey, new object[] { 1, ax, ay, 119 });
                                    }
                                }
                                else if (block[ax, ay + 1].BlockID == 139)
                                {
                                    con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                    con.Send(worldKey, new object[] { 1, ax, ay, 0 });
                                    con.Send(worldKey, new object[] { 0, ax, ay + 1, 140 });
                                    if (block[ax, ay - 1].BlockID == 119)
                                    {
                                        con.Send(worldKey, new object[] { 0, ax, ay - 1, 0 });
                                        con.Send(worldKey, new object[] { 1, ax, ay, 119 });
                                    }
                                }
                                else if (block[ax, ay + 1].BlockID == 140)
                                {
                                    con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                    con.Send(worldKey, new object[] { 1, ax, ay, 0 });
                                    con.Send(worldKey, new object[] { 0, ax, ay + 1, 141 });
                                    if (block[ax, ay - 1].BlockID == 119)
                                    {
                                        con.Send(worldKey, new object[] { 0, ax, ay - 1, 0 });
                                        con.Send(worldKey, new object[] { 1, ax, ay, 119 });
                                    }
                                }
                                else if (block[ax, ay + 1].BlockID == 141)
                                {
                                    con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                    con.Send(worldKey, new object[] { 1, ax, ay, 0 });
                                    con.Send(worldKey, new object[] { 0, ax, ay + 1, 142 });
                                }
                                else if (block[ax, ay + 1].BlockID == 142)
                                {
                                    con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                    con.Send(worldKey, new object[] { 1, ax, ay, 0 });
                                    if (block[ax, ay - 1].BlockID == 119)
                                    {
                                        con.Send(worldKey, new object[] { 0, ax, ay - 1, 0 });
                                        con.Send(worldKey, new object[] { 1, ax, ay, 119 });
                                    }
                                }
                            }
                        }
                        #endregion
                        #region TNT  
                        if (tntallowd.Checked)
                        {
                            if (blockID == 12)
                            {
                                if (block[ax, ay + 1].BlockID == 0)
                                {
                                    Thread.Sleep(400);
                                    con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                                    Thread.Sleep(250);
                                    con.Send(worldKey, new object[] { 0, ax, ay + 1, 12 });
                                }
                                else
                                {
                                    //blow up
                                    #region Red
                                    con.Send(worldKey, new object[] { 1, ax + 1, ay, 613 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax - 1, ay, 613 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax, ay + 1, 613 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax, ay - 1, 613 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax - 1, ay - 1, 613 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax + 1, ay - 1, 613 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax - 1, ay + 1, 613 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax + 1, ay + 1, 613 }); Thread.Sleep(175);
                                    #endregion
                                    #region Yellow
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay - 1, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay + 1, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay + 1, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay - 1, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax, ay + 2, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax, ay - 2, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay - 2, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay - 2, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay + 2, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay + 2, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax + 1, ay + 2, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax - 1, ay + 2, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax - 1, ay - 2, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax + 1, ay - 2, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax - 2, ay - 2, 614 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 1, ax + 2, ay - 2, 614 }); Thread.Sleep(175);
                                    #endregion
                                    Thread.Sleep(3000); Thread.Sleep(175); // wait 3s
                                    #region Clear Explosion
                                    con.Send(worldKey, new object[] { 0, ax, ay, 0 }); Thread.Sleep(175);
                                    #region Clear Red
                                    con.Send(worldKey, new object[] { 0, ax + 1, ay, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax - 1, ay, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax, ay + 1, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax, ay - 1, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax - 1, ay - 1, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax + 1, ay - 1, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax - 1, ay + 1, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax + 1, ay + 1, 0 }); Thread.Sleep(175);
                                    #endregion
                                    #region Clear Yellow
                                    con.Send(worldKey, new object[] { 0, ax + 2, ay, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax - 2, ay, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax + 2, ay - 1, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax - 2, ay + 1, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax + 2, ay + 1, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax - 2, ay - 1, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax, ay + 2, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax, ay - 2, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax - 2, ay - 2, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax + 2, ay - 2, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax - 2, ay + 2, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax + 2, ay + 2, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax + 1, ay + 2, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax - 1, ay + 2, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax - 1, ay - 2, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax + 1, ay - 2, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax - 2, ay - 2, 0 }); Thread.Sleep(175);
                                    con.Send(worldKey, new object[] { 0, ax + 2, ay - 2, 0 }); Thread.Sleep(175);
                                    #endregion
                                    #endregion
                                }
                            }
                        }
                        #endregion

                        if (paintbrushauto.Checked)
                        {
                            int idof = Convert.ToInt32(idofit.Text);
                            if (blockID == idof)
                            {
                                for (int x = 0; x < worldWidth; x++)
                                {
                                    for (int y = 0; y < worldHeight; y++)
                                    {
                                        con.Send(worldKey, new object[] { flayer, x, y, idof });
                                        System.Threading.Thread.Sleep(Convert.ToInt32(fdelay.Value)); //Changeable considering your internet speed
                                    }
                                }
                            }
                        }

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
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 74 });
                            }
                            if (blockID == 74)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 75 });
                            }
                            if (blockID == 75)
                            {
                                Thread.Sleep(7);
                                con.Send(worldKey, new object[] { 0, ax, ay, 76 });
                            }
                            if (blockID == 76)
                            {
                                Thread.Sleep(7);
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
                                Thread.Sleep(250);
                                con.Send(worldKey, new object[] { 0, ax, ay, 12 });
                            }
                            if (blockID == 12)
                            {
                                Thread.Sleep(250);
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
                                Thread.Sleep(250);
                                con.Send(worldKey, new object[] { 0, ax, ay, 70 });
                            }
                            if (blockID == 70)
                            {
                                Thread.Sleep(250);
                                con.Send(worldKey, new object[] { 0, ax, ay, 0 });
                            }
                        }

                        if (lavadrawer.Checked)
                        {
                            if (blockID == 119 && !botIsPlacing)
                            {
                                int BGcolor = 574;
                                if (waterchoice2.Checked)
                                {
                                    BGcolor = 530;
                                }
                                for (int i = 0; i < Convert.ToInt32(lavaP.Value); i++)
                                {
                                    botIsPlacing = true;
                                    con.Send(worldKey, new object[] { 1, ax + i, ay, 0 });
                                    Thread.Sleep(15);
                                    con.Send(worldKey, new object[] { 0, ax + i, ay, 119 });
                                    Thread.Sleep(15);
                                    con.Send(worldKey, new object[] { 1, ax + i, ay, BGcolor });
                                    Thread.Sleep(15);
                                }
                                botIsPlacing = false;
                                #region commented
                                //con.Send(worldKey, new object[] { 1, ax + 2, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 3, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 3, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 4, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 4, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 5, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 5, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 6, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 6, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 7, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 7, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 8, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 8, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 9, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 9, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 10, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 10, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 11, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 11, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 12, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 12, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 13, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 13, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 14, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 14, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 15, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 15, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 16, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 16, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 17, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 17, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 18, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 18, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 19, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 19, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 20, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 20, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 21, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 21, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 22, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 22, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 23, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 23, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 24, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 24, ay, BGcolor });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 0, ax + 25, ay, 119 });
                                //Thread.Sleep(250);
                                //con.Send(worldKey, new object[] { 1, ax + 25, ay, BGcolor });
                                #endregion
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
                                con.Send(worldKey, new object[] { 0, ax, ay, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 1, ay, 29 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax + 2, ay, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax + 3, ay, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 4, ay, 29 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 5, ay, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax, ay + 1, 46 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 5, ay + 1, 46 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax - 1, ay + 1, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax + 6, ay + 1, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 2, ay + 1, 800 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 7, ay + 1, 800 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 2, ay + 2, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 7, ay + 2, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 3, ay + 2, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 8, ay + 2, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 3, ay + 3, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 8, ay + 3, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 4, ay + 3, 800 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 9, ay + 3, 800 });
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
                                con.Send(worldKey, new object[] { 0, ax - 5, ay + 5, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 10, ay + 5, 42 });
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
                                con.Send(worldKey, new object[] { 0, ax - 5, ay + 10, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 10, ay + 10, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 4, ay + 10, 46 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 9, ay + 10, 46 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax - 4, ay + 11, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax + 9, ay + 11, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 4, ay + 12, 800 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 9, ay + 12, 800 });
                                Thread.Sleep(12);
                                //////////////////////////////////////////////////////////////////
                                con.Send(worldKey, new object[] { 0, ax - 3, ay + 12, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 8, ay + 12, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 3, ay + 13, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 8, ay + 13, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 2, ay + 13, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 7, ay + 13, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax - 2, ay + 14, 800 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 7, ay + 14, 800 });
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
                                con.Send(worldKey, new object[] { 0, ax, ay + 15, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 5, ay + 15, 42 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 1, ay + 15, 29 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 0, ax + 4, ay + 15, 29 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax + 2, ay + 15, 540 });
                                Thread.Sleep(12);
                                con.Send(worldKey, new object[] { 1, ax + 3, ay + 15, 540 });
                                #endregion
                            }
                        }
                        #endregion

                    }

                    return;
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
                            if (names.ContainsValue(stalkMover.Text))
                            {
                                if (stalkMover.Text.Contains(names[m.GetInt(0)].ToString()))
                                {
                                    con.Send("m", m.GetDouble(1), m.GetDouble(2), m.GetDouble(3), m.GetDouble(4), m.GetDouble(5), m.GetDouble(6), m.GetDouble(7), m.GetDouble(8), m.GetInt(9), m.GetBoolean(10), m.GetBoolean(11), m.GetBoolean(12));
                                }
                            }
                        }
                    }



                    return;
                case "god":
                    return;
                case "say":
                    if (botFullyConnected)
                    {
                        str = m.GetString(1);

                        if (m.GetInt(0) != botid)
                        {
                            if (names.ContainsKey(m.GetInt(0)))
                            {
                                chatbox.Items.Add(names[m.GetInt(0)].ToString() + ": " + m.GetString(1));


                                if (str.StartsWith("!autokick "))
                                {
                                    string[] option = str.Split(' ');
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
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
                                                        con.Send("say", "/pm " + names[m.GetInt(0)].ToString() + " Warning limit reached! You are getting banned.");
                                                        Thread.Sleep(5);
                                                        con.Send("say", "/ban " + names[m.GetInt(0)].ToString());
                                                    }
                                                    else
                                                    {
                                                        con.Send("say", "/kick " + names[m.GetInt(0)].ToString() + " Warning limit reached!");
                                                    }
                                                }
                                                else
                                                {
                                                    player[m.GetInt(0)].warnings = player[m.GetInt(0)].warnings + 1;
                                                    Thread.Sleep(250);
                                                    con.Send("say", names[m.GetInt(0)].ToString().ToUpper() + ": Please don't use /respawn. Warning " + player[m.GetInt(0)].warnings + " out of " + textBox1.Text + ".");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "/kick " + names[m.GetInt(0)].ToString() + " Please don't use /respawn command!");
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
                                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": AutoKick is already turned ON.");
                                                    }
                                                    else
                                                    {
                                                        autokickvalue.Checked = true;
                                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": AutoKick turned ON.");
                                                        #region BOT LOG
                                                        DefineLogZones();
                                                        Thread.Sleep(250);
                                                        log1.Text = "1. " + names[m.GetInt(0)].ToString().ToUpper() + " enabled autokick.";
                                                        #endregion
                                                    }
                                                }
                                                else
                                                {
                                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": AutoKick isn't allowed by whoever is using this bot.");
                                                }
                                            }
                                            else if (option[1] == "false" || option[1] == "off" || option[1] == "no")
                                            {
                                                if (autokickallowd.Checked)
                                                {
                                                    if (autokickvalue.Checked)
                                                    {
                                                        autokickvalue.Checked = false;
                                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": AutoKick turned OFF.");
                                                        #region BOT LOG
                                                        DefineLogZones();
                                                        Thread.Sleep(250);
                                                        log1.Text = "1. " + names[m.GetInt(0)].ToString().ToUpper() + " disabled autokick.";
                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": AutoKick is already turned OFF.");
                                                    }
                                                }
                                                else
                                                {
                                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": AutoKick isn't allowed by whoever is using this bot.");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Option doesn't exist or option misspellen.");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You are not an admin in the bot! D:<");
                                    }
                                }
                                else if (str.StartsWith("!kick "))
                                {
                                    if (kickCbox.Checked)
                                    {
                                        if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                        {
                                            string cmdPar = str.Substring(7);
                                            if (cmdPar.Length > 1)
                                            {
                                                string[] aaa = cmdPar.Split(' ');
                                                string[] fullSource = str.Split(' ');
                                                string kicking = fullSource[1];

                                                if (names.ContainsValue(kicking))
                                                {
                                                    string reasson = cmdPar.Replace(kicking, "");
                                                    reasson = reasson.Substring(reasson.Length - (names[m.GetInt(0)].Length + 1), reasson.Length);
                                                    if (reasson == "")
                                                    {
                                                        reasson = "The bot admin " + names[m.GetInt(0)].ToString() + " has kicked you.";
                                                    }

                                                    con.Send("say", "/kick " + kicking + " " + reasson);
                                                    #region BOT LOG
                                                    DefineLogZones();
                                                    Thread.Sleep(250);
                                                    log1.Text = "1. " + names[m.GetInt(0)].ToString().ToUpper() + " kicked " + kicking + ".";
                                                    #endregion
                                                }
                                                else
                                                {
                                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Unknown username.");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Command not used correctly.");
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You are not an admin in the bot! D:<");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Command Disabled. >:)");
                                    }
                                }
                                else if (str.StartsWith("!revert "))
                                {
                                    if (revertCboxLOL.Checked)
                                    {
                                        if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                        {
                                            string[] me = str.Split(' ');
                                            string revertin = me[1];

                                            con.Send("say", "/pm " + names[m.GetInt(0)] + "[R42Bot++] Reverting " + revertin);
                                            for (int x = 0; x < worldWidth; x++)
                                            {
                                                for (int y = 0; y < worldHeight; y++)
                                                {
                                                    if (block[x, y].placer == revertin)
                                                    {
                                                        con.Send(worldKey, 0, x, y, block[x, y].BlockID);
                                                    }
                                                }
                                            }
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Done reverting [" + revertin + "]!");

                                            //for (int test = 0; test < block.Length; test++)
                                            //{
                                            //    if (block[test].placer == revertin)
                                            //    {

                                            //    }
                                            //}
                                        }
                                        else
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You are not an admin in the bot! D:<");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Command Disabled. >:)");
                                    }
                                }
                                else if (str.StartsWith("!snakespeed "))
                                {
                                    if (revertCboxLOL.Checked)
                                    {
                                        if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
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
                                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Snake speed " + valu + " is not accepted.");
                                                    Thread.Sleep(200);
                                                }
                                                else
                                                {
                                                    decimal value = Convert.ToDecimal(valu);
                                                    numericUpDown1.Value = value;
                                                    #region BOT LOG
                                                    DefineLogZones();
                                                    Thread.Sleep(250);
                                                    log1.Text = "1. " + names[m.GetInt(0)].ToString().ToUpper() + " changed snake speed to " + valu + "ms.";
                                                    #endregion
                                                    Thread.Sleep(200);
                                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Snake speed changed to " + valu);
                                                    Thread.Sleep(200);
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Value was not a number.");
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You are not an admin in the bot! D:<");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Command Disabled. >:)");
                                    }
                                }
                                else if (str.StartsWith("!name "))
                                {
                                    if (krockhateseers.Checked)
                                    {
                                        if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                        {
                                            string NewName = str.Substring(6);
                                            con.Send("name", NewName);
                                        }
                                        else
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You are not an admin in the bot! D:<");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Command Disabled. >:)");
                                    }
                                }
                                else if (str.StartsWith("!admins"))
                                {
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        string admins = "";

                                        foreach (string namez in Admins.Items)
                                        {
                                            admins += namez.ToUpper() + ",";
                                        }

                                        if (admins == "")
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)].ToString() + " [R42Bot++] Bot admins:");
                                            con.Send("say", "/pm " + names[m.GetInt(0)].ToString() + " No one.");
                                        }
                                        else
                                        {
                                            con.Send("say", "/pm " + names[m.GetInt(0)].ToString() + " [R42Bot++] Bot admins:");
                                            con.Send("say", "/pm " + names[m.GetInt(0)].ToString() + " " + admins);
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You can't see bot admins cause you are not an admin!");
                                    }
                                }
                                else if (str.StartsWith("!microphone "))
                                {
                                    string begin = str.Substring(0, 12);
                                    string message = str.Replace(begin, "");
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        con.Send("say", "[R42Bot++] Listen Everybody!");
                                        Thread.Sleep(1000);
                                        con.Send("say", "[R42Bot++] Please Listen!!!");
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
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString() + ": You are not an admin in the bot!");
                                    }
                                }
                                else if (str.StartsWith("!affect ")) // must be StartsWith(" "), so, if the commands starts like that... The blank space is for username! (if you dont want it just remove it, and it will be !affectexample (example as user)
                                {
                                    string[] username = str.Split(' '); // usernameGetter, if you removed blank space, it must be str.Split('');
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        // If user that said this is an admin...
                                        if (names.ContainsValue(username[1])) //username[1] is the string[] we made.
                                        {
                                            // the user will be affected.
                                            con.Send("say", "/giveedit " + username[1]);
                                            Thread.Sleep(100); // delay 100ms
                                            con.Send("say", "/removeedit " + username[1]);
                                            Thread.Sleep(100);
                                            con.Send("say", "/togglepotions off");
                                            Thread.Sleep(100);
                                            con.Send("say", "/togglepotions on");
                                            Thread.Sleep(100);
                                            con.Send("say", "/respawn " + username[1]);
                                            Console.WriteLine(username[1] + " has been affected by admin " + names[m.GetInt(0)].ToString() + "."); // Console.WriteLine writes something to Output, actually, you can make an listbox that tells everything admins did!
                                        }
                                        else
                                        {
                                            con.Send("say", names[m.GetInt(0)].ToString() + ": You can't affect '" + username[1] + "' cause it isn't an valid username or this user isn't in this world.");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", names[m.GetInt(0)].ToString() + ": You little troller! You can't affect people if you aren't an admin in the bot! >:O");
                                    }
                                }
                                else if (str.StartsWith("!stalk "))
                                {
                                    string[] userinuse = str.Split(' ');
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        if (alstalking.Checked)
                                        {
                                            if (names.ContainsValue(userinuse[1]))
                                            {
                                                if (stalkMover.Text != userinuse[1])
                                                {
                                                    stalkMover.Text = "";
                                                    #region BOT LOG
                                                    DefineLogZones();
                                                    Thread.Sleep(250);
                                                    log1.Text = "1. " + names[m.GetInt(0)].ToString().ToUpper() + " made bot stalk " + userinuse[1].ToUpper() + ".";
                                                    #endregion
                                                    Thread.Sleep(250);
                                                    stalkMover.Text = userinuse[1];
                                                }
                                                else
                                                {
                                                    if (pmresult.Checked)
                                                    {
                                                        con.Send("say", "/pm " + names[m.GetInt(0)].ToString() + " [R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": " + userinuse[1] + " is already in the stalking list!");
                                                    }
                                                    else
                                                    {
                                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": " + userinuse[1] + " is already in the stalking list!");
                                                    }
                                                    // if is already in Stalking's List.
                                                }
                                            }
                                            else
                                            {

                                                if (pmresult.Checked)
                                                {
                                                    con.Send("say", "/pm " + names[m.GetInt(0)].ToString() + " [R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": " + userinuse[1] + " is not in this world!");
                                                }
                                                else
                                                {
                                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": " + userinuse[1] + " is not in this world!");
                                                }
                                                // if not in world
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Stalking is disabled. Sorry!");
                                        }
                                    }
                                    else
                                    {
                                        if (userinuse[1] == ".realwizard42." || userinuse[1] == ".REALWIZARD42.")
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": " + userinuse[1] + " is the bot and you can't make bot stalk people since you are not an admin in the bot!");
                                        }
                                        else
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": " + userinuse[1] + " can't be stalked because you are not an admin!");
                                        }
                                        // if not an admin
                                    }
                                }
                                else if (str.StartsWith("!unstalk "))
                                {
                                    string[] userinuse = str.Split(' ');
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        if (names.ContainsValue(userinuse[1]))
                                        {
                                            if (stalkMover.Text == userinuse[1])
                                            {
                                                stalkMover.Text = "";
                                            }
                                            else
                                            {
                                                if (pmresult.Checked)
                                                {
                                                    con.Send("say", "/pm " + names[m.GetInt(0)].ToString() + " [R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": " + userinuse[1] + " is not in the stalking list!");
                                                }
                                                else
                                                {
                                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": " + userinuse[1] + " is not in the stalking list!");
                                                }
                                                // if isn't in Stalking's List.
                                            }
                                        }
                                        else
                                        {
                                            if (pmresult.Checked)
                                            {
                                                con.Send("say", "/pm " + names[m.GetInt(0)].ToString() + " [R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": " + userinuse[1] + " is not in this world!");
                                            }
                                            else
                                            {
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": " + userinuse[1] + " is not in this world!");
                                            }
                                            // if not in world
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": " + userinuse[1] + " can't be removed from stalking, because you aren't an admin!");
                                        // if not an admin
                                    }
                                }
                                else if (str.StartsWith("!mywins"))
                                {
                                    if (winsystem1.Checked == true)
                                    {
                                        if (enus.Checked == true && ptbr.Checked == false)
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You won " + player[m.GetInt(0)].wins + " times.");
                                        }
                                        else if (ptbr.Checked == true && enus.Checked == false)
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Voçê ganhou " + player[m.GetInt(0)].wins + " vezes.");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Wins System has been disabled by the user using this bot.");
                                    }
                                }
                                else if (str.StartsWith("!say "))
                                {
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        string beginz = str.Substring(0, 5);
                                        string endz = str.Replace(beginz, "");
                                        con.Send("say", "[R42Bot++] " + endz);
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You are not an admin in the bot!");
                                    }
                                }
                                else if (str.StartsWith("!amiadmin"))
                                {
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Yes, you are an admin in the bot.");
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": No, you aren't an admin in the bot.");
                                    }
                                }
                                #region is [player]...
                                else if (str.StartsWith("!is "))
                                {
                                    string[] userinuse = str.Split(' ');
                                    if (str.StartsWith("!is " + userinuse[1] + " admin"))
                                    {
                                        if (Admins.Items.Contains(userinuse[1]))
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Yes, " + userinuse[1] + " is an admin in the bot.");
                                        }
                                        else
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": No, " + userinuse[1] + " is not an admin in the bot.");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Unknown question or misspellen question.");
                                    }
                                }
                                #endregion
                                else if (str.StartsWith("!botlog"))
                                {
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        con.Send("say", "===-===-===-===");
                                        Thread.Sleep(575);
                                        con.Send("say", "==---BOT_LOG---==");
                                        Thread.Sleep(575);
                                        con.Send("say", "===-===-===-===");
                                        Thread.Sleep(575);
                                        if (log1.Text != "")
                                        {
                                            con.Send("say", log1.Text);
                                            Thread.Sleep(575);
                                        }
                                        else
                                        {
                                            con.Send("say", "1. Empty");
                                            Thread.Sleep(575);
                                        }
                                        if (log2.Text != "")
                                        {
                                            con.Send("say", log2.Text);
                                            Thread.Sleep(575);
                                        }
                                        else
                                        {
                                            con.Send("say", "2. Empty");
                                            Thread.Sleep(575);
                                        }
                                        if (log3.Text != "")
                                        {
                                            con.Send("say", log3.Text);
                                            Thread.Sleep(575);
                                        }
                                        else
                                        {
                                            con.Send("say", "3. Empty");
                                            Thread.Sleep(575);
                                        }
                                        if (log4.Text != "")
                                        {
                                            con.Send("say", log4.Text);
                                            Thread.Sleep(575);
                                        }
                                        else
                                        {
                                            con.Send("say", "4. Empty");
                                            Thread.Sleep(575);
                                        }
                                        if (log5.Text != "")
                                        {
                                            con.Send("say", log5.Text);
                                            Thread.Sleep(575);
                                        }
                                        else
                                        {
                                            con.Send("say", "5. Empty");
                                            Thread.Sleep(575);
                                        }
                                        con.Send("say", "===-===-===-===");
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": To view bot log you must be an admin in the bot!");
                                    }
                                }

                                #region POLL COMMANDS

                                else if (str.StartsWith("!vote "))
                                {
                                    if (votersList.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You've already voted!");
                                    }
                                    else if (pollname.Text == "")
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": No poll is currently in progress!");
                                    }
                                    else
                                    {
                                        if (names[m.GetInt(0)].ToString() == pollstartername.Text)
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You can't vote because you made the poll!");
                                        }
                                        else
                                        {
                                            string[] voted = str.Split(' ');
                                            if (voted[1] == choice1.Text.ToLower() || voted[1] == choice1.Text)
                                            {
                                                votersList.Items.Add(names[m.GetInt(0)].ToString());
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You voted " + choice1.Text + ".");
                                                int votes1 = Convert.ToInt32(vot1.Text);
                                                votes1 = votes1 + 1;
                                                Thread.Sleep(250);
                                                string nuvots = Convert.ToString(votes1);
                                                vot1.Text = nuvots;
                                            }
                                            else if (voted[1] == choice2.Text.ToLower() || voted[1] == choice2.Text)
                                            {
                                                votersList.Items.Add(names[m.GetInt(0)].ToString());
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You voted " + choice2.Text + ".");
                                                int votes2 = Convert.ToInt32(vot2.Text);
                                                votes2 = votes2 + 1;
                                                Thread.Sleep(250);
                                                string nuvots = Convert.ToString(votes2);
                                                vot2.Text = nuvots;
                                            }
                                            else if (voted[1] == choice3.Text.ToLower() || voted[1] == choice3.Text)
                                            {
                                                votersList.Items.Add(names[m.GetInt(0)].ToString());
                                                if (choice3.Visible == true)
                                                {
                                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You voted " + choice3.Text + ".");
                                                    int votes3 = Convert.ToInt32(vot3.Text);
                                                    votes3 = votes3 + 1;
                                                    Thread.Sleep(250);
                                                    string nuvots = Convert.ToString(votes3);
                                                    vot3.Text = nuvots;
                                                }
                                                else
                                                {
                                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Tirth option has been removed by whoever is using this bot.");
                                                }
                                            }
                                            else
                                            {
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Unknown Option for voting polls.");
                                            }
                                        }
                                    }
                                }
                                else if (str.StartsWith("!pc1 "))
                                {
                                    string[] choiced = str.Split(' ');
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        if (pollname.Text == "")
                                        {
                                            if (choice1.Text == choiced[1])
                                            {
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": The first choice is the same as the new one!");
                                            }
                                            else
                                            {
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Choice1 changed to " + choiced[1] + ".");
                                                choice1.Text = choiced[1];
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": There is an poll in progress!");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You aren't an admin in the bot so you can't change the bot's poll choices!");
                                    }
                                }
                                else if (str.StartsWith("!pc2 "))
                                {
                                    string[] choiced = str.Split(' ');
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        if (pollname.Text == "")
                                        {
                                            if (choice2.Text == choiced[1])
                                            {
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": The second choice is the same as the new one!");
                                            }
                                            else
                                            {
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Choice2 changed to " + choiced[1] + ".");
                                                choice2.Text = choiced[1];
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": There is a poll in progress!");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You aren't an admin in the bot so you can't change the bot's poll choices!");
                                    }
                                }
                                else if (str.StartsWith("!pc3 "))
                                {
                                    string[] choiced = str.Split(' ');
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        if (pollname.Text == "")
                                        {
                                            if (choice3.Text == choiced[1])
                                            {
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": The tirth choice is the same as the new one!");
                                            }
                                            else
                                            {
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Choice3 changed to " + choiced[1] + ".");
                                                choice3.Text = choiced[1];
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": There is a poll in progress!");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You aren't an admin in the bot so you can't change the bot's poll choices!");
                                    }
                                }
                                else if (str.StartsWith("!endpoll"))
                                {
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        if (pollname.Text == "")
                                        {
                                            con.Send("say", "[R42Bot++] " + Shortest(names[m.GetInt(0)]).ToUpper() + ": There is no polls at progress.");
                                        }
                                        else
                                        {
                                            if (vot3.Visible == false)
                                            {
                                                con.Send("say", "[R42Bot++] " + Shortest(names[m.GetInt(0)]).ToUpper() + ": Poll '" + pollname.Text + "' stoped.");
                                                Thread.Sleep(575);
                                                con.Send("say", "[R42Bot++] " + Shortest(names[m.GetInt(0)]).ToUpper() + ": Results: " + choice1.Text + " - " + vot1.Text + " & " + choice2.Text + " - " + vot2.Text);
                                            }
                                            else
                                            {
                                                con.Send("say", "[R42Bot++] " + Shortest(names[m.GetInt(0)]).ToUpper() + ": Poll '" + pollname.Text + "' stoped.");
                                                Thread.Sleep(575);
                                                con.Send("say", "[R42Bot++] " + Shortest(names[m.GetInt(0)]).ToUpper() + ": Results: " + choice1.Text + " - " + vot1.Text + " , " + choice2.Text + " - " + vot2.Text + " & " + choice3.Text + " - " + vot3.Text);
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
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You aren't an admin in the bot so you can't end polls!");
                                    }
                                }
                                else if (str.StartsWith("!poll "))
                                {
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        if (pollname.Text == "")
                                        {
                                            if (vot3.Visible == false)
                                            {
                                                string beginz = str.Substring(0, 6);
                                                string endz = str.Replace(beginz, "");

                                                pollname.Text = endz;
                                                pollstartername.Text = names[m.GetInt(0)].ToString();
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
                                                pollstartername.Text = names[m.GetInt(0)].ToString();
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
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": There is already an poll in progress!");
                                        }
                                    }
                                    else
                                    {
                                        Thread.Sleep(575);
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You can't start polls cause you aren't an admin!");
                                    }
                                }
                                else if (str.StartsWith("!pollhelp"))
                                {
                                    con.Send("say", "[R42Bot++] " + Shortest(names[m.GetInt(0)]).ToUpper() + ": !vote [option], !poll [name], !endpoll,");
                                    Thread.Sleep(575);
                                    con.Send("say", "[R42Bot++] " + Shortest(names[m.GetInt(0)]).ToUpper() + ": !pc1 [choice1], !pc2 [choice2] and !pc3 [choice3].");
                                }
                                #endregion
                                else if (str.StartsWith("!giveeditall"))
                                {
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        foreach (Player s in player)
                                        {
                                            con.Send("say", "/giveedit " + s.username);
                                            Thread.Sleep(200);
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": You can't give everyone edit cause you are not an admin in the bot!");
                                    }
                                }
                                else if (str.StartsWith("!removeditall"))
                                {
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        foreach (Player s in player)
                                        {
                                            con.Send("say", "/removeedit " + s.username);
                                            Thread.Sleep(575);
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + Shortest(names[m.GetInt(0)]).ToUpper() + ": You can't remove everyone's edit cause you are not an admin in the bot!");
                                    }
                                }
                                else if (str.StartsWith("!download"))
                                {
                                    con.Send("say", "[R42Bot++] " + Shortest(names[m.GetInt(0)]).ToUpper() + ": http://realmaster42-projects.weebly.com/r42bot1.html");
                                }


                                else if (str.StartsWith("!listhelp"))
                                {
                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !amiadmin, !botlog, !kick [player] [reasson], !save, !loadlevel, !clear, !evenhelp c:");
                                }
                                else if (str.StartsWith("!evenhelp"))
                                {
                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !revert [player], !snakespeed [speed_in_ms]. HOORAY!");
                                }
                                else if (str.StartsWith("!giveedithelp"))
                                {
                                    con.Send("say", "[R42Bot++] " + Shortest(names[m.GetInt(0)]).ToUpper() + ": !removeditall, !giveeditall");
                                }
                                else if (str.StartsWith("!specialhelp"))
                                {
                                    con.Send("say", "[R42Bot++] " + Shortest(names[m.GetInt(0)]).ToUpper() + ": !giveedithelp");
                                }
                                else if (str.StartsWith("!more"))
                                {
                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !specialhelp, !listhelp, ");
                                    Thread.Sleep(575);
                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !say [msg], !affect [plr],");
                                    Thread.Sleep(575);
                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !microphone [msg], !pollhelp, !is [plr] admin. c:");
                                }

                                #region HELP COMMAND
                                else if (str.StartsWith("!help")) // COMMANDYS COMMANDAS COMMANOS OMG
                                {
                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !more, !chelp, !download, !mywins, !halp,");
                                    Thread.Sleep(575);
                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !version, !survival [plr], !creative [plr]. c:");
                                }
                                else if (str.StartsWith("!halp"))
                                {
                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !stalk [plr], !unstalk [plr]");
                                }
                                #endregion

                                else if (str.StartsWith("!chelp "))
                                {
                                    string[] command = str.Split(' ');
                                    #region commands
                                    if (command[1] == "chelp")
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !chelp [command]. Makes you know how to use the command and how it works.");
                                    }
                                    else if (command[1] == "help")
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !help. Makes you know the commands available in the bot.");
                                    }
                                    else if (command[1] == "more")
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !more. Makes you know more commands available in the bot.");
                                    }
                                    else if (command[1] == "mywins")
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !mywins. Makes you know your own wins, doesn't work if it is disabled!");
                                    }
                                    else if (command[1] == "affect")
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !affect [plr]. This command makes players sometimes get kicked or lag.");
                                    }
                                    else if (command[1] == "amiadmin")
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !amiadmin. Checks and tells you if you are an admin in the bot or not.");
                                    }
                                    else if (command[1] == "is admin")
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !is [plr] admin. Checkes and tells you if the player is an admin in the bot or not.");
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Command Mispellen or Unknown command. CommandHelp couldn't recognize that command!");
                                    }
                                    #endregion


                                }



                                else if (str.StartsWith("!version"))
                                {
                                    con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Bot version " + version);
                                }
                                else if (str.StartsWith("!creative "))
                                {
                                    string[] split = str.Split(' ');
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
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
                                            else if (names.ContainsValue(split[1]))
                                            {
                                                con.Send("say", "/giveedit " + split[1]);
                                                Thread.Sleep(200);
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": " + split[1].ToUpper() + " is now in creative mode.");
                                                Thread.Sleep(200);
                                                con.Send("say", "/pm " + split[1] + " [R42Bot++] hey... you are now in creative mode!");
                                                Thread.Sleep(200);
                                            }
                                            else
                                            {
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": " + split[1] + " isn't in this world or isn't an valid username.");
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !creative [plr] command has been disabled by who is using this bot...");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": you are not an admin in the bot! D:<");
                                    }
                                }
                                else if (str.StartsWith("!survival "))
                                {
                                    string[] split = str.Split(' ');
                                    if (Admins.Items.Contains(names[m.GetInt(0)].ToString()))
                                    {
                                        if (scommand.Checked)
                                        {
                                            if (split[1] == "nikooooooos" && names.ContainsValue("nikooos"))
                                            {
                                                con.Send("say", "/removeedit NIKOOOOOOOS");
                                                con.Send("say", "[R42Bot++] NIKOOO(...)s is now in survival mode.");
                                                con.Send("say", "/pm NIKOOOOOOOS [R42Bott+] hey... you are now in survival mode.");
                                            }
                                            else if (names.ContainsValue(split[1]))
                                            {
                                                con.Send("say", "/removeedit " + split[1]);
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + " " + split[1].ToUpper() + " is now in survival mode.");
                                                con.Send("say", "/pm " + split[1] + " [R42Bot++] hey... you are now in survival mode!");
                                            }
                                            else
                                            {
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": " + split[1].ToUpper() + " isn't in this world or isn't an valid username.");
                                            }
                                        }
                                        else
                                        {
                                            con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": !survival [plr] command has been disabled by who is using this bot...");
                                        }
                                    }
                                    else
                                    {
                                        con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": you are not an admin in the bot! D:<");
                                    }
                                }


                                else
                                {
                                    if (str.StartsWith("!"))
                                    {
                                        if (names[m.GetInt(0)].ToString() == botName)
                                        {
                                            Console.WriteLine("Bot tried to spam. Nevermind that, bot >:O");
                                        }
                                        else
                                        {
                                            if (str.StartsWith("$"))
                                            {
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Wrong Prefix.");
                                            }
                                            else
                                            {
                                                con.Send("say", "[R42Bot++] " + names[m.GetInt(0)].ToString().ToUpper() + ": Command misspelen or Unknown Command.");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                names.Add(m.GetInt(0), null);
                            }
                        }
                    }

                    return;

            }
        }

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
                            MessageBox.Show("Email, Password and WorldID must be fillen up!", "R42Bot++ v" + version + " System");
                        }
                        else
                        {
                            MessageBox.Show("TokenID and WorldID must be fillen up!", "R42Bot++ v" + version + " System");
                        }
                    }
                }
                else if (email.Text == "Email" && (pass.Text == "" && pass.Enabled == true) && idofworld.Text == "ID do mundo")
                {
                    if (ptbr.Checked == true && enus.Checked == false)
                    {
                        MessageBox.Show("O Email, a password e o ID do mundo tenhem de ser preenchidos.", "R42Bot++ v" + version + " System");
                    }
                }
                else
                {
                    if (!isFacebook.Checked)
                    {
                        this.Connect(email.Text, pass.Text, idofworld.Text, false);
                    }
                    else
                    {
                        this.Connect(email.Text, "", idofworld.Text, true);
                    }

                    try
                    {
                        con.Send("init");
                        con.Send("access");
                    }
                    catch (PlayerIOError error)
                    {
                        MessageBox.Show(error.Message);
                    }
                    if (botName == null)
                    {

                    }
                    else
                    {
                        Admins.Items.Add(botName.ToString());
                    }
                    connector.Text = "Disconnect";
                }
            }
            else if (connector.Text == "Disconnect")
            {
                lavaP.Enabled = false;
                botFullyConnected = false;
                BlockPlacingTilVal1 = 1;
                BlockPlacingTilVal2 = 2;
                con.Disconnect();

                connector.Text = "Connect";
                Admins.Items.Remove(botName);
                MessageBox.Show("Disconnected.");
            }

        }

        public struct Player
        {
            public int x, y, userid;
            public string username;
            public bool isGuest;
            public bool isGod, isAdmin, isFriend;

            public int wins,
                warnings;

        }


        public void Command(string command, string user)
        {
            if (user != null)
            {
                if (command.Contains("/"))
                {
                    con.Send("say", command + " " + user);
                }
                else
                {
                    con.Send("say", "/" + command + " " + user);
                }
            }
            else
            {
                if (!"clear|save|woot|levelcomplete".Contains(command))
                {
                    if (command.Contains("/"))
                    {
                        con.Send("say", command);
                    }
                    else
                    {
                        con.Send("say", "/" + command);
                    }
                }
                else
                {
                    switch (command)
                    {
                        case "clear":
                            con.Send("clear");
                            break;
                        case "save":
                            con.Send("save");
                            break;
                        case "like":
                            con.Send("like");
                            break;
                        case "levelcomplete":
                            con.Send("levelcomplete");
                            break;
                    }
                }
            }
        }

        public void KickUser(string username)
        {
            con.Send("say", "/kick " + username);
        }

        public void KillUser(string username)
        {
            con.Send("say", "/kill " + username);
        }

        public void BanUser(string username)
        {
            con.Send("say", "/ban " + username);
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (Admins.Items.Contains(removeText.Text))
            {
                if (removeText.Text == "marcoantonimsantos")
                {
                    MessageBox.Show("You cant remove marcoantonimsantos, he made this bot!", "R42Bot v" + version + " System");
                    removeText.Clear();
                }
                else if (removeText.Text == "realmaster")
                {
                    MessageBox.Show("You cant remove realmaster, he made this bot!", "R42Bot v" + version + " System");
                    removeText.Clear();
                }
                else if (removeText.Text == "awzome")
                {
                    MessageBox.Show("You cant remove awzome, hes my best EE friend!", "R42Bot v" + version + " System");
                    removeText.Clear();
                }
                else if (removeText.Text == botName)
                {
                    MessageBox.Show("You cant remove " + botName + ", it is the bot!", "R42Bot v" + version + " System");
                    removeText.Clear();
                }
                else if (removeText.Text == "legitturtle09")
                {
                    MessageBox.Show("You cant remove legitturtle09, he helped with this bot!", "R42Bot v" + version + " System");
                    removeText.Clear();
                }
                else
                {
                    Admins.Items.Remove(removeText.Text);
                    removeText.Clear();
                }
            }
            else
            {
                MessageBox.Show(removeText.Text + " isn't a admin!");
                removeText.Clear();
            }

        }

        private void add_Click(object sender, EventArgs e)
        {
            if (!Admins.Items.Contains(addText.Text))
            {
                if (names.ContainsValue(addText.Text))
                {
                    Admins.Items.Add(addText.Text);
                    Thread.Sleep(250);
                    addText.Clear();
                }
                else
                {
                    MessageBox.Show(addText.Text + " is not in the connected world!");
                    addText.Clear();
                }
            }
            else
            {
                MessageBox.Show(addText.Text + " is already in the list...");
                addText.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("StalkInfo.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Information));
                FileStream read = new FileStream("StalkInfo.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                Information info = (Information)xs.Deserialize(read);
                if (info.Data5 == "r0")
                {
                    alstalking.Checked = false;
                }
                else
                {
                    alstalking.Checked = true;
                }
            }
            if (File.Exists("R42Bot++Customization.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Information));
                FileStream read = new FileStream("R42Bot++Customization.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                Information info = (Information)xs.Deserialize(read);
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
                    stalkmovementpage.BackColor = Color.White;
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
                    stalkmovementpage.BackColor = info.Color1;
                    smileytabs.BackColor = info.Color1;
                }
                #endregion
            }

            if (File.Exists("R42Bot++SavedData.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Information));
                FileStream read = new FileStream("R42Bot++SavedData.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                Information info = (Information)xs.Deserialize(read);
                textBox2.Text = info.Data1;
                textBox3.Text = info.Data2;
                textBox1.Text = info.Data3;
                textBox4.Text = info.Data4;
            }
            Thread.Sleep(250);
            #region restriction commands
            #region /respawn
            if (textBox2.Text == "r0")
            {
                noRespawn.Checked = false;
            }
            else
            {
                noRespawn.Checked = true;
            }

            if (textBox3.Text == "r0")
            {
                warningGiver.Checked = false;
            }
            else
            {
                warningGiver.Checked = true;
            }

            if (textBox4.Text == "r0")
            {
                bwl.Checked = false;
            }
            else
            {
                bwl.Checked = true;
            }
            #endregion
            #endregion

            this.Text = this.Text + this.version;

            //Checks or it needs to run the downloader
            if (new System.Net.WebClient().DownloadString(versionlink) != this.version)
            {
                upgradedVersion = new System.Net.WebClient().DownloadString(versionlink);
                label48.Text = "Your R42Bot++ version (" + version + ") is outdated! Newest version is " + upgradedVersion + " ! ";
            }
            else
            {
                label48.Visible = true;
                label48.ForeColor = Color.DarkOliveGreen;
                label48.Text = "Your R42Bot++ version (" + version + ") is up-to-date.";
            }
        }

        private void botconnectionfail_Click(object sender, EventArgs e)
        {
            if (enus.Checked == true && ptbr.Checked == false)
            {
                MessageBox.Show(possible_causes_botconnect_eu[new Random().Next(0, possible_causes_botconnect_eu.Length + 1)]);
            }
            else if (ptbr.Checked == true && enus.Checked == false)
            {
                MessageBox.Show(possible_causes_botconnect_pt[new Random().Next(0, possible_causes_botconnect_pt.Length + 1)]);
            }
        }

        private void scommand_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void enus_CheckedChanged(object sender, EventArgs e)
        {
            if (enus.Checked == true)
            {
                MessageBox.Show("Language is now EU/US!", "R42Bot++ v" + version + " System");
                ltu.Checked = false;
                ptbr.Checked = false;
                Translate();
            }
            else if (!ptbr.Checked && !ltu.Checked)
            {
                enus.Checked = true;
                Translate();
            }
        }

    private void ptbr_CheckedChanged(object sender, EventArgs e)
        {
            if (ptbr.Checked == true)
            {
                MessageBox.Show("A Linguagem é PT/BR agora.", "R42Bot++ v" + version + " System");
                ltu.Checked = false;
                enus.Checked = false;
                Translate();
            }
            else if (!enus.Checked && !ltu.Checked)
            {
                enus.Checked = true;
                Translate();
            }
        }

        private void autobuild1_CheckedChanged(object sender, EventArgs e)
        {
            if (autobuild1.Checked)
            {
                MessageBox.Show("To start making an smiley border, use basic gray background.");
            }
        }

        private void winsystem1_CheckedChanged(object sender, EventArgs e)
        {
            if (winsystem1.Checked == true)
            {
                MessageBox.Show("Now, whenever someone touches a crown, it declares a win.", "R42Bot++ v" + version + " System");
            }
            else if (winsystem1.Checked == false)
            {
                MessageBox.Show("Wins System OFF.", "R42Bot++ v" + version + " System");
            }
        }

        private void clearstalkering_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sucefully removed " + stalkMover.Text + " from stalking user.");
            Thread.Sleep(250);
            stalkMover.Text = "";
        }

        private void telllemgend_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Some Message Boxes will appear. Do you wanna see them?", "R42Bot++ v" + version + " System", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                MessageBox.Show("*WorldData, Tells about WorldData system in the bot.", "R42Bot++ v" + version + " System");
                MessageBox.Show("*R42Bot++ System, System of this bot.", "R42Bot++ v" + version + " System");
                MessageBox.Show("Nothing, Default texts appearing.", "R42Bot++ v" + version + " System");
            }
            else
            {
                MessageBox.Show("No legend could be shown, message box progress has been cancelled.", "R42Bot++ v" + version + " System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void welcomemsg_TextChanged(object sender, EventArgs e)
        {

        }

        private void leftallmsg_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            con.Send(worldKey + "f", 5);
        }

        private void lavadrawer_CheckedChanged(object sender, EventArgs e)
        {
            if (lavadrawer.Checked)
            {
                MessageBox.Show("Now placing water bricks will auto-update.", "R42Bot++ v" + version + " System");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            con.Send(worldKey + "f", 0);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (enus.Checked)
            {
                Console.WriteLine("...S M I L E Y  C H A N G E R  C H O O S E  C H A N G E D...");
            }
            else if (ptbr.Checked)
            {
                Console.WriteLine("...E S C O L H A  D O  M U D A D O R  D E  S M I L E Y S  M U D A D A...");
            }
            else if (ltu.Checked)
            {
                Console.WriteLine("...S M A I L U  K E I T E J O  P A S I R I N K T I S  P A K E I S T A...");
            }
        }

        private void autochangerface_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                con.Send(worldKey + "f", 0);
                Thread.Sleep(500);
                con.Send(worldKey + "f", 5);
            }
        }

        private void autokick_Tick(object sender, EventArgs e)
        {
            if (autokickvalue.Checked)
            {
                foreach (Player kicking in player)
                {
                    if (!Admins.Items.Contains(kicking.username))
                    {
                        if (enus.Checked)
                        {
                            con.Send("say", "/kick " + kicking.username + " [R42Bot++] Autokick enabled.");
                        }
                        else if (ptbr.Checked)
                        {
                            con.Send("say", "/kick " + kicking.username + " [R42Bot++] Tirar do mapa automaticamente ativado.");
                        }
                        else if (ltu.Checked)
                        {
                            con.Send("say", "/kick " + kicking.username + " [R42Bot++] auto-Ispirimas ijungtas");
                        }
                        Thread.Sleep(200);
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
                    if (enus.Checked)
                    {
                        con.Send("say", "[R42Bot++] World reseted. Reseting marked for every " + msdelay + " ms!");
                    }
                    else if (ptbr.Checked)
                    {
                        con.Send("say", "[R42Bot++] Mundo resetado. «Reseting» marcado para cada " + msdelay + " ms!");
                    }
                    else if (ltu.Checked)
                    {
                        con.Send("say", "[R42Bot++] pasaulis restartoutas, nustatyta kiekvienai " + msdelay + " milisekundziu!");
                    }
                }
            }
        }

        private void tntallowd_CheckedChanged(object sender, EventArgs e)
        {
            if (tntallowd.Checked)
            {
                MessageBox.Show("Now whenever someones places a red checker block it will FALL and destroy!", "R42Bot++ v" + version + " System");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                Thread.Sleep(250);
                button7.Text = "New choice...";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Thread.Sleep(575);
            if (names.ContainsValue(userpm.Text))
            {
                con.Send("say", userpm.Text + " " + textpm.Text);
            }
            else
            {
                MessageBox.Show("'" + userpm.Text + "' isn't in the connected world." + "R42Bot++ v" + version + " System");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Thread.Sleep(575);
            con.Send("say", "[R42Bot++] " + saytext.Text);
        }

        private void srandomizer_Click(object sender, EventArgs e)
        {
            con.Send(worldKey + "f", faceslist[new Random().Next(0, faceslist.Length + 1)]);
        }
        
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string[] nums = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

            if (!nums.Contains(e.KeyChar.ToString()))
            {
                if (e.KeyChar != 8)
                {
                    MessageBox.Show("You must enter a valid number.", "R42Bot++ System v" + version);
                    Thread.Sleep(250);
                    textBox1.Text = "3";
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                // /respawn
                Information info = new Information();
                info.Data1 = textBox2.Text;
                info.Data2 = textBox3.Text;
                info.Data3 = textBox1.Text;
                info.Data4 = textBox4.Text;
                Class1.SaveData(info, "R42Bot++SavedData.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void noRespawn_CheckedChanged(object sender, EventArgs e)
        {
            if (noRespawn.Checked)
            {
                textBox2.Text = "r1";
            }
            else
            {
                textBox2.Text = "r0";
            }
        }

        private void warningGiver_CheckedChanged(object sender, EventArgs e)
        {
            if (warningGiver.Checked)
            {
                textBox3.Text = "r1";
            }
            else
            {
                textBox3.Text = "r0";
            }
        }

        private void bwl_CheckedChanged(object sender, EventArgs e)
        {
            if (bwl.Checked)
            {
                textBox4.Text = "r1";
            }
            else
            {
                textBox4.Text = "r0";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                // /respawn
                Information info = new Information();
                if (currentColor.Text == "Default")
                {
                    info.Color1 = Color.White;
                }
                else
                {
                    info.Color1 = c.Color;
                }
                Class1.SaveData(info, "R42Bot++Customization.xml");
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
            Main.BackColor = Color.White;
            pollTab.BackColor = Color.White;
            autoPage.BackColor = Color.White;
            NEWS.BackColor = Color.Beige;
            LanguageOrSettings.BackColor = Color.White;
            autobolder.BackColor = Color.White;
            advancedEditor.BackColor = Color.White;
            snakepage.BackColor = Color.White;
            stalkmovementpage.BackColor = Color.White;
            smileytabs.BackColor = Color.White;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            c.ShowDialog();
            currentColor.Text = c.Color.ToString();

            tabPage1.BackColor = c.Color;
            tabPage2.BackColor = c.Color;
            tabPage3.BackColor = c.Color;
            Main.BackColor = c.Color;
            pollTab.BackColor = c.Color;
            autoPage.BackColor = c.Color;
            NEWS.BackColor = c.Color;
            LanguageOrSettings.BackColor = c.Color;
            autobolder.BackColor = c.Color;
            advancedEditor.BackColor = c.Color;
            snakepage.BackColor = c.Color;
            stalkmovementpage.BackColor = c.Color;
            smileytabs.BackColor = c.Color;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "3";
            }
        }

        private void alstalking_CheckedChanged(object sender, EventArgs e)
        {
            if (alstalking.Checked)
            {
                textBox5.Text = "r1";
            }
            else
            {
                textBox5.Text = "r0";
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                // /respawn
                Information info = new Information();
                if (textBox5.Text == "r0")
                {
                    info.Data5 = "r0";
                }
                else
                {
                    info.Data5 = "r1";
                }
                Class1.SaveData(info, "StalkInfo.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void xBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch))
            {
                if (e.KeyChar != 8)
                {
                    e.Handled = true;
                    MessageBox.Show("X should be a valid number.");
                }
            }
            else
            {
                if (connector.Text != "Connect")
                {
                    
                }
                else
                {
                    MessageBox.Show("You are not connected to any world!");
                    e.Handled = true;
                }
            }
        }

        private void yBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch))
            {
                if (e.KeyChar != 8)
                {
                    e.Handled = true;
                    MessageBox.Show("Y should be a valid number.");
                }
            }
            else
            {
                if (connector.Text != "Connect")
                {
                    
                }
                else
                {
                    MessageBox.Show("You are not connected to any world!");
                    e.Handled = true;
                }
            }
        }

        private void fbTokenGet_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Access Token is a key for entering PlayerIO games from facebook, to find out your's, go to https://developers.facebook.com/tools/explorer/ in your browser.", "What's access token?", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox6.Text = "http://realmaster42-projects.weebly.com/r42bot1.html";
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            textBox7.Text = "https://trello.com/b/bs7b8Flx/r42bot";
        }

        private void freeadmin_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void waterchoice1_CheckedChanged(object sender, EventArgs e)
        {
            if (waterchoice1.Checked)
            {
                waterchoice2.Checked = false;
            }
            else
            {
                waterchoice2.Checked = true;
            }
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

        private void allowSnakeSpecial_CheckedChanged(object sender, EventArgs e)
        {
            if (allowSnakeSpecial.Checked)
            {
                BlockPlacer.Enabled = true;
            }
            else
            {
                BlockPlacer.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Information info = new Information();
                ListBox.ObjectCollection items = Admins.Items;
                string[] Items1 = new string[] { };
                for (int i=1;i<items.Count;i++)
                {
                    Items1[i] = items[i].ToString();
                }
                info.Admins = Items1;
                Class1.SaveData(info, "R42Bot++Admins.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists("R42Bot++Admins.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Information));
                FileStream read = new FileStream("R42Bot++Admins.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                Information info = (Information)xs.Deserialize(read);

                for (int i = 1; i < info.Admins.Length; i++)
                {
                    if (!Admins.Items.Contains(info.Admins[i]))
                    {
                        Admins.Items.Add(info.Admins[i]);
                    }
                }
            }
            else
            {
                MessageBox.Show("Save File not found. (R42Bot++Admins.xml)", "R42Bot++ System v"+version, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pgeb100loldef_CheckedChanged(object sender, EventArgs e)
        {
            if (pgeb100loldef.Checked)
            {
                pgeb100loldo.Checked = false;
            }
        }

        private void pgeb100loldo_CheckedChanged(object sender, EventArgs e)
        {
            if (pgeb100loldo.Checked)
            {
                pgeb100loldef.Checked = false;
            }
        }

        private void BlockPlacer_Tick(object sender, EventArgs e)
        {
            if (botFullyConnected)
            {
                int Delay = Convert.ToInt32(fdelay.Value);
                for (int i = BlockPlacingTilVal1; i < BlockPlacingTilVal2; i++)
                {
                    con.Send(worldKey, new object[] { 0, BlockPlacingTilX, BlockPlacingTilY, i });
                    con.Send(worldKey, new object[] { 1, BlockPlacingTilX, BlockPlacingTilY, i });
                    Thread.Sleep(Delay);
                }
            }
        }

        private void ltu_CheckedChanged(object sender, EventArgs e)
        {
            if (ltu.Checked == true)
            {
                MessageBox.Show("Kalba dabar yra LTU/LT", "R42Bot++ v" + version + " sistema");
                enus.Checked = false;
                ptbr.Checked = false;
                Translate();
            }
            else if (!ptbr.Checked && !enus.Checked)
            {
                enus.Checked = true;
                Translate();
            }
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

        private void waterchoice2_CheckedChanged(object sender, EventArgs e)
        {
            if (waterchoice2.Checked)
            {
                waterchoice1.Checked = false;
            }
            else
            {
                waterchoice1.Checked = true;
            }
        }

        private void xBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void yBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void idBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch))
            {
                if (e.KeyChar != 8)
                {
                    e.Handled = true;
                    MessageBox.Show("BlockID should be a valid number.");
                }
            }
        }

        private void fgCheck_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void bgCheck_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void idofit_TextChanged(object sender, EventArgs e)
        {

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

        private void addText_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (isFacebook.Checked)
            {
                email.Text = "Token ID";
                pass.Enabled = false;
                fbTokenGet.Visible = true;
            }
            else
            {
                email.Text = "Email";
                pass.Enabled = true;
                fbTokenGet.Visible = false;
            }
        }
    }
}

#endregion