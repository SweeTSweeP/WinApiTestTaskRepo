using System.Diagnostics;
using System.Runtime.InteropServices;
using static WinApiNotepadDemo.WinApiWrapper.EnumWindowProcess;

namespace WinApiNotepadDemo.WinApiWrapper
{
    internal class WindowDetector
    {
        public List<IntPtr> GetAllWindowsByProcess(string processName, bool onlyVisible)
        {
            List<IntPtr> result = new();

            Process? process = null;

            Process[] p = Process.GetProcessesByName(processName);
            
            if (p.Length > 0)
                process = p[0]; 
            else 
                return result;

            nint win = process.MainWindowHandle;

            result.Add(win);

            result.AddRange(GetChildWindows(win));

            List<IntPtr> additional = new();

            foreach (IntPtr w in result) 
                if (w != win) 
                    additional.AddRange(GetChildWindows(w));

            result.AddRange(additional);

            result = result.Distinct().ToList();

            for (int i = 0; i < result.Count; i++)
            {
                if ((onlyVisible && !WinApiWrapper.IsWindowVisible(result[i])) || (GetProcessId(result[i]) != process.Id))
                {
                    result.RemoveAt(i);
                    i--;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a list of child windows
        /// </summary>
        /// <param name="parent">Parent of the windows to return</param>
        /// <returns>List of child windows</returns>
        private List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new();
            GCHandle listHandle = GCHandle.Alloc(result);

            try
            {
                EnumWindowsProcess childProc = EnumWindow;
                WinApiWrapper.EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            
            return result.Distinct().ToList();
        }

        private int GetProcessId(IntPtr win)
        {
            int id = 0;

            WinApiWrapper.GetWindowThreadProcessId(win, ref id);

            return id;
        }

        /// <summary>
        /// Callback method to be used when enumerating windows.
        /// </summary>
        /// <param name="handle">Handle of the next window</param>
        /// <param name="pointer">Pointer to a GCHandle that holds a reference to the list to fill</param>
        /// <returns>True to continue the enumeration, false to bail</returns>
        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(new IntPtr(pointer));

            if (gch.Target is not List<IntPtr> list)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }

            list.Add(handle);

            return true;
        }
    }
}
