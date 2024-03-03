using System.Diagnostics;
using System.Runtime.InteropServices;
using WinApiNotepadDemo.Service;
using static WinApiNotepadDemo.WinApiWrapper.WindowService.EnumWindowProcess;

namespace WinApiNotepadDemo.WinApiWrapper.WindowService
{
    public class WindowDetector : IWindowDetector, IService
    {
        public List<IntPtr> GetAllWindowsByProcess(string processName, bool onlyVisible)
        {
            List<IntPtr> result = new();

            Process process;

            var p = Process.GetProcessesByName(processName);
            
            if (p.Length > 0)
                process = p[0]; 
            else 
                return result;

            var win = process.MainWindowHandle;

            result.Add(win);

            result.AddRange(GetChildWindows(win));

            List<IntPtr> additional = new();

            foreach (var w in result) 
                if (w != win) 
                    additional.AddRange(GetChildWindows(w));

            result.AddRange(additional);

            result = result.Distinct().ToList();

            for (var i = 0; i < result.Count; i++)
            {
                if ((onlyVisible && !WinApiWrapper.IsWindowVisible(result[i])) || (GetProcessId(result[i]) != process.Id))
                {
                    result.RemoveAt(i);
                    i--;
                }
            }

            return result;
        }
        
        private List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new();
            var listHandle = GCHandle.Alloc(result);

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
            var id = 0;

            WinApiWrapper.GetWindowThreadProcessId(win, ref id);

            return id;
        }
        
        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            var gch = GCHandle.FromIntPtr(new IntPtr(pointer));

            if (gch.Target is not List<IntPtr> list)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }

            list.Add(handle);

            return true;
        }
    }
}
