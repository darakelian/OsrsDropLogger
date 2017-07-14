using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OsrsDropEditor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load up a list of NPCs in the game and their respective links. Only do this once.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            OsrsDataContainers.LoadData();
            List<Drop> drops = (List<Drop>)OsrsDataContainers.GetDropsForNpc("Zulrah");
            foreach (Drop drop in drops)
                Console.WriteLine(drop);
        }
    }
}
