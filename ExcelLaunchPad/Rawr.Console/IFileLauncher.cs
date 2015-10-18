namespace Rawr.LaunchPad.ConsoleApp
{
    public interface IFileLauncher 
    {
        bool FileExists { get; }
        void Launch();
    }
}