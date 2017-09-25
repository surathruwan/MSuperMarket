using System;
using System.Windows.Forms;

namespace inventory
{
    public partial class newcatsub : Form
    {
        public newcatsub()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            categoryform f1 = new categoryform();
            f1.Show();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            subcatform f1 = new subcatform();
            f1.Show();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
