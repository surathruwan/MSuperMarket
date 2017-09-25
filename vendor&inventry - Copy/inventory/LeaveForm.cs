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
using MySql.Data;

namespace madushaTemp
{
    public partial class LeaveForm : Form
    {
        public LeaveForm()
        {
            InitializeComponent();
        }
        DataTable dbdataset;
        public void loadleave() {
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);
            MySqlCommand cm = new MySqlCommand("select empid as 'Employee Id',sdate as 'Start Date',edate as 'End Date',nodates as 'Number of dates',reason as 'Reason',approve as 'Approve'  from super_market.leaving;", dbcon);



            try
            {

                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cm;
                dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dbdataset;
                bunifuCustomDataGrid1.DataSource = bsource;
                sda.Update(dbdataset);




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void DOB_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LeaveForm_Load(object sender, EventArgs e)
        {


            loadleave();

    }

    private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void search_OnValueChanged(object sender, EventArgs e)
        {

            if (!(search.Text.Length == 0))
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);

                MySqlCommand cm = new MySqlCommand("select empid as 'Employee Id',sdate as 'Start Date',edate as 'End Date',nodates as 'Number of dates',reason as 'Reason',approve as 'Approve'  from super_market.leaving  where empid='" + search.Text + "'", dbcon);

                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cm;
                    DataTable set = new DataTable();
                    sda.Fill(set);
                    BindingSource s = new BindingSource();

                    s.DataSource = set;
                    bunifuCustomDataGrid1.DataSource = s;
                    sda.Update(set);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            else
            {
                loadleave();
            }
        }
    }
}
