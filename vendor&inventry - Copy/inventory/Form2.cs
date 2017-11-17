using MySql.Data.MySqlClient;
using System;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Xml;
using System.Net;
using inventory;
using System.Drawing.Printing;

namespace madushaTemp
{
    
    
    public partial class Form2 : Form
    {
        public Boolean cat = true;

        public Form2()
        {
            InitializeComponent();

            //string stat;


            int v = Session.getUser();
            if (v == 0)
            {
                //button1.Enabled = false;
                // ((Control)this.tabPage1).Enabled = false;
                //tabPage1.Enabled = false;
                // tabControl1.TabPages.Remove(tabPage4);

            }
            else if (v == 1)
            {
                //button2.Enabled = false;
                tabControl1.TabPages.Remove(tabPage5);
                // ((Control)this.tabPage2).Enabled = false;
            }
            else if( v == 2 )
            { }




            LoadTableDelivery_request();
            FillCombo();
            LoadTableDriver_Availability();
            LoadTableShedule();
           
            bunifuCustomLabel43.Text= IncrementID("MS/PO/0000009", 9);


        }


        //DataTable dbdataset;
        string availability;
       //string status;
        double lorryprice = 20.00;
        double threewprice = 15.00;
        double bikeprice = 10.00;
        double distance;

       
       
        public void LoadTableDelivery_request()
        {
            string st = "datasource=localhost;port=3306;username=root";
            MySqlConnection condb = new MySqlConnection(st);
            MySqlCommand cmddb = new MySqlCommand("select * from supermarket.delivery_request ;", condb);

            try
            {
                MySqlDataAdapter sd = new MySqlDataAdapter();
                sd.SelectCommand = cmddb;
                DataTable dbdataset = new DataTable();
                sd.Fill(dbdataset);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dbdataset;
                bunifuCustomDataGrid1.DataSource = bsource;
                sd.Update(dbdataset);
                bunifuCustomDataGrid1.Columns[1].Width = 100;
                bunifuCustomDataGrid1.Columns[6].Width = 150;
                bunifuCustomDataGrid1.Columns[9].Width = 170;
                condb.Close();
            }
            catch (Exception ex)
            {
                throw;
                //MessageBox.Show(ex.Message.ToString(), "Error");
            }

        }




