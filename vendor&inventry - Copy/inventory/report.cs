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
using System.Globalization;

namespace inventory
{
    public partial class report : Form
    {
        public report()
        {
            InitializeComponent();
            fillYear(y1);
            fillYear(y2);
            fillYear(y3);
            fillmonth(m1);
            fillmonth(m2);
            fillmonth(m3);
            m1.Text = "Select Month";
            m2.Text = "Select Month";
            m3.Text = "Select Month";
            y1.Text = "Select year";
            y2.Text = "Select year";
            y3.Text = "Select year";


        }
        public void chartLoadProLosBar()
        {
            chart1.Series["Count"].Points.Clear();
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
            chart2.Series["Count"].Points.Clear();
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
        public void fillYear(ComboBox item)
        {
            for (int year = 2015; year <= DateTime.UtcNow.Year; year++)
            {
               item.Items.Add(year);
            }

           
           
        }
        public void fillmonth(ComboBox item)
        {
            var americanCulture = new CultureInfo("en-US");
            item.Items.AddRange(americanCulture.DateTimeFormat.MonthNames);



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

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
           // main.pdfReport(y1.Text,"select * from supermarket.transfer where date like '"+y1.Text+"%'","yrep1.pdf");
        }
    }
}
