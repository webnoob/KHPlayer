﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using KHPlayer.Classes;
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
        private FEditPlayList _fEditPlayList;
        private List<PlayListItem> _currentPlayListItems;
        private WMPPlayState _currentVideoState;
        private PlayListMode _playListMode;
        private PlayMode _playMode;
        
        public bool FullScreen { get { return cbFullScreen.Checked; } set { cbFullScreen.Checked = value; } }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        
        public WMPPlayState VideoState
        {
            get { return _currentVideoState; }
            set
            {
                _currentVideoState = value;
                PlayStateChanged();
            }
        }

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

            _currentVideoState = WMPPlayState.wmppsStopped;
            _playListMode = PlayListMode.PlayList;
            _playMode = PlayMode.Single;

            //Commenting this option out for now as it's hard to use when in the hall.
            //If the projector is unplugged, it resets the optoin to zero which means it resets your
            //saved value on close.
            //numScreen.Maximum = Screen.AllScreens.Count() - 1;
            
            RefreshPlayLists(null, null);
            SetButtonState();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            // Set window location
            Location = Settings.Default.WindowLocation;
            cbFullScreen.Checked = Settings.Default.FullScreen;
            _dbService.Load();

            RefreshPlayLists(null, null);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            
            // Copy window location to app settings
            Settings.Default.WindowLocation = Location;
            Settings.Default.FullScreen = cbFullScreen.Checked;

            if (_fEditPlayList != null)
            {
                var confirmResult = MessageBox.Show("Save current play lists?", "Save Play List?",
                    MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                    _dbService.SaveOnOk();
            }

            _dbService.Save();
            Settings.Default.Save();
        }

        private void PlayStateChanged()
        {
            switch (_currentVideoState)
            {
                case WMPPlayState.wmppsStopped:
                    SetNextVideo();
                    break;
                case WMPPlayState.wmppsMediaEnded:
                    SetNextVideo();
                    break;
            }

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

        private void bLaunch_Click(object sender, EventArgs e)
        {
            LaunchPlayer(null);
        }

        private FPlayer LaunchPlayer(PlayerScreen screen)
        {
            if (_fplayers.Any(p => p.PlayerScreen == screen))
                return null;

            var player = new FPlayer(this, screen);
            player.Closing += ClosePlayer;
            player.Show(this);
            _fplayers.Add(player);
            SetButtonState();
            return player;
        }

        private void ClosePlayer(object sender, EventArgs e)
        {
            if (_currentVideoState == WMPPlayState.wmppsPlaying || _currentVideoState == WMPPlayState.wmppsPaused)
                bStop_Click(sender, e);
            
            _currentPlayListItems.Clear();
            var fplayer = sender as FPlayer;
            if (fplayer != null)
            {
                fplayer.DoBeforeClose();
                fplayer.Dispose();
                _fplayers.RemoveAll(p => p.PlayerScreen == fplayer.PlayerScreen);
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

            lbPlayListItems.DataSource = playList.Items.Where(f => f != null).ToList();
            lbPlayListItems.DisplayMember = "TagName";
            lbPlayListItems.ValueMember = "FileName";

            if (lbPlayListItems.Items.Count == 0)
            {
                //Selected index change won't be triggered so make sure the image resets.
                pbCurrentlySelected.Image = pbCurrentlySelected.InitialImage;
                return;
            }

            if (!_currentPlayListItems.Any())
                lbPlayListItems.SelectedIndex = 0;
            else
            {
                for (int i = 0; i < lbPlayListItems.Items.Count; i++)
                {
                    var playListItem = lbPlayListItems.Items[i] as PlayListItem;
                    if (playListItem == null)
                        continue;

                    if (playListItem == _currentPlayListItems.FirstOrDefault())
                    {
                        if (lbPlayListItems.Items.Count >= i)
                            lbPlayListItems.SelectedIndex = i;
                        break;
                    }
                }
            }
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
                var player = LaunchPlayer(playListItem.Screen);
                player.PlayPlayListItem(playListItem);
            }

            UpdatePlayListItemDisplays();
            SetButtonState();
        }

        private List<PlayListItem> GetNextPlayListItems()
        {
            if (_playListMode == PlayListMode.RandomSong)
                return new List<PlayListItem> {GetRandomSong()};

            return _playListService.GetAllPlatListItemsInGroup(GetSelectedPlayListItem()).ToList();
        }

        private PlayListItem GetRandomSong()
        {
            var songNum = _songService.GetRandomSongNumer();
            var songFilePath = _songService.GetSongFile(songNum);
            return _playListItemService.Create(songFilePath, new PlayList {Guid = Guid.NewGuid().ToString()});
        }

        private void SetNextVideo()
        {
            if (_playListMode == PlayListMode.PlayList && lbPlayListItems.SelectedIndex != lbPlayListItems.Items.Count - 1)
            {
                var nextIndex = lbPlayListItems.SelectedIndex + 1;
                lbPlayListItems.SelectedIndex = nextIndex >= lbPlayListItems.Items.Count
                    ? 0
                    : nextIndex;
            }
            else if (lbPlayListItems.Items.Count > 0)
                lbPlayListItems.SelectedIndex = 0;

            lblNowPlaying.Text = "Nothing currently playing.";

            if (_playMode == PlayMode.AutoPlay)
                PlayNext();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            _playMode = PlayMode.Single;

            //Don't foreach on this as .Stop() might close the player form if Settings.Default.ClosePlayerOnStop is true
            for (var i = _fplayers.Count - 1; i >= 0; i--)
                _fplayers[i].Stop();
        }

        private void bPause_Click(object sender, EventArgs e)
        {
            foreach (var player in _fplayers)
                player.Pause();
        }

        private void SetButtonState()
        {
            var showPlay = (_currentVideoState == WMPPlayState.wmppsStopped ||
                           _currentVideoState == WMPPlayState.wmppsTransitioning ||
                           _currentVideoState == WMPPlayState.wmppsReady ||
                           _currentVideoState == WMPPlayState.wmppsUndefined);

            var currentFile = _currentPlayListItems.FirstOrDefault();
            
            if (currentFile != null)
                showPlay &= currentFile.Type != PlayListItemType.Pdf;

            bPlayNext.Visible = showPlay;
            
            bStop.Visible = !showPlay;
            bPause.Visible = !showPlay && _currentVideoState != WMPPlayState.wmppsPaused;
            bResume.Visible = _currentVideoState == WMPPlayState.wmppsPaused;
            timerVideoClock.Enabled = _currentVideoState == WMPPlayState.wmppsPlaying;
            bClosePlayerWindow.Visible = _fplayers != null;
            bLaunch.Visible = _fplayers == null;
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
            return lbPlayListItems.SelectedItem as PlayListItem;
        }

        private void cbFullScreen_CheckedChanged(object sender, EventArgs e)
        {
            if (!_fplayers.Any()) 
                return;

            foreach (var player in _fplayers)
                player.SetFullScreen(cbFullScreen.Checked);
        }

        private void timerVideoClock_Tick(object sender, EventArgs e)
        {
            if (_fplayers == null)
                return;

            lblNowPlaying.Text = "";

            foreach (var playListItem in _currentPlayListItems)
            {
                var player = GetPlayListItemPlayer(playListItem);
                if (player == null)
                    continue;

                lblNowPlaying.Text += String.Format("Playing: {0} [{1} / {2}] {3}", playListItem.TagName,
                    player.WmPlayer.Ctlcontrols.currentPositionString, player.WmPlayer.currentMedia.durationString, Environment.NewLine);
            }
        }

        private FPlayer GetPlayListItemPlayer(PlayListItem playListItem)
        {
            if (playListItem == null)
                return null;

            return _fplayers.FirstOrDefault(player => player.PlayerScreen == playListItem.Screen);
        }

        private void bClosePlayerWindow_Click(object sender, EventArgs e)
        {
            ClosePlayer(sender, e);
        }

        private void bPlayIntroMusic_Click(object sender, EventArgs e)
        {
            _playListMode = PlayListMode.RandomSong;
            _playMode = PlayMode.AutoPlay;
            PlayNext();
        }

        private void bHelp_Click(object sender, EventArgs e)
        {
            var proc = new Process
            {
                StartInfo =
                {
                    FileName = PathHelper.GetApplicationPath() + "\\KHPlayer.chm"
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
    }
}
