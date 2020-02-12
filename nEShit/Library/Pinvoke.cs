using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

public class PInvoke 
{
    [DllImport("kernel32.dll")]
    public static extern IntPtr CreateMutex(IntPtr lpMutexAttributes, bool bInitialOwner, string lpName);

    [DllImport("kernel32.dll")]
    public static extern bool AllocConsole();

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr CreateFile(string lpFileName, [MarshalAs(UnmanagedType.U4)] DesiredAccess dwDesiredAccess, [MarshalAs(UnmanagedType.U4)] FileShare dwShareMode, uint lpSecurityAttributes, [MarshalAs(UnmanagedType.U4)] FileMode dwCreationDisposition, [MarshalAs(UnmanagedType.U4)] FileAttributes dwFlagsAndAttributes, uint hTemplateFile);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool SetStdHandle(StdHandle nStdHandle, IntPtr hHandle);

    [DllImport("kernel32.dll")]
    public unsafe static extern void CopyMemory(void* dest, void* src, int count);

    [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
    public unsafe static extern void MoveMemory(void* dest, void* src, int size);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool VirtualProtect(IntPtr lpAddress, int dwSize, MemoryProtectionFlags flNewProtect, out MemoryProtectionFlags lpflOldProtect);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool FreeLibrary(IntPtr hModule);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr VirtualAlloc(IntPtr lpAddress, UIntPtr dwSize, AllocationTypeFlags lAllocationType, MemoryProtectionFlags flProtect);

    [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern bool VirtualFree(IntPtr lpAddress, uint dwSize, FreeType dwFreeType);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern int VirtualQuery(IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

    [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenMutex(uint dwDesiredAccess, bool bInheritHandle, string lpName);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool ReleaseMutex(IntPtr hMutex);

    [SuppressUnmanagedCodeSecurity]
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, IntPtr hSourceHandle, IntPtr hTargetProcessHandle, out IntPtr lpTargetHandle, uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwOptions);

    [Flags]
    public enum DesiredAccess : uint
    {
        GenericRead = 2147483648u,
        GenericWrite = 1073741824u,
        GenericExecute = 536870912u,
        GenericAll = 268435456u
    }

    public enum StdHandle
    {
        Input = -10,
        Output = -11,
        Error = -12
    }

    public struct MEMORY_BASIC_INFORMATION
    {
        public IntPtr BaseAddress;

        public IntPtr AllocationBase;

        public AllocationTypeFlags AllocationProtect;

        public IntPtr RegionSize;

        public uint State;

        public MemoryProtectionFlags Protect;

        public uint Type;
    }

    [Flags]
    public enum MemoryProtectionFlags
    {
        ZeroAccess = 0,
        Execute = 16,
        ExecuteRead = 32,
        ExecuteReadWrite = 64,
        ExecuteWriteCopy = 128,
        NoAccess = 1,
        ReadOnly = 2,
        ReadWrite = 4,
        WriteCopy = 8,
        Guard = 256,
        NoCache = 512,
        WriteCombine = 1024
    }

    [Flags]
    public enum AllocationTypeFlags
    {
        Commit = 4096,
        Reserve = 8192,
        Decommit = 16384,
        Release = 32768,
        Reset = 524288,
        Physical = 4194304,
        TopDown = 1048576,
        WriteWatch = 2097152,
        LargePages = 536870912
    }

    public enum FreeType
    {
        Decommit = 16384,
        Release = 32768
    }
}
