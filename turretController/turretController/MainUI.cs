using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace turretController
{
    public partial class MainUI : Form
    {
        private Program p;
        public MainUI(Program p)
        {
            InitializeComponent();
            this.p = p;
        }

        private void fireButton_Click(object sender, EventArgs e)
        {
            p.log("Fire clicked.");
            p.fire();
        }

        private void MainUI_Load(object sender, EventArgs e)
        {
            p.reset();
            p.initLeap();
        }

        private void MainUI_Closing(object sender, FormClosingEventArgs e)
        {
            p.log("Closing Main UI.");
            p.deInitLeap();
        }
    }
}
