namespace WinApiNotepadDemo.WinApiWrapper.WindowService;

public interface IWindowDetector
{
    List<IntPtr> GetAllWindowsByProcess(string processName, bool onlyVisible);
}