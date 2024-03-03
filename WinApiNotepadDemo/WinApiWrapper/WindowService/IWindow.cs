namespace WinApiNotepadDemo.WinApiWrapper.WindowService;

public interface IWindow
{
    string GetClass(IntPtr win);
    string? GetTitle(IntPtr hWnd);
    void Maximize(IntPtr hWnd);
    void SetFocused(IntPtr hWnd);
}