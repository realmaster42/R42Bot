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
        public bool AlreadyReedit,
            isBot;

        public int wins,
            warnings,
            BlocksPlacedInaSecond,
            Face,
            diggyscore,
            emeralds,
            rubies,
            silver,
            gold,
            diamond,
            emeraldingots,
            rubieingots,
            silveringots,
            goldingots,
            diamondingots,
            pickHard;

        public bool FillFirstPhase,
            FillSecondPhase,
            FillLastPhase,
            FillBIDSet;
        public int FillXCor,
            FillX2Cor,
            FillYCor,
            FillY2Cor,
            FillBID;


        public List<List<int>> blocks;
    }
}
