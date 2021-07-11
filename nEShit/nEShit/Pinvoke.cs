﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


class Pinvoke
{
    [DllImport("kernel32.dll")]
    internal static extern IntPtr GetConsoleWindow();
    [DllImport("user32.dll")]
    internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    const int SW_HIDE = 0;
    const int SW_SHOW = 5;

    public class InterObject
    {
        public int pid { get; set; }
        public IntPtr whandle { get; set; }
        public string windowname { get; set; }
        public string pname { get; set; }
        public double processTime { get; set; }
        public InterObject()
        {
            pid = 0;
            windowname = "";
            whandle = IntPtr.Zero;
            pname = "";
            processTime = 0;
        }
        public override string ToString()
        {
            const int MaxLength = 14;

            string outPut = "";
            if (windowname.Length > MaxLength)
            {
                outPut = windowname.Substring(0, MaxLength);
            }
            return $"[{pid}] [{outPut}]";
        }
    }

    public static InterObject SelectedProcessInfo = new InterObject();

    internal static void GetCurrentProccess()
    {
        Process[] plist = Process.GetProcesses();

        foreach (var p in plist)
        {
            if (p.ProcessName.Contains("game.bin"))
            {
                SelectedProcessInfo.pid = p.Id;
                SelectedProcessInfo.windowname = p.MainWindowTitle;
                Console.WriteLine($"{SelectedProcessInfo}");
            }
            if (p.ProcessName.Contains("Launcher"))
            {
                SelectedProcessInfo.pid = p.Id;
                SelectedProcessInfo.windowname = p.MainWindowTitle;
                Console.WriteLine($"{SelectedProcessInfo}");
            }

        }
    }

}
