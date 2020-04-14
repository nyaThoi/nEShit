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

            string[] mnemonics = new string[]
                {
                        "use32",
                        $"push {(int)ReviveType}",
                        $"call {MemoryStore.PLAYER_Resurrection}",
                        "add esp,4",
                        "retn",
                };
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
            string[] mnemonics = new string[]
            {
                    "use32",
                    "push 0",
                    "push 0",
                    $"push {slotType.ToString("D")}",

                    $"call {MemoryStore.PLAYER_DoUIAction}",
                    "add esp, 0xC",
                    "retn"
            };
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
            string[] mnemonics = new string[]
 {
                    "use32",
                    $"mov eax, [{MemoryStore.TARGETING_COLLECTIONS_BASE}]",
                    "test eax, eax",
                    "je .out",
                    "mov ecx, eax",
                    "xor eax, eax",
                    $"call {MemoryStore.GET_LOCAL_PLAYER}",
                    "je .out",
                    $"push {(int)slotID}",
                    "mov ecx, eax",
                    $"call {MemoryStore.EUDEMON_GETEUDEMON_FUNCTION}",
                    ".out:",
                    "retn"
 };

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
            string[] mnemonics = new string[]
        {
                    "use32",
                    $"mov eax, {eudemon.Pointer}",
                    "mov eax, [eax]",
                    "push 0",
                    $"push {action.ToString("D")}",
                    $"push {arg1}",
                    "push eax",

                    $"call {MemoryStore.EUDEMON_SENDCOMMAND_FUNCTION}",
                    "add esp, 10h",
                    "retn"
        };
            Memory.Assemble.Execute<IntPtr>(mnemonics, "Eudemon - TryEudemonAction");
            //Memory.Assemble.InjectAndExecute(mnemonics);
            return true;
        }
        private bool IsEudemonMeditating(int slotID)
        {
            int rt = 0;
            string[] mnemonics = new string[]
                {
                "use32",
                $"mov eax, [{MemoryStore.TARGETING_COLLECTIONS_BASE}]",
                "test eax, eax",
                "je @out",
                "mov ecx, eax",
                "xor eax, eax",
                $"call {MemoryStore.GET_LOCAL_PLAYER}",
                "je @out",
                $"push {(int)slotID}",
                "mov ecx, eax",
                $"call {MemoryStore.EUDEMON_ISMEDITATING_FUNCTION}",
                "test eax, eax",
                "movzx eax, al",
                "@out:",
                "retn"
                };
            rt = Memory.Assemble.Execute<int>(mnemonics, "Eudemon - IsEudemonMeditating");
            //rt = (int)Memory.Assemble.InjectAndExecute(mnemonics);
            return rt == 1;
        }
        private bool HasEudemonGift(int slotID)
        {
            int rt = 0;
            string[] mnemonics = new string[]
            {
                "use32",
                /*get LocalPlayer*/
                $"mov eax, [{MemoryStore.TARGETING_COLLECTIONS_BASE}]",
                "test eax, eax",
                "je @out",
                "mov ecx, eax",
                "xor eax, eax",
                $"call {MemoryStore.GET_LOCAL_PLAYER}",
                "je @out",
                /*func*/
                $"push {(int)slotID}",
                "mov ecx, eax",
                $"call {MemoryStore.EUDEMON_HASGIFT_FUNCTION}",
                "test eax, eax",
                "movzx eax, al",
                "@out:",
                "retn"
            };
            rt = Memory.Assemble.Execute<int>(mnemonics, "Eudemon - HasEudemonGift");
            //rt = (int)Memory.Assemble.InjectAndExecute(mnemonics);
            return rt == 1;
        }
        public void UpdateEudemons(int sleepTime, bool localPlayerCheck = false)
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
        private bool IsValid
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
    }

    #endregion
}
