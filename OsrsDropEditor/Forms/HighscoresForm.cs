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
                Label skillLabel = (Label)Controls.Find(skillName.ToLower() + "Label", true).FirstOrDefault();
                if (skillLabel != null)
                {
                    string prefix = skillName == "Overall" ? "Overall: " : "";
                    skillLabel.Text = $"{prefix}{highscores.playerSkillLevels[skillName].Level.ToString()}";
                }
            }
        }
    }
}
