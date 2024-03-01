using System.Runtime.InteropServices;
using System.Text;
using static WinApiNotepadDemo.WinApiWrapper.EnumWindowProcess;


namespace WinApiNotepadDemo.WinApiWrapper
{
    public static class WinApiWrapper
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowVisible(IntPtr win);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, ref int processId);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumChildWindows(IntPtr window, EnumWindowsProcess callback, IntPtr i);

        [DllImport("user32.dll")]
        internal static extern uint RealGetWindowClass(IntPtr win, StringBuilder pszType, uint cchType);

        [DllImport("user32.dll")]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern void keybd_event(int bVk, byte bScan, UInt32 dwFlags, int dwExtraInfo);

        internal const UInt32 KEYEVENTF_EXTENDEDKEY = 1;
        internal const UInt32 KEYEVENTF_KEYUP = 2;
        internal const int KEY_ALT = 0x12;
        internal const int KEY_CONTROL = 0x11;

    }
}
