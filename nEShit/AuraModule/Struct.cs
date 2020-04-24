using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Struct
{
    #region Entity
    public class Entity
    {
        public Entity(IntPtr intPtr)
        {
            Pointer = intPtr;
        }
        public IntPtr Pointer { get; set; }
        public bool IsValid
        {
            get
            {
                return Pointer != IntPtr.Zero;
            }
        }
        public uint entityID
        {
            get
            {
                return Memory.Reader.Read<uint>(Pointer + 0x8);
            }
        }
        public EntityInfo GetEntityInfo
        {
            get
            {
                return new EntityInfo(Memory.Reader.Read<IntPtr>(Pointer + 0xC));
            }
        }
        public ModelInfo GetModelInfo
        {
            get
            {
                return new ModelInfo(Memory.Reader.Read<IntPtr>(Pointer + 0x10));
            }
        }
        public ActorInfo GetActorInfo
        {
            get
            {
                return new ActorInfo(Memory.Reader.Read<IntPtr>(Pointer + 0x30));
            }
        }

        public override string ToString()
        {
            return string.Concat(new string[]
            {
                "Entity Ptr: 0x",
                Pointer.ToString("X"),
                Environment.NewLine,
                "EntityInfo Ptr: 0x",
                GetEntityInfo.Pointer.ToString("X"),
                Environment.NewLine,
            });
        }

        #region ASM
        private IntPtr Resurrection(int ReviveType)
        {

            string[] mnemonics =
                nMnemonics.localPlayer.Resurrection(ReviveType, MemoryStore.PLAYER_Resurrection);

            return Memory.Assemble.Execute<IntPtr>(mnemonics, "Revive");
            //return Memory.Assemble.InjectAndExecute(mnemonics);

        }
        public void FullResurrection()
        {
            if (IsValid && GetEntityInfo.IsValid && GetEntityInfo.currentHP == 0)
                Resurrection(0);
        }
        private IntPtr DoUIAction(int slotType)
        {
            string[] mnemonics =
                nMnemonics.localPlayer.DoUIAction(slotType, MemoryStore.PLAYER_DoUIAction);

            return Memory.Assemble.Execute<IntPtr>(mnemonics, "DoUIAction");
            //return Memory.Assemble.InjectAndExecute(mnemonics);

        }
        public void TeleportInterface()
        {
            DoUIAction(0x11);
        }
        #endregion

        public float SetMovementValue(float val = 20)
        {
            if (IsValid && GetEntityInfo.IsValid && GetEntityInfo.MovementValue != 0)
            {
                if (GetEntityInfo.MovementValue < val)
                    GetEntityInfo.MovementValue = val;

                if (GetModelInfo.IsMounted)
                    GetEntityInfo.MovementValue = val + 10;
            }
            return GetEntityInfo.MovementValue;
        }
    }
    public class EntityInfo
    {
        public EntityInfo(IntPtr intPtr)
        {
            Pointer = intPtr;
        }
        public IntPtr Pointer { get; set; }
        public bool IsValid
        {
            get
            {
                return Pointer != IntPtr.Zero;
            }
        }
        public uint currentHP
        {
            get
            {
                return Memory.Reader.Read<uint>(Pointer + 0x8);
            }
        }
        public uint level
        {
            get
            {
                return Memory.Reader.Read<uint>(Pointer + 0x10);
            }
        }
        public float MovementValue
        {
            get
            {
                return Memory.Reader.Read<float>(Pointer + 0x14);
            }
            set
            {
                Memory.Writer.Write<float>(Pointer + 0x14, value);
            }


        }
        public string charName
        {
            get
            {
                return Memory.Reader.ReadString(Pointer + 0x12C, Encoding.UTF7);
            } 
        }
        public uint fishingDurability
        {
            get
            {
                return Memory.Reader.Read<uint>(Pointer + 0xEC);
            }
        }
        public IntPtr inventoryPtr
        {
            get
            {
                return Memory.Reader.Read<IntPtr>(Pointer + 0x540);
            }
        }

    }
    public class ModelInfo
    {
        public ModelInfo(IntPtr intPtr)
        {
            Pointer = intPtr;
        }
        public IntPtr Pointer { get; set; }
        public bool IsValid
        {
            get
            {
                return Pointer != IntPtr.Zero;
            }
        }
        public bool IsMounted
        {
            get
            {
                return Memory.Reader.Read<uint>(Pointer + 0x1F4) != 0u;
            }
        }

    }
    public class ActorInfo
    {
        public ActorInfo(IntPtr intPtr)
        {
            Pointer = intPtr;
        }
        public IntPtr Pointer { get; set; }
        public bool IsValid
        {
            get
            {
                return Pointer != IntPtr.Zero;
            }
        }
    }
    #endregion
    public class CurrentMap
    {
        public CurrentMap(IntPtr intPtr)
        {
            Pointer = intPtr;
        }
        public IntPtr Pointer { get; set; }
        public bool IsValid
        {
            get
            {
                return Pointer != IntPtr.Zero;
            }
        }
        public short mapID
        {
            get
            {
                return Memory.Reader.Read<short>(Pointer + 0x8);
            }
        }

    }
    public class Eudemon
    {
        public Eudemon(IntPtr intPtr)
        {
            Pointer = intPtr;
        }
        public IntPtr Pointer { get; set; }
        public bool IsValid
        {
            get
            {
                return Pointer != IntPtr.Zero;
            }
        }
        public uint currentPM
        {
            get
            {
                return Memory.Reader.Read<uint>(Pointer + 0x124);
            }
        }
        public short chatAttempts
        {
            get
            {
                return Memory.Reader.Read<short>(Pointer + 0x128);
            }
        }

    }

    #region Inventory
    public class InventoryBag
    {
        public InventoryBag(IntPtr intPtr)
        {
            Pointer = intPtr;
        }
        public IntPtr Pointer { get; set; }
        public bool IsValid
        {
            get
            {
                return Pointer != IntPtr.Zero;
            }
        }
        private uint bagID
        {
            get
            {
                return Memory.Reader.Read<uint>(Pointer + 0x4);
            }
        }
        
        private InventoryItem begin
        {
            get
            {
                return new InventoryItem(Memory.Reader.Read<IntPtr>(Pointer + 0x8));
            }
        }
        private InventoryItem end
        {
            get
            {
                return new InventoryItem(Memory.Reader.Read<IntPtr>(Pointer + 0xC));
            }
        }

        private uint Begin
        {
            get
            {
                return Memory.Reader.Read<uint>(Pointer + 0x8);
            }
        }
        private uint End
        {
            get
            {
                return Memory.Reader.Read<uint>(Pointer + 0xC);
            }
        }

        public uint GetItemCount()
        {
            return (((uint)end.Pointer - (uint)begin.Pointer) >> 2);
        }
        public InventoryItem GetItem(uint index)
        {
            return (index < GetItemCount()) && begin.itemID == index ? begin : null;
        }

        public uint NumSlots
        {
            get
            {
                return ((End - Begin) / 4u);
            }
        }
        public uint UsedSlots
        {
            get
            {
                uint num = 0;
                IntPtr pointer = new IntPtr(Begin);
                uint num2 = 0;
                while (num2 < NumSlots && num2 <= 400)// IBT_MAX = 20 x20
                {
                    IntPtr intPtr = Memory.Reader.Read<IntPtr>(pointer + (int)num2 * 4);
                    if (intPtr != IntPtr.Zero && Memory.Reader.Read<IntPtr>(intPtr + 0) != IntPtr.Zero)
                    {
                        num++;
                    }
                    num2++;
                }
                return num;
            }
        }
        public uint FreeSlots
        {
            get
            {
                return NumSlots - UsedSlots;
            }
        }
    }
    public class InventoryItem
    {
        public InventoryItem(IntPtr intPtr)
        {
            Pointer = intPtr;
        }
        public IntPtr Pointer { get; set; }
        public bool IsValid
        {
            get
            {
                return Pointer != IntPtr.Zero;
            }
        }        
        public uint itemID
        {
            get
            {
                return Memory.Reader.Read<uint>(Pointer);
            }
        }
        public bool IsFilled
        {
            get
            {
                return itemID != 0;
            }
        }
    }
    #endregion

    #region WindowManager Struct
    public class EudemonExtendWindow
    {
        public EudemonExtendWindow(IntPtr intPtr)
        {
            WndPointer = intPtr;
        }
        public IntPtr WndPointer { get; set; }
        private bool IsValid
        {
            get
            {
                return WndPointer != IntPtr.Zero;
            }
        }

        private readonly int[] bestMessageIDs = new int[]
{
            0x36,
            0xd8,
            0xd9
};
        private enum EudemonAction
        {
            EA_TALK = 1,
            EA_MEDITATION = 2,
            EA_RETRIEVE = 4
        };
        private Struct.Eudemon GetEudemonBySlot(int slotID)
        {
            if (slotID < 0 || slotID > 3) 
                return new Struct.Eudemon(IntPtr.Zero);
            string[] mnemonics =
                nMnemonics.eudemon.GetEudemonBySlot(MemoryStore.TARGETING_COLLECTIONS_BASE, MemoryStore.GET_LOCAL_PLAYER, slotID, MemoryStore.EUDEMON_GETEUDEMON_FUNCTION);

            return new Struct.Eudemon(Memory.Assemble.Execute<IntPtr>(mnemonics, "Eudemon - GetEudemonBySlot"));
            //return new Struct.Eudemon(Memory.Assemble.InjectAndExecute(mnemonics));
        }
        private bool TryEudemonAction(int slotID, EudemonAction action)
        {
            int arg1 = 0;
            Random random = new Random();
            Struct.Eudemon eudemon = GetEudemonBySlot(slotID);
            if (eudemon.Pointer == IntPtr.Zero) return false;
            switch (action)
            {
                case EudemonAction.EA_TALK:
                    if (eudemon.chatAttempts == 0 /*|| eudemon.chatAttempts == 99*/)
                        return false;
                    //		LogMessage(Eidolons, StringFormat("Talking to eidolon at slot %d.", slotID + 1));
                    Console.WriteLine($"[EAL]: Talking to eidolon at slot {slotID + 1}");
                    arg1 = bestMessageIDs[random.Next(bestMessageIDs.Length)];
                    break;
                case EudemonAction.EA_MEDITATION:
                    if (eudemon.currentPM < 10)
                        return false;
                    //		LogMessage(Eidolons, StringFormat("Linking eidolon at slot %d.", slotID + 1));
                    Console.WriteLine($"[EAL]: Linking eidolon at slot {slotID + 1}");
                    break;
                case EudemonAction.EA_RETRIEVE:
                    //		LogMessage(Eidolons, StringFormat("Retrieving object from eidolon at slot %d.", slotID + 1));
                    Console.WriteLine($"[EAL]: Retrieving object from eidolon at slot {slotID + 1}");
                    break;
            }
            string[] mnemonics =
                nMnemonics.eudemon.TryEudemonAction(eudemon.Pointer, (int)action, arg1, MemoryStore.EUDEMON_SENDCOMMAND_FUNCTION);

            Memory.Assemble.Execute<IntPtr>(mnemonics, "Eudemon - TryEudemonAction");
            //Memory.Assemble.InjectAndExecute(mnemonics);
            return true;
        }
        private bool IsEudemonMeditating(int slotID)
        {
            int rt = 0;
            string[] mnemonics =
                nMnemonics.eudemon.IsEudemonMeditating(MemoryStore.TARGETING_COLLECTIONS_BASE, MemoryStore.GET_LOCAL_PLAYER, slotID, MemoryStore.EUDEMON_ISMEDITATING_FUNCTION);

            rt = Memory.Assemble.Execute<int>(mnemonics, "Eudemon - IsEudemonMeditating");
            //rt = (int)Memory.Assemble.InjectAndExecute(mnemonics);
            return rt == 1;
        }
        private bool HasEudemonGift(int slotID)
        {
            int rt = 0;
            string[] mnemonics =
                nMnemonics.eudemon.HasEudemonGift(MemoryStore.TARGETING_COLLECTIONS_BASE, MemoryStore.GET_LOCAL_PLAYER, slotID, MemoryStore.EUDEMON_HASGIFT_FUNCTION);

            rt = Memory.Assemble.Execute<int>(mnemonics, "Eudemon - HasEudemonGift");
            //rt = (int)Memory.Assemble.InjectAndExecute(mnemonics);
            return rt == 1;
        }
        public void UpdateEudemons(bool localPlayerCheck = false)
        {
            if (localPlayerCheck)
                if (!AuraModule.Utils.IsInGame() || AuraModule.Utils.locPlayer.GetEntityInfo.level <= 1)
                    return;

            if (!IsValid) return;

            for (int i = 0; i < 4; ++i)
            {
                if (!IsEudemonMeditating(i))
                {
                    if (HasEudemonGift(i))
                    {
                        if (TryEudemonAction(i, EudemonAction.EA_RETRIEVE))
                            break;
                    }
                    if (!TryEudemonAction(i, EudemonAction.EA_TALK))
                    {
                        if (TryEudemonAction(i, EudemonAction.EA_MEDITATION))
                            break;// only 1 meditation start at a time
                    }
                    else// 1 talk only at a time
                        break;
                }
            }
        }

    }
    public class FishingWindow
    {
        public FishingWindow(IntPtr intPtr)
        {
            WndPointer = intPtr;
        }
        public IntPtr WndPointer { get; set; }
        public bool IsValid
        {
            get
            {
                return WndPointer != IntPtr.Zero;
            }
        }
        private float blueRangeMax
        {
            get
            {
                return Memory.Reader.Read<float>(WndPointer + 0x240);
            }
            set
            {
                Memory.Writer.Write<float>(WndPointer + 0x240, value);
            }
        }
        private float blueRangeMin
        {
            get
            {
                return Memory.Reader.Read<float>(WndPointer + 0x244);
            }
            set
            {
                Memory.Writer.Write<float>(WndPointer + 0x244, value);
            }
        }
        private float currentLine
        {
            get
            {
                return Memory.Reader.Read<float>(WndPointer + 0x260);
            }
            set
            {
                Memory.Writer.Write<float>(WndPointer + 0x260, value);
            }
        }
        private float blueCenterValue
        {
            get
            {
                return (blueRangeMin + blueRangeMax) / 2f;

            }
        }
        public void setBlueRangeHack()
        {
            if (!IsValid) return;
            blueRangeMax = 0f;
            blueRangeMin = 1000f;
        }
        public void setCenterLineValue()
        {
            if (!IsValid) return;
            currentLine = blueCenterValue;
        }
        private void SetNextState()
        {
            //Check FishingWnd is Valid
            if (!IsValid)
                return;

            //Create AllocateMemory
            IntPtr AllocateMemory = Memory.Allocator.Allocate(32);

            //Check AllocteMemory if null or zero return
            if (AllocateMemory == null || AllocateMemory == IntPtr.Zero)
                return;
            string[] mnemonics =
                nMnemonics.fishing.SetNextState(AllocateMemory, WndPointer, MemoryStore.FISHING_SetNextState);

            //Part of execute mnemonics
            Memory.Assemble.Execute<IntPtr>(mnemonics, "SetNextState");

            //Dispose Allocate Memory
            if(AllocateMemory != IntPtr.Zero)
                Memory.Allocator.DisposeAlloc(AllocateMemory);
        }
    }

    #endregion
}
