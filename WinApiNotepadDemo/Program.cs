using WinApiNotepadDemo.WinApiWrapper;
using System.Windows;
using WinApiNotepadDemo.NotepadService;
using WinApiNotepadDemo.Service;
using WinApiNotepadDemo.WinApiWrapper.ClipboardService;
using WinApiNotepadDemo.WinApiWrapper.KeyboardService;
using WinApiNotepadDemo.WinApiWrapper.WindowService;

namespace WinApiNotepadDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var detector = ServiceFactory.WindowDetector;
            var keyboard = ServiceFactory.PCKeyboard;
            var clipboard = ServiceFactory.Clipboard;
            var notepadWrapper = ServiceFactory.NotepadWrapper;

            System.Diagnostics.Process.Start("notepad");
            Thread.Sleep(2000);
           
            clipboard.SaveDataToClipboard("Text from clipboard");
            
            var windows = detector.GetAllWindowsByProcess("notepad", false);
            var mainWindow = windows.FirstOrDefault(x => x.GetClass().ToLower() == "notepad");

            mainWindow.Maximize();
            mainWindow.SetFocused();
            
            keyboard.PressCtrlV();
            keyboard.PressEnter();

            keyboard.Type("Hello ", 5);
            keyboard.Type("Привет ", 5);
            keyboard.Type("!@#$%^&*(*)", 5);
            
            notepadWrapper.ReplaceText("Hello", "Bye");
            notepadWrapper.ReplaceText("Привет", "Пока");
            
            notepadWrapper.SaveNotepadFile();
            notepadWrapper.CloseNotepad();
        }
    }
}
