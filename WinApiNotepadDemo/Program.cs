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
            KeyboardLayout keyboardLayout = new();
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

            keyboardLayout.SetKeyboardLayoutAltShift(keyboard, KeyboardLayout.Language.English);
            keyboard.Type("Hello ", 5);

            keyboardLayout.SetKeyboardLayoutAltShift(keyboard, KeyboardLayout.Language.Russian);
            keyboard.Type("Привет ", 5);
            
            keyboardLayout.SetKeyboardLayoutAltShift(keyboard, KeyboardLayout.Language.English);
            keyboard.Type("!@#$%^&*(*)", 5);
            
            keyboardLayout.SetKeyboardLayoutAltShift(keyboard, KeyboardLayout.Language.English);
            notepadWrapper.ReplaceText(keyboard, "Hello", "Bye");
            
            keyboardLayout.SetKeyboardLayoutAltShift(keyboard, KeyboardLayout.Language.Russian);
            notepadWrapper.ReplaceText(keyboard, "Привет", "Пока");
            
            keyboardLayout.SetKeyboardLayoutAltShift(keyboard, KeyboardLayout.Language.English);
            notepadWrapper.SaveNotepadFile(keyboard);
        }
    }
}
