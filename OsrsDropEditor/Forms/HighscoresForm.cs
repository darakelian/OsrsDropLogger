using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsrsDropEditor.Forms
{
    public partial class HighscoresForm : Form
    {
        private string username;
        private string gamemode;

        private Highscores highscores;

        /// <summary>
        /// Creates a new highscores form and loads the stats of the given username.
        /// </summary>
        /// <param name="username"></param>
        public HighscoresForm(string username, string gamemode)
        {
            InitializeComponent();
            this.username = username;
            this.gamemode = gamemode;
            Text += $" - {username}";

            highscores = new Highscores();
            highscores.GetHighscoresForPlayer(username, gamemode);
            setupImages();
            setupLabels();
        }

        private void HighscoresForm_Load(object sender, EventArgs e)
        {
            if (Owner != null)
                Location = new Point(Owner.Location.X + Owner.Width / 2 - Width / 2,
                    Owner.Location.Y + Owner.Height / 2 - Height / 2);
        }

        private void setupImages()
        {
            foreach (string skillName in highscores.playerSkillLevels.Keys)
            {
                PictureBox pictureBox = (PictureBox)Controls.Find(skillName.ToLower() + "Icon", true).FirstOrDefault();
                if (pictureBox != null)
                {
                    Bitmap icon = Utility.GetImageFromFile(skillName);
                    if (icon != null)
                        pictureBox.Image = icon;
                    else
                        Console.WriteLine($"Couldn't set icon for: {skillName}");
                }
            }
        }

        private void setupLabels()
        {
            foreach (string skillName in highscores.playerSkillLevels.Keys)
            {
                Label skillLabel = (Label)Controls.Find(skillName + "Label", true).FirstOrDefault();
                PictureBox skillIcon = (PictureBox)Controls.Find(skillName + "Icon", true).FirstOrDefault();
                if (skillLabel != null)
                {
                    string prefix = skillName == "overall" ? "Overall: " : "";
                    skillLabel.Text = $"{prefix}{highscores.playerSkillLevels[skillName].Level.ToString()}";
                    xpTooltip.SetToolTip(skillLabel, $"XP: {highscores.GetExperienceForSkill(skillName)}");
                    if (skillIcon != null)
                        xpTooltip.SetToolTip(skillIcon, $"XP: {highscores.GetExperienceForSkill(skillName)}");
                }
            }
        }

        private void skillLabel_MouseHover(object sender, EventArgs e)
        {
            string skillName = ((Label)sender).Name.Replace("Label", "");
            updateProgressBar(skillName);
        }

        private void skillIcon_MouseHover(object sender, EventArgs e)
        {
            string skillName = ((PictureBox)sender).Name.Replace("Icon", "");
            updateProgressBar(skillName);
        }

        private void updateProgressBar(string skillName)
        {
            double percent = highscores.GetPercentNextLevel(skillName);
            int rank = highscores.GetRankForSkill(skillName);
            if (percent >= 0)
            {
                levelProgressBar.Value = (int)percent;
                progressLabel.Text = String.Format("Progress: {0:0.##}%", percent);
                rankingLabel.Text = $"Rank: {rank}";
            }
            else
            {
                progressLabel.Text = $"Rank: {rank}";
                rankingLabel.Text = String.Empty;
            }
        }

        private void compareUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.Handled = e.SuppressKeyPress = true;
                if (String.IsNullOrEmpty(compareUsername.Text))
                    return;

                try
                {
                    new CompareHighscoresForm(username, compareUsername.Text).Show(this);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to compare highscores --- check spelling of username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
    }
}
