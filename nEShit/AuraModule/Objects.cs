using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Objects
{
    private enum EntityCollectionType
    {
        ECT_Chara,
        ECT_Effect,
        ECT_Duel
    }
    private static Dictionary<uint, IntPtr> EntityCollection(IntPtr addr)
    {
        IntPtr container = Memory.Reader.Read<IntPtr>(addr + 0x8);
        Dictionary<uint, IntPtr> dictionary = new Dictionary<uint, IntPtr>();
        List<IntPtr> list = new List<IntPtr>();
        container = Memory.Reader.Read<IntPtr>(container);
        if (container == IntPtr.Zero)
            return dictionary;

        while (container != IntPtr.Zero && !list.Contains(container))
        {
            uint key = Memory.Reader.Read<uint>(container + 0x8);//entityID
            IntPtr intPtr2 = Memory.Reader.Read<IntPtr>(container + 0xC); //EntityInfo
            if (intPtr2 != IntPtr.Zero && !dictionary.ContainsKey(key))
            {
                dictionary.Add(key, intPtr2);
            }
            list.Add(container);
            container = Memory.Reader.Read<IntPtr>(container);
        }
        return dictionary;
    }
    private static IntPtr GetEntityCollection(EntityCollectionType type)
    {
        int size;
        switch (type)
        {
            case (EntityCollectionType.ECT_Chara):
                size = 0x0;
                break;
            case (EntityCollectionType.ECT_Effect):
                size = 0x5A;
                break;
            case (EntityCollectionType.ECT_Duel):
                size = 0x90;
                break;
            default:
                size = 0x0;
                break;
        }
        IntPtr tcb = Memory.Reader.Read<IntPtr>(MemoryStore.TARGETING_COLLECTIONS_BASE);
        if (tcb == IntPtr.Zero)
            return IntPtr.Zero;
        else
            return tcb + 0x51C + size;
    }

}