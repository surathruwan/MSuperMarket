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


namespace inventory
{
    public partial class adduser : Form
    {
        public adduser()
        {
            InitializeComponent();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void add_Click(object sender, EventArgs e)
        {

            if( (String.IsNullOrEmpty(aname.Text))|| (String.IsNullOrEmpty(pwd.Text))|| (String.IsNullOrEmpty(type.Text)))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {

                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);
                //string query = ("insert into supermarket.attendance(empid,sdate,,edate,nodates,reason,approve,sdate) values('" + this.id.Text + "' ,'" + this.name.Text + "', '" + this.start.Text + "' ,'" + this.end.Text + "' , '" + this.dates.Text + "' ,'" + this.reason.Text + "' ,'" + this.approve.Text + '")", dbcon);
                MySqlCommand cm = new MySqlCommand("insert into supermarket.users(user,password,type) values('" + this.aname.Text + "','" + this.pwd.Text + "','" + this.type.Text + "')", dbcon);
                MySqlDataReader r;
                try
                {
                    dbcon.Open();
                    r = cm.ExecuteReader();
                    MessageBox.Show("Inserted successfully!");
                    {

                    }
                    
                    dbcon.Close();
                    aname.Text = "";
                    pwd.Text = "";
                    type.Text = "";
                    






                }
                catch (Exception)
                {
                    MessageBox.Show("user already recorded!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(aname.Text))
            {
                MessageBox.Show("Please select Raw First!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string constring = "datasource=localhost;port=3306;username=root";
                string query = "delete from supermarket.users where user='" + this.aname.Text + "'; ";
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
                    aname.Text = "";
                    pwd.Text = "";
                    type.Text = "";





                }
                catch (Exception ex)
                {
                    MessageBox.Show("Record is already deleted");
                }
            }
        }
    }
}
