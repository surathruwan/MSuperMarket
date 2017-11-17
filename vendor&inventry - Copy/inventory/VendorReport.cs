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
        string constring = "Sslmode=none;datasource=localhost;port=3306;username=root;password=  ";
        public VendorReport()
        {
            
            InitializeComponent();
<<<<<<< HEAD
            
            vendorReport cr = new vendorReport();
            crystalReportViewer1.ReportSource = cr;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.RefreshReport();
=======
            fillCombo();
>>>>>>> 4932bef6cc42f0bb8e746ba9778ac024339127c8
        }

        void fillCombo()
        {
            try
            {
                this.comboBox1.Items.Clear();
               
                
                string query = "select * from supermarket.vendor;";
                MySqlConnection conDatabase = new MySqlConnection(constring);
                MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);
                MySqlDataReader myReader;
                

                conDatabase.Open();
                myReader = cmdDatabase.ExecuteReader();
               


                while (myReader.Read())
                {
                    string Vencode = myReader.GetString("code");
                    comboBox1.Items.Add(Vencode);
                  
                }
                
                  
                
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            
            

        }

        void refNum()
        {
            try
            {

                string query1 = "select * from supermarket.vendorquotation where VendorCode='"+comboBox1.Text+"';";
                MySqlConnection conDatabase1 = new MySqlConnection(constring);
                MySqlCommand cmdDatabase1 = new MySqlCommand(query1, conDatabase1);
                MySqlDataReader myReader1;
                
                conDatabase1.Open();
                myReader1 = cmdDatabase1.ExecuteReader();



                while (myReader1.Read())
                {
                    string refNum = myReader1.GetString("ReferenceNum");
                    comboBox4.Items.Add(refNum);
                }



            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }




        }

        void gtVendorName()
        {
            try
            {
                string query = "select fullname from supermarket.vendor where code='" + comboBox1.Text + "';";
                MySqlConnection conDatabase = new MySqlConnection(constring);
                MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);
                MySqlDataReader myReader;

                conDatabase.Open();
                myReader = cmdDatabase.ExecuteReader();

                while (myReader.Read())
                {
                    string Vencode = myReader.GetString("fullname");
                    bunifuMetroTextbox2.Text = Vencode;
                }


            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sent_Click(object sender, EventArgs e)
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

        private void body_TextChanged(object sender, EventArgs e)
        {

        }

        private void subject_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void to_TextChanged(object sender, EventArgs e)
        {

        }

        private void from_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void smtp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bunifuMetroTextbox2.Visible = true;
            label1.Visible = true;
            refNum();
            gtVendorName();
        }
    }
}
