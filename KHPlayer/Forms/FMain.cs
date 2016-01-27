using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using KHPlayer.Classes;
using KHPlayer.Helpers;
using KHPlayer.Properties;
using KHPlayer.Services;
using WMPLib;

namespace KHPlayer.Forms
{
    public partial class FMain : Form
    {
        private readonly PlayListService _playListService;
        private readonly DbService _dbService;
        private readonly SongService _songService;
        private readonly PlayListItemService _playListItemService;
        private readonly List<FPlayer> _fplayers;
        private readonly Dictionary<int, string> _groupColours; 

        private FEditPlayList _fEditPlayList;
        private List<PlayListItem> _currentPlayListItems;
        private PlayListMode _playListMode;
        private PlayMode _playMode;
        private PlayListItem _lastPlayedItem;
        
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        
        public FMain()
        {
            InitializeComponent();
            if (!Directory.Exists(Settings.Default.SongLocation))
                Directory.CreateDirectory(Settings.Default.SongLocation);

            _playListService = new PlayListService();
            _dbService = new DbService();
            _songService = new SongService();
            _playListItemService = new PlayListItemService();
            _currentPlayListItems = new List<PlayListItem>();
            _fplayers = new List<FPlayer>();
            _groupColours = new Dictionary<int, string>();

            _playListMode = PlayListMode.PlayList;
            _playMode = PlayMode.Single;

            LoadGroupColours();
            RefreshPlayLists(null, null);
            SetButtonState();
        }

        private void LoadGroupColours()
        {
            _groupColours.Add(0, ColorTranslator.ToHtml(Color.White));
            _groupColours.Add(1, ColorTranslator.ToHtml(Color.Blue));
            _groupColours.Add(2, ColorTranslator.ToHtml(Color.Red));
            _groupColours.Add(3, ColorTranslator.ToHtml(Color.Orange));
            _groupColours.Add(4, ColorTranslator.ToHtml(Color.Aqua));
            _groupColours.Add(5, ColorTranslator.ToHtml(Color.Lime));
            _groupColours.Add(6, ColorTranslator.ToHtml(Color.Yellow));
            _groupColours.Add(7, ColorTranslator.ToHtml(Color.MediumPurple));
            _groupColours.Add(8, ColorTranslator.ToHtml(Color.DeepPink));
            _groupColours.Add(9, ColorTranslator.ToHtml(Color.Gold));
            _groupColours.Add(10, ColorTranslator.ToHtml(Color.Brown));


            //Adds a slight delay on load and I really don't see us needing more than 10 groups.
            /*for (var i = 1; i < 100; i++)
            {
                _groupColours.Add(i, GetRandomColor());
            }*/
        }

