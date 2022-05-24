using MyMemory.Hooks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



public class Attach
    {
        public unsafe static bool OpenProcess()
        {
            remoteProcess = new MyMemory.RemoteProcess(PID);

            if (remoteProcess.ProcessId != 0)
            {
                PID = remoteProcess.ProcessId;
                IsValid = true;
                return true;
            }
            return false;
        }
        public unsafe static bool IsValid = false;
        public unsafe static uint PID { get; set; }
        public unsafe static MyMemory.RemoteProcess remoteProcess;
        public unsafe static MyMemory.Memory.MemoryManager memoryManager
        {
            get
            {
                return remoteProcess.MemoryManager;
            }
        }
        public unsafe static IntPtr BaseAddress
        {
            get
            {
                return remoteProcess.ModulesManager.MainModule.BaseAddress;
            }
        }
        public unsafe static uint ProcessID
        {
            get
            {
                return remoteProcess.ProcessId;
            }
        }
        public unsafe static MyMemory.Hooks.HooksManager HooksManager
        {
            get
            {
                return remoteProcess.HooksManager.Process.HooksManager;
            }
        }
        public unsafe static CallbackNotifier.NotifyCallback notifyCallback;

    }
public unsafe static class ExecuteCB
{
    public unsafe static MyMemory.Hooks.HookBase MainLoop { get; private set; }
    public unsafe static List<MyMemory.Hooks.HookBase> HookList { get; private set; }
    public unsafe static MyMemory.Hooks.CallbackExecutor Executor { get; private set; }
    public unsafe static bool InstallHook(IntPtr DETOUR_MAIN_LOOP_OFFSET, int Size)
    {
        if (DETOUR_MAIN_LOOP_OFFSET == IntPtr.Zero) return false;

        ExecuteCB.HookList = new List<MyMemory.Hooks.HookBase>();
        ExecuteCB.MainLoop = Attach.HooksManager.CreateAndApplyJmpHook(DETOUR_MAIN_LOOP_OFFSET, Size);
        ExecuteCB.Executor = MainLoop.Executor;
        Debug.WriteLine($"Hooking MainLoop on 0x{ExecuteCB.Executor.EntryPointPointer.ToString("X")}");
        return true;

    }
    public unsafe static bool InstallFishingCallback (IntPtr FishingCallback, int Size)
    {
        if (FishingCallback == IntPtr.Zero) return false;
        AddedHook(FishingCallback, Size, new CallbackNotifier.NotifyCallback(Callback.Fishing.Callback), true);
        Debug.WriteLine($"Hooking CB_Fishing 0x{FishingCallback.ToString("X")}");
        return true;
    }
    internal static HookJmp AddedHook(IntPtr intptr_0, int int_0, CallbackNotifier.NotifyCallback notifyCallback_0, bool bool_0 = true)
    {
        HookJmp hookJmp = Attach.HooksManager.CreateJmpHook(intptr_0, int_0);
        if (bool_0)
        {
            hookJmp.Apply();
        }
        hookJmp.Notifier.AddCallback(notifyCallback_0);
        ExecuteCB.HookList.Add(hookJmp);
        return hookJmp;
    }
    public unsafe static void Detach()
    {
        if (Attach.PID != 0)
        {
            //Hookbase to access to remove
            foreach (MyMemory.Hooks.HookBase hookBase in ExecuteCB.HookList)
            {
                if (hookBase.IsApplied)
                {
                    hookBase.Remove();
                }
            }
            ExecuteCB.MainLoop.Remove();
            bool MainLoop = Memory.Allocator.DisposeAlloc(ExecuteCB.Executor.EntryPointPointer);
            Console.WriteLine($"UnHooking : {MainLoop}");
        }
    }
}
public class Callback
{
    public class Fishing
    {
        public unsafe static void Callback(CallbackNotifier.NotifyArgs  notifyArgs)
        {
            IntPtr pointer = Memory.Reader.Read<IntPtr>(notifyArgs.Registers.Esp + 0x4);
            if (Memory.Reader.Read<short>(pointer + 20) == 2)
            {
                short num = Memory.Reader.Read<short>(pointer + 0x18);
                FishType type;
                string str;
                switch (num)
                {
                    case 3941:
                        type = FishType.MiracleCube;
                        str = "Miracle Cube";
                        break;
                    case 3942:
                        type = FishType.White;
                        str = "White Fish";
                        break;
                    case 3943:
                        type = FishType.Green;
                        str = "Green Fish";
                        break;
                    case 3944:
                        type = FishType.Orange;
                        str = "Orange Fish";
                        break;
                    case 3945:
                        type = FishType.FishKing;
                        str = "Fish King";
                        break;
                    default:
                        type = FishType.Unknown;
                        str = "Unknown : " + num.ToString();
                        break;
                }
                //Debug.WriteLine(str + " captured !");
                StreamTable streamTable = table;
                if (streamTable == null)
                {
                    return;
                }
                streamTable(type);
            }

        }
        [CompilerGenerated]
        public unsafe static StreamTable table;
        public delegate void StreamTable(FishType type);
        public enum FishType
        {
            White = 3942,
            Green = 3943,
            Orange = 3944,
            MiracleCube = 3941,
            FishKing = 3945,
            Unknown = 0
        }
        public unsafe static event Fishing.StreamTable GetVal
        {
            [CompilerGenerated]
            add
            {
                StreamTable streamTable = table;
                StreamTable streamTable2;
                do
                {
                    streamTable2 = streamTable;
                    StreamTable value2 = (StreamTable)Delegate.Combine(streamTable2, value);
                    streamTable = Interlocked.CompareExchange<StreamTable>(ref table, value2, streamTable2);
                }
                while (streamTable != streamTable2);
            }
            [CompilerGenerated]
            remove
            {
                StreamTable streamTable = table;
                StreamTable streamTable2;
                do
                {
                    streamTable2 = streamTable;
                    StreamTable value2 = (StreamTable)Delegate.Remove(streamTable2, value);
                    streamTable = Interlocked.CompareExchange<StreamTable>(ref table, value2, streamTable2);
                }
                while (streamTable != streamTable2);
            }
        }
    }
}
public class Memory
{
    public class Assemble
    {
        public unsafe static IntPtr InjectAndExecute(string[] mnemonics, IntPtr lpAddress)
        {
            return Attach.remoteProcess.Yasm.InjectAndExecute(mnemonics, lpAddress);
        }
        public unsafe static IntPtr InjectAndExecute(string[] mnemonics)
        {
            return Attach.remoteProcess.Yasm.InjectAndExecute(mnemonics);
        }
        public unsafe static bool Inject(string[] mnemonics, IntPtr lpAddress)
        {
            return Attach.remoteProcess.Yasm.Inject(mnemonics, lpAddress);
        }
        public unsafe static void SetBufferSize(int size)
        {
            Attach.remoteProcess.Yasm.SetBufferSize(size);
        }
        public unsafe static byte[] Assembler(string[] mnemonics)
        {
            return Attach.remoteProcess.Yasm.Assemble(mnemonics);
        }
        public unsafe static byte[] Assembler(string[] mnemonics, IntPtr org)
        {
            return Attach.remoteProcess.Yasm.Assemble(mnemonics, org);
        }
        public unsafe static T Execute<T>(string[] mnemonics, string Info = "NotDefine") where T : struct
        {
            return ExecuteCB.Executor.Execute<T>(mnemonics, Info);
        }
    }
    public class Allocator
    {
        public unsafe static IntPtr Allocate(uint Allocatesize)
        {
            return Attach.remoteProcess.MemoryManager.AllocateRawMemory(Allocatesize);
        }
        public unsafe static bool IsValid(IntPtr intPtr)
        {
            if (intPtr != IntPtr.Zero)
            {
                return true;
            }
            return false;
        }
        public unsafe static bool DisposeAlloc(IntPtr intPtr)
        {
            if (intPtr != IntPtr.Zero)
            {
                return Attach.remoteProcess.MemoryManager.FreeRawMemory(intPtr);
            }
            return false;
        }
        public unsafe static int GetAllocatorSizeMnemonics(string[] mnemonics)
        {
            return mnemonics.Length;
        }
    }
    public class Reader
    {        
        public unsafe static T Read<T>(IntPtr address) where T : struct
        { 
            var Mem = Attach.remoteProcess.MemoryManager;
            return Mem.Read<T>(address);
        }
        public unsafe static T[] ReadArray<T>(IntPtr address, int count) where T : struct
        {
            var Mem = Attach.remoteProcess.MemoryManager;
            return Mem.ReadArray<T>(address,count);
        }
        public unsafe static string ReadString(IntPtr address, Encoding encoding, int maxLength = 128)
        {
            var Mem = Attach.remoteProcess.MemoryManager;
            return Mem.ReadString(address,encoding,maxLength);
        }
        public unsafe static byte[] ReadBytes(IntPtr address, int count)
        {
            var Mem = Attach.remoteProcess.MemoryManager;
            return Mem.ReadBytes(address,count);
        }

