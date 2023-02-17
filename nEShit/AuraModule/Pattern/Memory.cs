using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MemoryStore
{
    public static IntPtr GET_LOCAL_PLAYER = IntPtr.Zero;
    public static IntPtr INVENTORY_ACCESS_FUNCTION = IntPtr.Zero;
    public static IntPtr TARGETING_COLLECTIONS_BASE = IntPtr.Zero;
    public static IntPtr WND_INTERFACE_BASE = IntPtr.Zero;
    public static IntPtr EUDEMON_GETEUDEMON_FUNCTION = IntPtr.Zero;
    public static IntPtr EUDEMON_SENDCOMMAND_FUNCTION = IntPtr.Zero;
    public static IntPtr EUDEMON_SELECT_FUNCTION = IntPtr.Zero;
    public static IntPtr EUDEMON_ISMEDITATING_FUNCTION = IntPtr.Zero;
    public static IntPtr EUDEMON_HASGIFT_FUNCTION = IntPtr.Zero;
    public static IntPtr CURRENT_MAP_BASE = IntPtr.Zero;
    public static IntPtr DETOUR_MAIN_LOOP_OFFSET = IntPtr.Zero;
    public static IntPtr DETOUR_FISHING_CALLBACK = IntPtr.Zero;

    public static IntPtr PLAYER_Resurrection = IntPtr.Zero;
    public static IntPtr PLAYER_DoUIAction = IntPtr.Zero;

    public static IntPtr FISHING_SetNextState = IntPtr.Zero;
    public static IntPtr FISHING_ExitState = IntPtr.Zero;

    public static IntPtr AK_COLLECTION_LIST = IntPtr.Zero;

}
public class Load_Pattern
{
    public static void SetPatterns()
    {
        string procname = "game.bin";
        if (SteamClient)
            procname = "Launcher.exe";

        System.Diagnostics.ProcessModule gameproc = Minimem.FindProcessModule(procname, false);
        MemoryStore.DETOUR_MAIN_LOOP_OFFSET = PatternManager.FindPattern(gameproc, "55 8b ec 83 ec ? 80 3d ? ? ? ? ? 74 ? 8b 0d");
        MemoryStore.DETOUR_FISHING_CALLBACK = PatternManager.FindPattern(gameproc, "55 8b ec 64 a1 ? ? ? ? 6a ? 68 ? ? ? ? 50 64 89 25 ? ? ? ? 81 ec ? ? ? ? 53 56 57 8b 7d ? 85 ff 0f 84 ? ? ? ? 8b 0d");

        MemoryStore.GET_LOCAL_PLAYER = PatternManager.FindPatternAlain(gameproc, "E8 ? ? ? ? 8B F8 0F BF 86 ? ? ? ?", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        MemoryStore.INVENTORY_ACCESS_FUNCTION = PatternManager.FindPattern(gameproc, "55 8B EC 8B 55 08 33 C0 83 FA 0D");
        MemoryStore.TARGETING_COLLECTIONS_BASE = PatternManager.FindPatternAlain(gameproc, "8B 0D ? ? ? ? E8 ? ? ? ? 8B F8 0F BF 86 ? ? ? ?", 1, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES_RAW);
        MemoryStore.WND_INTERFACE_BASE = PatternManager.FindPatternAlain(gameproc, "3b 35 ? ? ? ? 0f 85 ? ? ? ? 5f 5b 5e", 1, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES_RAW);
        MemoryStore.EUDEMON_GETEUDEMON_FUNCTION = PatternManager.FindPatternAlain(gameproc, "E8 ? ? ? ? 85 C0 75 2A 8D 78 03 EB 07", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        MemoryStore.EUDEMON_SENDCOMMAND_FUNCTION = PatternManager.FindPatternAlain(gameproc, "E8 ? ? ? ? 0F B7 87 ? ? ? ? 83 C4 10 8B CF", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        MemoryStore.EUDEMON_SELECT_FUNCTION = PatternManager.FindPatternAlain(gameproc, "E8 ? ? ? ? E9 ? ? ? ? 0F B7 41 08 50", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        // occu 1: EUDEMON_HASGIFT_FUNCTION || occu 2: EUDEMON_ISMEDITATING_FUNCTION: 00 00 7D 29 8B,
        MemoryStore.EUDEMON_HASGIFT_FUNCTION = PatternManager.FindPatternAlain(gameproc, "E8 ? ? ? ? 84 C0 74 10 8B CF", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        MemoryStore.EUDEMON_ISMEDITATING_FUNCTION = PatternManager.FindPatternAlain(gameproc, "E8 ? ? ? ? 84 C0 74 31 56", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);

        MemoryStore.CURRENT_MAP_BASE = PatternManager.FindPatternAlain(gameproc, "8B 0D ? ? ? ? E8 ? ? ? ? A1 ? ? ? ? 85 C0 75 05 E8 ? ? ? ? ", 1, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES_RAW);

        MemoryStore.PLAYER_Resurrection = PatternManager.FindPatternAlain(gameproc, "e8 ? ? ? ? 83 c4 ? 83 7d ? ? 72 ? ff 75 ? e8 ? ? ? ? 83 c4 ? 8b 4d ? b0 ? 64 89 0d ? ? ? ? 8b e5 5d c2 ? ? 8d 49", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        MemoryStore.PLAYER_DoUIAction = PatternManager.FindPatternAlain(gameproc, "E8 ? ? ? ? 83 C4 14 E9 ? ? ? ? 6A 07", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);

        MemoryStore.FISHING_SetNextState = PatternManager.FindPattern(gameproc, "55 8b ec 64 a1 ? ? ? ? 6a ? 68 ? ? ? ? 50 8b 45 ? 64 89 25 ? ? ? ? 83 ec ? 83 78 ? ? 56 8b f1 0f 85 ? ? ? ? 0f b7 86 ? ? ? ? 83 e8");
        MemoryStore.FISHING_ExitState = PatternManager.FindPattern(gameproc, "55 8b ec 6a ? 68 ? ? ? ? 64 a1 ? ? ? ? 50 64 89 25 ? ? ? ? 51 56 8b f1 89 75 ? c7 06 ? ? ? ? ff b6 ? ? ? ? c7 45 ? ? ? ? ? e8 ? ? ? ? 8b 8e ? ? ? ? 83 c4 ? 85 c9 74 ? 8b 01 6a ? ff 10 c7 86 ? ? ? ? ? ? ? ? 8b ce e8");
        //MemoryStore.AK_COLLECTION_LIST = PatternManager.FindPattern(gameproc, "xx");
    }
    public static bool SteamClient = false;
    public static bool RetrieveAddresses(uint gamePID)
    {
        Attach.PID = gamePID;
        if (Attach.PID == 0) return false;
        Attach.OpenProcess();
        SetPatterns();//Load Patterns

#if DEBUG
        Console.WriteLine($"----- Debug Pattern -----");
        Console.WriteLine($"DETOUR_MAIN_LOOP_OFFSET 0x{MemoryStore.DETOUR_MAIN_LOOP_OFFSET.ToString("X")}\n");
        Console.WriteLine($"DETOUR_FISHING_CALLBACK 0x{MemoryStore.DETOUR_FISHING_CALLBACK.ToString("X")}\n");
        Console.WriteLine($"GET_LOCAL_PLAYER 0x{MemoryStore.GET_LOCAL_PLAYER.ToString("X")}\n");
        Console.WriteLine($"INVENTORY_ACCESS_FUNCTION 0x{MemoryStore.INVENTORY_ACCESS_FUNCTION.ToString("X")}\n");
        Console.WriteLine($"TARGETING_COLLECTIONS_BASE 0x{MemoryStore.TARGETING_COLLECTIONS_BASE.ToString("X")}\n");
        Console.WriteLine($"WND_INTERFACE_BASE 0x{MemoryStore.WND_INTERFACE_BASE.ToString("X")}\n");
        Console.WriteLine($"EUDEMON_GETEUDEMON_FUNCTION 0x{MemoryStore.EUDEMON_GETEUDEMON_FUNCTION.ToString("X")}\n");
        Console.WriteLine($"EUDEMON_SENDCOMMAND_FUNCTION 0x{MemoryStore.EUDEMON_SENDCOMMAND_FUNCTION.ToString("X")}\n");
        Console.WriteLine($"EUDEMON_SELECT_FUNCTION 0x{MemoryStore.EUDEMON_SELECT_FUNCTION.ToString("X")}\n");
        Console.WriteLine($"EUDEMON_ISMEDITATING_FUNCTION 0x{MemoryStore.EUDEMON_ISMEDITATING_FUNCTION.ToString("X")}\n");
        Console.WriteLine($"EUDEMON_HASGIFT_FUNCTION 0x{MemoryStore.EUDEMON_HASGIFT_FUNCTION.ToString("X")}\n");
        Console.WriteLine($"CURRENT_MAP_BASE 0x{MemoryStore.CURRENT_MAP_BASE.ToString("X")}\n");
        Console.WriteLine($"PLAYER_Resurrection 0x{MemoryStore.PLAYER_Resurrection.ToString("X")}\n");
        Console.WriteLine($"PLAYER_DoUIAction 0x{MemoryStore.PLAYER_DoUIAction.ToString("X")}\n");
        Console.WriteLine($"FISHING_SetNextState 0x{MemoryStore.FISHING_SetNextState.ToString("X")}\n");
        Console.WriteLine($"FISHING_ExitState 0x{MemoryStore.FISHING_ExitState.ToString("X")}\n");
        Console.WriteLine($"AK_COLLECTION_LIST 0x{MemoryStore.AK_COLLECTION_LIST.ToString("X")}\n");
        Console.WriteLine($"----- End -----");
#endif
        if
            (
            MemoryStore.DETOUR_MAIN_LOOP_OFFSET == IntPtr.Zero ||
            MemoryStore.DETOUR_FISHING_CALLBACK == IntPtr.Zero ||
            MemoryStore.GET_LOCAL_PLAYER == IntPtr.Zero ||
            MemoryStore.INVENTORY_ACCESS_FUNCTION == IntPtr.Zero ||
            MemoryStore.TARGETING_COLLECTIONS_BASE == IntPtr.Zero ||
            MemoryStore.WND_INTERFACE_BASE == IntPtr.Zero ||
            MemoryStore.EUDEMON_GETEUDEMON_FUNCTION == IntPtr.Zero ||
            MemoryStore.EUDEMON_SENDCOMMAND_FUNCTION == IntPtr.Zero ||
            MemoryStore.EUDEMON_SELECT_FUNCTION == IntPtr.Zero ||
            MemoryStore.EUDEMON_ISMEDITATING_FUNCTION == IntPtr.Zero ||
            MemoryStore.EUDEMON_HASGIFT_FUNCTION == IntPtr.Zero ||
            MemoryStore.CURRENT_MAP_BASE == IntPtr.Zero ||

            MemoryStore.PLAYER_DoUIAction == IntPtr.Zero ||
            MemoryStore.PLAYER_Resurrection == IntPtr.Zero ||

            MemoryStore.FISHING_SetNextState == IntPtr.Zero ||
            MemoryStore.FISHING_ExitState == IntPtr.Zero 
            //||
            //MemoryStore.AK_COLLECTION_LIST == IntPtr.Zero
            )
            return true;
        else return false;
    }
}
