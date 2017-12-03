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
    public partial class AddTreasureTrailRewardForm : Form
    {
        private OsrsDataContainers osrsDataContainers;

        public AddTreasureTrailRewardForm(OsrsDataContainers osrsDataContainers)
        {
            InitializeComponent();
            this.osrsDataContainers = osrsDataContainers;
        }
    }
}
