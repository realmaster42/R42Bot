﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerIOClient;

namespace R42Bot
{
    public partial class Version
    {
        public static string buildlink = "https://dl.dropbox.com/s/r2km3r838zo14ag/build.txt";
        public static string versionlink = "https://dl.dropbox.com/s/6x0q6qmhd4afu40/version.txt";
        public static string buildversionlink = "https://dl.dropbox.com/s/yr8b5jlqpaa8yym/upgradedversion.txt";

        public static string version = "2.0 - Great Optimization";
        public static string upgradedVersion = "1 - A new reborn";
        public static string upgradedBuildVersion = "1 - A new reborn";
        public static string upgradedBuild = "0";
        public static string UpToDate = "";
        public static string OutOfDate = "";
        public static string OutOfDateBuild = "";
        public static string TestBuild = "";

        public static bool versionLoaded = false;
    }
}
