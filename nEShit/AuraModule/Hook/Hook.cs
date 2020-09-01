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
        if (!ExecuteCB.InstallHook(MemoryStore.DETOUR_MAIN_LOOP_OFFSET, 6))
        {
            MessageBox.Show("Failed to create and apply JmpHook", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(0);
        }
        if (!ExecuteCB.InstallFishingCallback(MemoryStore.DETOUR_FISHING_CALLBACK, 9))
        {
            MessageBox.Show("Failed to create and apply JmpHook\nFishing_CB", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(0);
        }
    }

    public static void UnsetHook()
    {
        ExecuteCB.Detach();
    }
}

