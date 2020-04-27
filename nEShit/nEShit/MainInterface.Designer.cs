namespace nEShit
{
    partial class MainInterface
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.loop = new System.Windows.Forms.Timer(this.components);
            this.cb_loop = new System.Windows.Forms.CheckBox();
            this.b_debug = new System.Windows.Forms.Button();
            this.btn_teleportInterface = new System.Windows.Forms.Button();
            this.eal = new System.Windows.Forms.CheckBox();
            this.resurrection = new System.Windows.Forms.CheckBox();
            this.speedhack = new System.Windows.Forms.CheckBox();
            this.btn_Resurrection = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.cb_fishHelper = new System.Windows.Forms.CheckBox();
            this.cb_fishingAssist = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // loop
            // 
            this.loop.Enabled = true;
            this.loop.Interval = 500;
            this.loop.Tick += new System.EventHandler(this.loop_Tick);
            // 
            // cb_loop
            // 
            this.cb_loop.AutoSize = true;
            this.cb_loop.Checked = true;
            this.cb_loop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_loop.Location = new System.Drawing.Point(167, 3);
            this.cb_loop.Name = "cb_loop";
            this.cb_loop.Size = new System.Drawing.Size(50, 17);
            this.cb_loop.TabIndex = 1;
            this.cb_loop.Text = "Loop";
            this.cb_loop.UseVisualStyleBackColor = true;
            this.cb_loop.CheckedChanged += new System.EventHandler(this.cb_loop_CheckedChanged);
            // 
            // b_debug
            // 
            this.b_debug.Location = new System.Drawing.Point(313, 31);
            this.b_debug.Name = "b_debug";
            this.b_debug.Size = new System.Drawing.Size(75, 23);
            this.b_debug.TabIndex = 0;
            this.b_debug.Text = "Debug";
            this.b_debug.UseVisualStyleBackColor = true;
            this.b_debug.Click += new System.EventHandler(this.b_debug_Click);
            // 
            // btn_teleportInterface
            // 
            this.btn_teleportInterface.Location = new System.Drawing.Point(9, 132);
            this.btn_teleportInterface.Name = "btn_teleportInterface";
            this.btn_teleportInterface.Size = new System.Drawing.Size(112, 23);
            this.btn_teleportInterface.TabIndex = 2;
            this.btn_teleportInterface.Text = "Teleport Interface";
            this.btn_teleportInterface.UseVisualStyleBackColor = true;
            this.btn_teleportInterface.Click += new System.EventHandler(this.btn_teleportInterface_Click);
            // 
            // eal
            // 
            this.eal.AutoSize = true;
            this.eal.Checked = true;
            this.eal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.eal.Location = new System.Drawing.Point(12, 12);
            this.eal.Name = "eal";
            this.eal.Size = new System.Drawing.Size(109, 17);
            this.eal.TabIndex = 3;
            this.eal.Text = "Eidolon Auto Link";
            this.eal.UseVisualStyleBackColor = true;
            // 
            // resurrection
            // 
            this.resurrection.AutoSize = true;
            this.resurrection.Checked = true;
            this.resurrection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.resurrection.Location = new System.Drawing.Point(12, 35);
            this.resurrection.Name = "resurrection";
            this.resurrection.Size = new System.Drawing.Size(111, 17);
            this.resurrection.TabIndex = 4;
            this.resurrection.Text = "Auto Resurrection";
            this.resurrection.UseVisualStyleBackColor = true;
            // 
            // speedhack
            // 
            this.speedhack.AutoSize = true;
            this.speedhack.Checked = true;
            this.speedhack.CheckState = System.Windows.Forms.CheckState.Checked;
            this.speedhack.Location = new System.Drawing.Point(12, 58);
            this.speedhack.Name = "speedhack";
            this.speedhack.Size = new System.Drawing.Size(81, 17);
            this.speedhack.TabIndex = 5;
            this.speedhack.Text = "Speedhack";
            this.speedhack.UseVisualStyleBackColor = true;
            // 
            // btn_Resurrection
            // 
            this.btn_Resurrection.Location = new System.Drawing.Point(9, 161);
            this.btn_Resurrection.Name = "btn_Resurrection";
            this.btn_Resurrection.Size = new System.Drawing.Size(112, 23);
            this.btn_Resurrection.TabIndex = 6;
            this.btn_Resurrection.Text = "Full Resurrection";
            this.btn_Resurrection.UseVisualStyleBackColor = true;
            this.btn_Resurrection.Click += new System.EventHandler(this.btn_Resurrection_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 81);
            this.trackBar1.Maximum = 35;
            this.trackBar1.Minimum = 10;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(205, 45);
            this.trackBar1.TabIndex = 7;
            this.trackBar1.Value = 15;
            // 
            // cb_fishHelper
            // 
            this.cb_fishHelper.AutoSize = true;
            this.cb_fishHelper.Checked = true;
            this.cb_fishHelper.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_fishHelper.Location = new System.Drawing.Point(167, 35);
            this.cb_fishHelper.Name = "cb_fishHelper";
            this.cb_fishHelper.Size = new System.Drawing.Size(105, 17);
            this.cb_fishHelper.TabIndex = 8;
            this.cb_fishHelper.Text = "BlueRangeHack";
            this.cb_fishHelper.UseVisualStyleBackColor = true;
            // 
            // cb_fishingAssist
            // 
            this.cb_fishingAssist.AutoSize = true;
            this.cb_fishingAssist.Checked = true;
            this.cb_fishingAssist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_fishingAssist.Location = new System.Drawing.Point(167, 58);
            this.cb_fishingAssist.Name = "cb_fishingAssist";
            this.cb_fishingAssist.Size = new System.Drawing.Size(86, 17);
            this.cb_fishingAssist.TabIndex = 9;
            this.cb_fishingAssist.Text = "FishingAssist";
            this.cb_fishingAssist.UseVisualStyleBackColor = true;
            // 
            // MainInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 196);
            this.Controls.Add(this.cb_fishingAssist);
            this.Controls.Add(this.cb_fishHelper);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.btn_Resurrection);
            this.Controls.Add(this.speedhack);
            this.Controls.Add(this.resurrection);
            this.Controls.Add(this.eal);
            this.Controls.Add(this.btn_teleportInterface);
            this.Controls.Add(this.cb_loop);
            this.Controls.Add(this.b_debug);
            this.MaximizeBox = false;
            this.Name = "MainInterface";
            this.Text = "nEShit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainInterface_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer loop;
        private System.Windows.Forms.CheckBox cb_loop;
        private System.Windows.Forms.Button b_debug;
        private System.Windows.Forms.Button btn_teleportInterface;
        private System.Windows.Forms.CheckBox eal;
        private System.Windows.Forms.CheckBox resurrection;
        private System.Windows.Forms.CheckBox speedhack;
        private System.Windows.Forms.Button btn_Resurrection;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.CheckBox cb_fishHelper;
        private System.Windows.Forms.CheckBox cb_fishingAssist;
    }
}

