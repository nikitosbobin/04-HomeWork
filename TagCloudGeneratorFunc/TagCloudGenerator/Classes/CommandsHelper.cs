using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagCloudGenerator.Classes
{
    static class CommandsHelper
    {
        private class CommandData
        {
            public CommandData(string pattern, string keyWord, Func<string, string, object> commandConverter)
            {
                Pattern = pattern;
                KeyWord = keyWord;
                CommandConverter = commandConverter;
            }

            public string Pattern { get; set; }
            public string KeyWord { get; set; }
            public Func<string, string, object> CommandConverter { get; set; }
        }

        public static T GetResource<T>(string[] args)
        {
            var currentCommand = commands[typeof (T)];
            var command = args.Where(c => c.IndexOf(currentCommand.KeyWord, StringComparison.Ordinal) == 0).ToArray();
            if (command.Length == 0)
                return default(T);
            return (T) currentCommand.CommandConverter(command[0], currentCommand.Pattern);
        }

        private static string GetPath(string stringCommand, string pattern)
        {
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand.Substring(6, stringCommand.Length - 7);
            if (!File.Exists(stringCommand))
                throw new Exception();
            return stringCommand;
        }

        private static Size GetSize(string stringCommand, string pattern)
        {
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand.Substring(5);
            var splitted = stringCommand.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            return new Size(int.Parse(splitted[0]), int.Parse(splitted[1]));
        }

        private static int GetScale(string stringCommand, string pattern)
        {
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand[6].ToString();
            return int.Parse(stringCommand);
        }

        private static bool GetDensityFlag(string stringCommand, string pattern)
        {
            if (stringCommand != pattern)
                return false;
            return true;
        }

        private static HashSet<string> GetBoringWords(string stringCommand, string pattern)
        {
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand.Substring(8, stringCommand.Length - 9);
            var splited = stringCommand.Split(new[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries);
            var boringWords = new HashSet<string>();
            foreach (var word in splited)
                boringWords.Add(word);
            return boringWords;
        }

        private static List<SolidBrush> GetColors(string stringCommand, string pattern)
        {
            var wordsBrushes = new List<SolidBrush>();
            var converter = new ColorConverter();
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand.Substring(8, stringCommand.Length - 9);
            var splited = stringCommand.Split(new[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries);
            Color tempColor;
            foreach (var color in splited)
            {
                try
                {
                    tempColor = (Color) converter.ConvertFromString(color);
                }
                catch (Exception)
                {
                    throw new Exception("Can not convert" + color);
                }
                wordsBrushes.Add(new SolidBrush(tempColor));
            }
            return wordsBrushes;
        }

        private static Font GetFont(string stringCommand, string pattern)
        {
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand.Substring(5);
            return new Font(stringCommand, 12f);
        }

        private static Dictionary<Type, CommandData> commands = new Dictionary<Type, CommandData>
        {
            {typeof (string), new CommandData("path:<.+>", "path", (c,d) => GetPath(c,d))},
            {typeof (Size), new CommandData("size:[0-9]+,[0-9]+", "size", (c,d) => GetSize(c,d))},
            {typeof (int), new CommandData("scale:[1-9]", "scale", (c,d) => GetScale(c,d))},
            {typeof (bool), new CommandData("moreDensity", "moreDensity", (c,d) => GetDensityFlag(c,d))},
            {typeof (HashSet<string>), new CommandData("boring:<.+>", "boring", (c,d) => GetBoringWords(c,d))},
            {typeof (Font), new CommandData("font:[a-zA-Z ]", "font", (c,d) => GetFont(c,d))},
            {typeof (List<SolidBrush>), new CommandData("colors:<.+>", "colors", (c,d) => GetColors(c,d))}
        };
    }
}
