using System;
using System.IO;
using System.Xml.Serialization;

namespace Planner
{
    public static class Library
    {
        public static string MainSavePath = @"save.xml";
        public static string FinishedPath = @"done.xml";

        public static Save LoadFile() => LoadFile(MainSavePath);

        /// <summary>
        /// Returns a <see cref="Save"/> from an available file
        /// </summary>
        /// <returns></returns>
        public static Save LoadFile(string d)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Save));

            using (var f = new FileStream(d, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return (Save)xs.Deserialize(f);
            }
        }

        public static void SaveFile(Save s) => SaveFile(s, MainSavePath);

        /// <summary>
        /// Writes a file using the given <see cref="Save"/>
        /// </summary>
        /// <param name="s"></param>
        public static void SaveFile(Save s, string d)
        {
            var xs = new XmlSerializer(typeof(Save));

            using (TextWriter sw = new StreamWriter(d))
            {
                xs.Serialize(sw, s);
            }
        }
    }
}
