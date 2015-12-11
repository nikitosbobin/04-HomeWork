using System;
using System.Collections.Generic;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes.DefaultCommands
{
    class Help : ICommand
    {
        public Help(Dictionary<string, ICommand> registeredCommands)
        {
            _registeredCommands = registeredCommands;
        }

        public object GetResource()
        {
            foreach (var command in _registeredCommands)
            {
                Console.WriteLine(command.Value.GetDescription());
            }
            return null;
        }

        public ICommand CreateCommand(string stringCommand)
        {
            if (stringCommand != "help")
                throw new Exception();
            return this;
        }

        private Dictionary<string, ICommand> _registeredCommands;

        public string GetKeyWord()
        {
            return "help";
        }

        public string GetDescription()
        {
            return "Выводит описание всех возможных команд.\nИспользование:\nhelp";
        }
    }
}