        public unsafe static string ReadSTDString(IntPtr intPtr, Encoding encoding)
        {
            var Mem = Attach.remoteProcess.MemoryManager;
            if (intPtr == IntPtr.Zero) 
                return string.Empty;

            int num = Mem.Read<int>(intPtr + 16);
            int num2 = Mem.Read<int>(intPtr + 20);
            if (num <= 0 || num > 4096) 
                return string.Empty;
            if (num2 < 16) 
                return ReadString(intPtr, encoding, num);

            return ReadString(Read<IntPtr>(intPtr), encoding, num);
        }


        public unsafe static Task<T> AsyncRead<T>(IntPtr address) where T : struct
        {
            return Task.Run(() => Read<T>(address));
        }
        public unsafe static Task<string> AsyncReadString(IntPtr address, Encoding encoding, int maxLength = 128)
        {
            return Task.Run(() => ReadString(address, encoding, maxLength));
        }
        public unsafe static Task<byte[]> AsyncReadBytes(IntPtr address, int size)
        {
            return Task.Run(() => ReadBytes(address, size));
        }
        public unsafe static Task<T[]> AsyncReadArray<T>(IntPtr address, int count) where T : struct
        {
            return Task.Run(() => ReadArray<T>(address, count));
        }

    }
    public class Writer
    {
        public unsafe static bool Write<T>(IntPtr address, T obj) where T : struct
        {
            var Mem = Attach.remoteProcess.MemoryManager;
            return Mem.Write<T>(address, obj);
        }
		public unsafe static bool WriteBytes(IntPtr address, byte[] buffer)
        {
            var Mem = Attach.remoteProcess.MemoryManager;
            return Mem.WriteBytes(address, buffer);
        }
        public unsafe static bool WriteString(IntPtr address, string value, Encoding encoding)
        {
            var Mem = Attach.remoteProcess.MemoryManager;
            return Mem.WriteString(address, encoding,value);
        }

