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

namespace madushaTemp
{
    public partial class AdviceOfDispatch : Form
    {
        public AdviceOfDispatch()
        {
            InitializeComponent();
            tableLoadSuspend();
            SuspendCount();
        }
        public void tableLoadSuspend()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select invoiceno as Invoice, nic as NIC,name as FullName,amount as Amount,date as Date,noofins as Installment_Count,insval as Installment_Value,prepareby as PreparedBy  from supermarket.suspend ;", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable tab = new DataTable();
                sda.Fill(tab);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = tab;
                tblsuspend.DataSource = bsource;
                sda.Update(tab);
                
                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }




        }

        private void AdviceOfDispatch_Load(object sender, EventArgs e)
        {
            search("");
            search1("");

        }
        public void search(string valueTo)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

            MySqlCommand cmd = new MySqlCommand("select invoiceno as Invoice, nic as NIC,name as FullName,amount as Amount,date as Date,noofins as Installment_Count,insval as Installment_Value,prepareby as PreparedBy  from supermarket.suspend where nic like '" + valueTo + "%' ", conn);

            MySqlDataAdapter sda = new MySqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable dbt = new DataTable();
            sda.Fill(dbt);
            BindingSource bS = new BindingSource();

            bS.DataSource = dbt;
            tblsuspend.DataSource = bS;


        }

        public void search1(string valueTo)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

            MySqlCommand cmd = new MySqlCommand("select invoiceno as Invoice, nic as NIC,name as FullName,amount as Amount,date as Date,noofins as Installment_Count,insval as Installment_Value,prepareby as PreparedBy  from supermarket.suspend where date like '" + valueTo + "' ", conn);

            MySqlDataAdapter sda = new MySqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable dbt = new DataTable();
            sda.Fill(dbt);
            BindingSource bS = new BindingSource();

            bS.DataSource = dbt;
            tblsuspend.DataSource = bS;


        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnnicsearch_Click(object sender, EventArgs e)
        {
            string valueTo = txtnicaod.Text.ToString();
            search(valueTo);
        }

        private void bunifuThinButton29_Click(object sender, EventArgs e)
        {
            tableLoadSuspend();
        }

        private void btndatese_Click(object sender, EventArgs e)
        {
            string valueTo = dateaod.Text.ToString();
            search1(valueTo);
        }

        private void btnremoveaod_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("delete from supermarket.suspend where nic='" + this.txtnicaod.Text + "'", conn);

                    cmd.ExecuteNonQuery();



                    MessageBox.Show("Deleted Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tableLoadSuspend();
                    txtnicaod.Text = "";
                    SuspendCount(); 

                    conn.Close();
                }
                catch (Exception r)
                {

                    MessageBox.Show(r.Message);
                }
            }
        }

        private void tblsuspend_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.tblsuspend.Rows[e.RowIndex];
                txtnicaod.Text = row.Cells["NIC"].Value.ToString();
               

            }
        }

        private void tblsuspend_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //double tot = double.Parse(tblsuspend.SelectedRows[0].Cells[2].ToString());
            //int noi=Convert.ToInt32( tblsuspend.SelectedRows[0].Cells[4].ToString());
            //double ival = double.Parse(tblsuspend.SelectedRows[0].Cells[5].ToString());
            //int oi = Convert.ToInt32(tblsuspend.SelectedRows[0].Cells[7].ToString());

            //TestForm n = new TestForm(tblsuspend.SelectedRows[0].Cells[0].Value.ToString(), 
            //    tblsuspend.SelectedRows[0].Cells[1].Value.ToString(),
            //    tblsuspend.SelectedRows[0].Cells[2].Value.ToString(), 
            //    tblsuspend.SelectedRows[0].Cells[3].Value.ToString(), 
            //    tblsuspend.SelectedRows[0].Cells[4].Value.ToString(),
            //    tblsuspend.SelectedRows[0].Cells[5].Value.ToString(),
            //    tblsuspend.SelectedRows[0].Cells[6].Value.ToString());
            //n.Show();
            //this.Close();

        }

        public void SuspendCount()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

            MySqlCommand cmd1 = new MySqlCommand("select invoiceno from supermarket.suspend order by date desc", conn);
            MySqlCommand cmd2 = new MySqlCommand("select sum(amount) from supermarket.suspend", conn);
            MySqlCommand cmd3 = new MySqlCommand("select count(*)  from supermarket.suspend", conn);
            
            try
            {

                conn.Open();

                lbl1.Text = cmd1.ExecuteScalar().ToString();
                lbl2.Text = cmd2.ExecuteScalar().ToString();
                lbl3.Text = cmd3.ExecuteScalar().ToString();
                


                conn.Close();
            }
            catch (Exception r)
            {

                MessageBox.Show(r.Message);
            }
        }


    }
}
