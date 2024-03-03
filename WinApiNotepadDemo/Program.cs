using WinApiNotepadDemo.WinApiWrapper;
using System.Windows;

namespace WinApiNotepadDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WindowDetector detector = new();
            Keyboard keyboard = new();
            Clipboard clipboard = new();
            NotepadWrapper notepadWrapper = new();

            System.Diagnostics.Process.Start("notepad");
            Thread.Sleep(2000);
           
            clipboard.SaveDataToClipboard("Text from clipboard");
            
            var windows = detector.GetAllWindowsByProcess("notepad", false);
            var mainWindow = windows.FirstOrDefault(x => x.GetClass().ToLower() == "notepad");

            mainWindow.Maximize();
            mainWindow.SetFocused();
            
            keyboard.PasteText();
            keyboard.PressEnter();

            keyboard.Type("Hello ", 5);
            keyboard.Type("Привет ", 5);
            keyboard.Type("!@#$%^&*(*)", 5);
            
            notepadWrapper.ReplaceText(keyboard, "Hello", "Bye");
            notepadWrapper.ReplaceText(keyboard, "Привет", "Пока");
            
            notepadWrapper.SaveNotepadFile(keyboard);
            notepadWrapper.CloseNotepad(keyboard);
        }
    }
}
