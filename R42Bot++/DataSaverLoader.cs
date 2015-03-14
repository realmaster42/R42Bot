﻿using System.IO;
using System.Xml.Serialization;

namespace R42Bot
{
    internal class Class1
    {
        public static void SaveData(object obj, string filename)
        {
            var sr = new XmlSerializer(obj.GetType());
            TextWriter writer = new StreamWriter(filename);
            sr.Serialize(writer, obj);
            writer.Close();
        }
    }
}