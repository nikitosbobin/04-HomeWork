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
        public static T GetResource<T>(string[] args)
        {
            if (types[typeof (T)] == "path")
            {
                foreach (var word in args)
                {
                    if (File.Exists(word))
                        return (T)commands[types[typeof(T)]](word);
                }
            }
            var command = args.Where(c => c.IndexOf(types[typeof(T)], StringComparison.Ordinal) == 0).ToArray();
            if (command.Length == 0)
                return default(T);
            return (T) commands[types[typeof(T)]](command[0]);
        }

        private static string GetPath(string stringCommand)
        {
            if (!File.Exists(stringCommand))
                throw new Exception();
            return stringCommand;
        }

        private static Size GetSize(string stringCommand)
        {
            var pattern = "size:[0-9]+,[0-9]+";
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand.Substring(5);
            var splitted = stringCommand.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            return new Size(int.Parse(splitted[0]), int.Parse(splitted[1]));
        }

        private static int GetScale(string stringCommand)
        {
            var pattern = "scale:[1-9]";
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand[6].ToString();
            return int.Parse(stringCommand);
        }

        private static bool GetDensityFlag(string stringCommand)
        {
            if (stringCommand != "moreDensity")
                return false;
            return true;
        }

        private static HashSet<string> GetBoringWords(string stringCommand)
        {
            var pattern = "boring:<.+>";
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
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
            var pattern = "colors:<.+>";
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

        private static Font GetFont(string stringCommand)
        {
            var pattern = "font:[a-zA-Z ]";
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand.Substring(5);
            return new Font(stringCommand, 12f);
        }

        private static Dictionary<Type, string> types = new Dictionary<Type, string>
        {
            {typeof (string), "path"},
            {typeof (Size), "size"},
            {typeof (int), "scale"},
            {typeof (bool), "moreDinsity"},
            {typeof (HashSet<string>), "boring"},
            {typeof (Font), "font"},
            {typeof (List<SolidBrush>), "colors"}
        };

        private static Dictionary<string, Func<string, object>> commands = new Dictionary<string, Func<string, object>>
        {
            {"path", c => GetPath(c)},
            {"size", c => GetSize(c)},
            {"scale", c => GetScale(c)},
            {"moreDinsity", c => GetDensityFlag(c)},
            {"boring", c => GetBoringWords(c)},
            {"font", c => GetFont(c)},
            {"colors", c => GetColors(c)}
        };
    }
}
