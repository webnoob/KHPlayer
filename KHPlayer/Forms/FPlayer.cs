using System;
using System.Collections.Generic;
using System.Drawing;
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
        private const bool Debug = false;
        private const int WmNclbuttonDown = 0xA1;
        private const int HtCaption = 0x2;

        private PlayListItem _currentlyPlayListItem;
        private WMPPlayState _currentPlayState;
        private WaveOut _waveOut;
        private int _axReaderCurrentScrollOffset;
        private int _axReaderCurrentPage;
        private int _axReaderTotalPages;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public AxWindowsMediaPlayer WmPlayer { get { return wmPlayer; } }
        public WMPPlayState CurrentPlayState { get { return _currentPlayState; } }
        public PlayListItem PlayListItem { get { return _currentlyPlayListItem; } }

        public delegate void StandardEventHandler(object sender, EventArgs e);
        public event StandardEventHandler OnPlayStateChanged;
        public event StandardEventHandler OnWindowStateChanged;
        
        public FPlayer(PlayListItem playListItem)
        {
            _currentlyPlayListItem = playListItem;

            //Do the window movement before InitializeComponent so we can ensure the window is located in the correct screen (full screen mode only).
            if (_currentlyPlayListItem != null && _currentlyPlayListItem.FullScreen)
                WindowState = FormWindowState.Maximized;

            InitializeScreen();

            InitializeComponent();
            
            wmPlayer.settings.volume += 100;
            _currentPlayState = WMPPlayState.wmppsStopped;
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
            if (_currentlyPlayListItem != null)
                _currentlyPlayListItem.FullScreen = WindowState == FormWindowState.Maximized;
        }

        public void ShowPlayer()
        {
            wmPlayer.Visible = _currentlyPlayListItem.Type == PlayListItemType.Video || Debug;
            if (wmPlayer.Visible)
            {
                wmPlayer.Dock = DockStyle.Fill;
                if (_currentPlayState == WMPPlayState.wmppsPlaying)
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

            if (_currentlyPlayListItem.Type == PlayListItemType.Video || 
                _currentlyPlayListItem.Type == PlayListItemType.Audio)
            {
                if (playListItem.SupportsMultiCast && playListItem.Screen == null)
                    throw new Exception("No Screen Setup");

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
                //NOTE: See the comment in the InitializeComponent() method (where this code should actually be!)
                this.Controls.Add(this.axReader);

                axReader.LoadFile(playListItem.FilePath);
                axReader.setView(playListItem.PdfView);
                axReader.setShowScrollbars(false);
                axReader.setShowToolbar(false);
                axReader.setCurrentPage(playListItem.PdfPageNumber);
                _axReaderCurrentPage = playListItem.PdfPageNumber;

                //Get total number of pages for use in the navigation properties.
                var pdfReader = new PdfReader(playListItem.FilePath);
                _axReaderTotalPages = pdfReader.NumberOfPages;

                //This is madness. The PDF won't actually show unless the form is resized.
                //Tried a refresh but didn't help. This causes a slight flicker but it's the best I can do for now.
                var currentWindowState = WindowState;
                WindowState = FormWindowState.Minimized;
                WindowState = currentWindowState;
            }
            else if (_currentlyPlayListItem.Type == PlayListItemType.Image)
            {
                pbMain.Image = Image.FromFile(playListItem.FilePath);
                pbMain.Visible = true;
                pbMain.SizeMode = PictureBoxSizeMode.Zoom;
                //pbMain.SizeMode  
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
            if (!Debug)
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
            if (wmPlayer.playState == _currentPlayState)
                return;

            SetStateChanged(wmPlayer.playState);
        }

        private void SetStateChanged(WMPPlayState playState)
        {
            _currentPlayState = playState;
            OnOnPlayStateChanged();
            
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
            WindowState = fullscreen ? FormWindowState.Maximized : FormWindowState.Normal;

            if (wmPlayer.playState == WMPPlayState.wmppsPlaying || wmPlayer.playState == WMPPlayState.wmppsPaused)
            {
                if (_currentlyPlayListItem.Type == PlayListItemType.Video)
                {
                    wmPlayer.fullScreen = fullscreen;
                }
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
            //axReader.LoadFile(""); //Ensure we release the previous file. This doesn't seem to always happen.
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
                axReader.setViewScroll(_currentlyPlayListItem.PdfView, _axReaderCurrentScrollOffset);
                return;
            }
            
            //Reset if we're moving up and hit the start of the document.
            if (_axReaderCurrentScrollOffset < 0)
                _axReaderCurrentScrollOffset = 0; //Reset at start of document when 

            //Do the move.
            axReader.setViewScroll(_currentlyPlayListItem.PdfView, _axReaderCurrentScrollOffset);
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

        protected override void WndProc(ref Message m)
        {
            var org = WindowState;
            base.WndProc(ref m);
            if (WindowState != org)
                OnOnWindowStateChanged();
        }

        protected virtual void OnOnPlayStateChanged()
        {
            var handler = OnPlayStateChanged;
            if (handler != null) 
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnOnWindowStateChanged()
        {
            var handler = OnWindowStateChanged;
            if (handler != null) 
                handler(this, EventArgs.Empty);
        }

        private void pbMain_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WmNclbuttonDown, HtCaption, 0);
        }
    }
}
