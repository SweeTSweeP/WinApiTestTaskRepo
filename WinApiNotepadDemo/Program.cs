using WinApiNotepadDemo.WinApiWrapper;

namespace WinApiNotepadDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WindowDetector detector = new();
            Keyboard keyboard = new();

            System.Diagnostics.Process.Start("notepad");
            Thread.Sleep(2000);

            var windows = detector.GetAllWindowsByProcess("notepad", false);

            foreach (var window in windows)
                Console.WriteLine($"{window.GetTitle()} - {window.GetClass()}");

            var mainWindow = windows.FirstOrDefault(x => x.GetClass().ToLower() == "notepad");

            mainWindow.Maximize();
            mainWindow.SetFocused();

            keyboard.Type("hello, hello, hello how are you", 5);
            Console.ReadLine();
        }
    }
}
