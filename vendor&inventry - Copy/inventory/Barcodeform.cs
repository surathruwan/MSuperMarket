using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using MySql.Data.MySqlClient;

using CrystalDecisions.CrystalReports.Engine;
using MySql.Data.MySqlClient;

namespace inventory
{
    public partial class Barcodeform : Form
    {
        MySqlConnection myConn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        ReportDocument crystal = new ReportDocument();
        public Barcodeform()
        {
            InitializeComponent();
        }

        private void Barcodeform_Load(object sender, EventArgs e)
        {
            crystal.Load(@"C:\Users\Hp\Documents\GitHub\MSuperMarket\vendor&inventry - Copy\inventory\Barcode.rpt");
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {

                MySqlDataAdapter mda = new MySqlDataAdapter("SELECT * FROM supermarket.needtosendtemp where id='" + txtrepairID.Text + "' ", myConn);
                DataSet ds = new DataSet();
                mda.Fill(ds, "Customer");
                crystal.SetDataSource(ds);
                crystalReportViewer1.ReportSource = crystal;




            }
            catch (Exception )
            {
                
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            try
            {

                MySqlDataAdapter mda = new MySqlDataAdapter("SELECT *  FROM supermarket.needtosendtemp", myConn);
                DataSet ds = new DataSet();
                mda.Fill(ds, "Customer");
                crystal.SetDataSource(ds);
                crystalReportViewer1.ReportSource = crystal;




            }
            catch (Exception )
            {
              
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                MySqlDataAdapter mda1 = new MySqlDataAdapter("SELECT * FROM supermarket.needtosendtemp where id =  '" + txtrepairID.Text  + "' ", myConn);
                DataSet ds1 = new DataSet();
                mda1.Fill(ds1, "Customer");
                crystal.SetDataSource(ds1);
                crystalReportViewer1.ReportSource = crystal;




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                MySqlDataAdapter mda = new MySqlDataAdapter("SELECT *  FROM supermarket.needtosendtemp ", myConn);
                DataSet ds = new DataSet();
                mda.Fill(ds, "Customer");
                crystal.SetDataSource(ds);
                crystalReportViewer1.ReportSource = crystal;




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
