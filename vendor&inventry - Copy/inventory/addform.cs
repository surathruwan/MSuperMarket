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
    public partial class addform : Form
    {
        public addform()
        {
            InitializeComponent();
            fillcombocat();
            catcombo.Text = "Select Category";
            subcombo.Text = "Select Sub Category";
        }
        void reset()
        {

            catcombo.Text = "Select Category";
            subcombo.Text = "Select Sub Category";
            brand.Text = "";
            name.Text = "";
            code.Text = "";
            bcode.Text = "";
            //bunifuMetroTextbox1.Text = "";
            des.Text = "";
            wprice.Text = "";
            rprice.Text = "";
            warrenty.Text = "";
        }
        void fillcombocat()
        {
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);
            MySqlCommand cm = new MySqlCommand("Select distinct name from madusha.category", dbcon);
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
        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to insert the item?", "Add item", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);
                MySqlCommand cm = new MySqlCommand("insert into madusha.item values('" + name.Text + "','" + code.Text + "','" + bcode.Text + "','" + catcombo.Text + "','" + subcombo.Text + "','" + brand.Text + "','" + wprice.Text + "','" + rprice.Text + "',0,'" + des.Text + "','" + warrenty.Text + "',','"+"null"+"')", dbcon);
                MySqlDataReader r;
                try
                {
                    dbcon.Open();
                    r = cm.ExecuteReader();
                    MessageBox.Show("Inserted successfully!");
                    while (r.Read())
                    {

                    }
                    reset();

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

        private void catcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            subcombo.Text = "Select Sub Category";
            subcombo.Items.Clear();
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);
            MySqlCommand cm = new MySqlCommand("Select sub from madusha.category where name='" + catcombo.Text + "'", dbcon);
            MySqlDataReader r;
            try
            {
                dbcon.Open();
                r = cm.ExecuteReader();

                while (r.Read())
                {
                    string cat = r.GetString("sub");

                    subcombo.Items.Add(cat);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to reset the fields?", "Reset", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                reset();
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
            

        }

        private void icode_OnTextChange(object sender, EventArgs e)
        {

        }

        private void addform_Load(object sender, EventArgs e)
        {

        }
    }
}
