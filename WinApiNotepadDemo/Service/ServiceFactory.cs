using WinApiNotepadDemo.NotepadService;
using WinApiNotepadDemo.WinApiWrapper.ClipboardService;
using WinApiNotepadDemo.WinApiWrapper.KeyboardService;
using WinApiNotepadDemo.WinApiWrapper.WindowService;

namespace WinApiNotepadDemo.Service;

public static class ServiceFactory
{
    public static IPCKeyboard PCKeyboard => GetService<Keyboard>();
    public static IWindowDetector WindowDetector => GetService<WindowDetector>();
    public static IClipboard Clipboard => GetService<Clipboard>();
    public static INotepadWrapper NotepadWrapper => GetService<NotepadWrapper>();
    public static IWindow Window => GetService<Window>();
    
    private static T GetService<T>() where T : IService, new() => new();
}