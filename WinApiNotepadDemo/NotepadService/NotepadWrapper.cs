using WinApiNotepadDemo.Service;
using WinApiNotepadDemo.WinApiWrapper.KeyboardService;

namespace WinApiNotepadDemo.NotepadService;

public class NotepadWrapper : INotepadWrapper
{
    private IPCKeyboard _keyboard;

    private IPCKeyboard Keyboard => _keyboard ??= ServiceFactory.PCKeyboard;
    
    public void ReplaceText(string oldText, string newText)
    {
        Keyboard.PressCtrlH();
        Thread.Sleep(1000);
        Keyboard.Type(oldText, 5);
        Keyboard.PressTab();
        Keyboard.Type(newText, 5);
        Keyboard.PressTab();
        Keyboard.PressTab();
        Keyboard.PressTab();
        Keyboard.PressTab();
        Keyboard.PressTab();
        Keyboard.PressEnter();
        Keyboard.PressEscape();
    }
    
    public void SaveNotepadFile()
    {
        var fileName = "MyFile";
        
        Keyboard.PressCtrlS();

        Thread.Sleep(1000);
        
        var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $@"\{fileName}";
        
        Keyboard.Type(filePath, 5);
        Keyboard.PressEnter();

        if (!File.Exists($"{filePath}.txt")) return;
        
        Keyboard.PressTab();
        Keyboard.PressEnter();
    }

    public void CloseNotepad() => 
        Keyboard.PressAltF4();
}