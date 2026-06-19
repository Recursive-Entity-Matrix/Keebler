namespace Keebler.Models;

// Enum copies for better UX
// Taken from FFXIVClientStructs/FFXIV/Client/System/Input/InputData.cs

public enum VirtualKeyType
{
    Gamepad = 0,
    Keyboard = 1,
    Unknown = 255
}

public enum KeyboardVirtualKey
{
    /// <summary>
    /// This is an addendum to use on functions in which you have to pass a zero value to represent no key code.
    /// </summary>
    NO_KEY = 0,

    /// <summary>Left mouse button.</summary>
    LBUTTON = 1,

    /// <summary>Right mouse button.</summary>
    RBUTTON = 2,

    /// <summary>Control-break processing.</summary>
    CANCEL = 3,

    /// <summary>Middle mouse button (three-button mouse).</summary>
    /// <remarks>NOT contiguous with L and R buttons.</remarks>
    MBUTTON = 4,

    /// <summary>X1 mouse button.</summary>
    /// <remarks>NOT contiguous with L and R buttons.</remarks>
    XBUTTON1 = 5,

    /// <summary>X2 mouse button.</summary>
    /// <remarks>NOT contiguous with L and R buttons.</remarks>
    XBUTTON2 = 6,

    /// <summary>BACKSPACE key.</summary>
    BACK = 8,

    /// <summary>TAB key.</summary>
    TAB = 9,

    /// <summary>CLEAR key.</summary>
    CLEAR = 12, // 0x0C

    /// <summary>RETURN key.</summary>
    RETURN = 13, // 0x0D

    /// <summary>SHIFT key.</summary>
    SHIFT = 16, // 0x10

    /// <summary>CONTROL key.</summary>
    CONTROL = 17, // 0x11

    /// <summary>ALT key.</summary>
    MENU = 18, // 0x12

    /// <summary>PAUSE key.</summary>
    PAUSE = 19, // 0x13

    /// <summary>CAPS LOCK key.</summary>
    CAPITAL = 20, // 0x14

    /// <summary>
    /// IME Hangeul mode (maintained for compatibility; use User32.VirtualKey.HANGUL).
    /// </summary>
    HANGEUL = 21, // 0x15

    /// <summary>IME Hangul mode.</summary>
    HANGUL = 21, // 0x15

    /// <summary>IME Kana mode.</summary>
    KANA = 21, // 0x15

    /// <summary>IME Junja mode.</summary>
    JUNJA = 23, // 0x17

    /// <summary>IME final mode.</summary>
    FINAL = 24, // 0x18

    /// <summary>IME Hanja mode.</summary>
    HANJA = 25, // 0x19

    /// <summary>IME Kanji mode.</summary>
    KANJI = 25, // 0x19

    /// <summary>ESC key.</summary>
    ESCAPE = 27, // 0x1B

    /// <summary>IME convert.</summary>
    CONVERT = 28, // 0x1C

    /// <summary>IME nonconvert.</summary>
    NONCONVERT = 29, // 0x1D

    /// <summary>IME accept.</summary>
    ACCEPT = 30, // 0x1E

    /// <summary>IME mode change request.</summary>
    MODECHANGE = 31, // 0x1F

    /// <summary>SPACEBAR.</summary>
    SPACE = 32, // 0x20

    /// <summary>PAGE UP key.</summary>
    PRIOR = 33, // 0x21

    /// <summary>PAGE DOWN key.</summary>
    NEXT = 34, // 0x22

    /// <summary>END key.</summary>
    END = 35, // 0x23

    /// <summary>HOME key.</summary>
    HOME = 36, // 0x24

    /// <summary>LEFT ARROW key.</summary>
    LEFT = 37, // 0x25

    /// <summary>UP ARROW key.</summary>
    UP = 38, // 0x26

    /// <summary>RIGHT ARROW key.</summary>
    RIGHT = 39, // 0x27

    /// <summary>DOWN ARROW key.</summary>
    DOWN = 40, // 0x28

    /// <summary>SELECT key.</summary>
    SELECT = 41, // 0x29

    /// <summary>PRINT key.</summary>
    PRINT = 42, // 0x2A

