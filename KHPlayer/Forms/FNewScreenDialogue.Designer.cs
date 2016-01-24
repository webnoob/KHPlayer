namespace KHPlayer.Forms
{
    partial class FNewScreenDialogue
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
            this.bOk = new System.Windows.Forms.Button();
            this.tbScreenName = new System.Windows.Forms.TextBox();
            this.lScreenName = new System.Windows.Forms.Label();
            this.lScreenNumber = new System.Windows.Forms.Label();
            this.bCancel = new System.Windows.Forms.Button();
            this.lAudioDevice = new System.Windows.Forms.Label();
            this.cbAudioDevice = new System.Windows.Forms.ComboBox();
            this.cbScreenDevice = new System.Windows.Forms.ComboBox();
            this.lColour = new System.Windows.Forms.Label();
            this.cbDefault = new System.Windows.Forms.CheckBox();
            this.colorDlgMain = new System.Windows.Forms.ColorDialog();
            this.tbColour = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(11, 193);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(263, 23);
            this.bOk.TabIndex = 11;
            this.bOk.Text = "Ok";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // tbScreenName
            // 
            this.tbScreenName.Location = new System.Drawing.Point(12, 25);
            this.tbScreenName.Name = "tbScreenName";
            this.tbScreenName.Size = new System.Drawing.Size(261, 20);
            this.tbScreenName.TabIndex = 12;
            // 
            // lScreenName
            // 
            this.lScreenName.AutoSize = true;
            this.lScreenName.Location = new System.Drawing.Point(12, 9);
            this.lScreenName.Name = "lScreenName";
            this.lScreenName.Size = new System.Drawing.Size(72, 13);
            this.lScreenName.TabIndex = 13;
            this.lScreenName.Text = "Screen Name";
            // 
            // lScreenNumber
            // 
            this.lScreenNumber.AutoSize = true;
            this.lScreenNumber.Location = new System.Drawing.Point(12, 110);
            this.lScreenNumber.Name = "lScreenNumber";
            this.lScreenNumber.Size = new System.Drawing.Size(81, 13);
            this.lScreenNumber.TabIndex = 14;
            this.lScreenNumber.Text = "Screen Number";
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(11, 222);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(263, 23);
            this.bCancel.TabIndex = 16;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // lAudioDevice
            // 
            this.lAudioDevice.AutoSize = true;
            this.lAudioDevice.Location = new System.Drawing.Point(13, 150);
            this.lAudioDevice.Name = "lAudioDevice";
            this.lAudioDevice.Size = new System.Drawing.Size(71, 13);
            this.lAudioDevice.TabIndex = 17;
            this.lAudioDevice.Text = "Audio Device";
            // 
            // cbAudioDevice
            // 
            this.cbAudioDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAudioDevice.FormattingEnabled = true;
            this.cbAudioDevice.Location = new System.Drawing.Point(12, 166);
            this.cbAudioDevice.Name = "cbAudioDevice";
            this.cbAudioDevice.Size = new System.Drawing.Size(261, 21);
            this.cbAudioDevice.TabIndex = 18;
            // 
            // cbScreenDevice
            // 
            this.cbScreenDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScreenDevice.FormattingEnabled = true;
            this.cbScreenDevice.Location = new System.Drawing.Point(12, 126);
            this.cbScreenDevice.Name = "cbScreenDevice";
            this.cbScreenDevice.Size = new System.Drawing.Size(261, 21);
            this.cbScreenDevice.TabIndex = 19;
            // 
            // lColour
            // 
            this.lColour.AutoSize = true;
            this.lColour.Location = new System.Drawing.Point(12, 48);
            this.lColour.Name = "lColour";
            this.lColour.Size = new System.Drawing.Size(37, 13);
            this.lColour.TabIndex = 20;
            this.lColour.Text = "Colour";
            // 
            // cbDefault
            // 
            this.cbDefault.AutoSize = true;
            this.cbDefault.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDefault.Location = new System.Drawing.Point(12, 90);
            this.cbDefault.Name = "cbDefault";
            this.cbDefault.Size = new System.Drawing.Size(97, 17);
            this.cbDefault.TabIndex = 21;
            this.cbDefault.Text = "Default Screen";
            this.cbDefault.UseVisualStyleBackColor = true;
            // 
            // colorDlgMain
            // 
            this.colorDlgMain.AnyColor = true;
            this.colorDlgMain.FullOpen = true;
            // 
            // tbColour
            // 
            this.tbColour.Location = new System.Drawing.Point(12, 64);
            this.tbColour.Name = "tbColour";
            this.tbColour.Size = new System.Drawing.Size(261, 20);
            this.tbColour.TabIndex = 22;
            this.tbColour.Click += new System.EventHandler(this.tbColour_Click);
            // 
            // FNewScreenDialogue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 258);
            this.Controls.Add(this.tbColour);
            this.Controls.Add(this.cbDefault);
            this.Controls.Add(this.lColour);
            this.Controls.Add(this.cbScreenDevice);
            this.Controls.Add(this.cbAudioDevice);
            this.Controls.Add(this.lAudioDevice);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.lScreenNumber);
            this.Controls.Add(this.lScreenName);
            this.Controls.Add(this.tbScreenName);
            this.Controls.Add(this.bOk);
            this.Name = "FNewScreenDialogue";
            this.Text = "Please Input";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.TextBox tbScreenName;
        private System.Windows.Forms.Label lScreenName;
        private System.Windows.Forms.Label lScreenNumber;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label lAudioDevice;
        private System.Windows.Forms.ComboBox cbAudioDevice;
        private System.Windows.Forms.ComboBox cbScreenDevice;
        private System.Windows.Forms.Label lColour;
        private System.Windows.Forms.CheckBox cbDefault;
        private System.Windows.Forms.ColorDialog colorDlgMain;
        private System.Windows.Forms.TextBox tbColour;
    }
}