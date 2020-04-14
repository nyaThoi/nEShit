using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraModule
{
    public class ASM
    {
        public static Struct.Entity GetLocalPlayer(/* = TARGETING_COLLECTIONS_BASE*/ /* = GET_LOCAL_PLAYER*/)
        {
            if (MemoryStore.GET_LOCAL_PLAYER == IntPtr.Zero || MemoryStore.TARGETING_COLLECTIONS_BASE == IntPtr.Zero) 
                return new Struct.Entity(IntPtr.Zero);

            string[] mnemonics =
    {
                "use32",
                $"mov eax,{MemoryStore.TARGETING_COLLECTIONS_BASE}",
                "mov ecx, [ds:eax]",
                "test ecx,ecx",
                "jz .Finnish",
                $"call {MemoryStore.GET_LOCAL_PLAYER}",
                ".Finnish:",
                "retn",
            };

            return new Struct.Entity(Memory.Assemble.Execute<IntPtr>(mnemonics, "GetLocalPlayer"));
            //return new Struct.Entity(Memory.Assemble.InjectAndExecute(mnemonics));

        }
    }
    public class Utils
    {
        public static Struct.Entity locPlayer;
        private static Struct.CurrentMap GetCurrentMap(/* = CURRENT_MAP_BASE*/)
        {
            if (MemoryStore.CURRENT_MAP_BASE == IntPtr.Zero)
                return new Struct.CurrentMap(IntPtr.Zero);
            IntPtr cw = Memory.Reader.Read<IntPtr>(MemoryStore.CURRENT_MAP_BASE);
            return new Struct.CurrentMap(cw);
        }
        public static bool IsInGame()
        {
            Struct.CurrentMap curMap = GetCurrentMap();
            if (curMap.IsValid && curMap.mapID > 0 && curMap.mapID != 0x63/*char selection*/)
            {
                locPlayer = ASM.GetLocalPlayer();
                if (locPlayer.IsValid)
                    return locPlayer.GetEntityInfo.IsValid && locPlayer.GetModelInfo.IsValid && locPlayer.GetActorInfo.IsValid && locPlayer.GetEntityInfo.charName != "";
            }
            return false;
        }

    }
}
