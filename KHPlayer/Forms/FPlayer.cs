using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using AxWMPLib;
using KHPlayer.Classes;
using KHPlayer.Properties;
using WMPLib;

namespace KHPlayer.Forms
{
    public partial class FPlayer : Form
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        
        private readonly FMain _parent;
        private PlayListItem _currentlyPlayListItem;
        private WMPPlayState _currentState;

        public AxWindowsMediaPlayer WmPlayer { get { return wmPlayer; } }

        public FPlayer(FMain parent, int screenNumber)
        {
            _parent = parent;

            //Do the window movement before InitializeComponent so we can ensure the window is located in the correct screen (full screen mode only).
            if (parent.FullScreen)
                WindowState = FormWindowState.Maximized;
            MoveToScreen(screenNumber);

            InitializeComponent();
            
            wmPlayer.settings.volume += 100;
            _currentState = WMPPlayState.wmppsStopped;
            StopAndHidePlayer();
        }

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            _parent.FullScreen = WindowState == FormWindowState.Maximized;
        }

        public void ShowPlayer()
        {
            wmPlayer.Visible = _currentlyPlayListItem.Type == PlayListItemType.Video;
            if (wmPlayer.Visible)
            {
                if (_currentlyPlayListItem.Type == PlayListItemType.Video)
                {
                    wmPlayer.Dock = DockStyle.Fill;
                    wmPlayer.fullScreen = _parent.FullScreen;
                }
            }

            axReader.Visible = _currentlyPlayListItem.Type == PlayListItemType.Pdf;
            if (axReader.Visible)
            {
                axReader.Dock = DockStyle.Fill;
            }
        }

        public void PlayPlayListItem(List<PlayListItem> playListItems)
        {
            foreach (var playListItem in playListItems)
                PlayPlayListItem(playListItem);
        }

        public void PlayPlayListItem(PlayListItem playListItem)
        {
            if (playListItem == null)
                return;

            _currentlyPlayListItem = playListItem;
            ShowPlayer();

            if (_currentlyPlayListItem.Type == PlayListItemType.Video || _currentlyPlayListItem.Type == PlayListItemType.Audio)
                wmPlayer.URL = playListItem.FilePath;
            else if (_currentlyPlayListItem.Type == PlayListItemType.Pdf)
            {
                axReader.LoadFile("");
                axReader.LoadFile(playListItem.FilePath);
                //This is madness. The PDF won't actually show unless the form is resized.
                //Tried a refresh but didn't help. This causes a slight flicker but it's the best I can do for now.
                var currentWindowState = WindowState;
                WindowState = FormWindowState.Minimized;
                WindowState = currentWindowState;
            }
        }

        public void Stop()
        {
            SetStateChanged(WMPPlayState.wmppsStopped);
            wmPlayer.Ctlcontrols.stop();
            SetStopped();
        }

        private void SetStopped()
        {
            wmPlayer.close();
            StopAndHidePlayer();
        }

        private void StopAndHidePlayer()
        {
            wmPlayer.uiMode = Settings.Default.UiMode;
            wmPlayer.Visible = false;
        }

        public void Pause()
        {
            wmPlayer.Ctlcontrols.pause();
        }

        public void Resume()
        {
            wmPlayer.Ctlcontrols.play();
        }

        private void wmPlayer_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            //Don't use this event as it gets confused.
            //SetStateChanged(wmPlayer.playState);
        }

        private void timerPlayerStateChange_Tick(object sender, EventArgs e)
        {
            if (wmPlayer.playState == _currentState)
                return;

            SetStateChanged(wmPlayer.playState);
        }

        private void SetStateChanged(WMPPlayState playState)
        {
            _parent.VideoState = playState;
            _currentState = playState;

            switch (wmPlayer.playState)
            {
                case WMPPlayState.wmppsUndefined:
                    break;
                case WMPPlayState.wmppsStopped:
                    SetStopped();
                    break;
                case WMPPlayState.wmppsPaused:
                    ;
                    break;
                case WMPPlayState.wmppsPlaying:
                    ShowPlayer();
                    break;
                case WMPPlayState.wmppsScanForward:
                    break;
                case WMPPlayState.wmppsScanReverse:
                    break;
                case WMPPlayState.wmppsBuffering:
                    ;
                    break;
                case WMPPlayState.wmppsWaiting:
                    ;
                    break;
                case WMPPlayState.wmppsMediaEnded:
                    SetStopped();
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
        }

        public void SetFullScreen(bool fullscreen)
        {
            if (wmPlayer.playState == WMPPlayState.wmppsPlaying || wmPlayer.playState == WMPPlayState.wmppsPaused)
            {
                if (_currentlyPlayListItem.Type == PlayListItemType.Video)
                    wmPlayer.fullScreen = fullscreen;
            }
        }

        public void MoveToScreen(int screenNumber)
        {
            if (screenNumber < Screen.AllScreens.Count())
            {
                Location = Screen.AllScreens[screenNumber].Bounds.Location;
            }
        }

        public void DoBeforeClose()
        {
            if (axReader != null)
            {
                //The following lines are required in order to close the adobe PDF form viewer properly.
                //Not sure why but the application hangs when closing without these.
                axReader.Dispose();
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void FPlayer_Load(object sender, EventArgs e)
        {

        }

        public void ScrollPdf(ArrangeDirection direction)
        {
            if (_currentlyPlayListItem == null || _currentlyPlayListItem.Type != PlayListItemType.Pdf)
                return;
            
            //Do something to scroll the PDF content down.
        }
    }
}
