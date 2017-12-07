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
            yourHighscores.GetHighscoresForPlayer(username);
            theirHighscores.GetHighscoresForPlayer(opponentName);
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

                bool youHigher = yourLevel > theirLevel;
                bool tie = yourLevel == theirLevel;

                var index = levelsGridView.Rows.Add();
                DataGridViewRow dataGridViewRow = levelsGridView.Rows[index];
                dataGridViewRow.Cells[0].Value = skill;
                dataGridViewRow.Cells[1].Value = yourLevel;
                dataGridViewRow.Cells[2].Value = theirLevel;
                if (tie)
                {
                    dataGridViewRow.Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewRow.Cells[2].Style.BackColor = Color.LightGray;
                }
                else if (youHigher)
                {
                    dataGridViewRow.Cells[1].Style.BackColor = Color.Green;
                    dataGridViewRow.Cells[2].Style.BackColor = Color.Red;
                }
                else
                {
                    dataGridViewRow.Cells[1].Style.BackColor = Color.Red;
                    dataGridViewRow.Cells[2].Style.BackColor = Color.Green;
                }

                //levelsGridView.Rows.Add(dataGridViewRow);
                levelsGridView.Refresh();
            }
        }
    }
}
