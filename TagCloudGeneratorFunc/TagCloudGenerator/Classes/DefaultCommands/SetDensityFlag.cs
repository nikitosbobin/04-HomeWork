using System;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes.DefaultCommands
{
    class SetDensityFlag : ICommand
    {

        public object GetResource()
        {
            return true;
        }

        public string GetKeyWord()
        {
            return "moreDensity";
        }

        public ICommand CreateCommand(string stringCommand)
        {
            if (stringCommand != "moreDensity")
                throw new Exception();
            return this;
        }

        public string GetDescription()
        {
            return "Задаёт флаг повышенной плотности размещения слов в облаке.\nИспользование:\nmoreDensity";
        }
    }
}
