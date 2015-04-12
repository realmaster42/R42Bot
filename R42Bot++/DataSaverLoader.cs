using System.Xml.Serialization;
using System.IO;

namespace R42Bot
{
    internal class Saver
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
