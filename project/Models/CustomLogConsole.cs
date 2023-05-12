namespace project.Models
{
    public class CustomLogConsole : ICustomLog
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
