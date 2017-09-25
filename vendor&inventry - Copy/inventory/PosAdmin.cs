﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;



namespace inventory
{
    public partial class PosAdmin : Form
    {
        public PosAdmin()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            startDate.Format = DateTimePickerFormat.Custom;
            startDate.CustomFormat = "yyyy-MM-dd";
            startDate.ShowUpDown = false;


            endDate.Format = DateTimePickerFormat.Custom;
            endDate.CustomFormat = "yyyy-MM-dd";
            endDate.ShowUpDown = false;

            InitOrderCartHeaders();

           
        }

        public void InitOrderCartHeaders()
        {
            orderCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            orderCart.Columns.Add("ItemName", "ItemName");
            orderCart.Columns.Add("UnitPrice", "UnitPrice");
            orderCart.Columns.Add("Quantity", "Quantity");
            orderCart.Columns.Add("Discount", "Discount");
            orderCart.Columns.Add("SubTotal", "SubTotal");
           

        }

        private void inv_Load(object sender, EventArgs e)
        {
            
            this.recordsellingdetailsTableAdapter.Fill(this.supermarketDataSet2.recordsellingdetails);
            
            this.ordersTableAdapter.Fill(this.supermarketDataSet1.orders);

            count_accout();
            timer1.Start();
            DateTime time = DateTime.Now;
            string formatD = "yyyy-MM-dd";
            TimeTest.Text = time.ToString(formatD);
        }

        //Method

        //LoadChartDateWise
        public void LoadChart()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

            string sDate = startDate.Text;
            string eDate = endDate.Text;

            
            MySqlCommand cmd = new MySqlCommand("SELECT a.InvoiceID,a.ItemName,a.Qty,a.unitPrice,a.SubTotal,a.date,b.Customer,b.Total FROM supermarket.recordsellingdetails a, supermarket.invoicerecords b Where a.InvoiceID = b.InvoiceID AND ItemName LIKE '" + txtSearch.Text + "' AND date Between '" + sDate + "' AND '" + eDate + "' ", conn);

            MySqlDataReader myReader;

            SpeedChart.Series[0].Points.Clear();


