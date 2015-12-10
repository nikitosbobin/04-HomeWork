using TagCloudGenerator.Classes;

namespace TagCloudGenerator.Interfaces
{
    interface ICommand
    {
        object GetResource();
        ICommand CreateCommand(string stringCommand);
        CommandsParser ParentParser { get; }
        string GetKeyWord();
        string GetDescription();
    }
}
