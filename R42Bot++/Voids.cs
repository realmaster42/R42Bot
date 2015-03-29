using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerIOClient;

namespace R42Bot
{
    public partial class Voids
    {
        #region Position Database Information <click to see>
        public static string derot(string arg1)
        {
            int num = 0;
            string str = "";
            for (int i = 0; i < arg1.Length; i++)
            {
                num = arg1[i];
                if ((num >= 0x61) && (num <= 0x7a))
                {
                    if (num > 0x6d)
                    {
                        num -= 13;
                    }
                    else
                    {
                        num += 13;
                    }
                }
                else if ((num >= 0x41) && (num <= 90))
                {
                    if (num > 0x4d)
                    {
                        num -= 13;
                    }
                    else
                    {
                        num += 13;
                    }
                }
                str = str + ((char)num);
            }
            return str;
        }
        #endregion

        public static string Shortest(string name)
        {
            if (name.Length >= 5)
            {
                return name.Substring(0, 5);
            }
            else if (name.Length >= 3)
            {
                return name.Substring(0, 3);
            }
            return name;
        }

        public static string GetLangFile(string LangType, int FileId)
        {
            if (LangType == "enUS")
            {
                return Lang.En.Organizate[FileId - 1];
            }
            else if (LangType == "ptbr")
            {
                return Lang.PT.Organizate[FileId - 1];
            }
            else if (LangType == "ltu")
            {
                return Lang.LTU.Organizate[FileId - 1];
            }
            else if (LangType == "dutch")
            {
                return Lang.Dutch.Organizate[FileId - 1];
            }
            return "<LangError>";
        }
    }
}
