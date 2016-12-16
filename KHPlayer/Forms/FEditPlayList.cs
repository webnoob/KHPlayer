using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KHPlayer.Classes;
using KHPlayer.Properties;
using KHPlayer.Services;
using Microsoft.VisualBasic;

namespace KHPlayer.Forms
{
    public partial class FEditPlayList : Form
    {
        private readonly PlayListService _playListService;
        private readonly PlayListItemService _playListItemService;
        private readonly SongService _songService;
        private readonly PlaylistImportExportService _playlistImportExportService;
        private readonly ScreenService _screenService;
        private readonly MaintenanceService _maintenanceService;

        public FEditPlayList()
        {
            InitializeComponent();
            
            _playListService = new PlayListService();
            _playListItemService = new PlayListItemService();
            _songService = new SongService();
            _playlistImportExportService = new PlaylistImportExportService();
            _playlistImportExportService.OnUpdateProgress += UpdateProgress;
            _screenService = new ScreenService();
            _maintenanceService = new MaintenanceService();
            
            //This is causing the application to crash when the projectror / screen is plugged in / unplugged.
            //We don't really need to offer USB import so I'm removing for now.
            //var driveDetector = new DriveDetector();
            //driveDetector.DeviceArrived += OnDriveArrived;

            fDlgPlayList.Multiselect = true;
            RefreshLists(null, null);
        }

        private void RefreshLists(object sender, EventArgs eventArgs)
        {
            LoadPlayLists();
            LoadPlayListItems();
            SetupNewSongsAndLyricsButtons();
        }

        private void UpdateProgress(object sender, ProgressEventArgs e)
        {
            var max = Convert.ToInt32(100*e.MaxItems);
            var currentOverallPercentage = Convert.ToInt32(e.CurrentItem*e.CurrentItemPercentage);

            pnlProgress.Visible = true;
            progressBarMain.Maximum = max;
            progressBarMain.Value = e.CurrentItem * 100;
            lblProgressStatus.Text = e.Status;
        }

        private async void OnDriveArrived(object sender, DriveDetectorEventArgs e)
        {
            var confirmResult = MessageBox.Show("USB device detected, would you like to import the files into current playlist?", "Import Media?", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
                await _playlistImportExportService.ImportFromDirectory(e.Drive, GetSelectedPlayList());

            LoadPlayListItems();
        }

        private void bAddVideos_Click(object sender, EventArgs e)
        {
            var result = fDlgPlayList.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var playList = GetSelectedPlayList();
                if (playList == null)
                    return;

                _playListItemService.AddRangeToPlaylist(fDlgPlayList.FileNames, playList);
            }


            LoadPlayListItems();
        }

        private PlayList GetSelectedPlayList()
        {
            var playListName = cbPlaylists.Text;
            return _playListService.GetByName(playListName);
        }

        private void LoadPlayLists()
        {
            var playLists = _playListService.Get().ToList();
            if (!playLists.Any())
            {
                _playListService.Insert(_playListService.Create(String.Format("Play List - {0}", DateTime.Now.ToString("d"))));
                playLists = _playListService.Get().ToList();
            }

            cbPlaylists.Items.Clear();
            foreach (var playList in playLists)
                cbPlaylists.Items.Add(playList.Name);

            if (cbPlaylists.Items.Count > 0)
                cbPlaylists.SelectedIndex = 0;
        }

        private void LoadPlayListItems(PlayListItem selectedItem = null)
        {
            pnlProgress.Visible = false;

            var selectedPlayList = GetSelectedPlayList();
            if (selectedPlayList == null)
                return;

            lbPlayListItems.DataSource = selectedPlayList.Items.Where(f => f != null).ToList();
            lbPlayListItems.DisplayMember = "TagName";
            lbPlayListItems.ValueMember = "FileName";

            if (lbPlayListItems.Items.Count > 0)
            {
                if (selectedItem == null)
                    lbPlayListItems.SelectedIndex = 0;
                else
                    lbPlayListItems.SelectedItem = selectedItem;
            }
            else
                pbCurrentlySelected.Image = pbCurrentlySelected.InitialImage;

        }

