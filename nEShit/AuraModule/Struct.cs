using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            //return Memory.Assemble.Execute<IntPtr>(mnemonics, "Revive");
            return Memory.Assemble.InjectAndExecute(mnemonics);

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
            //return Memory.Assemble.Execute<IntPtr>(mnemonics, "DoUIAction");
            return Memory.Assemble.InjectAndExecute(mnemonics);

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
                return Memory.Reader.Read<uint>(Pointer + 0x134);
            }
        }
        public short chatAttempts
        {
            get
            {
                return Memory.Reader.Read<short>(Pointer + 0x138);
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
        private short action
        {
            get
            {
                return Memory.Reader.Read<short>(WndPointer + 0x21C);
            }
        }
        private float blueRangeMax
        {
            get
            {
                return Memory.Reader.Read<float>(WndPointer + 0x24C);
            }
            set
            {
                blueRangeMax = value;
            }
        }
        private float blueRangeMin
        {
            get
            {
                return Memory.Reader.Read<float>(WndPointer + 0x250);
            }
            set
            {
                blueRangeMin = value;
            }
        }
        private float cursorValue
        {
            get
            {
                return Memory.Reader.Read<float>(WndPointer + 0x268);
            }
        }
        private float currentLine
        {
            get
            {
                return Memory.Reader.Read<float>(WndPointer + 0x26C);
            }
            set
            {
                currentLine = value;
            }
        }
        public float blueCenterValue()
        {
            return (blueRangeMin + blueRangeMax) / 2f;
        }
        public void setBlueRangeHack()
        {
            if(IsValid)
            {
                blueRangeMax = 0f;
                blueRangeMin = 1000f;
            }
        }
        public void setCenterLineValue()
        {
            if(IsValid)
            {
                currentLine = blueCenterValue();
            }
        }
    }

    #endregion
}
