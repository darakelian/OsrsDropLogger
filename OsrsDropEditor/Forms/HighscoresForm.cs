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
        }

        private void HighscoresForm_Load(object sender, EventArgs e)
        {
            if (Owner != null)
                Location = new Point(Owner.Location.X + Owner.Width / 2 - Width / 2,
                    Owner.Location.Y + Owner.Height / 2 - Height / 2);
        }
    }
}