        private void bDeletePlayList_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to clear your play list?", "Confirm Clear!",
                MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                _playListService.Delete(GetSelectedPlayList());
                LoadPlayLists();
                LoadPlayListItems();
            }
        }

        private void bSaveAndContinue_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bAddNewPlayList_Click(object sender, EventArgs e)
        {
            var point = GetScreenPoint();
            var playListName = Interaction.InputBox("Please enter the play list name.", "Play List Name", "", point.X, point.Y);
            if (String.IsNullOrEmpty(playListName))
            {
                MessageBox.Show("No play list name entered.");
                return;
            }

            var playList = _playListService.GetByName(playListName);
            if (playList != null)
                return;

            _playListService.Insert(_playListService.Create(playListName));
            LoadPlayLists();
            cbPlaylists.Text = playListName;
            LoadPlayListItems();
        }

        private Point GetScreenPoint()
        {
            var screen = Screen.FromControl(this);
            var workingArea = screen.WorkingArea;
            var x = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - Width) / 2);
            var y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - Height) / 2);

            return new Point(x, y);
        }

        private void cbPlaylists_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPlayListItems();
        }

        private void lbPlayListItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            var playListItem = GetSelectedPlayListItem();
            if (playListItem == null)
                return;

            LoadPlayListItemDetail(playListItem);
        }

        private void LoadPlayListItemDetail(PlayListItem playListItem)
        {
            pbCurrentlySelected.Image = File.Exists(playListItem.ThumbnailPath)
                ? Image.FromFile(playListItem.ThumbnailPath)
                : pbCurrentlySelected.InitialImage;

            numGroup.Text = playListItem.Group.ToString();
            pScreenSelection.Visible = playListItem.SupportsMultiCast;
            numPdfPageNumber.Text = playListItem.PdfPageNumber.ToString();
            cbPdfView.SelectedIndex = playListItem.PdfView == "FitV" ? 1 : 0;

            if (playListItem.StartTime != null)
            {
                numStartMin.Text = playListItem.StartTime.Split(':').FirstOrDefault();
                numStartSec.Text = playListItem.StartTime.Split(':').LastOrDefault();
            }

            if (playListItem.StopTime != null)
            {
                numStopMin.Text = playListItem.StopTime.Split(':').FirstOrDefault();
                numStopSec.Text = playListItem.StopTime.Split(':').LastOrDefault();
            }

            pPdfOptions.Visible = playListItem.Type == PlayListItemType.Pdf;
            if (playListItem.SupportsMultiCast)
                LoadScreens(playListItem.Screen);
        }

        private void LoadScreens(PlayerScreen screen)
        {
            //For some reason, binding the combobox below to the items var didn't update the cb.items).
            //had to use a bindinglist and source. Strange.
            var items = _screenService.Get();
            var bindinglist = new BindingList<PlayerScreen>(items.ToList());
            var bSource = new BindingSource {DataSource = bindinglist};
            cbScreen.DataSource = bSource;
            cbScreen.DisplayMember = "FriendlyName";
            cbScreen.ValueMember = "Guid";

            if (cbScreen.Items.Count > 0)
            {
                foreach (var item in cbScreen.Items.Cast<PlayerScreen>())
                {
                    if (screen == null || item.FriendlyName == screen.FriendlyName)
                    {
                        cbScreen.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private PlayListItem GetSelectedPlayListItem()
        {
            return lbPlayListItems.SelectedItem as PlayListItem;;
        }

        private void bPlayListItemUp_Click(object sender, EventArgs e)
        {
            var selectedItem = GetSelectedPlayListItem();
            _playListService.MoveItem(selectedItem, true);
            LoadPlayListItems(selectedItem);
        }

        private void bPlayListItemDown_Click(object sender, EventArgs e)
        {
            var selectedItem = GetSelectedPlayListItem();
            _playListService.MoveItem(selectedItem, false);
            LoadPlayListItems(selectedItem);
        }

        private void bDeletePlayListItem_Click(object sender, EventArgs e)
        {
            var playListItem = GetSelectedPlayListItem();
            if (playListItem == null)
                return;

            _playListItemService.Delete(playListItem);
            LoadPlayListItems();
        }

        private void AddSongs(bool withLyrics)
        {
            var point = GetScreenPoint();
            var songList = Interaction.InputBox("Please enter your songs (split multiple song numbers with comma, space, fullstop or hyphen)", "Song Selection", "", point.X, point.Y);
            if (String.IsNullOrEmpty(songList))
                return;

            var songStrings = songList.Split(',', ' ', '.', '-').Select(s => s.Trim());
            var playList = GetSelectedPlayList();
            if (playList == null)
                return;

            var errorStr = "";
            foreach (var songStr in songStrings)
            {
                var songFilePath = _songService.GetSongFile(songStr, withLyrics);
                if (!File.Exists(songFilePath))
                {
                    errorStr += String.Format("Could not add song number {0} - Invalid Number / Song Not Found. {1}", songStr, Environment.NewLine);
                    continue;
                }

                _playListItemService.AddToPlaylistUsingFilePath(songFilePath, playList);
            }

            if (!String.IsNullOrEmpty(errorStr))
            {
                MessageBox.Show(errorStr);
                return;
            }

            LoadPlayListItems();
        }

        private void bAddSongWithLyrics_Click(object sender, EventArgs e)
        {
            AddSongs(true);
        }

        private void bAddSong_Click(object sender, EventArgs e)
        {
            AddSongs(false);
        }

        private async void playlistsFromDriveFromToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = dDlgPlaylist.ShowDialog(this);
            var path = dDlgPlaylist.SelectedPath;
            var playlist = GetSelectedPlayList();
            await _playlistImportExportService.ImportFromDirectory(path, playlist);
            LoadPlayListItems();
        }

        private void openThumbnailFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Warning: Do not delete the \"Songs\" folder as this is required.");

            var info = new ProcessStartInfo
            {
                FileName = "explorer",
                Arguments = String.Format("/e, /select, \"{0}\"", Settings.Default.ThumbnailLocation)
            };

            Process.Start(info);
        }

        private void bAddStream_Click(object sender, EventArgs e)
        {
            var point = GetScreenPoint();
            var url = Interaction.InputBox("Please enter the Url of the media you would like to stream.", "Stream Url Input", "", point.X, point.Y);
            if (string.IsNullOrEmpty(url))
                return;

            var playList = GetSelectedPlayList();
            if (playList == null)
                return;

            _playListItemService.AddToPlaylistUsingUrl(url, playList);
            LoadPlayListItems();
        }

        private void screenSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new FScreenSetup { StartPosition = FormStartPosition.CenterParent })
            {
                form.Closed += RefreshLists;
                form.ShowDialog(this);
            }
        }

        private void cbScreen_SelectedIndexChanged(object sender, EventArgs e)
        {
            var playListItem = GetSelectedPlayListItem();
            if (playListItem == null)
                return;

            playListItem.Screen = (cbScreen.SelectedItem as PlayerScreen);
            playListItem.ScreenGuid = playListItem.Screen.Guid;
        }

        private void numGroup_ValueChanged(object sender, EventArgs e)
        {
            var playListItem = GetSelectedPlayListItem();
            if (playListItem == null)
                return;

            playListItem.Group = Convert.ToInt32(numGroup.Value);
        }

        private void verifyMediaIntegrityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var results = _maintenanceService.CheckMediaFiles().ToList();
            if (results.All(r => r.Status == MaintenanceIntegrityResultStatus.Passed))
            {
                MessageBox.Show("All files Ok");
                return;
            }

            MessageBox.Show(string.Join(Environment.NewLine,
                results.Where(r => r.Status == MaintenanceIntegrityResultStatus.Failed)
                    .Select(r => string.Format("{0} - {1}", r.FilePath, r.StatusDetail))));
        }

        private void numPdfPageNumber_ValueChanged(object sender, EventArgs e)
        {
            var playListItem = GetSelectedPlayListItem();
            if (playListItem == null)
                return;

            playListItem.PdfPageNumber = Convert.ToInt32(numPdfPageNumber.Value);
        }

        private void cbPdfView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var playListItem = GetSelectedPlayListItem();
            if (playListItem == null)
                return;

            switch (cbPdfView.SelectedIndex)
            {
                case 0: playListItem.PdfView = "FitH"; break;
                case 1: playListItem.PdfView = "FitV"; break;
            }
        }

        private void numStarMin_ValueChanged(object sender, EventArgs e)
        {
            var playListItem = GetSelectedPlayListItem();
            if (playListItem == null)
                return;

            playListItem.StartMin = Convert.ToInt32(numStartMin.Value);
        }

        private void numStopMin_ValueChanged(object sender, EventArgs e)
        {
            var playListItem = GetSelectedPlayListItem();
            if (playListItem == null)
                return;

            playListItem.StopMin = Convert.ToInt32(numStopMin.Value);
        }

        private void numStartSec_ValueChanged(object sender, EventArgs e)
        {
            var playListItem = GetSelectedPlayListItem();
            if (playListItem == null)
                return;

            playListItem.StartSec = Convert.ToInt32(numStartSec.Value);
        }

        private void numStopSec_ValueChanged(object sender, EventArgs e)
        {
            var playListItem = GetSelectedPlayListItem();
            if (playListItem == null)
                return;

            playListItem.StopSec = Convert.ToInt32(numStopSec.Value);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            return;
        }

        private void SetupNewSongsAndLyricsButtons()
        {
            bAddSongWithLyrics.Enabled = Settings.Default.UseNewSongs;
            enableNewSongsToolStripMenuItem.Text = (Settings.Default.UseNewSongs ? "Disable" : "Enable") + " New Songs";
            enableLyricsOnRandomSongsToolStripMenuItem.Text = (Settings.Default.UseLyricsOnRandomSongs ? "Disable" : "Enable") + " Lyrics on Random Songs";
        }
        
        private void makeNewSongFileNamesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            _songService.RenameAllNewSongFolders();
        }

        private void enableNewSongsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Settings.Default.UseNewSongs = !Settings.Default.UseNewSongs;
            Settings.Default.Save();
            SetupNewSongsAndLyricsButtons();
        }

        private void enableLyricsOnRandomSongsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.UseLyricsOnRandomSongs = !Settings.Default.UseLyricsOnRandomSongs;
            Settings.Default.Save();
            SetupNewSongsAndLyricsButtons();
        }
    }
}
