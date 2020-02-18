using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class Hook
{
    public static void SetHook()
    {
        if (!ExecuteCB.InstallHook(MemoryStore.DETOUR_MAIN_LOOP_OFFSET, 7))
        {
            MessageBox.Show("Failed to create and apply JmpHook", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(0);
        }
    }
    public static void UnsetHook()
    {
        ExecuteCB.Detach();
    }
}

