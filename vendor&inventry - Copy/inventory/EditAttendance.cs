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
    public partial class EditAttendance : Form
    {
        public EditAttendance()
        {
            InitializeComponent();
        }
        public void load() {
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);
            MySqlCommand cm = new MySqlCommand("select Empid as 'Employee Id',full_name as ' Employee  Name   '  from super_market.employee_details ;", dbcon);



            try
            {

                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cm;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dbdataset;
                bunifuCustomDataGrid1.DataSource = bsource;
                sda.Update(dbdataset);

                aeid.Text = "";
                aname.Text = "";
                astart.Text = "";
                aend.Text = "";
                bunifuMetroTextbox1.Text = "";
                aname.Text = "";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void aSave_Click(object sender, EventArgs e)
        {

            string constring = "datasource=localhost;port=3306;username=root";
            string query = "delete from super_market.attendance where empid='" + this.aeid.Text + "'; ";
            MySqlConnection dbcon = new MySqlConnection(constring);
            MySqlCommand cmdb = new MySqlCommand(query, dbcon);
            MySqlDataReader reader;



            try
            {
                dbcon.Open();


                reader = cmdb.ExecuteReader();
                MessageBox.Show("Deleted successfully!");
                while (reader.Read())
                {

                }


                dbcon.Close();
                aeid.Text = "";
                aname.Text = "";
                astart.Text = "";
                aend.Text = "";
                bunifuMetroTextbox1.Text = "";
                aname.Text = "";





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            int a=100;
            a++;
            if (String.IsNullOrEmpty(aeid.Text) || String.IsNullOrWhiteSpace(aname.Text) || String.IsNullOrWhiteSpace(astart.Text) || String.IsNullOrWhiteSpace(aend.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



            else
            {

                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);
                //string query = ("insert into super_market.attendance(empid,sdate,,edate,nodates,reason,approve,sdate) values('" + this.id.Text + "' ,'" + this.name.Text + "', '" + this.start.Text + "' ,'" + this.end.Text + "' , '" + this.dates.Text + "' ,'" + this.reason.Text + "' ,'" + this.approve.Text + '")", dbcon);
                MySqlCommand cm = new MySqlCommand("insert into super_market.attendance(aid,empid,arrival,end_time) values('" + a + "','" + this.aeid.Text + "','" + this.astart.Text + "','" + this.aend.Text + "')", dbcon);
                MySqlDataReader r;
                try
                {
                    dbcon.Open();
                    r = cm.ExecuteReader();
                    MessageBox.Show("Inserted successfully!");
                    {

                    }
                    dbcon.Close();
                    aeid.Text = "";
                    aname.Text = "";
                    astart.Text = "";
                    aend.Text = "";
                    bunifuMetroTextbox1.Text = "";
                    aname.Text = "";






                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void EditAttendance_Load(object sender, EventArgs e)
        {

            load();
    }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void update_Click(object sender, EventArgs e)
        {
            string constring = "datasource=localhost;port=3306;username=root";
            string query = "update super_market.salary set arrival='" + this.astart.Text + "' ,end_time='" + this.aend.Text + "'    where empid='" + this.aeid.Text + "'; ";
            MySqlConnection dbcon = new MySqlConnection(constring);
            MySqlCommand cmdb = new MySqlCommand(query, dbcon);
            MySqlDataReader reader;



            try
            {
                dbcon.Open();


                reader = cmdb.ExecuteReader();
                MessageBox.Show("Updated successfully!");
                while (reader.Read())
                {

                }

                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuMetroTextbox2_OnValueChanged(object sender, EventArgs e)
        {
            if (!(bunifuMetroTextbox1.Text.Length == 0))
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);

                MySqlCommand cm = new MySqlCommand("select Empid as 'Employee Id',full_name as ' Employee  Name   '  from super_market.employee_details    where Empid='" + bunifuMetroTextbox2.Text + "'", dbcon);

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
                load();
            }
        }
    }
}
