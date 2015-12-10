using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TagCloudGenerator.Classes.DefaultCommands;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class CommandsParser
    {
        private HashSet<ICommand> _commands;

        public ICommand[] GetCommands()
        {
            return _commands.ToArray();
        }

        public Dictionary<string, ICommand> RegisteredCommands { get; }

        public bool ExecuteAllCommands()
        {
            if (_commands.Count == 1 && _commands.ToArray()[0].GetKeyWord() == "help")
            {
                _commands.ToArray()[0].GetResource();
                return false;
            }
            foreach (var command in _commands)
                command.GetResource();
            return true;
        }

        public T GetResource<T>(string key)
        {
            if (RegisteredCommands.ContainsKey(key))
                return (T) RegisteredCommands[key].GetResource();
            return default(T);
        }

        public CommandsParser(string[] args)
        {
            RegisteredCommands = new Dictionary<string, ICommand>
            {
                { "path", new SetPath(this) },
                { "size", new SetSize(this) },
                { "scale", new SetWordsScale(this) },
                { "moreDensity", new SetDensityFlag(this) },
                { "boring", new SetBoringWords(this) },
                { "font", new SetFontFamily(this) },
                { "colors", new SetWordsColors(this)},
                { "help", new Help(this) }
            };
            _commands = new HashSet<ICommand>();

            var pattern = ".+:|.+$";
            string keyWord;
            foreach (var command in args)
            {
                if (File.Exists(command))
                {
                    _commands.Add(RegisteredCommands["path"].CreateCommand(command));
                    continue;
                }
                keyWord = Regex.Match(command, pattern).ToString().Replace(":", "");
                if (RegisteredCommands.ContainsKey(keyWord))
                    _commands.Add(RegisteredCommands[keyWord].CreateCommand(command));
            }
        }

        public bool AddForeignCommand(string keyWord, ICommand command)
        {
            try
            {
                RegisteredCommands.Add(keyWord, command);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
