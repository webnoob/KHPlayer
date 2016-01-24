using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KHPlayer.Classes;
using KHPlayer.Services;

namespace KHPlayer.Forms
{
    public partial class FNewScreenDialogue : Form
    {
        private readonly AudioDeviceService _audioDeviceService;
        private readonly ScreenService _screenService;
        private Color _selectedColour;

        public PlayerScreen Screen { get; set; }

        public FNewScreenDialogue(PlayerScreen screen)
        {
            InitializeComponent();
            _audioDeviceService = new AudioDeviceService();
            _screenService = new ScreenService();

            if (screen != null)
            {
                Screen = screen;
                tbScreenName.Text = Screen.FriendlyName;
                tbColour.Text = Screen.ColourName;
                cbDefault.Checked = Screen.DefaultScreen;
            }

            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            cbAudioDevice.DataSource = _audioDeviceService.Get();
            cbAudioDevice.DisplayMember = "Name";
            cbAudioDevice.ValueMember = "Id";
            if (Screen != null)
            {
                foreach (var item in from object item in cbAudioDevice.Items let audioDevice = item as AudioDevice where audioDevice != null && audioDevice.Id == Screen.AudioDevice.Id select item)
                {
                    cbAudioDevice.SelectedItem = item;
                }
            }

            cbScreenDevice.DataSource = _screenService.GetAvailableScreens();
            cbScreenDevice.DisplayMember = "DeviceName";
            cbScreenDevice.ValueMember = "Id";
            if (Screen != null)
            {
                foreach (var item in from object item in cbScreenDevice.Items let screenDevice = item as ScreenDevice where screenDevice != null && screenDevice.Id == Screen.ScreenDevice.Id select item)
                {
                    cbScreenDevice.SelectedItem = item;
                }
            }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            Screen = Screen ?? new PlayerScreen
            {
                Guid = Guid.NewGuid().ToString()
            };

            Screen.FriendlyName = Name = tbScreenName.Text;
            Screen.ScreenDevice = cbScreenDevice.SelectedItem as ScreenDevice;
            Screen.AudioDevice = cbAudioDevice.SelectedItem as AudioDevice;
            Screen.ColourName = tbColour.Text;
            Screen.DefaultScreen = cbDefault.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void tbColour_Click(object sender, EventArgs e)
        {
            var result = colorDlgMain.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                _selectedColour = colorDlgMain.Color;
                tbColour.Text = ColorTranslator.ToHtml(colorDlgMain.Color);
            }
        }
    }
}
