using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nEShit
{
    static class Program
    {
 

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine($"ShitStorm Tool for AK.TO");
            Console.WriteLine($"Gameclient with Launcher.exe (AK.to on Steam) = 1 \nFor default Gameclient any number (not 1) lol.");
            int.TryParse(Console.ReadLine(), out int taskmsg);
            if (taskmsg == 1)
            {
                Load_Pattern.SteamClient = true;
                Console.WriteLine($"Launcher.exe Client used");
            }
            else Console.WriteLine($"Default, game.bin Client used.");

            Pinvoke.GetCurrentProccess();
            int.TryParse(Console.ReadLine(), out int msg);
            if (msg != 0)
            {
                Pinvoke.InjectPID = (uint)msg;
                if (Load_Pattern.RetrieveAddresses((uint)msg))
                {
                    MessageBox.Show("Pattern not found! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
                Hook.SetHook();
                var handle = Pinvoke.GetConsoleWindow();
#if RELEASE
                Pinvoke.ShowWindow(handle, 0);
#endif

            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainInterface());
        }
    }
}
