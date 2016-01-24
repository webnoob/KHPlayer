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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FScreenSetup));
            this.bAddNewScreen = new System.Windows.Forms.Button();
            this.bDeleteScreen = new System.Windows.Forms.Button();
            this.lvScreens = new System.Windows.Forms.ListView();
            this.colScreen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colScreenNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAudioDevice1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bClose = new System.Windows.Forms.Button();
            this.gvScreens = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScreenDevice = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colAudioDevice = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colDefault = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bEditScreen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvScreens)).BeginInit();
            this.SuspendLayout();
            // 
            // bAddNewScreen
            // 
            this.bAddNewScreen.Location = new System.Drawing.Point(11, 113);
            this.bAddNewScreen.Name = "bAddNewScreen";
            this.bAddNewScreen.Size = new System.Drawing.Size(146, 23);
            this.bAddNewScreen.TabIndex = 10;
            this.bAddNewScreen.Text = "Add Screen";
            this.bAddNewScreen.UseVisualStyleBackColor = true;
            this.bAddNewScreen.Click += new System.EventHandler(this.bAddNewScreen_Click);
            // 
            // bDeleteScreen
            // 
            this.bDeleteScreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.bDeleteScreen.Location = new System.Drawing.Point(313, 113);
            this.bDeleteScreen.Name = "bDeleteScreen";
            this.bDeleteScreen.Size = new System.Drawing.Size(146, 23);
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
            this.colAudioDevice1});
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
            // colAudioDevice1
            // 
            this.colAudioDevice1.Width = 225;
            // 
            // bClose
            // 
            this.bClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bClose.Location = new System.Drawing.Point(11, 142);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(448, 23);
            this.bClose.TabIndex = 12;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // gvScreens
            // 
            this.gvScreens.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gvScreens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvScreens.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colScreenDevice,
            this.colAudioDevice,
            this.colDefault,
            this.colColor});
            this.gvScreens.Location = new System.Drawing.Point(212, 113);
            this.gvScreens.Name = "gvScreens";
            this.gvScreens.Size = new System.Drawing.Size(61, 50);
            this.gvScreens.TabIndex = 13;
            this.gvScreens.Visible = false;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "FriendlyName";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.Width = 90;
            // 
            // colScreenDevice
            // 
            this.colScreenDevice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colScreenDevice.DefaultCellStyle = dataGridViewCellStyle1;
            this.colScreenDevice.HeaderText = "Screen Device";
            this.colScreenDevice.Name = "colScreenDevice";
            this.colScreenDevice.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colScreenDevice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colScreenDevice.Width = 95;
            // 
            // colAudioDevice
            // 
            this.colAudioDevice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colAudioDevice.HeaderText = "Audio Device";
            this.colAudioDevice.Name = "colAudioDevice";
            this.colAudioDevice.Width = 69;
            // 
            // colDefault
            // 
            this.colDefault.HeaderText = "Default";
            this.colDefault.Name = "colDefault";
            this.colDefault.Width = 89;
            // 
            // colColor
            // 
            this.colColor.HeaderText = "Colour";
            this.colColor.Name = "colColor";
            this.colColor.Width = 90;
            // 
            // bEditScreen
            // 
            this.bEditScreen.Location = new System.Drawing.Point(162, 113);
            this.bEditScreen.Name = "bEditScreen";
            this.bEditScreen.Size = new System.Drawing.Size(145, 23);
            this.bEditScreen.TabIndex = 14;
            this.bEditScreen.Text = "Edit Screen";
            this.bEditScreen.UseVisualStyleBackColor = true;
            this.bEditScreen.Click += new System.EventHandler(this.bEditScreen_Click);
            // 
            // FScreenSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 177);
            this.Controls.Add(this.bEditScreen);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.lvScreens);
            this.Controls.Add(this.bAddNewScreen);
            this.Controls.Add(this.bDeleteScreen);
            this.Controls.Add(this.gvScreens);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FScreenSetup";
            this.Text = "Screen Setup";
            ((System.ComponentModel.ISupportInitialize)(this.gvScreens)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bAddNewScreen;
        private System.Windows.Forms.Button bDeleteScreen;
        private System.Windows.Forms.ListView lvScreens;
        private System.Windows.Forms.ColumnHeader colScreen;
        private System.Windows.Forms.ColumnHeader colScreenNum;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.ColumnHeader colAudioDevice1;
        private System.Windows.Forms.DataGridView gvScreens;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colScreenDevice;
        private System.Windows.Forms.DataGridViewComboBoxColumn colAudioDevice;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colDefault;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColor;
        private System.Windows.Forms.Button bEditScreen;

    }
}