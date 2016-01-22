using System;
using System.Windows.Forms;

namespace KHPlayer.Forms
{
    public partial class FDialogue : Form
    {
        public string ResultOne { get; set; }
        public string ResultTwo { get; set; }

        public FDialogue()
        {
        }

        public FDialogue(string lOneValue, string lTwoValue)
        {
            InitializeComponent();
            lOne.Text = lOneValue;
            lTwo.Text = lTwoValue;
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            ResultOne = tbOne.Text;
            ResultTwo = tbTwo.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
