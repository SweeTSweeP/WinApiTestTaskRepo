using WinApiNotepadDemo.Service;
using WinApiNotepadDemo.WinApiWrapper.KeyboardService;

namespace WinApiNotepadDemo.NotepadService;

public interface INotepadWrapper : IService
{
    void ReplaceText(string oldText, string newText);
    void SaveNotepadFile();
    void CloseNotepad();
}