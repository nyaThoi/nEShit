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

                }
                Hook.SetHook();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainInterface());
        }
    }
}
