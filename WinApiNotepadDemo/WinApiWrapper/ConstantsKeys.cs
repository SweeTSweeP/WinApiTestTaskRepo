namespace WinApiNotepadDemo.WinApiWrapper
{
    public static class ConstantsKeys
    {
        public const int VK_TAB = 9;
        public const int VK_RETURN = 13;
        public const int VK_MENU = 18;
        public const int VK_ESCAPE = 27;
        public const int VK_SPACE = 32;
        public const int VK_SNAPSHOT = 44;
        public const int VK_DECIMAL = 110;
        public const int VK_F4 = 115;
        public const int VK_LSHIFT = 160;
        public const int VK_LCONTROL = 162;

        public static readonly Dictionary<char, (byte code, bool isShift)> SpecialSymbols = new()
        {
            { '!', (0x31, true) }, { '@', (0x32, true) }, { '#', (0x33, true) }, { '$', (0x34, true) }, 
            { '%', (0x35, true) }, { '^', (0x36, true) }, { '&', (0x37, true) }, { '*', (0x38, true) }, 
            { '(', (0x39, true) }, { ')', (0x30, true) }, { '_', (0xBD, true) }, { '-', (0xBD, false) },
            { '+', (0xBB, true) }, { '=', (0xBB, false) }, { '{', (0xDB, true) }, { '}', (0xDD, true) }, 
            { '[', (0xDB, true) }, { ']', (0xDD, true) }, { ':', (0xBA, true) }, { ';', (0xBA, false) }, 
            { '"', (0xDE, true) }, { '\'', (0xDE, false) }, { '<', (0xBC, true) }, { ',', (0xBC, false) }, 
            { '>', (0xBE, true) }, { '.', (0xBE, false) }, { '/', (0xBF, false) }, { '?', (0x3F, true) },
            { '\\', (0xDC, false) }, { '|', (0x7C, true)  }, { '`', (0x60, false)  }
        };

        public static readonly Dictionary<char, char> RussianSymbols = new()
        {
            { 'й', 'q' }, { 'ё', '`' }, { 'ц', 'w' }, { 'у', 'e' }, { 'к', 'r' }, { 'е', 't' }, { 'н', 'y' }, 
            { 'г', 'u' }, { 'ш', 'i' }, { 'щ', 'o' }, { 'з', 'p' }, { 'х', '[' }, { 'ъ', ']' }, { 'ф', 'a' }, 
            { 'ы', 's' }, { 'в', 'd' }, { 'а', 'f' }, { 'п', 'g' }, { 'р', 'h' }, { 'о', 'j' }, { 'л', 'k' },
            { 'д', 'l' }, { 'ж', ';' }, { 'э', '\'' }, { 'я', 'z' }, { 'ч', 'x' }, { 'с', 'c' }, { 'м', 'v' },
            { 'и', 'b' }, { 'т', 'n' }, { 'ь', 'm' }, { 'б', ',' }, { 'ю', '.' },
        };
    }
}
