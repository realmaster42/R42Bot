﻿//http://www.binpress.com/license/view/l/79c35f4cb0919616b8c86a8d466c0362
#region SOURCE
#region using...
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
using System.IO;
using System.Xml.Serialization;
using PlayerIOClient;
#endregion

#region BOT

namespace R42Bot
{

    public partial class Form1 : Form
    {
        public static ColorDialog c = new ColorDialog();

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
            if (enus.Checked)
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
            else if (ptbr.Checked)
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

        #region Connect(string...
        private void Connect(string email, string pass, string idofworld, bool isFB)
        {
            if (isFB == false)
            {
                try
                {
                    Variables.client = PlayerIO.QuickConnect.SimpleConnect("everybody-edits-su9rn58o40itdbnw69plyw", email, pass);
                    Variables.con = Variables.client.Multiplayer.JoinRoom(idofworld, null);
                    Variables.con.OnMessage += new MessageReceivedEventHandler(onMessage);
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
                    Variables.client = PlayerIO.QuickConnect.FacebookOAuthConnect("everybody-edits-su9rn58o40itdbnw69plyw", email, "");
                    Variables.con = Variables.client.Multiplayer.JoinRoom(idofworld, null);
                    Variables.con.OnMessage += new MessageReceivedEventHandler(onMessage);
                }
                catch (PlayerIOError error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }
        #endregion

        public void onMessage(object sender, PlayerIOClient.Message m)
        {
            switch (m.Type)
            {

                case "k":
                    Calls.Crown.Setup(m);
                    return;
                case "ks":
                    if (givegodwithtrophycbox.Checked)
                    {
                        Variables.con.Send("say", "/godon " + Variables.names[m.GetInt(0)]);
                    }
                    return;
                case "write":
                    return;
                case "updatemeta":
                    Calls.UpdateMeta.Setup(m);
                    return;
                case "init":
                    try
                    {
                        Calls.Init.Init2();
                        Thread.Sleep(575);
                        Calls.Init.Setup(m);
                        CallsSettings.Welcome_Text = welcomemsg.Text;
                        CallsSettings.Welcome_Text_2 = welcomemsg2.Text;
                        CallsSettings.Goodbye_Text = leftallmsg.Text;
                        CallsSettings.Goodbye_Text_2 = leftall2.Text;
                        lavaP.Maximum = Variables.worldWidth;
                        lavaP.Value = 1;
                        lavaP.Enabled = true;
                        boxHeightNUD.Maximum = Variables.worldHeight - 1;
                        boxWidthNUD.Maximum = Variables.worldWidth - 1;
                        Variables.blockIDs = new uint[2, m.GetInt(12), m.GetInt(13)];
                        Variables.blockPLACERs = new string[2, m.GetInt(12), m.GetInt(13)];
                        var chunks = InitParse.Parse(m);
                        foreach (var chunk in chunks)
                        {
                            foreach (var pos in chunk.Locations)
                            {
                                Variables.blockIDs[chunk.Layer, pos.X, pos.Y] = chunk.Type;
                            }
                        }

                        //Read(m, 20);//18);
                    }
                    catch (PlayerIOError Error)
                    {
                        MessageBox.Show(Error.Message);
                    }
                    return;
                case "add":
                    Calls.PlayersInGame.New.Setup(m);
                    Thread.Sleep(575);
                    if (freeadmin.Checked)
                    {
                        add.Enabled = false;
                        if (!Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                        {
                            Admins.Items.Add(Variables.names[m.GetInt(0)]);
                        }
                    }
                    else
                    {
                        add.Enabled = true;
                    }
                    return;
                case "access":
                    Variables.con.Send("say", Voids.GetLangFile(Variables.CurrentLang, 74));
                    return;
                case "lostaccess":
                    Variables.con.Send("say", Voids.GetLangFile(Variables.CurrentLang, 75));
                    return;
                case "left":
                    if (!kJoiners.Checked)
                    {
                        if (Variables.botFullyConnected)
                        {
                            Calls.PlayersInGame.Leave.Setup(m);
                            if (freeadmin.Checked)
                            {
                                Admins.Items.Remove(Variables.names[m.GetInt(0)]);
                                add.Enabled = false;
                            }
                            else
                            {
                                add.Enabled = true;
                            }

                            Thread.Sleep(250); // destroys username finally.
                            Calls.PlayersInGame.Leave.Fix(m);
                        }
                    }
                    return;
                case "b":
                    if (Variables.botFullyConnected)
                    {
                        Variables.blockIDs[m.GetInt(0), m.GetInt(1), m.GetInt(2)] = Convert.ToUInt32(m.GetInt(3));
                        int layer = m.GetInt(0);
                        int flayer = 0;
                        Variables.ax = m.GetInt(1); // left and right
                        Variables.ay = m.GetInt(2); //up and down
                        if (Variables.names.ContainsKey(m.GetInt(4)))
                        {
                            if (unfairBlox.Checked)
                            {
                                if (((Variables.player[m.GetInt(4)].BlocksPlacedInaSecond >= 30 && Variables.names[m.GetInt(4)] != Variables.botName) && !Admins.Items.Contains(Variables.names[m.GetInt(4)])) || (Variables.player[m.GetInt(4)].isBot && Variables.player[m.GetInt(4)].BlocksPlacedInaSecond >= 5))
                                {
                                    Variables.con.Send("say", "[R42Bot++] " + Variables.names[m.GetInt(4)].ToUpper() + " detected.");
                                    if (!Variables.player[m.GetInt(4)].AlreadyReedit)
                                    {
                                        Variables.con.Send("say", "/removeedit " + Variables.names[m.GetInt(4)]);
                                        Variables.player[m.GetInt(4)].AlreadyReedit = true;
                                    }
                                }
                                else
                                {
                                    Variables.player[m.GetInt(4)].BlocksPlacedInaSecond++;
                                }
                            }
                            Variables.blockPLACERs[layer, Variables.ax, Variables.ay] = Variables.names[m.GetInt(4)];
                        }
                        else
                        {
                            Variables.blockPLACERs[layer, Variables.ax, Variables.ay] = "* SYSTEM";
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
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                                Thread.Sleep(250);
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay, 0 });
                            }
                        }
                        #region LAVA SNAKE
                        if (lsbx.Checked)
                        {
                            if (blockID == 204)
                            {
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 203 });
                                Thread.Sleep(thedelay);
                            }
                            else if (blockID == 203)
                            {
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 202 });
                                Thread.Sleep(thedelay);
                            }
                            else if (blockID == 202)
                            {
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
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
                                    Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                                }
                                else if (pgeb100loldef.Checked)
                                {
                                    bool RainbowMineral = mineralRAINBOWFAST.Checked;
                                    Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                                    Thread.Sleep(12);
                                    Variables.CheckSnakeUpdate = false;
                                    mineralRAINBOWFAST.Checked = true;
                                    Thread.Sleep(12);
                                    Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 1, Variables.ay, 71 });
                                    Thread.Sleep(12);
                                    Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 1, Variables.ay, 71 });
                                    Thread.Sleep(12);
                                    Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 1, Variables.ay + 1, 71 });
                                    Thread.Sleep(12);
                                    Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 1, Variables.ay - 1, 71 });
                                    Thread.Sleep(12);
                                    Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 1, Variables.ay + 1, 71 });
                                    Thread.Sleep(12);
                                    Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 1, Variables.ay - 1, 71 });
                                    Thread.Sleep(12);
                                    Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay + 1, 71 });
                                    Thread.Sleep(12);
                                    Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay - 1, 71 });
                                    Thread.Sleep(12);
                                    Variables.CheckSnakeUpdate = true;
                                    CheckSnakes(Variables.currentChecked);
                                }
                            }
                        }
                        #endregion

                        if (boxPlaceCBOX.Checked)
                        {
                            if (blockID == 182 && !Variables.botIsPlacing)
                            {
                                int Wid = Convert.ToInt32(boxWidthNUD.Value);
                                int Hei = Convert.ToInt32(boxHeightNUD.Value);
                                Variables.botIsPlacing = true;
                                for (int i = 0; i < Wid; i++)
                                {
                                    Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + i, Variables.ay, 9 });
                                    Thread.Sleep(15);
                                }
                                for (int o = 0; o < Wid; o++)
                                {
                                    Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + o, Variables.ay + Hei, 9 });
                                    Thread.Sleep(15);
                                }
                                for (int p = 0; p < Hei; p++)
                                {
                                    Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay + p, 9 });
                                    Thread.Sleep(15);
                                }
                                for (int a = 0; a < Hei + 1; a++)
                                {
                                    Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + Wid, Variables.ay + a, 9 });
                                    Thread.Sleep(15);
                                }
                                Variables.botIsPlacing = false;
                            }
                        }

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

                            Variables.BlockPlacingTilVal1 = one;
                            Variables.BlockPlacingTilVal2 = two;
                            Variables.BlockPlacingTilX = Variables.ax;
                            Variables.BlockPlacingTilY = Variables.ay;
                        }
                        #endregion

                        #region WET SAND
                        if (wetsandCbox.Checked)
                        {
                            if (blockID == 119)
                            {
                                ThreadPool.QueueUserWorkItem(delegate
                                {
                                    if (Variables.blockIDs[layer, Variables.ax, Variables.ay + 1] == 138)
                                    {
                                        Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay, 0 });
                                        Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay + 1, 137 });
                                        if (Variables.blockIDs[layer, Variables.ax, Variables.ay - 1] == 119)
                                        {
                                            Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay - 1, 0 });
                                            Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay, 119 });
                                        }
                                    }
                                    else if (Variables.blockIDs[layer, Variables.ax, Variables.ay + 1] == 137)
                                    {
                                        Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay, 0 });
                                        Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay + 1, 139 });
                                        if (Variables.blockIDs[layer, Variables.ax, Variables.ay - 1] == 119)
                                        {
                                            Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay - 1, 0 });
                                            Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay, 119 });
                                        }
                                    }
                                    else if (Variables.blockIDs[layer, Variables.ax, Variables.ay + 1] == 139)
                                    {
                                        Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay, 0 });
                                        Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay + 1, 140 });
                                        if (Variables.blockIDs[layer, Variables.ax, Variables.ay - 1] == 119)
                                        {
                                            Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay - 1, 0 });
                                            Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay, 119 });
                                        }
                                    }
                                    else if (Variables.blockIDs[layer, Variables.ax, Variables.ay + 1] == 140)
                                    {
                                        Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay, 0 });
                                        Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay + 1, 141 });
                                        if (Variables.blockIDs[layer, Variables.ax, Variables.ay - 1] == 119)
                                        {
                                            Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay - 1, 0 });
                                            Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay, 119 });
                                        }
                                    }
                                    else if (Variables.blockIDs[layer, Variables.ax, Variables.ay + 1] == 141)
                                    {
                                        Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay, 0 });
                                        Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay + 1, 142 });
                                    }
                                    else if (Variables.blockIDs[layer, Variables.ax, Variables.ay + 1] == 142)
                                    {
                                        Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay, 0 });
                                        if (Variables.blockIDs[layer, Variables.ax, Variables.ay - 1] == 119)
                                        {
                                            Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay - 1, 0 });
                                            Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay, 119 });
                                        }
                                    }
                                });
                            }
                        }
                        #endregion
                        #region TNT  
                        if (tntallowd.Checked)
                        {
                            if (blockID == 12)
                            {
                                ThreadPool.QueueUserWorkItem(delegate
                                {
                                    if (Variables.blockIDs[layer, Variables.ax, Variables.ay + 1] == 0)
                                    {
                                        Thread.Sleep(400);
                                        Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                                        Thread.Sleep(250);
                                        Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay + 1, 12 });
                                    }
                                    else
                                    {
                                        //blow up
                                        #region Red
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 1, Variables.ay, 613 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 1, Variables.ay, 613 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay + 1, 613 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay - 1, 613 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 1, Variables.ay - 1, 613 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 1, Variables.ay - 1, 613 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 1, Variables.ay + 1, 613 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 1, Variables.ay + 1, 613 }); Thread.Sleep(175);
                                        #endregion
                                        #region Yellow
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 2, Variables.ay, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 2, Variables.ay, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 2, Variables.ay - 1, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 2, Variables.ay + 1, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 2, Variables.ay + 1, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 2, Variables.ay - 1, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay + 2, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay - 2, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 2, Variables.ay - 2, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 2, Variables.ay - 2, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 2, Variables.ay + 2, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 2, Variables.ay + 2, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 1, Variables.ay + 2, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 1, Variables.ay + 2, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 1, Variables.ay - 2, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 1, Variables.ay - 2, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 2, Variables.ay - 2, 614 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 2, Variables.ay - 2, 614 }); Thread.Sleep(175);
                                        #endregion
                                        Thread.Sleep(3000); Thread.Sleep(175); // wait 3s
                                        #region Clear Explosion
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay, 0 }); Thread.Sleep(175);
                                        #region Clear Red
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 1, Variables.ay, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 1, Variables.ay, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay + 1, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay - 1, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 1, Variables.ay - 1, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 1, Variables.ay - 1, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 1, Variables.ay + 1, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 1, Variables.ay + 1, 0 }); Thread.Sleep(175);
                                        #endregion
                                        #region Clear Yellow
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 2, Variables.ay, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 2, Variables.ay, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 2, Variables.ay - 1, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 2, Variables.ay + 1, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 2, Variables.ay + 1, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 2, Variables.ay - 1, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay + 2, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay - 2, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 2, Variables.ay - 2, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 2, Variables.ay - 2, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 2, Variables.ay + 2, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 2, Variables.ay + 2, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 1, Variables.ay + 2, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 1, Variables.ay + 2, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 1, Variables.ay - 2, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 1, Variables.ay - 2, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 2, Variables.ay - 2, 0 }); Thread.Sleep(175);
                                        Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 2, Variables.ay - 2, 0 }); Thread.Sleep(175);
                                        #endregion
                                        #endregion
                                    }
                                });
                            }
                        }
                        #endregion

                        if (paintbrushauto.Checked)
                        {
                            int idof = Convert.ToInt32(idofit.Text);
                            if (blockID == idof)
                            {
                                for (int x = 0; x < Variables.worldWidth; x++)
                                {
                                    for (int y = 0; y < Variables.worldHeight; y++)
                                    {
                                        Variables.con.Send(Variables.worldKey, new object[] { flayer, x, y, idof });
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
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 10 });
                            }
                            if (blockID == 10)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 11 });
                            }
                            if (blockID == 11)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 12 });
                            }
                            if (blockID == 12)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 1018 });
                            }
                            if (blockID == 1018)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 13 });
                            }
                            if (blockID == 13)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 14 });
                            }
                            if (blockID == 14)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 15 });
                            }
                            if (blockID == 15)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 182 });
                            }
                            if (blockID == 182)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                            }
                        }
                        else if (frbs.Checked)
                        {
                            if (blockID == 9)
                            {
                                Thread.Sleep(7);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 10 });
                            }
                            if (blockID == 10)
                            {
                                Thread.Sleep(7);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 11 });
                            }
                            if (blockID == 11)
                            {
                                Thread.Sleep(7);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 12 });
                            }
                            if (blockID == 12)
                            {
                                Thread.Sleep(7);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 13 });
                            }
                            if (blockID == 13)
                            {
                                Thread.Sleep(7);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 14 });
                            }
                            if (blockID == 14)
                            {
                                Thread.Sleep(7);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 15 });
                            }
                            if (blockID == 15)
                            {
                                Thread.Sleep(7);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 182 });
                            }
                            if (blockID == 182)
                            {
                                Thread.Sleep(7);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                            }
                        }
                        #endregion
                        #region rainbow mineral snake
                        if (mineralRAINBOW.Checked)
                        {
                            if (blockID == 70)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 71 });
                            }
                            if (blockID == 71)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 72 });
                            }
                            if (blockID == 72)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 73 });
                            }
                            if (blockID == 73)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 74 });
                            }
                            if (blockID == 74)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 75 });
                            }
                            if (blockID == 75)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 76 });
                            }
                            if (blockID == 76)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                            }
                        }
                        else if (mineralRAINBOWFAST.Checked)
                        {
                            if (blockID == 70)
                            {
                                Thread.Sleep(7);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 71 });
                            }
                            if (blockID == 71)
                            {
                                Thread.Sleep(7);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 72 });
                            }
                            if (blockID == 72)
                            {
                                Thread.Sleep(7);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 73 });
                            }
                            if (blockID == 73)
                            {
                                Thread.Sleep(7);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 74 });
                            }
                            if (blockID == 74)
                            {
                                Thread.Sleep(7);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 75 });
                            }
                            if (blockID == 75)
                            {
                                Thread.Sleep(7);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 76 });
                            }
                            if (blockID == 76)
                            {
                                Thread.Sleep(7);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                            }
                        }
                        #endregion
                        if (nbs.Checked)
                        {
                            if (blockID == 14)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 12 });
                            }
                            if (blockID == 12)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                            }
                        }
                        else if (fnbs.Checked)
                        {
                            if (blockID == 14)
                            {
                                Thread.Sleep(250);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 12 });
                            }
                            if (blockID == 12)
                            {
                                Thread.Sleep(250);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                            }
                        }
                        if (fax.Checked)
                        {
                            if (blockID == 74)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 70 });
                            }
                            if (blockID == 70)
                            {
                                Thread.Sleep(thedelay);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                            }
                        }
                        else if (faxII.Checked)
                        {
                            if (blockID == 74)
                            {
                                Thread.Sleep(250);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 70 });
                            }
                            if (blockID == 70)
                            {
                                Thread.Sleep(250);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                            }
                        }

                        if (lavadrawer.Checked)
                        {
                            if (blockID == 119 && !Variables.botIsPlacing)
                            {
                                int BGcolor = 574;
                                if (waterchoice2.Checked)
                                {
                                    BGcolor = 530;
                                }
                                for (int i = 0; i < Convert.ToInt32(lavaP.Value); i++)
                                {
                                    Variables.botIsPlacing = true;
                                    Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + i, Variables.ay, 0 });
                                    Thread.Sleep(15);
                                    Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + i, Variables.ay, 119 });
                                    Thread.Sleep(15);
                                    Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + i, Variables.ay, BGcolor });
                                    Thread.Sleep(15);
                                }
                                Variables.botIsPlacing = false;
                                #region commented
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 2, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 3, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 3, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 4, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 4, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 5, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 5, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 6, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 6, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 7, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 7, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 8, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 8, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 9, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 9, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 10, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 10, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 11, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 11, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 12, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 12, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 13, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 13, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 14, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 14, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 15, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 15, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 16, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 16, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 17, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 17, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 18, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 18, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 19, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 19, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 20, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 20, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 21, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 21, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 22, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 22, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 23, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 23, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 24, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 24, Variables.ay, BGcolor });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 25, Variables.ay, 119 });
                                //Thread.Sleep(250);
                                //Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 25, Variables.ay, BGcolor });
                                #endregion
                            }
                        }
                        if (autobuild1.Checked)
                        {
                            #region smiley border
                            if (blockID == 500)
                            {
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 0 });
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax, Variables.ay, 0 });
                                Thread.Sleep(575);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 1, Variables.ay, 29 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 2, Variables.ay, 540 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 3, Variables.ay, 540 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 4, Variables.ay, 29 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 5, Variables.ay, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay + 1, 46 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 5, Variables.ay + 1, 46 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 1, Variables.ay + 1, 540 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 6, Variables.ay + 1, 540 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 2, Variables.ay + 1, 800 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 7, Variables.ay + 1, 800 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 2, Variables.ay + 2, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 7, Variables.ay + 2, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 3, Variables.ay + 2, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 8, Variables.ay + 2, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 3, Variables.ay + 3, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 8, Variables.ay + 3, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 4, Variables.ay + 3, 800 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 9, Variables.ay + 3, 800 });
                                Thread.Sleep(12);
                                // Separation //////////////////////////////////
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 4, Variables.ay + 4, 540 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 9, Variables.ay + 4, 540 });
                                Thread.Sleep(12);
                                ////////////////////////////////////////////////
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 4, Variables.ay + 5, 46 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 9, Variables.ay + 5, 46 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 5, Variables.ay + 5, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 10, Variables.ay + 5, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 5, Variables.ay + 6, 29 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 10, Variables.ay + 6, 29 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 5, Variables.ay + 7, 540 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 10, Variables.ay + 7, 540 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 5, Variables.ay + 8, 540 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 10, Variables.ay + 8, 540 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 5, Variables.ay + 9, 29 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 10, Variables.ay + 9, 29 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 5, Variables.ay + 10, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 10, Variables.ay + 10, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 4, Variables.ay + 10, 46 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 9, Variables.ay + 10, 46 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 4, Variables.ay + 11, 540 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 9, Variables.ay + 11, 540 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 4, Variables.ay + 12, 800 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 9, Variables.ay + 12, 800 });
                                Thread.Sleep(12);
                                //////////////////////////////////////////////////////////////////
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 3, Variables.ay + 12, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 8, Variables.ay + 12, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 3, Variables.ay + 13, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 8, Variables.ay + 13, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 2, Variables.ay + 13, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 7, Variables.ay + 13, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax - 2, Variables.ay + 14, 800 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 7, Variables.ay + 14, 800 });
                                //////////////////////////////////////////////////////////////////
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax - 1, Variables.ay + 14, 540 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 6, Variables.ay + 14, 540 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay + 14, 46 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 5, Variables.ay + 14, 46 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax, Variables.ay + 15, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 5, Variables.ay + 15, 42 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 1, Variables.ay + 15, 29 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.ax + 4, Variables.ay + 15, 29 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 2, Variables.ay + 15, 540 });
                                Thread.Sleep(12);
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.ax + 3, Variables.ay + 15, 540 });
                                #endregion
                            }
                        }
                        #endregion
                    }

                    return;
                case "m":
                    if (Variables.botFullyConnected)
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
                            if (Variables.names.ContainsValue(stalkMover.Text))
                            {
                                if (stalkMover.Text.Contains(Variables.names[m.GetInt(0)]))
                                {
                                    Variables.con.Send("m", m.GetDouble(1), m.GetDouble(2), m.GetDouble(3), m.GetDouble(4), m.GetDouble(5), m.GetDouble(6), m.GetDouble(7), m.GetDouble(8), m.GetInt(9), m.GetBoolean(10), m.GetBoolean(11));
                                }
                            }
                        }
                    }



                    return;
                case "god":
                    return;
                case "say":
                    if (Variables.botFullyConnected)
                    {
                        Variables.str = m.GetString(1);

                        if (m.GetInt(0) != Variables.botid)
                        {
                            if (Variables.names.ContainsKey(m.GetInt(0)))
                            {
                                chatbox.Items.Add(Variables.names[m.GetInt(0)] + ": " + m.GetString(1));


                                if (Variables.str.StartsWith("!autokick "))
                                {
                                    string[] option = Variables.str.Split(' ');
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        if (noRespawn.Checked)
                                        {
                                            if (warningGiver.Checked)
                                            {
                                                int warnumber = Convert.ToInt32(textBox1.Text);
                                                if (Variables.player[m.GetInt(0)].warnings > warnumber)
                                                {
                                                    if (bwl.Checked)
                                                    {
                                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + " Warning limit reached! You are getting banned.");
                                                        Thread.Sleep(5);
                                                        Variables.con.Send("say", "/ban " + Variables.names[m.GetInt(0)]);
                                                    }
                                                    else
                                                    {
                                                        Variables.con.Send("say", "/kick " + Variables.names[m.GetInt(0)] + " Warning limit reached!");
                                                    }
                                                }
                                                else
                                                {
                                                    Variables.player[m.GetInt(0)].warnings = Variables.player[m.GetInt(0)].warnings + 1;
                                                    Thread.Sleep(250);
                                                    Variables.con.Send("say", Variables.names[m.GetInt(0)].ToUpper() + ": Please don't use /respawn. Warning " + Variables.player[m.GetInt(0)].warnings + " out of " + textBox1.Text + ".");
                                                }
                                            }
                                            else
                                            {
                                                Variables.con.Send("say", "/kick " + Variables.names[m.GetInt(0)] + " Please don't use /respawn command!");
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
                                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  AutoKick is already turned ON.");
                                                    }
                                                    else
                                                    {
                                                        autokickvalue.Checked = true;
                                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  AutoKick turned ON.");
                                                        #region BOT LOG
                                                        DefineLogZones();
                                                        Thread.Sleep(250);
                                                        log1.Text = "1. " + Variables.names[m.GetInt(0)].ToUpper() + " enabled autokick.";
                                                        #endregion
                                                    }
                                                }
                                                else
                                                {
                                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  AutoKick isn't allowed by whoever is using this bot.");
                                                }
                                            }
                                            else if (option[1] == "false" || option[1] == "off" || option[1] == "no")
                                            {
                                                if (autokickallowd.Checked)
                                                {
                                                    if (autokickvalue.Checked)
                                                    {
                                                        autokickvalue.Checked = false;
                                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  AutoKick turned OFF.");
                                                        #region BOT LOG
                                                        DefineLogZones();
                                                        Thread.Sleep(250);
                                                        log1.Text = "1. " + Variables.names[m.GetInt(0)].ToUpper() + " " + Voids.GetLangFile(Variables.CurrentLang, 102);
                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  AutoKick is already turned OFF.");
                                                    }
                                                }
                                                else
                                                {
                                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  AutoKick isn't allowed by whoever is using this bot.");
                                                }
                                            }
                                            else
                                            {
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Option doesn't exist or option misspellen.");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You are not an admin in the bot! D:<");
                                    }
                                }
                                else if (Variables.str.StartsWith("!kick "))
                                {
                                    if (kickCbox.Checked)
                                    {
                                        if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                        {
                                            string cmdPar = Variables.str.Substring(7);
                                            if (cmdPar.Length > 1)
                                            {
                                                string[] aaa = cmdPar.Split(' ');
                                                string[] fullSource = Variables.str.Split(' ');
                                                string kicking = fullSource[1];

                                                if (Variables.names.ContainsValue(kicking.ToLower()) || Variables.names.ContainsValue(kicking.ToUpper()))
                                                {
                                                    if (!Admins.Items.Contains(kicking.ToLower()) && !Admins.Items.Contains(kicking.ToUpper()))
                                                    {
                                                        string sample = cmdPar.Replace("!kick ", "");
                                                        string reasson = "";

                                                        reasson = sample.Substring(((kicking.Length - 1) + 1) + 1);

                                                        if (reasson == "" || reasson == " ")
                                                        {
                                                            reasson = "The bot admin " + Variables.names[m.GetInt(0)] + " has kicked you.";
                                                        }

                                                        Variables.con.Send("say", "/kick " + kicking + " " + reasson);
                                                        #region BOT LOG
                                                        DefineLogZones();
                                                        Thread.Sleep(250);
                                                        log1.Text = "1. " + Variables.names[m.GetInt(0)].ToUpper() + " kicked " + kicking + ".";
                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You can't kick admins.");
                                                    }
                                                }
                                                else
                                                {
                                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Unknown username.");
                                                }
                                            }
                                            else
                                            {
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Command not used correctly.");
                                            }
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You are not an admin in the bot! D:<");
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Command Disabled. >:)");
                                    }
                                }
                                else if (Variables.str.StartsWith("!revert "))
                                {
                                    if (revertCboxLOL.Checked)
                                    {
                                        if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                        {
                                            string[] me = Variables.str.Split(' ');
                                            string revertin = me[1];

                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "[R42Bot++] Reverting " + revertin);
                                            ThreadPool.QueueUserWorkItem(delegate
                                            {
                                                try
                                                {
                                                    for (int x = 0; x < Variables.worldWidth; x++)
                                                    {
                                                        for (int y = 0; y < Variables.worldHeight; y++)
                                                        {
                                                            if (Variables.blockPLACERs[0, x, y] == revertin)
                                                            {
                                                                Variables.con.Send(Variables.worldKey, 0, x, y, 0);
                                                            }
                                                            else if (Variables.blockPLACERs[1, x, y] == revertin)
                                                            {
                                                                Variables.con.Send(Variables.worldKey, 1, x, y, 0);
                                                            }
                                                            Thread.Sleep(75);
                                                        }
                                                        Thread.Sleep(75);
                                                    }
                                                }
                                                catch (PlayerIOError io)
                                                {
                                                    Console.WriteLine("");
                                                    Console.WriteLine("=====================");
                                                    Console.WriteLine(io.Message);
                                                    Console.WriteLine("=====================");
                                                    Console.WriteLine("");
                                                }
                                            });
                                            Thread.Sleep(250);
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Done reverting [" + revertin + "]!");
                                            #region BOT LOG
                                            DefineLogZones();
                                            Thread.Sleep(250);
                                            log1.Text = "1. " + Variables.names[m.GetInt(0)].ToUpper() + " reverted " + Voids.Shortest(revertin).ToUpper() + "'s work.";
                                            #endregion

                                            //for (int test = 0; test < block.Length; test++)
                                            //{
                                            //    if (Variables.blockIDs[layer, test].placer == revertin)
                                            //    {

                                            //    }
                                            //}
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You are not an admin in the bot! D:<");
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Command Disabled. >:)");
                                    }
                                }
                                else if (Variables.str.StartsWith("!snakespeed "))
                                {
                                    if (revertCboxLOL.Checked)
                                    {
                                        if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                        {
                                            string[] me = Variables.str.Split(' ');
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
                                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Snake speed " + valu + " is not accepted.");
                                                    Thread.Sleep(200);
                                                }
                                                else
                                                {
                                                    decimal value = Convert.ToDecimal(valu);
                                                    numericUpDown1.Value = value;
                                                    #region BOT LOG
                                                    DefineLogZones();
                                                    Thread.Sleep(250);
                                                    log1.Text = "1. " + Variables.names[m.GetInt(0)].ToUpper() + " changed snake speed to " + valu + "ms.";
                                                    #endregion
                                                    Thread.Sleep(200);
                                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Snake speed changed to " + valu);
                                                    Thread.Sleep(200);
                                                }
                                            }
                                            else
                                            {
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Value was not a number.");
                                            }
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You are not an admin in the bot! D:<");
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Command Disabled. >:)");
                                    }
                                }
                                else if (Variables.str.StartsWith("!name "))
                                {
                                    if (krockhateseers.Checked)
                                    {
                                        if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                        {
                                            string NewName = Variables.str.Substring(6);
                                            Variables.con.Send("name", NewName);
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", " You are not an admin in the bot! D:<");
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + " " + Voids.GetLangFile(Variables.CurrentLang, 102));
                                    }
                                }
                                else if (Variables.str.StartsWith("!admins"))
                                {
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        string admins = "";

                                        foreach (string namez in Admins.Items)
                                        {
                                            admins += namez.ToUpper() + ",";
                                        }

                                        if (admins == "")
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + " [R42Bot++] Bot admins:");
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + " No one.");
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + " [R42Bot++] Bot admins:");
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + " " + admins);
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You can't see bot admins cause you are not an admin!");
                                    }
                                }
                                else if (Variables.str.StartsWith("!microphone "))
                                {
                                    string begin = Variables.str.Substring(0, 12);
                                    string message = Variables.str.Replace(begin, "");
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        Variables.con.Send("say", "[R42Bot++] Listen Everybody!");
                                        Thread.Sleep(1000);
                                        Variables.con.Send("say", "[R42Bot++] Please Listen!!!");
                                        Thread.Sleep(1000);
                                        Variables.con.Send("say", "[R42Bot++] " + message);
                                        Thread.Sleep(1000);
                                        Variables.con.Send("say", "[R42Bot++] " + message);
                                        Thread.Sleep(575);
                                        Variables.con.Send("say", "[R42Bot++] " + message);
                                        Thread.Sleep(575);
                                        Variables.con.Send("say", "[R42Bot++] " + message);
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "[R42Bot++] " + Variables.names[m.GetInt(0)] + ": You are not an admin in the bot!");
                                    }
                                }
                                else if (Variables.str.StartsWith("!affect ")) // must be StartsWith(" "), so, if the commands starts like that... The blank space is for username! (if you dont want it just remove it, and it will be !affectexample (example as user)
                                {
                                    string[] username = Variables.str.Split(' '); // usernameGetter, if you removed blank space, it must be Variables.str.Split('');
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        // If user that said this is an admin...
                                        if (Variables.names.ContainsValue(username[1])) //username[1] is the string[] we made.
                                        {
                                            // the user will be affected.
                                            Variables.con.Send("say", "/giveedit " + username[1]);
                                            Thread.Sleep(100); // delay 100ms
                                            Variables.con.Send("say", "/removeedit " + username[1]);
                                            Thread.Sleep(100);
                                            Variables.con.Send("say", "/togglepotions off");
                                            Thread.Sleep(100);
                                            Variables.con.Send("say", "/togglepotions on");
                                            Thread.Sleep(100);
                                            Variables.con.Send("say", "/respawn " + username[1]);
                                            Console.WriteLine(username[1] + " has been affected by admin " + Variables.names[m.GetInt(0)] + "."); // Console.WriteLine writes something to Output, actually, you can make an listbox that tells everything admins did!
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", Variables.names[m.GetInt(0)] + ": You can't affect '" + username[1] + "' cause it isn't an valid username or this user isn't in this world.");
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", Variables.names[m.GetInt(0)] + ": You little troller! You can't affect people if you aren't an admin in the bot! >:O");
                                    }
                                }
                                else if (Variables.str.StartsWith("!stalk "))
                                {
                                    string[] userinuse = Variables.str.Split(' ');
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        if (alstalking.Checked)
                                        {
                                            if (Variables.names.ContainsValue(userinuse[1]))
                                            {
                                                if (stalkMover.Text != userinuse[1])
                                                {
                                                    stalkMover.Text = "";
                                                    #region BOT LOG
                                                    DefineLogZones();
                                                    Thread.Sleep(250);
                                                    log1.Text = "1. " + Variables.names[m.GetInt(0)].ToUpper() + " made bot stalk " + userinuse[1].ToUpper() + ".";
                                                    #endregion
                                                    Thread.Sleep(250);
                                                    stalkMover.Text = userinuse[1];
                                                }
                                                else
                                                {
                                                    if (pmresult.Checked)
                                                    {
                                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + " [R42Bot++] " + Variables.names[m.GetInt(0)].ToUpper() + ": " + userinuse[1] + " is already in the stalking list!");
                                                    }
                                                    else
                                                    {
                                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  " + userinuse[1] + " is already in the stalking list!");
                                                    }
                                                    // if is already in Stalking's List.
                                                }
                                            }
                                            else
                                            {

                                                if (pmresult.Checked)
                                                {
                                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + " [R42Bot++] " + Variables.names[m.GetInt(0)].ToUpper() + ": " + userinuse[1] + " is not in this world!");
                                                }
                                                else
                                                {
                                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  " + userinuse[1] + " is not in this world!");
                                                }
                                                // if not in world
                                            }
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + " " + Voids.GetLangFile(Variables.CurrentLang, 102));
                                        }
                                    }
                                    else
                                    {
                                        if (userinuse[1] == ".realwizard42." || userinuse[1] == ".REALWIZARD42.")
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  " + userinuse[1] + " is the bot and you can't make bot stalk people since you are not an admin in the bot!");
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  " + userinuse[1] + " can't be stalked because you are not an admin!");
                                        }
                                        // if not an admin
                                    }
                                }
                                else if (Variables.str.StartsWith("!unstalk "))
                                {
                                    string[] userinuse = Variables.str.Split(' ');
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        if (Variables.names.ContainsValue(userinuse[1]))
                                        {
                                            if (stalkMover.Text == userinuse[1])
                                            {
                                                stalkMover.Text = "";
                                            }
                                            else
                                            {
                                                if (pmresult.Checked)
                                                {
                                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + " [R42Bot++] " + Variables.names[m.GetInt(0)].ToUpper() + ": " + userinuse[1] + " is not in the stalking list!");
                                                }
                                                else
                                                {
                                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  " + userinuse[1] + " is not in the stalking list!");
                                                }
                                                // if isn't in Stalking's List.
                                            }
                                        }
                                        else
                                        {
                                            if (pmresult.Checked)
                                            {
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + " [R42Bot++] " + Variables.names[m.GetInt(0)].ToUpper() + ": " + userinuse[1] + " is not in this world!");
                                            }
                                            else
                                            {
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  " + userinuse[1] + " is not in this world!");
                                            }
                                            // if not in world
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  " + userinuse[1] + " can't be removed from stalking, because you aren't an admin!");
                                        // if not an admin
                                    }
                                }
                                else if (Variables.str.StartsWith("!mywins"))
                                {
                                    if (winsystem1.Checked == true)
                                    {
                                        if (enus.Checked == true && ptbr.Checked == false)
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You won " + Variables.player[m.GetInt(0)].wins + " times.");
                                        }
                                        else if (ptbr.Checked == true && enus.Checked == false)
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Voçê ganhou " + Variables.player[m.GetInt(0)].wins + " vezes.");
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + " " + Voids.GetLangFile(Variables.CurrentLang, 102));
                                    }
                                }
                                else if (Variables.str.StartsWith("!say "))
                                {
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        string beginz = Variables.str.Substring(0, 5);
                                        string endz = Variables.str.Replace(beginz, "");
                                        Variables.con.Send("say", "[R42Bot++] " + endz);
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You are not an admin in the bot!");
                                    }
                                }
                                else if (Variables.str.StartsWith("!amiadmin"))
                                {
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Yes, you are an admin in the bot.");
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  No, you aren't an admin in the bot.");
                                    }
                                }
                                #region is [player]...
                                else if (Variables.str.StartsWith("!is "))
                                {
                                    string[] userinuse = Variables.str.Split(' ');
                                    if (Variables.str.StartsWith("!is " + userinuse[1] + " admin"))
                                    {
                                        if (Admins.Items.Contains(userinuse[1]))
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Yes, " + userinuse[1] + " is an admin in the bot.");
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  No, " + userinuse[1] + " is not an admin in the bot.");
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Unknown question or misspellen question.");
                                    }
                                }
                                #endregion
                                else if (Variables.str.StartsWith("!botlog"))
                                {
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        Variables.con.Send("say", "===-===-===-===");
                                        Thread.Sleep(575);
                                        Variables.con.Send("say", "==---BOT_LOG---==");
                                        Thread.Sleep(575);
                                        Variables.con.Send("say", "===-===-===-===");
                                        Thread.Sleep(575);
                                        if (log1.Text != "")
                                        {
                                            Variables.con.Send("say", log1.Text);
                                            Thread.Sleep(575);
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "1. Empty");
                                            Thread.Sleep(575);
                                        }
                                        if (log2.Text != "")
                                        {
                                            Variables.con.Send("say", log2.Text);
                                            Thread.Sleep(575);
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "2. Empty");
                                            Thread.Sleep(575);
                                        }
                                        if (log3.Text != "")
                                        {
                                            Variables.con.Send("say", log3.Text);
                                            Thread.Sleep(575);
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "3. Empty");
                                            Thread.Sleep(575);
                                        }
                                        if (log4.Text != "")
                                        {
                                            Variables.con.Send("say", log4.Text);
                                            Thread.Sleep(575);
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "4. Empty");
                                            Thread.Sleep(575);
                                        }
                                        if (log5.Text != "")
                                        {
                                            Variables.con.Send("say", log5.Text);
                                            Thread.Sleep(575);
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "5. Empty");
                                            Thread.Sleep(575);
                                        }
                                        Variables.con.Send("say", "===-===-===-===");
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  To view bot log you must be an admin in the bot!");
                                    }
                                }

                                #region POLL COMMANDS

                                else if (Variables.str.StartsWith("!vote "))
                                {
                                    if (votersList.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You've already voted!");
                                    }
                                    else if (pollname.Text == "")
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  No poll is Variables.currently in progress!");
                                    }
                                    else
                                    {
                                        if (Variables.names[m.GetInt(0)] == pollstartername.Text)
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You can't vote because you made the poll!");
                                        }
                                        else
                                        {
                                            string[] voted = Variables.str.Split(' ');
                                            if (voted[1] == choice1.Text.ToLower() || voted[1] == choice1.Text)
                                            {
                                                votersList.Items.Add(Variables.names[m.GetInt(0)]);
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You voted " + choice1.Text + ".");
                                                int votes1 = Convert.ToInt32(vot1.Text);
                                                votes1 = votes1 + 1;
                                                Thread.Sleep(250);
                                                string nuvots = Convert.ToString(votes1);
                                                vot1.Text = nuvots;
                                            }
                                            else if (voted[1] == choice2.Text.ToLower() || voted[1] == choice2.Text)
                                            {
                                                votersList.Items.Add(Variables.names[m.GetInt(0)]);
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You voted " + choice2.Text + ".");
                                                int votes2 = Convert.ToInt32(vot2.Text);
                                                votes2 = votes2 + 1;
                                                Thread.Sleep(250);
                                                string nuvots = Convert.ToString(votes2);
                                                vot2.Text = nuvots;
                                            }
                                            else if (voted[1] == choice3.Text.ToLower() || voted[1] == choice3.Text)
                                            {
                                                votersList.Items.Add(Variables.names[m.GetInt(0)]);
                                                if (choice3.Visible == true)
                                                {
                                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You voted " + choice3.Text + ".");
                                                    int votes3 = Convert.ToInt32(vot3.Text);
                                                    votes3 = votes3 + 1;
                                                    Thread.Sleep(250);
                                                    string nuvots = Convert.ToString(votes3);
                                                    vot3.Text = nuvots;
                                                }
                                                else
                                                {
                                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Tirth option has been removed by whoever is using this bot.");
                                                }
                                            }
                                            else
                                            {
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Unknown Option for voting polls.");
                                            }
                                        }
                                    }
                                }
                                else if (Variables.str.StartsWith("!pc1 "))
                                {
                                    string[] choiced = Variables.str.Split(' ');
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        if (pollname.Text == "")
                                        {
                                            if (choice1.Text == choiced[1])
                                            {
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  The first choice is the same as the new one!");
                                            }
                                            else
                                            {
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Choice1 changed to " + choiced[1] + ".");
                                                choice1.Text = choiced[1];
                                            }
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  There is an poll in progress!");
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You aren't an admin in the bot so you can't change the bot's poll choices!");
                                    }
                                }
                                else if (Variables.str.StartsWith("!pc2 "))
                                {
                                    string[] choiced = Variables.str.Split(' ');
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        if (pollname.Text == "")
                                        {
                                            if (choice2.Text == choiced[1])
                                            {
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  The second choice is the same as the new one!");
                                            }
                                            else
                                            {
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Choice2 changed to " + choiced[1] + ".");
                                                choice2.Text = choiced[1];
                                            }
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  There is a poll in progress!");
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You aren't an admin in the bot so you can't change the bot's poll choices!");
                                    }
                                }
                                else if (Variables.str.StartsWith("!pc3 "))
                                {
                                    string[] choiced = Variables.str.Split(' ');
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        if (pollname.Text == "")
                                        {
                                            if (choice3.Text == choiced[1])
                                            {
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  The tirth choice is the same as the new one!");
                                            }
                                            else
                                            {
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Choice3 changed to " + choiced[1] + ".");
                                                choice3.Text = choiced[1];
                                            }
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  There is a poll in progress!");
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You aren't an admin in the bot so you can't change the bot's poll choices!");
                                    }
                                }
                                else if (Variables.str.StartsWith("!endpoll"))
                                {
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        if (pollname.Text == "")
                                        {
                                            Variables.con.Send("say", "[R42Bot++] " + Voids.Shortest(Variables.names[m.GetInt(0)]).ToUpper() + ": There is no polls at progress.");
                                        }
                                        else
                                        {
                                            if (vot3.Visible == false)
                                            {
                                                Variables.con.Send("say", "[R42Bot++] " + Voids.Shortest(Variables.names[m.GetInt(0)]).ToUpper() + ": Poll '" + pollname.Text + "' stoped.");
                                                Thread.Sleep(575);
                                                Variables.con.Send("say", "[R42Bot++] " + Voids.Shortest(Variables.names[m.GetInt(0)]).ToUpper() + ": Results: " + choice1.Text + " - " + vot1.Text + " & " + choice2.Text + " - " + vot2.Text);
                                            }
                                            else
                                            {
                                                Variables.con.Send("say", "[R42Bot++] " + Voids.Shortest(Variables.names[m.GetInt(0)]).ToUpper() + ": Poll '" + pollname.Text + "' stoped.");
                                                Thread.Sleep(575);
                                                Variables.con.Send("say", "[R42Bot++] " + Voids.Shortest(Variables.names[m.GetInt(0)]).ToUpper() + ": Results: " + choice1.Text + " - " + vot1.Text + " , " + choice2.Text + " - " + vot2.Text + " & " + choice3.Text + " - " + vot3.Text);
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
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You aren't an admin in the bot so you can't end polls!");
                                    }
                                }
                                else if (Variables.str.StartsWith("!poll "))
                                {
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        if (pollname.Text == "")
                                        {
                                            if (vot3.Visible == false)
                                            {
                                                string beginz = Variables.str.Substring(0, 6);
                                                string endz = Variables.str.Replace(beginz, "");

                                                pollname.Text = endz;
                                                pollstartername.Text = Variables.names[m.GetInt(0)];
                                                Thread.Sleep(575);
                                                Variables.con.Send("say", "[R42Bot++] Everyone! Answer the following poll.");
                                                Thread.Sleep(575);
                                                Variables.con.Send("say", "[R42Bot++] '" + endz + "'.");
                                                Thread.Sleep(575);
                                                Variables.con.Send("say", "[R42Bot++]  Options: " + choice1.Text + " and " + choice2.Text + ". Do !vote [option] to vote!");
                                            }
                                            else
                                            {
                                                string beginz = Variables.str.Substring(0, 5);
                                                string endz = Variables.str.Replace(beginz, "");

                                                pollname.Text = endz;
                                                pollstartername.Text = Variables.names[m.GetInt(0)];
                                                Thread.Sleep(575);
                                                Variables.con.Send("say", "[R42Bot++] Everyone! Answer the following poll.");
                                                Thread.Sleep(575);
                                                Variables.con.Send("say", "[R42Bot++] '" + endz + "'.");
                                                Thread.Sleep(575);
                                                Variables.con.Send("say", "[R42Bot++]  Options: " + choice1.Text + " , " + choice2.Text + " and " + choice3.Text + ". Do !vote [option] to vote!");
                                            }
                                        }
                                        else
                                        {
                                            Thread.Sleep(575);
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  There is already an poll in progress!");
                                        }
                                    }
                                    else
                                    {
                                        Thread.Sleep(575);
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You can't start polls cause you aren't an admin!");
                                    }
                                }
                                else if (Variables.str.StartsWith("!pollhelp"))
                                {
                                    Variables.con.Send("say", "[R42Bot++] " + Voids.Shortest(Variables.names[m.GetInt(0)]).ToUpper() + ": !vote [option], !poll [name], !endpoll,");
                                    Thread.Sleep(575);
                                    Variables.con.Send("say", "[R42Bot++] " + Voids.Shortest(Variables.names[m.GetInt(0)]).ToUpper() + ": !pc1 [choice1], !pc2 [choice2] and !pc3 [choice3].");
                                }
                                #endregion
                                else if (Variables.str.StartsWith("!giveeditall"))
                                {
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        foreach (Player s in Variables.player)
                                        {
                                            Variables.con.Send("say", "/giveedit " + s.username);
                                            Thread.Sleep(400);
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  You can't give everyone edit cause you are not an admin in the bot!");
                                    }
                                }
                                else if (Variables.str.StartsWith("!removeditall"))
                                {
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        foreach (Player s in Variables.player)
                                        {
                                            Variables.con.Send("say", "/removeedit " + s.username);
                                            Thread.Sleep(400);
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "[R42Bot++] " + Voids.Shortest(Variables.names[m.GetInt(0)]).ToUpper() + ": You can't remove everyone's edit cause you are not an admin in the bot!");
                                    }
                                }
                                else if (Variables.str.StartsWith("!loadlevel"))
                                {
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        Variables.con.Send("say", "/loadlevel");
                                        Variables.con.Send("say", "[R42Bot++] " + Voids.Shortest(Variables.names[m.GetInt(0)]).ToUpper() + ": level loaded.");
                                        #region BOT LOG
                                        DefineLogZones();
                                        Thread.Sleep(250);
                                        log1.Text = "1. " + Variables.names[m.GetInt(0)].ToUpper() + " loaded the level.";
                                        #endregion
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  you are not an admin in the bot! D:<");
                                    }
                                }
                                else if (Variables.str.StartsWith("!download"))
                                {
                                    Variables.con.Send("say", "[R42Bot++] " + Voids.Shortest(Variables.names[m.GetInt(0)]).ToUpper() + ": http://realmaster42-projects.weebly.com/r42bot1.html");
                                }


                                else if (Variables.str.StartsWith("!listhelp"))
                                {
                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  !amiadmin, !botlog, !kick [player] [reasson], !save, !loadlevel, !clear, !evenhelp c:");
                                }
                                else if (Variables.str.StartsWith("!evenhelp"))
                                {
                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  !revert [player], !snakespeed [speed_in_ms]. HOORAY!");
                                }
                                else if (Variables.str.StartsWith("!giveedithelp"))
                                {
                                    Variables.con.Send("say", "[R42Bot++] " + Voids.Shortest(Variables.names[m.GetInt(0)]).ToUpper() + ": !removeditall, !giveeditall");
                                }
                                else if (Variables.str.StartsWith("!specialhelp"))
                                {
                                    Variables.con.Send("say", "[R42Bot++] " + Voids.Shortest(Variables.names[m.GetInt(0)]).ToUpper() + ": !giveedithelp");
                                }
                                else if (Variables.str.StartsWith("!more"))
                                {
                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  !specialhelp, !listhelp, ");
                                    Thread.Sleep(575);
                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  !say [msg], !affect [plr],");
                                    Thread.Sleep(575);
                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  !microphone [msg], !pollhelp, !is [plr] admin. c:");
                                }

                                #region HELP COMMAND
                                else if (Variables.str.StartsWith("!help")) // COMMANDYS COMMANDAS COMMANOS OMG
                                {
                                    ThreadPool.QueueUserWorkItem(delegate
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)].ToUpper() + " [R42Bot++] !more, !chelp, !download, !mywins, !halp,");
                                        Thread.Sleep(575);
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)].ToUpper() + " [R42Bot++] !version, !survival [plr], !creative [plr]. c:");
                                    });
                                }
                                else if (Variables.str.StartsWith("!halp"))
                                {
                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  !stalk [plr], !unstalk [plr]");
                                }
                                #endregion

                                else if (Variables.str.StartsWith("!chelp "))
                                {
                                    string[] command = Variables.str.Split(' ');
                                    #region commands
                                    if (command[1] == "chelp")
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  !chelp [command]. Makes you know how to use the command and how it works.");
                                    }
                                    else if (command[1] == "help")
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  !help. Makes you know the commands available in the bot.");
                                    }
                                    else if (command[1] == "more")
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  !more. Makes you know more commands available in the bot.");
                                    }
                                    else if (command[1] == "mywins")
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  !mywins. Makes you know your own wins, doesn't work if it is disabled!");
                                    }
                                    else if (command[1] == "affect")
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  !affect [plr]. This command makes players sometimes get kicked or lag.");
                                    }
                                    else if (command[1] == "amiadmin")
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  !amiadmin. Checks and tells you if you are an admin in the bot or not.");
                                    }
                                    else if (command[1] == "is admin")
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  !is [plr] admin. Checkes and tells you if the player is an admin in the bot or not.");
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  Command Mispellen or Unknown command. CommandHelp couldn't recognize that command!");
                                    }
                                    #endregion
                                }



                                else if (Variables.str.StartsWith("!version"))
                                {
                                    Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + " [R42Bot++] Bot version " + Version.version + " build " + Variables.BuildVersion.ToString());
                                }
                                else if (Variables.str.StartsWith("!creative "))
                                {
                                    string[] split = Variables.str.Split(' ');
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        if (scommand2.Checked)
                                        {
                                            if (split[1] == "nikooos" && Variables.names.ContainsValue("nikooooooos"))
                                            {
                                                Variables.con.Send("say", "/giveedit NIKOOOOOOOS");
                                                Thread.Sleep(200);
                                                Variables.con.Send("say", "[R42Bot++] NIKOOO(...)s is now in creative mode.");
                                                Thread.Sleep(200);
                                                Variables.con.Send("say", "/pm NIKOOOOOOOS [R42Bott+] hey... you are now in creative mode.");
                                                Thread.Sleep(200);
                                            }
                                            else if (Variables.names.ContainsValue(split[1]))
                                            {
                                                Variables.con.Send("say", "/giveedit " + split[1]);
                                                Thread.Sleep(200);
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  " + split[1].ToUpper() + " is now in creative mode.");
                                                Thread.Sleep(200);
                                                Variables.con.Send("say", "/pm " + split[1] + " [R42Bot++] hey... you are now in creative mode!");
                                                Thread.Sleep(200);
                                            }
                                            else
                                            {
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  " + split[1] + " isn't in this world or isn't an valid username.");
                                            }
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + " " + Voids.GetLangFile(Variables.CurrentLang, 102));
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  you are not an admin in the bot! D:<");
                                    }
                                }
                                else if (Variables.str.StartsWith("!survival "))
                                {
                                    string[] split = Variables.str.Split(' ');
                                    if (Admins.Items.Contains(Variables.names[m.GetInt(0)]))
                                    {
                                        if (scommand.Checked)
                                        {
                                            if (split[1] == "nikooooooos" && Variables.names.ContainsValue("nikooos"))
                                            {
                                                Variables.con.Send("say", "/removeedit NIKOOOOOOOS");
                                                Variables.con.Send("say", "[R42Bot++] NIKOOO(...)s is now in survival mode.");
                                                Variables.con.Send("say", "/pm NIKOOOOOOOS [R42Bott+] hey... you are now in survival mode.");
                                            }
                                            else if (Variables.names.ContainsValue(split[1]))
                                            {
                                                Variables.con.Send("say", "/removeedit " + split[1]);
                                                Variables.con.Send("say", "[R42Bot++] " + Variables.names[m.GetInt(0)].ToUpper() + " " + split[1].ToUpper() + " is now in survival mode.");
                                                Variables.con.Send("say", "/pm " + split[1] + " [R42Bot++] hey... you are now in survival mode!");
                                            }
                                            else
                                            {
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  " + split[1].ToUpper() + " isn't in this world or isn't an valid username.");
                                            }
                                        }
                                        else
                                        {
                                            Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + " " + Voids.GetLangFile(Variables.CurrentLang, 102));
                                        }
                                    }
                                    else
                                    {
                                        Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)] + "  you are not an admin in the bot! D:<");
                                    }
                                }


                                else
                                {
                                    if (Variables.str.StartsWith("!"))
                                    {
                                        if (Variables.names[m.GetInt(0)] == Variables.botName)
                                        {
                                            Console.WriteLine("Bot tried to spam. Nevermind that, bot >:O");
                                        }
                                        else
                                        {
                                            if (Variables.str.StartsWith("$"))
                                            {
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)].ToUpper() + " [R42Bot++] Wrong Prefix.");
                                            }
                                            else
                                            {
                                                Variables.con.Send("say", "/pm " + Variables.names[m.GetInt(0)].ToUpper() + " [R42Bot++] Command misspelen or Unknown Command.");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Variables.names.Add(m.GetInt(0), null);
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

                    return;

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
                            MessageBox.Show("Email, Password and WorldID must be fillen up!", "R42Bot++ v" + Version.version + " System");
                        }
                        else
                        {
                            MessageBox.Show("TokenID and WorldID must be fillen up!", "R42Bot++ v" + Version.version + " System");
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
                        Variables.con.Send("init");
                        Variables.con.Send("access");
                    }
                    catch (PlayerIOError error)
                    {
                        MessageBox.Show(error.Message);
                    }
                    if (Variables.botName != null)
                    {
                        Admins.Items.Add(Variables.botName.ToString());
                    }
                    connector.Text = "Disconnect";
                    button8.Enabled = true;
                    button9.Enabled = true;
                    grbutton.Enabled = true;
                }
            }
            else if (connector.Text == "Disconnect")
            {
                lavaP.Enabled = false;
                Variables.botFullyConnected = false;
                Variables.BlockPlacingTilVal1 = 1;
                Variables.BlockPlacingTilVal2 = 2;
                Variables.botIsPlacing = false;
                Variables.con.Disconnect();

                connector.Text = "Connect";
                button8.Enabled = false;
                button9.Enabled = false;
                grbutton.Enabled = false;
                Admins.Items.Remove(Variables.botName);
                MessageBox.Show("Disconnected.");
            }

        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (Admins.Items.Contains(removeText.Text))
            {
                Admins.Items.Remove(removeText.Text);
                removeText.Clear();
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
                if (Variables.names.ContainsValue(addText.Text))
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
                MessageBox.Show(addText.Text + " is already on the list...");
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

            if (File.Exists("R42Bot++LanguageFile.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Information));
                FileStream read = new FileStream("R42Bot++LanguageFile.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                Information info = (Information)xs.Deserialize(read);
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
            }
            Thread.Sleep(250);
            #region restriction commands
            #region /respawn
            noRespawn.Checked = (textBox2.Text == "r0") ? false : true;

            warningGiver.Checked = (textBox3.Text == "r0") ? false : true;

            bwl.Checked = (textBox4.Text == "r0") ? false : true;
            #endregion
            #endregion

            Variables.BuildVersion = R42Bot.Properties.Settings.Default.Build;
            Version.upgradedBuild = new System.Net.WebClient().DownloadString(Version.buildlink);
        }

        private void enus_CheckedChanged(object sender, EventArgs e)
        {
            if (enus.Checked == true)
            {
                MessageBox.Show("Language is now EU/US!", "R42Bot++ v" + Version.version + " System");
                Variables.CurrentLang = "enUS";
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
        }

        private void ptbr_CheckedChanged(object sender, EventArgs e)
        {
            if (ptbr.Checked == true)
            {
                MessageBox.Show("A Linguagem é agora português.", "R42Bot++ v" + Version.version + " System");
                Variables.CurrentLang = "ptbr";
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
            CallsSettings.WinSystem = true;
        }

        private void clearstalkering_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sucefully removed " + stalkMover.Text + " from stalking user.");
            Thread.Sleep(250);
            stalkMover.Text = "";
        }

        private void telllemgend_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Some Message Boxes will appear. Do you wanna see them?", "R42Bot++ v" + Version.version + " System", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                MessageBox.Show("*WorldData, Tells about WorldData system in the bot.", "R42Bot++ v" + Version.version + " System");
                MessageBox.Show("*R42Bot++ System, System of this bot.", "R42Bot++ v" + Version.version + " System");
                MessageBox.Show("Nothing, Default texts appearing.", "R42Bot++ v" + Version.version + " System");
            }
            else
            {
                MessageBox.Show("No legend could be shown, message box progress has been cancelled.", "R42Bot++ v" + Version.version + " System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            Variables.con.Send(Variables.worldKey + "f", 5);
        }

        private void lavadrawer_CheckedChanged(object sender, EventArgs e)
        {
            if (lavadrawer.Checked)
            {
                MessageBox.Show("Now placing water bricks will auto-update.", "R42Bot++ v" + Version.version + " System");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Variables.con.Send(Variables.worldKey + "f", 0);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine(Voids.GetLangFile(Variables.CurrentLang, 102));
        }

        private void autochangerface_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    Variables.con.Send(Variables.worldKey + "f", 0);
                    Thread.Sleep(500);
                    Variables.con.Send(Variables.worldKey + "f", 5);
                });
            }
        }

        private void autokick_Tick(object sender, EventArgs e)
        {
            if (autokickvalue.Checked)
            {
                foreach (Player kicking in Variables.player)
                {
                    if (!Admins.Items.Contains(kicking.username))
                    {
                        Variables.con.Send("say", "/kick " + kicking.username + " " + Voids.GetLangFile(Variables.CurrentLang, 101));
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
                Variables.con.Send("say", "/reset");
                if (autoresetmsg.Checked)
                {
                    Variables.con.Send("say", Voids.GetLangFile(Variables.CurrentLang, 100));
                }
            }
        }

        private void tntallowd_CheckedChanged(object sender, EventArgs e)
        {
            if (tntallowd.Checked)
            {
                MessageBox.Show("Now whenever someones places a red checker block it will FALL and destroy!", "R42Bot++ v" + Version.version + " System");
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
            if (Variables.names.ContainsValue(userpm.Text))
            {
                Variables.con.Send("say", userpm.Text + " " + textpm.Text);
                chatbox.Items.Add("*TO " + userpm.Text + ": " + textpm.Text);
            }
            else
            {
                MessageBox.Show("'" + userpm.Text + "' isn't in the connected world." + "R42Bot++ v" + Version.version + " System");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Thread.Sleep(575);
            Variables.con.Send("say", "[R42Bot++] " + saytext.Text);
            chatbox.Items.Add(">" + saytext.Text);
        }

        private void srandomizer_Click(object sender, EventArgs e)
        {
            Variables.con.Send(Variables.worldKey + "f", new Random().Next(0, 15));
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string[] nums = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

            if (!nums.Contains(e.KeyChar.ToString()))
            {
                if (e.KeyChar != 8)
                {
                    MessageBox.Show("You must enter a valid number.", "R42Bot++ System v" + Version.version);
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

        private void noRespawn_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Text = (noRespawn.Checked) ? "r1" : "r0";
        }

        private void warningGiver_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Text = (warningGiver.Checked) ? "r1" : "r0";
        }

        private void bwl_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.Text = (bwl.Checked) ? "r1" : "r0";
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
            textBox5.Text = (alstalking.Checked) ? "r1" : "r0";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                // /respawn
                Information info = new Information();
                info.Data5 = (textBox5.Text == "r0") ? "r0" : "r1";
                Class1.SaveData(info, "StalkInfo.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                if (Variables.CheckSnakeUpdate)
                {
                    Variables.currentChecked = "mineralRAINBOW";
                }
            }
            else
            {
                if (Variables.CheckSnakeUpdate)
                {
                    Variables.currentChecked = "";
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
                if (Variables.CheckSnakeUpdate)
                {
                    Variables.currentChecked = "mineralRAINBOWFAST";
                }
            }
            else
            {
                if (Variables.CheckSnakeUpdate)
                {
                    Variables.currentChecked = "";
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
                if (Variables.CheckSnakeUpdate)
                {
                    Variables.currentChecked = "fax";
                }
            }
            else
            {
                if (Variables.CheckSnakeUpdate)
                {
                    Variables.currentChecked = "";
                }
            }
        }

        private void allowSnakeSpecial_CheckedChanged(object sender, EventArgs e)
        {
            BlockPlacer.Enabled = (allowSnakeSpecial.Checked) ? true : false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Information info = new Information();
                ListBox.ObjectCollection items = Admins.Items;
                string[] Items1 = new string[] { };
                for (int i = 1; i < items.Count; i++)
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
                MessageBox.Show("Save File not found. (R42Bot++Admins.xml)", "R42Bot++ System v" + Version.version, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TrollCatcherBlockDelete_Tick(object sender, EventArgs e)
        {
            if (Variables.botFullyConnected)
            {
                if (unfairBlox.Checked)
                {
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        for (int i = 1; i < Variables.names.Count; i++)
                        {
                            Variables.player[i].BlocksPlacedInaSecond = 0;
                            Variables.player[i].AlreadyReedit = false;
                            Thread.Sleep(150);
                        }
                    });
                }
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
            if (Variables.botFullyConnected)
            {
                if (allowSnakeSpecial.Checked)
                {
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        int Delay = Convert.ToInt32(fdelay.Value);
                        for (int i = Variables.BlockPlacingTilVal1; i < Variables.BlockPlacingTilVal2; i++)
                        {
                            if (i > 1000 || i < 500)
                            {
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.BlockPlacingTilX, Variables.BlockPlacingTilY, i });
                            }
                            else
                            {
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.BlockPlacingTilX, Variables.BlockPlacingTilY, i });
                            }
                            if (i == Variables.BlockPlacingTilVal2)
                            {
                                Variables.con.Send(Variables.worldKey, new object[] { 0, Variables.BlockPlacingTilX, Variables.BlockPlacingTilY, 0 });
                                Variables.con.Send(Variables.worldKey, new object[] { 1, Variables.BlockPlacingTilX, Variables.BlockPlacingTilY, 0 });
                                break;
                            }
                            Thread.Sleep(Delay);
                        }
                    });
                }
            }
        }

        private void ltu_CheckedChanged(object sender, EventArgs e)
        {
            if (ltu.Checked == true)
            {
                MessageBox.Show("Kalba dabar yra LTU/LT", "R42Bot++ v" + Version.version + " sistema");
                Variables.CurrentLang = "ltu";
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
        }

        private void faxII_CheckedChanged(object sender, EventArgs e)
        {
            if (faxII.Checked)
            {
                mineralRAINBOWFAST.Checked = false;
                fax.Checked = false;
                mineralRAINBOW.Checked = false;
                if (Variables.CheckSnakeUpdate)
                {
                    Variables.currentChecked = "faxII";
                }
            }
            else
            {
                if (Variables.CheckSnakeUpdate)
                {
                    Variables.currentChecked = "";
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
        }

        private void leftallcase_CheckedChanged(object sender, EventArgs e)
        {
            if (leftallcase.Checked)
            {
                leftallupper.Checked = false;
                CallsSettings.Goodbye_Upper = false;
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
        }

        private void welcomeallupper_CheckedChanged(object sender, EventArgs e)
        {
            if (welcomeallupper.Checked)
            {
                welcomealllower.Checked = false;
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
            ThreadPool.QueueUserWorkItem(delegate
            {
                for (int x = 1; x < Variables.worldHeight; x++)
                {
                    for (int y = 1; y < Variables.worldWidth; y++)
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
                            targetID = new Random().Next(1, 99); portalID = new Random().Next(1, 99);
                        }
                        if (Bid == 374)
                        {
                            worldid = true; worldID = idofworld.Text;
                        }
                        if (Bid == 113 || Bid == 185 || Bid == 184)
                        {
                            switchid = true; switchD = new Random().Next(1, 99);
                        }
                        if (Bid == 165 || Bid == 214 || Bid == 43 || Bid == 213 || Bid == 1011 || Bid == 1012)
                        {
                            value = true; valueID = new Random().Next(1, 99);
                        }
                        if (Bid == 385)
                        {
                            List<string> alphabet = new List<string> { "abc", "abcd", "abcde", "abcdef", "abcdefg", "abcdefgh" };
                            sign = true; message = alphabet[new Random().Next(1, alphabet.Count)];
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
                            Variables.con.Send(Variables.worldKey, new object[] { FG, x, y, Bid });
                        }
                        else
                        {
                            if (rotate == true && id == false)
                            {
                                Variables.con.Send(Variables.worldKey, new object[] { FG, x, y, Bid, rotateID });
                            }
                            else if (rotate == true && id == true)
                            {
                                Variables.con.Send(Variables.worldKey, new object[] { FG, x, y, Bid, rotateID, portalID, targetID });
                            }
                            if (worldid)
                            {
                                Variables.con.Send(Variables.worldKey, new object[] { FG, x, y, Bid, worldID });
                            }
                            if (switchid)
                            {
                                Variables.con.Send(Variables.worldKey, new object[] { FG, x, y, Bid, switchD });
                            }
                            if (value)
                            {
                                Variables.con.Send(Variables.worldKey, new object[] { FG, x, y, Bid, valueID });
                            }
                            if (sign)
                            {
                                Variables.con.Send(Variables.worldKey, new object[] { FG, x, y, Bid, message });
                            }
                        }
                        #endregion
                        Thread.Sleep(30);
                    }
                    Thread.Sleep(30);
                }
                grbutton.Text = "Generate Random Bricks";
                Gen_RB.Stop();
                Gen_RB.Enabled = false;
            });
        }

        private void dutchCBOX_CheckedChanged(object sender, EventArgs e)
        {
            if (dutchCBOX.Checked == true)
            {
                MessageBox.Show("Je taal is nu Nederlands.", "R42Bot++ v" + Version.version + Voids.GetLangFile("dutch", 96));
                Variables.CurrentLang = "dutch";
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
        }

        private void AutoFixBot_Tick(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                this.Text = "R42Bot++ v" + Version.version + " " + Voids.GetLangFile(Variables.CurrentLang, 99) + " " + R42Bot.Properties.Settings.Default.Build.ToString();
                Version.UpToDate = Voids.GetLangFile(Variables.CurrentLang, 98).Replace("(V)", Version.version);
                Version.OutOfDate = Voids.GetLangFile(Variables.CurrentLang, 96).Replace("(V)", Version.version).Replace("[V]", new System.Net.WebClient().DownloadString(Version.versionlink));
                Version.OutOfDateBuild = Voids.GetLangFile(Variables.CurrentLang, 97).Replace("(B)", Variables.BuildVersion.ToString()).Replace("[B]", new System.Net.WebClient().DownloadString(Version.buildlink));

                if (new System.Net.WebClient().DownloadString(Version.versionlink) != Version.version)
                {
                    Version.upgradedVersion = new System.Net.WebClient().DownloadString(Version.versionlink);
                    label48.Text = Version.OutOfDate;
                }
                else if (new System.Net.WebClient().DownloadString(Version.buildlink) != Variables.BuildVersion.ToString())
                {
                    Version.upgradedBuild = new System.Net.WebClient().DownloadString(Version.buildlink);
                    label48.Text = Version.OutOfDateBuild;
                }
                else
                {
                    label48.Visible = true;
                    label48.ForeColor = Color.DarkOliveGreen;
                    label48.Text = Version.UpToDate;
                }
                Thread.Sleep(100);
            });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AutoFixBot.Stop();
            AutoFixBot.Enabled = false;
            Gen_RB.Stop();
            Gen_RB.Enabled = false;
            BlockPlacer.Stop();
            BlockPlacer.Enabled = false;
        }

        private void saveLang_Click(object sender, EventArgs e)
        {
            try
            {
                // /respawn
                Information info = new Information();
                info.language = Variables.CurrentLang;
                Class1.SaveData(info, "R42Bot++LanguageFile.xml");
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