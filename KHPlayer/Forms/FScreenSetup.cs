﻿using System;
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
                var listViewItem = new ListViewItem(new[] {screen.Name, screen.ScreenNumber.ToString(), "0"});
                lvScreens.Items.Add(listViewItem);
            }
        }

        private void bAddNewScreen_Click(object sender, EventArgs e)
        {
            using (var form = new FDialogue("Screen Name", "Screen Number"))
            {
                var dialogueResult = form.ShowDialog(this);
                if (dialogueResult == DialogResult.OK)
                {
                    _screenService.AddScreen(form.ResultOne, Convert.ToInt32(form.ResultTwo));
                }
            }

            LoadScreens();
        }

        private void bDeleteScreen_Click(object sender, EventArgs e)
        {
            var screenName = lvScreens.SelectedItems[0].Text;
            var screen = _screenService.GetByName(screenName);
            _screenService.Delete(screen);
            LoadScreens();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}