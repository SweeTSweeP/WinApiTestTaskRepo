using System.Text;

namespace WinApiNotepadDemo.WinApiWrapper
{
    public class Window
    {
        public string GetClass(IntPtr win)
        {
            if (win == IntPtr.Zero) return "";

            var title = new StringBuilder(512);
            
            WinApiWrapper.RealGetWindowClass(win, title, 512);

            return title.ToString().Trim();
        }

        /// <summary>
        /// Get the title of the specified window.
        /// </summary>
        /// <param name="hWnd">The handle to the window.</param>
        /// <returns>The title of the window.</returns>
        public string? GetTitle(IntPtr hWnd)
        {
            const int nChars = 256;
            var Buff = new StringBuilder(nChars);

            if (WinApiWrapper.GetWindowText(hWnd, Buff, nChars) > 0)
                return Buff.ToString();

            return null;
        }

        /// <summary>
        /// Maximize the specified window.
        /// </summary>
        /// <param name="hWnd">The handle to the window.</param>
        public static void Maximize(IntPtr hWnd) => 
            WinApiWrapper.ShowWindow(hWnd, 3);

        /// <summary>
        /// Put focus on a window.
        /// </summary>
        /// <param name="hWnd">The handle to the window.</param>
        public static void SetFocused(IntPtr hWnd) => 
            WinApiWrapper.SetForegroundWindow(hWnd);
    }
}
