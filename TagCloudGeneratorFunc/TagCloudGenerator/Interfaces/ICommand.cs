using TagCloudGenerator.Classes;

namespace TagCloudGenerator.Interfaces
{
    interface ICommand
    {
        object GetResource();
        ICommand CreateCommand(string stringCommand);
        string GetKeyWord();
        string GetDescription();
    }
}
