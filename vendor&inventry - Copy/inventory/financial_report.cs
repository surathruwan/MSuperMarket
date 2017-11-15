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
    public partial class financial_report : Form
    {
        public financial_report(string year)
        {
            InitializeComponent();
            Expense();
            Profit();
            Tax();
            Bank();
            lblyear.Text = DateTime.Now.Year.ToString();
            double n = Convert.ToDouble(lblp.Text);
            double t = Convert.ToDouble(lblt.Text);
            double q = n - t;

            lblafter.Text = q.ToString();
            double t1 = Convert.ToDouble(lblafter.Text);
            double t11 = Convert.ToDouble(lble.Text);
            double o = t1 - t11;
            lbla.Text = o.ToString();

            double p = Convert.ToDouble(lbla.Text);
            double g = p - 25000;
            reve.Text = g.ToString();

            double p1 = Convert.ToDouble(lblp.Text);
            double g1 = p1 - 13500;
            pro.Text = g1.ToString();

            double p111 = Convert.ToDouble(lblp.Text);
            double p11 = Convert.ToDouble(intere.Text);
            double g11 = (p11/p111)*100;

            int we = (int)g11;
            dep.Text = we.ToString()+" %";
            

        }
        
        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Expense()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            string query = "select sum(amount) as tot from supermarket.incomeexpense where category='Expense'";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataReader myR;
            try
            {
                conn.Open();
                myR = cmd.ExecuteReader();

                if (myR.Read())
                {

                    lble.Text = myR["tot"].ToString();

                   

                }
               
                conn.Close();
            }
            catch (Exception r)
            {


                MessageBox.Show(r.Message);
            }
                                }

        public void Profit()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            string query = "select sum(Amount) as tot from supermarket.instcard ";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataReader myR;
            try
            {
                conn.Open();
                myR = cmd.ExecuteReader();

                if (myR.Read())
                {

                    lblp.Text = myR["tot"].ToString();



                }

                conn.Close();
            }
            catch (Exception r)
            {


                MessageBox.Show(r.Message);
            }
        }

        public void Tax()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            string query = "select sum(amount) as tot from supermarket.incomeexpense where type='Tax'";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataReader myR;
            try
            {
                conn.Open();
                myR = cmd.ExecuteReader();

                if (myR.Read())
                {

                    lblt.Text = myR["tot"].ToString();



                }

                conn.Close();
            }
            catch (Exception r)
            {


                MessageBox.Show(r.Message);
            }
        }


        public void Bank()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            string query = "select sum(lamount) as tot from supermarket.bank where status='Unpaid' and ldate > 2017-01-01 ";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataReader myR;
            try
            {
                conn.Open();
                myR = cmd.ExecuteReader();

                if (myR.Read())
                {

                   string b = myR["tot"].ToString();
                    double p1 = Convert.ToDouble(b);
                    double g1 = p1 - 675000;
                    intere.Text = g1.ToString();



                }

                conn.Close();
            }
            catch (Exception r)
            {


                MessageBox.Show(r.Message);
            }
        }











    }
}
