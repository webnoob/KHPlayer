﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AxWMPLib;
using KHPlayer.Classes;
using KHPlayer.Properties;
using WMPLib;

namespace KHPlayer.Forms
{
    public partial class FPlayer : Form
    {
        private readonly FMain _parent;
        private PlayListItem _currentlyPlayListItem;

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
            StopAndHidePlayer();
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
                wmPlayer.Dock = DockStyle.Fill;
                wmPlayer.fullScreen = _parent.FullScreen;
            }
        }

        public void PlayPlayListItem(PlayListItem playListItem)
        {
            if (playListItem == null)
                return;

            _currentlyPlayListItem = playListItem;
            wmPlayer.URL = _currentlyPlayListItem.FilePath;
        }

        public void Stop()
        {
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
            _parent.VideoState = wmPlayer.playState;
            
            switch (wmPlayer.playState)
            {
                case WMPPlayState.wmppsUndefined:
                    break;
                case WMPPlayState.wmppsStopped:
                    SetStopped();
                    break;
                case WMPPlayState.wmppsPaused:
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
    }
}