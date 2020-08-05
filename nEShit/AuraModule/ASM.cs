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
                nMnemonics.inventory.GetInventoryBag(thisPtr,MemoryStore.INVENTORY_ACCESS_FUNCTION,(int)bagId,(int)inventoryType);

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
                    return locPlayer.GetEntityInfo.IsValid && locPlayer.GetModelInfo.IsValid && locPlayer.GetActorInfo.IsValid && locPlayer.GetEntityInfo.charName != "";
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
    }

}
