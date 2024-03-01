﻿namespace WinApiNotepadDemo.WinApiWrapper
{
    public static class Extensions
    {
        private static Window window;

        private static Window Window => window ??= new Window();

        public static string GetClass(this IntPtr window) =>
           Window.GetClass(window);

        public static string? GetTitle(this IntPtr window) => 
           Window.GetTitle(window);

        public static void Maximize(this IntPtr window) => 
            Window.Maximize(window);

        public static void SetFocused(this IntPtr window) => 
            Window.SetFocused(window);
    }
}
