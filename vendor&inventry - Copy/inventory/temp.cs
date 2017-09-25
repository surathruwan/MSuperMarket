using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory
{
    public partial class temp : Form
    {
        public temp()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void AddNewTab(Form frm)
        {

            TabPage tab = new TabPage(frm.Text);

            frm.TopLevel = false;

            frm.Parent = tab;

            frm.Visible = true;

            tabControl1.TabPages.Add(tab);

            frm.Location = new Point((tab.Width - frm.Width) / 2, (tab.Height - frm.Height) / 2);

            tabControl1.SelectedTab = tab;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            main m = new main();
            AddNewTab(m);
            addform add = new addform();
            AddNewTab(add);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
