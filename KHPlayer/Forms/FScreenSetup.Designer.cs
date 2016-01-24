namespace KHPlayer.Forms
{
    partial class FScreenSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FScreenSetup));
            this.bAddNewScreen = new System.Windows.Forms.Button();
            this.bDeleteScreen = new System.Windows.Forms.Button();
            this.lvScreens = new System.Windows.Forms.ListView();
            this.colScreen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colScreenNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bClose = new System.Windows.Forms.Button();
            this.colAudioDevice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // bAddNewScreen
            // 
            this.bAddNewScreen.Location = new System.Drawing.Point(12, 113);
            this.bAddNewScreen.Name = "bAddNewScreen";
            this.bAddNewScreen.Size = new System.Drawing.Size(220, 23);
            this.bAddNewScreen.TabIndex = 10;
            this.bAddNewScreen.Text = "Add Screen";
            this.bAddNewScreen.UseVisualStyleBackColor = true;
            this.bAddNewScreen.Click += new System.EventHandler(this.bAddNewScreen_Click);
            // 
            // bDeleteScreen
            // 
            this.bDeleteScreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.bDeleteScreen.Location = new System.Drawing.Point(237, 113);
            this.bDeleteScreen.Name = "bDeleteScreen";
            this.bDeleteScreen.Size = new System.Drawing.Size(222, 23);
            this.bDeleteScreen.TabIndex = 9;
            this.bDeleteScreen.Text = "Delete Screen";
            this.bDeleteScreen.UseVisualStyleBackColor = true;
            this.bDeleteScreen.Click += new System.EventHandler(this.bDeleteScreen_Click);
            // 
            // lvScreens
            // 
            this.lvScreens.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colScreen,
            this.colScreenNum,
            this.colAudioDevice});
            this.lvScreens.Location = new System.Drawing.Point(12, 12);
            this.lvScreens.Name = "lvScreens";
            this.lvScreens.Size = new System.Drawing.Size(446, 95);
            this.lvScreens.TabIndex = 11;
            this.lvScreens.UseCompatibleStateImageBehavior = false;
            // 
            // colScreen
            // 
            this.colScreen.Text = "Friendly Name";
            this.colScreen.Width = 105;
            // 
            // colScreenNum
            // 
            this.colScreenNum.Text = "Screen Number";
            this.colScreenNum.Width = 110;
            // 
            // bClose
            // 
            this.bClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bClose.Location = new System.Drawing.Point(12, 142);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(447, 23);
            this.bClose.TabIndex = 12;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // colAudioDevice
            // 
            this.colAudioDevice.Width = 225;
            // 
            // FScreenSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 177);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.lvScreens);
            this.Controls.Add(this.bAddNewScreen);
            this.Controls.Add(this.bDeleteScreen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FScreenSetup";
            this.Text = "Screen Setup";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bAddNewScreen;
        private System.Windows.Forms.Button bDeleteScreen;
        private System.Windows.Forms.ListView lvScreens;
        private System.Windows.Forms.ColumnHeader colScreen;
        private System.Windows.Forms.ColumnHeader colScreenNum;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.ColumnHeader colAudioDevice;

    }
}