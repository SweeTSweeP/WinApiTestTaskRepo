using System.Runtime.InteropServices;

namespace WinApiNotepadDemo.WinApiWrapper.KeyboardService;

public class InputSender
{
    const uint INPUT_KEYBOARD = 1;
    
    public void PressKeys(int[] keyCodes)
    {
        INPUT[] inputs = new INPUT[keyCodes.Length * 2];

        var step = 0;
        
        foreach (var keyCode in keyCodes)
        {
            inputs[step].type = INPUT_KEYBOARD;
            inputs[step].U.ki.wVk = (ushort)keyCode;
            inputs[step].U.ki.dwFlags = 0;

            step++;
        }
        
        foreach (var keyCode in keyCodes)
        {
            inputs[step].type = INPUT_KEYBOARD;
            inputs[step].U.ki.wVk = (ushort)keyCode;
            inputs[step].U.ki.dwFlags = WinApiWrapper.KEYEVENTF_KEYUP;

            step++;
        }

        WinApiWrapper.SendInput(4, inputs, Marshal.SizeOf(typeof(INPUT)));
    }
}