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

         
            int v = Session.getUser();
            if (v == 0)
            {


            }
            else if (v == 1)
            {
                

            }
            else if (v == 2)
            {
                

            }

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
            LoadDate();
            count_accout();
            timer1.Start();
        }

       
        private void txtDescription_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                ItemsAutoComplete();
            }
            if (e.KeyCode == Keys.Insert)
            {
                AddItemtoCart();
            }

        }

        //Methods
        //auto complete text boxes
        void ItemsAutoComplete()
        {
            txtDescription.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtDescription.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
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


        //Search item via Barcode && Load data into table
        public void SearchBarcode()
        {
            //Load data into table
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT item_code,Barcode,item_name,Warrenty,freeIssue,sqty,Rprice  from supermarket.item where Barcode = '" + txtBarcode.Text + "' ", conn);
            conn.Open();
            DataTable catetable = new DataTable();
            adapter.Fill(catetable);

            BindingSource source = new BindingSource();
            source.DataSource = catetable;

            MySqlCommand cmd = conn.CreateCommand();

            //Search item via Barcode
            cmd.CommandText = ("SELECT item_name,Item_code,Rprice,item_name from supermarket.item where Barcode = '" + txtBarcode.Text + "' ");
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

        //Searchprice only via Barcode
        public void SearchPriceBarcode()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = ("SELECT Rprice from supermarket.item where Barcode = '" + txtBarcode.Text + "' ");
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
                cmd.CommandText = "SELECT Surname,initials,Points from supermarket.loyaltycustomer where Mobile = '" + txtPhone.Text + "' ";
                MySqlDataReader Dataread = cmd.ExecuteReader();
                Dataread.Read();
                if (Dataread.HasRows)
                {
                    lblName.Text = Dataread[1].ToString() + " " + Dataread[0].ToString();
                    lblPoint.Text = Dataread[2].ToString();
                    
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


        //Calculate Balance cash
        public void BalanceCal()
        {
            
            try
            {
                double Balance;
                double cash = 0;
                double points = 0;
                if (!double.TryParse(this.txtCash.Text, out cash))
                {
                    //MessageBox.Show(
                    //    //    String.Format(
                    //    //        "Bad input from this.Amount.text: \"{0}\"",
                    //    //        this.txtCash.Text
                    //    //    )
                    //    //);
                }
                double total = 0;
                if (!double.TryParse(this.lblAmount.Text, out total))
                {
                
                }

                if (!double.TryParse(this.txtPoints.Text, out points))
                {

                }
                if (!(string.IsNullOrWhiteSpace(txtPoints.Text)))
                {
                    Balance = (cash + points) - total;
                    lblBalanceAmount1.Text = Balance.ToString("0.00");
                }
                else
                {
                    Balance = cash- total;
                    lblBalanceAmount1.Text = Balance.ToString("0.00");
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

            }
        }

        


        //Calculate Balance points
        public void BalanceCalPoints()
        {

            try
            {
                   

                    double Balance;
                
                double points = 0;
                double total = 0;
                double cash = 0; 
                if (!double.TryParse(this.txtPoints.Text, out points))
                {
                    
                }
                
                if (!double.TryParse(this.lblAmount.Text, out total))
                {

                }
                if (!double.TryParse(this.txtCash.Text, out cash))
                {

                }

                if ((string.IsNullOrWhiteSpace(lblPoint.Text)))
                {
                   // txtPoints.Text = "";
                    MessageBox.Show("Before using the Loyality points user needs to verify the account");

                }
                if (!(string.IsNullOrWhiteSpace(txtCash.Text)))
                {
                    Balance = (cash + points) - total;
                    lblBalanceAmount1.Text = Balance.ToString("0.00");
                }
                ///// need more implementation 
                //check enogh points
                else
                {
                    Balance = points - total;
                    lblBalanceAmount1.Text = Balance.ToString("0.00");
                }


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

            }



        }

        //total Amount Changed
        public void totalChanged()
        {
            double Balance;

            double points = 0;
            double total = 0;
            double cash = 0;
           
            if (!double.TryParse(this.lblAmount.Text, out total))
            {

            }
            if (!double.TryParse(this.txtPoints.Text, out points))
            {

            }
            if (!double.TryParse(this.txtCash.Text, out cash))
            {

            }

            double updateBalance = (points + cash) - total;
            lblBalanceAmount1.Text = updateBalance.ToString("0.00");

        }

        //Validation
        public bool Regexp(string re, TextBox tb, PictureBox pc, string s)
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
            //Accepts only 9 digits,
            Regex pattern = new Regex(@"(?<!\d)\d{9}(?!\d)");

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
                count_accout();
                AddPoints();
                printer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //Add points to Loyality Customers
        public void AddPoints()
        {
            try
            {
                string total = lblAmount.Text;
                double addPoints = Double.Parse(total) * (0.02);
                int phone = Convert.ToInt32(txtPhone.Text);
               
                string constr = "server=localhost;user id=root;persistsecurityinfo=True;database=madusha";
                MySqlConnection conn = new MySqlConnection(constr);
                conn.Open();
               

                    MySqlCommand cmd = new MySqlCommand("Update supermarket.loyaltycustomer  set Points= Points + @Addpoint   where Mobile= @mobile ", conn);

                    cmd.Parameters.AddWithValue("@Addpoint",addPoints);
                    cmd.Parameters.AddWithValue("@mobile", phone);
                    cmd.ExecuteNonQuery();
                

               
            }
            catch(Exception ex)
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
                    
                }

            }
            else
            {
               
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
              
                MySqlCommand cmd = new MySqlCommand("Update supermarket.item  set sqty= sqty - @qty   where Item_code= @itemcode ", conn);
                
                cmd.Parameters.AddWithValue("@itemcode", this.cart.Rows[i].Cells[1].Value.ToString());
                cmd.Parameters.AddWithValue("@qty", this.cart.Rows[i].Cells[3].Value.ToString());
                cmd.ExecuteNonQuery();
            }
          
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back) || char.IsLetter(e.KeyChar);
            
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !( e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));

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
            if (e.KeyCode == Keys.Insert)
            {
                AddItemtoCart();
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
                        cart.Rows.Clear();
                    }
                
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
            if (e.KeyCode == Keys.Back)
            {
                txtBarcode.Text = "";
                clearFields();
                LoadDate();
            }

             if (e.KeyCode == Keys.Insert)
            {
                AddItemtoCart();
                LoadDate();
            }

            if (e.KeyCode == Keys.F3)
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

       
        //Printer Configured to print Receipt
        public void printer()
        {
            var installedPrinters = PrinterSettings.InstalledPrinters; //I have choosed a printername from 'installedPrinters'
            try
            {
                    int height = (cart.RowCount) * 10 + 50;
                    MadushaPrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Bill", 76, height);
                    MadushaPrintDocument.PrinterSettings.PrinterName = "EPSON TM-U220 Receipt"; //Specify the printer to use.

                    MadushaPrintDocument.PrintPage += new PrintPageEventHandler(this.MadushaPrintDocument_PrintPage);
                    MadushaPrintDocument.Print();

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeTest.Text= DateTime.Now.ToLongTimeString();
            
        }
        //Discount Method
        public void DiscountItemWise()
        {
            string discount = txtDiscount.Text;
            float maximumDiscount = float.Parse(maxDis.Text);
            for (int i = 0; i < discount.ToString().Length; i++)
            {
                if ((txtDiscount.Text[i].ToString() == "%"))
                {
                    txtDiscount.MaxLength = i;
                    string rate = discount.Substring(0, i).ToString();
                    float fRate = float.Parse(rate);

                    float IPrice = float.Parse(txtPrice.Text);
                    float NewPrice = IPrice - (IPrice * (fRate / 100));
                    float reduce_amount = (IPrice * (fRate / 100));
                    if ((NewPrice) >= 0)
                    {
                        txtPrice.Text = NewPrice.ToString();

                        txtDiscount.MaxLength = i;


                        if (maximumDiscount < reduce_amount)
                        {
                            MessageBox.Show("Sorry !! Your Discount amount exceeded for a item  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtDiscount.Text = "";
                            SearchPriceBarcode();
                        }

                    }

                    else
                    {
                        MessageBox.Show("Invalid Discount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtDiscount.Text = "";
                        SearchPriceBarcode();
                    }
                   

                }
                if ((txtDiscount.Text[i].ToString() == "-"))
                {
                    txtDiscount.MaxLength = i;
                    float disPrice = float.Parse(discount.Substring(0, i).ToString());

                    float initialPrice = float.Parse(txtPrice.Text);
                    float ApplyDis = initialPrice - disPrice;
                    float reduce_amount = disPrice;
                    if (ApplyDis >= 0)
                    {
                        txtPrice.Text = ApplyDis.ToString();
                        if (maximumDiscount < reduce_amount)
                        {
                            MessageBox.Show("Sorry !! Your Discount amount exceeded for a item  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtDiscount.Text = "";
                            SearchPriceBarcode();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Discount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    txtDiscount.MaxLength = 6;

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
                
               // MessageBox.Show(ex.Message);
            }
        }

        private void txtDiscount_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Back)
            {
                txtDiscount.Text = "";
                SearchPriceBarcode();

            }
           if (e.KeyCode == Keys.F3)
            {
                txtBarcode.Focus();
            }

            if (e.KeyCode == Keys.Insert)
            {
                AddItemtoCart();
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

        private void txtQty_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                AddItemtoCart();
                LoadDate();
            }
        }

        private void txtPrice_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                AddItemtoCart();
                LoadDate();
            }
        }

    public void discountLevel()
        {
           
            try
            {
                string constr = "server=localhost;user id=root;persistsecurityinfo=True;database=supermarket";
                MySqlConnection conn = new MySqlConnection(constr);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT user,maximumDiscount from supermarket.users where user = '"+login.getUsername()+"' ";
               
                MySqlDataReader Dataread = cmd.ExecuteReader();
                Dataread.Read();
                if (Dataread.HasRows)
                {
                    maxDis.Text = Dataread[1].ToString() ;

                    
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           
                if (sPanel.Width >= 100)
                {


                this.sPanel.Width = 25;
                button1.Text = "V\nE\nR\nI\nF\nY";
                this.timer2.Enabled = false;


            }
                else 
                {
                    this.sPanel.Width += 230;
                    button1.Text = "H\nI\nD\nE";
                    this.timer2.Enabled = false;


            }

           
            
        }

        public void smsVerification()
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                try
                {
                    Random random = new Random();
                    int value = random.Next(1001, 9999);

                    lblRandom.Text = value.ToString();




                    string url = " http://smsc.vianett.no/v3/send.ashx?" +
                        "src=" +"94"+ txtPhone.Text + "&" +
                        "dst=" + "94" + txtPhone.Text + "&" +
                        "msg=" + System.Web.HttpUtility.UrlEncode("Your Verification Code is :" + value + "\n -From Madusha SuperMarket-", System.Text.Encoding.GetEncoding("ISO-8859-1")) + "&" +
                        "username=" + System.Web.HttpUtility.UrlEncode("lksurath@gmail.com") + "&" +
                        "password=" + System.Web.HttpUtility.UrlEncode("w9d9a");
                    string result = client.DownloadString(url);
                    if (result.Contains("OK"))
                    {
                       MessageBox.Show("Send Sucessfully");
                    }
                    else
                    {
                        MessageBox.Show("Failed to Send");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (lblRandom.Text == txtVerify.Text)
            {
                lblVeriMsg.Text = "Verified Sucessfully";
            }
            else
            {
                lblVeriMsg.Text = "Not Verified";
            }
        }

        

       

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (txtPhone.Text.Length == 9)
            {
                SearchCustomer();
                
                Regexp(@"(?<!\d)\d{9}(?!\d)", txtPhone, pictureBox1, "Please Enter 9 digits without 0");
            }
            else
            {
                pictureBox1.Image = null;
                lblName.Text = "";
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
        
            e.Handled = !(e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Space);
        }

        private void txtPhone_KeyUp(object sender, KeyEventArgs e)
        {
            if((txtPhone.Text.Length == 9) && (string.IsNullOrWhiteSpace(txtBarcode.Text)))
            {
                if(e.KeyCode == Keys.F1)
                {
                   smsVerification();
                }
            }
        }

        private void txtDiscount_Enter(object sender, EventArgs e)
        {
            discountLevel();
        }

        private void txtBarcode_MouseEnter(object sender, EventArgs e)
        {
          
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtPoints_TextChanged(object sender, EventArgs e)
        {
            BalanceCalPoints();
        }

        private void txtCash_KeyUp(object sender, KeyEventArgs e)
        {
           


               
                
            }

        private void lblAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double cash = 0;
                double points = 0;
                if (!double.TryParse(this.txtCash.Text, out cash))
                {

                }
                if (!double.TryParse(this.txtPoints.Text, out points))
                {

                }


                if (!(string.IsNullOrWhiteSpace(txtCash.Text)) || (!(string.IsNullOrWhiteSpace(txtPoints.Text))))
                {
                    totalChanged();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblName_TextChanged(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(lblName.Text)))
            {
                txtPoints.Enabled = true;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

        
    }
    

