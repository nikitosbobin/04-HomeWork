using System;
using System.IO;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes.DefaultCommands
{
    class SetPath : ICommand
    {
        public SetPath(CommandsParser parser)
        {
            ParentParser = parser;
        }

        public object GetResource()
        {
            return _path;
        }

        private string _path;

        public ICommand CreateCommand(string stringCommand)
        {
            if (!File.Exists(stringCommand))
                throw new Exception();
            _path = stringCommand;
            return this;
        }

        public CommandsParser ParentParser { get; }
        public string GetKeyWord()
        {
            return "path";
        }

        public string GetDescription()
        {
            return "Параметр задаёт файл с входным текстом.\nИспользование:\nПуть к файлу задаётся без ключевых слов";
        }
    }
}
