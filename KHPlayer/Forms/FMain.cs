using System;
using System.ComponentModel;
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
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int WM_APPCOMMAND = 0x319;

        private readonly PlayListService _playListService;
        private readonly DbService _dbService;

        private FPlayer _fplayer;
        private FEditPlayList _fEditPlayList;
        private PlayListItem _currentFile;
        private WMPPlayState _currentVideoState;
        
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
            _playListService = new PlayListService();
            _dbService = new DbService();

            if (!Directory.Exists(Settings.Default.SongLocation))
                Directory.CreateDirectory(Settings.Default.SongLocation);

            _currentVideoState = WMPPlayState.wmppsStopped;
            numScreen.Maximum = Screen.AllScreens.Count() - 1;

            RefreshPlayLists(null, null);
            SetButtonState();
        }

        //Not using this method at the moment as I'm not convinced about the reliability with not being able to check if sound is muted first.
        //I need a Mute() and UnMute().
        private void ToggleMute()
        {
            SendMessageW(Handle, WM_APPCOMMAND, Handle, (IntPtr)APPCOMMAND_VOLUME_MUTE);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            // Set window location
            Location = Settings.Default.WindowLocation;
            cbFullScreen.Checked = Settings.Default.FullScreen;
            numScreen.Text = Settings.Default.LaunchScreen;
            numScreen.Enabled = cbFullScreen.Checked;
            _dbService.Load();

            RefreshPlayLists(null, null);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            
            // Copy window location to app settings
            Settings.Default.WindowLocation = Location;
            Settings.Default.FullScreen = cbFullScreen.Checked;
            Settings.Default.LaunchScreen = numScreen.Text;

            if (_fEditPlayList != null)
            {
                var confirmResult = MessageBox.Show("Save current play lists?", "Save Play List?",
                    MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                    _dbService.Save();
            }

            Settings.Default.Save();
        }

        private void PlayStateChanged()
        {
            switch (_currentVideoState)
            {
                case WMPPlayState.wmppsUndefined:
                    break;
                case WMPPlayState.wmppsStopped:
                    SetNextVideo();
                    break;
                case WMPPlayState.wmppsPaused:
                    break;
                case WMPPlayState.wmppsPlaying:
                    break;
                case WMPPlayState.wmppsScanForward:
                    break;
                case WMPPlayState.wmppsScanReverse:
                    break;
                case WMPPlayState.wmppsBuffering:
                    break;
                case WMPPlayState.wmppsWaiting:
                    break;
                case WMPPlayState.wmppsMediaEnded:
                    SetNextVideo();
                    break;
                case WMPPlayState.wmppsTransitioning:
                    break;
                case WMPPlayState.wmppsReady:
                    break;
                case WMPPlayState.wmppsReconnecting:
                    break;
                case WMPPlayState.wmppsLast:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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
            if (_fplayer == null)
            {
                _fplayer = new FPlayer(this, Convert.ToInt32(numScreen.Text));
                _fplayer.Closed += PlayerClosed;
                _fplayer.Show(this);
                SetButtonState();
            }
        }

        private void PlayerClosed(object sender, EventArgs e)
        {
            _fplayer.Stop();
            _fplayer.Dispose();
            _fplayer = null;
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
                return;

            if (_currentFile == null)
                lbPlayListItems.SelectedIndex = 0;
            else
            {
                for (int i = 0; i < lbPlayListItems.Items.Count; i++)
                {
                    var playListItem = lbPlayListItems.Items[i] as PlayListItem;
                    if (playListItem == null)
                        continue;

                    if (playListItem == _currentFile)
                    {
                        if (lbPlayListItems.Items.Count >= i)
                            lbPlayListItems.SelectedIndex = i;
                        /*else
                            lbPlayListItems.SelectedIndex = 0;*/
                        break;
                    }
                }
            }
        }

        private PlayList GetCurrentPlayList()
        {
            return _playListService.GetByName(cbPlayLists.Text);
        }

        private void bPlayNext_Click(object sender, EventArgs e)
        {
            var playList = GetCurrentPlayList();

            if (!playList.Items.Any())
            {
                var confirmResult = MessageBox.Show("No play list currently configured, do you want to configure one now?", "Configure Play List?",
                MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                    bEditPlayList_Click(sender, e);
                else
                    return;
            }

            _currentFile = GetNextVideo();
            if (_currentFile == null)
            {
                MessageBox.Show("No play list item selected.");
                return;
            }

            if (_fplayer == null)
                bLaunch_Click(sender, e);
            
            if (_fplayer == null)
                return;

            PlayCurrentFile();
        }

        private void PlayCurrentFile()
        {
            _fplayer.PlayPlayListItem(_currentFile);
            UpdatePlayListItemDisplays();
        }

        private PlayListItem GetNextVideo()
        {
            return GetSelectedPlayListItem();
        }

        private void SetNextVideo()
        {
            if (lbPlayListItems.SelectedIndex != lbPlayListItems.Items.Count - 1)
            {
                var nextIndex = lbPlayListItems.SelectedIndex + 1;
                lbPlayListItems.SelectedIndex = nextIndex >= lbPlayListItems.Items.Count
                    ? 0
                    : nextIndex;
            }
            else
                lbPlayListItems.SelectedIndex = -1;

            lblNowPlaying.Text = "Nothing currently playing.";
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            _fplayer.Stop();
        }

        private void bPause_Click(object sender, EventArgs e)
        {
            _fplayer.Pause();
        }

        private void SetButtonState()
        {
            var showPlay = _currentVideoState == WMPPlayState.wmppsStopped ||
                           _currentVideoState == WMPPlayState.wmppsTransitioning ||
                           _currentVideoState == WMPPlayState.wmppsReady;

            bPlayNext.Visible = showPlay;
            
            bStop.Visible = !showPlay;
            bPause.Visible = !showPlay && _currentVideoState != WMPPlayState.wmppsPaused;
            bResume.Visible = _currentVideoState == WMPPlayState.wmppsPaused;
            timerVideoClock.Enabled = _currentVideoState == WMPPlayState.wmppsPlaying;
            bClosePlayerWindow.Visible = _fplayer != null;
            bLaunch.Visible = _fplayer == null;
        }

        private void bResume_Click(object sender, EventArgs e)
        {
            _fplayer.Resume();
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
            if (_fplayer != null)
                _fplayer.SetFullScreen(cbFullScreen.Checked);

            numScreen.Enabled = cbFullScreen.Checked;
        }

        private void timerVideoClock_Tick(object sender, EventArgs e)
        {
            if (_fplayer == null)
                return;

            lblNowPlaying.Text = String.Format("Playing: {0} [{1} / {2}]", _currentFile.TagName, _fplayer.WmPlayer.Ctlcontrols.currentPositionString, _fplayer.WmPlayer.currentMedia.durationString);
        }

        private void numScreen_ValueChanged(object sender, EventArgs e)
        {
            if (_fplayer == null)
                return;

            _fplayer.MoveToScreen(Convert.ToInt32(numScreen.Text));
        }

        private void bClosePlayerWindow_Click(object sender, EventArgs e)
        {
            PlayerClosed(sender, e);
        }
    }
}
