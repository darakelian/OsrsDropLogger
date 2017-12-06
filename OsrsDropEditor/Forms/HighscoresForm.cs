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

        /// <summary>
        /// Creates a new highscores form and loads the stats of the given username.
        /// </summary>
        /// <param name="username"></param>
        public HighscoresForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }
    }
}
