using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using PlayerIOClient;

namespace R42Bot
{
    public partial class CallsSettings
    {
        public static bool WinSystem = false;
        public static bool AllowJoiners = true,
            KickBots = false;
        public static bool Welcome_Upper = true,
            Welcome = true;
        public static bool Goodbye_Upper = true,
            Goodbye = false;
        public static bool FreeEdit = false;
        public static string Welcome_Text = "",
            Welcome_Text_2 = "";
        public static string Goodbye_Text = "",
            Goodbye_Text_2 = "";
        public static List<string> Bans = new List<string> { };
    }
}