    /// <summary>EXECUTE key.</summary>
    EXECUTE = 43, // 0x2B

    /// <summary>PRINT SCREEN key.</summary>
    SNAPSHOT = 44, // 0x2C

    /// <summary>INS key.</summary>
    INSERT = 45, // 0x2D

    /// <summary>DEL key.</summary>
    DELETE = 46, // 0x2E

    /// <summary>HELP key.</summary>
    HELP = 47, // 0x2F

    /// <summary>0 key.</summary>
    KEY_0 = 48, // 0x30

    /// <summary>1 key.</summary>
    KEY_1 = 49, // 0x31

    /// <summary>2 key.</summary>
    KEY_2 = 50, // 0x32

    /// <summary>3 key.</summary>
    KEY_3 = 51, // 0x33

    /// <summary>4 key.</summary>
    KEY_4 = 52, // 0x34

    /// <summary>5 key.</summary>
    KEY_5 = 53, // 0x35

    /// <summary>6 key.</summary>
    KEY_6 = 54, // 0x36

    /// <summary>7 key.</summary>
    KEY_7 = 55, // 0x37

    /// <summary>8 key.</summary>
    KEY_8 = 56, // 0x38

    /// <summary>9 key.</summary>
    KEY_9 = 57, // 0x39

    /// <summary>A key.</summary>
    A = 65, // 0x41

    /// <summary>B key.</summary>
    B = 66, // 0x42

    /// <summary>C key.</summary>
    C = 67, // 0x43

    /// <summary>D key.</summary>
    D = 68, // 0x44

    /// <summary>E key.</summary>
    E = 69, // 0x45

    /// <summary>F key.</summary>
    F = 70, // 0x46

    /// <summary>G key.</summary>
    G = 71, // 0x47

    /// <summary>H key.</summary>
    H = 72, // 0x48

    /// <summary>I key.</summary>
    I = 73, // 0x49

    /// <summary>J key.</summary>
    J = 74, // 0x4A

    /// <summary>K key.</summary>
    K = 75, // 0x4B

    /// <summary>L key.</summary>
    L = 76, // 0x4C

    /// <summary>M key.</summary>
    M = 77, // 0x4D

    /// <summary>N key.</summary>
    N = 78, // 0x4E

    /// <summary>O key.</summary>
    O = 79, // 0x4F

    /// <summary>P key.</summary>
    P = 80, // 0x50

    /// <summary>Q key.</summary>
    Q = 81, // 0x51

    /// <summary>R key.</summary>
    R = 82, // 0x52

    /// <summary>S key.</summary>
    S = 83, // 0x53

    /// <summary>T key.</summary>
    T = 84, // 0x54

    /// <summary>U key.</summary>
    U = 85, // 0x55

    /// <summary>V key.</summary>
    V = 86, // 0x56

    /// <summary>W key.</summary>
    W = 87, // 0x57

    /// <summary>X key.</summary>
    X = 88, // 0x58

    /// <summary>Y key.</summary>
    Y = 89, // 0x59

    /// <summary>Z key.</summary>
    Z = 90, // 0x5A

    /// <summary>Left Windows key (Natural keyboard).</summary>
    LWIN = 91, // 0x5B

    /// <summary>Right Windows key (Natural keyboard).</summary>
    RWIN = 92, // 0x5C

    /// <summary>Applications key (Natural keyboard).</summary>
    APPS = 93, // 0x5D

    /// <summary>Computer Sleep key.</summary>
    SLEEP = 95, // 0x5F

    /// <summary>Numeric keypad 0 key.</summary>
    NUMPAD0 = 96, // 0x60

    /// <summary>Numeric keypad 1 key.</summary>
    NUMPAD1 = 97, // 0x61

    /// <summary>Numeric keypad 2 key.</summary>
    NUMPAD2 = 98, // 0x62

    /// <summary>Numeric keypad 3 key.</summary>
    NUMPAD3 = 99, // 0x63

    /// <summary>Numeric keypad 4 key.</summary>
    NUMPAD4 = 100, // 0x64

    /// <summary>Numeric keypad 5 key.</summary>
    NUMPAD5 = 101, // 0x65

