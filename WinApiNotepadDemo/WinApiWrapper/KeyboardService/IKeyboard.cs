namespace WinApiNotepadDemo.WinApiWrapper.KeyboardService;

public interface IKeyboard
{
    void Type(string text, int delay);
    
    void PressEnter();
    void PressTab();
    void PressEscape();
    
}