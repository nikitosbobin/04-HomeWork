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
            public CommandData(string pattern, string keyWord, Func<string, object> commandConverter)
            {
                Pattern = pattern;
                KeyWord = keyWord;
                CommandConverter = commandConverter;
            }

            public string Pattern { get; set; }
            public string KeyWord { get; set; }
            public Func<string, object> CommandConverter { get; set; }
        }

        public static T GetResource<T>(string[] args)
        {
            var currentCommand = commands[typeof (T)];
            var commandName = args.Where(c => c.IndexOf(currentCommand.KeyWord, StringComparison.Ordinal) == 0).ToArray();
            if (commandName.Length == 0 || !Regex.IsMatch(commandName[0], currentCommand.Pattern))
                return default(T);
            return (T) currentCommand.CommandConverter(commandName[0]);
        }

        private static string GetPath(string stringCommand)
        {
            stringCommand = stringCommand.Substring(6, stringCommand.Length - 7);
            if (!File.Exists(stringCommand))
                throw new Exception();
            return stringCommand;
        }

        private static Size GetSize(string stringCommand)
        {
            stringCommand = stringCommand.Substring(5);
            var splitted = stringCommand.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            return new Size(int.Parse(splitted[0]), int.Parse(splitted[1]));
        }

        private static int GetScale(string stringCommand)
        {
            stringCommand = stringCommand[6].ToString();
            return int.Parse(stringCommand);
        }

        private static bool GetDensityFlag(string stringCommand)
        {
            return true;
        }

        private static HashSet<string> GetBoringWords(string stringCommand)
        {
            stringCommand = stringCommand.Substring(8, stringCommand.Length - 9);
            var splited = stringCommand.Split(new[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries);
            var boringWords = new HashSet<string>();
            foreach (var word in splited)
                boringWords.Add(word);
            return boringWords;
        }

        private static List<SolidBrush> GetColors(string stringCommand)
        {
            var wordsBrushes = new List<SolidBrush>();
            var converter = new ColorConverter();
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

        private static Font GetFont(string stringCommand)
        {
            stringCommand = stringCommand.Substring(5);
            return new Font(stringCommand, 12f);
        }

        private static Dictionary<Type, CommandData> commands = new Dictionary<Type, CommandData>
        {
            {typeof (string), new CommandData("path:<.+>", "path", (c) => GetPath(c))},
            {typeof (Size), new CommandData("size:[0-9]+,[0-9]+", "size", (c) => GetSize(c))},
            {typeof (int), new CommandData("scale:[1-9]", "scale", (c) => GetScale(c))},
            {typeof (bool), new CommandData("moreDensity", "moreDensity", (c) => GetDensityFlag(c))},
            {typeof (HashSet<string>), new CommandData("boring:<.+>", "boring", (c) => GetBoringWords(c))},
            {typeof (Font), new CommandData("font:[a-zA-Z ]", "font", (c) => GetFont(c))},
            {typeof (List<SolidBrush>), new CommandData("colors:<.+>", "colors", (c) => GetColors(c))}
        };
    }
}
