using MySql.Data.MySqlClient;
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
    public partial class VendorReport : Form
    {
        
        public VendorReport()
        {
            
            InitializeComponent();
            vendorReport cr = new vendorReport();
            crystalReportViewer1.ReportSource = cr;
            crystalReportViewer1.Refresh();
        }

 
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }



      

       
    }
}
