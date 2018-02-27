using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Wangrong.BatchSubMerchant.Filereader
{
    public class CsvFileReader : IFileReader<string[]>
    {
        private const string QUOTE = "\"";
        private const string ESCAPED_QUOTE = "\"\"";
        private static readonly Regex rexCsvSplitter = new Regex(@",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))");
        private static readonly Regex rexRunOnLine = new Regex(@"^[^""]*(?:""[^""]*""[^""]*)*""[^""]*$");
        private static readonly char[] CHARACTERS_THAT_MUST_BE_QUOTED = {',', '"', '\n'};

        public IEnumerable<T> ReadFrom<T>(string file, int skipRows, int? top, Func<string[], T> converTo,
            int sheetIndex)
            where T : IImportResult
        {
            var list = new List<T>();
            using (var reader = new StreamReader(File.OpenRead(file)))
            {
                string sLine = null;
                var rowIndex = 0;
                while (null != (sLine = reader.ReadLine()))
                {
                    string sNextLine;
                    rowIndex++;
                    while (rexRunOnLine.IsMatch(sLine) && null != (sNextLine = reader.ReadLine()))
                        sLine += "\n" + sNextLine;
                    if (skipRows >= rowIndex)
                        continue;
                    var values = rexCsvSplitter.Split(sLine);

                    for (var i = 0; i < values.Length; i++)
                        values[i] = Unescape(values[i]);

                    list.Add(converTo(values));
                }
            }
            return list;
        }

        public static string Escape(string s)
        {
            if (s.Contains(QUOTE))
                s = s.Replace(QUOTE, ESCAPED_QUOTE);

            if (s.IndexOfAny(CHARACTERS_THAT_MUST_BE_QUOTED) > -1)
                s = QUOTE + s + QUOTE;

            return s;
        }

        public static string Unescape(string s)
        {
            if (s.StartsWith(QUOTE) && s.EndsWith(QUOTE))
            {
                s = s.Substring(1, s.Length - 2);

                if (s.Contains(ESCAPED_QUOTE))
                    s = s.Replace(ESCAPED_QUOTE, QUOTE);
            }

            return s;
        }
    }
}