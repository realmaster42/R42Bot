using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R42Bot
{
    public struct Player
    {
        public int x, y, userid;
        public string username;
        public bool isGuest;
        public bool isGod, isAdmin, isFriend;

        public int wins,
            warnings,
            BlocksPlacedInaSecond;

    }

    public struct GetBlock
    {
        public int BlockID { get; set; }
        public string placer { get; set; }
    }
}
