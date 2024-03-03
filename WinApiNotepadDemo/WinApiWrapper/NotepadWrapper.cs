namespace WinApiNotepadDemo.WinApiWrapper;

public class NotepadWrapper
{
    public void ReplaceText(Keyboard keyboard, string oldText, string newText)
    {
        keyboard.PressCtrlH();
        Thread.Sleep(1000);
        keyboard.Type(oldText, 5);
        keyboard.PressTab();
        keyboard.Type(newText, 5);
        keyboard.PressTab();
        keyboard.PressTab();
        keyboard.PressTab();
        keyboard.PressTab();
        keyboard.PressTab();
        keyboard.PressEnter();
        keyboard.PressEscape();
    }
    
    public void SaveNotepadFile(Keyboard keyboard)
    {
        var fileName = "MyFile";
        
        keyboard.PressCtrlS();

        Thread.Sleep(1000);
        
        var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $@"\{fileName}";
        
        keyboard.Type(filePath, 5);
        keyboard.PressEnter();

        if (!File.Exists($"{filePath}.txt")) return;
        
        keyboard.PressTab();
        keyboard.PressEnter();
    }

    public void CloseNotepad(Keyboard keyboard) => 
        keyboard.PressAltF4();
}