﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AxWMPLib;
using iTextSharp.text.pdf;
using KHPlayer.Classes;
using KHPlayer.Properties;
using NAudio.Wave;
using WMPLib;

namespace KHPlayer.Forms
{
    public partial class FPlayer : Form
    {
        private const string AxReaderViewMode = "FitH";
        private const int WmNclbuttonDown = 0xA1;
        private const int HtCaption = 0x2;

        private readonly FMain _parent;
        private PlayListItem _currentlyPlayListItem;
        private WMPPlayState _currentState;
        private WaveOut _waveOut;
        private int _axReaderCurrentScrollOffset;
        private int _axReaderCurrentPage;
        private int _axReaderTotalPages;

        public AxWindowsMediaPlayer WmPlayer { get { return wmPlayer; } }
        [DllImport("user32.dll")]

        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        
        public static extern bool ReleaseCapture();
        public PlayListItem PlayListItem { get; set; }
        
        public FPlayer(FMain parent, PlayListItem playListItem)
        {
            PlayListItem = playListItem;
            _parent = parent;
            
            //Do the window movement before InitializeComponent so we can ensure the window is located in the correct screen (full screen mode only).
            if (parent.FullScreen)
                WindowState = FormWindowState.Maximized;

            InitializeScreen();

            InitializeComponent();
            
            wmPlayer.settings.volume += 100;
            _currentState = WMPPlayState.wmppsStopped;
            _axReaderCurrentScrollOffset = 0;
            _axReaderCurrentPage = 1;
            _axReaderTotalPages = 0;
            StopAndHidePlayer();
        }

