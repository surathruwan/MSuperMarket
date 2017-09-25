using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace inventory
{
    public partial class report : Form
    {
        public report()
        {
            InitializeComponent();
        }
        public void chartLoadProLosBar()
        {
            chart1.Visible = true;

            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select count(*) as 'count',Category from supermarket.item group by Category ;", conn);
            MySqlDataReader myR;
            try
            {
                conn.Open();
                myR = cmd.ExecuteReader();

                while (myR.Read())
                {
                    this.chart1.Series["Count"].Points.AddXY(myR.GetString("Category"), myR.GetInt32("Count"));
                }


            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }




        }

        public void chartLoadProLosPie()
        {

            chart1.Visible = true;
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select count(*) as 'count',Category from supermarket.item group by Category ;", conn);
            MySqlDataReader myR;
            try
            {
                conn.Open();
                myR = cmd.ExecuteReader();

                while (myR.Read())
                {
                    this.chart2.Series["Count"].Points.AddXY(myR.GetString("Category"), myR.GetInt32("count"));
                }


            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }



        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void report_Load(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            if (cmbchart.SelectedIndex == 0)
            {
                chart2.Visible = false;
                chart1.Visible = true;
                chartLoadProLosBar();


            }
            else if (cmbchart.SelectedIndex == 1)
            {
                
                chart1.Visible = false;
                chart2.Visible = true;

                chartLoadProLosPie();

            }

        }
    }
}
