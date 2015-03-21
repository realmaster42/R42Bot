using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerIOClient;

namespace R42Bot
{

    public partial class Variables
    {
        public static Connection con;
        public static Client client;
        public static int BuildVersion;

        public static List<string> banList = new List<string> { "realmaster42", "", "", "", "", "" };
        public static Dictionary<int, string> names = new Dictionary<int, string>();
        public static Player[] player = new Player[9999];
        public static int players;
        public static string worldKey;
        public static string worldowner, worldtitle, str;
        public static string botName = null;
        public static int ax, ay, plays, woots, totalwoots, botid, worldWidth, worldHeight;
        public static uint[,,] blockIDs;
        public static string[,,] blockPLACERs;
        public static GetBlock[,] block;
        public static int[] blockMoverArray = new int[] { 12 };
        public static int blockID1 = 0;
        public static bool isFG = false,
            botIsPlacing = false,
            botFullyConnected = false,
            CheckSnakeUpdate = true;
        public static int old_x = 0;
        public static string currentOwner = " ";
        public static string currentTitle = " ";
        public static string currentChecked = "";
        public static int currentPlays = 0;
        public static int currentWoots = 0;
        public static int BlockPlacingTilVal1 = 1,
            BlockPlacingTilVal2 = 2,
            BlockPlacingTilX = 1,
            BlockPlacingTilY = 1;
    }
}
