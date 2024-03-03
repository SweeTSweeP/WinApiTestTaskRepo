using System.Runtime.InteropServices;
using WinApiNotepadDemo.Service;

namespace WinApiNotepadDemo.WinApiWrapper.ClipboardService;

public class Clipboard : IClipboard, IService
{
   public void SaveDataToClipboard(string data)
   {
      WinApiWrapper.OpenClipboard(IntPtr.Zero);
      var ptr = Marshal.StringToHGlobalUni(data);
      WinApiWrapper.SetClipboardData(13, ptr);
      WinApiWrapper.CloseClipboard();
      Marshal.FreeHGlobal(ptr);
   }
}