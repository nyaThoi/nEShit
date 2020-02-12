using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct
{
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
        public EntityInfo GetEntityInfo
        {
            get
            {
                return new EntityInfo(Memory.Reader.Read<IntPtr>(Pointer + 12));
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
        }
        public void FullResurrection()
        {
            if(IsValid && GetEntityInfo.IsValid && GetEntityInfo.currentHP == 0)
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
        }
        public void TeleportInterface()
        {
            DoUIAction(0x11);
        }
        public float SetMovementValue(float val = 20)
        {
            if (IsValid && GetEntityInfo.IsValid && GetEntityInfo.MovementValue != 0)
                GetEntityInfo.MovementValue = val;
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


    }

}
