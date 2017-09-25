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
using System.Text.RegularExpressions;

namespace inventory
{
    public partial class upitem : Form 
    {
        public Boolean status = true;
        public upitem(string v)
        {
            InitializeComponent();
            code.Text = v;
            filldetails();
        }

        private void upitem_Load(object sender, EventArgs e)
        {
            fillcombocat();
           
        }
        public void Regexp(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {
            ToolTip buttonToolTip = new ToolTip();
            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {

                pc.Image = Properties.Resources.invalid;
                n.SetToolTip(pc, s);
                status = false;


            }
            else
            {
                pc.Image = null;

            }

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
        void filldetails()
        {
            try
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);
                //MySqlDataAdapter a = new MySqlDataAdapter();
                //a.SelectCommand = new MySqlCommand("Select Brand,Wprice,Description,Item_name,Warrenty,Category,Sub_category,Rprice from supermarket.Item where Item_code='" + code.Text + "'", dbcon);

                MySqlCommand cm = new MySqlCommand("Select Brand,Wprice,Description,Item_name,Warrenty,Category,Sub_category,Rprice,Floor,shelf,packSize,freeIssue from supermarket.Item where Item_code='" + code.Text + "'", dbcon);
               // MySqlCommandBuilder cb = new MySqlCommandBuilder(a);
                dbcon.Open();
                DataSet d = new DataSet();
                MySqlDataReader sdr = cm.ExecuteReader();

                while (sdr.Read())
                {
                    brand.Text = sdr["Brand"].ToString();
                    wprice.Text = sdr["Wprice"].ToString();
                    des.Text = sdr["Description"].ToString();
                    name.Text = sdr["Item_name"].ToString();
                    warrenty.Text = sdr["Warrenty"].ToString();
                    catcombo.Text = sdr["Category"].ToString();
                    subcombo.Text = sdr["Sub_category"].ToString();
                    rprice.Text = sdr["Rprice"].ToString();
                    floor.Text = sdr["Floor"].ToString();
                    ushelf.Text = sdr["shelf"].ToString();
                    upsize.Text = sdr["packSize"].ToString();
                    free.Text = sdr["freeIssue"].ToString();


                }
                dbcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void catcombo_SelectedIndexChanged(object sender, EventArgs e)
        {

            subcombo.Text = "Select Sub Category";
            subcombo.Items.Clear();
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);
            MySqlCommand cm = new MySqlCommand("Select sub from supermarket.category where name='" + catcombo.Text + "'", dbcon);
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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {

        }

        private void minimizeButton_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuThinButton211_Click(object sender, EventArgs e)
        {
            status= true;
            Regexp("^[0-9]+$", rprice, urpe, "Error");
            Regexp("^[0-9]+$", wprice, uwpe, "Error");
            Regexp("^[0-9]+$", upsize, upse, "Error");
            if (status == true)
            {

                DialogResult dialogResult = MessageBox.Show("Do you want to update item details?", "Update item", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);
                    MySqlCommand cm = new MySqlCommand("update supermarket.Item set Brand='" + brand.Text + "',Wprice='" + wprice.Text + "',Description='" + des.Text + "',Item_name='" + name.Text + "',Warrenty='" + warrenty.Text + "',Category='" + catcombo.Text + "',Sub_category='" + subcombo.Text +"',Floor='"+ floor.Text +"',shelf='"+ushelf.Text + "',packSize='"+upsize.Text + "',freeIssue='"+free.Text+"',modifiedD='"+ DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + "',Rprice='" + rprice.Text + "' where Item_code='" + code.Text + "'", dbcon);
                    MySqlDataReader r;
                    try
                    {
                        dbcon.Open();
                        r = cm.ExecuteReader();
                        MessageBox.Show("Updated successfully!");
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
                    MessageBox.Show("Not Updated!");
                }

            }
        }

        private void bunifuMetroTextbox2_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
