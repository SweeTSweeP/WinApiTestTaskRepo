using System;

namespace WinApiNotepadDemo.WinApiWrapper
{
    public class Keyboard
    {
        private Random random = new(Environment.TickCount);

        public void Type(string text, int delay) =>
            Type(text, delay, delay);
        
        public void PressEnter() => 
            PressKey(ConstantsKeys.VK_RETURN);

        public void PasteText() => 
            PressKey((int)char.ToUpper('v'), false, false, true);
        
        public void PressCtrlShift()
        {
            //PressKey(ConstantsKeys.VK_LSHIFT, false, true, false);
            
            const int KEYEVENTF_EXTENDEDKEY = 0x0001;
            const int KEYEVENTF_KEYUP = 0x0002;
            const int VK_CONTROL = 0x11; // Код клавиши "Ctrl"
            const int VK_SHIFT = 0x10;
            
            WinApiWrapper.keybd_event(VK_CONTROL, 0, KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);

            // Нажатие клавиши "Shift"
            WinApiWrapper.keybd_event(VK_SHIFT, 0, KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);

            // Отпускание клавиш "Ctrl" и "Shift"
            WinApiWrapper.keybd_event(VK_SHIFT, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, UIntPtr.Zero);
            WinApiWrapper.keybd_event(VK_CONTROL, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, UIntPtr.Zero);
        }

        private void Type(string text, int delayFrom, int delayTo)
        {
            foreach (var symbol in text)
            {
                var shift = char.IsUpper(symbol);

                int key;
                if (char.IsLetterOrDigit(symbol)) 
                    key = (int)char.ToUpper(symbol);
                else
                    switch (symbol)
                    {
                        case '.' or ',':
                            key = ConstantsKeys.VK_DECIMAL;
                            break;
                        case ' ':
                            key = ConstantsKeys.VK_SPACE;
                            break;
                        default:
                            continue;
                    }

                PressKey(key, shift, false, false);

                int delay = random.Next(delayFrom, delayTo);

                Thread.Sleep(delay);
            }
        }

        private void PressKey(int key, bool shift, bool alt, bool ctrl) => 
            PressKey(key, shift, alt, ctrl, 0);

        private void PressKey(int key) => 
            PressKey(key, false, false, false);
        
        private void PressKey(int key, bool shift, bool alt, bool ctrl, int holdFor)
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