        //Printer Configured to print Receipt
        public void printer()
        {
            var installedPrinters = PrinterSettings.InstalledPrinters; //I have choosed a printername from 'installedPrinters'
            try
            {


                try
                {

                    int height = 100;
                    MadushaPrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Bill", 76, height);
                    MadushaPrintDocument.PrinterSettings.PrinterName = "Send To OneNote 2013"; //Specify the printer to use.

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

            e.Graphics.DrawString("MADUSHA", new System.Drawing.Font("Century", 12, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Black, new System.Drawing.Point(70, 0)); // x,y
            e.Graphics.DrawString("SUPER MARKET", new System.Drawing.Font("Century", 12, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Black, new System.Drawing.Point(44, 20));
            e.Graphics.DrawString("No. 46, ", new System.Drawing.Font("Century", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(50, 40));
            e.Graphics.DrawString("Deraniyagala", new System.Drawing.Font("Century", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(77, 55));
            e.Graphics.DrawString("Tel : 036 2249369 / 071 5555533", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(32, 75));
            e.Graphics.DrawString("Date : " + time.ToString(formatD), new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 100));
            //e.Graphics.DrawString("Time : " + SysTime, new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(130, 100));
            //e.Graphics.DrawString("Invoice No : " + txtt4invoiceno.Text, new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 115));
           // e.Graphics.DrawString("Repair ID : " + txtt4Rid.Text, new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(130, 115));
            e.Graphics.DrawString("Cashier :" + "Surath", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 130));
            e.Graphics.DrawString("SalesRep : " + "Ruwan", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(130, 130));
            e.Graphics.DrawString("Customer : " + "Ruchira", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 145));
            e.Graphics.DrawString("-----------------------------------------------------------", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 155));
            

            e.Graphics.DrawString("Serial No", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 210));
            e.Graphics.DrawString(":" + bunifuCustomLabel43.Text, new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(160, 210));
            e.Graphics.DrawString("Date", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 230));
            e.Graphics.DrawString(":" + bunifuCustomLabel44.Text, new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(160, 230));

            e.Graphics.DrawString("Driver Name", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 250));
            e.Graphics.DrawString(":" + bunifuCustomLabel12.Text, new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(160, 250));
            e.Graphics.DrawString("Driver NIC", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 270));
            e.Graphics.DrawString(":" + bunifuCustomLabel18.Text, new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(160, 270));
            e.Graphics.DrawString("Vehicle No", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 290));
            e.Graphics.DrawString(":" + bunifuCustomLabel45.Text, new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(160, 290));
            e.Graphics.DrawString("Customer Name", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 310));
            e.Graphics.DrawString(":" + bunifuCustomLabel47.Text, new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(160, 310));

            e.Graphics.DrawString("Address", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 330));
            e.Graphics.DrawString(":" + bunifuCustomLabel48.Text, new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(160, 330));

            e.Graphics.DrawString("Phone No", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 350));
            e.Graphics.DrawString(":" + bunifuCustomLabel51.Text, new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(160, 350));

            e.Graphics.DrawString("Driver Signature", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 370));
            e.Graphics.DrawString(":" + ".......................", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(160, 370));

            e.Graphics.DrawString("Manager Signature", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 390));
            e.Graphics.DrawString(":" + ".......................", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(160, 390));

            e.Graphics.DrawString("-----------------------------------------------------------", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 155));
        }


        public void LoadTableDriver_Availability()
        {
            string st = "datasource=localhost;port=3306;username=root";
            MySqlConnection condb = new MySqlConnection(st);
            MySqlCommand cmddb = new MySqlCommand("select driver_id,driver_name,delivery_date,availability as status from supermarket.driver_availability ;", condb);

            try
            {
                MySqlDataAdapter sd = new MySqlDataAdapter();
                sd.SelectCommand = cmddb;
                DataTable dbdataset = new DataTable();
                sd.Fill(dbdataset);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dbdataset;
                bunifuCustomDataGrid2.DataSource = bsource;
                sd.Update(dbdataset);


                condb.Close();
            }
            catch (Exception ex)
            {
                throw;
                // MessageBox.Show(ex.Message.ToString(), "Error");
            }

        }


        public void LoadTableShedule()
        {
            string st = "datasource=localhost;port=3306;username=root";
            MySqlConnection condb = new MySqlConnection(st);
            MySqlCommand cmddb = new MySqlCommand("select  o.OrderID,o.Customer,o.OrderDate,o.address,o.ToBuy,d.driver_name,dr.delivery_date,dr.availability as status from supermarket.orders o ,supermarket.delivery_request d,supermarket.driver_availability dr where o.OrderID=d.order_id and dr.driver_name=d.driver_name and o.ToBuy='Home Delivery';", condb);

            try
            {
                MySqlDataAdapter sd = new MySqlDataAdapter();
                sd.SelectCommand = cmddb;
                DataTable dbdataset = new DataTable();
                sd.Fill(dbdataset);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dbdataset;
                bunifuCustomDataGrid3.DataSource = bsource;
                sd.Update(dbdataset);
                condb.Close();
            }
            catch (Exception ex)
            {
                throw;
               // MessageBox.Show(ex.Message.ToString(), "Error");
            }

        }


        public void LoadTableOrderDetails()
        {
            string st = "datasource=localhost;port=3306;username=root";
            MySqlConnection condb = new MySqlConnection(st);
            MySqlCommand cmddb = new MySqlCommand("select OrderID,ItemName,UnitPrice,Quantity,Discount,SubTotal from supermarket.orderdetails where OrderID='"+txtoid.Text+"' ;", condb);

            try
            {
                MySqlDataAdapter sd = new MySqlDataAdapter();
                sd.SelectCommand = cmddb;
                DataTable dbdataset = new DataTable();
                sd.Fill(dbdataset);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dbdataset;
                dataGridView1.DataSource = bsource;
                sd.Update(dbdataset);
                dataGridView1.Columns[1].Width = 150;
                condb.Close();
            }
            catch (Exception ex)
            {
                throw;
                //MessageBox.Show(ex.Message.ToString(), "Error");
            }

        }






        //filling the vehical combox
        public void FillCombo()
        {

            string st = "datasource=localhost;port=3306;username=root";
            MySqlConnection condb = new MySqlConnection(st);
            string q = "select * from supermarket.vehicle ;";
            MySqlCommand cmddb = new MySqlCommand(q, condb);
            MySqlDataReader myReader;

            try
            {
                condb.Open();
                myReader = cmddb.ExecuteReader();

                while (myReader.Read())
                {

                    string stype = myReader.GetString("type");
                    txtvtype.Items.Add(stype);

                }


            }
            catch (Exception ex)
            {
                throw;
                // MessageBox.Show(ex.Message.ToString(), "Error");
            }



        }




        private void bunifuCustomLabel23_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel35_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox16_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel29_Click(object sender, EventArgs e)
        {

        }

      
        


        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            string source = txtsource.Text; 

            string destination = txtdesti.Text;

            //if (String.IsNullOrEmpty(txtsource.Text) || String.IsNullOrWhiteSpace(txtdesti.Text))
            //{
            //    MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            //Regexp(@"^[a-zA-Z]$", txtsource, pictureBox6, "source");
            //Regexp(@"^[a-zA-Z]$", txtdesti, pictureBox7, "destination");
         






            try
            {
                StringBuilder qyeryaddress = new StringBuilder();
                qyeryaddress.Append("http://maps.google.com/maps?q=");

                if (source != string.Empty)
                {
                    qyeryaddress.Append(source + "," + "+");
                }
                if (destination != string.Empty)
                {
                    qyeryaddress.Append(destination + "," + "+");
                }

                webBrowser1.Navigate(qyeryaddress.ToString());
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message.ToString(), "Error");
                throw;
            }




        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {

            try { 
            string st = "datasource=localhost;port=3306;username=root";

            MySqlConnection condb = new MySqlConnection(st);
                 string q = "select* from supermarket.driver_availability where driver_id like '%"+ txtdrivrid.Text +"%' and delivery_date  like '%"+ dateTimePicker1.Text +"%'";
               // string q = "select e.Empid,a.date from supermarket.employee_details e,attendance a where e.Empid=a.empid and e.position='Driver'";
                MySqlCommand cmddb = new MySqlCommand(q, condb);
               // MySqlDataReader myReader;


                MySqlDataAdapter sd = new MySqlDataAdapter();
                sd.SelectCommand = cmddb;
                DataTable dbdataset = new DataTable();
                sd.Fill(dbdataset);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dbdataset;
                bunifuCustomDataGrid2.DataSource = bsource;
                sd.Update(dbdataset);
                condb.Close();

                

            }
            catch (Exception ex)
            {
                throw;

                //  MessageBox.Show(ex.Message.ToString(), "Error");
            }




        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void bunifuThinButton27_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtdname.Text) || String.IsNullOrWhiteSpace(txtnicc.Text) || String.IsNullOrWhiteSpace(txtvno.Text) || String.IsNullOrWhiteSpace(txtpho.Text)|| String.IsNullOrWhiteSpace(txtvtype.Text)|| String.IsNullOrWhiteSpace(txtdis.Text)|| String.IsNullOrWhiteSpace(txtcusn.Text)|| String.IsNullOrWhiteSpace(txtcuspho.Text)|| String.IsNullOrWhiteSpace(txtoid.Text)|| String.IsNullOrWhiteSpace(txtadd.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {


                string st = "datasource=localhost;port=3306;username=root";
                string q = "insert into supermarket.delivery_request(driver_name,nic,vehicle_no,phone,vehicle_type,distance,cus_name,cus_phone,order_id,cus_add,delivery_cost) values ('" + txtdname.Text + "','" + this.txtnicc.Text + "','" + this.txtvno.Text + "','" + this.txtpho.Text + "','" + this.txtvtype.SelectedItem + "','" + this.txtdis.Text + "','" + this.txtcusn.Text + "','" + this.txtcuspho.Text + "','" + this.txtoid.Text + "','" + this.txtadd.Text + "','" + this.textBox2.Text + "'); ";
                cat = true;
                Regexp(@"^[0-9]{9}[v|V]$", txtnicc, pictureBox2, "NIC");
                Regexp(@"^[0-9]{3}[0-9]{7}$", txtpho, pictureBox1, "Phone");
                Regexp(@"^[0-9]{3}[0-9]{7}$", txtcuspho, pictureBox3, "Phone no");

                if (cat == true)
                {
                    MySqlConnection condb = new MySqlConnection(st);
                    MySqlCommand cmddb = new MySqlCommand(q, condb);
                    MySqlDataReader myReader;

                    try
                    {
                        condb.Open();
                        myReader = cmddb.ExecuteReader();

                        MessageBox.Show("Inserted !!!!!!! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);



                        while (myReader.Read())
                        {



                        }

                    }
                    catch (Exception ex)
                    {
                      //  MessageBox.Show(ex.Message.ToString(), "Error");

                        throw;
                    }

                }
            }
        }


        public void Regexp(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {
            ToolTip buttonToolTip = new ToolTip();
            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (regex.IsMatch(tb.Text))
            {

                pc.Image = null;


            }
            else
            {
                pc.Image = inventory.Properties.Resources.delete_icon;
                n.SetToolTip(pictureBox1, "Please enter a ten digit phone number.");
                n.SetToolTip(pictureBox2, "Please include numbers in your nic.");
                n.SetToolTip(pictureBox3, "Please enter a ten digit phone number.");
                cat = false;
            }
            
            
            

        }







        private void bunifuThinButton23_Click_1(object sender, EventArgs e)
        {
            vehicle_availa f2 = new vehicle_availa();   //open second form
            f2.Show();
          
            
           // this.Hide();
           

        }



       


        private void bunifuCustomLabel47_Click(object sender, EventArgs e)
        {
           
        }

        private void Email_Click(object sender, EventArgs e)
        {

           Emaild f3 = new Emaild();   //open second form
            f3.Show();
            




        }

        private void bunifuThinButton219_Click(object sender, EventArgs e)
        {
            string st = "datasource=localhost;port=3306;username=root";
            string q = "update supermarket.delivery_request set driver_name='" + this.txtdname.Text + "',vehicle_no='" + this.txtvno.Text + "',phone='" + this.txtpho.Text + "',vehicle_type='" + this.txtvtype.Text + "',distance='" + this.txtdis.Text + "',cus_name='" + this.txtcusn.Text + "',cus_phone='" + this.txtcuspho.Text + "',order_id='" + this.txtoid.Text + "',cus_add='" + this.txtadd.Text + "',delivery_cost='" + this.textBox2.Text + "'  where nic ='" + this.txtnicc.Text + "' ; ";
            //Regexp(@"^[0-9]{9}[v|V]$", txtnicc, pictureBox2, "NIC");
            //Regexp(@"^[0-9]{3}[0-9]{7}$", txtpho, pictureBox1, "Phone");
            //Regexp(@"^[0-9]{3}[0-9]{7}$", txtpho, pictureBox3, "Phone no");
            MySqlConnection condb = new MySqlConnection(st);
            MySqlCommand cmddb = new MySqlCommand(q, condb);
            MySqlDataReader myReader;
            cat = true;
            Regexp(@"^[0-9]{9}[v|V]$", txtnicc, pictureBox2, "NIC");
            Regexp(@"^[0-9]{3}[0-9]{7}$", txtpho, pictureBox1, "Phone");
            Regexp(@"^[0-9]{3}[0-9]{7}$", txtcuspho, pictureBox3, "Phone no");

            if (cat == true)
            {
                try
                {
                    condb.Open();
                    myReader = cmddb.ExecuteReader();

                    MessageBox.Show("Data Updated Successfully!","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTableDelivery_request();
                    while (myReader.Read())
                    {



                    }
                }

                catch (Exception ex)
                {
                    throw;
                    //  MessageBox.Show(ex.Message.ToString(), "Error");
                }
            }
          

        }

        private void bunifuCustomDataGrid1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.bunifuCustomDataGrid1.Rows[e.RowIndex];
                txtdname.Text = row.Cells["driver_name"].Value.ToString();
                txtnicc.Text = row.Cells["nic"].Value.ToString();
                txtvno.Text = row.Cells["vehicle_no"].Value.ToString();
                txtpho.Text = row.Cells["phone"].Value.ToString();
                txtvtype.Text = row.Cells["vehicle_type"].Value.ToString();
                txtdis.Text = row.Cells["distance"].Value.ToString();
                txtcusn.Text = row.Cells["cus_name"].Value.ToString();
                txtcuspho.Text = row.Cells["cus_phone"].Value.ToString();
                txtoid.Text = row.Cells["order_id"].Value.ToString();
               // txtitemname.Text = row.Cells["item_name"].Value.ToString();
                txtadd.Text = row.Cells["cus_add"].Value.ToString();
                textBox2.Text = row.Cells["delivery_cost"].Value.ToString();




            }
        }

        private void bunifuThinButton218_Click(object sender, EventArgs e)
        {
            string st = "datasource=localhost;port=3306;username=root";
            string q = "delete from supermarket.delivery_request where nic ='" + this.txtnicc.Text + "' ; ";
            MySqlConnection condb = new MySqlConnection(st);
            MySqlCommand cmddb = new MySqlCommand(q, condb);
            MySqlDataReader myReader;

            if (DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {

                try
                {
                    condb.Open();
                    myReader = cmddb.ExecuteReader();
                    MessageBox.Show("Deleted Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtdname.Text = "";
                    txtnicc.Text = "";
                    txtvno.Text = "";
                    txtpho.Text = "";
                    txtvtype.Text = "";
                    txtdis.Text = "";
                    txtcusn.Text = "";
                    txtcuspho.Text = "";
                    txtoid.Text = "";
                  //  txtitemname.Text = "";
                    txtadd.Text = "";
                    textBox2.Text = "";

                    LoadTableDelivery_request();
                    while (myReader.Read())
                    {



                    }
                }
                catch (Exception ex)
                {
                    throw;
                    // MessageBox.Show(ex.Message.ToString(), "Error");
                }
            }
        }

        private void bunifuThinButton217_Click(object sender, EventArgs e)
        {
            txtdname.Text = "";
            txtnicc.Text = "";
            txtvno.Text = "";
            txtpho.Text = "";
            txtvtype.Text = "";
            txtdis.Text = "";
            txtcusn.Text = "";
            txtcuspho.Text = "";
            txtoid.Text = "";
           // txtitemname.Text = "";
            txtadd.Text = "";
            textBox2.Text = "";
        }


        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtdrid.Text) || String.IsNullOrWhiteSpace(txtdrname.Text) || String.IsNullOrWhiteSpace(dateTime1.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }







            else
            {
                string st = "datasource=localhost;port=3306;username=root";
                string q = "insert into supermarket.driver_availability(driver_id,driver_name,delivery_date,availability) values ('" + txtdrid.Text + "','" + this.txtdrname.Text + "','" + this.dateTime1.Text + "','" + availability + "'); ";

                MySqlConnection condb = new MySqlConnection(st);
                MySqlCommand cmddb = new MySqlCommand(q, condb);
                MySqlDataReader myReader;

                try
                {
                    condb.Open();
                    myReader = cmddb.ExecuteReader();

                    MessageBox.Show("Data Inserted Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTableDriver_Availability();


                    while (myReader.Read())
                    {



                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message.ToString(), "Error");
                    throw;
                }

            }

        }

        private void bunifuThinButton28_Click(object sender, EventArgs e)
        {

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@"Delivery_Requests.pdf", FileMode.Create));
            doc.Open();

            MessageBox.Show("Create PDF sucessfuly!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Add border to page
            PdfContentByte content = w.DirectContent;
            iTextSharp.text.Rectangle rectangle = new iTextSharp.text.Rectangle(doc.PageSize);
            rectangle.Left += doc.LeftMargin - 5;
            rectangle.Right -= doc.RightMargin - 5;
            rectangle.Top -= doc.TopMargin - 22;
            rectangle.Bottom += doc.BottomMargin - 5;
            content.SetColorStroke(BaseColor.BLUE);
            content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
            content.Stroke();


            //BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN,BaseFont.CP1252,BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 30, BaseColor.BLUE);
            Paragraph prg = new Paragraph();
            prg.Alignment = Element.ALIGN_CENTER;
            prg.Add(new Chunk("Delivery Requests", font5));
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

            PdfPTable table = new PdfPTable(bunifuCustomDataGrid1.Columns.Count);

            //add headers from gridview to table
            iTextSharp.text.Font fonth = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);



            for (int j = 0; j < bunifuCustomDataGrid1.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(bunifuCustomDataGrid1.Columns[j].HeaderText.ToUpper(), fonth));
                table.AddCell(cell);

            }

            //flag first row as header
            table.HeaderRows = 1;


            //add actual rows from grid to table
            for (int i = 0; i < bunifuCustomDataGrid1.Rows.Count; i++)
            {
                table.WidthPercentage = 100;

                for (int k = 0; k < bunifuCustomDataGrid1.Columns.Count; k++)
                {
                    if (bunifuCustomDataGrid1[k, i].Value != null)
                    {

                        table.AddCell(new Phrase(bunifuCustomDataGrid1[k, i].Value.ToString()));
                    }

                }


            }

            //add out table
            doc.Add(table);

            doc.Close();
            System.Diagnostics.Process.Start(@"Delivery_Requests.pdf");
        }





        private void bunifuCustomDataGrid2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.bunifuCustomDataGrid2.Rows[e.RowIndex];

                txtdrid.Text = row.Cells["driver_id"].Value.ToString();
                txtdrname.Text = row.Cells["driver_name"].Value.ToString();
                dateTime1.Text = row.Cells["delivery_date"].Value.ToString();
                availability = row.Cells["availability"].Value.ToString();
                if (availability == "delivering")
                {
                    radiodel.Checked = true;

                }
                else
                    radiohold.Checked = true;



            }
        }





        private void bunifuCustomDataGrid3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    DataGridViewRow row = this.bunifuCustomDataGrid3.Rows[e.RowIndex];

            //    txtdrid.Text = row.Cells["driver_id"].Value.ToString();
            //    txtdrname.Text = row.Cells["driver_name"].Value.ToString();
            //    dateTime1.Text = row.Cells["delivery_date"].Value.ToString();
            //    // dateTimePicker1.Text = row.Cells["DOB"].Value.ToString();

            //    // txtvtype.Text = row.Cells["vehicle_type"].Value.ToString();




            //}
        }




        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            availability = "delivering";

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            availability = "hold";
        }



        private void bunifuThinButton210_Click_1(object sender, EventArgs e)
        {
            txtdrid.Text = "";
            txtdrname.Text = "";
            dateTime1.Text = "";
            radiodel.Checked = false;
            radiohold.Checked = false;
        }




        //calculate distance submit button
        private void bunifuThinButton211_Click(object sender, EventArgs e)
        {
            GetDistance(bunifuMetroTextbox2.Text, bunifuMetroTextbox1.Text);
            //txtdis.Text = bunifuCustomLabel8.Text;

        }



        //calculating dsitance and time function
        public void GetDistance(string origin, string destination)
        {
            string url = @"http://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + destination + "&sensor=false";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();

            DataSet ds = new DataSet();
            ds.ReadXml(new XmlTextReader(new StringReader(responsereader)));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables["element"].Rows[0]["status"].ToString() == "OK")
                {
                    bunifuCustomLabel7.Text = ds.Tables["duration"].Rows[0]["text"].ToString();
                    bunifuCustomLabel8.Text = ds.Tables["distance"].Rows[0]["text"].ToString();
                }
            }

        }


        //search  in shedule by delivery date
        private void bunifuThinButton29_Click(object sender, EventArgs e)
        {
            try
            {
                string st = "datasource=localhost;port=3306;username=root";

                MySqlConnection condb = new MySqlConnection(st);
                string q = "select  o.OrderID,o.Customer,o.OrderDate,o.address,d.driver_name,dr.delivery_date,dr.availability from supermarket.orders o INNER JOIN supermarket.delivery_request d ON o.OrderID=d.order_id JOIN  supermarket.driver_availability dr ON dr.driver_name=d.driver_name where delivery_date  like '%" + dateTimePicker3.Text + "%'";
                MySqlCommand cmddb = new MySqlCommand(q, condb);
                // MySqlDataReader myReader;


                MySqlDataAdapter sd = new MySqlDataAdapter();
                sd.SelectCommand = cmddb;
                DataTable dbdataset = new DataTable();
                sd.Fill(dbdataset);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dbdataset;
                bunifuCustomDataGrid3.DataSource = bsource;
                sd.Update(dbdataset);
                condb.Close();



            }
            catch (Exception ex)
            {
                throw;
                // MessageBox.Show(ex.Message.ToString(), "Error");
            }



        }



        //calculating distance
        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            distance = Convert.ToDouble(this.txtdis.Text);



            if (txtvtype.SelectedItem.ToString() == "lorry")
            {
                double p1 = (distance * lorryprice)-(1*lorryprice);
                textBox2.Text = Convert.ToString(p1);


            }

            else if (txtvtype.SelectedItem.ToString() == "threewheeler")
            {
                double p2 = (distance * threewprice)-(1*threewprice);
                textBox2.Text = Convert.ToString(p2);


            }

            else if (txtvtype.SelectedItem.ToString() == "bike")
            {

                double p3 = (distance * bikeprice)-(1*bikeprice);
                textBox2.Text = Convert.ToString(p3);

            }
        }



        //loading the chart 
        private void bunifuThinButton212_Click(object sender, EventArgs e)
        {
            string st = "datasource=localhost;port=3306;username=root";
            MySqlConnection condb = new MySqlConnection(st);
            string q = "select * from supermarket.delivery_request ;";
            MySqlCommand cmddb = new MySqlCommand(q, condb);
            MySqlDataReader myReader;



            try
            {
                condb.Open();
                myReader = cmddb.ExecuteReader();

                while (myReader.Read())
                {
                    

                    this.chart1.Series["distance"].Points.AddXY(myReader.GetString("driver_name"), myReader.GetString("distance"));

                }


            }

            catch (Exception ex)
            {
                throw;
                // MessageBox.Show(ex.Message);
            }


        }




        private void bunifuThinButton213_Click(object sender, EventArgs e)
        {
            txtdrivrid.Text = "";
            dateTimePicker1.Text = "";

        }





        private void bunifuCustomLabel48_Click(object sender, EventArgs e)
        {
        }
     

        private void bunifuCustomLabel51_Click(object sender, EventArgs e)
        {
        }

        private void bunifuCustomLabel45_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel12_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuCustomLabel18_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuCustomLabel43_Click(object sender, EventArgs e)
        {


         
        }







        public static string IncrementID(string startValue, int numNonDigits)
        {
            string nonDigits = startValue.Substring(0, numNonDigits);
            int len = startValue.Length - numNonDigits;
            int number = int.Parse(startValue.Substring(numNonDigits));
            number++;
            if (number >= Math.Pow(10, len)) number = 1; // start again at 1
            return String.Format("{0}{1:D" + len.ToString() + "}", nonDigits, number);
        }

        private void bunifuCustomLabel44_Click(object sender, EventArgs e)
        {
        }




        //gatepass generate buutton
        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            LoadTableOrderDetails();
            bunifuCustomLabel47.Text = txtcusn.Text;
            bunifuCustomLabel48.Text = txtadd.Text;
            bunifuCustomLabel51.Text = txtcuspho.Text;
            bunifuCustomLabel45.Text = txtvno.Text;
            bunifuCustomLabel12.Text = txtdname.Text;
            bunifuCustomLabel18.Text = txtnicc.Text;
            DateTime dateTime = DateTime.Now;
            this.bunifuCustomLabel44.Text = dateTime.ToString();


        }

        private void txtdname_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtdis_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtnicc_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtpho_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtcuspho_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }





        //updating the schedule table
        private void bunifuThinButton220_Click(object sender, EventArgs e)
        {
            string c = "completed";
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

            string query = "update supermarket.driver_availability dr,supermarket.delivery_request d,supermarket.orders o set dr.availability='" + c + "' where o.OrderID=d.order_id and dr.driver_name=d.driver_name and d.order_id ='" + this.bunifuMetroTextbox4.Text + "';";
            // MySqlConnection con = new MySqlConnection(constring);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader myR;
            try
            {
                conn.Open();
                myR = cmd.ExecuteReader();
                MessageBox.Show("Data Updated Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTableShedule();
                while (myR.Read()) { }
                conn.Close();



            }
            catch (Exception)
            {

                throw;
            }


            //  bunifuCustomDataGrid3.Rows[1].Cells[6].Value = "completed";


            //foreach (DataRow dr in bunifuCustomDataGrid3.Rows)
            //{
            //    dr["availability"]= "completed";
            //}






        }

        private void bunifuThinButton216_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(bunifuMetroTextbox2.Text) || String.IsNullOrWhiteSpace(bunifuMetroTextbox1.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            else
            {
                string st = "datasource=localhost;port=3306;username=root";
                string q = "insert into supermarket.location(source,destination,time,distance) values ('" + bunifuMetroTextbox2.Text + "','" + this.bunifuMetroTextbox1.Text + "','" + this.bunifuCustomLabel7.Text + "','" + this.bunifuCustomLabel8.Text + "'); ";

                MySqlConnection condb = new MySqlConnection(st);
                MySqlCommand cmddb = new MySqlCommand(q, condb);
                MySqlDataReader myReader;

                try
                {
                    condb.Open();
                    myReader = cmddb.ExecuteReader();

                    MessageBox.Show("Data Inserted Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTableDriver_Availability();


                    while (myReader.Read())
                    {



                    }
                }
                catch (Exception ex)
                {
                   // MessageBox.Show(ex.Message.ToString(), "Error");
                    throw;
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtdis_OnValueChanged(object sender, EventArgs e)
        {
             
        }

        private void txtnicc_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton221_Click(object sender, EventArgs e)
        {

            try
            {
                string st = "datasource=localhost;port=3306;username=root";

                MySqlConnection condb = new MySqlConnection(st);
                string q = "select OrderID,Customer,Phone,Address from supermarket.orders where OrderID like '%" + txtoid.Text + "%'";
                MySqlCommand cmddb = new MySqlCommand(q, condb);
                MySqlDataReader myReader;

                try
                {
                    condb.Open();
                    myReader = cmddb.ExecuteReader();

                    while (myReader.Read())
                    {
                        txtcusn.Text = myReader[1].ToString();
                        txtcuspho.Text = myReader[2].ToString();
                        txtadd.Text = myReader[3].ToString();





                    }


                }

                catch (Exception ex)
                {
                    throw;
                    // MessageBox.Show(ex.Message);
                }


                //MySqlDataAdapter sd = new MySqlDataAdapter();
                //sd.SelectCommand = cmddb;
                //DataTable dbdataset = new DataTable();
                //sd.Fill(dbdataset);
                //BindingSource bsource = new BindingSource();

                //bsource.DataSource = dbdataset;
                ////bunifuCustomDataGrid2.DataSource = bsource;
                //sd.Update(dbdataset);
                //condb.Close();



            }
            catch (Exception)
            {

                throw;
            }
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void bunifuThinButton222_Click(object sender, EventArgs e)
        {
            try
            {
                string st = "datasource=localhost;port=3306;username=root";

                MySqlConnection condb = new MySqlConnection(st);
                string q = "select  driver_name,vehicle_no,vehicle_type from supermarket.vehicle_details  where driver_name like '%" + txtdname.Text + "%'";
                MySqlCommand cmddb = new MySqlCommand(q, condb);
                MySqlDataReader myReader;

                try
                {
                    condb.Open();
                    myReader = cmddb.ExecuteReader();

                    while (myReader.Read())
                    {
                        txtvno.Text = myReader[1].ToString();
                        txtvtype.Text = myReader[2].ToString();
                      





                    }


                }

                catch (Exception ex)
                {
                    throw;
                    // MessageBox.Show(ex.Message);


                }

            }
            catch (Exception)
            {

                throw;
            }

            
            }



        //check the order item
        private void bunifuThinButton223_Click(object sender, EventArgs e)
        {
            try
            {
                string st = "datasource=localhost;port=3306;username=root";

                MySqlConnection condb = new MySqlConnection(st);
                string q = "select * from supermarket.orderdetails where OrderID like '%" + bunifuMetroTextbox3.Text + "%' ";
                MySqlCommand cmddb = new MySqlCommand(q, condb);
                // MySqlDataReader myReader;


                MySqlDataAdapter sd = new MySqlDataAdapter();
                sd.SelectCommand = cmddb;
                DataTable dbdataset = new DataTable();
                sd.Fill(dbdataset);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dbdataset;
                bunifuCustomDataGrid4.DataSource = bsource;
                sd.Update(dbdataset);
                
                condb.Close();



            }
            catch (Exception)
            {

                throw;
            }






        }

        private void bunifuThinButton215_Click(object sender, EventArgs e)
        {
            printer();
        }

        private void bunifuThinButton214_Click(object sender, EventArgs e)
        {

        }


        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e) { }

        private void bunifuCustomLabel21_Click(object sender, EventArgs e)
        {

        }

        private void dateTime1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtdrid_OnValueChanged(object sender, EventArgs e)

        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            driverReport d = new driverReport();
            d.Show();
        }
    }
}
