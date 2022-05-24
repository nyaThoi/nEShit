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
                nMnemonics.localPlayer.GetLocalPlayer(MemoryStore.TARGETING_COLLECTIONS_BASE, MemoryStore.GET_LOCAL_PLAYER);

            return new Struct.Entity(Memory.Assemble.Execute<IntPtr>(mnemonics, "GetLocalPlayer"));
            //return new Struct.Entity(Memory.Assemble.InjectAndExecute(mnemonics));

        }
        public static Struct.InventoryBag GetInventoryBag(uint bagId, InventoryType inventoryType = InventoryType.IT_BackPack)
        {
            IntPtr thisPtr = Utils.GetInventoryAccessPtr();
            if (thisPtr == IntPtr.Zero) return new Struct.InventoryBag(IntPtr.Zero);

            string[] mnemonics =
                nMnemonics.inventory.GetInventoryBag(thisPtr, MemoryStore.INVENTORY_ACCESS_FUNCTION, (int)bagId, (int)inventoryType);

            return new Struct.InventoryBag(Memory.Assemble.Execute<IntPtr>(mnemonics, "GetInventoryBag"));

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
                    return locPlayer.GetEntityInfo.IsValid && locPlayer.GetModelInfo.IsValid && locPlayer.GetActorInfo.IsValid && locPlayer.GetEntityInfo.charName != "" && locPlayer.GetEntityInfo.charName.Length >= 2 /*Min Charactername Length check*/;
            }
            return false;
        }
        public static IntPtr GetEntityCollectionPtr()
        {
            if (MemoryStore.TARGETING_COLLECTIONS_BASE == IntPtr.Zero)
                return IntPtr.Zero;
            return Memory.Reader.Read<IntPtr>(MemoryStore.TARGETING_COLLECTIONS_BASE);
        }
        public static IntPtr GetInventoryAccessPtr()
        {
            Struct.Entity info = ASM.GetLocalPlayer();
            if (!info.IsValid || !info.GetEntityInfo.IsValid) return IntPtr.Zero;
            IntPtr res = info.GetEntityInfo.inventoryPtr;
            return res;
        }
        public static uint GetInventorySize()
        {
            uint count = 0;
            for (uint i = 0; i <= (uint)InventoryBagType.IBT_MAX; ++i)
            {
                Struct.InventoryBag bag = ASM.GetInventoryBag(i, InventoryType.IT_BackPack);
                if (bag.IsValid)
                {
                    count += bag.GetItemCount();
                }
            }
            return count;
        }
        private static List<Struct.InventoryBag> EquippedBag
        {
            get
            {
                List<Struct.InventoryBag> list = new List<Struct.InventoryBag>();
                for (uint i = 0; i < (uint)InventoryBagType.IBT_MAX; i++)
                {
                    Struct.InventoryBag bag = ASM.GetInventoryBag(i, InventoryType.IT_BackPack);
                    if (bag.IsValid)
                    {
                        list.Add(bag);
                    }
                }
                return list;
            }
        }
        private static uint BagsFreeSlots
        {
            get
            {
                uint num = 0;
                foreach (Struct.InventoryBag akinventoryBag in EquippedBag)
                {
                    num += akinventoryBag.FreeSlots;
                }
                return num;
            }
        }

        public static Dictionary<int, IntPtr> GetAKCollection(/* = TARGETING_COLLECTIONS_BASE*/ Int32 offset)
        {
            if (MemoryStore.AK_COLLECTION_LIST == IntPtr.Zero) return null;
            IntPtr tmp = Memory.Reader.Read<IntPtr>(MemoryStore.AK_COLLECTION_LIST);
            IntPtr intPtr = Memory.Reader.Read<IntPtr>((tmp + offset) + 0x8);
            Dictionary<int, IntPtr> dictionary = new Dictionary<int, IntPtr>();
            List<IntPtr> list = new List<IntPtr>();
            if (intPtr == IntPtr.Zero)
                return dictionary;
            intPtr = Memory.Reader.Read<IntPtr>(intPtr);
            if (intPtr == IntPtr.Zero)
                return dictionary;

            while (intPtr != IntPtr.Zero && !list.Contains(intPtr))
            {
                int key = Memory.Reader.Read<int>(intPtr + 0x8);//container
                IntPtr intPtr2 = Memory.Reader.Read<IntPtr>(intPtr + 0xC); //nbElements

                if (intPtr2 != IntPtr.Zero && !dictionary.ContainsKey(key))
                    dictionary.Add(key, intPtr2);

                list.Add(intPtr);
                intPtr = Memory.Reader.Read<IntPtr>(intPtr);
            }
            return dictionary;
        }
        public static void GetItemData()
        {
            int Count = 0;
            foreach (KeyValuePair<int, IntPtr> keyValuePair in GetAKCollection(0xB38))
            {
                if (keyValuePair.Key != 0 && keyValuePair.Value != IntPtr.Zero)
                    Count++;
            }
        }
    }

}
