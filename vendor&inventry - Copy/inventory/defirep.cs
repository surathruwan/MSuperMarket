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
        private static String user = null;
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
                 user = us.Text;
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
                    Session.startSession(us.Text);
                    MainForm F2 = new MainForm();
                    F2.Show();
                    us.Text = "";
                    pw.Text = "";
                    this.WindowState = FormWindowState.Minimized;
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Invalid Login","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
         
        public static string getUsername() { return user; }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pw_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void pw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                bunifuThinButton211.Select();
        }
    }
}
