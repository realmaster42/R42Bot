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
        public partial class CleverBot
        {
            public static string OP = "";
            public static bool Match(string t, string tx)
            {
                if (t.Contains(tx) || t.Contains(tx.ToLower()) || t.Contains(tx.ToUpper()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public static bool IsNumber(string input)
            {
                try
                {
                    int number = Convert.ToInt32(input);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            public static int ToNumber(string input)
            {
                try
                {
                    return Convert.ToInt32(input);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
            public static bool HasMath(string input)
            {
                List<string> Operators = new List<string> { "+", "-", "/", "*", "^" };

                if (input.Contains("+") || input.Contains("-") || input.Contains("/") || input.Contains("*") || input.Contains("^"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public static void Operate(string input)
            {
                string[] split = input.Split(' ');
                List<List<string>> results = new List<List<string>> { };
                List<string> Operators = new List<string> { "+", "-", "/", "*", "^" };
                bool passedFirst = false;
                string result = "0";

                for (int i = 1; i < split.Length; i++)
                {
                    if (IsNumber(split[i]))
                    {
                        if (i < split.Length && (i + 2 <= split.Length))
                        {
                            List<string> str = new List<string> { };
                            if (Operators.Contains(split[i + 1]) && IsNumber(split[i + 2]))
                            {
                                passedFirst = true;
                                if (split[i + 1] == "+")
                                {
                                    str.Add((ToNumber(split[i]) + ToNumber(split[i + 2])).ToString());
                                }
                                else if (split[i + 1] == "-")
                                {
                                    str.Add((ToNumber(split[i]) - ToNumber(split[i + 2])).ToString());
                                }
                                else if (split[i + 1] == "/")
                                {
                                    str.Add((ToNumber(split[i]) / ToNumber(split[i + 2])).ToString());
                                }
                                else if (split[i + 1] == "*")
                                {
                                    str.Add((ToNumber(split[i]) * ToNumber(split[i + 2])).ToString());
                                }
                                else if (split[i + 1] == "^")
                                {
                                    str.Add((ToNumber(split[i]) ^ ToNumber(split[i + 2])).ToString());
                                }
                                results.Add(str);
                            }
                            else
                            {
                                if (!passedFirst)
                                {
                                    result = "notMath";
                                    break;
                                }
                                else
                                {
                                    passedFirst = false;
                                }
                            }
                        }
                    }
                }
                for (int o=1;o<results.Count;o++)
                {
                    result = Convert.ToInt32(result + Convert.ToInt32(results[o][0])).ToString();
                }
                OP = result;
            }
            public static string Operation(string input)
            {
                Operate(input);
                return OP;
            }
            public static bool IsInsultingBot(string input)
            {
                if ((Match(input, "RClever42") || Match(input, "R42Bot++")) && (Match(input, "duchbag") || Match(input, "idiot") || Match(input, "noob") || Match(input, "dickhead") || Match(input, "coward") || Match(input, "stupid") || Match(input, "ridiculous") || Match(input, "crazy") || Match(input, "sucks") || Match(input, "moron") || Match(input, "g@y")))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public static bool IsInsulting(string input)
            {
                if (Match(input, "duchbag") || Match(input, "idiot") || Match(input, "noob") || Match(input, "dickhead") || Match(input, "coward") || Match(input, "stupid") || Match(input, "ridiculous") || Match(input, "crazy") || Match(input, "sucks") || Match(input, "moron") || Match(input, "g@y"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public static bool IsWelcoming(string input)
            {
                if (Match(input, "hi") || Match(input, "sup") || (Match(input, "wa") && (Match(input, "sup") || Match(input, "zup"))) || Match(input, "hai") || Match(input, "hello"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
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
        public static string FloorUINT(uint Uint)
        {
            string Uinta = Uint.ToString();

            if (Uinta.IndexOf(".")!=0)
            {
                return Uinta.Substring(0, Uinta.IndexOf("."));
            }
            else
            {
                return Uinta;
            }
        }
        public static string HexFromUInt(uint Uint)
        {
            return "#" + FloorUINT(Uint);
        }
        public static string GetLangFile(int FileId)
        {
            if (System.IO.Directory.Exists(Environment.CurrentDirectory + @"\language\"))
            {
                if (CallsSettings.CurrentLang!="")
                {
                    if (System.IO.File.Exists(Environment.CurrentDirectory + @"\language\" + CallsSettings.CurrentLang + ".txt"))
                    {
                        if (System.IO.File.ReadAllLines(Environment.CurrentDirectory + @"\language\" + CallsSettings.CurrentLang + ".txt").Length-1 >= (FileId-1))
                        {
                            return System.IO.File.ReadAllLines(Environment.CurrentDirectory + @"\language\" + CallsSettings.CurrentLang + ".txt")[FileId-1];
                        }
                    }
                }
            }
            return "<LangError>";
        }
    }
}
