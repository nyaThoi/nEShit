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
            this.eal = new System.Windows.Forms.CheckBox();
            this.resurrection = new System.Windows.Forms.CheckBox();
            this.speedhack = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.cb_fishHelper = new System.Windows.Forms.CheckBox();
            this.cb_fishingAssist = new System.Windows.Forms.CheckBox();
            this.fishType = new System.Windows.Forms.Label();
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
            this.cb_loop.Location = new System.Drawing.Point(16, 7);
            this.cb_loop.Margin = new System.Windows.Forms.Padding(4);
            this.cb_loop.Name = "cb_loop";
            this.cb_loop.Size = new System.Drawing.Size(60, 20);
            this.cb_loop.TabIndex = 1;
            this.cb_loop.Text = "Loop";
            this.cb_loop.UseVisualStyleBackColor = true;
            this.cb_loop.CheckedChanged += new System.EventHandler(this.cb_loop_CheckedChanged);
            // 
            // b_debug
            // 
            this.b_debug.Location = new System.Drawing.Point(491, 198);
            this.b_debug.Margin = new System.Windows.Forms.Padding(4);
            this.b_debug.Name = "b_debug";
            this.b_debug.Size = new System.Drawing.Size(100, 28);
            this.b_debug.TabIndex = 0;
            this.b_debug.Text = "Debug";
            this.b_debug.UseVisualStyleBackColor = true;
            this.b_debug.Click += new System.EventHandler(this.b_debug_Click);
            // 
            // eal
            // 
            this.eal.AutoSize = true;
            this.eal.Checked = true;
            this.eal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.eal.Location = new System.Drawing.Point(16, 36);
            this.eal.Margin = new System.Windows.Forms.Padding(4);
            this.eal.Name = "eal";
            this.eal.Size = new System.Drawing.Size(132, 20);
            this.eal.TabIndex = 3;
            this.eal.Text = "Eidolon Auto Link";
            this.eal.UseVisualStyleBackColor = true;
            // 
            // resurrection
            // 
            this.resurrection.AutoSize = true;
            this.resurrection.Checked = true;
            this.resurrection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.resurrection.Location = new System.Drawing.Point(16, 64);
            this.resurrection.Margin = new System.Windows.Forms.Padding(4);
            this.resurrection.Name = "resurrection";
            this.resurrection.Size = new System.Drawing.Size(135, 20);
            this.resurrection.TabIndex = 4;
            this.resurrection.Text = "Auto Resurrection";
            this.resurrection.UseVisualStyleBackColor = true;
            // 
            // speedhack
            // 
            this.speedhack.AutoSize = true;
            this.speedhack.Checked = true;
            this.speedhack.CheckState = System.Windows.Forms.CheckState.Checked;
            this.speedhack.Location = new System.Drawing.Point(16, 92);
            this.speedhack.Margin = new System.Windows.Forms.Padding(4);
            this.speedhack.Name = "speedhack";
            this.speedhack.Size = new System.Drawing.Size(99, 20);
            this.speedhack.TabIndex = 5;
            this.speedhack.Text = "Speedhack";
            this.speedhack.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(16, 133);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(4);
            this.trackBar1.Maximum = 35;
            this.trackBar1.Minimum = 10;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(273, 56);
            this.trackBar1.TabIndex = 7;
            this.trackBar1.Value = 15;
            // 
            // cb_fishHelper
            // 
            this.cb_fishHelper.AutoSize = true;
            this.cb_fishHelper.Checked = true;
            this.cb_fishHelper.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_fishHelper.Location = new System.Drawing.Point(223, 36);
            this.cb_fishHelper.Margin = new System.Windows.Forms.Padding(4);
            this.cb_fishHelper.Name = "cb_fishHelper";
            this.cb_fishHelper.Size = new System.Drawing.Size(129, 20);
            this.cb_fishHelper.TabIndex = 8;
            this.cb_fishHelper.Text = "BlueRangeHack";
            this.cb_fishHelper.UseVisualStyleBackColor = true;
            // 
            // cb_fishingAssist
            // 
            this.cb_fishingAssist.AutoSize = true;
            this.cb_fishingAssist.Checked = true;
            this.cb_fishingAssist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_fishingAssist.Location = new System.Drawing.Point(223, 64);
            this.cb_fishingAssist.Margin = new System.Windows.Forms.Padding(4);
            this.cb_fishingAssist.Name = "cb_fishingAssist";
            this.cb_fishingAssist.Size = new System.Drawing.Size(108, 20);
            this.cb_fishingAssist.TabIndex = 9;
            this.cb_fishingAssist.Text = "FishingAssist";
            this.cb_fishingAssist.UseVisualStyleBackColor = true;
            // 
            // fishType
            // 
            this.fishType.AutoSize = true;
            this.fishType.Location = new System.Drawing.Point(219, 7);
            this.fishType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fishType.Name = "fishType";
            this.fishType.Size = new System.Drawing.Size(64, 16);
            this.fishType.TabIndex = 10;
            this.fishType.Text = "FishType";
            // 
            // MainInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 186);
            this.Controls.Add(this.fishType);
            this.Controls.Add(this.cb_fishingAssist);
            this.Controls.Add(this.cb_fishHelper);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.speedhack);
            this.Controls.Add(this.resurrection);
            this.Controls.Add(this.eal);
            this.Controls.Add(this.cb_loop);
            this.Controls.Add(this.b_debug);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.CheckBox eal;
        private System.Windows.Forms.CheckBox resurrection;
        private System.Windows.Forms.CheckBox speedhack;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.CheckBox cb_fishHelper;
        private System.Windows.Forms.CheckBox cb_fishingAssist;
        private System.Windows.Forms.Label fishType;
    }
}

