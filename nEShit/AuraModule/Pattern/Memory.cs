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
}
public class Load_Pattern
{
    public static void SetPatterns()
    {
        System.Diagnostics.ProcessModule gameproc = Minimem.FindProcessModule("game.bin", false);
        MemoryStore.DETOUR_MAIN_LOOP_OFFSET = PatternManager.FindPattern(gameproc, "55 8b ec 83 ec ? 80 3d ? ? ? ? ? 74 ? 8b 0d");
        //Add the Fishing CB Pattern here
        MemoryStore.DETOUR_FISHING_CALLBACK = IntPtr.Zero;

        MemoryStore.GET_LOCAL_PLAYER = PatternManager.FindPatternAlain(gameproc, "E8 ? ? ? ? 8B F8 0F BF 86 ? ? ? ?", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        MemoryStore.INVENTORY_ACCESS_FUNCTION = PatternManager.FindPattern(gameproc, "55 8B EC 8B 55 08 33 C0 83 FA 0D");
        MemoryStore.TARGETING_COLLECTIONS_BASE = PatternManager.FindPatternAlain(gameproc, "8B 0D ? ? ? ? E8 ? ? ? ? 8B F8 0F BF 86 ? ? ? ?", 1, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES_RAW);
        MemoryStore.WND_INTERFACE_BASE = PatternManager.FindPatternAlain(gameproc, "3b 35 ? ? ? ? 0f 85 ? ? ? ? 5f 5b 5e", 1, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES_RAW);
        MemoryStore.EUDEMON_GETEUDEMON_FUNCTION = PatternManager.FindPatternAlain(gameproc, "e8 ? ? ? ? 85 c0 74 ? 8b c8 e8 ? ? ? ? 89 45 ? eb ? c7 45", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        MemoryStore.EUDEMON_SENDCOMMAND_FUNCTION = PatternManager.FindPatternAlain(gameproc, "e8 ? ? ? ? 83 c4 ? 5e c3 cc cc 55 ", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        MemoryStore.EUDEMON_SELECT_FUNCTION = PatternManager.FindPatternAlain(gameproc, "E8 ? ? ? ? 6A 01 8B CF E8 ? ? ? ? 8B CF E8 ? ? ? ? 8B CF 5F 5B E9 ? ? ? ?", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        // occu 1: EUDEMON_HASGIFT_FUNCTION || occu 2: EUDEMON_ISMEDITATING_FUNCTION: 00 00 7D 29 8B,
        MemoryStore.EUDEMON_HASGIFT_FUNCTION = PatternManager.FindPatternAlain(gameproc, "55 8b ec 66 8b 45 ? 66 3b 81 ? ? ? ? 7d ? 8b 91 ? ? ? ? 0f bf ? 53 8b c1 bb ? ? ? ? c1 e8 ? 83 e1 ? d3 e3 23 1c ? f7 db 1b db f7 db 8a c3 5b 5d c2 ? ? 32 c0 5d c2 ? ? cc 55", 0, 1, PatternManager.MemoryType.RT_ADDRESS);
        MemoryStore.EUDEMON_ISMEDITATING_FUNCTION = PatternManager.FindPatternAlain(gameproc, "55 8b ec 66 8b 45 ? 66 3b 81 ? ? ? ? 7d ? 8b 91 ? ? ? ? 0f bf ? 53 8b c1 bb ? ? ? ? c1 e8 ? 83 e1 ? d3 e3 23 1c ? f7 db 1b db f7 db 8a c3 5b 5d c2 ? ? 32 c0 5d c2 ? ? cc 8b 81", 0, 1, PatternManager.MemoryType.RT_ADDRESS);
        MemoryStore.CURRENT_MAP_BASE = PatternManager.FindPatternAlain(gameproc, "8B 0D ? ? ? ? E8 ? ? ? ? A1 ? ? ? ? 85 C0 75 05 E8 ? ? ? ? ", 1, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES_RAW);
        
        MemoryStore.PLAYER_Resurrection = PatternManager.FindPatternAlain(gameproc, "e8 ? ? ? ? 83 c4 ? 83 7d ? ? 72 ? ff 75 ? e8 ? ? ? ? 83 c4 ? 8b 4d ? b0 ? 64 89 0d ? ? ? ? 8b e5 5d c2 ? ? 8d 49", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        MemoryStore.PLAYER_DoUIAction = PatternManager.FindPatternAlain(gameproc, "e8 ? ? ? ? 83 c4 ? b0 ? c3 cc cc cc cc 55 8b ec 56", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);

        MemoryStore.FISHING_SetNextState = PatternManager.FindPattern(gameproc, "55 8b ec 64 a1 ? ? ? ? 6a ? 68 ? ? ? ? 50 8b 45 ? 64 89 25 ? ? ? ? 83 ec ? 83 78 ? ? 56 8b f1 0f 85 ? ? ? ? 0f b7 86 ? ? ? ? 83 e8");
        MemoryStore.FISHING_ExitState = PatternManager.FindPattern(gameproc, "55 8b ec 6a ? 68 ? ? ? ? 64 a1 ? ? ? ? 50 64 89 25 ? ? ? ? 51 56 8b f1 89 75 ? c7 06 ? ? ? ? ff b6 ? ? ? ? c7 45 ? ? ? ? ? e8 ? ? ? ? 8b 8e ? ? ? ? 83 c4 ? 85 c9 74 ? 8b 01 6a ? ff 10 c7 86 ? ? ? ? ? ? ? ? 8b ce e8");
    }

    public static bool RetrieveAddresses(uint gamePID)
    {
        Attach.PID = gamePID;
        if (Attach.PID == 0) return false;
        Attach.OpenProcess();
        SetPatterns();//Load Patterns
#if DEBUG
        Debug.WriteLine($"----- Debug Pattern -----");
        Debug.WriteLine($"DETOUR_MAIN_LOOP_OFFSET 0x{MemoryStore.DETOUR_MAIN_LOOP_OFFSET.ToString("X")}\n");
        Debug.WriteLine($"GET_LOCAL_PLAYER 0x{MemoryStore.GET_LOCAL_PLAYER.ToString("X")}\n");
        Debug.WriteLine($"INVENTORY_ACCESS_FUNCTION 0x{MemoryStore.INVENTORY_ACCESS_FUNCTION.ToString("X")}\n");
        Debug.WriteLine($"TARGETING_COLLECTIONS_BASE 0x{MemoryStore.TARGETING_COLLECTIONS_BASE.ToString("X")}\n");
        Debug.WriteLine($"WND_INTERFACE_BASE 0x{MemoryStore.WND_INTERFACE_BASE.ToString("X")}\n");
        Debug.WriteLine($"EUDEMON_GETEUDEMON_FUNCTION 0x{MemoryStore.EUDEMON_GETEUDEMON_FUNCTION.ToString("X")}\n");
        Debug.WriteLine($"EUDEMON_SENDCOMMAND_FUNCTION 0x{MemoryStore.EUDEMON_SENDCOMMAND_FUNCTION.ToString("X")}\n");
        Debug.WriteLine($"EUDEMON_SELECT_FUNCTION 0x{MemoryStore.EUDEMON_SELECT_FUNCTION.ToString("X")}\n");
        Debug.WriteLine($"EUDEMON_ISMEDITATING_FUNCTION 0x{MemoryStore.EUDEMON_ISMEDITATING_FUNCTION.ToString("X")}\n");
        Debug.WriteLine($"EUDEMON_HASGIFT_FUNCTION 0x{MemoryStore.EUDEMON_HASGIFT_FUNCTION.ToString("X")}\n");
        Debug.WriteLine($"CURRENT_MAP_BASE 0x{MemoryStore.CURRENT_MAP_BASE.ToString("X")}\n");
        Debug.WriteLine($"----- End -----");
#endif
        if
            (
            MemoryStore.DETOUR_MAIN_LOOP_OFFSET == IntPtr.Zero ||
            //MemoryStore.DETOUR_FISHING_CALLBACK == IntPtr.Zero ||
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
            MemoryStore.PLAYER_Resurrection == IntPtr.Zero //||

            //MemoryStore.FISHING_SetNextState == IntPtr.Zero            

            )
            return true;
        else return false;
    }
}
