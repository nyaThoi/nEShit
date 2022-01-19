using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

class Hotkey
{
    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);
    [DllImport("user32.dll")]
    private static extern int ConsoleGetAsyncKeyState(System.Windows.Forms.Keys vKeys);
    public static bool F10KeyPress()
    {
        bool cstate = false;
        string buffer = string.Empty;

        foreach (System.Int32 i in Enum.GetValues(typeof(System.Windows.Forms.Keys)))
        {
            int x = GetAsyncKeyState(System.Windows.Forms.Keys.F10);
            if ((x == 1) || (x == Int16.MinValue))
            {
                buffer += Enum.GetName(typeof(System.Windows.Forms.Keys), i);
            }
        }
        if (buffer != string.Empty)
            cstate = true;

        return cstate;
    }

}

