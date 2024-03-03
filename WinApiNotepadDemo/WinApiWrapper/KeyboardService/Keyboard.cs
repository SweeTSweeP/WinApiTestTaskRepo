using WinApiNotepadDemo.Service;

namespace WinApiNotepadDemo.WinApiWrapper.KeyboardService
{
    public class Keyboard : IPCKeyboard, IService
    {
        private Random random = new(Environment.TickCount);

        public void Type(string text, int delay) =>
            Type(text, delay, delay);

        public void PressCtrlV() => 
            PressKey((byte)char.ToUpper('v'), false, false, true);

        public void PressAltShift() => 
            PressKey(ConstantsKeys.VK_LSHIFT, false, true, false);

        public void PressCtrlH() =>
            PressKey((byte)char.ToUpper('h'), false, false, true);
        
        public void PressCtrlS() =>
            PressKey((byte)char.ToUpper('s'), false, false, true);

        public void PressEnter() => 
            PressKey(ConstantsKeys.VK_RETURN);

        public void PressTab() =>
            PressKey(ConstantsKeys.VK_TAB, false, false, false);

        public void PressEscape() =>
            PressKey(ConstantsKeys.VK_ESCAPE, false, false, false);
        
        public void PressAltF4() =>
            PressKey(ConstantsKeys.VK_F4, false, true, false);

        private void PressKey(byte key, bool shift, bool alt, bool ctrl) => 
            PressKey(key, shift, alt, ctrl, 0);
        
        private void Type(string text, int delayFrom, int delayTo)
        {
            KeyboardLayout keyboardLayout = new();
            
            foreach (var symbolInText in text)
            {
                var shift = char.IsUpper(symbolInText);

                var lowerSymbol = char.ToLower(symbolInText);
                char symbol; 
                
                if (ConstantsKeys.RussianSymbols.TryGetValue(lowerSymbol, out var res))
                {
                    keyboardLayout.SetKeyboardLayoutAltShift(this, KeyboardLayout.Language.Russian);
                    symbol = res;
                }
                else
                {
                    keyboardLayout.SetKeyboardLayoutAltShift(this, KeyboardLayout.Language.English);
                    symbol = symbolInText;
                }

                byte key;
                
                if (char.IsLetterOrDigit(symbol)) 
                    key = (byte)char.ToUpper(symbol);
                else if (ConstantsKeys.SpecialSymbols.TryGetValue(symbol, out var symbolCode))
                {
                    shift = symbolCode.isShift;
                    key = symbolCode.code;
                }
                else if (symbol == ' ')
                    key = ConstantsKeys.VK_SPACE;
                else
                    continue;

                PressKey(key, shift, false, false);

                var delay = random.Next(delayFrom, delayTo);

                Thread.Sleep(delay);
            }
        }

        private void PressKey(byte key) => 
            PressKey(key, false, false, false);
        
        private void PressKey(byte key, bool shift, bool alt, bool ctrl, int holdFor)
        {
            byte bScan = 0x45;
            
            if (key == ConstantsKeys.VK_SNAPSHOT) bScan = 0;
            if (key == ConstantsKeys.VK_SPACE) bScan = 39;

            if (alt) WinApiWrapper.keybd_event(ConstantsKeys.VK_MENU, 0, 0, UIntPtr.Zero);
            if (ctrl) WinApiWrapper.keybd_event(ConstantsKeys.VK_LCONTROL, 0, 0, UIntPtr.Zero);
            if (shift) WinApiWrapper.keybd_event(ConstantsKeys.VK_LSHIFT, 0, 0, UIntPtr.Zero);

            WinApiWrapper.keybd_event(key, bScan, WinApiWrapper.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);

            if (holdFor > 0) Thread.Sleep(holdFor);

            WinApiWrapper.keybd_event(key, bScan, WinApiWrapper.KEYEVENTF_EXTENDEDKEY | WinApiWrapper.KEYEVENTF_KEYUP, UIntPtr.Zero);

            if (shift) WinApiWrapper.keybd_event(ConstantsKeys.VK_LSHIFT, 0, WinApiWrapper.KEYEVENTF_KEYUP, UIntPtr.Zero);
            if (ctrl) WinApiWrapper.keybd_event(ConstantsKeys.VK_LCONTROL, 0, WinApiWrapper.KEYEVENTF_KEYUP, UIntPtr.Zero);
            if (alt) WinApiWrapper.keybd_event(ConstantsKeys.VK_MENU, 0, WinApiWrapper.KEYEVENTF_KEYUP, UIntPtr.Zero);
        }
    }
}
