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
            p.reset();
            p.initLeap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p.fire();

        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            p.deInitLeap();
        }
    }
}
