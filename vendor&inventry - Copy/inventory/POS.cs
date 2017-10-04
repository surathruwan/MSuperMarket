using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing.Printing;
using System.IO;
namespace inventory
{
    public partial class POS : Form
    {
        private StreamReader streamToPrint;
         MainForm ourMain = new MainForm();
        public POS()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

        }


        public void loadForm(Form form)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
           
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            label18.Visible = false;
            panel4.Dock = DockStyle.Fill;
            form.TopLevel = false;
            form.AutoScroll = true;
            panel4.Controls.Add(form);
            form.Show();
        }

        private void POS_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'supermarketDataSet.item' table. You can move, or remove it, as needed.
            //this.itemTableAdapter.Fill(this.supermarketDataSet.item);
            LoadDate();

            count_accout();
            timer1.Start();



        }

        private void btnCustomerQuotation_Click(object sender, EventArgs e)
        {
           

        }


      



        private void txtDescription_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                ItemsAutoComplete();
            }

        }



        //Methods

        
        


        //auto complete text boxes
        void ItemsAutoComplete()
        {
            txtDescription.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtDescription.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();


            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=madusha");


            string sqlquery = "SELECT Item_name from item ";
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

            txtDescription.AutoCompleteCustomSource = coll;

        }


        //auto complete text boxes using Barcode
        void ItemBarcodeAutoComplete()
        {
            txtBarcode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtBarcode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();


            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=madusha");


            string sqlquery = "SELECT Barcode from item ";
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

            txtBarcode.AutoCompleteCustomSource = coll;
            
        }

        //auto complete text boxes
        void ItemsCodeAutoComplete()
        {
            txtCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();


            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=madusha");


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

            txtCode.AutoCompleteCustomSource = coll;

        }

        //Auto Increment invoiceID
        public void count_accout()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                

                string query = "select ID from supermarket.invoicerecords order by ID";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                {
                    while (dr.Read())
                        lblInvoice.Text = dr["ID"].ToString();
                }

                int i = int.Parse(lblInvoice.Text);
                i = i + 1;
                lblInvoice.Text = i.ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //empty field validation
        public bool validateform()
        {
            if ((string.IsNullOrWhiteSpace(txtBarcode.Text)) || (string.IsNullOrWhiteSpace(txtCode.Text)) || (string.IsNullOrWhiteSpace(txtPrice.Text)) || (string.IsNullOrWhiteSpace(txtQty.Text)))
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
            if (cart.Rows.Count > 0)
            {

                //Check if the product Id exists with the same Price
                foreach (DataGridViewRow row in cart.Rows)
                {
                    if (Convert.ToString(row.Cells[0].Value) == txtDescription.Text && Convert.ToString(row.Cells[2].Value) == txtPrice.Text)
                    {
                        //Update the Quantity of the found row
                        row.Cells[3].Value = Convert.ToString(Convert.ToInt16(txtQty.Text) + Convert.ToInt16(row.Cells[3].Value));
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
            txtBarcode.Text = "";
            txtQty.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
            txtCode.Text = "";
            txtDiscount.Text = "";
        }
        //Items Add to bucket Shortcut

        public void AddItemtoCart()
        {


            if (barcodeCheck() &&(validateform() == false))
            {
                if (CheckDuplicate() == false)
                {
                    int n = cart.Rows.Add();


                    cart.Rows[n].Cells[0].Value = txtDescription.Text;
                    cart.Rows[n].Cells[1].Value = txtCode.Text;
                    cart.Rows[n].Cells[2].Value = txtPrice.Text;
                    cart.Rows[n].Cells[3].Value = txtQty.Text;


                    String Sqty = txtQty.Text;
                    String Sprice = txtPrice.Text;


                    float qty = float.Parse(Sqty);
                    float tot = float.Parse(Sprice);

                    float amount = qty * tot;

                    txtBarcode.Text = "";
                    txtQty.Text = "";
                    txtDescription.Text ="";
                    txtPrice.Text = "";
                    txtCode.Text = "";
                    txtDiscount.Text = "";
                    

                    string strAmount = Convert.ToString(amount);
                    cart.Rows[n].Cells[4].Value = amount;
                    
                }
                else
                {
                    clearFields();
                }
            }

        }
        //barcode exists or not
        public bool barcodeCheck()
        {

            string constr = "server=localhost;user id=root;persistsecurityinfo=True;database=supermarket";
            MySqlConnection conn = new MySqlConnection(constr);
            MySqlCommand cmd = new MySqlCommand("Select Barcode from supermarket.item where Barcode= @barcode AND Item_name =@description", conn);
            cmd.Parameters.AddWithValue("@barcode", this.txtBarcode.Text);
            cmd.Parameters.AddWithValue("@description", this.txtDescription.Text);
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
       

        //Search item via Barcode

        public void SearchBarcode()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

           MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT item_code,Barcode,item_name,Warrenty,freeIssue,sqty,Rprice  from supermarket.item where Barcode LIKE '" + txtBarcode.Text + "%' ", conn);
           
            conn.Open();
            DataTable catetable = new DataTable();
            adapter.Fill(catetable);



            BindingSource source = new BindingSource();
            source.DataSource = catetable;

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = ("SELECT item_name,Item_code,Rprice,item_name from supermarket.item where Barcode LIKE '%" + txtBarcode.Text + "%' ");
            MySqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                txtCode.Text = r[1].ToString();
                txtPrice.Text = r[2].ToString();

                txtQty.Text = "1";
                txtDescription.Text = r[0].ToString();
                detailsGrid.AllowUserToAddRows = false;
                detailsGrid.DataSource = source;
            }
        }

        //Search only via Barcode
        public void SearchPriceBarcode()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

           
            conn.Open();
            
            MySqlCommand cmd = conn.CreateCommand();
            
            cmd.CommandText = ("SELECT Rprice from supermarket.item where Barcode LIKE '%" + txtBarcode.Text + "%' ");
            MySqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {

                txtPrice.Text = r[0].ToString();

               
                
            }
        }

        // Search Customer
        public void SearchCustomer()
        {
            try {
                string constr = "server=localhost;user id=root;persistsecurityinfo=True;database=supermarket";
                MySqlConnection conn = new MySqlConnection(constr);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Surname,initials from supermarket.loyaltycustomer where Mobile LIKE '" + bunifuMaterialTextbox1.Text + "%' ";


                MySqlDataReader Dataread = cmd.ExecuteReader();
                Dataread.Read();


                if (Dataread.HasRows)
                {
                    lblName.Text = Dataread[1].ToString() + " " + Dataread[0].ToString();
                }
                else
                {
                    MessageBox.Show("Customer Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }


        //Row added to the Cart Table and Calculate the Total Amount

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

                    for (int i = 0; i < cart.Rows.Count; i++)
                    {
                        sum += Convert.ToDouble(cart.Rows[i].Cells[4].Value);

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


        //Calculate Balance
        public void BalanceCal()
        {
            try
            {

                double cash = Double.Parse(txtCash.Text);
                double total = Double.Parse(lblAmount.Text);

                double balance = cash - total;
                if (balance >= 0)
                {
                    lblBalanceAmount1.Text = balance.ToString("0.00");
                }
                else
                {
                    lblBalanceAmount1.Text = "0.00";
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }
        }


        //Validation
        public bool Regexp(string re, Bunifu.Framework.UI.BunifuMaterialTextbox tb, PictureBox pc, string s)
        {
            ToolTip buttonToolTip = new ToolTip();
            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {

                pc.Image = Properties.Resources.invalid;
                n.SetToolTip(pc, s);
                return false;

            }
            else
            {
                pc.Image = null;
                return true;

            }

        }
        //Windows Text box validate
        public bool RegexpEmpty(string re, TextBox tb, PictureBox pc, string s)
        {
            ToolTip buttonToolTip = new ToolTip();
            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {

                pc.Image = Properties.Resources.invalid;
                n.SetToolTip(pc, s);
                return false;

            }
            else
            {
                pc.Image = null;
                return true;

            }

        }

        //phone Number Length validate

        public bool ValidatePhoneNumber(string mobile)
        {
            //Accepts only 10 digits,
            Regex pattern = new Regex(@"(?<!\d)\d{10}(?!\d)");

            if (pattern.IsMatch(mobile))
            {

                return true;
            }
            else
            {

                return false;
            }
        }


        //Getters Setters
        public string Barcode
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
        public string Cash
        { get; set; }
        public string Balance
        { get; set; }
        public string OrderID
        { get; set; }
        public string subAmount
        { get; set; }

        public string TotalAmount
        { get; set; }

        //Send Data to Order Details

        public void SendData()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                conn.Open();
                for (int i = 0; i < cart.Rows.Count; i++)
                {
                    

                    

                    
                    MySqlCommand cmd2 = conn.CreateCommand();

                    Barcode = this.txtBarcode.Text;
                    ItemCode = this.cart.Rows[i].Cells[1].Value.ToString();
                    ItemName = this.cart.Rows[i].Cells[0].Value.ToString();
                    Quantity = this.cart.Rows[i].Cells[3].Value.ToString();
                    Price = this.cart.Rows[i].Cells[2].Value.ToString();
                    subAmount = this.cart.Rows[i].Cells[4].Value.ToString(); ;
                    Discount = this.txtDiscount.Text;
                    Cash = this.txtCash.Text;
                    CustomerName = this.lblName.Text;
                    OrderID = this.lblINV.Text + this.lblInvoice.Text;
                    Balance = this.txtBalance.Text;
                    TotalAmount = this.lblAmount.Text;
                    string Datestr = DateTime.Now.ToString("yyyy-MM-dd");

                    

                    cmd2 = new MySqlCommand(@"INSERT INTO supermarket.recordsellingdetails(InvoiceID,ItemName,Qty,unitprice,SubTotal,date) VALUES ('" + OrderID + "','" + ItemName + "','" + Quantity + "','" + Price + "','" + subAmount + "','" + Datestr + "')", conn);
                    
                    cmd2.ExecuteNonQuery();

                }
                MySqlCommand cmd1 = conn.CreateCommand();
                cmd1 = new MySqlCommand(@"INSERT INTO supermarket.invoicerecords(InvoiceID,Customer,Total) VALUES ('" + OrderID + "','" + CustomerName + "','"+ TotalAmount +"')", conn);
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Order Done Succesfully", "Madusha Super Market", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        //decrement qty Value
        public bool DecrementQtyValue()
        {


            //Boolean to check if he has row has been
            bool Found = false;
            if (cart.Rows.Count > 0)
            {

                //Check if the product Id exists with the same Price
                foreach (DataGridViewRow row in cart.Rows)
                {
                    if (Convert.ToString(row.Cells[0].Value) == txtDescription.Text && Convert.ToString(row.Cells[2].Value) == txtPrice.Text)
                    {
                        //Update the Quantity of the found row
                        row.Cells[3].Value = Convert.ToString( Convert.ToInt16(row.Cells[3].Value)-1);
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
                //Add the row to grid view for the first time
                //cart.Rows.Add(txtDescription.Text, txtCode.Text, txtPrice.Text, 1, 1000);
                //cart.Rows.Add(textBox_ProductId.Text, textBox_Price.Text, 1);
            }
            return Found;

        }
        //reduce item's Quantity

        public void ReduceItemQuantity()
        {
            try
            {


                if (this.cart.SelectedRows.Count > 0)
                {
                    
                    cart.SelectedRows[0].Cells[3].Value = Convert.ToString(Convert.ToInt16(this.cart.SelectedRows[0].Cells[3].Value) - 1);
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


                if (this.cart.SelectedRows.Count > 0)
                {
                    
                    cart.SelectedRows[0].Cells[3].Value = Convert.ToString(Convert.ToInt16(this.cart.SelectedRows[0].Cells[3].Value) + 1);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        //Remove the Items of the bucket


        public void RemoveItem()
        {
            try
            {


                if (this.cart.SelectedRows.Count > 0)
                {
                    string value = cart.CurrentRow.Cells[4].Value.ToString();
                    double redValue = Double.Parse(value);

                    string total = lblAmount.Text;
                    double redTot = Double.Parse(total);

                    double realValue = redTot - redValue;
                    lblAmount.Text = realValue.ToString();

                    cart.Rows.RemoveAt(this.cart.SelectedRows[0].Index);
                    
                }

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
                for (int i = 0; i < cart.Rows.Count; i++)
                {

                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM supermarket.item WHERE Item_code = '" + this.cart.Rows[i].Cells[1].Value.ToString() + "' AND sqty >= '" + Convert.ToInt32(this.cart.Rows[i].Cells[3].Value.ToString()) + "'  ", conn);
                    MySqlDataReader dr = cmd.ExecuteReader();


                    if (dr.HasRows)
                    {

                        result = false;
                        dr.Close();

                }

                    else
                    {

                        MessageBox.Show("Not Sufficient to process order Right now !! ", "Madusha SuperMarket", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        result = true;
                    break;
                    dr.Close();
                    
                    }

                }

           
                return result;
           
        }
            

        //Stock Update
        public void StockUpdate()
        {
            
            string constr = "server=localhost;user id=root;persistsecurityinfo=True;database=madusha";
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            for (int i = 0; i < cart.Rows.Count; i++)
            {
               
           
                MySqlCommand cmd = new MySqlCommand("Update madusha.item  set sqty= sqty - @qty   where Item_code= @itemcode ", conn);
                
                cmd.Parameters.AddWithValue("@itemcode", this.cart.Rows[i].Cells[1].Value.ToString());
                cmd.Parameters.AddWithValue("@qty", this.cart.Rows[i].Cells[3].Value.ToString());
                cmd.ExecuteNonQuery();



                
            }
            //MessageBox.Show("Update Sucessfully");


        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back) || char.IsLetter(e.KeyChar);
            if (e.KeyChar == 13)
            {
                AddItemtoCart();
            }
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !( e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));

            //if (e.KeyChar == 13)
            //{
            //    AddItemtoCart();
            //}



        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            if ((txtBarcode.Text.Length >= 13)||(txtBarcode.Text.Length>=15))
            {
                
                SearchBarcode();

            }
            if((string.IsNullOrWhiteSpace(txtBarcode.Text)))
            {
                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT item_code,Barcode,item_name,Warrenty,freeIssue,sqty,Rprice  from supermarket.item where Barcode LIKE '" + txtBarcode.Text + "%' ", conn);

                conn.Open();
                DataTable catetable = new DataTable();
                adapter.Fill(catetable);



                BindingSource source = new BindingSource();
                source.DataSource = catetable;
                conn.Close();
            }
          
        }

        private void txtCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                ItemsCodeAutoComplete();
            }
        }

        private void cart_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            AddedAndCal();


        }





        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            BalanceCal();
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !( e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Space);
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back) ;
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
          //  e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back) || char.IsLetter(e.KeyChar);
        }

        private void bunifuMaterialTextbox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           


            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);

            if (bunifuMaterialTextbox1.Text.Length == 10)
            {
                if (e.KeyChar == 13)
                {


                    Regexp(@"(?<!\d)\d{10}(?!\d)", bunifuMaterialTextbox1, pictureBox1, "Please Enter 10 digits Phone Number");

                }
                SearchCustomer();
            }
        }

        //Data Send to the Database or Complete the order
        private void cart_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
               
                try
                {
                    if (StockChecker() == false)
                    {
                        
                        StockUpdate();
                        SendData();
                        cart.Rows.Clear();                    }
                
                else
                {
                    
                }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void cart_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 'd' - 96)
          
            {
                
                RemoveItem();
            }
        }

        private void txtBarcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                cart.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                txtBarcode.Text = "";
                clearFields();
                LoadDate();
            }

            else if (e.KeyCode == Keys.Insert)
            {
                AddItemtoCart();
                LoadDate();


            }

            else if (e.KeyCode == Keys.F3)
            {
                txtDiscount.Focus();
            }





        }
        //**Important :: When qty cahnged sub total will be changed
        private void cart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            double sum=0;

            try
            {
                
                for (int i = 0; i < cart.Rows.Count; i++)
                {    
                  

                 //Total Amount Label will be updated
                 double total = Convert.ToDouble(cart.Rows[i].Cells[2].Value) * Convert.ToDouble(cart.Rows[i].Cells[3].Value);
                 cart.Rows[i].Cells[4].Value = total;
                 sum += Convert.ToDouble(cart.Rows[i].Cells[4].Value);
                 }

                lblAmount.Text = sum.ToString();

               




                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //+ key incremet items and - mark decrement that item you selected
        private void cart_KeyDown(object sender, KeyEventArgs e)
        {
           try {
                if (e.KeyCode == Keys.OemMinus)
                {
                    if (cart.Rows.Count != 0)
                    {
                        if (Convert.ToInt16(cart.SelectedRows[0].Cells[3].Value.ToString()) > 1)
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
                if (cart.Rows.Count != 0)
                {
                    if (Convert.ToInt16(cart.SelectedRows[0].Cells[3].Value.ToString()) > 0)
                    {
                        IncreaseItemQuantity();
                    }
                }

            }

           
        }

        private void bunifuTileButton6_Click(object sender, EventArgs e)
        {
            var installedPrinters = PrinterSettings.InstalledPrinters; //I have choosed a printername from 'installedPrinters'
            try {
                
                
                try
                {
                    
                    int height = (cart.RowCount) * 10 + 50;
                    MadushaPrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Bill", 76, height);
                    MadushaPrintDocument.PrinterSettings.PrinterName = "EPSON UB-U03II"; //Specify the printer to use.

                    MadushaPrintDocument.PrintPage += new PrintPageEventHandler(this.MadushaPrintDocument_PrintPage);
                    MadushaPrintDocument.Print();
                   
                    
                    
                }
            finally
            {
               
                   // MessageBox.Show("data Exported");
                }

        }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                    int height = (cart.RowCount) * 10 + 50;
                    MadushaPrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Bill", 76, height);
                    MadushaPrintDocument.PrinterSettings.PrinterName = "Canon iP2800 series"; //Specify the printer to use.

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


        //Bill Generate and print
        private void MadushaPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
         

            DateTime time = DateTime.Now;
            string formatD = "yyyy-MM-dd";
            
            string SysTime = TimeTest.Text;
            

            e.Graphics.DrawString("MADUSHA", new System.Drawing.Font("Century", 12,System.Drawing.FontStyle.Bold),System.Drawing.Brushes.Black,new System.Drawing.Point(70,0)); // x,y
            e.Graphics.DrawString("SUPER MARKET", new System.Drawing.Font("Century", 12, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Black, new System.Drawing.Point(44, 20));
            e.Graphics.DrawString("No.181 , Galle Road", new System.Drawing.Font("Century", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(50, 40));
            e.Graphics.DrawString("Hikkaduwa", new System.Drawing.Font("Century", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(77, 55));
            e.Graphics.DrawString("Tel : 091 2277939 / 091 4946386", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(32, 75));
            e.Graphics.DrawString("Date : "+ time.ToString(formatD), new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 100));
            e.Graphics.DrawString("Time : " + SysTime, new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(130, 100));
            e.Graphics.DrawString("Invoice No : " + lblINV.Text+lblInvoice.Text, new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 115));
            e.Graphics.DrawString("Terminal : " + "001", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(130, 115));
            e.Graphics.DrawString("Cashier :" + "Surath", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 130));
            e.Graphics.DrawString("SalesRep : " + "Ruwan", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(130, 130));
            e.Graphics.DrawString("Customer : " + "Ruchira", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 145));
            e.Graphics.DrawString("-----------------------------------------------------------", new System.Drawing.Font("Century", 8, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 155));

            int x = 165;
            int x1 = 180;
            for (int i = 0; i < cart.Rows.Count; i++)
            {
                e.Graphics.DrawString(cart.Rows[i].Cells[0].Value.ToString(), new System.Drawing.Font("Times New Roman", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, x));
                x = x + 30;
                if ((Convert.ToInt32(cart.Rows[i].Cells[2].Value.ToString().Length) == 5))
                {

                    if ((Convert.ToInt32(cart.Rows[i].Cells[4].Value.ToString().Length) >=5))
                    {
                        e.Graphics.DrawString(cart.Rows[i].Cells[1].Value.ToString() + "\t" + cart.Rows[i].Cells[3].Value.ToString() + "\t  " + cart.Rows[i].Cells[2].Value.ToString() + "\t  " + cart.Rows[i].Cells[4].Value.ToString(), new System.Drawing.Font("Times New Roman", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, x1));
                    }
                }
                else if ((Convert.ToInt32(cart.Rows[i].Cells[2].Value.ToString().Length) == 4))
                {

                    if ((Convert.ToInt32(cart.Rows[i].Cells[4].Value.ToString().Length) >= 4))
                    {
                        e.Graphics.DrawString(cart.Rows[i].Cells[1].Value.ToString() + "\t" + cart.Rows[i].Cells[3].Value.ToString() + "\t    " + cart.Rows[i].Cells[2].Value.ToString() + "\t  " + cart.Rows[i].Cells[4].Value.ToString(), new System.Drawing.Font("Times New Roman", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, x1));
                    }
                }
                else
                {
                    e.Graphics.DrawString(cart.Rows[i].Cells[1].Value.ToString() + "\t" + cart.Rows[i].Cells[3].Value.ToString() + "\t" + cart.Rows[i].Cells[2].Value.ToString() + "\t" + cart.Rows[i].Cells[4].Value.ToString(), new System.Drawing.Font("Times New Roman", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, x1));
                }
                x1 = x1 + 30;

            }

        }

        private void btnSettlePayement_Click(object sender, EventArgs e)
        {
            
            MadushaPrintPreviewDialog.Document = MadushaPrintDocument;
            int height = (cart.RowCount) * 10 +50 ;
            MadushaPrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Bill", 76, height);
            MadushaPrintDocument.PrinterSettings.PrinterName = "Microsoft Print to PDF"; //Specify the printer to use.
            MadushaPrintDocument.PrintPage += new PrintPageEventHandler(this.MadushaPrintDocument_PrintPage);
            MadushaPrintPreviewDialog.ShowDialog();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeTest.Text= DateTime.Now.ToLongTimeString();
            
        }
        //Discount Method
        public void DiscountItemWise()
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
        
        //Apply Discount for Item (Precentage Wise and decimal Wise)
        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DiscountItemWise();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtDiscount_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Back)
            {
                txtDiscount.Text = "";
                SearchPriceBarcode();

            }
            else if (e.KeyCode == Keys.F3)
            {
                txtBarcode.Focus();
            }
        }

        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {
            loadForm(new PosAdmin());
        }

       

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem.ToString() == "Print Receipt			F5")
            {
                printer();
                //MessageBox.Show("Hi");
            }

            else if (listBox1.SelectedItem.ToString() == "More Option			F6")
            {
                loadForm(new PosAdmin());



            }
        }
        //LoadItemTable
        public void LoadDate()
        {
           
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * from item", conn);

            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                adapter.Fill(ds, "items");
                detailsGrid.DataSource = ds.Tables["items"];
                detailsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TimeTest_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1)
            //{
            //    SearchCustomer();
            //}
        }
    }
    }

