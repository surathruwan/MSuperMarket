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
    public partial class categoryform : Form
    {
        public categoryform()
        {
            InitializeComponent();
            catcombo.Text = "View Categories";
            fillcombocat();
        }
        void fillcombocat()
        {
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);
            MySqlCommand cm = new MySqlCommand("Select distinct name from supermarket.category", dbcon);
            MySqlDataReader r;

            try
            {
                dbcon.Open();
                r = cm.ExecuteReader();

                while (r.Read())
                {
                    string cat = r.GetString("name");

                    catcombo.Items.Add(cat);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
        private void catcombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            

        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to insert the new category?", "Add item", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);
                MySqlCommand cm = new MySqlCommand("insert into supermarket.category values('" + name.Text + "','" + sub.Text + "')", dbcon);
                MySqlDataReader r;
                try
                {
                    dbcon.Open();
                    r = cm.ExecuteReader();
                    MessageBox.Show("Inserted successfully!");
                    while (r.Read())
                    {

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Not Inserted!");
            }
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