        public string GetRandomColor()
        {
            while (true)
            {
                var randonGen = new Random();
                var randomColor = Color.FromArgb((byte) randonGen.Next(255), (byte) randonGen.Next(255), (byte) randonGen.Next(255), (byte) randonGen.Next(255));
                var htmlColor = ColorTranslator.ToHtml(randomColor);

                if (_groupColours.ContainsValue(htmlColor)) 
                    continue;

                return htmlColor;
                break;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            // Set window location
            Location = Settings.Default.WindowLocation;
            _dbService.Load();

            RefreshPlayLists(null, null);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            
            // Copy window location to app settings
            Settings.Default.WindowLocation = Location;
            
            var confirmResult = MessageBox.Show("Save current play lists?", "Save Play List?", MessageBoxButtons.YesNo);
            _dbService.Save(confirmResult == DialogResult.Yes);
            Settings.Default.Save();
        }

        private void PlayStateChanged(object sender, EventArgs eventArgs)
        {
            SetButtonState();
        }

        private void ReloadPlayLists()
        {
            cbPlayLists.Items.Clear();
            foreach (var playList in _playListService.Get())
                cbPlayLists.Items.Add(playList.Name);

            if (cbPlayLists.Items.Count > 0)
                cbPlayLists.SelectedIndex = 0;
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private FPlayer LaunchPlayer(PlayListItem playListItem)
        {
            if (_fplayers.Any(p => p.PlayListItem.Screen == playListItem.Screen))
                return null;

            var player = new FPlayer(playListItem);
            player.Closing += ClosePlayer;
            player.OnPlayStateChanged += PlayStateChanged;
            player.Show(this);
            _fplayers.Add(player);
            SetButtonState();
            return player;
        }

        private void ClosePlayer(object sender, EventArgs e)
        {
            //TODO: Not happy with the ClosePlayer doing stuff like SetNextVideo().
            //This should be up to stop but I'm having issues with various references
            //so leaving for now.

            var fPlayer = sender as FPlayer;
            if (fPlayer != null)
            {
                if (fPlayer.CurrentPlayState == WMPPlayState.wmppsPlaying || fPlayer.CurrentPlayState == WMPPlayState.wmppsPaused)
                    bStop_Click(sender, e);

                _currentPlayListItems.RemoveAll(p => p == fPlayer.PlayListItem);
                _lastPlayedItem = fPlayer.PlayListItem; //Get so we know where to base the next selection off.)
                fPlayer.DoBeforeClose();
                fPlayer.Dispose();
                _fplayers.RemoveAll(p => p.PlayListItem == null || p.PlayListItem.Screen == fPlayer.PlayListItem.Screen);
                SetNextVideo();
            }

            SetButtonState();
        }

        private void bEditPlayList_Click(object sender, EventArgs e)
        {
            _fEditPlayList = new FEditPlayList
            {
                StartPosition = FormStartPosition.CenterParent
            };

            _fEditPlayList.Closed += RefreshPlayLists; 
            _fEditPlayList.ShowDialog(this);
        }

        private void RefreshPlayLists(object sender, EventArgs e)
        {
            ReloadPlayLists();
            UpdatePlayListItemDisplays();
        }

        private void UpdatePlayListItemDisplays()
        {
            var playList = GetCurrentPlayList();
            if (playList == null)
                return;

            var list = GetOrderedPlayListItems(playList.Items.Where(f => f != null).ToList());
            gvPlayListItems.AutoGenerateColumns = false;
            gvPlayListItems.DataSource = list;
            
            foreach (var row in gvPlayListItems.Rows.Cast<DataGridViewRow>())
            {
                row.Selected = _currentPlayListItems.Contains(row.DataBoundItem);
                if (row.Selected)
                    BringGridIndexIntoView(row.Index);
            }

            if (gvPlayListItems.RowCount > 0 && gvPlayListItems.SelectedRows.Count == 0)
                SelectAllPlayListItemsInGroup(gvPlayListItems.Rows[0].DataBoundItem as PlayListItem);
        }

        private void BringGridIndexIntoView(int index)
        {
            gvPlayListItems.FirstDisplayedScrollingRowIndex = index > 0 ? index - 1 : index;
        }

        private static BindingList<PlayListItem> GetOrderedPlayListItems(List<PlayListItem> list)
        {
            var result = new BindingList<PlayListItem>();
            foreach (var playListItem in list.Where(playListItem => playListItem != null && !result.Contains(playListItem)))
            {
                if (playListItem.Group <= 0)
                {
                    result.Add(playListItem);
                    continue;
                }

                result.Add(playListItem);
                var item = playListItem;
                foreach (var groupedItemToAdd in list.Where(p => p != item && p.Group == item.Group))
                    result.Add(groupedItemToAdd);
            }

            return result;
        }

        private PlayList GetCurrentPlayList()
        {
            var playList = _playListService.GetByName(cbPlayLists.Text);
            return playList;
        }

        private void bPlayNext_Click(object sender, EventArgs e)
        {
            _playListMode = PlayListMode.PlayList;
            PlayNext();
        }

        private void PlayNext()
        {
            var playList = GetCurrentPlayList();
            
            if (playList != null && !playList.Items.Any() && _playListMode != PlayListMode.RandomSong)
            {
                var confirmResult = MessageBox.Show("No play list currently configured, do you want to configure one now?", "Configure Play List?",
                MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                    bEditPlayList_Click(null, null);
                else
                    return;
            }

            _currentPlayListItems = GetNextPlayListItems();
            if (!_currentPlayListItems.Any())
            {
                MessageBox.Show("No play list item selected.");
                return;
            }

            PlayCurrent();
        }

        private void PlayCurrent()
        {
            if (!_currentPlayListItems.Any())
                return;

            foreach (var playListItem in _currentPlayListItems)
            {
                var player = LaunchPlayer(playListItem);
                if (player == null)
                    continue; 

                player.PlayPlayListItem(playListItem);
                playListItem.State = PlayListItemState.Playing;
            }

            UpdatePlayListItemDisplays();
            SetButtonState();
        }

        private List<PlayListItem> GetNextPlayListItems()
        {
            if (_playListMode == PlayListMode.RandomSong)
                return new List<PlayListItem> {GetRandomSong()};

            return _playListService.GetAllPlayListItemsInGroup(GetSelectedPlayListItem()).ToList();
        }

        private PlayListItem GetRandomSong()
        {
            var songNum = _songService.GetRandomSongNumer();
            var songFilePath = _songService.GetSongFile(songNum);
            return _playListItemService.Create(songFilePath, new PlayList {Guid = Guid.NewGuid().ToString()});
        }

        private void SetNextVideo()
        {
            //We don't want to move on to the next one if one video has stopped and we still have one playing.
            if (_fplayers.Any(p => p.CurrentPlayState == WMPPlayState.wmppsPlaying) ||
                _fplayers.Any(p => p.CurrentPlayState == WMPPlayState.wmppsPaused))
            {
                return;
            }
            
            //Get the last selected item in the list (as we could have 2 selected that are grouped).
            //Select the next item in the list. 
            var index = GetNextItemIndex();
            if (_playListMode == PlayListMode.PlayList && index != gvPlayListItems.RowCount)
                index = index + 1;

            if (index == gvPlayListItems.RowCount)
                index = 0;

            gvPlayListItems.ClearSelection();
            gvPlayListItems.Rows[index].Selected = true;
            BringGridIndexIntoView(index);
            SetIntroMusicButtonText(14, "Play Random Songs");

            if (_playMode == PlayMode.AutoPlay)
                PlayNext();
        }

        private int GetNextItemIndex()
        {
            //Do the group index a little differently.
            //We can't assume that item 2 will finish last so we must get the 
            //last one in the group that is assigned to this PlayListItem
            var groupNum = _lastPlayedItem.Group;
            if (groupNum > 0)
            {
                return (
                    from row in gvPlayListItems.Rows.Cast<DataGridViewRow>().OrderByDescending(r => r.Index) 
                    let playListItem = (row.DataBoundItem as PlayListItem)
                    where playListItem != null && playListItem.Group == groupNum
                    select row.Index)
                    .FirstOrDefault();
            }

            return (
                from row in gvPlayListItems.Rows.Cast<DataGridViewRow>()
                where row.DataBoundItem == _lastPlayedItem
                select row.Index)
                .FirstOrDefault();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            if (!_fplayers.Any())
                return;

            _playMode = PlayMode.Single;
            //_lastPlayedItem = _fplayers.Last().PlayListItem; //Get so we know where to base the next selection off.

            //Don't foreach on this as .Stop() might close the player form if Settings.Default.ClosePlayerOnStop is true
            for (var i = _fplayers.Count - 1; i >= 0; i--)
            {
                _fplayers[i].PlayListItem.State = PlayListItemState.Played;
                _fplayers[i].Stop();   
            }

            /*UpdatePlayListItemDisplays();
            SetNextVideo();*/
        }

        private void bPause_Click(object sender, EventArgs e)
        {
            foreach (var player in _fplayers)
                player.Pause();
        }

        private void SetButtonState()
        {
            var showPlay = (!_fplayers.Any() ||
                _fplayers.Any(p => p.CurrentPlayState == WMPPlayState.wmppsStopped) ||
                           _fplayers.Any(p => p.CurrentPlayState == WMPPlayState.wmppsTransitioning) ||
                           _fplayers.Any(p => p.CurrentPlayState == WMPPlayState.wmppsReady) ||
                           _fplayers.Any(p => p.CurrentPlayState == WMPPlayState.wmppsUndefined));

            //Using this on the assumption that the things requiring "currentFile" will
            //only be played on their own (i.e PDFs)
            var currentFile = _currentPlayListItems.FirstOrDefault();
            if (currentFile != null)
                showPlay &= currentFile.Type != PlayListItemType.Pdf;

            bPlayNext.Visible = showPlay;
            
            bStop.Visible = !showPlay;
            bPause.Visible = !showPlay && _fplayers.Any(p => p.CurrentPlayState != WMPPlayState.wmppsPaused);
            bResume.Visible = _fplayers.Any(p => p.CurrentPlayState == WMPPlayState.wmppsPaused);
            timerVideoClock.Enabled = _fplayers.Any(p => p.CurrentPlayState == WMPPlayState.wmppsPlaying);
            bPdfScrollDown.Visible = currentFile != null && currentFile.Type == PlayListItemType.Pdf;
            bPdfScrollUp.Visible = bPdfScrollDown.Visible;
            bPdfPageUp.Visible = bPdfScrollDown.Visible;
            bPdfPageDown.Visible = bPdfScrollDown.Visible;

            if (currentFile != null)
                bPause.Visible &= currentFile.Type != PlayListItemType.Pdf;
        }

        private void bResume_Click(object sender, EventArgs e)
        {
            foreach (var player in _fplayers)
                player.Resume();
        }

        private void cbPlayLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePlayListItemDisplays();
        }

        private void gvPlayListItems_SelectionChanged(object sender, EventArgs e)
        {
            var playListItem = GetSelectedPlayListItem();
            if (playListItem == null)
                return;

            SelectAllPlayListItemsInGroup(playListItem);
        }

        private void SelectAllPlayListItemsInGroup(PlayListItem playListItem)
        {
            foreach (
                var row in
                    gvPlayListItems.Rows.Cast<DataGridViewRow>()
                        .Where(
                            r =>
                                (r.DataBoundItem as PlayListItem) == playListItem ||
                                ((r.DataBoundItem as PlayListItem).Group == playListItem.Group && playListItem.Group > 0))
                )
            {
                row.Selected = true;
            }
        }

        private PlayListItem GetSelectedPlayListItem()
        {
            return gvPlayListItems.SelectedRows.Count == 0
                ? null
                : gvPlayListItems.SelectedRows[0].DataBoundItem as PlayListItem;
        }

        private void timerVideoClock_Tick(object sender, EventArgs e)
        {
            if (_fplayers == null)
                return;

            foreach (var playListItem in _currentPlayListItems)
            {
                var player = GetPlayListItemPlayer(playListItem);
                if (player == null)
                    continue;

                player.PlayListItem.MaxTime = player.WmPlayer.currentMedia.durationString;
                player.PlayListItem.CurrentTime = player.WmPlayer.Ctlcontrols.currentPositionString;
                    

                if (_playListMode == PlayListMode.RandomSong)
                {
                    SetIntroMusicButtonText(8f, string.Format("Playing Random Song - [{0}] {1}", player.PlayListItem.Time, player.PlayListItem.TagName));
                }
                else
                {
                    foreach (
                        var row in
                            gvPlayListItems.Rows.Cast<DataGridViewRow>()
                                .Where(r => r.DataBoundItem == player.PlayListItem))
                    {
                        var rowTimeCell = row.Cells["colTime"];
                        rowTimeCell.Value = player.PlayListItem.Time;
                        gvPlayListItems.UpdateCellValue(rowTimeCell.ColumnIndex, row.Index);

                        var rowFullScreenCell = row.Cells["colFullScreen"];
                        rowFullScreenCell.Value = playListItem.FullScreen;
                        gvPlayListItems.UpdateCellValue(rowFullScreenCell.ColumnIndex, row.Index);
                    }
                }
            }
        }

        private void SetIntroMusicButtonText(float fontSize, string text)
        {
            bPlayIntroMusic.Font = new Font(bPlayIntroMusic.Font.FontFamily.Name, fontSize);
            bPlayIntroMusic.Text = text;
        }

        private FPlayer GetPlayListItemPlayer(PlayListItem playListItem)
        {
            if (playListItem == null)
                return null;

            return _fplayers.FirstOrDefault(player => player.PlayListItem.Screen == playListItem.Screen);
        }

        private void bPlayIntroMusic_Click(object sender, EventArgs e)
        {
            bStop_Click(sender, e);

            _playListMode = PlayListMode.RandomSong;
            _playMode = PlayMode.AutoPlay;
            PlayNext();
        }

        private void bHelp_Click(object sender, EventArgs e)
        {
            var filePath = PathHelper.GetApplicationPath() + "\\KHPlayer.chm";
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Help Not Found!");
                return;
            }

            var proc = new Process
            {
                StartInfo =
                {
                    FileName = filePath
                }
            
            };

            proc.Start();
        }

        private void bPdfScrollUp_Click(object sender, EventArgs e)
        {
            DoScroll(sender);
        }

        private void bPdfScollDown_Click(object sender, EventArgs e)
        {
            DoScroll(sender);
        }

        private void DoScroll(object sender)
        {
            if (_fplayers == null)
                return;

            var control = sender as Control;
            var player = _fplayers.FirstOrDefault();
            if (player == null)
                return;

            player.ScrollPdf(control != null && control.Name.Contains("Down"));
        }

        private void bPdfPageDown_Click(object sender, EventArgs e)
        {
            var player = _fplayers.FirstOrDefault();
            if (player == null)
                return;

            player.MovePage(true);
        }

        private void bPdfPageUp_Click(object sender, EventArgs e)
        {
            var player = _fplayers.FirstOrDefault();
            if (player == null)
                return;

            player.MovePage(false);
        }

        private void gvPlayListItems_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow currentRow in gvPlayListItems.Rows)
            {
                currentRow.Height = 50;

                var playListItem = currentRow.DataBoundItem as PlayListItem;
                if (playListItem == null)
                    return;

                var colour = _groupColours[playListItem.Group];
                //Colour Column
                currentRow.Cells["colGroupColour"].Style.BackColor = ColorTranslator.FromHtml(colour);
                currentRow.Cells["colGroupColour"].Style.SelectionBackColor = ColorTranslator.FromHtml(colour);
                currentRow.Cells["colImage"].Value =
                    ImageHelper.ResizeImage(playListItem.ThumbnailPath, 50, 50, true) ??
                    ImageHelper.ResizeImage(Resources.jworg, 50, 50, true);

                currentRow.Cells["colName"].Value = string.Format("[Screen - {0}][Group - {1}]{2}[{3} - {4}] - {5}",
                    playListItem.Screen != null ? playListItem.Screen.FriendlyName : "Main", playListItem.Group,
                    Environment.NewLine, playListItem.State, playListItem.Type, playListItem.TagName);

                if (playListItem.Screen != null)
                {
                    if (playListItem.Screen.ColourName != null)
                    {
                        for (var i = 1; i < currentRow.Cells.Count; i++)
                        {
                            var cellColour = ColorTranslator.FromHtml(playListItem.Screen.ColourName);
                            currentRow.Cells[i].Style.BackColor = cellColour;
                        }
                    }
                }
            }
        }

        private void gvPlayListItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvPlayListItems.Columns["colFullScreen"].Index)
            {
                if (!_fplayers.Any())
                    return;

                var playListItem = gvPlayListItems.Rows[e.RowIndex].DataBoundItem as PlayListItem;
                if (playListItem == null)
                    return;

                foreach (var player in _fplayers.Where(player => player.PlayListItem == playListItem))
                    player.SetFullScreen(playListItem.FullScreen);
            }
        }

        private void gvPlayListItems_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == gvPlayListItems.Columns["colFullScreen"].Index)
            {
                gvPlayListItems.EndEdit();
            }
        }
    }
}
