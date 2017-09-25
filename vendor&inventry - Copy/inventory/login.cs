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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuThinButton211_Click(object sender, EventArgs e)
        {
            int count = 0;
            try
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);

                MySqlCommand cm = new MySqlCommand("Select * from supermarket.users where user='" + us.Text + "' and password='" + pw.Text + "'", dbcon);

                dbcon.Open();
                DataSet d = new DataSet();
                MySqlDataReader sdr = cm.ExecuteReader();

                while (sdr.Read())
                {
                    count = count + 1;

                }
                dbcon.Close();

                if (count == 1)
                {
                   
                    MainForm F2 = new MainForm();
                    F2.Show();
                    us.Text = "";
                    pw.Text = "";
                    this.WindowState = FormWindowState.Minimized;
                    //this.Close();

                }
                else
                {
                    MessageBox.Show("Invalid Login","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
