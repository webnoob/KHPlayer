using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace KHPlayer.Extensions
{
    public static class SerializationExtensions
    {
        public static T ToDeserialisedXml<T>(this Object obj, bool isFile = false)
        {
            string useStr = obj.ToString();

            if (isFile)
                using (var sr = new StreamReader(obj.ToString(), System.Text.Encoding.UTF8))
                    useStr = sr.ReadToEnd();

            var serialiser = new XmlSerializer(typeof(T));
            return (T)serialiser.Deserialize(new StringReader(useStr));
        }

        public static string ToSerialisedXml<T>(this T toSerialise, string filePath = null)
        {
            var serialiser = new XmlSerializer(toSerialise.GetType());
            if (!string.IsNullOrEmpty(filePath))
                using (var sr = new StreamWriter(filePath))
                    serialiser.Serialize(sr, toSerialise);

            var textWriter = new StringWriter();
            serialiser.Serialize(textWriter, toSerialise);
            return textWriter.ToString();
        }

        public static string ToSerializedJson<T>(this T toSerialise, string filePath = null)
        {
            var serializedString = JsonConvert.SerializeObject(toSerialise);
            if (!string.IsNullOrEmpty(filePath))
                using (var sr = new StreamWriter(filePath))
                    sr.Write(serializedString);
            
            return serializedString;
        }

        public static T ToDeserialisedJson<T>(this String obj, bool isFile = false)
        {
            string useStr = obj.ToString();

            if (isFile)
                using (var sr = new StreamReader(obj.ToString(), System.Text.Encoding.UTF8))
                    useStr = sr.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(useStr);
        }
    }
}
