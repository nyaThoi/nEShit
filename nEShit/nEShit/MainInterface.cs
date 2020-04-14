using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nEShit
{
    public partial class MainInterface : Form
    {
        public MainInterface()
        {
            InitializeComponent();
        }

        private void b_debug_Click(object sender, EventArgs e)
        {

        }

        private void MainInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Call Game Crash Msg lul
            //Hook.UnsetHook();
        }

        private void loop_Tick(object sender, EventArgs e)
        {
            if (!cb_loop.Checked) return;

            if (!AuraModule.Utils.IsInGame() || AuraModule.Utils.locPlayer.GetEntityInfo.level <= 1)
                return;

            // Set Movement Value in Float
            AuraModule.Utils.locPlayer.SetMovementValue(20);
            // Auto Resurrection if currentLife is 0
            AuraModule.Utils.locPlayer.FullResurrection(); 
            // Auto Blue Range Hack assist for Fishing
            WindowManager.GetFishingWindow.setBlueRangeHack();
            // Auto Eidolon Link without Inventory Check
            WindowManager.GetEudemonExtendWindow.UpdateEudemons(25, false); 
        }

        private void cb_loop_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_loop.Checked)
                loop.Enabled = true;
            else loop.Enabled = false;
        }

        private void btn_teleportInterface_Click(object sender, EventArgs e)
        {
            AuraModule.Utils.locPlayer.TeleportInterface();
        }
    }
}
