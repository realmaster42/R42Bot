using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerIOClient;

namespace R42Bot
{
    public struct Player
    {
        public int x, y, userid;
        public string username;
        public bool isGuest;
        public bool isGod, isAdmin, isFriend;
        public bool AlreadyReedit;

        public int wins,
            warnings,
            BlocksPlacedInaSecond;

    }

    public partial class BlockUtils
    {
        public static bool CoinDoor(GetBlock b)
        {
            if (b.BlockID == 43 || b.BlockID == 213)
            {
                return true;
            }
            return false;
        }
        public static bool YellowCoinDoor(GetBlock b)
        {
            if (b.BlockID == 43)
            {
                return true;
            }
            return false;
        }
        public static bool BlueCoinDoor(GetBlock b)
        {
            if (b.BlockID == 213)
            {
                return true;
            }
            return false;
        }
        public static bool CoinGate(GetBlock b)
        {
            if (b.BlockID == 165 || b.BlockID == 213)
            {
                return true;
            }
            return false;
        }
        public static bool YellowCoinGate(GetBlock b)
        {
            if (b.BlockID == 165)
            {
                return true;
            }
            return false;
        }
        public static bool BlueCoinGate(GetBlock b)
        {
            if (b.BlockID == 213)
            {
                return true;
            }
            return false;
        }
    }
    public struct GetBlock
    {
        public int BlockID { get; set; }
        public int CoinValue { get; set; }
        public int Rotation { get; set; }
        public int Target { get; set; }
        public int ID { get; set; }
        public bool YellowCoinDoor { get; set; }
        public bool BlueCoinDoor { get; set; }
        public bool YellowCoinGate { get; set; }
        public bool BlueCoinGate { get; set; }
        public string placer { get; set; }
    }
}