            try
            {
                //Generate Graph according to database details
                conn.Open();
                myReader = cmd.ExecuteReader();
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        SpeedChart.Series[0].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;

                        this.SpeedChart.Series[0].Points.AddXY(myReader.GetDateTime("date"), myReader.GetInt32("qty"));
                        SpeedChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        
                        SpeedChart.Series[0].IsValueShownAsLabel = true;
                        SpeedChart.Series[0].MarkerStyle = MarkerStyle.Square;
                        SpeedChart.Series[0].MarkerSize = 12;
                        SpeedChart.Series[0].MarkerColor = Color.Navy;

                    }
                }
                else
                {
                    MessageBox.Show("No Enough Data","Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //LoadChartInvoiceWise
        public void LoadChartInvoice()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=madusha");

            string sInv = txtStartIn.Text;
            string eInv = txtEndIn.Text;


            MySqlCommand cmd = new MySqlCommand("SELECT a.InvoiceID, a.ItemName, a.Qty, a.unitPrice, a.SubTotal, a.date, b.Customer, b.Total FROM supermarket.recordsellingdetails a, supermarket.invoicerecords b Where a.InvoiceID = b.InvoiceID AND ItemName LIKE '" + txtSearch.Text + "' AND a.InvoiceID Between '" + sInv + "' AND '" + eInv + "' ", conn);

            MySqlDataReader myReader;

            SpeedChart.Series[0].Points.Clear();


            try
            {
                //Generate Graph according to database details
                conn.Open();
                myReader = cmd.ExecuteReader();
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        SpeedChart.Series[0].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;

                        this.SpeedChart.Series[0].Points.AddXY(myReader.GetDateTime("date"), myReader.GetInt32("qty"));
                        SpeedChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                        SpeedChart.Series[0].IsValueShownAsLabel = true;
                        SpeedChart.Series[0].MarkerStyle = MarkerStyle.Square;
                        SpeedChart.Series[0].MarkerSize = 12;
                        SpeedChart.Series[0].MarkerColor = Color.Navy;

                    }
                }
                else
                {
                    MessageBox.Show("No Enough Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //loadTable When load chart(Date Wise)

        public void LoadDateWiseTable()
        {
            string sDate = startDate.Text;
            string eDate = endDate.Text;
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT a.InvoiceID,a.date,b.Customer,a.ItemName,a.unitPrice,a.Qty,a.SubTotal FROM supermarket.recordsellingdetails a, supermarket.invoicerecords b Where a.InvoiceID = b.InvoiceID AND ItemName LIKE '" + txtSearch.Text + "' AND date Between '" + sDate + "' AND '" + eDate + "' ", conn);

            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                adapter.Fill(ds, "items");
                itemSummry.DataSource = ds.Tables["items"];
                itemSummry.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //loadTable When load chart(Invoice Wise)

        public void LoadInvoiceWiseTable()
        {
            string sInv = txtStartIn.Text;
            string eInv = txtEndIn.Text;
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT a.InvoiceID,a.date,b.Customer,a.ItemName,a.unitPrice,a.Qty,a.SubTotal FROM supermarket.recordsellingdetails a, supermarket.invoicerecords b Where a.InvoiceID = b.InvoiceID AND ItemName LIKE '" + txtSearch.Text + "' AND a.InvoiceID Between '" + sInv + "' AND '" + eInv + "' ", conn);

            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                adapter.Fill(ds, "items");
                itemSummry.DataSource = ds.Tables["items"];
                itemSummry.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }








//Change Chart

        public void ChangeChart()
            {
            if (linkLabel1.Text == "View in Column Chart")
            {
                SpeedChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                SpeedChart.Series[0].MarkerSize = 10;
                SpeedChart.Series[0].IsValueShownAsLabel = true;
                SpeedChart.Series[0].MarkerStyle = MarkerStyle.Square;
                SpeedChart.Series[0].MarkerSize = 12;
                SpeedChart.Series[0].MarkerColor = Color.Navy;
            }
            else
            {
                SpeedChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                SpeedChart.Series[0].MarkerSize = 10;
                SpeedChart.Series[0].IsValueShownAsLabel = true;
                SpeedChart.Series[0].MarkerStyle = MarkerStyle.Square;
                SpeedChart.Series[0].MarkerSize = 12;
                SpeedChart.Series[0].MarkerColor = Color.Navy;
            }

        }

        //Items Load AutoComplete
        void ItemsAutoComplete()
        {
            txtSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();


            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");


            string sqlquery = "SELECT Item_name FROM item";
            MySqlCommand cmd = new MySqlCommand(sqlquery, conn);
            MySqlDataReader msReader;

            try
            {
                conn.Open();
                msReader = cmd.ExecuteReader(); ;

                while (msReader.Read())
                {
                    string sname = msReader.GetString(0);
                    coll.Add(sname);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

            txtSearch.AutoCompleteCustomSource = coll;


        }
        //Customer Search
        public void SearchCustomer()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");


                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = ("SELECT Surname,Initials,Mobile,AddressLine1,AddressLine2,City from supermarket.loyaltycustomer where Mobile LIKE '%" + txtPhone.Text + "%' ");
                MySqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {

                    OlblName.Text = r[0].ToString() +" " + r[1].ToString();
                    OlblPhone.Text = r[2].ToString();
                    OlblAddressLine1.Text = r[3].ToString();
                    OlblAddressLine2.Text = r[4].ToString();
                    OlblCity.Text = r[5].ToString();



                }
                r.Close();
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //InvoiceAuto Complete
        public void InvoiceAutoComplete()
        {
            txtStartIn.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtStartIn.AutoCompleteSource = AutoCompleteSource.CustomSource;


            txtEndIn.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtEndIn.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();


            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");


            string sqlquery = "SELECT InvoiceID FROM invoicerecords";
            MySqlCommand cmd = new MySqlCommand(sqlquery, conn);
            MySqlDataReader msReader;

            try
            {
                conn.Open();
                msReader = cmd.ExecuteReader(); ;

                while (msReader.Read())
                {
                    string sname = msReader.GetString(0);
                    coll.Add(sname);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

            txtStartIn.AutoCompleteCustomSource = coll;
            txtEndIn.AutoCompleteCustomSource = coll;


        }

        //Item exists or not
        public bool ItemCheck()
        {

            string constr = "server=localhost;user id=root;persistsecurityinfo=True;database=madusha";
            MySqlConnection conn = new MySqlConnection(constr);
            MySqlCommand cmd = new MySqlCommand("Select Barcode from supermarket.item where name= @item OR name = @item1 ", conn);
            cmd.Parameters.AddWithValue("@item", this.txtSearch.Text);
            cmd.Parameters.AddWithValue("@item1", this.txtSearchItem.Text);


            conn.Open();

            int TotalRows = 0;
            TotalRows = Convert.ToInt32(cmd.ExecuteScalar());
            if (TotalRows > 0)
            {

                return true;
            }
            else
            {
                //need to change error message
                MessageBox.Show("Item doesnot exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnLoadChart_Click(object sender, EventArgs e)
        {
            LoadChart();
            LoadDateWiseTable();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangeChart();
        }

        private void linkLabel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (linkLabel1.Text == "View in Column Chart")
            {
                linkLabel1.Text = "View in Line chart";
            }
            else
            {
                linkLabel1.Text = "View in Column Chart";
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                ItemsAutoComplete();
            }
        }
        public void loadForm(Form form)
        {
           // panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            // panel4.Visible = false;
            panel5.Visible = false;
            
            label18.Visible = false;
            panel1.Dock = DockStyle.Fill;
            form.TopLevel = false;
            form.AutoScroll = true;
            panel4.Controls.Add(form);
            form.Show();
        }

        private void PosAdmin_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Back)
            
            {
                loadForm(new POS());
            }
        }

        private void btnLoadInvoice_Click(object sender, EventArgs e)
        {
            LoadChartInvoice();
            LoadInvoiceWiseTable();
        }

        private void txtStartIn_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                InvoiceAutoComplete();
            }
        }

        private void txtEndIn_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                InvoiceAutoComplete();
            }
        }

        private void txtPhone_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
            {
                SearchCustomer();
            }
        }

        //auto complete Item(barcodeWise)
        public void ItemsAutoCompleteItemName()
        {
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();


            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");


            string sqlquery = "SELECT Item_name from item where Item_name like '%" + txtItem.Text+"%'";
            MySqlCommand cmd = new MySqlCommand(sqlquery, conn);
            MySqlDataReader msReader;

            try
            {
                conn.Open();
                msReader = cmd.ExecuteReader(); ;

                while (msReader.Read())
                {
                    string sname = msReader.GetString(0);
                    coll.Add(sname);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

            txtItem.AutoCompleteCustomSource = coll;
            txtSearchItem.AutoCompleteCustomSource = coll;


        }

        //auto complete Item(ItemCodeWise)
        public void ItemsAutoCompleteItemCode()
        {
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();


            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");


            string sqlquery = "SELECT Item_code from item ";
            MySqlCommand cmd = new MySqlCommand(sqlquery, conn);
            MySqlDataReader msReader;

            try
            {
                conn.Open();
                msReader = cmd.ExecuteReader(); ;

                while (msReader.Read())
                {
                    string sname = msReader.GetString(0);
                    coll.Add(sname);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

            txtItem.AutoCompleteCustomSource = coll;
            txtSearchItem.AutoCompleteCustomSource = coll;

        }

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
           
        }

        private void radBarcode_CheckedChanged(object sender, EventArgs e)
        {
            txtItem.Text = "";
            txtPrice.Text = "";
            ItemsAutoCompleteItemName();
        }

      

        private void radItemCode_CheckedChanged(object sender, EventArgs e)
        {
            txtItem.Text = "";
            txtPrice.Text = "";
            ItemsAutoCompleteItemCode();
        }
        //ItemName exists or not
        public bool ItemExist()
        {

            string constr = "server=localhost;user id=root;persistsecurityinfo=True;database=supermarket";
            MySqlConnection conn = new MySqlConnection(constr);
            MySqlCommand cmd = new MySqlCommand("Select Item_code from supermarket.item where Item_name = '" + txtItem.Text + "'", conn);
            
            conn.Open();


            MySqlDataReader myReader;
            myReader = cmd.ExecuteReader(); ;

            if (myReader.HasRows)
            {

                return true;

            }

            else
            {
                MessageBox.Show("Item doesnot exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }




        }


        //ItemCode exists or not
        public bool ItemCodeExist()
        {

            string constr = "server=localhost;user id=root;persistsecurityinfo=True;database=supermarket";
            MySqlConnection conn = new MySqlConnection(constr);
            MySqlCommand cmd = new MySqlCommand("Select Item_code from supermarket.item where Item_code = '" + txtItem.Text + "'", conn);

            conn.Open();


            MySqlDataReader myReader;
            myReader = cmd.ExecuteReader(); ;

            if (myReader.HasRows)
            {

                return true;

            }

            else
            {
                MessageBox.Show("Item doesnot exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }


        //Discount Method
        public void DiscountItemWise()
        {
            try
            {
                string discount = txtDiscount.Text;

                for (int i = 0; i < discount.ToString().Length; i++)
                {
                    if ((txtDiscount.Text[i].ToString() == "%"))
                    {
                        txtDiscount.MaxLength = i;
                        string rate = discount.Substring(0, i).ToString();
                        float fRate = float.Parse(rate);

                        float IPrice = float.Parse(txtPrice.Text);
                        float NewPrice = IPrice - (IPrice * (fRate / 100));
                        txtPrice.Text = NewPrice.ToString();
                        

                        txtDiscount.MaxLength = i;
                        break;

                    }
                    if ((txtDiscount.Text[i].ToString() == "-"))
                    {
                        txtDiscount.MaxLength = i;
                        float disPrice = float.Parse(discount.Substring(0, i).ToString());

                        float initialPrice = float.Parse(txtPrice.Text);
                        float ApplyDis = initialPrice - disPrice;
                        txtPrice.Text = ApplyDis.ToString();
                       
                    }
                    else
                    {
                        txtDiscount.MaxLength = 5;


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Get Item Details
        public void getDetails()
        {
            try {
                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Brand,Item_name,Rprice,sqty,Warrenty,freeIssue,image FROM supermarket.item WHERE Item_name = '" + txtItem.Text + "' OR Item_code = '" + txtItem.Text + "' ";


                MySqlDataReader Dataread = cmd.ExecuteReader();
                Dataread.Read();


                if (Dataread.HasRows)
                {

                    
                    DlblBrand.Text = Dataread[0].ToString();
                    DlblDescription.Text = Dataread[1].ToString();
                    DlblPrice.Text = Dataread[2].ToString();
                    DlblStock.Text = Dataread[3].ToString();
                    DlblWarrenty.Text = Dataread[4].ToString();
                    DlblFreeIssue.Text = Dataread[5].ToString();


                    byte[] images = ((byte[])Dataread[6]);



                    if (images == null)
                    {
                        pictureBox1.Image = null;
                    }

                    else
                    {
                        MemoryStream mstream = new MemoryStream(images);
                        pictureBox1.Image = Image.FromStream(mstream);
                    }
                }

                else
                {
                    MessageBox.Show("This Data Not Available..!");

                }
                conn.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
       }


        //empty field validation
        public bool validateform()
        {
            if ((string.IsNullOrWhiteSpace(txtItem.Text)) || (string.IsNullOrWhiteSpace(txtPrice.Text)) || (string.IsNullOrWhiteSpace(txtQty.Text)))
            {
                MessageBox.Show("All fields Must be Filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;

            }
          
            else
            {
                return false;
            }
        }


        //empty field validation
        public bool validateform2()
        {
            if ((string.IsNullOrWhiteSpace(txtPhone.Text)) || (string.IsNullOrWhiteSpace(OlblName.Text)) || (orderCart.Rows.Count == 0))
            {
                MessageBox.Show("All fields Must be Filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;

            }
            else
            {
                return false;
            }
        }

        //Check Same Item in Bucket
        public bool CheckDuplicate()
        {


            //Boolean to check if he has row has been
            bool Found = false;
            if (orderCart.Rows.Count > 0)
            {

                //Check if the product Id exists with the same Price
                foreach (DataGridViewRow row in orderCart.Rows)
                {
                    if (Convert.ToString(row.Cells[0].Value) == DlblDescription.Text && Convert.ToString(row.Cells[1].Value) == txtPrice.Text)
                    {
                        //Update the Quantity of the found row
                        row.Cells[2].Value = Convert.ToString(Convert.ToInt16(txtQty.Text) + Convert.ToInt16(row.Cells[2].Value));
                        Found = true;
                    }

                }
                if (!Found)
                {
                    Found = false;
                    //Add the row to grid view
                    //cart.Rows.Add(txtDescription.Text, txtCode.Text,txtPrice.Text, 1,1000);
                }

            }
            else
            {

            }
            return Found;

        }

        //Clear Text Fields

        public void clearFields()
        {
            txtItem.Text = "";
            txtQty.Text = "";
            
            txtPrice.Text = "";
            txtDiscount.Text = "";
        }



        //Items Add to order Cart



        public void AddItemtoCart()
        {


            if ((validateform() == false))
            {
                if (CheckDuplicate() == false)
                {
                    int n = orderCart.Rows.Add();


                    orderCart.Rows[n].Cells[0].Value = DlblDescription.Text;
                    orderCart.Rows[n].Cells[1].Value = txtPrice.Text;
                    orderCart.Rows[n].Cells[2].Value = txtQty.Text;
                    float newPrice = float.Parse(txtPrice.Text);
                    float Original = float.Parse(DlblPrice.Text);
                    orderCart.Rows[n].Cells[3].Value = Original - newPrice;


                    String Sqty = txtQty.Text;
                    String Sprice = txtPrice.Text;


                    float qty = float.Parse(Sqty);
                    float tot = float.Parse(Sprice);

                    float amount = qty * tot;


                    txtQty.Text = "";
                    txtItem.Text = "";
                    txtPrice.Text = "";

                    txtDiscount.Text = "";


                    string strAmount = Convert.ToString(amount);
                    orderCart.Rows[n].Cells[4].Value = amount;

                }
                else
                {
                    clearFields();
                }
            }

        }
     
        //When Row Added Calculate the amount
        public void AddedAndCal()
        {
            try
            {
                if (validateform() == false)
                {
                    String Sqty = txtQty.Text;
                    String Sprice = txtPrice.Text;


                    double qty = double.Parse(Sqty);
                    double tot = double.Parse(Sprice);

                    double amount = qty * tot;
                    double sum = amount;

                    for (int i = 0; i < orderCart.Rows.Count; i++)
                    {
                        sum += Convert.ToDouble(orderCart.Rows[i].Cells[4].Value);

                    }



                    lblAmount.Text = sum.ToString();

                }
                //MessageBox.Show(sum.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //reduce item's Quantity

        public void ReduceItemQuantity()
        {
            try
            {


                if (this.orderCart.SelectedRows.Count > 0)
                {
                    

                   
                    orderCart.SelectedRows[0].Cells[2].Value = Convert.ToString(Convert.ToInt16(this.orderCart.SelectedRows[0].Cells[2].Value) - 1);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }


        //increase item's Quantity

        public void IncreaseItemQuantity()
        {
            try
            {


                if (this.orderCart.SelectedRows.Count > 0)
                {
                    
                    orderCart.SelectedRows[0].Cells[2].Value = Convert.ToString(Convert.ToInt16(this.orderCart.SelectedRows[0].Cells[2].Value) + 1);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        //Remove Item
        public void RemoveItem()
        {
            try
            {


                if (this.orderCart.SelectedRows.Count > 0)
                {
                    string value = orderCart.CurrentRow.Cells[4].Value.ToString();
                    double redValue = Double.Parse(value);

                    string total = lblAmount.Text;
                    double redTot = Double.Parse(total);

                    double realValue = redTot - redValue;
                    lblAmount.Text = realValue.ToString();

                    orderCart.Rows.RemoveAt(this.orderCart.SelectedRows[0].Index);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }



        public void SearchPriceItemcode()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");


            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = ("SELECT Rprice from supermarket.item where Item_code LIKE '%" + txtItem.Text + "%' ");
            MySqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {

                txtPrice.Text = r[0].ToString();
                txtPrice1.Text = r[0].ToString();



            }
        }

        public void SearchPriceItemName()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");


            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = ("SELECT Rprice from supermarket.item where Item_name LIKE '%" + txtItem.Text + "%' ");
            MySqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {

                txtPrice.Text = r[0].ToString();
                txtPrice1.Text = r[0].ToString();



            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if((radName.Checked==true)&&(ItemExist()==true))
            {
                SearchPriceItemName();
                if (!(string.IsNullOrWhiteSpace(txtQty.Text)))
                {
                   // MessageBox.Show("Yes");
                }

            }
            else if ((radItemCode.Checked == true) && ItemCodeExist() == true)
            {
                SearchPriceItemcode();
                if (!(string.IsNullOrWhiteSpace(txtQty.Text)))
                {
                    //MessageBox.Show("YesQ");
                }
            }
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
            {
                if ((radName.Checked == true) && (ItemExist() == true))
                {
                    getDetails();
                    SearchPriceItemName();
                    
                    txtQty.Text = "1";



                }
                else if ((radItemCode.Checked == true) && ItemCodeExist() == true)
                {
                    
                    getDetails();
                    SearchPriceItemcode();
                    txtQty.Text = "1";

                }
            }
            if(e.KeyCode == Keys.Back)
            {
                txtPrice.Text = "";
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            DiscountItemWise();
        }

        private void txtDiscount_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                //txtPrice.Text = "";
                txtDiscount.Text = "";
                if (radItemCode.Checked == true)
                {
                    SearchPriceItemcode();
                }
                else if (radName.Checked == true)
                {
                    SearchPriceItemName();
                }
            }
        }

        //**Important :: When qty cahnged sub total will be changed
        private void orderCart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            double sum = 0;
            if(e.ColumnIndex == 2)
            {
                try
                {

                    for (int i = 0; i < orderCart.Rows.Count; i++)
                    {
                        //Total Amount Label will be updated
                        double total = Convert.ToDouble(orderCart.Rows[i].Cells[1].Value) * Convert.ToDouble(orderCart.Rows[i].Cells[2].Value);
                        orderCart.Rows[i].Cells[4].Value = total;
                        sum += Convert.ToDouble(orderCart.Rows[i].Cells[4].Value);

                    }

                    lblAmount.Text = sum.ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
 
        //old Value Bill Calc

        public void CalPrice()
        {
            try
            {
                double sum = 0;
                for (int i = 0; i < orderCart.Rows.Count; i++)
                {

                    sum += double.Parse(orderCart.Rows[i].Cells[4].Value.ToString());
                    lblAmount.Text = sum.ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void orderCart_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.OemMinus)
                {
                    if (orderCart.Rows.Count != 0)
                    {
                        if (Convert.ToInt16(orderCart.SelectedRows[0].Cells[2].Value.ToString()) > 1)
                        {
                            ReduceItemQuantity();



                        }
                        else
                        {
                            RemoveItem();

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (e.KeyCode == Keys.Oemplus)
            {
                if (orderCart.Rows.Count != 0)
                {
                    if (Convert.ToInt16(orderCart.SelectedRows[0].Cells[2].Value.ToString()) > 0)
                    {
                        IncreaseItemQuantity();
                    }
                }

            }
        }

        private void orderCart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'd' - 96)

            {

                RemoveItem();
            }
        }



        //Auto Increment invoiceID
        public void count_accout()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");


                string query = "select ID from supermarket.orders order by ID";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                {
                    while (dr.Read())
                        lblOrder.Text = dr["ID"].ToString();
                }

                int i = int.Parse(lblOrder.Text);
                i = i + 1;
                lblOrder.Text = i.ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if(validateform2()==false && StockChecker()==false)
            { 
            SendOrder();
            LoadOrderTable();
            DeleteItem();
            }

        }
        //Getters Setters
        public string OrderID
        { get; set; }
        public string ItemCode
        { get; set; }
        public string ItemName
        { get; set; }
        public string Price
        { get; set; }
        public string Quantity
        { get; set; }
        public string Discount
        { get; set; }
        public string CustomerName
        { get; set; }
        public string ToBuy
        { get; set; }
        
        public string Address
        { get; set; }
        public string subAmount
        { get; set; }

        public string TotalAmount
        { get; set; }

        public string Mobile
        { get; set; }

        public string UnitPrice
        { get; set; }

        public void SendOrder()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                conn.Open();

                for (int i = 0; i < orderCart.Rows.Count; i++)
                {





                    MySqlCommand cmd2 = conn.CreateCommand();
                    

                    OrderID = this.lblINV.Text + this.lblOrder.Text;
                    CustomerName = this.OlblName.Text;
                    Mobile = this.OlblPhone.Text;
                    ToBuy = this.cmbToBuy.SelectedItem.ToString();
                    TotalAmount = this.lblAmount.Text;
                    Address = this.OlblAddressLine1.Text + " ," + OlblAddressLine2.Text + " ," + OlblCity.Text;

                    //ItemCode = this.orderCart.Rows[i].Cells[1].Value.ToString();
                    ItemName = this.orderCart.Rows[i].Cells[0].Value.ToString();
                    UnitPrice = this.orderCart.Rows[i].Cells[1].Value.ToString();
                    Quantity = this.orderCart.Rows[i].Cells[2].Value.ToString();
                    Discount = this.orderCart.Rows[i].Cells[3].Value.ToString();
                    subAmount = this.orderCart.Rows[i].Cells[4].Value.ToString();

                    cmd2 = new MySqlCommand(@"INSERT INTO supermarket.orderdetails(OrderID,ItemName,Unitprice,Quantity,Discount,SubTotal) VALUES ('" + OrderID + "','" + ItemName + "','" + UnitPrice + "','" + Quantity + "','" + Discount + "','" + subAmount + "')", conn);

                    cmd2.ExecuteNonQuery();
               }

                MySqlCommand cmd1 = conn.CreateCommand();
                cmd1 = new MySqlCommand("INSERT INTO supermarket.orders(OrderID,Customer,Phone,ToBuy,TotalAmount,orderDate,Address) VALUES ('" + OrderID + "','" + CustomerName + "','" + Mobile + "','" + ToBuy + "','" + TotalAmount + "','"+TimeTest.Text+"','" + Address + "')", conn);
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Order Done Succesfully", "Madusha Super Market", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        //Stock Check
        public bool StockChecker()
        {

            bool result = false;
            string constr = "server=localhost;user id=root;persistsecurityinfo=True;database=supermarket";
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            for (int i = 0; i < orderCart.Rows.Count; i++)
            {

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM supermarket.item WHERE Item_name = '" + this.orderCart.Rows[i].Cells[0].Value.ToString() + "' AND sqty >= '" + Convert.ToInt32(this.orderCart.Rows[i].Cells[2].Value.ToString()) + "'  ", conn);
                MySqlDataReader dr = cmd.ExecuteReader();


                if (dr.HasRows)
                {

                    result = false;
                    dr.Close();

                }

                else
                {

                    MessageBox.Show("Please check the Stock !! ", "Madusha SuperMarket", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    result = true;
                    break;
                    dr.Close();

                }

            }


            return result;

        }

        public void DeleteItem()
        {
            try
            {
         
                    orderCart.Rows.Clear();
         
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        //LoadItemTable
        public void LoadOrderTable()
        {

            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT OrderID,Customer,Phone,ToBuy,TotalAmount,orderDate from supermarket.orders", conn);

            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                adapter.Fill(ds, "order");
                OrderDetails.DataSource = ds.Tables["order"];
                OrderDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        //Get Order Details
        public void LoadOrderDetails()
        {
            

            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT od.ItemName,od.UnitPrice,od.Quantity,od.Discount, od.SubTotal FROM supermarket.orders O, supermarket.orderdetails od    where od.OrderID = o.OrderID AND o.OrderID ='"+OrderDetails.SelectedRows[0].Cells[0].Value.ToString()+"' ", conn);
            try
            {
             

            }catch (Exception ex)
            {

            }
            try
            {
                orderCart.Columns.Clear();
                conn.Open();
                DataSet ds = new DataSet();
                adapter.Fill(ds, "orderDetails");
                orderCart.DataSource = ds.Tables["orderDetails"];
                orderCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void OrderDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            LoadOrderDetails();
            orderCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CalPrice();

        }

        private void orderCart_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = orderCart.CurrentCell.ColumnIndex;
            MessageBox.Show(columnIndex.ToString());

        }

        private void btnAdd_Click_2(object sender, EventArgs e)
        {
             try
            {

                if(orderCart.Rows.Count>0)
                {
                    orderCart.DataSource = null;
                }

                if (orderCart.ColumnCount == 5)
                {
                  
                    AddItemtoCart();
                    orderCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                if (orderCart.ColumnCount == 0)
                {
                    orderCart.Columns.Add("ItemName", "ItemName");
                    orderCart.Columns.Add("UnitPrice", "UnitPrice");
                    orderCart.Columns.Add("Quantity", "Quantity");
                    orderCart.Columns.Add("Discount", "Discount");
                    orderCart.Columns.Add("SubTotal", "SubTotal");
                    orderCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    AddItemtoCart();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void orderCart_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
           // CalPrice();
        }

        public void UpdateOrder()
        {
            string method = OrderDetails.SelectedRows[0].Cells[3].Value.ToString();
            string OrderNum = OrderDetails.SelectedRows[0].Cells[0].Value.ToString();
            MySqlConnection conn = new MySqlConnection("server = localhost; user id = root; persistsecurityinfo = True; database = supermarket");
            conn.Open();
            if (method == "Reserve")
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE supermarket.orders  SET ToBuy='Home Delivery' Where OrderID ='" + OrderNum + "'", conn);



                cmd.ExecuteNonQuery();
            }
            if (method == "Home Delivery")
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE supermarket.orders  SET ToBuy='Reserve' Where OrderID ='" + OrderNum + "'", conn);



                cmd.ExecuteNonQuery();
            }


            conn.Close();
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            UpdateOrder();
            LoadOrderTable();
        }

        private void radIteam1_CheckedChanged(object sender, EventArgs e)
        {
            txtItem.Text = "";
            txtPrice.Text = "";
            ItemsAutoCompleteItemName();
        }

        private void radItemName1_CheckedChanged(object sender, EventArgs e)
        {
            txtItem.Text = "";
            txtPrice.Text = "";
            ItemsAutoCompleteItemCode();
        }

        private void txtSearchItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if ((radIteam1.Checked == true) )
                {

                    SearchPriceItemName();
                    //getDetails();
                    txtQty1.Text = "1";

                }
                
            }
            if (e.KeyCode == Keys.Back)
            {
                txtPrice.Text = "";
            }
        }

        private void txtDiscount1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string discount1 = txtDiscount1.Text;

                for (int i = 0; i < discount1.ToString().Length; i++)
                {
                    if ((txtDiscount1.Text[i].ToString() == "%"))
                    {
                        txtDiscount1.MaxLength = i;
                        string rate = discount1.Substring(0, i).ToString();
                        float fRate = float.Parse(rate);

                        float IPrice = float.Parse(txtPrice1.Text);
                        float NewPrice = IPrice - (IPrice * (fRate / 100));
                        txtPrice1.Text = NewPrice.ToString();


                        txtDiscount1.MaxLength = i;
                        break;

                    }
                    if ((txtDiscount1.Text[i].ToString() == "-"))
                    {
                        txtDiscount1.MaxLength = i;
                        float disPrice = float.Parse(discount1.Substring(0, i).ToString());

                        float initialPrice = float.Parse(txtPrice1.Text);
                        float ApplyDis = initialPrice - disPrice;
                        txtPrice1.Text = ApplyDis.ToString();

                    }
                    else
                    {
                        txtDiscount1.MaxLength = 5;


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void AddItemtoCartQuotation()
        {


                if (CheckDuplicate() == false)
                {
                    int n = cartQuotation.Rows.Add();

                    cartQuotation.Rows[n].Cells[0].Value = txtSearchItem.Text;
                    cartQuotation.Rows[n].Cells[1].Value = txtPrice1.Text;
                    cartQuotation.Rows[n].Cells[2].Value = txtQty1.Text;
    
                    String Sqty = txtQty1.Text;
                    String Sprice = txtPrice1.Text;

                    float qty = float.Parse(Sqty);
                    float tot = float.Parse(Sprice);

                    float amount = qty * tot;

                    txtQty1.Text = "";
                    txtPrice1.Text = "";
                    txtDiscount1.Text = "";
                    txtSearchItem.Text = "";
                    string strAmount = Convert.ToString(amount);
                    cartQuotation.Rows[n].Cells[3].Value = amount;

                }
                else
                {
                    clearFields();
                }
   
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            checker();

        }

        public bool checker()
        {
            if ((string.IsNullOrWhiteSpace(txtSearchItem.Text)) || (string.IsNullOrWhiteSpace(txtPrice1.Text)) || (string.IsNullOrWhiteSpace(txtQty1.Text)))
            {
                MessageBox.Show("All fields Must be Filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;

            }

            else
            {
                AddItemtoCartQuotation();
                return false;
            }
        }

        private void cartQuotation_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
           {
               
                    String Sqty = txtQty1.Text;
                    String Sprice = txtPrice1.Text;


                    double qty = double.Parse(Sqty);
                    double tot = double.Parse(Sprice);

                    double amount = qty * tot;
                    double sum = amount;

                    for (int i = 0; i < orderCart.Rows.Count; i++)
                    {
                        sum =+ Convert.ToDouble(orderCart.Rows[i].Cells[3].Value);

                    }



                lblAmount1.Text = sum.ToString();

                
                //MessageBox.Show(sum.ToString());
            }
            catch (Exception ex)
            {
                
            }
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                conn.Open();

                MySqlCommand cmd2 = conn.CreateCommand();


                string DeleteID = OrderDetails.SelectedRows[0].Cells[0].Value.ToString();

                cmd2 = new MySqlCommand("Delete from supermarket.orders where OrderID = '" + DeleteID + "'", conn);

                cmd2.ExecuteNonQuery();
                
                LoadOrderTable();
               orderCart.Rows.Clear();


            }
            catch(Exception ex)
            {
                
            }
        }
    }
    }
  

        //Update  order
  
    

