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
        public static Struct.Entity GetLocalPlayer()
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

        }

    }
}
