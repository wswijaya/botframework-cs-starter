using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;

namespace StarterBot.Base
{
    public static class StringExtensions
    {

        public static string[] stopWords = { @"\bGET_STARTED_PAYLOAD\b", @"^\?", "^FEEDBACK", "^Scope", "Terms of Use", "Privacy Statement" };

        public static string[] Wrap(this string text, int max)
        {
            var charCount = 0;
            var lines = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return lines.GroupBy(w => (charCount += (((charCount % max) + w.Length + 1 >= max)
                            ? max - (charCount % max) : 0) + w.Length + 1) / max)
                        .Select(g => string.Join(" ", g.ToArray()))
                        .ToArray();
        }

        public static List<string> SplitIt(this string text, int max)
        {
            if (text.Length <= max)
            {
                return new List<string>() { text };
            }
            string[] lines = Regex.Split(text, "( |<a.*?>.*?</a>)");

            StringBuilder sb = new StringBuilder();
            List<string> result = new List<string>();
            foreach (string l in lines)
            {
                var length = l.Length;
                if (l.StartsWith("<") && l.EndsWith(">"))
                {
                    string[] temp = Regex.Split(l, @"<.*?>(.*?)</.*?>");
                    length = 0;
                    foreach (var t in temp)
                    {
                        length += t.Length;
                    }
                }
                if (sb.Length + length > max)
                {
                    result.Add(sb.ToString());
                    sb.Clear();
                    sb.Append(l);
                }
                else
                {
                    sb.Append(l);
                }
            }
            if (sb.Length > 0) result.Add(sb.ToString());
            return result;
        }

        public static List<string> SplitIt2(this string text, int max)
        {
            if (text.Length <= max)
            {
                return new List<string>() { text };
            }
            string[] lines = Regex.Split(text, " ");

            StringBuilder sb = new StringBuilder();
            List<string> result = new List<string>();
            foreach (string l in lines)
            {
                if (sb.Length + l.Length >= max) //to fix split issue where first chunk of text exactly = to max.
                {
                    result.Add(sb.ToString());
                    sb.Clear();
                    sb.Append(l);
                    sb.Append(" ");
                }
                else
                {
                    sb.Append(l);
                    sb.Append(" ");
                }
            }
            if (sb.Length > 0) result.Add(sb.ToString());
            return result;
        }

        public static string GZipSerialize(this string conversationReference)
        {
            using (var cmpStream = new MemoryStream())
            using (var stream = new GZipStream(cmpStream, CompressionMode.Compress))
            {
                new BinaryFormatter().Serialize(stream, conversationReference);
                stream.Close();
                return Convert.ToBase64String(cmpStream.ToArray());
            }
        }

        public static string GZipDeserialize(this string conversationReferenceZip)
        {
            byte[] bytes = Convert.FromBase64String(conversationReferenceZip);

            using (var stream = new MemoryStream(bytes))
            using (var gz = new GZipStream(stream, CompressionMode.Decompress))
            {
                return (string)(new BinaryFormatter().Deserialize(gz));
            }
        }
    }
}