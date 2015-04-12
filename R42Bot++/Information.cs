﻿using System.Drawing;
using System.Collections.Generic;

namespace R42Bot
{
    public class Information
    {
        public bool Stalk { get; set; }
        public List<List<string>> Restrictions { get; set; }
        public string language{ get; set; }
        public string[] Admins{ get; set; }

        public Color Color1{ get; set; }
    }
}
