using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using MySql.Data.MySqlClient;
using System.IO;
using System.Text.RegularExpressions;
using BarcodeLib;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using BarcodeLib.Symbologies;
using WinFormCharpWebCam;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Printing;
using System.Xml.Linq;
using inventory;

namespace madushaTemp
{
    public partial class TestForm : Form
    {
        //Sliding panel variables
        int PW;
        bool Hided;
        String t;
        string status;
        string summaryradio;
        string bankradio;
        int a,a1;
        int b;
        int c;



        public TestForm()
        {
            InitializeComponent();
            int v = Session.getUser();
            if (v == 1)
            {
               // btncconfirm.Visible = false;
                //button1.Enabled = false;
                // ((Control)this.tabPage1).Enabled = false;
                //tabPage1.Enabled = false;
                // tabControl1.TabPages.Remove(tabPage4);

            }
            else if (v == 2)
            {
                //button2.Enabled = false;
                tabControl1.TabPages.Remove(tabPage4);
                // ((Control)this.tabPage2).Enabled = false;
                bunifuThinButton214.Enabled = false;
                pictureBox12.Enabled = false;
                btnseized.Enabled = false;
                tblinscust.Enabled = false;
                tableseized.Enabled = false;
                btnprintrecep.Enabled = false;
                tblei.Enabled = false;
            }
            else {
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Remove(tabPage4);

            }

            chart1.Visible = false;
            chart3.Visible = false;
            chart2.Visible = false;

            tblhide.Visible = false;
            tblseizedhide.Visible = false;

            //setting default values
            cmbyear.SelectedIndex = 0;
            cmbprepare.SelectedIndex = 0;
            cmdbuyer.SelectedIndex = 0;
            cmdbname.SelectedIndex = 0;
            cmdbranch.SelectedIndex = 0;
            cmdaccno.SelectedIndex = 0;
            cmbinex.SelectedIndex = 0;
            cmbyearselect.SelectedIndex = 0;
            cmbmonthselect.SelectedIndex = 0;

            //sliding panel
            PW = Spanel.Width;
            Hided = false;

            //loading tables
            tableLoadItems();
            tableLoadOrders();
            tableLoadinstallments();
            tableLoadSeizedItems();
           // tableLoadInstaCust();
            tableLoadBank();
            tableLoadEI();
            tableLoadInstallmntformload();

            datei.MinDate = DateTime.Now;

            // chartLoadIExp();
            //chartLoadProLos();


            Random rnd1 = new Random();
            int count = rnd1.Next(1000, 10000);
            txtoid.Text = count.ToString();

            Random rnd2 = new Random();
            int count1 = rnd2.Next(100, 1000);
            lblbid.Text = count1.ToString();

            Random rnd3 = new Random();
            int count3 = rnd3.Next(100, 1000);
            lblid.Text = count3.ToString();

            seizedlblAddDeleteCount();
            BankAddDeleteCount();

            radbnpa.Checked = true;
            radsumm.Checked = true;
            radexp.Checked = true;
            radgenchat.Checked = true;
            radfin.Checked = true;
            radbnpa.Checked = true;
            tblinssummary.Visible = false;


            ToolTip n = new ToolTip();
            n.SetToolTip(btngeneratesticker, "Generate Barcode");

            ToolTip n1 = new ToolTip();
            n1.SetToolTip(bunifuThinButton21, "Save Barcode");

            ToolTip n2 = new ToolTip();
            n2.SetToolTip(pictureBox28, "Add Years");

            ToolTip n3 = new ToolTip();
            n3.SetToolTip(pictureBox29, "Remove Years"); 
            
            ToolTip n4 = new ToolTip();
            n4.SetToolTip(btniconfirm, "Add To Installment Scheme"); 

            ToolTip n5 = new ToolTip();
            n5.SetToolTip(bunifuImageButton3, "Open Customer Registration Form"); 

            ToolTip n6 = new ToolTip();
            n6.SetToolTip(btnprintrecep, "Print Receipt"); 

            ToolTip n7 = new ToolTip();
            n7.SetToolTip(bunifuImageButton1, "Add New Invoice"); 
           
            ToolTip n8 = new ToolTip();
            n8.SetToolTip(btnchartgene, "Generate Chart"); 

            ToolTip n9 = new ToolTip();
            n9.SetToolTip(pictureBox2, "Generate Chart");

        }
        public void ConvertText()
        {
            try
            {
                string Date = DateTime.Now.ToLongTimeString();
                string Time = DateTime.Now.ToLongDateString();
                String Datestr = DateTime.Now.ToString("yyyy-M-dd-HH-mm-ss");
                string total = txtgtot.Text;
                string cash = txtdpay.Text;
                string balance = lblbal.Text;
                string prepare = cmbprepare.Text;
                string path = System.IO.Path.Combine("", "1Madusha" + Datestr + ".txt");
                TextWriter writer = new StreamWriter(path);
                writer.WriteLine("\t \t MADUSHA SUPERMARKET \t \t");
                writer.WriteLine("--------------------------------------------------");
                writer.WriteLine("" + Date + "\t \t" + Time);
                writer.WriteLine("Cashier: " + prepare);
                writer.WriteLine("");
                
                writer.WriteLine("\tItem Name\t Item Code  Price  Quantity ");
                writer.WriteLine("");
                for (int i = 0; i < bunifuCustomDataGrid1.Rows.Count; i++)
                {
                    writer.Write("\t" + bunifuCustomDataGrid1.Rows[i].Cells[1].Value.ToString() + "\t" + " " +
                    bunifuCustomDataGrid1.Rows[i].Cells[2].Value.ToString() + "  " + "  " +
                    bunifuCustomDataGrid1.Rows[i].Cells[3].Value.ToString() + "  " + "    " +
                    bunifuCustomDataGrid1.Rows[i].Cells[4].Value.ToString() + "   " + " ");
                    writer.WriteLine("");
                    writer.WriteLine("--------------------------------------------------");
                }
                writer.WriteLine("\t Total Amount \t \t \t" + total);
                writer.WriteLine("\t Down Payment \t \t \t" + cash);
                writer.WriteLine("\t Balance \t \t \t" + balance);
              
                writer.Close();
                MessageBox.Show("Your Bill is Created","",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void printer()
        {
            var installedPrinters = PrinterSettings.InstalledPrinters; //I have choosed a printername from 'installedPrinters'
            try
            {


                try
                {

                    int height = (tblinssummary.RowCount) * 10 + 50;
                    MadushaPrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Bill", 76, height);
                    MadushaPrintDocument.PrinterSettings.PrinterName = "PDFCreator"; //Specify the printer to use.

                    MadushaPrintDocument.PrintPage += new PrintPageEventHandler(this.MadushaPrintDocument_PrintPage);
                    MadushaPrintDocument.Print();



                }
                finally
                {

                    // MessageBox.Show("data Exported");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MadushaPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {


            DateTime time = DateTime.Now;
            string formatD = "yyyy-MM-dd";

            string SysTime = "2017-09-12";


            e.Graphics.DrawString("MADUSHA", new System.Drawing.Font("Century", 12, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Black, new System.Drawing.Point(70, 0)); // x,y
            e.Graphics.DrawString("SUPER MARKET", new System.Drawing.Font("Century", 12, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Black, new System.Drawing.Point(44, 20));
            e.Graphics.DrawString("No.181 , Galle Road", new System.Drawing.Font("Century", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(50, 40));
            e.Graphics.DrawString("Hikkaduwa", new System.Drawing.Font("Century", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(77, 55));
            e.Graphics.DrawString("Tel : 091 2277939 / 091 4946386", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(32, 75));
            e.Graphics.DrawString("Date : " + time.ToString(formatD), new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 100));
            e.Graphics.DrawString("Time : " + SysTime, new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(130, 100));
            e.Graphics.DrawString("Invoice No : " + lblin.Text, new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 115));
            e.Graphics.DrawString("Terminal : " + "001", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(130, 115));
            e.Graphics.DrawString("Cashier :" + "Surath", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 130));
            e.Graphics.DrawString("SalesRep : " + "Ruwan", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(130, 130));
            e.Graphics.DrawString("Customer : " + "Ruchira", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 145));
            e.Graphics.DrawString("-----------------------------------------------------------", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 155));

            int x = 165;
            int x1 = 180;
            for (int i = 0; i < tblinssummary.Rows.Count; i++)
            {
                e.Graphics.DrawString(tblinssummary.Rows[i].Cells[0].Value.ToString(), new System.Drawing.Font("Times New Roman", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, x));
                x = x + 30;
                if ((Convert.ToInt32(tblinssummary.Rows[i].Cells[2].Value.ToString().Length) == 5))
                {

                    if ((Convert.ToInt32(tblinssummary.Rows[i].Cells[4].Value.ToString().Length) >= 5))
                    {
                        e.Graphics.DrawString(tblinssummary.Rows[i].Cells[1].Value.ToString() + "\t" + tblinssummary.Rows[i].Cells[3].Value.ToString() + "\t  " + tblinssummary.Rows[i].Cells[2].Value.ToString() + "\t  " + tblinssummary.Rows[i].Cells[4].Value.ToString(), new System.Drawing.Font("Times New Roman", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, x1));
                    }
                }
                else if ((Convert.ToInt32(tblinssummary.Rows[i].Cells[2].Value.ToString().Length) == 4))
                {

                    if ((Convert.ToInt32(tblinssummary.Rows[i].Cells[4].Value.ToString().Length) >= 4))
                    {
                        e.Graphics.DrawString(tblinssummary.Rows[i].Cells[1].Value.ToString() + "\t" + tblinssummary.Rows[i].Cells[3].Value.ToString() + "\t    " + tblinssummary.Rows[i].Cells[2].Value.ToString() + "\t  " + tblinssummary.Rows[i].Cells[4].Value.ToString(), new System.Drawing.Font("Times New Roman", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, x1));
                    }
                }
                else
                {
                    e.Graphics.DrawString(tblinssummary.Rows[i].Cells[1].Value.ToString() + "\t" + tblinssummary.Rows[i].Cells[3].Value.ToString() + "\t" + tblinssummary.Rows[i].Cells[2].Value.ToString() + "\t" + tblinssummary.Rows[i].Cells[4].Value.ToString(), new System.Drawing.Font("Times New Roman", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, x1));
                }
                x1 = x1 + 30;

            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.Rows.Count > 0)
            {
                if (DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    try
                    {
                        MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("delete from supermarket.ordersi where iname='" + this.txtItmname.Text + "'", conn);

                        cmd.ExecuteNonQuery();



                        MessageBox.Show("Deleted Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tableLoadOrders();
                        txtItmname.Text = "";
                        lblicode.Text = "";
                        lbluprice.Text = "";
                        txtnouni.Text = "";
                        lblnamount.Text = "";
                        txtdis.Text = "";
                        lblfamount.Text = "";

                        conn.Close();
                    }
                    catch (Exception r)
                    {

                        MessageBox.Show(r.Message);
                    }
                }
            }
            else { MessageBox.Show("No Data To Be Deleted  !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        //validation
        public void RegexpSeized(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {


            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {

                pc.Image = inventory.Properties.Resources.invalid;
                n.SetToolTip(piccus, "NIC should have 9 digits followed with v or x");
                a1 = -99;

            }
            else
            {
                pc.Image = null;
                a1 = 0;

            }

        }
        public void Regexp2(string re, Bunifu.Framework.UI.BunifuMaterialTextbox tb, PictureBox pc, string s)
        {
            

            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {
              
                pc.Image = inventory.Properties.Resources.invalid;
                n.SetToolTip(pictureBox25, "NIC should have 9 digits followed with v or x");
                a = -99;

            }
            else
            {
                pc.Image = null;
                a = 0;

            }

        }

        public void Regexp3(string re, Bunifu.Framework.UI.BunifuMaterialTextbox tb, PictureBox pc, string s)
        {
            
            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {

                pc.Image = inventory.Properties.Resources.invalid;
                n.SetToolTip(pictureBox27, "Phone number should have exactly 10 digits");
                b = -99;

            }
            else
            {
                pc.Image = null;
                b = 0;

            }

        }

        public void Regexp4(string re, Bunifu.Framework.UI.BunifuMaterialTextbox tb, PictureBox pc, string s)
        {

            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {

                pc.Image = inventory.Properties.Resources.invalid;
                n.SetToolTip(pictureBox26, "Phone number should have exactly 10 digits");
                c = -99;
                

            }
            else
            {
                pc.Image = null;
                c = 0;

            }

        }

        //chart load

        public void chartLoadIExp()
        {
            chart2.Visible = true;
            chart2.Series["Type"].Points.Clear();
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select * from supermarket.incomeexpense ;", conn);
            MySqlDataReader myR;
            try
            {
                conn.Open();
                myR = cmd.ExecuteReader();
                while (myR.Read())
                {
                    this.chart2.Series["Type"].Points.AddXY(myR.GetString("type"), myR.GetInt32("amount"));
                }


            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }


        }

        public void chartLoadProLosBar()
        {
            chart1.Series["Type"].Points.Clear();
            chart1.Visible = true;
            
       
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select * from supermarket.incomeexpense ;", conn);
            MySqlDataReader myR;
            try
            {
                conn.Open();
                myR = cmd.ExecuteReader();

                while (myR.Read())
                {
                    this.chart1.Series["Type"].Points.AddXY(myR.GetString("type"), myR.GetInt32("amount"));
                }


            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }



        }

        public void chartLoadProLosPie()
        {
            chart3.Series["Type"].Points.Clear();
            chart3.Visible = true;
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select * from supermarket.incomeexpense ;", conn);
            MySqlDataReader myR;
            try
            {
                conn.Open();
                myR = cmd.ExecuteReader();

                while (myR.Read())
                {
                    this.chart3.Series["Type"].Points.AddXY(myR.GetString("type"), myR.GetInt32("amount"));
                }


            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }



        }

      

        //tab1 tables
        public void tableLoadItems()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select Item_name ,Item_code,Rprice as Price  from supermarket.item ;", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable tab = new DataTable();
                sda.Fill(tab);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = tab;
                bunifuCustomDataGrid3.DataSource = bsource;
                sda.Update(tab);
                
                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }




        }


        public void tableLoadOrders()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select orderid as Order_ID,iname as Item_Name,icode as Item_Code,uprice as Unit_Price,nou as Unit_Count,namount as Net_Amount,discount as Discount,famount as Final_Amount from supermarket.ordersi ;", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable tab = new DataTable();
                sda.Fill(tab);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = tab;
                bunifuCustomDataGrid1.DataSource = bsource;
                sda.Update(tab);
                bunifuCustomDataGrid1.Columns[0].Width = 130;
                bunifuCustomDataGrid1.Columns[1].Width = 73;
                bunifuCustomDataGrid1.Columns[2].Width = 70;
                bunifuCustomDataGrid1.Columns[3].Width = 45;
                bunifuCustomDataGrid1.Columns[4].Width = 65;
                bunifuCustomDataGrid1.Columns[5].Width = 60;
                bunifuCustomDataGrid1.Columns[6].Width = 78;
                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }




        }

        
        public void tableLoadinstallments()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select orderid as ID,gtot as Total,dpaymnt as Paid,bal as Balance,ivalue as Installment_value,warrenty as Warrenty,noi as Installments,preparedby as Prepared,date as Date from supermarket.installments ;", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable tab = new DataTable();
                sda.Fill(tab);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = tab;
                tableinstallments.DataSource = bsource;
                sda.Update(tab);
                tableinstallments.Columns[0].Width = 35;
                tableinstallments.Columns[1].Width = 65;
                tableinstallments.Columns[2].Width = 60;
                tableinstallments.Columns[3].Width = 65;
                tableinstallments.Columns[4].Width = 60;
                tableinstallments.Columns[5].Width = 60;
                tableinstallments.Columns[6].Width = 55;
                tableinstallments.Columns[7].Width = 70;
                tableinstallments.Columns[8].Width = 65;

                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }




        }



      

        //tab2 tables

        public void tableLoadSeizedItems()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select invoice_no,old_item_code,new_item_code,customer_id,balance_amount,date_seized  from supermarket.seized ;", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable tab = new DataTable();
                sda.Fill(tab);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = tab;
                tableseized.DataSource = bsource;
                sda.Update(tab);

                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            

        }

        public void tableLoadInstaCust()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select CustID,Cust_Name,Date_Due,Invoice_No,email as Email from supermarket.instcard  ;", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable tab = new DataTable();
                sda.Fill(tab);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = tab;
                tblinscust.DataSource = bsource;
                sda.Update(tab);

                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        public void tableLoadInstallmntformload()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select * from supermarket.instcust", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable tab = new DataTable();
                sda.Fill(tab);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = tab;
                tblinscust.DataSource = bsource;
                sda.Update(tab);

                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        public void tableLoadInstaSuumary(string nic)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
           
            MySqlCommand cmd = new MySqlCommand("select Installments,Date_Due,Amount,Status,Invoice_No from supermarket.instcard where CustID = '"+nic+"'", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable tab = new DataTable();
                sda.Fill(tab);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = tab;
                tblinssummary.DataSource = bsource;
                sda.Update(tab);

                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }




        }

        public void tableLoadInstaSuumary2(string nic)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

            MySqlCommand cmd = new MySqlCommand("select Installments,Date_Due,Amount,Status from supermarket.instcard where CustID = '" + nic + "'", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable tab = new DataTable();
                sda.Fill(tab);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = tab;
                tblhide.DataSource = bsource;
                sda.Update(tab);

                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }




        }