        public unsafe static Task<bool> AsyncWrite<T>(IntPtr address, T obj) where T : struct
        {
            return Task.Run(() => Write<T>(address, obj));
        }
        public unsafe static Task<bool> AsyncWriteBytes(IntPtr address, byte[] buffer)
        {
            return Task.Run(() => WriteBytes(address, buffer));
        }
        public unsafe static Task<bool> AsyncWriteString(IntPtr address, string value, Encoding encoding)
        {
            return Task.Run(() => WriteString(address, value, encoding));
        }
    }
}
public class PatternManager
{
    public unsafe static IntPtr FindPattern(ProcessModule processModule, string pattern, bool resultAbsolute = true)
    {
        if (processModule == null || string.IsNullOrEmpty(pattern)) return IntPtr.Zero;

        var list = new List<byte>();
        var list2 = new List<bool>();
        var array = pattern.Split(' ');
#if x86
		int refBufferStartAddress = resultAbsolute ? processModule.BaseAddress.ToInt32() : 0;
#else
        long refBufferStartAddress = resultAbsolute ? processModule.BaseAddress.ToInt64() : 0;
#endif
        byte[] buffer = Memory.Reader.ReadBytes(processModule.BaseAddress, processModule.ModuleMemorySize);
        if (buffer == null || buffer.Length != processModule.ModuleMemorySize)
        {
            Debug.WriteLine("Failed Reading bytes - FindPattern");
            return IntPtr.Zero;
        }

        var num = 0;
        if (0 < array.Length)
            do
            {
                var text = array[num];
                if (!string.IsNullOrEmpty(text))
                    if (text != "?" && text != "??")
                    {
                        byte b;
                        if (!byte.TryParse(text, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out b))
                            break;
                        list.Add(Convert.ToByte(text, 16));
                        list2.Add(true);
                    }
                    else
                    {
                        list.Add(0);
                        list2.Add(false);
                    }
                num++;
            } while (num < array.Length);
        var count = list.Count;
        var num2 = buffer.Length - count;
        var num3 = 0;
        if (0 < num2)
        {
            for (; ; )
            {
                var num4 = 0;
                if (0 >= count)
                    break;
                while (!list2[num4] || list[num4] == buffer[num4 + num3])
                {
                    num4++;
                    if (num4 >= count)
                        return new IntPtr(refBufferStartAddress + num3);
                }
                num3++;
                if (num3 >= num2)
                    return IntPtr.Zero;
            }
            return new IntPtr(refBufferStartAddress + num3);
        }
        return IntPtr.Zero;
    }
    public unsafe static Task<IntPtr> AsyncFindPattern(ProcessModule processModule, string pattern, bool resultAbsolute = true)
    {
        return Task.Run(() => FindPattern(processModule, pattern, resultAbsolute));
    }
    public unsafe static List<IntPtr> FindPatternMany(ProcessModule processModule, string pattern, bool resultAbsolute = true)
    {
        if (processModule == null || string.IsNullOrEmpty(pattern)) return new List<IntPtr>();

        List<IntPtr> lpResults = new List<IntPtr>();
        List<byte> bytesPattern = new List<byte>();
        List<bool> boolMask = new List<bool>();

        byte[] bytesBuffer = Memory.Reader.ReadBytes(processModule.BaseAddress, processModule.ModuleMemorySize);
        if (bytesBuffer.Length < 1) throw new Exception("Failed reading bytes for region 'processModule'");

        foreach (string s in pattern.Split(' '))
        {
            if (string.IsNullOrEmpty(s)) continue;
            if (s == "?" || s == "??")
            {
                bytesPattern.Add(0x0);
                boolMask.Add(false);
            }
            else
            {
                byte b;
                if (byte.TryParse(s, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out b))
                {
                    bytesPattern.Add(Convert.ToByte(s, 16));
                    boolMask.Add(true);
                }
                else
                {
                    break;
                }
            }
        }

        int intIx, intIy = 0;
        int intPatternLength = bytesPattern.Count;
        int intDataLength = bytesBuffer.Length - intPatternLength;

        for (intIx = 0; intIx < intDataLength; intIx++)
        {
            var boolFound = true;
            for (intIy = 0; intIy < intPatternLength; intIy++)
            {
                if (boolMask[intIy] && bytesPattern[intIy] != bytesBuffer[intIx + intIy])
                {
                    boolFound = false;
                    break;
                }
            }

            if (boolFound)
            {
#if x86
				lpResults.Add(!resultAbsolute ? new IntPtr(intIx) : new IntPtr(processModule.BaseAddress.ToInt32() + intIx));
#else
                lpResults.Add(!resultAbsolute ? new IntPtr(intIx) : new IntPtr(processModule.BaseAddress.ToInt64() + intIx));
#endif
            }
        }

        return lpResults;
    }
    public unsafe static Task<List<IntPtr>> AsyncFindPatternMany(ProcessModule processModule, string pattern, bool resultAbsolute = true)
    {
        return Task.Run(() => FindPatternMany(processModule, pattern, resultAbsolute));
    }
    public unsafe static IntPtr FindPatternAlain(ProcessModule processModule, string pattern, int offset = 0, int occurenceIdx = -1, MemoryType type = MemoryType.RT_ADDRESS, bool CheckResult = false, string RT_LOCATION_Checkbytes = "55 8B EC", bool resultAbsolute = true)
    {
        List<IntPtr> lpResults = FindPatternMany(processModule, pattern);
        if (lpResults.Count < 1) return IntPtr.Zero;

        if (occurenceIdx > -1)
        {
            if (occurenceIdx - 1 < 0) occurenceIdx = 0;
            IntPtr lpAddr_occurence = lpResults[occurenceIdx - 1];
            if (offset != 0)
                lpAddr_occurence = IntPtr.Add(lpAddr_occurence, offset);

            switch (type)
            {
                case MemoryType.RT_READNEXT4_BYTES_RAW:
                    byte[] next4BytesArrRaw = new byte[4];
                    Array.Copy(Memory.Reader.ReadBytes(lpAddr_occurence + 1, 4), 0, next4BytesArrRaw, 0, 4); // +1 just assumes pattern returns a address where CALL is first instruction
                    return new IntPtr(BitConverter.ToInt32(next4BytesArrRaw, 0));
                case MemoryType.RT_READNEXT4_BYTES: // Return Absolute address
                    byte[] next4BytesArrAbs = new byte[4];
                    Array.Copy(Memory.Reader.ReadBytes(lpAddr_occurence + 1, 4), 0, next4BytesArrAbs, 0, 4); // +1 just assumes pattern returns a address where CALL is first instruction
                                                                                                                                    //if (BitConverter.IsLittleEndian)
                                                                                                                                    //	Array.Reverse(next4BytesArrAbs);
                    return new IntPtr(processModule.BaseAddress.ToInt32() + BitConverter.ToInt32(next4BytesArrAbs, 0) - (processModule.BaseAddress.ToInt32() - lpAddr_occurence.ToInt32() - 5));
                case MemoryType.RT_REL_ADDRESS:
                    if (CheckResult)
                    {
                        //byte[] fiveBytes = ReadBytes(lpAddr_occurence.ToInt64() - 1, 5);
                        byte[] fiveBytes = Memory.Reader.ReadBytes(lpAddr_occurence - 1, 5);
                        if (fiveBytes[0] == 0xE8) // CALL
                            return new IntPtr(lpAddr_occurence.ToInt32() + 4);
                        else
                        {
                            Debug.WriteLine("FindPattern with argument RT_REL_ADDRESS failed as found address was not lead by a CALL(0xE8)");
                            return IntPtr.Zero;
                        }
                    }
                    else
                        return lpAddr_occurence;
                case MemoryType.RT_ADDRESS:
                    return lpAddr_occurence;
                case MemoryType.RT_LOCATION:
                    if (CheckResult)
                    {
                        if (string.IsNullOrEmpty(RT_LOCATION_Checkbytes))
                        {
                            string[] RT_LOCATION_CheckbytesHardCoded = "55 8B EC".Split(' ');
                            int intBytesToRead = RT_LOCATION_CheckbytesHardCoded.Length;
                            //byte[] bytesCheckBuffer = ReadBytes(lpAddr_occurence.ToInt64(), intBytesToRead);
                            byte[] bytesCheckBuffer = Memory.Reader.ReadBytes(lpAddr_occurence, intBytesToRead);
                            for (int intByteIdx = 0; intByteIdx < bytesCheckBuffer.Length; intByteIdx++)
                            {
                                if (Convert.ToByte(RT_LOCATION_CheckbytesHardCoded[intByteIdx], 16) != bytesCheckBuffer[intByteIdx])
                                    return IntPtr.Zero;
                            }

                            return lpAddr_occurence;
                        }
                        else
                        {
                            string[] checkBytes = RT_LOCATION_Checkbytes.Split(' ');
                            int intBytesToRead = checkBytes.Length;
                            //byte[] bytesCheckBuffer = ReadBytes(lpAddr_occurence.ToInt64(), intBytesToRead);
                            byte[] bytesCheckBuffer = Memory.Reader.ReadBytes(lpAddr_occurence, intBytesToRead);
                            for (int intByteIdx = 0; intByteIdx < bytesCheckBuffer.Length; intByteIdx++)
                            {
                                if (Convert.ToByte(checkBytes[intByteIdx], 16) != bytesCheckBuffer[intByteIdx])
                                    return IntPtr.Zero;
                            }

                            return lpAddr_occurence;
                        }
                    }
                    else
                        return lpAddr_occurence;
                default:
                    return lpAddr_occurence;
            }
        }

        IntPtr lpAddr_first = lpResults[0];
        if (offset != 0)
            lpAddr_first = IntPtr.Add(lpAddr_first, offset);

        switch (type)
        {
            case MemoryType.RT_READNEXT4_BYTES_RAW:
                byte[] next4BytesArrRaw = new byte[4];
                Array.Copy(Memory.Reader.ReadBytes(lpAddr_first + 1, 4), 0, next4BytesArrRaw, 0, 4); // +1 just assumes pattern returns a address where CALL is first instruction
                return new IntPtr(BitConverter.ToInt32(next4BytesArrRaw, 0));
            case MemoryType.RT_READNEXT4_BYTES: // Return Absolute address
                byte[] next4BytesArrAbs = new byte[4];
                Array.Copy(Memory.Reader.ReadBytes(lpAddr_first + 1, 4), 0, next4BytesArrAbs, 0, 4); // +1 just assumes pattern returns a address where CALL is first instruction
                return new IntPtr(processModule.BaseAddress.ToInt32() + BitConverter.ToInt32(next4BytesArrAbs, 0) - (processModule.BaseAddress.ToInt32() - lpAddr_first.ToInt32() - 5));
            case MemoryType.RT_REL_ADDRESS:
                if (CheckResult)
                {
                    byte[] fiveBytes = Memory.Reader.ReadBytes(lpAddr_first - 1, 5);
                    if (fiveBytes[0] == 0xE8) // CALL
                    {
                        return new IntPtr(lpAddr_first.ToInt32() + 5);
                    }
                    else
                    {
                        throw new Exception("MemoryType 'RT_RELATIVE' was passed as a argument but result was not a CALL instruction!");
                    }
                }
                else
                    return lpAddr_first;

            case MemoryType.RT_ADDRESS:
                return lpAddr_first;
            case MemoryType.RT_LOCATION:
                if (CheckResult)
                {
                    if (string.IsNullOrEmpty(RT_LOCATION_Checkbytes))
                    {
                        string[] RT_LOCATION_CheckbytesHardCoded = "55 8B EC".Split(' ');
                        int intBytesToRead = RT_LOCATION_CheckbytesHardCoded.Length;
                        byte[] bytesCheckBuffer = Memory.Reader.ReadBytes(lpAddr_first, intBytesToRead);
                        for (int intByteIdx = 0; intByteIdx < bytesCheckBuffer.Length; intByteIdx++)
                        {
                            if (Convert.ToByte(RT_LOCATION_CheckbytesHardCoded[intByteIdx], 16) != bytesCheckBuffer[intByteIdx])
                                return IntPtr.Zero;
                        }
                        return lpAddr_first;
                    }
                    else
                    {
                        string[] checkBytes = RT_LOCATION_Checkbytes.Split(' ');
                        int intBytesToRead = checkBytes.Length;
                        byte[] bytesCheckBuffer = Memory.Reader.ReadBytes(lpAddr_first, intBytesToRead);
                        for (int intByteIdx = 0; intByteIdx < bytesCheckBuffer.Length; intByteIdx++)
                        {
                            if (Convert.ToByte(checkBytes[intByteIdx], 16) != bytesCheckBuffer[intByteIdx])
                                return IntPtr.Zero;
                        }
                        return lpAddr_first;
                    }
                }
                else
                    return lpAddr_first;

            default:
                return lpAddr_first;
        }

    }
    public unsafe static Task<IntPtr> AsyncFindPatternAlain(ProcessModule processModule, string pattern, int offset = 0, int occurenceIdx = -1, MemoryType type = MemoryType.RT_ADDRESS, bool CheckResult = false, string RT_LOCATION_Checkbytes = "55 8B EC", bool resultAbsolute = true)
    {
        return Task.Run(() => FindPatternAlain(processModule, pattern, offset, occurenceIdx, type, CheckResult, RT_LOCATION_Checkbytes, resultAbsolute));
    }

    public enum MemoryType : uint
    {
        RT_REL_ADDRESS = 0x0,
        RT_ADDRESS = 0x1,
        RT_LOCATION = 0x2,

        RT_READNEXT4_BYTES = 0x3,
        RT_READNEXT4_BYTES_RAW = 0x4,
    }
}
public class Minimem
{
    private static Process processObject = System.Diagnostics.Process.GetProcessById((int)Attach.PID);
    public unsafe static ProcessModule FindProcessModule(string moduleName, bool sloppySearch = false)
    {
        if (string.IsNullOrEmpty(moduleName)) throw new Exception($"Module Name cannot be null or empty!");

        foreach (ProcessModule pm in processObject.Modules)
        {
            if (sloppySearch && (pm.ModuleName.ToLower().Contains(moduleName.ToLower()) || pm.ModuleName.ToLower().StartsWith(moduleName.ToLower())))
                return pm;
            else if (string.Equals(pm.ModuleName, moduleName, StringComparison.CurrentCultureIgnoreCase)) return pm;
        }
        throw new Exception($"Cannot find any process module with name \"{moduleName}\"");
    }
}
