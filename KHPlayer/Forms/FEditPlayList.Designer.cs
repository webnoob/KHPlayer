namespace KHPlayer.Forms
{
    partial class FEditPlayList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FEditPlayList));
            this.bAddVideos = new System.Windows.Forms.Button();
            this.bDeletePlayList = new System.Windows.Forms.Button();
            this.fDlgPlayList = new System.Windows.Forms.OpenFileDialog();
            this.lbPlayListItems = new System.Windows.Forms.ListBox();
            this.bSaveAndContinue = new System.Windows.Forms.Button();
            this.cbPlaylists = new System.Windows.Forms.ComboBox();
            this.bAddNewPlayList = new System.Windows.Forms.Button();
            this.bDeletePlayListItem = new System.Windows.Forms.Button();
            this.bPlayListItemUp = new System.Windows.Forms.Button();
            this.bPlayListItemDown = new System.Windows.Forms.Button();
            this.bAddSong = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playlistsFromDriveFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playlistsFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playListsToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maintenanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openThumbnailFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dDlgPlaylist = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.progressBarMain = new System.Windows.Forms.ProgressBar();
            this.lblProgressStatus = new System.Windows.Forms.Label();
            this.bAddStream = new System.Windows.Forms.Button();
            this.pbCurrentlySelected = new System.Windows.Forms.PictureBox();
            this.pScreenSelection = new System.Windows.Forms.Panel();
            this.cbScreen = new System.Windows.Forms.ComboBox();
            this.lScreen = new System.Windows.Forms.Label();
            this.lGroup = new System.Windows.Forms.Label();
            this.numGroup = new System.Windows.Forms.NumericUpDown();
            this.verifyMediaIntegrityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrentlySelected)).BeginInit();
            this.pScreenSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // bAddVideos
            // 
            this.bAddVideos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bAddVideos.Location = new System.Drawing.Point(238, 56);
            this.bAddVideos.Name = "bAddVideos";
            this.bAddVideos.Size = new System.Drawing.Size(220, 64);
            this.bAddVideos.TabIndex = 2;
            this.bAddVideos.Text = "Add Video / Drama / PDF";
            this.bAddVideos.UseVisualStyleBackColor = true;
            this.bAddVideos.Click += new System.EventHandler(this.bAddVideos_Click);
            // 
            // bDeletePlayList
            // 
            this.bDeletePlayList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.bDeletePlayList.Location = new System.Drawing.Point(356, 28);
            this.bDeletePlayList.Name = "bDeletePlayList";
            this.bDeletePlayList.Size = new System.Drawing.Size(102, 22);
            this.bDeletePlayList.TabIndex = 4;
            this.bDeletePlayList.Text = "Clear Play List";
            this.bDeletePlayList.UseVisualStyleBackColor = true;
            this.bDeletePlayList.Click += new System.EventHandler(this.bDeletePlayList_Click);
            // 
            // lbPlayListItems
            // 
            this.lbPlayListItems.FormattingEnabled = true;
            this.lbPlayListItems.HorizontalScrollbar = true;
            this.lbPlayListItems.Location = new System.Drawing.Point(12, 196);
            this.lbPlayListItems.Name = "lbPlayListItems";
            this.lbPlayListItems.Size = new System.Drawing.Size(220, 134);
            this.lbPlayListItems.TabIndex = 5;
            this.lbPlayListItems.SelectedIndexChanged += new System.EventHandler(this.lbPlayListItems_SelectedIndexChanged);
            // 
            // bSaveAndContinue
            // 
            this.bSaveAndContinue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bSaveAndContinue.Location = new System.Drawing.Point(12, 126);
            this.bSaveAndContinue.Name = "bSaveAndContinue";
            this.bSaveAndContinue.Size = new System.Drawing.Size(446, 64);
            this.bSaveAndContinue.TabIndex = 6;
            this.bSaveAndContinue.Text = "Save and Return to Player";
            this.bSaveAndContinue.UseVisualStyleBackColor = true;
            this.bSaveAndContinue.Click += new System.EventHandler(this.bSaveAndContinue_Click);
            // 
            // cbPlaylists
            // 
            this.cbPlaylists.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlaylists.FormattingEnabled = true;
            this.cbPlaylists.Items.AddRange(new object[] {
            "New Play List"});
            this.cbPlaylists.Location = new System.Drawing.Point(12, 29);
            this.cbPlaylists.Name = "cbPlaylists";
            this.cbPlaylists.Size = new System.Drawing.Size(220, 21);
            this.cbPlaylists.TabIndex = 7;
            this.cbPlaylists.SelectedIndexChanged += new System.EventHandler(this.cbPlaylists_SelectedIndexChanged);
            // 
            // bAddNewPlayList
            // 
            this.bAddNewPlayList.Location = new System.Drawing.Point(238, 28);
            this.bAddNewPlayList.Name = "bAddNewPlayList";
            this.bAddNewPlayList.Size = new System.Drawing.Size(112, 23);
            this.bAddNewPlayList.TabIndex = 8;
            this.bAddNewPlayList.Text = "Add Play List";
            this.bAddNewPlayList.UseVisualStyleBackColor = true;
            this.bAddNewPlayList.Click += new System.EventHandler(this.bAddNewPlayList_Click);
            // 
            // bDeletePlayListItem
            // 
            this.bDeletePlayListItem.Location = new System.Drawing.Point(238, 307);
            this.bDeletePlayListItem.Name = "bDeletePlayListItem";
            this.bDeletePlayListItem.Size = new System.Drawing.Size(52, 23);
            this.bDeletePlayListItem.TabIndex = 13;
            this.bDeletePlayListItem.Text = "Delete";
            this.bDeletePlayListItem.UseVisualStyleBackColor = true;
            this.bDeletePlayListItem.Click += new System.EventHandler(this.bDeletePlayListItem_Click);
            // 
            // bPlayListItemUp
            // 
            this.bPlayListItemUp.Location = new System.Drawing.Point(238, 196);
            this.bPlayListItemUp.Name = "bPlayListItemUp";
            this.bPlayListItemUp.Size = new System.Drawing.Size(52, 23);
            this.bPlayListItemUp.TabIndex = 14;
            this.bPlayListItemUp.Text = "Up";
            this.bPlayListItemUp.UseVisualStyleBackColor = true;
            this.bPlayListItemUp.Click += new System.EventHandler(this.bPlayListItemUp_Click);
            // 
            // bPlayListItemDown
            // 
            this.bPlayListItemDown.Location = new System.Drawing.Point(238, 225);
            this.bPlayListItemDown.Name = "bPlayListItemDown";
            this.bPlayListItemDown.Size = new System.Drawing.Size(52, 23);
            this.bPlayListItemDown.TabIndex = 15;
            this.bPlayListItemDown.Text = "Down";
            this.bPlayListItemDown.UseVisualStyleBackColor = true;
            this.bPlayListItemDown.Click += new System.EventHandler(this.bPlayListItemDown_Click);
            // 
            // bAddSong
            // 
            this.bAddSong.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bAddSong.Location = new System.Drawing.Point(12, 56);
            this.bAddSong.Name = "bAddSong";
            this.bAddSong.Size = new System.Drawing.Size(108, 64);
            this.bAddSong.TabIndex = 16;
            this.bAddSong.Text = "Add Song(s)";
            this.bAddSong.UseVisualStyleBackColor = true;
            this.bAddSong.Click += new System.EventHandler(this.bAddSong_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.advancedToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(470, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // advancedToolStripMenuItem
            // 
            this.advancedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.maintenanceToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.screenSetupToolStripMenuItem});
            this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            this.advancedToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.advancedToolStripMenuItem.Text = "Advanced";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playlistsFromDriveFromToolStripMenuItem,
            this.playlistsFromFileToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // playlistsFromDriveFromToolStripMenuItem
            // 
            this.playlistsFromDriveFromToolStripMenuItem.Name = "playlistsFromDriveFromToolStripMenuItem";
            this.playlistsFromDriveFromToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.playlistsFromDriveFromToolStripMenuItem.Text = "Files from Drive";
            this.playlistsFromDriveFromToolStripMenuItem.Click += new System.EventHandler(this.playlistsFromDriveFromToolStripMenuItem_Click);
            // 
            // playlistsFromFileToolStripMenuItem
            // 
            this.playlistsFromFileToolStripMenuItem.Name = "playlistsFromFileToolStripMenuItem";
            this.playlistsFromFileToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.playlistsFromFileToolStripMenuItem.Text = "Playlists from File";
            this.playlistsFromFileToolStripMenuItem.Visible = false;
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playListsToFileToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Visible = false;
            // 
            // playListsToFileToolStripMenuItem
            // 
            this.playListsToFileToolStripMenuItem.Name = "playListsToFileToolStripMenuItem";
            this.playListsToFileToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.playListsToFileToolStripMenuItem.Text = "Playlists to File";
            // 
            // maintenanceToolStripMenuItem
            // 
            this.maintenanceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openThumbnailFolderToolStripMenuItem,
            this.verifyMediaIntegrityToolStripMenuItem});
            this.maintenanceToolStripMenuItem.Name = "maintenanceToolStripMenuItem";
            this.maintenanceToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.maintenanceToolStripMenuItem.Text = "Maintenance";
            // 
            // openThumbnailFolderToolStripMenuItem
            // 
            this.openThumbnailFolderToolStripMenuItem.Name = "openThumbnailFolderToolStripMenuItem";
            this.openThumbnailFolderToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.openThumbnailFolderToolStripMenuItem.Text = "Open Storage Folder in Explorer";
            this.openThumbnailFolderToolStripMenuItem.Click += new System.EventHandler(this.openThumbnailFolderToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Visible = false;
            // 
            // screenSetupToolStripMenuItem
            // 
            this.screenSetupToolStripMenuItem.Name = "screenSetupToolStripMenuItem";
            this.screenSetupToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.screenSetupToolStripMenuItem.Text = "Screen Setup";
            this.screenSetupToolStripMenuItem.Click += new System.EventHandler(this.screenSetupToolStripMenuItem_Click);
            // 
            // pnlProgress
            // 
            this.pnlProgress.Controls.Add(this.progressBarMain);
            this.pnlProgress.Controls.Add(this.lblProgressStatus);
            this.pnlProgress.Location = new System.Drawing.Point(12, 196);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Size = new System.Drawing.Size(220, 134);
            this.pnlProgress.TabIndex = 19;
            // 
            // progressBarMain
            // 
            this.progressBarMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBarMain.Location = new System.Drawing.Point(0, 0);
            this.progressBarMain.Maximum = 4;
            this.progressBarMain.Name = "progressBarMain";
            this.progressBarMain.Size = new System.Drawing.Size(220, 23);
            this.progressBarMain.TabIndex = 19;
            this.progressBarMain.Value = 1;
            // 
            // lblProgressStatus
            // 
            this.lblProgressStatus.Location = new System.Drawing.Point(3, 34);
            this.lblProgressStatus.MinimumSize = new System.Drawing.Size(214, 97);
            this.lblProgressStatus.Name = "lblProgressStatus";
            this.lblProgressStatus.Size = new System.Drawing.Size(214, 99);
            this.lblProgressStatus.TabIndex = 20;
            this.lblProgressStatus.Text = "Copying ...";
            // 
            // bAddStream
            // 
            this.bAddStream.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bAddStream.Location = new System.Drawing.Point(125, 56);
            this.bAddStream.Name = "bAddStream";
            this.bAddStream.Size = new System.Drawing.Size(108, 64);
            this.bAddStream.TabIndex = 20;
            this.bAddStream.Text = "Add URL to Stream";
            this.bAddStream.UseVisualStyleBackColor = true;
            this.bAddStream.Click += new System.EventHandler(this.bAddStream_Click);
            // 
            // pbCurrentlySelected
            // 
            this.pbCurrentlySelected.Image = global::KHPlayer.Properties.Resources.jworg;
            this.pbCurrentlySelected.InitialImage = global::KHPlayer.Properties.Resources.jworg;
            this.pbCurrentlySelected.Location = new System.Drawing.Point(238, 254);
            this.pbCurrentlySelected.Name = "pbCurrentlySelected";
            this.pbCurrentlySelected.Size = new System.Drawing.Size(52, 47);
            this.pbCurrentlySelected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCurrentlySelected.TabIndex = 12;
            this.pbCurrentlySelected.TabStop = false;
            // 
            // pScreenSelection
            // 
            this.pScreenSelection.Controls.Add(this.cbScreen);
            this.pScreenSelection.Controls.Add(this.lScreen);
            this.pScreenSelection.Controls.Add(this.lGroup);
            this.pScreenSelection.Controls.Add(this.numGroup);
            this.pScreenSelection.Location = new System.Drawing.Point(296, 196);
            this.pScreenSelection.Name = "pScreenSelection";
            this.pScreenSelection.Size = new System.Drawing.Size(162, 134);
            this.pScreenSelection.TabIndex = 25;
            // 
            // cbScreen
            // 
            this.cbScreen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScreen.FormattingEnabled = true;
            this.cbScreen.Location = new System.Drawing.Point(0, 73);
            this.cbScreen.Name = "cbScreen";
            this.cbScreen.Size = new System.Drawing.Size(162, 21);
            this.cbScreen.TabIndex = 28;
            this.cbScreen.SelectedIndexChanged += new System.EventHandler(this.cbScreen_SelectedIndexChanged);
            // 
            // lScreen
            // 
            this.lScreen.AutoSize = true;
            this.lScreen.Location = new System.Drawing.Point(-3, 57);
            this.lScreen.Name = "lScreen";
            this.lScreen.Size = new System.Drawing.Size(97, 13);
            this.lScreen.TabIndex = 27;
            this.lScreen.Text = "Screen To Play On";
            // 
            // lGroup
            // 
            this.lGroup.AutoSize = true;
            this.lGroup.Location = new System.Drawing.Point(-3, 13);
            this.lGroup.Name = "lGroup";
            this.lGroup.Size = new System.Drawing.Size(36, 13);
            this.lGroup.TabIndex = 26;
            this.lGroup.Text = "Group";
            // 
            // numGroup
            // 
            this.numGroup.Location = new System.Drawing.Point(0, 29);
            this.numGroup.Name = "numGroup";
            this.numGroup.Size = new System.Drawing.Size(162, 20);
            this.numGroup.TabIndex = 25;
            this.numGroup.ValueChanged += new System.EventHandler(this.numGroup_ValueChanged);
            // 
            // verifyMediaIntegrityToolStripMenuItem
            // 
            this.verifyMediaIntegrityToolStripMenuItem.Name = "verifyMediaIntegrityToolStripMenuItem";
            this.verifyMediaIntegrityToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.verifyMediaIntegrityToolStripMenuItem.Text = "Verify Media Integrity";
            this.verifyMediaIntegrityToolStripMenuItem.Click += new System.EventHandler(this.verifyMediaIntegrityToolStripMenuItem_Click);
            // 
            // FEditPlayList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 344);
            this.Controls.Add(this.pScreenSelection);
            this.Controls.Add(this.bAddStream);
            this.Controls.Add(this.pnlProgress);
            this.Controls.Add(this.bAddSong);
            this.Controls.Add(this.bPlayListItemDown);
            this.Controls.Add(this.bPlayListItemUp);
            this.Controls.Add(this.bDeletePlayListItem);
            this.Controls.Add(this.pbCurrentlySelected);
            this.Controls.Add(this.bAddNewPlayList);
            this.Controls.Add(this.cbPlaylists);
            this.Controls.Add(this.bSaveAndContinue);
            this.Controls.Add(this.lbPlayListItems);
            this.Controls.Add(this.bDeletePlayList);
            this.Controls.Add(this.bAddVideos);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FEditPlayList";
            this.Text = "Edit Playlists";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlProgress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrentlySelected)).EndInit();
            this.pScreenSelection.ResumeLayout(false);
            this.pScreenSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bAddVideos;
        private System.Windows.Forms.Button bDeletePlayList;
        private System.Windows.Forms.OpenFileDialog fDlgPlayList;
        private System.Windows.Forms.ListBox lbPlayListItems;
        private System.Windows.Forms.Button bSaveAndContinue;
        private System.Windows.Forms.ComboBox cbPlaylists;
        private System.Windows.Forms.Button bAddNewPlayList;
        private System.Windows.Forms.PictureBox pbCurrentlySelected;
        private System.Windows.Forms.Button bDeletePlayListItem;
        private System.Windows.Forms.Button bPlayListItemUp;
        private System.Windows.Forms.Button bPlayListItemDown;
        private System.Windows.Forms.Button bAddSong;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playListsToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playlistsFromDriveFromToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog dDlgPlaylist;
        private System.Windows.Forms.ToolStripMenuItem playlistsFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maintenanceToolStripMenuItem;
        private System.Windows.Forms.Panel pnlProgress;
        private System.Windows.Forms.Label lblProgressStatus;
        private System.Windows.Forms.ProgressBar progressBarMain;
        private System.Windows.Forms.ToolStripMenuItem openThumbnailFolderToolStripMenuItem;
        private System.Windows.Forms.Button bAddStream;
        private System.Windows.Forms.ToolStripMenuItem screenSetupToolStripMenuItem;
        private System.Windows.Forms.Panel pScreenSelection;
        private System.Windows.Forms.ComboBox cbScreen;
        private System.Windows.Forms.Label lScreen;
        private System.Windows.Forms.Label lGroup;
        private System.Windows.Forms.NumericUpDown numGroup;
        private System.Windows.Forms.ToolStripMenuItem verifyMediaIntegrityToolStripMenuItem;
    }
}