        public void tableLoadInstaSuumary()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

            MySqlCommand cmd = new MySqlCommand("select Installments,Date_Due,Amount,Status from supermarket.instcard", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable tab = new DataTable();
                sda.Fill(tab);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = tab;
                tblinssummary.DataSource = bsource;
                sda.Update(tab);

                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }




        }

        //tab3 tables



        public void tableLoadEI()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select *  from supermarket.incomeexpense ;", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable tab = new DataTable();
                sda.Fill(tab);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = tab;
                tblei.DataSource = bsource;
                sda.Update(tab);

                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        public void tableLoadEIHide()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select *  from supermarket.incomeexpense where date < '"+ dateseizedselect.Text+ "' ;", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable tab = new DataTable();
                sda.Fill(tab);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = tab;
                tblseizedhide.DataSource = bsource;
                sda.Update(tab);

                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        public void tableLoadBank()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select *  from supermarket.bank ;", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable tab = new DataTable();
                sda.Fill(tab);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = tab;
                tblbank.DataSource = bsource;
                sda.Update(tab);

                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }


        private void bunifuCustomDataGrid3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0)
            {
                DataGridViewRow row = this.bunifuCustomDataGrid3.Rows[e.RowIndex];
                txtItmname.Text = row.Cells["Item_name"].Value.ToString();
                lblicode.Text = row.Cells["Item_code"].Value.ToString();
                lbluprice.Text = row.Cells["Price"].Value.ToString();

               

            }
        }

        private void txtnouni_OnValueChanged(object sender, EventArgs e)

        {
            if (txtnouni.Text != "") {

                int nounits = Convert.ToInt32(txtnouni.Text);
                int uprice = Convert.ToInt32(lbluprice.Text);
                lblnamount.Text = (nounits * uprice).ToString();
            }
            

        }

        private void txtdis_OnValueChanged(object sender, EventArgs e)
        {
            if (txtdis.Text !="")
            {

                double dis = Convert.ToDouble(txtdis.Text);
                double net = Convert.ToDouble(lblnamount.Text);
                double discal = net * (dis / 100);
                lblfamount.Text = (net-discal).ToString();
            }
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.bunifuCustomDataGrid1.Rows[e.RowIndex];

                txtoid.Text = row.Cells["Order_ID"].Value.ToString();
                txtItmname.Text = row.Cells["Item_Name"].Value.ToString();
                lblicode.Text = row.Cells["Item_Code"].Value.ToString();
                lbluprice.Text = row.Cells["Unit_Price"].Value.ToString();
                txtnouni.Text = row.Cells["Unit_Count"].Value.ToString();
                lblnamount.Text = row.Cells["Net_Amount"].Value.ToString();
                txtdis.Text = row.Cells["Discount"].Value.ToString();
                lblfamount.Text = row.Cells["Final_Amount"].Value.ToString();
  
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtoid.Text) || String.IsNullOrWhiteSpace(txtItmname.Text) || String.IsNullOrWhiteSpace(txtnouni.Text) || String.IsNullOrWhiteSpace(txtdis.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

                     

            else
            {

                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                string query = "insert into supermarket.ordersi(orderid,iname ,icode ,uprice ,nou,namount,discount,famount)  values('" + this.txtoid.Text + "','" + this.txtItmname.Text + "','" + this.lblicode.Text + "','" + this.lbluprice.Text + "','" + this.txtnouni.Text + "','" + this.lblnamount.Text + "','" + this.txtdis.Text + "','" + this.lblfamount.Text + "');";
               
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader myR;
                try
                {
                    conn.Open();
                    myR = cmd.ExecuteReader();
                    MessageBox.Show("Item Added To Cart Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    while (myR.Read()) { }
                    tableLoadOrders();
                    conn.Close();

                    txtItmname.Text = "";
                    lblicode.Text = "";
                    lbluprice.Text = "";
                    txtnouni.Text = "";
                    lblnamount.Text = "";
                    txtdis.Text = "";
                    lblfamount.Text = "";
                }
                catch (Exception)
                {

                    MessageBox.Show("Item Already Exists In The Cart !! ","", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtnicsearch.Text) || String.IsNullOrWhiteSpace(txtgtot.Text) || String.IsNullOrWhiteSpace(lblfname.Text) || String.IsNullOrWhiteSpace(txtnoi.Text) || String.IsNullOrWhiteSpace(txtivalue.Text) || String.IsNullOrWhiteSpace(txtoid.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            else
            {
             

                string constring = "datasource=localhost;port=3306;username=root";
                string query = "insert into supermarket.suspend(nic,name,amount,date,noofins,insval,prepareby,invoiceno)  values('" + this.txtnicsearch.Text + "','" + this.lblfname.Text + "','" + this.txtgtot.Text + "','" + this.datei.Text + "','" + this.txtnoi.Text + "','" + this.txtivalue.Text + "','" + cmbprepare.Text + "','" + this.txtoid.Text + "');";
                MySqlConnection con = new MySqlConnection(constring);

                MySqlCommand cmd = new MySqlCommand(query, con);

                MySqlDataReader myR;



                try
                {
                    con.Open();


                    myR = cmd.ExecuteReader();
                   
                    while (myR.Read()) { }
                    
                    con.Close();

                    txtnicsearch.Text = "";
                    lblfname.Text = "";
                    txtgtot.Text = "";
                    txtname1.Text = "";
                    txtaddress1.Text = "";
                    txtphone1.Text = "";
                    txtjob1.Text = "";
                    txtname2.Text = "";
                    txtaddress2.Text = "";
                    txtphone2.Text = "";
                    txtjob2.Text = "";
                    lblnwi.Text = "";
                    lbladdr.Text = "";
                    lblemail.Text = "";
                    lblphone.Text = "";
                    lbldob.Text = "";
                    lbljob.Text = "";
                    lblcity.Text = "";
                    txtwmonths.Text = "";
                    txtoid.Text = "";
                    txtgtot.Text = "";
                    txtdpay.Text = "";
                    lblbal.Text = "";
                    txtnoi.Text = "";
                    txtivalue.Text = "";
                   



                    AdviceOfDispatch ad = new AdviceOfDispatch();
                    ad.ShowDialog();
                }

                catch (Exception)
                {

                   MessageBox.Show("Customer Already Exists in Suspension !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }


            
        }






        

        private void btnexecute_Click(object sender, EventArgs e)
        {
            //Random rnd = new Random();
            //int count = rnd.Next(1000,10000);

            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            string query = "select sum(famount) as tot from supermarket.ordersi";
           
            MySqlCommand cmd = new MySqlCommand(query, conn);

                       MySqlDataReader myR;
            try
            {
                conn.Open();
                myR = cmd.ExecuteReader();

                if (myR.Read())
                {
                    
                    txtgtot.Text = myR["tot"].ToString();
                    
                    //txtoid.Text = count.ToString();
                    
                }
                txtItmname.Text = "";
                lblicode.Text = "";
                lbluprice.Text = "";
                txtnouni.Text = "";
                lblnamount.Text = "";
                txtdis.Text = "";
                lblfamount.Text = "";
                conn.Close();
            }
            catch (Exception r)
            {


                MessageBox.Show(r.Message);
            }

          



        }

        public void sentDataToTable()
        {
            string con = "datasource=localhost;port=3306;username=root";
            string StrQuery;
            using (MySqlConnection conn = new MySqlConnection(con))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    try
                    {
                        comm.Connection = conn;
                        conn.Open();
                        for (int i = 0; i < bunifuCustomDataGrid1.Rows.Count; i++)
                        {
                            StrQuery = "INSERT INTO supermarket.permanentorder VALUES(" + bunifuCustomDataGrid1.Rows[i].Cells["Order_ID"].Value + ",'" + bunifuCustomDataGrid1.Rows[i].Cells["Item_Name"].Value + "','" + bunifuCustomDataGrid1.Rows[i].Cells["Item_Code"].Value + "'," + bunifuCustomDataGrid1.Rows[i].Cells["Unit_Price"].Value + "," + bunifuCustomDataGrid1.Rows[i].Cells["Unit_Count"].Value + "," + bunifuCustomDataGrid1.Rows[i].Cells["Net_Amount"].Value + "," + bunifuCustomDataGrid1.Rows[i].Cells["Discount"].Value + "," + bunifuCustomDataGrid1.Rows[i].Cells["Final_Amount"].Value + ")";
                            comm.CommandText = StrQuery;
                            comm.ExecuteNonQuery();

                        }
                        
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }




        }

        private void lbliprceshow_Click(object sender, EventArgs e)
        {

        }

        private void btniclear_Click(object sender, EventArgs e)
        {
            lbliprceshow.Text = "";
            lbl3m.Text = "";
            lbl6m.Text = "";
            lbl12m.Text = "";
            lbl24m.Text = "";
            lbl36m.Text = "";
        }

        private void bunifuMetroTextbox12_OnValueChanged(object sender, EventArgs e)
        {
            if (txtdpay.Text != "")
            {

                double gtot = Convert.ToDouble(txtgtot.Text);
                double dpay = Convert.ToDouble(txtdpay.Text);

                double bal = gtot-dpay;
                lblbal.Text = bal.ToString();
                lbliprceshow.Text =bal.ToString();

                //installment calculation
                //3 months = 5 %
                //6 months = 9 %
                //12 months = 12 %
                //24 months = 16.5 %
                //36 months = 20 %

                lbl3m.Text  = (Math.Round((bal + (bal * 0.05)) / 3,2)).ToString();
                lbl6m.Text  = (Math.Round((bal + (bal * 0.09)) / 6, 2)).ToString();
                lbl12m.Text = (Math.Round((bal + (bal * 0.12)) / 12, 2)).ToString();
                lbl24m.Text = (Math.Round((bal + (bal * 0.165)) / 24, 2)).ToString();
                lbl36m.Text = (Math.Round((bal + (bal * 0.2)) / 36, 2)).ToString();



            }


            
        }


        private void btncconfirm_Click(object sender, EventArgs e)
        {
            Regexp3(@"^\d{10}$", txtphone1, pictureBox27, "phone1");
            Regexp4(@"^\d{10}$", txtphone2, pictureBox26, "phone2");
            Regexp2(@"^\d{9}(x|v)$", txtnicsearch, pictureBox25, "NIC");

            if (String.IsNullOrEmpty(txtname1.Text) || String.IsNullOrWhiteSpace(txtaddress1.Text) || String.IsNullOrWhiteSpace(txtphone1.Text) || String.IsNullOrWhiteSpace(txtjob1.Text) || String.IsNullOrWhiteSpace(txtname2.Text) || String.IsNullOrWhiteSpace(txtaddress2.Text) || String.IsNullOrWhiteSpace(txtphone2.Text) || String.IsNullOrWhiteSpace(txtjob2.Text) || String.IsNullOrWhiteSpace(txtnicsearch.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (a==-99||b==-99||c==-99)
            { }

            else if(a == 0 && b == 0 && c == 0)
            {


                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                string query = "update supermarket.customer set name1='" + this.txtname1.Text + "',address1='" + this.txtaddress1.Text + "',phone1='" + this.txtphone1.Text + "',job1='" + this.txtjob1.Text + "',name2='" + this.txtname2.Text + "',address2='" + this.txtaddress2.Text + "',phone2='" + this.txtphone2.Text + "',job2='" + this.txtjob2.Text + "' where nic='" + this.txtnicsearch.Text + "';";
                // MySqlConnection con = new MySqlConnection(constring);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader myR;
                try
                {
                    conn.Open();
                    myR = cmd.ExecuteReader();
                    MessageBox.Show("Data Updated Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    while (myR.Read()) { }
                    conn.Close();
                    txtname1.Text = "";
                    txtaddress1.Text = "";
                    txtphone1.Text = "";
                    txtjob1.Text = "";
                    txtname2.Text = "";
                    txtaddress2.Text = "";
                    txtphone2.Text = "";
                    txtjob2.Text = "";

                    lblfname.Text = "";
                    lblnwi.Text = "";
                    lbladdr.Text = "";
                    lblemail.Text = "";
                    lblphone.Text = "";
                    lbldob.Text = "";
                    lbljob.Text = "";
                    lblcity.Text = "";
                    txtnicsearch.Text = "";


                }
                catch (Exception)
                {

                    throw;
                }


            }
        }



        private void btniconfirm_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtoid.Text) || String.IsNullOrWhiteSpace(txtgtot.Text) || String.IsNullOrWhiteSpace(txtdpay.Text) || String.IsNullOrWhiteSpace(lblbal.Text) || String.IsNullOrWhiteSpace(txtnoi.Text) || String.IsNullOrWhiteSpace(txtivalue.Text) || String.IsNullOrWhiteSpace(txtwmonths.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                string query = "insert into supermarket.installments(orderid,gtot,dpaymnt,bal,noi,ivalue,warrenty,preparedby,date)  values('" + this.txtoid.Text + "','" + this.txtgtot.Text + "','" + this.txtdpay.Text + "','" + this.lblbal.Text + "','" + this.txtnoi.Text + "','" + this.txtivalue.Text + "','" + this.txtwmonths.Text + "','" + this.cmbprepare.Text + "','" + this.datei.Text + "');";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader myR;
                try
                {

                    conn.Open();

                    myR = cmd.ExecuteReader();
                    MessageBox.Show("Data Inserted Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    while (myR.Read()) { }
                    tableLoadinstallments();

                    conn.Close();
                }
                catch (Exception)
                {

                    MessageBox.Show("Invoice Already Exsists !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }




                try
                {
                    MySqlConnection conn1 = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                    //String sDate = datei.Text;
                    String sDate = DateTime.Now.ToString("yyyy-MM-dd");
                    DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));


                    String dy = datevalue.Day.ToString();
                    String mn = datevalue.Month.ToString();
                    String yr = datevalue.Year.ToString();

                    int d = Convert.ToInt32(dy);
                    int m = Convert.ToInt32(mn);
                    int y = Convert.ToInt32(yr);

                    DateTime dueDate = new DateTime(y, m, d);

                    t = dueDate.ToString("yyyy-MM-dd");



                    int i = Convert.ToInt32(txtnoi.Text);
                    MySqlDataReader myR2;



                    for (int j = 1; j <= i; j++)
                    {
                        conn1.Open();
                        string query2 = "insert into supermarket.instcard (Installments, Date_Due, Amount, Status, Cust_Name, CustID, Invoice_No,Email)  values('" + j + "','" + t + "','" + this.txtivalue.Text + "','" + "Unpaid" + "','" + this.lblnwi.Text + "','" + this.txtnicsearch.Text + "','" + this.txtoid.Text + "','" + this.lblemail.Text + "');";

                        MySqlCommand cmd2 = new MySqlCommand(query2, conn1);

                        myR2 = cmd2.ExecuteReader();
                        while (myR2.Read()) { }

                        String sDate1 = t;
                        DateTime datevalue1 = (Convert.ToDateTime(sDate1.ToString()));


                        String dy1 = datevalue1.Day.ToString();
                        String mn1 = datevalue1.Month.ToString();
                        String yr1 = datevalue1.Year.ToString();

                        int d1 = Convert.ToInt32(dy1);
                        int m1 = Convert.ToInt32(mn1);
                        int y1 = Convert.ToInt32(yr1);

                        DateTime dueDate1 = new DateTime(y1, m1, d1);
                        DateTime x1 = dueDate1.AddMonths(1);
                        t = x1.ToString("yyyy-MM-dd");
                        //tableLoadInstallmntformload();
                        conn1.Close();
                    }





                }
                catch (Exception)
                {

                    throw;
                }



                try
                {
                    Random rnd1 = new Random();
                    int count = rnd1.Next(1, 10);
                   
                    MySqlConnection conn1 = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                    MySqlDataReader myR3;
                    string query3 = "insert into supermarket.instcust (Installments, Date_Due, Amount, Status, Cust_Name, CustID, Invoice_No,Email)  values('" + count + "','" + t + "','" + this.txtivalue.Text + "','" + "unpaid" + "','" + this.lblnwi.Text + "','" + this.txtnicsearch.Text + "','" + this.txtoid.Text + "','" + this.lblemail.Text + "');";
                    conn1.Open();
                    MySqlCommand cmd3 = new MySqlCommand(query3, conn1);

                    myR3 = cmd3.ExecuteReader();
                    while (myR3.Read()) { }
                    tableLoadInstallmntformload();
                    conn1.Close();

                }
                catch (Exception)
                {
                    throw;
                }
                }

        }

       

        

        private void r3_CheckedChanged(object sender, EventArgs e)
        {
            if (r3.Checked)
                txtivalue.Text = lbl3m.Text;
                txtnoi.Text = lbl3.Text;
        }

        private void r6_CheckedChanged(object sender, EventArgs e)
        {
            if (r6.Checked)
                txtivalue.Text = lbl6m.Text;
               txtnoi.Text = lbl6.Text;
        }

        private void r12_CheckedChanged(object sender, EventArgs e)
        {
            if (r12.Checked)
                txtivalue.Text = lbl12m.Text;
            txtnoi.Text = lbl12.Text;
        }

        private void r24_CheckedChanged(object sender, EventArgs e)
        {
            if (r24.Checked)
                txtivalue.Text = lbl24m.Text;
            txtnoi.Text = lbl24.Text;
        }

        private void r36_CheckedChanged(object sender, EventArgs e)
        {
            if (r36.Checked)
                txtivalue.Text = lbl36m.Text;
            txtnoi.Text = lbl36.Text;
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            txtname1.Text = "";
            txtaddress1.Text = "";
            txtphone1.Text = "";
            txtjob1.Text = "";
            txtname2.Text = "";
            txtaddress2.Text = "";
            txtphone2.Text = "";
            txtjob2.Text = "";
            txtnicsearch.Text = "";
            lblfname.Text = "";
            lblnwi.Text = "";
            lbladdr.Text = "";
            lblemail.Text = "";
            lblphone.Text = "";
            lbldob.Text = "";
            lbljob.Text = "";
            lblcity.Text = "";
            txtnicsearch.Text = "";
        }

        private void bunifuThinButton21_Click_1(object sender, EventArgs e)
        {
            AdviceOfDispatch o = new AdviceOfDispatch();
            o.Show();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

       


       

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {

            debtors d = new debtors();
            d.ShowDialog();
        }

        private void btnnicsearch_Click(object sender, EventArgs e)
        {
            Regexp2(@"^\d{9}(x|v)$", txtnicsearch, pictureBox25, "NIC");
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select count(*),fullname,nwinitials,address,email,phone,dob,job,city  from supermarket.customer where nic ='"+txtnicsearch.Text+"' ;", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
               
                int i = Convert.ToInt32(cmd.ExecuteScalar());

                if (i == 1)
                {
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        lblfname.Text = dr["fullname"].ToString();
                        lblnwi.Text = dr["nwinitials"].ToString();
                        lbladdr.Text = dr["address"].ToString();
                        lblemail.Text = dr["email"].ToString();
                        lblphone.Text = dr["phone"].ToString();
                        lbldob.Text = dr["dob"].ToString();
                        lbljob.Text = dr["job"].ToString();
                        lblcity.Text = dr["city"].ToString();
                    }


                }
                else
                {
                    MessageBox.Show("No Matching Customer Found !!","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    lblfname.Text = "";
                    lblnwi.Text = "";
                    lbladdr.Text = "";
                    lblemail.Text = "";
                    lblphone.Text = "";
                    lbldob.Text = "";
                    lbljob.Text = "";
                    lblcity.Text = "";




                }

                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.Rows.Count > 0)
            {


                double nou = Convert.ToDouble(txtnouni.Text);

                double uprice = Convert.ToDouble(lbluprice.Text);
                lblnamount.Text = (nou * uprice).ToString();
                double namt = nou * uprice;
                double dis = Convert.ToDouble(txtdis.Text);
                //  lblfamount.Text =  (namt-(namt*dis)).ToString();
                try
                {
                    MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update supermarket.ordersi set nou='" + this.txtnouni.Text + "',namount='" + this.lblnamount.Text + "',discount='" + this.txtdis.Text + "',famount='" + this.lblfamount.Text + "' where iname='" + this.txtItmname.Text + "'", conn);

                    cmd.ExecuteNonQuery();


                    MessageBox.Show("Data Updated Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    tableLoadOrders();
                    conn.Close();
                }
                catch (Exception r)
                {

                    MessageBox.Show("Select Data To Be Updated  !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else { MessageBox.Show("No Data To Be Updated  !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void txtname2_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtItmname_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lbluprice_Click(object sender, EventArgs e)
        {

        }

        private void txtname1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Hided)
            {
                Spanel.Width = Spanel.Width + 20;
                if (Spanel.Width >= PW)
                {
                    timer1.Stop();
                    Hided = false;
                    this.Refresh();
                }
            }
            else
            {
                Spanel.Width = Spanel.Width - 20;
                if (Spanel.Width <= 0)
                {
                    timer1.Stop();
                    Hided = true;
                    this.Refresh();

                }
            }
    }
        
        private void tblinscust_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            email ema = new email();
            ema.txtto.Text = tblinscust.SelectedRows[0].Cells[4].Value.ToString();
            ema.Show();
        }

        private void tblinscust_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string nic = tblinscust.SelectedRows[0].Cells[6].Value.ToString();


            tableLoadInstaSuumary(nic);

            tblinssummary.Visible = true;
        }


       

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            chartLoadIExp();
        }

        private void tableinstallments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void txtdpay_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtwmonths_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtphone1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtphone2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void bunifuMetroTextbox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void bunifuMetroTextbox20_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

      
        private void bunifuMetroTextbox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void groupBox14_Enter(object sender, EventArgs e)
        {

        }
        //item search

        private void TestForm_Load(object sender, EventArgs e)
        {
            search("");
            searchDebtors("","");
        }
        private void btnsearchitem_Click(object sender, EventArgs e)
        {
            string valueTo = txtItmname.Text.ToString();
            search(valueTo);
        }

        public void search(string valueTo)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

            MySqlCommand cmd = new MySqlCommand("select Item_name ,Item_code,Rprice as Price from supermarket.item where Item_name like '%" + valueTo + "%' ", conn);

            MySqlDataAdapter sda = new MySqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable dbt = new DataTable();
            sda.Fill(dbt);
            BindingSource bS = new BindingSource();

            bS.DataSource = dbt;
            bunifuCustomDataGrid3.DataSource = bS;


        }

        private void txtnouni_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtdis_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void btnaddSeized_Click(object sender, EventArgs e)
        {
            RegexpSeized(@"^\d{9}(x|v)$", txtnicse, piccus, "email");

            if (String.IsNullOrEmpty(txtnicse.Text) || String.IsNullOrWhiteSpace(txtino.Text) || String.IsNullOrWhiteSpace(txton.Text) || String.IsNullOrWhiteSpace(txtbal.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (a1==-99) { }


            else if (a1==0)
            {

                string constring = "datasource=localhost;port=3306;username=root";
                string query = "insert into supermarket.seized(invoice_no,old_item_code,new_item_code,customer_id,balance_amount,date_seized)  values('" + this.txtino.Text + "','" + this.txton.Text + "','" + this.txtnn.Text + "','" + this.txtnicse.Text + "','" + this.txtbal.Text + "','" + this.datese.Text + "');";
                MySqlConnection con = new MySqlConnection(constring);

                MySqlCommand cmd = new MySqlCommand(query, con);


                MySqlDataReader myR;



                try
                {
                    con.Open();


                    myR = cmd.ExecuteReader();
                    MessageBox.Show("Item Added Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    while (myR.Read()) { }
                    tableLoadSeizedItems();
                    con.Close();
                    txtnicse.Text = "";
                    txtino.Text = "";
                    txton.Text = "";
                    txtbal.Text = "";
                    txtnn.Text = "";
                    seizedlblAddDeleteCount();

                }

                catch (Exception)
                {

                    MessageBox.Show("Invoice Number Already Exists !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
        }

        private void txton_OnValueChanged(object sender, EventArgs e)
        {
            txtnn.Text = "US" + txton.Text;
        }

        private void tableseized_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.tableseized.Rows[e.RowIndex];
                txtnicse.Text = row.Cells["customer_id"].Value.ToString();
                txtino.Text = row.Cells["invoice_no"].Value.ToString();
                txton.Text = row.Cells["old_item_code"].Value.ToString();
                txtnn.Text = row.Cells["new_item_code"].Value.ToString();
                txtbal.Text = row.Cells["balance_amount"].Value.ToString();
                datese.Text = row.Cells["date_seized"].Value.ToString();
                

            }
        }

        private void btndeleteSeized_Click(object sender, EventArgs e)
        {
            if (txtino.Text != "")
            {
                if (DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    try
                    {
                        MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("delete from supermarket.seized where invoice_no='" + this.txtino.Text + "'", conn);


                        cmd.ExecuteNonQuery();



                        MessageBox.Show("Deleted Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tableLoadSeizedItems();
                        txtnicse.Text = "";
                        txtino.Text = "";
                        txton.Text = "";
                        txtnn.Text = "";
                        txtbal.Text = "";
                        datese.Text = "";
                        seizedlblAddDeleteCount();

                        conn.Close();
                    }
                    catch (Exception r)
                    {

                        MessageBox.Show(r.Message);
                    }
                }
            }
            else { MessageBox.Show("Please Select a Record to be Deleted !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }



        }

       
        public void seizedlblAddDeleteCount()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd1 = new MySqlCommand("select sum(balance_amount) as tot from supermarket.seized", conn);
            MySqlCommand cmd2 = new MySqlCommand("select count(*) as tot from supermarket.seized", conn);
            try
            {

                conn.Open();

                lbltot.Text = cmd1.ExecuteScalar().ToString();
                lblscount.Text = cmd2.ExecuteScalar().ToString();



                conn.Close();
            }
            catch (Exception r)
            {

                MessageBox.Show(r.Message);
            }
        }

        public void BankAddDeleteCount()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

            MySqlCommand cmd1 = new MySqlCommand("select count(*) as tot from supermarket.bank", conn);
            MySqlCommand cmd2 = new MySqlCommand("select DATE_FORMAT(ldate,'%Y-%m-%d') as Date from supermarket.bank order by ldate desc", conn);
            MySqlCommand cmd3 = new MySqlCommand("select sum(lamount) as tot from supermarket.bank", conn);
            MySqlCommand cmd4 = new MySqlCommand("select count(*)  from supermarket.bank where status='Paid'", conn);
            try
            {

                conn.Open();

                lblcount.Text = cmd1.ExecuteScalar().ToString();
                lbldate.Text = cmd2.ExecuteScalar().ToString();
                lblbtot.Text = cmd3.ExecuteScalar().ToString();
                lblpcount.Text = cmd4.ExecuteScalar().ToString();


                conn.Close();
            }
            catch (Exception r)
            {

                MessageBox.Show(r.Message);
            }
        }

        private void label47_Click(object sender, EventArgs e)
        {

        }


        private void btnaddbank_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(lblbid.Text) || String.IsNullOrWhiteSpace(txtpurp.Text)) { 
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



            else
            {

                string constring = "datasource=localhost;port=3306;username=root";
                string query = "insert into supermarket.bank(id,bname,branch,accno,lamount,status,buyer,purpose,ldate,datecomp) values('" + this.lblbid.Text + "','" + cmdbname.Text + "','" + cmdbranch.Text + "','" + cmdaccno.Text + "','"+this.txtlamt .Text+ "','" + bankradio + "','" + cmdbuyer.Text + "','" + this.txtpurp.Text + "','" + this.datetak.Text + "','" + this.datecom.Text + "');";
                MySqlConnection con = new MySqlConnection(constring);

                MySqlCommand cmd = new MySqlCommand(query, con);


                MySqlDataReader myR;



                try
                {
                    con.Open();
                    Random rnd = new Random();
                    int count1 = rnd.Next(100, 1000);
                    lblbid.Text = count1.ToString();

                    myR = cmd.ExecuteReader();
                    MessageBox.Show("Loan Details Added Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    while (myR.Read()) { }
                    tableLoadBank();
                    BankAddDeleteCount();
                    con.Close();
                    txtlamt.Text = "";
                    txtpurp.Text = "";

                }

                catch (Exception)
                {
                    
                   MessageBox.Show("Loan ID Already Exists !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
           bankradio = "Paid";
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            
            bankradio = "Unpaid";
        }

        private void label54_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {

        }


        private void txtlamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void tblbank_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.tblbank.Rows[e.RowIndex];
                lblbid.Text = row.Cells["id"].Value.ToString();
                cmdbname.Text = row.Cells["bname"].Value.ToString();
                cmdbranch.Text = row.Cells["branch"].Value.ToString();
                cmdaccno.Text = row.Cells["accno"].Value.ToString();
                txtlamt.Text = row.Cells["lamount"].Value.ToString();
                datecom.Text = row.Cells["datecomp"].Value.ToString();
                datetak.Text = row.Cells["ldate"].Value.ToString();
                cmdbuyer.Text = row.Cells["buyer"].Value.ToString();
                txtpurp.Text = row.Cells["purpose"].Value.ToString();

                bankradio = row.Cells["status"].Value.ToString();
                if (bankradio == "Paid")
                {
                    radbpa.Checked = true;

                }
                else
                    radbnpa.Checked = true;




            }



        }

        private void bunifuThinButton211_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("update supermarket.instcard set Date_Due='" + this.dates.Text + "' , Status='" + summaryradio + "' where Installments='" + this.lbl.Text+ "'", conn);

                cmd.ExecuteNonQuery();



                MessageBox.Show("Data Updated Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string nic = tblinscust.SelectedRows[0].Cells[6].Value.ToString();
                tableLoadInstaSuumary(nic);
                conn.Close();
            }
            catch (Exception )
            {
                //throw;

                MessageBox.Show("Data Updation Failed !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tblinssummary_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.tblinssummary.Rows[e.RowIndex];
                lblin.Text = row.Cells["Invoice_No"].Value.ToString();
                dates.Text = row.Cells["Date_Due"].Value.ToString();
                lbl.Text = row.Cells["Installments"].Value.ToString();
                summaryradio = row.Cells["Status"].Value.ToString();
                if (summaryradio == "Paid")
                {
                    radsumm.Checked = true;

                }
                else
                    radsumm1.Checked = true;


               

            }
        }

        private void radsumm_CheckedChanged(object sender, EventArgs e)
        {
            summaryradio = "Paid";
        }

        private void radsumm1_CheckedChanged(object sender, EventArgs e)
        {
            summaryradio = "Unpaid";
        }

        private void btnupdatebank_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("update supermarket.bank set bname='" + this.cmdbname.Text + "' , branch='" + this.cmdbranch.Text + "', accno='" + this.cmdaccno.Text + "', lamount='" + this.txtlamt.Text + "', status='" + bankradio + "', buyer='" + this.cmdbuyer.Text + "', purpose='" + this.txtpurp.Text + "', ldate='" + this.datetak.Text + "', datecomp='" + this.datecom.Text + "' where id='" + this.lblbid.Text + "'", conn);

                cmd.ExecuteNonQuery();



                MessageBox.Show("Data Updated Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                tableLoadBank();
                txtlamt.Text = "";
                txtpurp.Text = "";
                BankAddDeleteCount();

                conn.Close();
            }
            catch (Exception r)
            {

                MessageBox.Show(r.Message);
                MessageBox.Show("Data Updation Failed !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bunifuThinButton219_Click(object sender, EventArgs e)
        {

            if (DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("delete from supermarket.bank where id='" + this.lblbid.Text + "'", conn);

                    cmd.ExecuteNonQuery();



                    MessageBox.Show("Deleted Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tableLoadBank();
                    txtlamt.Text = "";
                    txtpurp.Text = "";
                    BankAddDeleteCount();
                    conn.Close();
                }
                catch (Exception r)
                {

                    MessageBox.Show(r.Message);
                }
            }
        }

        private void btnaddex_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtamt.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



            else
            {

                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                string query = "insert into supermarket.incomeexpense(id,category,type,amount,date) values('"+this.lblid.Text+"','" + status + "','" + this.cmbinex.Text + "','" + this.txtamt.Text + "','" + this.dateie.Text + "');";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader myR;
                try
                {
                    conn.Open();
                    Random rnd3 = new Random();
                    int count3 = rnd3.Next(100, 1000);
                    lblid.Text = count3.ToString();

                    myR = cmd.ExecuteReader();
                    MessageBox.Show("Data Added Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    while (myR.Read()) { }
                    tableLoadEI();
                    chartLoadIExp();
                    conn.Close();

                    txtamt.Text = "";
                    
                    

                }
                catch (Exception)
                {

                    MessageBox.Show("ID Already Exists !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void radexp_CheckedChanged(object sender, EventArgs e)
        {
            status = "Expense";
        }

        private void radinc_CheckedChanged(object sender, EventArgs e)
        {
            status = "Income";
        }

        private void txtamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }


        private void btnupdateie_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("update supermarket.incomeexpense set category='" + status + "' , type='" + this.cmbinex.Text + "', amount='" + this.txtamt.Text + "' where id='" + this.lblid.Text + "'", conn);

                cmd.ExecuteNonQuery();



                MessageBox.Show("Data Updated Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                tableLoadEI();
                txtamt.Text = "";
                
               // chartLoadIExp();

                conn.Close();
            }
            catch (Exception r)
            {

               // MessageBox.Show(r.Message);
                MessageBox.Show("Data Updation Failed !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tblei_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.tblei.Rows[e.RowIndex];
                lblid.Text = row.Cells["id"].Value.ToString();
                cmbinex.Text = row.Cells["type"].Value.ToString();
                txtamt.Text = row.Cells["amount"].Value.ToString();
                dateie.Text = row.Cells["date"].Value.ToString();
                status = row.Cells["category"].Value.ToString();
                if (status == "Expense")
                {
                    radexp.Checked = true;

                }
                else
                    radinc.Checked = true;




            }

        }

        private void btndeleteei_Click(object sender, EventArgs e)
        {

            if (DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("delete from supermarket.incomeexpense where id='" + this.lblid.Text + "'", conn);

                    cmd.ExecuteNonQuery();



                    MessageBox.Show("Deleted Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tableLoadEI();
                    txtamt.Text = "";
                    chartLoadIExp();
                    conn.Close();
                }
                catch (Exception r)
                {

                    MessageBox.Show(r.Message);
                }
            }
        }

        private void bunifuImageButton5_Click_1(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton29_Click(object sender, EventArgs e)
        {
            tableLoadInstallmntformload();
        }

        private void bunifuThinButton28_Click(object sender, EventArgs e)
        {
            string valueFrom = datefrom.Text.ToString();
            string valueTo = dateto.Text.ToString();

            searchDebtors(valueFrom,valueTo);
        }

        public void searchDebtors(string valueFrom,string valueTo)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

            MySqlCommand cmd = new MySqlCommand("select * from supermarket.instcust where Date_Due between '"+ valueFrom + "' AND '"+ valueTo + "' ;", conn);

            MySqlDataAdapter sda = new MySqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable dbt = new DataTable();
            sda.Fill(dbt);
            BindingSource bS = new BindingSource();

            bS.DataSource = dbt;
            tblinscust.DataSource = bS;


        }

        private void btngeneratesticker_Click(object sender, EventArgs e)
        {
            if (txtino.Text != "")
            {
                BarcodeLib.Barcode barcode = new BarcodeLib.Barcode()
                {
                    IncludeLabel = true,
                    Alignment = AlignmentPositions.CENTER,
                    Width = 390,
                    Height = 150,
                    RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                };

                puty.Image = barcode.Encode(TYPE.CODE128B, txtino.Text);
            }
            else {
                MessageBox.Show("Please a Invoice","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        

        private void bunifuThinButton21_Click_3(object sender, EventArgs e)
        {
            Helper.SaveImageCapture(puty.Image);
        }

        private void btncustomercopy_Click(object sender, EventArgs e)
        {
            tableLoadInstaSuumary2(txtnicsearch.Text);

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@"aCustomer_Copy.pdf", FileMode.Create));
            doc.Open();

            MessageBox.Show("PDF Created Sucessfuly!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Add border to page
            PdfContentByte content = w.DirectContent;
            iTextSharp.text.Rectangle rectangle = new iTextSharp.text.Rectangle(doc.PageSize);
            rectangle.Left += doc.LeftMargin - 5;
            rectangle.Right -= doc.RightMargin - 5;
            rectangle.Top -= doc.TopMargin - 22;
            rectangle.Bottom += doc.BottomMargin - 5;
            content.SetColorStroke(BaseColor.BLACK);
            content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
            content.Stroke();


            //BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN,BaseFont.CP1252,BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 30, BaseColor.BLUE);
            Paragraph prg = new Paragraph();
            prg.Alignment = Element.ALIGN_CENTER;
            prg.Add(new Chunk("Customer Installment Copy", font5));
            doc.Add(prg);

            //Authors
            iTextSharp.text.Font font15 = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);
            Paragraph prg1 = new Paragraph();
            prg1.Alignment = Element.ALIGN_RIGHT;
            Paragraph prg2 = new Paragraph();
            prg2.Alignment = Element.ALIGN_RIGHT;
            prg1.Add(new Chunk("Prepared By: Upali Kariyawasam", font15));
            prg2.Add(new Chunk("Prepared Date: " + DateTime.Now.ToShortDateString(), font15));
            doc.Add(prg1);
            doc.Add(prg2);


            //line separator
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2.0f, 100.0f, BaseColor.BLACK, Element.ALIGN_CENTER, 9.0f)));
            doc.Add(p);

            PdfPTable table = new PdfPTable(tblhide.Columns.Count);

            //add headers from gridview to table
            iTextSharp.text.Font fonth = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);



            for (int j = 0; j < tblhide.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(tblhide.Columns[j].HeaderText.ToUpper(), fonth));
                table.AddCell(cell);

            }

            //flag first row as header
            table.HeaderRows = 1;


            //add actual rows from grid to table
            for (int i = 0; i < tblhide.Rows.Count; i++)
            {
                table.WidthPercentage = 100;

                for (int k = 0; k < tblhide.Columns.Count; k++)
                {
                    if (tblhide[k, i].Value != null)
                    {

                        table.AddCell(new Phrase(tblhide[k, i].Value.ToString()));
                    }

                }


            }

            //add out table
            doc.Add(table);

            doc.Close();

            System.Diagnostics.Process.Start(@"aCustomer_Copy.pdf");
        }

    



        private void bunifuThinButton214_Click_1(object sender, EventArgs e)
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@"aSeized_Goods.pdf", FileMode.Create));
            doc.Open();
            
            MessageBox.Show("PDF Created Sucessfuly!!","",MessageBoxButtons.OK,MessageBoxIcon.Information);

            //Add border to page
            PdfContentByte content = w.DirectContent;
            iTextSharp.text.Rectangle rectangle = new iTextSharp.text.Rectangle(doc.PageSize);
            rectangle.Left += doc.LeftMargin - 5;
            rectangle.Right -= doc.RightMargin - 5;
            rectangle.Top -= doc.TopMargin - 22;
            rectangle.Bottom += doc.BottomMargin - 5;
            content.SetColorStroke(BaseColor.BLACK);
            content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
            content.Stroke();


            //BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN,BaseFont.CP1252,BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 30, BaseColor.BLUE);
            Paragraph prg = new Paragraph();
            prg.Alignment = Element.ALIGN_CENTER;
            prg.Add(new Chunk("Seized Goods", font5));
            doc.Add(prg);

            //Authors
            iTextSharp.text.Font font15 = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);
            Paragraph prg1 = new Paragraph();
            prg1.Alignment = Element.ALIGN_RIGHT;
            Paragraph prg2 = new Paragraph();
            prg2.Alignment = Element.ALIGN_RIGHT;
            prg1.Add(new Chunk("Prepared By: Upali Kariyawasam", font15));
            prg2.Add(new Chunk("Prepared Date: " + DateTime.Now.ToShortDateString(), font15));
            doc.Add(prg1);
            doc.Add(prg2);


            //line separator
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2.0f, 100.0f, BaseColor.BLACK, Element.ALIGN_CENTER, 9.0f)));
            doc.Add(p);

            PdfPTable table = new PdfPTable(tableseized.Columns.Count);

            //add headers from gridview to table
            iTextSharp.text.Font fonth = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);



            for (int j = 0; j < tableseized.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(tableseized.Columns[j].HeaderText.ToUpper(), fonth));
                table.AddCell(cell);

            }

            //flag first row as header
            table.HeaderRows = 1;


            //add actual rows from grid to table
            for (int i = 0; i < tableseized.Rows.Count; i++)
            {
                table.WidthPercentage = 100;

                for (int k = 0; k < tableseized.Columns.Count; k++)
                {
                    if (tableseized[k, i].Value != null)
                    {

                        table.AddCell(new Phrase(tableseized[k, i].Value.ToString()));
                    }

                }


            }

            //add out table
            doc.Add(table);

            doc.Close();

            System.Diagnostics.Process.Start(@"aSeized_Goods.pdf");

        }


        private void pictureBox28_Click(object sender, EventArgs e)
        {
            cmbyear.Items.Add(txtaddy.Text);
            cmbyearselect.Items.Add(txtaddy.Text);
            txtaddy.Text = "";
        }

        private void pictureBox29_Click(object sender, EventArgs e)
        {
            cmbyear.Items.Remove(txtaddy.Text);
            cmbyearselect.Items.Remove(txtaddy.Text);
            txtaddy.Text = "";
        }

        private void btnprintrecep_Click(object sender, EventArgs e)
        {
            //printer();

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@"aSummary_Card.pdf", FileMode.Create));
            doc.Open();

            MessageBox.Show("PDF Created Sucessfuly!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Add border to page
            PdfContentByte content = w.DirectContent;
            iTextSharp.text.Rectangle rectangle = new iTextSharp.text.Rectangle(doc.PageSize);
            rectangle.Left += doc.LeftMargin - 5;
            rectangle.Right -= doc.RightMargin - 5;
            rectangle.Top -= doc.TopMargin - 22;
            rectangle.Bottom += doc.BottomMargin - 5;
            content.SetColorStroke(BaseColor.BLACK);
            content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
            content.Stroke();


            //BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN,BaseFont.CP1252,BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 30, BaseColor.BLUE);
            Paragraph prg = new Paragraph();
            prg.Alignment = Element.ALIGN_CENTER;
            prg.Add(new Chunk("Summary Card", font5));
            doc.Add(prg);

            //Authors
            iTextSharp.text.Font font15 = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);
            Paragraph prg1 = new Paragraph();
            prg1.Alignment = Element.ALIGN_RIGHT;
            Paragraph prg2 = new Paragraph();
            prg2.Alignment = Element.ALIGN_RIGHT;
            prg1.Add(new Chunk("Prepared By: Upali Kariyawasam", font15));
            prg2.Add(new Chunk("Prepared Date: " + DateTime.Now.ToShortDateString(), font15));
            doc.Add(prg1);
            doc.Add(prg2);


            //line separator
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2.0f, 100.0f, BaseColor.BLACK, Element.ALIGN_CENTER, 9.0f)));
            doc.Add(p);

            PdfPTable table = new PdfPTable(tblinssummary.Columns.Count);

            //add headers from gridview to table
            iTextSharp.text.Font fonth = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);



            for (int j = 0; j < tblinssummary.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(tblinssummary.Columns[j].HeaderText.ToUpper(), fonth));
                table.AddCell(cell);

            }

            //flag first row as header
            table.HeaderRows = 1;


            //add actual rows from grid to table
            for (int i = 0; i < tblinssummary.Rows.Count; i++)
            {
                table.WidthPercentage = 100;

                for (int k = 0; k < tblinssummary.Columns.Count; k++)
                {
                    if (tblinssummary[k, i].Value != null)
                    {

                        table.AddCell(new Phrase(tblinssummary[k, i].Value.ToString()));
                    }

                }


            }

            //add out table
            doc.Add(table);

            doc.Close();

            System.Diagnostics.Process.Start(@"aSummary_Card.pdf");


        }

        private void btnchartgene_Click(object sender, EventArgs e)
        {
            if (cmbchart.SelectedIndex==0)
            {
                chart3.Visible = false;
                
                chartLoadProLosBar();


            }
            else if(cmbchart.SelectedIndex == 1)
            {
                chart1.Visible = false;
                chart1.Series["Type"].Points.Clear();
                chartLoadProLosPie();

            }

            
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@"aUnpaid_List.pdf", FileMode.Create));
            doc.Open();

            MessageBox.Show("PDF Created Sucessfuly!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Add border to page
            PdfContentByte content = w.DirectContent;
            iTextSharp.text.Rectangle rectangle = new iTextSharp.text.Rectangle(doc.PageSize);
            rectangle.Left += doc.LeftMargin - 5;
            rectangle.Right -= doc.RightMargin - 5;
            rectangle.Top -= doc.TopMargin - 22;
            rectangle.Bottom += doc.BottomMargin - 5;
            content.SetColorStroke(BaseColor.BLACK);
            content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
            content.Stroke();


            //BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN,BaseFont.CP1252,BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 30, BaseColor.BLUE);
            Paragraph prg = new Paragraph();
            prg.Alignment = Element.ALIGN_CENTER;
            prg.Add(new Chunk("Unpaid List", font5));
            doc.Add(prg);

            //Authors
            iTextSharp.text.Font font15 = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);
            Paragraph prg1 = new Paragraph();
            prg1.Alignment = Element.ALIGN_RIGHT;
            Paragraph prg2 = new Paragraph();
            prg2.Alignment = Element.ALIGN_RIGHT;
            prg1.Add(new Chunk("Prepared By: Upali Kariyawasam", font15));
            prg2.Add(new Chunk("Prepared Date: " + DateTime.Now.ToShortDateString(), font15));
            doc.Add(prg1);
            doc.Add(prg2);


            //line separator
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2.0f, 100.0f, BaseColor.BLACK, Element.ALIGN_CENTER, 9.0f)));
            doc.Add(p);

            PdfPTable table = new PdfPTable(tblinscust.Columns.Count);

            //add headers from gridview to table
            iTextSharp.text.Font fonth = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);



            for (int j = 0; j < tblinscust.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(tblinscust.Columns[j].HeaderText.ToUpper(), fonth));
                table.AddCell(cell);

            }

            //flag first row as header
            table.HeaderRows = 1;


            //add actual rows from grid to table
            for (int i = 0; i < tblinscust.Rows.Count; i++)
            {
                table.WidthPercentage = 100;

                for (int k = 0; k < tblinscust.Columns.Count; k++)
                {
                    if (tblinscust[k, i].Value != null)
                    {

                        table.AddCell(new Phrase(tblinscust[k, i].Value.ToString()));
                    }

                }


            }

            //add out table
            doc.Add(table);

            doc.Close();

            System.Diagnostics.Process.Start(@"aUnPaid_List.pdf");
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
           
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            
            MySqlCommand cmd1 = new MySqlCommand("delete from supermarket.ordersi where orderid='" + this.txtoid.Text + "'", conn);
            sentDataToTable();
            try
            {

                conn.Open();


                cmd1.ExecuteNonQuery();

                tableLoadOrders();
                txtItmname.Text = "";
                lblicode.Text = "";
                lbluprice.Text = "";
                txtnouni.Text = "";
                lblnamount.Text = "";
                txtdis.Text = "";
                lblfamount.Text = "";
                txtnicsearch.Text = "";
                lblfname.Text = "";
                txtgtot.Text = "";
                txtname1.Text = "";
                txtaddress1.Text = "";
                txtphone1.Text = "";
                txtjob1.Text = "";
                txtname2.Text = "";
                txtaddress2.Text = "";
                txtphone2.Text = "";
                txtjob2.Text = "";
                lblnwi.Text = "";
                lbladdr.Text = "";
                lblemail.Text = "";
                lblphone.Text = "";
                lbldob.Text = "";
                lbljob.Text = "";
                lblcity.Text = "";
                txtwmonths.Text = "";
                txtoid.Text = "";
                txtgtot.Text = "";
                txtdpay.Text = "";
                lblbal.Text = "";
                txtnoi.Text = "";
                txtivalue.Text = "";

                conn.Close();
            }
            catch (Exception r)
            {

                MessageBox.Show(r.Message);
            }


        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int count = rnd.Next(1000, 10000);
            txtoid.Text = count.ToString();
        }

        private void bunifuThinButton218_Click(object sender, EventArgs e)
        {

        }

        private void btninvoice_Click(object sender, EventArgs e)
        {
            ConvertText();
        }

        private void btnclearsei_Click(object sender, EventArgs e)
        {
            txtnicse.Text = "";
            txtino.Text = "";
            txton.Text = "";
            txtbal.Text = "";
            txtnn.Text = "";
            puty.Image = null;
        }

        private void txtino_OnValueChanged(object sender, EventArgs e)
        {
            if (!(txtino.Text.Length == 0))
            {

                try
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);

                    MySqlCommand cm = new MySqlCommand("Select old_item_code,new_item_code,customer_id,balance_amount,date_seized from supermarket.seized where invoice_no='" + txtino.Text + "'", dbcon);

                    dbcon.Open();
                    DataSet d = new DataSet();
                    MySqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        txton.Text = sdr["old_item_code"].ToString();
                        txtnn.Text = sdr["new_item_code"].ToString();
                        txtnicse.Text = sdr["customer_id"].ToString();
                        txtbal.Text = sdr["balance_amount"].ToString();
                        datese.Text = sdr["date_seized"].ToString();
                        

                    }
                    dbcon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void btnseized_Click(object sender, EventArgs e)
        {
            tableLoadEIHide();

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@"aUnpaid_List.pdf", FileMode.Create));
            doc.Open();

            MessageBox.Show("PDF Created Sucessfuly!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Add border to page
            PdfContentByte content = w.DirectContent;
            iTextSharp.text.Rectangle rectangle = new iTextSharp.text.Rectangle(doc.PageSize);
            rectangle.Left += doc.LeftMargin - 5;
            rectangle.Right -= doc.RightMargin - 5;
            rectangle.Top -= doc.TopMargin - 22;
            rectangle.Bottom += doc.BottomMargin - 5;
            content.SetColorStroke(BaseColor.BLACK);
            content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
            content.Stroke();


            //BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN,BaseFont.CP1252,BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 30, BaseColor.BLUE);
            Paragraph prg = new Paragraph();
            prg.Alignment = Element.ALIGN_CENTER;
            prg.Add(new Chunk("Unpaid List", font5));
            doc.Add(prg);

            //Authors
            iTextSharp.text.Font font15 = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);
            Paragraph prg1 = new Paragraph();
            prg1.Alignment = Element.ALIGN_RIGHT;
            Paragraph prg2 = new Paragraph();
            prg2.Alignment = Element.ALIGN_RIGHT;
            prg1.Add(new Chunk("Prepared By: Upali Kariyawasam", font15));
            prg2.Add(new Chunk("Prepared Date: " + DateTime.Now.ToShortDateString(), font15));
            doc.Add(prg1);
            doc.Add(prg2);


            //line separator
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2.0f, 100.0f, BaseColor.BLACK, Element.ALIGN_CENTER, 9.0f)));
            doc.Add(p);

            PdfPTable table = new PdfPTable(tblseizedhide.Columns.Count);

            //add headers from gridview to table
            iTextSharp.text.Font fonth = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);



            for (int j = 0; j < tblseizedhide.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(tblseizedhide.Columns[j].HeaderText.ToUpper(), fonth));
                table.AddCell(cell);

            }

            //flag first row as header
            table.HeaderRows = 1;


            //add actual rows from grid to table
            for (int i = 0; i < tblseizedhide.Rows.Count; i++)
            {
                table.WidthPercentage = 100;

                for (int k = 0; k < tblseizedhide.Columns.Count; k++)
                {
                    if (tblseizedhide[k, i].Value != null)
                    {

                        table.AddCell(new Phrase(tblseizedhide[k, i].Value.ToString()));
                    }

                }


            }

            //add out table
            doc.Add(table);

            doc.Close();

            System.Diagnostics.Process.Start(@"aUnPaid_List.pdf");

        }

        private void bunifuCards5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            chartLoadIExp();
        }
    }
    }
    


 

