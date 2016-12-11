using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using R42Bot;

namespace R42Bot
{
    public partial class LangFB : Form
    {
        string ChosenId = "";

        public LangFB()
        {
            InitializeComponent();
            if (System.IO.Directory.Exists(Environment.CurrentDirectory + @"\language\"))
            {
                int files = 0;
                foreach (string ok in System.IO.Directory.GetFiles(Environment.CurrentDirectory + @"\language\"))
                {
                    files++;
                    string final = ok.Replace(Environment.CurrentDirectory, "").Substring(10);
                    if (ok.EndsWith(".txt"))
                    {
                        if (final == "en_us.txt" && CallsSettings.CurrentLang == "") {
                            CallsSettings.CurrentLang = "en_us";
                            ChosenId = "en_us";
                        }

                        listBox1.Items.Add(final.Replace(".txt", "") + " by " + System.IO.File.ReadAllLines(ok)[0]);
                    }
                }

                if (files == 0)
                    MessageBox.Show("No .txt files found.", "R42Bot++", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                System.IO.Directory.CreateDirectory(Environment.CurrentDirectory + @"\language\");
                MessageBox.Show("'language' folder not found. Re-created one, but needs a .txt file.", "R42Bot++", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ChosenId != "")
            {
                int ByDex = ChosenId.IndexOf("by");
                ChosenId = ChosenId.Substring(0, ByDex - 1);
                try
                {
                    if (!System.IO.File.Exists(@"R42Bot++SavedData.xml"))
                    {
                        var info = new Information();
                        info.language = ChosenId;
                        Saver.SaveData(info, "R42Bot++SavedData.xml");
                    }
                    else
                    {
                        var xs = new System.Xml.Serialization.XmlSerializer(typeof(Information));
                        var read = new System.IO.FileStream("R42Bot++SavedData.xml", System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                        var info = (Information)xs.Deserialize(read);

                        info.language = ChosenId;
                        Saver.SaveData(info, "R42Bot++SavedData.xml");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                CallsSettings.CurrentLang = ChosenId;
                CallsSettings.LangChangeInitialized = false;
                this.Close();
            }
            else
            {
                MessageBox.Show("No language file is chosen. Please select one.", "R42Bot++", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChosenId = listBox1.Items[listBox1.SelectedIndex].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ChosenId!="")
            {
                if (MessageBox.Show("This action cannot be undone! Are you sure you want to delete it?", "R42Bot++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    System.IO.File.Delete(Environment.CurrentDirectory + @"\language\" + listBox1.Items[listBox1.SelectedIndex].ToString().Substring(0, listBox1.Items[listBox1.SelectedIndex].ToString().IndexOf("by") - 2) + ".txt");
                }
            }
            else
            {
                MessageBox.Show("No .txt file is choosen to be deleted.", "R42Bot++", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
