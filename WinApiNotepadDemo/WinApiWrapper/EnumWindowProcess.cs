namespace WinApiNotepadDemo.WinApiWrapper
{
    internal class EnumWindowProcess
    {
        /// <summary>
        /// Delegate for the EnumChildWindows method
        /// </summary>
        /// <param name="hWnd">Window handle</param>
        /// <param name="parameter">Caller-defined variable; we use it for a pointer to our list</param>
        /// <returns>True to continue enumerating, false to bail.</returns>
        public delegate bool EnumWindowsProcess(IntPtr hWnd, IntPtr parameter);
    }
}