    /// <summary>Numeric keypad 6 key.</summary>
    NUMPAD6 = 102, // 0x66

    /// <summary>Numeric keypad 7 key.</summary>
    NUMPAD7 = 103, // 0x67

    /// <summary>Numeric keypad 8 key.</summary>
    NUMPAD8 = 104, // 0x68

    /// <summary>Numeric keypad 9 key.</summary>
    NUMPAD9 = 105, // 0x69

    /// <summary>Multiply key.</summary>
    MULTIPLY = 106, // 0x6A

    /// <summary>Add key.</summary>
    ADD = 107, // 0x6B

    /// <summary>Separator key.</summary>
    SEPARATOR = 108, // 0x6C

    /// <summary>Subtract key.</summary>
    SUBTRACT = 109, // 0x6D

    /// <summary>Decimal key.</summary>
    DECIMAL = 110, // 0x6E

    /// <summary>Divide key.</summary>
    DIVIDE = 111, // 0x6F

    /// <summary>F1 Key.</summary>
    F1 = 112, // 0x70

    /// <summary>F2 Key.</summary>
    F2 = 113, // 0x71

    /// <summary>F3 Key.</summary>
    F3 = 114, // 0x72

    /// <summary>F4 Key.</summary>
    F4 = 115, // 0x73

    /// <summary>F5 Key.</summary>
    F5 = 116, // 0x74

    /// <summary>F6 Key.</summary>
    F6 = 117, // 0x75

    /// <summary>F7 Key.</summary>
    F7 = 118, // 0x76

    /// <summary>F8 Key.</summary>
    F8 = 119, // 0x77

    /// <summary>F9 Key.</summary>
    F9 = 120, // 0x78

    /// <summary>F10 Key.</summary>
    F10 = 121, // 0x79

    /// <summary>F11 Key.</summary>
    F11 = 122, // 0x7A

    /// <summary>F12 Key.</summary>
    F12 = 123, // 0x7B

    /// <summary>F13 Key.</summary>
    F13 = 124, // 0x7C

    /// <summary>F14 Key.</summary>
    F14 = 125, // 0x7D

    /// <summary>F15 Key.</summary>
    F15 = 126, // 0x7E

    /// <summary>F16 Key.</summary>
    F16 = 127, // 0x7F

    /// <summary>NUM LOCK key.</summary>
    NUMLOCK = 128, // 0x80

    /// <summary>SCROLL LOCK key.</summary>
    SCROLL = 129, // 0x81

    /// <summary>F19 Key.</summary>
    F19 = 130, // 0x82

    /// <summary>F20 Key.</summary>
    F20 = 131, // 0x83

    /// <summary>F21 Key.</summary>
    F21 = 132, // 0x84

    /// <summary>F22 Key.</summary>
    F22 = 133, // 0x85

    /// <summary>F23 Key.</summary>
    F23 = 134, // 0x86

    /// <summary>F24 Key.</summary>
    F24 = 135, // 0x87
}

public enum GamepadVirtualKey
{
    PAD_NONE = 0,
    PAD_LMB = 160, // 0xA0
    PAD_MMB = 161, // 0xA1
    PAD_RMB = 162, // 0xA2
    PAD_MB4 = 163, // 0xA3
    PAD_MB5 = 164, // 0xA4
    PAD_MB6 = 165, // 0xA5
    PAD_MB7 = 166, // 0xA6
    PAD_UP = 167, // 0xA7
    PAD_DOWN = 168, // 0xA8
    PAD_LEFT = 169, // 0xA9
    PAD_RIGHT = 170, // 0xAA
    PAD_Y = 171, // 0xAB
    PAD_A = 172, // 0xAC
    PAD_X = 173, // 0xAD
    PAD_B = 174, // 0xAE
    PAD_LB = 175, // 0xAF
    PAD_LT = 176, // 0xB0
    PAD_LS = 177, // 0xB1
    PAD_RB = 178, // 0xB2
    PAD_RT = 179, // 0xB3
    PAD_RS = 180, // 0xB4
    PAD_Select = 181, // 0xB5
    PAD_Start = 182, // 0xB6
}