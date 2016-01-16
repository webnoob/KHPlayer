namespace KHPlayer.Forms
{
    partial class FPlayer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPlayer));
            this.timerPlayerStateChange = new System.Windows.Forms.Timer(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.axReader = new AxAcroPDFLib.AxAcroPDF();
            this.wmPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axReader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wmPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // timerPlayerStateChange
            // 
            this.timerPlayerStateChange.Enabled = true;
            this.timerPlayerStateChange.Tick += new System.EventHandler(this.timerPlayerStateChange_Tick);
            // 
            // axReader
            // 
            this.axReader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axReader.Enabled = true;
            this.axReader.Location = new System.Drawing.Point(0, 0);
            this.axReader.Name = "axReader";
            this.axReader.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axReader.OcxState")));
            this.axReader.Size = new System.Drawing.Size(506, 382);
            this.axReader.TabIndex = 1;
            this.axReader.UseWaitCursor = true;
            // 
            // wmPlayer
            // 
            this.wmPlayer.Enabled = true;
            this.wmPlayer.Location = new System.Drawing.Point(0, 0);
            this.wmPlayer.Name = "wmPlayer";
            this.wmPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmPlayer.OcxState")));
            this.wmPlayer.Size = new System.Drawing.Size(208, 163);
            this.wmPlayer.TabIndex = 0;
            this.wmPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.wmPlayer_PlayStateChange);
            // 
            // FPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(506, 382);
            this.Controls.Add(this.axReader);
            this.Controls.Add(this.wmPlayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FPlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Media Player";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axReader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wmPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer wmPlayer;
        private System.Windows.Forms.Timer timerPlayerStateChange;
        private System.Windows.Forms.BindingSource bindingSource1;
        private AxAcroPDFLib.AxAcroPDF axReader;
    }
}