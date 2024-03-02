using System.Runtime.InteropServices;
using System.Text;

namespace WinApiNotepadDemo.WinApiWrapper;

public class Clipboard
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