using System;
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

        public FEditPlayList()
        {
            InitializeComponent();
            
            _playListService = new PlayListService();
            _playListItemService = new PlayListItemService();
            _songService = new SongService();
            _playlistImportExportService = new PlaylistImportExportService();
            _playlistImportExportService.OnUpdateProgress += UpdateProgress;
            
            var driveDetector = new DriveDetector();
            driveDetector.DeviceArrived += OnDriveArrived;

            fDlgPlayList.Multiselect = true;
            
            LoadPlayLists();
            LoadPlayListItems();
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

            pbCurrentlySelected.Image = File.Exists(playListItem.ThumbnailPath)
                ? Image.FromFile(playListItem.ThumbnailPath)
                : pbCurrentlySelected.InitialImage;
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

        private void bAddSong_Click(object sender, EventArgs e)
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
                var songFilePath = _songService.GetSongFile(songStr);
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
    }
}
