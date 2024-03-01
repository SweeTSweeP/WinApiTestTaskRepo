using System;

namespace WinApiNotepadDemo.WinApiWrapper
{
    internal class Keyboard
    {
        private Random random = new(Environment.TickCount);

        public void Type(string text, int delay) =>
            Type(text, delay, delay);
        
        public void Type(string text, int delayFrom, int delayTo)
        {
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                bool shift = char.IsUpper(c);

                int key;
                if (char.IsLetterOrDigit(c)) key = (int)char.ToUpper(c);
                else
                {
                    if (c == '.' || c == ',') key = ConstantsKeys.VK_DECIMAL;
                    else if (c == ' ') key = ConstantsKeys.VK_SPACE;
                    else continue;
                }

                PressKey(key, shift, false, false);

                int delay = random.Next(delayFrom, delayTo);

                Thread.Sleep(delay);
            }
        }

        public void PressKey(int key, bool shift, bool alt, bool ctrl) => 
            PressKey(key, shift, alt, ctrl, 0);

        public void PressKey(int key) => 
            PressKey(key, false, false, false);


        public void PressKey(int key, bool shift, bool alt, bool ctrl, int holdFor)
        {
            byte bScan = 0x45;
            
            if (key == ConstantsKeys.VK_SNAPSHOT) bScan = 0;
            if (key == ConstantsKeys.VK_SPACE) bScan = 39;

            if (alt) WinApiWrapper.keybd_event(ConstantsKeys.VK_MENU, 0, 0, 0);
            if (ctrl) WinApiWrapper.keybd_event(ConstantsKeys.VK_LCONTROL, 0, 0, 0);
            if (shift) WinApiWrapper.keybd_event(ConstantsKeys.VK_LSHIFT, 0, 0, 0);

            WinApiWrapper.keybd_event(key, bScan, WinApiWrapper.KEYEVENTF_EXTENDEDKEY, 0);

            if (holdFor > 0) Thread.Sleep(holdFor);

            WinApiWrapper.keybd_event(key, bScan, WinApiWrapper.KEYEVENTF_EXTENDEDKEY | WinApiWrapper.KEYEVENTF_KEYUP, 0);

            if (shift) WinApiWrapper.keybd_event(ConstantsKeys.VK_LSHIFT, 0, WinApiWrapper.KEYEVENTF_KEYUP, 0);
            if (ctrl) WinApiWrapper.keybd_event(ConstantsKeys.VK_LCONTROL, 0, WinApiWrapper.KEYEVENTF_KEYUP, 0);
            if (alt) WinApiWrapper.keybd_event(ConstantsKeys.VK_MENU, 0, WinApiWrapper.KEYEVENTF_KEYUP, 0);
        }
    }
}
