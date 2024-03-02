﻿using System.Text;

namespace WinApiNotepadDemo.WinApiWrapper;

public class KeyboardLayout
{
    public enum Language
    {
        English,
        Russian
    }

    private const uint KLF_ACTIVATE = 1;
    private const int KL_NAMELENGTH = 9;
    
    private readonly Dictionary<Language, string> _keyboardLayouts = new();

    public KeyboardLayout() => 
        InitLayouts();

    public void SetKeyboardLayout(Language language) =>
        ActivateLayout(_keyboardLayouts.TryGetValue(language, out var layoutCode)
            ? layoutCode
            : _keyboardLayouts[Language.English]);

    public void SetKeyboardLayoutAltShift(Keyboard keyboard, Language language)
    {
        var layouts = new IntPtr[256];
        WinApiWrapper.GetKeyboardLayoutList(layouts.Length, layouts);

        var notZeroLayouts = layouts.Where(s => s != IntPtr.Zero).Select(s=>s.ToString()).ToList();

        if (_keyboardLayouts.TryGetValue(language, out var layoutCode))
        {
            var layoutName = new StringBuilder(KL_NAMELENGTH);
            WinApiWrapper.GetKeyboardLayoutName(layoutName);

            foreach (var dummy in notZeroLayouts)
            {
                if (layoutName.ToString() == layoutCode) break;

                keyboard.PressCtrlShift();

                WinApiWrapper.GetKeyboardLayoutName(layoutName);
            }
        }
    }

    private void ActivateLayout(string layoutCode)
    {
        var layout = WinApiWrapper.LoadKeyboardLayout(layoutCode, KLF_ACTIVATE);
        
        WinApiWrapper.ActivateKeyboardLayout(layout, KLF_ACTIVATE);
    }

    private void InitLayouts() 
    {
        _keyboardLayouts[Language.English] = "00000409";
        _keyboardLayouts[Language.Russian] = "00000419";
    }
} 