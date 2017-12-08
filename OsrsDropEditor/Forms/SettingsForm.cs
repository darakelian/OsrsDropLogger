using System;
using System.Windows.Forms;

namespace OsrsDropEditor
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Save the username as the username for looking up highscores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void usernameInput_TextChanged(object sender, EventArgs e)
        {
            Utility.SaveUsername(((TextBox)sender).Text);
        }

        private void usernameInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                Utility.SaveUsername(((TextBox)sender).Text);
            }
        }

        /// <summary>
        /// Save the gamemode as the highscore table to look through
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameModeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.gamemode = ((ComboBox)sender).SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Disables the default Windows beep sound when user presses enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameModeListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                e.SuppressKeyPress = true;
        }
    }
}
