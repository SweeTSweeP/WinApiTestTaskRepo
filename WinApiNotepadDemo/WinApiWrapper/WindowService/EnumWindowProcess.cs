namespace WinApiNotepadDemo.WinApiWrapper.WindowService
{
    public class EnumWindowProcess
    {
        public delegate bool EnumWindowsProcess(IntPtr hWnd, IntPtr parameter);
    }
}
