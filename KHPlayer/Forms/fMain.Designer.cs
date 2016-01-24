namespace KHPlayer.Forms
{
    partial class FMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
            this.bExit = new System.Windows.Forms.Button();
            this.bLaunch = new System.Windows.Forms.Button();
            this.bEditPlayList = new System.Windows.Forms.Button();
            this.bPlayNext = new System.Windows.Forms.Button();
            this.lbPlayListItems = new System.Windows.Forms.ListBox();
            this.bStop = new System.Windows.Forms.Button();
            this.bPause = new System.Windows.Forms.Button();
            this.bResume = new System.Windows.Forms.Button();
            this.cbPlayLists = new System.Windows.Forms.ComboBox();
            this.pbCurrentlySelected = new System.Windows.Forms.PictureBox();
            this.cbFullScreen = new System.Windows.Forms.CheckBox();
            this.timerVideoClock = new System.Windows.Forms.Timer(this.components);
            this.lblNowPlaying = new System.Windows.Forms.Label();
            this.bClosePlayerWindow = new System.Windows.Forms.Button();
            this.bPlayIntroMusic = new System.Windows.Forms.Button();
            this.bHelp = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.bPdfScrollDown = new System.Windows.Forms.Button();
            this.bPdfScrollUp = new System.Windows.Forms.Button();
            this.bPdfPageUp = new System.Windows.Forms.Button();
            this.bPdfPageDown = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrentlySelected)).BeginInit();
            this.SuspendLayout();
            // 
            // bExit
            // 
            this.bExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bExit.Location = new System.Drawing.Point(11, 573);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(447, 64);
            this.bExit.TabIndex = 0;
            this.bExit.Text = "Exit";
            this.bExit.UseVisualStyleBackColor = true;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // bLaunch
            // 
            this.bLaunch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bLaunch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bLaunch.Location = new System.Drawing.Point(12, 12);
            this.bLaunch.Name = "bLaunch";
            this.bLaunch.Size = new System.Drawing.Size(446, 64);
            this.bLaunch.TabIndex = 1;
            this.bLaunch.Text = "Launch Player Window";
            this.bLaunch.UseVisualStyleBackColor = false;
            this.bLaunch.Click += new System.EventHandler(this.bLaunch_Click);
            // 
            // bEditPlayList
            // 
            this.bEditPlayList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bEditPlayList.Location = new System.Drawing.Point(11, 363);
            this.bEditPlayList.Name = "bEditPlayList";
            this.bEditPlayList.Size = new System.Drawing.Size(447, 64);
            this.bEditPlayList.TabIndex = 2;
            this.bEditPlayList.Text = "Edit Playlists";
            this.bEditPlayList.UseVisualStyleBackColor = true;
            this.bEditPlayList.Click += new System.EventHandler(this.bEditPlayList_Click);
            // 
            // bPlayNext
            // 
            this.bPlayNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bPlayNext.Location = new System.Drawing.Point(11, 82);
            this.bPlayNext.Name = "bPlayNext";
            this.bPlayNext.Size = new System.Drawing.Size(447, 64);
            this.bPlayNext.TabIndex = 3;
            this.bPlayNext.Text = "Play";
            this.bPlayNext.UseVisualStyleBackColor = true;
            this.bPlayNext.Click += new System.EventHandler(this.bPlayNext_Click);
            // 
            // lbPlayListItems
            // 
            this.lbPlayListItems.FormattingEnabled = true;
            this.lbPlayListItems.HorizontalScrollbar = true;
            this.lbPlayListItems.Location = new System.Drawing.Point(12, 179);
            this.lbPlayListItems.Name = "lbPlayListItems";
            this.lbPlayListItems.Size = new System.Drawing.Size(445, 108);
            this.lbPlayListItems.TabIndex = 6;
            this.lbPlayListItems.SelectedIndexChanged += new System.EventHandler(this.lbPlayListItems_SelectedIndexChanged);
            // 
            // bStop
            // 
            this.bStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bStop.Location = new System.Drawing.Point(11, 82);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(219, 64);
            this.bStop.TabIndex = 7;
            this.bStop.Text = "Stop";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bPause
            // 
            this.bPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bPause.Location = new System.Drawing.Point(237, 82);
            this.bPause.Name = "bPause";
            this.bPause.Size = new System.Drawing.Size(220, 64);
            this.bPause.TabIndex = 8;
            this.bPause.Text = "Pause";
            this.bPause.UseVisualStyleBackColor = true;
            this.bPause.Click += new System.EventHandler(this.bPause_Click);
            // 
            // bResume
            // 
            this.bResume.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bResume.Location = new System.Drawing.Point(237, 82);
            this.bResume.Name = "bResume";
            this.bResume.Size = new System.Drawing.Size(220, 64);
            this.bResume.TabIndex = 9;
            this.bResume.Text = "Resume";
            this.bResume.UseVisualStyleBackColor = true;
            this.bResume.Click += new System.EventHandler(this.bResume_Click);
            // 
            // cbPlayLists
            // 
            this.cbPlayLists.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlayLists.FormattingEnabled = true;
            this.cbPlayLists.ItemHeight = 13;
            this.cbPlayLists.Location = new System.Drawing.Point(12, 152);
            this.cbPlayLists.Name = "cbPlayLists";
            this.cbPlayLists.Size = new System.Drawing.Size(445, 21);
            this.cbPlayLists.TabIndex = 10;
            this.cbPlayLists.SelectedIndexChanged += new System.EventHandler(this.cbPlayLists_SelectedIndexChanged);
            // 
            // pbCurrentlySelected
            // 
            this.pbCurrentlySelected.Image = global::KHPlayer.Properties.Resources.jworg;
            this.pbCurrentlySelected.InitialImage = global::KHPlayer.Properties.Resources.jworg;
            this.pbCurrentlySelected.Location = new System.Drawing.Point(11, 293);
            this.pbCurrentlySelected.Name = "pbCurrentlySelected";
            this.pbCurrentlySelected.Size = new System.Drawing.Size(52, 52);
            this.pbCurrentlySelected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCurrentlySelected.TabIndex = 11;
            this.pbCurrentlySelected.TabStop = false;
            // 
            // cbFullScreen
            // 
            this.cbFullScreen.AutoSize = true;
            this.cbFullScreen.Checked = true;
            this.cbFullScreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFullScreen.Location = new System.Drawing.Point(69, 293);
            this.cbFullScreen.Name = "cbFullScreen";
            this.cbFullScreen.Size = new System.Drawing.Size(102, 17);
            this.cbFullScreen.TabIndex = 12;
            this.cbFullScreen.Text = "Play Full Screen";
            this.cbFullScreen.UseVisualStyleBackColor = true;
            this.cbFullScreen.CheckedChanged += new System.EventHandler(this.cbFullScreen_CheckedChanged);
            // 
            // timerVideoClock
            // 
            this.timerVideoClock.Interval = 500;
            this.timerVideoClock.Tick += new System.EventHandler(this.timerVideoClock_Tick);
            // 
            // lblNowPlaying
            // 
            this.lblNowPlaying.AutoSize = true;
            this.lblNowPlaying.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblNowPlaying.Location = new System.Drawing.Point(65, 313);
            this.lblNowPlaying.MaximumSize = new System.Drawing.Size(446, 0);
            this.lblNowPlaying.MinimumSize = new System.Drawing.Size(390, 0);
            this.lblNowPlaying.Name = "lblNowPlaying";
            this.lblNowPlaying.Size = new System.Drawing.Size(390, 20);
            this.lblNowPlaying.TabIndex = 16;
            this.lblNowPlaying.Text = "Nothing currently playing.";
            // 
            // bClosePlayerWindow
            // 
            this.bClosePlayerWindow.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bClosePlayerWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.bClosePlayerWindow, "Tester");
            this.bClosePlayerWindow.Location = new System.Drawing.Point(11, 12);
            this.bClosePlayerWindow.Name = "bClosePlayerWindow";
            this.helpProvider1.SetShowHelp(this.bClosePlayerWindow, true);
            this.bClosePlayerWindow.Size = new System.Drawing.Size(447, 64);
            this.bClosePlayerWindow.TabIndex = 17;
            this.bClosePlayerWindow.Text = "Close Player Window";
            this.bClosePlayerWindow.UseVisualStyleBackColor = false;
            this.bClosePlayerWindow.Click += new System.EventHandler(this.bClosePlayerWindow_Click);
            // 
            // bPlayIntroMusic
            // 
            this.bPlayIntroMusic.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bPlayIntroMusic.Location = new System.Drawing.Point(11, 433);
            this.bPlayIntroMusic.Name = "bPlayIntroMusic";
            this.bPlayIntroMusic.Size = new System.Drawing.Size(447, 64);
            this.bPlayIntroMusic.TabIndex = 18;
            this.bPlayIntroMusic.Text = "Play Random Songs";
            this.bPlayIntroMusic.UseVisualStyleBackColor = true;
            this.bPlayIntroMusic.Click += new System.EventHandler(this.bPlayIntroMusic_Click);
            // 
            // bHelp
            // 
            this.bHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bHelp.Location = new System.Drawing.Point(11, 503);
            this.bHelp.Name = "bHelp";
            this.bHelp.Size = new System.Drawing.Size(447, 64);
            this.bHelp.TabIndex = 19;
            this.bHelp.Text = "Help";
            this.bHelp.UseVisualStyleBackColor = true;
            this.bHelp.Click += new System.EventHandler(this.bHelp_Click);
            // 
            // bPdfScrollDown
            // 
            this.bPdfScrollDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bPdfScrollDown.Location = new System.Drawing.Point(350, 82);
            this.bPdfScrollDown.Name = "bPdfScrollDown";
            this.bPdfScrollDown.Size = new System.Drawing.Size(108, 30);
            this.bPdfScrollDown.TabIndex = 20;
            this.bPdfScrollDown.Text = "Scroll Down";
            this.bPdfScrollDown.UseVisualStyleBackColor = true;
            this.bPdfScrollDown.Click += new System.EventHandler(this.bPdfScollDown_Click);
            // 
            // bPdfScrollUp
            // 
            this.bPdfScrollUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bPdfScrollUp.Location = new System.Drawing.Point(237, 82);
            this.bPdfScrollUp.Name = "bPdfScrollUp";
            this.bPdfScrollUp.Size = new System.Drawing.Size(108, 30);
            this.bPdfScrollUp.TabIndex = 21;
            this.bPdfScrollUp.Text = "Scroll Up";
            this.bPdfScrollUp.UseVisualStyleBackColor = true;
            this.bPdfScrollUp.Click += new System.EventHandler(this.bPdfScrollUp_Click);
            // 
            // bPdfPageUp
            // 
            this.bPdfPageUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bPdfPageUp.Location = new System.Drawing.Point(237, 116);
            this.bPdfPageUp.Name = "bPdfPageUp";
            this.bPdfPageUp.Size = new System.Drawing.Size(108, 30);
            this.bPdfPageUp.TabIndex = 22;
            this.bPdfPageUp.Text = "Page Up";
            this.bPdfPageUp.UseVisualStyleBackColor = true;
            this.bPdfPageUp.Click += new System.EventHandler(this.bPdfPageUp_Click);
            // 
            // bPdfPageDown
            // 
            this.bPdfPageDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bPdfPageDown.Location = new System.Drawing.Point(350, 116);
            this.bPdfPageDown.Name = "bPdfPageDown";
            this.bPdfPageDown.Size = new System.Drawing.Size(108, 30);
            this.bPdfPageDown.TabIndex = 23;
            this.bPdfPageDown.Text = "Page Down";
            this.bPdfPageDown.UseVisualStyleBackColor = true;
            this.bPdfPageDown.Click += new System.EventHandler(this.bPdfPageDown_Click);
            // 
            // FMain
            // 
            this.ClientSize = new System.Drawing.Size(470, 650);
            this.Controls.Add(this.bPdfPageDown);
            this.Controls.Add(this.bPdfPageUp);
            this.Controls.Add(this.bPdfScrollUp);
            this.Controls.Add(this.bPdfScrollDown);
            this.Controls.Add(this.bHelp);
            this.Controls.Add(this.bPlayIntroMusic);
            this.Controls.Add(this.bClosePlayerWindow);
            this.Controls.Add(this.lblNowPlaying);
            this.Controls.Add(this.cbFullScreen);
            this.Controls.Add(this.bPlayNext);
            this.Controls.Add(this.pbCurrentlySelected);
            this.Controls.Add(this.cbPlayLists);
            this.Controls.Add(this.bResume);
            this.Controls.Add(this.bPause);
            this.Controls.Add(this.bStop);
            this.Controls.Add(this.lbPlayListItems);
            this.Controls.Add(this.bEditPlayList);
            this.Controls.Add(this.bLaunch);
            this.Controls.Add(this.bExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FMain";
            this.Text = "KHPlayer";
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrentlySelected)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bExit;
        private System.Windows.Forms.Button bLaunch;
        private System.Windows.Forms.Button bEditPlayList;
        private System.Windows.Forms.Button bPlayNext;
        private System.Windows.Forms.ListBox lbPlayListItems;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Button bPause;
        private System.Windows.Forms.Button bResume;
        private System.Windows.Forms.ComboBox cbPlayLists;
        private System.Windows.Forms.PictureBox pbCurrentlySelected;
        private System.Windows.Forms.CheckBox cbFullScreen;
        private System.Windows.Forms.Timer timerVideoClock;
        private System.Windows.Forms.Label lblNowPlaying;
        private System.Windows.Forms.Button bClosePlayerWindow;
        private System.Windows.Forms.Button bPlayIntroMusic;
        private System.Windows.Forms.Button bHelp;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Button bPdfScrollDown;
        private System.Windows.Forms.Button bPdfScrollUp;
        private System.Windows.Forms.Button bPdfPageUp;
        private System.Windows.Forms.Button bPdfPageDown;
    }
}

