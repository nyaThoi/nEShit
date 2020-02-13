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
            this.SuspendLayout();
            // 
            // loop
            // 
            this.loop.Enabled = true;
            this.loop.Tick += new System.EventHandler(this.loop_Tick);
            // 
            // cb_loop
            // 
            this.cb_loop.AutoSize = true;
            this.cb_loop.Checked = true;
            this.cb_loop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_loop.Location = new System.Drawing.Point(16, 15);
            this.cb_loop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_loop.Name = "cb_loop";
            this.cb_loop.Size = new System.Drawing.Size(62, 21);
            this.cb_loop.TabIndex = 1;
            this.cb_loop.Text = "Loop";
            this.cb_loop.UseVisualStyleBackColor = true;
            this.cb_loop.CheckedChanged += new System.EventHandler(this.cb_loop_CheckedChanged);
            // 
            // b_debug
            // 
            this.b_debug.Location = new System.Drawing.Point(484, 39);
            this.b_debug.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.b_debug.Name = "b_debug";
            this.b_debug.Size = new System.Drawing.Size(100, 28);
            this.b_debug.TabIndex = 0;
            this.b_debug.Text = "Debug";
            this.b_debug.UseVisualStyleBackColor = true;
            this.b_debug.Click += new System.EventHandler(this.b_debug_Click);
            // 
            // btn_teleportInterface
            // 
            this.btn_teleportInterface.Location = new System.Drawing.Point(16, 43);
            this.btn_teleportInterface.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_teleportInterface.Name = "btn_teleportInterface";
            this.btn_teleportInterface.Size = new System.Drawing.Size(149, 28);
            this.btn_teleportInterface.TabIndex = 2;
            this.btn_teleportInterface.Text = "Teleport Interface";
            this.btn_teleportInterface.UseVisualStyleBackColor = true;
            this.btn_teleportInterface.Click += new System.EventHandler(this.btn_teleportInterface_Click);
            // 
            // MainInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 87);
            this.Controls.Add(this.btn_teleportInterface);
            this.Controls.Add(this.cb_loop);
            this.Controls.Add(this.b_debug);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainInterface";
            this.Text = "nEShit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainInterface_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer loop;
        private System.Windows.Forms.CheckBox cb_loop;
        private System.Windows.Forms.Button b_debug;
        private System.Windows.Forms.Button btn_teleportInterface;
    }
}

