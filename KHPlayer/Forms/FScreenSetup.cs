using System;
using System.Linq;
using System.Windows.Forms;
using KHPlayer.Services;

namespace KHPlayer.Forms
{
    public partial class FScreenSetup : Form
    {
        private readonly ScreenService _screenService;

        public FScreenSetup()
        {
            InitializeComponent();
            _screenService = new ScreenService();

            LoadScreens();

            lvScreens.View = View.Details;
            lvScreens.GridLines = true;
            lvScreens.FullRowSelect = true;
        }

        private void LoadScreens()
        {
            lvScreens.Items.Clear();

            foreach (var screen in _screenService.Get())
            {
                var listViewItem = new ListViewItem(new[] {screen.FriendlyName, screen.ScreenDevice.DeviceName, screen.AudioDevice.Name, "0"});
                lvScreens.Items.Add(listViewItem);
            }
        }

        private void bAddNewScreen_Click(object sender, EventArgs e)
        {
            using (var form = new FNewScreenDialogue{ StartPosition = FormStartPosition.CenterParent })
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
            Close();
        }
    }
}
