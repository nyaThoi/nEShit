using System;
using System.Collections.Generic;
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
            Console.WriteLine($"Search GameProcess...");
            Pinvoke.GetCurrentProccess();

            int.TryParse(Console.ReadLine(), out int msg);
            if (msg != 0)
            {
                if (Load_Pattern.RetrieveAddresses((uint)msg))
                {
                    MessageBox.Show("Pattern not found! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
                Hook.SetHook();
                var handle = Pinvoke.GetConsoleWindow();
                Pinvoke.ShowWindow(handle, 5);

            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainInterface());
        }
    }
}
