using System;
using System.Windows.Forms;
using KHPlayer.Classes;
using KHPlayer.Services;

namespace KHPlayer.Forms
{
    public partial class FNewScreenDialogue : Form
    {
        private readonly AudioDeviceService _audioDeviceService;
        private readonly ScreenService _screenService;

        public PlayerScreen Screen { get; set; }

        public FNewScreenDialogue()
        {
            InitializeComponent();
            _audioDeviceService = new AudioDeviceService();
            _screenService = new ScreenService();

            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            cbAudioDevice.DataSource = _audioDeviceService.Get();
            cbAudioDevice.DisplayMember = "Name";
            cbAudioDevice.ValueMember = "Id";

            cbScreenDevice.DataSource = _screenService.GetAvailableScreens();
            cbScreenDevice.DisplayMember = "DeviceName";
            cbScreenDevice.ValueMember = "Id";
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            Screen = new PlayerScreen
            {
                FriendlyName = Name = tbScreenName.Text,
                ScreenDevice = cbScreenDevice.SelectedItem as ScreenDevice,
                AudioDevice = cbAudioDevice.SelectedItem as AudioDevice
            };
            
            DialogResult = DialogResult.OK;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
