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
    public partial class CompareHighscoresForm : Form
    {
        private Highscores yourHighscores, theirHighscores;

        public CompareHighscoresForm(string username, string opponentName)
        {
            InitializeComponent();

            yourHighscores = new Highscores();
            theirHighscores = new Highscores();
            if (!yourHighscores.GetHighscoresForPlayer(username) || !theirHighscores.GetHighscoresForPlayer(opponentName)) {
                throw new Exception("Couldn't load player highscore information.");
            }
        }

        /// <summary>
        /// Setup the grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompareHighscoresForm_Load(object sender, EventArgs e)
        {
            foreach (string skill in Highscores.Skills)
            {
                int yourLevel = yourHighscores.GetLevelForSkill(skill);
                int theirLevel = theirHighscores.GetLevelForSkill(skill);

                int yourXp = yourHighscores.GetExperienceForSkill(skill);
                int theirXp = theirHighscores.GetExperienceForSkill(skill);

                bool youHigher = yourLevel > theirLevel;
                bool tie = yourLevel == theirLevel;

                //Initialize row
                int index = levelsGridView.Rows.Add();
                DataGridViewRow dataGridViewRow = levelsGridView.Rows[index];
                dataGridViewRow.Cells[0].Value = skill;
                dataGridViewRow.Cells[1].Value = yourLevel;
                dataGridViewRow.Cells[1].ToolTipText = $"XP: {yourXp}";
                dataGridViewRow.Cells[2].Value = theirLevel;
                dataGridViewRow.Cells[2].ToolTipText = $"XP: {theirXp}";

                //Set cell colors
                dataGridViewRow.Cells[1].Style.BackColor = youHigher ? Color.Green : tie ? Color.LightGray : Color.Red;
                dataGridViewRow.Cells[2].Style.BackColor = youHigher ? Color.Red : tie ? Color.LightGray : Color.Green;

                levelsGridView.Refresh();
            }
        }
    }
}
