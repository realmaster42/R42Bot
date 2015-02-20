using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace R42Bot
{
    public class Information
    {
        private string data1;
        private string data2;
        private string data3;
        private string data4;
        private string[] admins;

        private Color color1;

        private string data5;

        public string Data1
        {
           get { return data1; }
           set { data1 = value; }
        }

        public string Data2
        {
            get { return data2; }
            set { data2 = value; }
        }

        public string Data3
        {
            get { return data3; }
            set { data3 = value; }
        }

        public string Data4
        {
            get { return data4; }
            set { data4 = value; }
        }

        public string Data5
        {
            get { return data5; }
            set { data5 = value; }
        }

        public Color Color1
        {
            get { return color1; }
            set { color1 = value; }
        }

        public string[] Admins
        {
            get { return admins; }
            set { admins = value; }
        }
    }
}
