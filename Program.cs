using System;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;

namespace AutosarParser
{

    public static class XmlSerializerCache
    {
        private static readonly Dictionary<string, XmlSerializer> cache =
                                new Dictionary<string, XmlSerializer>();

        public static XmlSerializer Create(Type type, XmlRootAttribute root)
        {
            var key = String.Format(
                      CultureInfo.InvariantCulture,
                      "{0}:{1}",
                      type,
                      root.ElementName);

            if (!cache.ContainsKey(key))
            {
                cache.Add(key, new XmlSerializer(type, root));
            }

            return cache[key];
        }
    }

    class Program
    {
        

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

     

            var serializer = new XmlSerializer(typeof(AUTOSAR));
            var target = new AUTOSAR();

            using (Stream reader = new FileStream(@"AutosarXMLHere", FileMode.Open))
            {
                // Call the Deserialize method to restore the object's state.
                target = (AUTOSAR)serializer.Deserialize(reader);
            }
            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed);

         

        }
    }
}
