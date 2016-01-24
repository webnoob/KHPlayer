using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using KHPlayer.Classes;
using KHPlayer.Services;

namespace KHPlayer.Forms
{
    public partial class FScreenSetup : Form
    {
        private readonly ScreenService _screenService;
        private readonly AudioDeviceService _audioDeviceService;
        private readonly PlayListItemService _playListItemService;

        public FScreenSetup()
        {
            InitializeComponent();
            _screenService = new ScreenService();
            _audioDeviceService = new AudioDeviceService();
            _playListItemService = new PlayListItemService();

            LoadScreens();

            lvScreens.View = View.Details;
            lvScreens.GridLines = true;
            lvScreens.FullRowSelect = true;
        }

        private void LoadScreens()
        {
            /*var list = new BindingList<PlayerScreen>(_screenService.Get().ToList());
            gvScreens.AutoGenerateColumns = false;
            LoadScreenDevicesComboBox();
            LoadAudioDevicesComboBox();
            gvScreens.DataSource = list;*/

            lvScreens.Items.Clear();

            foreach (var screen in _screenService.Get())
            {
                var listViewItem = new ListViewItem(new[] {screen.FriendlyName, screen.ScreenDevice.DeviceName, screen.AudioDevice.Name, "0"});
                lvScreens.Items.Add(listViewItem);
            }
        }

        private void LoadAudioDevicesComboBox()
        {
            var cb = gvScreens.Columns["colAudioDevice"] as DataGridViewComboBoxColumn;
            if (cb == null)
                return;
            cb.ValueMember = "Id";
            cb.DisplayMember = "Name";
        }

        private void LoadScreenDevicesComboBox()
        {
            var cb = gvScreens.Columns["colScreenDevice"] as DataGridViewComboBoxColumn;
            if (cb == null)
                return;

            cb.ValueMember = "Id";
            cb.DisplayMember = "DeviceName";
            cb.DataSource = _screenService.GetAvailableScreens();
        }

        private void bAddNewScreen_Click(object sender, EventArgs e)
        {
            using (var form = new FNewScreenDialogue(null){ StartPosition = FormStartPosition.CenterParent })
            {
                var dialogueResult = form.ShowDialog(this);
                if (dialogueResult == DialogResult.OK)
                {
                    _screenService.Insert(form.Screen);
                }
            }

            LoadScreens();
        }

        private void bDeleteScreen_Click(object sender, EventArgs e)
        {
            if (lvScreens.SelectedItems.Count == 0)
                return;

            var screenName = lvScreens.SelectedItems[0].Text;
            var screen = _screenService.GetByName(screenName);
            if (screen == null)
                return;

            _screenService.Delete(screen);
            LoadScreens();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            //Update all the current play list items with the changes made.
            foreach (var playListItem in _playListItemService.Get())
            {
                if (playListItem.Screen == null)
                    continue;

                playListItem.Screen = _screenService.GetByGuid(playListItem.Screen.Guid);
            }
            Close();
        }

        private void bEditScreen_Click(object sender, EventArgs e)
        {
            if (lvScreens.SelectedItems.Count == 0)
                return;

            var screenName = lvScreens.SelectedItems[0].Text;
            var screen = _screenService.GetByName(screenName);
            if (screen == null)
                return;

            using (var form = new FNewScreenDialogue(screen)
            {
                StartPosition = FormStartPosition.CenterParent
            })
            {
                var dialogueResult = form.ShowDialog(this);
                if (dialogueResult == DialogResult.OK)
                {
                    _screenService.Update(form.Screen);
                }
            }

            LoadScreens();
        }

        /*private void gvScreens_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var screen = gvScreens.Rows[e.RowIndex].DataBoundItem as PlayerScreen;
            if (screen == null)
                return;

            var currentColumn = gvScreens.Columns[e.ColumnIndex];
            var currentRow = gvScreens.Rows[e.RowIndex];
            if (currentColumn.Name == "colScreenDevice")
            {
                e.Value = screen.ScreenDevice.DeviceName;
            }

            if (currentColumn.Name == "colAudioDevice")
            {
                e.Value = screen.AudioDevice.Name;
            }
        }*/
    }
}