        private void InitializeScreen()
        {
            if (PlayListItem != null && PlayListItem.Screen != null)
                MoveToScreen(PlayListItem.Screen.ScreenDevice.Id);
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
                if (_currentState == WMPPlayState.wmppsPlaying)
                {
                    //Not using the .fullScreen property now as when playing multiple video
                    //axWindowsMediaPlayer doesn't allow multiple FullScreen versions running
                    //instead do a fake full screen (although I can't see any difference)
                    //wmPlayer.fullScreen = _parent.FullScreen;
                    wmPlayer.Height = this.Height;
                    wmPlayer.Width = this.Width;
                    wmPlayer.stretchToFit = true;   
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
            {
                //Don't split the audio out if this is the default screen or it's not a compatable item.
                if (playListItem.SupportsMultiCast && !playListItem.Screen.DefaultScreen)
                {
                    //This code allows us to play the audio via any device we choose, we just need to know the device ID.
                    var waveReader = new MediaFoundationReader(playListItem.FilePath);
                    _waveOut = new WaveOut {DeviceNumber = PlayListItem.Screen.AudioDevice.Id};
                    _waveOut.Init(waveReader);
                    wmPlayer.settings.volume = 0;
                    wmPlayer.URL = playListItem.FilePath;
                    _waveOut.Play();
                }
                else
                {
                    wmPlayer.URL = playListItem.FilePath;
                }
            }
            else if (_currentlyPlayListItem.Type == PlayListItemType.Pdf)
            {
                axReader.LoadFile(playListItem.FilePath);
                axReader.setView(AxReaderViewMode);
                axReader.setShowScrollbars(false);
                axReader.setShowToolbar(false);
                axReader.setCurrentPage(1);

                //Get total number of pages for use in the navigation properties.
                var pdfReader = new PdfReader(playListItem.FilePath);
                _axReaderTotalPages = pdfReader.NumberOfPages;

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
            if (_waveOut != null)
                _waveOut.Stop();
            wmPlayer.close();
            StopAndHidePlayer();

            if (Settings.Default.ClosePlayerOnStop)
                Close();
        }

        private void StopAndHidePlayer()
        {
            wmPlayer.uiMode = Settings.Default.UiMode;
            wmPlayer.Visible = false;
        }

        public void Pause()
        {
            if (_waveOut != null)
                _waveOut.Pause();
            wmPlayer.Ctlcontrols.pause();
        }

        public void Resume()
        {
            if (_waveOut != null)
                _waveOut.Resume();
            wmPlayer.Ctlcontrols.play();
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
                case WMPPlayState.wmppsStopped:
                    SetStopped();
                    break;
                case WMPPlayState.wmppsPlaying:
                    ShowPlayer();
                    break;
                case WMPPlayState.wmppsMediaEnded:
                    SetStopped();
                    break;
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
            if (axReader == null) 
                return;

            //The following lines are required in order to close the adobe PDF form viewer properly.
            //Not sure why but the application hangs when closing without these.
            axReader.LoadFile(""); //Ensure we release the previous file. This doesn't seem to always happen.
            axReader.Dispose();
            Application.DoEvents();
        }

        public void ScrollPdf(bool down)
        {
            if (_currentlyPlayListItem == null || _currentlyPlayListItem.Type != PlayListItemType.Pdf)
                return;

            /*
            Ok, the code below might seem to be a little strange. It is.
            Adobe don't issue the current page number without using the full version of adobe which costs money.
            setViewScroll needs an offset based on the current page ... This is a problem when we don't know what 
            page we're on. So, with that in mind, we calculate how many times we've gone down which allows us to 
            see which page we're on (assuming offsetsPerPage is set correctly).
            When going back up the pages, we use setCurrentPage then scroll to the last position so it looks
            like we've just gone back to where we were ...
            EEK!
            */

            var offset = 100;
            var offsetsPerPage = 7;
            var offsetResetValue = offset*(offsetsPerPage-1);

            if (down)
                _axReaderCurrentScrollOffset += offset;
            else
                _axReaderCurrentScrollOffset -= offset;

            //Reset to current position if we're trying to scroll past the last offset on the page.
            if (down && _axReaderCurrentScrollOffset > offsetsPerPage*offset && _axReaderCurrentPage == _axReaderTotalPages)
            {
                _axReaderCurrentScrollOffset -= offset;
                return;
            }

            //Set to the next page if we're past the max offset for this page.
            if (_axReaderCurrentScrollOffset > offset*offsetsPerPage)
            {
                _axReaderCurrentScrollOffset = 0;
                _axReaderCurrentPage += 1;
            }

            //Change page to the previous one if we're going back up and hit the start of the offset.
            if (_axReaderCurrentScrollOffset < 0 && !down && _axReaderCurrentPage > 1)
            {
                _axReaderCurrentPage -= 1;
                _axReaderCurrentScrollOffset = offsetResetValue;
                axReader.setCurrentPage(_axReaderCurrentPage);
                axReader.setViewScroll(AxReaderViewMode, _axReaderCurrentScrollOffset);
                return;
            }
            
            //Reset if we're moving up and hit the start of the document.
            if (_axReaderCurrentScrollOffset < 0)
                _axReaderCurrentScrollOffset = 0; //Reset at start of document when 

            //Do the move.
            axReader.setViewScroll(AxReaderViewMode, _axReaderCurrentScrollOffset);
            Console.WriteLine(_axReaderCurrentPage);
        }

        public void MovePage(bool down)
        {
            _axReaderCurrentScrollOffset = 0;

            if (down)
                _axReaderCurrentPage += 1;
            else
                _axReaderCurrentPage -= 1;

            if (_axReaderCurrentPage < 1)
                _axReaderCurrentPage = 1;
            else if (_axReaderCurrentPage > _axReaderTotalPages)
                _axReaderCurrentPage = _axReaderTotalPages;

            axReader.setCurrentPage(_axReaderCurrentPage);
        }

        private void FPlayer_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WmNclbuttonDown, HtCaption, 0);
        }

        private void wmPlayer_MouseDownEvent(object sender, _WMPOCXEvents_MouseDownEvent e)
        {
            ReleaseCapture();
            SendMessage(Handle, WmNclbuttonDown, HtCaption, 0);
        }
    }
}
