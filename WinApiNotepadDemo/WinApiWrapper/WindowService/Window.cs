using System.Text;
using WinApiNotepadDemo.Service;

namespace WinApiNotepadDemo.WinApiWrapper.WindowService
{
    public class Window : IWindow, IService
    {
        public string GetClass(IntPtr win)
        {
            if (win == IntPtr.Zero) return "";

            var title = new StringBuilder(512);
            
            WinApiWrapper.RealGetWindowClass(win, title, 512);

            return title.ToString().Trim();
        }
        
        public string? GetTitle(IntPtr hWnd)
        {
            const int nChars = 256;
            var buff = new StringBuilder(nChars);

            if (WinApiWrapper.GetWindowText(hWnd, buff, nChars) > 0)
                return buff.ToString();

            return null;
        }
        
        public void Maximize(IntPtr hWnd) => 
            WinApiWrapper.ShowWindow(hWnd, 3);
        
        public void SetFocused(IntPtr hWnd) => 
            WinApiWrapper.SetForegroundWindow(hWnd);
    }
}
