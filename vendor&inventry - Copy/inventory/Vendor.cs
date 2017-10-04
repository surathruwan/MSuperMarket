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
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace vendor_management
{
    public partial class Vendor : Form
    {
        //made a change
        //Hiiiii

        //SSlmode =none because it gave an error in connecting
        string constring = "Sslmode=none;datasource=localhost;port=3306;username=root;password=  ";



        public Vendor()
        {
            InitializeComponent();
            load_table();
            load_table2();
            load_table3();
            load_table4();
            fillCombo();
            // comboBox3.Text = DateTime.Now.Year.ToString();
            //comboBox2.Text = DateTime.Now.ToString("MMMMMMMMMM");
            table1_init();


            date.MinDate = DateTime.Now;

        }
        public int z = 0;

        public string code
        { get; set; }
        public string Vname
        { get; set; }
        public string Cname
        { get; set; }
        public string Pnum
        { get; set; }
        public string address
        { get; set; }
        public string telephone
        { get; set; }
        public string fax
        { get; set; }
        public string email
        { get; set; }
        public string AccName
        { get; set; }
        public string AccNo
        { get; set; }
        public string bankName
        { get; set; }

        public void table1_init() {
            bunifuCustomDataGrid1.Columns[0].HeaderText = "Vendor Code";
            bunifuCustomDataGrid1.Columns[1].HeaderText = "Vendor Name";
            bunifuCustomDataGrid1.Columns[2].HeaderText = "Company Name";
            bunifuCustomDataGrid1.Columns[3].HeaderText = "Mobile Num";
            bunifuCustomDataGrid1.Columns[4].HeaderText = "Address";
            bunifuCustomDataGrid1.Columns[5].HeaderText = "Telephone";
            bunifuCustomDataGrid1.Columns[6].HeaderText = "Fax";
            bunifuCustomDataGrid1.Columns[7].HeaderText = "Email";
            bunifuCustomDataGrid1.Columns[8].HeaderText = "Account Name";
            bunifuCustomDataGrid1.Columns[9].HeaderText = "Account Number";
            bunifuCustomDataGrid1.Columns[10].HeaderText = "Bank Name";

        }

        public void Regexp(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {
            ToolTip buttonToolTip = new ToolTip();
            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {

                pc.Image = inventory.Properties.Resources.invalid;
                n.SetToolTip(pc, s);
                z = -99;

            }
            else
            {
                pc.Image = null;
                // n.SetToolTip(pc, "valid entry");
                z = 1;
            }
        }
        void fillCombo()
        {
            try
            {
                this.comboBox1.Items.Clear();
                this.txtVendor.Items.Clear();
                string query = "select * from supermarket.vendor;";
                MySqlConnection conDatabase = new MySqlConnection(constring);
                MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);
                MySqlDataReader myReader;

                conDatabase.Open();
                myReader = cmdDatabase.ExecuteReader();



                while (myReader.Read())
                {
                    string Vencode = myReader.GetString("code");
                    comboBox1.Items.Add(Vencode);
                    txtVendor.Items.Add(Vencode);
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            comboBox3.Text = DateTime.Now.Year.ToString();
            comboBox2.Text = DateTime.Now.ToString("MMMMMMMMMM");


        }


        void load_tab(String Query, Bunifu.Framework.UI.BunifuCustomDataGrid grid)//Load table method
        {

            try
            {
                //  string constring = "Sslmode=none;datasource=localhost;port=3306;username=root;password=  ";
                ///string Query = "select * from supermarket.vendor;";

                MySqlConnection MyConn2 = new MySqlConnection(constring);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);

                //For offline connection we weill use  MySqlDataAdapter class.  
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                //bunifuCustomDataGrid1.DataSource = dTable;

                grid.DataSource = dTable;  // here i have assign dTable object to the dataGridView1 object to display data.                         
                // MyConn2.Close();  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        void load_table()//for vendor form
        {
            string Query = "select * from supermarket.vendor;";
            load_tab(Query, bunifuCustomDataGrid1);
        }

        void load_table2()//for vendor scorecard form
        {
            string Query = "select * from supermarket.vendorscorecard order by year desc ;";
            load_tab(Query, bunifuCustomDataGrid2);

        }

        void load_table3()//for quotation form,references
        {
            string Query = "select * from supermarket.vendorquotation ;";
            load_tab(Query, bunifuCustomDataGrid3);

        }

        void load_table4()//for quotation form,items
        {

            string Query = "select * from supermarket.vendorquotation1 order by RefNo;";
            load_tab(Query, bunifuCustomDataGrid4);

        }






        //email sending
        private void bunifuFlatButton7_Click_1(object sender, EventArgs e)
        {
            emailCheck.Form1 cmail = new emailCheck.Form1();

            cmail.Show();
        }

        //vendor form
        private void btnSave_Click(object sender, EventArgs e)
        {
            code = this.vCode_txt.Text;
            Vname = this.vName_txt.Text;
            Cname = this.cName_txt.Text;
            Pnum = this.pNum_txt.Text;
            address = this.address_txt.Text;
            telephone = this.telephone_txt.Text;
            fax = this.faxNo_txt.Text;
            email = this.email_txt.Text;
            AccName = this.accName_txt.Text;
            AccNo = this.accNo_txt.Text;
            bankName = this.bankName_txt.Text;

            Regexp(@"^([\w]+)@([\w]+)\.([\w]+)$", email_txt, pictureBox1, "Email should consist @,.");
            Regexp(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", pNum_txt, pictureBox2, "Mobile should consist 10 digits");
            Regexp(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", telephone_txt, pictureBox3, "Telephone should consist 10 digits");
            Regexp(@"^[A-Z]{2}[1-9]{4}$", vCode_txt, pictureBox4, "Vendor code length should be 6 ");
            Regexp(@"^[0-9]{12}$", accNo_txt, pictureBox6, "Account number length should be 12");
            Regexp(@"^[0-9]{10}$", faxNo_txt, pictureBox5, "Fax number length should be null or 10");

            if ((Vname.Length == 0) || (Cname.Length == 0) || (Pnum.Length == 0) || (address.Length == 0) || (telephone.Length == 0) || (fax.Length == 0) || (AccNo.Length == 0) || (AccName.Length == 0) || (bankName.Length == 0) || (email.Length == 0))
                MessageBox.Show("Please fill all fields");


            else if (z == -99)
            {

            }


            else
            {
                try
                {
                    //SSlmode =none because it gave an error in connecting
                    //  string constring = "Sslmode=none;datasource=localhost;port=3306;username=root;password=  ";
                    string query = "insert into supermarket.vendor(code,fullname,name,mobile,address,telephone,fax,email,accountName,accountNum,bankName)values('" + code + "','" + Vname + "','" + Cname + "','" + Pnum + "','" + address + "','" + telephone + "','" + fax + "','" + email + "','" + AccName + "','" + AccNo + "','" + bankName + "');";
                    MySqlConnection conDatabase = new MySqlConnection(constring);
                    MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);
                    MySqlDataReader myReader;

                    conDatabase.Open();
                    myReader = cmdDatabase.ExecuteReader();
                    MessageBox.Show("Saved");
                    while (myReader.Read())
                    {

                    }

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
                load_table();
                fillCombo();
            }
        }
        //vendor form
        private void btnUpdate_Click(object sender, EventArgs e)
        {

            code = this.vCode_txt.Text;
            Vname = this.vName_txt.Text;
            Cname = this.cName_txt.Text;
            Pnum = this.pNum_txt.Text;
            address = this.address_txt.Text;
            telephone = this.telephone_txt.Text;
            fax = this.faxNo_txt.Text;
            email = this.email_txt.Text;
            AccName = this.accName_txt.Text;
            AccNo = this.accNo_txt.Text;
            bankName = this.bankName_txt.Text;

            Regexp(@"^([\w]+)@([\w]+)\.([\w]+)$", email_txt, pictureBox1, "Email should consist @,.");
            Regexp(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", pNum_txt, pictureBox2, "Mobile should consist 10 digits");
            Regexp(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", telephone_txt, pictureBox3, "Telephone should consist 10 digits");
            Regexp(@"^[A-Z]{2}[1-9]{4}$", vCode_txt, pictureBox4, "Vendor code length should be 6 ");
            Regexp(@"^[0-9]{12}$", accNo_txt, pictureBox6, "Account number length should be 12");
            Regexp(@"^[0-9]{10}$", faxNo_txt, pictureBox5, "Fax number length should be null or 10");

            if ((Vname.Length == 0) || (Cname.Length == 0) || (Pnum.Length == 0) || (address.Length == 0) || (telephone.Length == 0) || (fax.Length == 0) || (AccNo.Length == 0) || (AccName.Length == 0) || (bankName.Length == 0) || (email.Length == 0))
                MessageBox.Show("Please fill all fields");

            else if (z == -99)
            {

            }



            else
            {
                try
                {
                    //SSlmode =none because it gave an error in connecting
                    //string constring = "Sslmode=none;datasource=localhost;port=3306;username=root;password=  ";
                    string query = "update supermarket.vendor set code='" + this.vCode_txt.Text + "',fullname='" + this.vName_txt.Text + "',name='" + this.cName_txt.Text + "',mobile='" + this.pNum_txt.Text + "',address='" + this.address_txt.Text + "',telephone='" + this.telephone_txt.Text + "',fax='" + this.faxNo_txt.Text + "',email='" + this.email_txt.Text + "',accountName='" + this.accName_txt.Text + "',accountNum='" + this.accNo_txt.Text + "',bankName='" + this.bankName_txt.Text + "'where code='" + this.vCode_txt.Text + "' ;";
                    MySqlConnection conDatabase = new MySqlConnection(constring);
                    MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);
                    MySqlDataReader myReader;

                    conDatabase.Open();
                    myReader = cmdDatabase.ExecuteReader();
                    MessageBox.Show("Updated");
                    while (myReader.Read())
                    {

                    }

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
                load_table();
            }
        }
        //vendor form
        private void btnDelete_Click(object sender, EventArgs e)
        {

            code = this.vCode_txt.Text;
            Vname = this.vName_txt.Text;
            Cname = this.cName_txt.Text;
            Pnum = this.pNum_txt.Text;
            address = this.address_txt.Text;
            telephone = this.telephone_txt.Text;
            fax = this.faxNo_txt.Text;
            email = this.email_txt.Text;
            AccName = this.accName_txt.Text;
            AccNo = this.accNo_txt.Text;
            bankName = this.bankName_txt.Text;

            if (code.Length == 0)
                MessageBox.Show("Please fill Vendor code ");

            else
            {

                try
                {
                    //SSlmode =none because it gave an error in connecting
                    //string constring = "Sslmode=none;datasource=localhost;port=3306;username=root;password=  ";
                    string query = "delete from supermarket.vendor where code='" + this.vCode_txt.Text + "' ;";
                    MySqlConnection conDatabase = new MySqlConnection(constring);
                    MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);
                    MySqlDataReader myReader;

                    conDatabase.Open();
                    myReader = cmdDatabase.ExecuteReader();
                    MessageBox.Show("Deleted");
                    while (myReader.Read())
                    {

                    }

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    MessageBox.Show("Please make sure this deletion will not affect other tables");
                }
                load_table();
            }
        }
        //vendor form
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.vCode_txt.Text = "";
            this.vName_txt.Text = "";
            this.cName_txt.Text = "";
            this.pNum_txt.Text = "";
            this.address_txt.Text = "";
            this.telephone_txt.Text = "";
            this.faxNo_txt.Text = "";
            this.email_txt.Text = "";
            this.accName_txt.Text = "";
            this.accNo_txt.Text = "";
            this.bankName_txt.Text = "";

            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            pictureBox5.Image = null;
            pictureBox6.Image = null;
        }

        private void vCode_txt1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void vName_txt1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void address_txt_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void vCode_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));

        }

        private void vName_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);

        }

        private void cName_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);

        }

        private void pNum_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));

        }

        private void address_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || char.IsNumber(e.KeyChar) || char.IsPunctuation(e.KeyChar));

        }

        private void telephone_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));

        }

        private void faxNo_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar) || char.IsPunctuation(e.KeyChar));

        }

        private void accName_txt_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void accName_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);

        }

        private void bankName_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);

        }

        private void accNo_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));

        }




        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {



                DataGridViewRow row = this.bunifuCustomDataGrid1.Rows[e.RowIndex];
                code = row.Cells["code"].Value.ToString();
                Vname = row.Cells["fullname"].Value.ToString();
                Cname = row.Cells["name"].Value.ToString();
                Pnum = row.Cells["mobile"].Value.ToString();
                address = row.Cells["address"].Value.ToString();
                telephone = row.Cells["telephone"].Value.ToString();
                fax = row.Cells["fax"].Value.ToString();
                email = row.Cells["email"].Value.ToString();
                AccName = row.Cells["accountName"].Value.ToString();
                AccNo = row.Cells["accountNum"].Value.ToString();
                bankName = row.Cells["bankName"].Value.ToString();

                this.vCode_txt.Text = code;
                this.vName_txt.Text = Vname;
                this.cName_txt.Text = Cname;
                this.pNum_txt.Text = Pnum;
                this.address_txt.Text = address;
                this.telephone_txt.Text = telephone;
                this.faxNo_txt.Text = fax;
                this.email_txt.Text = email;
                this.accName_txt.Text = AccName;
                this.accNo_txt.Text = AccNo;
                this.bankName_txt.Text = bankName;


            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {


            //Regexp(@"^[A-Z]{2}[1-9]{4}$", txtVendor, pictureBox5, "Invalid");


            if ((txtRef.Text.Length == 0) || (txtVendor.Text.Length == 0) || (date.Text.Length == 0) || (dateValidUpto.Text.Length == 0))
                MessageBox.Show("Please fill all fields");


            else if (z == -99)
            {

            }


            else
            {
                try
                {
                    //SSlmode =none because it gave an error in connecting
                    //string constring = "Sslmode=none;datasource=localhost;port=3306;username=root;password=  ";
                    string query = "insert into supermarket.vendorquotation(ReferenceNum,VendorCode,Date,ValidUpto)values('" + txtRef.Text + "','" + txtVendor.Text + "','" + date.Text + "','" + dateValidUpto.Text + "');";


                    MySqlConnection conDatabase = new MySqlConnection(constring);
                    MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);
                    MySqlDataReader myReader;

                    conDatabase.Open();
                    myReader = cmdDatabase.ExecuteReader();
                    MessageBox.Show("Saved");
                    while (myReader.Read())
                    {

                    }


                }
                catch (Exception ee)
                {
                    //MessageBox.Show(ee.Message);
                    MessageBox.Show("Please check the fields again,Make sure vendor code exists in database");
                }
                load_table3();
            }
        }

        private void btnUpdate1_Click(object sender, EventArgs e)
        {


            //  Regexp(@"^[A-Z]{2}[1-9]{4}$", txtVendor, pictureBox5, "Vendor code length should be 6 ");

            if ((txtRef.Text.Length == 0) || (cmbType.Text.Length == 0) || (txtCode.Text.Length == 0) || (txtDescription.Text.Length == 0) || (txtQty.Text.Length == 0) || (txtDiscount.Text.Length == 0) || (txtAmount.Text.Length == 0))
                MessageBox.Show("Please fill all fields");


            else if (z == -99)
            {

            }


            else
            {
                try
                {
                    //SSlmode =none because it gave an error in connecting
                    //string constring = "Sslmode=none;datasource=localhost;port=3306;username=root;password=  ";
                    string query = "update supermarket.vendorquotation1 set Type='" + cmbType.Text + "',itemCode='" + txtCode.Text + "',Description='" + txtDescription.Text + "',Qty='" + txtQty.Text + "',Discount='" + txtDiscount.Text + "',Amount='" + txtAmount.Text + "' where itemCode='" + txtCode.Text + "'  AND RefNo='" + txtRef.Text + "';";

                    MySqlConnection conDatabase = new MySqlConnection(constring);
                    MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);
                    MySqlDataReader myReader;

                    conDatabase.Open();
                    myReader = cmdDatabase.ExecuteReader();
                    MessageBox.Show("Updated");
                    while (myReader.Read())
                    {

                    }

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
                load_table4();
            }
        }

        private void bunifuCustomDataGrid3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = this.bunifuCustomDataGrid3.Rows[e.RowIndex];

                txtRef.Text = row.Cells["ReferenceNum"].Value.ToString();
                txtVendor.Text = row.Cells["VendorCode"].Value.ToString();

                //set the min value because there is an exception
                date.MinDate = DateTime.Parse(row.Cells["Date"].Value.ToString());
                date.Text = row.Cells["Date"].Value.ToString();

                dateValidUpto.Text = row.Cells["ValidUpto"].Value.ToString();


                txtVendor.Show();
                date.Show();
                dateValidUpto.Show();
                btnadd.Show();
                label25.Show();
                label22.Show();
                label21.Show();

                cmbType.Text = null;
                txtCode.Text = null;
                txtDescription.Text = null;
                txtQty.Text = null;
                txtDiscount.Text = null;
                txtAmount.Text = null;

            }
        }

        private void date_ValueChanged(object sender, EventArgs e)
        {
            dateValidUpto.MinDate = date.Value;

        }

        private void ctnAdd2_Click(object sender, EventArgs e)
        {
            if ((txtRef.Text.Length == 0) || (cmbType.Text.Length == 0) || (txtCode.Text.Length == 0) || (txtDescription.Text.Length == 0) || (txtQty.Text.Length == 0) || (txtDiscount.Text.Length == 0) || (txtAmount.Text.Length == 0))
                MessageBox.Show("Please fill all fields");





            else
            {
                try
                {
                    //SSlmode =none because it gave an error in connecting
                    //string constring = "Sslmode=none;datasource=localhost;port=3306;username=root;password=  ";
                    string query = "insert into supermarket.vendorquotation1(RefNo,Type,itemCode,Description,Qty,Discount,Amount)values('" + txtRef.Text + "','" + cmbType.Text + "','" + txtCode.Text + "','" + txtDescription.Text + "','" + txtQty.Text + "','" + txtDiscount.Text + "','" + txtAmount.Text + "');";


                    MySqlConnection conDatabase = new MySqlConnection(constring);
                    MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);
                    MySqlDataReader myReader;

                    conDatabase.Open();
                    myReader = cmdDatabase.ExecuteReader();
                    MessageBox.Show("Saved");
                    while (myReader.Read())
                    {

                    }


                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);

                }
                load_table4();
                txtCode.Text = "";
                txtDescription.Text = "";
                txtQty.Text = "";
                txtDiscount.Text = "";
                txtAmount.Text = "";
            }

        }

        private void bunifuCustomDataGrid4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = this.bunifuCustomDataGrid4.Rows[e.RowIndex];
                txtRef.Text = row.Cells["RefNo"].Value.ToString();

                txtVendor.Hide();
                date.Hide();
                dateValidUpto.Hide();
                btnadd.Hide();
                label25.Hide();
                label22.Hide();
                label21.Hide();


                cmbType.Text = row.Cells["Type"].Value.ToString();
                txtCode.Text = row.Cells["itemCode"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();
                txtQty.Text = row.Cells["Qty"].Value.ToString();
                txtDiscount.Text = row.Cells["Discount"].Value.ToString();
                txtAmount.Text = row.Cells["Amount"].Value.ToString();

            }
        }

        private void btnDelRef_Click(object sender, EventArgs e)
        {
            if (txtRef.Text.Length == 0)
                MessageBox.Show("Please fill reference number");





            else
            {
                try
                {
                    //SSlmode =none because it gave an error in connecting
                    //string constring = "Sslmode=none;datasource=localhost;port=3306;username=root;password=  ";
                    string query = "delete from supermarket.vendorquotation where ReferenceNum='" + txtRef.Text + "';";


                    MySqlConnection conDatabase = new MySqlConnection(constring);
                    MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);
                    MySqlDataReader myReader;

                    conDatabase.Open();
                    myReader = cmdDatabase.ExecuteReader();
                    MessageBox.Show("Reference Deleted");
                    while (myReader.Read())
                    {

                    }


                }
                catch (Exception ee)
                {
                    // MessageBox.Show(ee.Message);
                    MessageBox.Show("Please check whether deletion affect other databases");
                }
                load_table3();
            }
        }
        private void btnDelete1_Click(object sender, EventArgs e)
        {
            if ((txtRef.Text.Length == 0) || (txtCode.Text.Length == 0))
                MessageBox.Show("You need to fill both reference number and item code");





            else
            {
                try
                {
                    //SSlmode =none because it gave an error in connecting
                    //string constring = "Sslmode=none;datasource=localhost;port=3306;username=root;password=  ";
                    string query = "delete from supermarket.vendorquotation1 where RefNo='" + txtRef.Text + "' and ItemCode='" + txtCode.Text + "';";


                    MySqlConnection conDatabase = new MySqlConnection(constring);
                    MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);
                    MySqlDataReader myReader;

                    conDatabase.Open();
                    myReader = cmdDatabase.ExecuteReader();
                    MessageBox.Show("Item Deleted");
                    while (myReader.Read())
                    {

                    }


                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);

                }
                load_table4();
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {

            cmbType.Text = "";
            txtCode.Text = "";
            txtDescription.Text = "";
            txtQty.Text = "";
            txtDiscount.Text = "";
            txtAmount.Text = "";
        }


        private void CodeTxt_KeyUp(object sender, KeyEventArgs e)
        {




        }

        private void CodeTxt_Leave(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void dateValidUpto_ValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            txtVendor.Show();
            date.Show();
            dateValidUpto.Show();
            btnadd.Show();
            label25.Show();
            label22.Show();
            label21.Show();

            date.MinDate = DateTime.Now;
            date.Text = "";
            dateValidUpto.Text = "";

            txtRef.Text = "";
            txtVendor.Text = "";
            cmbType.Text = "";
            txtCode.Text = "";
            txtDescription.Text = "";
            txtQty.Text = "0";
            txtDiscount.Text = "0";
            txtAmount.Text = "0";
            load_table3();
            load_table4();
            textBox1.Text = "";
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));

        }

        private void txtRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));

        }

        private void txtVendor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));

        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));

        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));

        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));

        }

        private void txtVendor_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back && !(e.KeyChar == (char)Keys.Back));
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back && !(e.KeyChar == (char)Keys.Back));
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            try
            {
                string query = "select fullname from supermarket.vendor where code='" + comboBox1.Text + "';";
                MySqlConnection conDatabase = new MySqlConnection(constring);
                MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);
                MySqlDataReader myReader;

                conDatabase.Open();
                myReader = cmdDatabase.ExecuteReader();

                while (myReader.Read())
                {
                    string Vencode = myReader.GetString("fullname");
                    nameTxt.Text = Vencode;
                }


            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back && !(e.KeyChar == (char)Keys.Back));
        }

        private void comboBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back && !(e.KeyChar == (char)Keys.Back));
        }






        private void gradingBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string GRADE = null;
                int point = 0;
                double grd = 0;
                int DEF = Convert.ToInt32(defectiveTxt.Text.ToString());
                int fal = Convert.ToInt32(failuresTxt.Text.ToString());
                int prof = Convert.ToInt32(profitsTxt.Text.ToString());
                int cred = Convert.ToInt32(creditTxt.Text.ToString());
                int negate = Convert.ToInt32(negativesTxt.Text.ToString());

                grd = ((cred * 0.8) - (DEF * 0.5) - (fal * 0.5) + (prof * 0.001) - (negate * 1));


                if (grd >= 50)
                {
                    GRADE = "A";
                    point = 5;
                }
                else if (grd >= 30)
                {
                    GRADE = "B";
                    point = 4;
                }
                else if (grd >= 10)
                {
                    GRADE = "C";
                    point = 3;
                }

                else if (grd >= 5)
                {
                    GRADE = "D";
                    point = 2;
                }
                else
                {
                    GRADE = "E";
                    point = 1;
                }

                label5.Text = GRADE;
                label27.Text = point.ToString();
            } catch (Exception eee) {

                MessageBox.Show("Please check values");
            }


        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if ((comboBox1.Text.Length == 0) || (nameTxt.Text.Length == 0) || (comboBox2.Text.Length == 0) || (comboBox3.Text.Length == 0) || (label5.Text.Length == 0))
                MessageBox.Show("Please fill all fields and Press Generate grading button");

            else
            {
                try
                {
                    //SSlmode =none because it gave an error in connecting
                    //  string constring = "Sslmode=none;datasource=localhost;port=3306;username=root;password=  ";
                    string query = "insert into supermarket.vendorscorecard(vendorCode,VendorName,month,year,Grading,point)values('" + comboBox1.Text + "','" + nameTxt.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + label5.Text + "','"+ label27.Text + "');";

                    MySqlConnection conDatabase = new MySqlConnection(constring);
                    MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);
                    MySqlDataReader myReader;

                    conDatabase.Open();
                    myReader = cmdDatabase.ExecuteReader();
                    MessageBox.Show("Saved");
                    while (myReader.Read())
                    {

                    }

                }
                catch (Exception a)
                {

                    try
                    {
                        string query1 = "update supermarket.vendorscorecard set Grading='" + label5.Text + "',point='"+ label27.Text + "' where vendorCode='" + comboBox1.Text + "'  AND month='" + comboBox2.Text + "' AND year='" + comboBox3.Text + "';";

                        MySqlConnection conDatabase = new MySqlConnection(constring);
                        MySqlCommand cmdDatabase = new MySqlCommand(query1, conDatabase);
                        MySqlDataReader myReader;

                        conDatabase.Open();
                        myReader = cmdDatabase.ExecuteReader();
                        MessageBox.Show("Updated");
                        while (myReader.Read()) { }
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message);
                    }

                }


                load_table2();

            }
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));

        }

        private void cmbType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);


        }




       //Search function
        public void searchVendor(String valueTosearch, String query, Bunifu.Framework.UI.BunifuCustomDataGrid tablee)
        {


            try
            {

                MySqlConnection MyConn2 = new MySqlConnection(constring);
                MySqlCommand MyCommand2 = new MySqlCommand(query, MyConn2);

                //For offline connection we weill use  MySqlDataAdapter class.  
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                //bunifuCustomDataGrid1.DataSource = dTable;
                tablee.DataSource = dTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        //tab1
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            String valueTosearch = bunifuMetroTextbox14.Text.ToString();
            if (radioButton1.Checked)
            {
                string query = "select * from supermarket.vendor where code like '%" + valueTosearch + "%';";
                searchVendor(valueTosearch, query, bunifuCustomDataGrid1);
            }
            else
            {
                string query = "select * from supermarket.vendor where fullname like '%" + valueTosearch + "%';";
                searchVendor(valueTosearch, query, bunifuCustomDataGrid1);
            }

        }


  
        //tab3
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            String valueTosearch = textBox1.Text.ToString();

            string query = "select * from supermarket.vendorquotation1 where RefNo like '%" + valueTosearch + "%';";
            //searchRef(valueTosearch, query);
            searchVendor(valueTosearch, query, bunifuCustomDataGrid4);

        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            
            try
            {
                this.chart1.Series["Grading"].Points.Clear();
                string query = "select * from supermarket.vendorscorecard where month='"+ comboBox5.Text+ "' and year='"+ comboBox4.Text + "';";
                MySqlConnection conDatabase = new MySqlConnection(constring);
                MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);
                MySqlDataReader myReader;

                conDatabase.Open();
                myReader = cmdDatabase.ExecuteReader();
                
                while (myReader.Read())
                {
                    this.chart1.Series["Grading"].Points.AddXY(myReader.GetString("vendorCode"), myReader.GetString("point"));
                    
                }
                // conDatabase.Close();

                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuCustomDataGrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {



                DataGridViewRow row = this.bunifuCustomDataGrid2.Rows[e.RowIndex];
                comboBox1.Text= row.Cells["vendorCode"].Value.ToString();
               // nameTxt.Text = row.Cells["VendorName"].Value.ToString();
                comboBox2.Text = row.Cells["month"].Value.ToString();
                comboBox3.Text = row.Cells["year"].Value.ToString();

                       


            }
        }

        private void delbtn_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length == 0)
                MessageBox.Show("Please fill Vendor code ");

            else
            {

                try
                {
                    //SSlmode =none because it gave an error in connecting
                    //string constring = "Sslmode=none;datasource=localhost;port=3306;username=root;password=  ";
                    string query = "delete from supermarket.vendorscorecard where vendorCode='" + comboBox1.Text + "'  AND month='" + comboBox2.Text + "' AND year='" + comboBox3.Text + "' ;";
                    MySqlConnection conDatabase = new MySqlConnection(constring);
                    MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);
                    MySqlDataReader myReader;

                    conDatabase.Open();
                    myReader = cmdDatabase.ExecuteReader();
                    MessageBox.Show("Record Deleted");
                    while (myReader.Read())
                    {

                    }

                }
                catch (Exception ee)
                {
                    //MessageBox.Show(ee.Message);
                    MessageBox.Show("Make sure this deletion will not affect other tables");
                }
                load_table2();
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton3_MouseHover(object sender, EventArgs e)
        {
           // ToolTip buttonToolTip = new ToolTip();
            ToolTip n = new ToolTip();
            n.SetToolTip(bunifuImageButton3, "Refresh Form");
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@"VendorQuotation.pdf", FileMode.Create));
            doc.Open();

            MessageBox.Show("PDF Created sucessfuly!!");

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
            prg.Add(new Chunk("Vendor Quotation", font5));
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

            PdfPTable table = new PdfPTable(bunifuCustomDataGrid4.Columns.Count);

            //add headers from gridview to table
            iTextSharp.text.Font fonth = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);



            for (int j = 0; j < bunifuCustomDataGrid4.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(bunifuCustomDataGrid4.Columns[j].HeaderText.ToUpper(), fonth));
                table.AddCell(cell);

            }

            //flag first row as header
            table.HeaderRows = 1;


            //add actual rows from grid to table
            for (int i = 0; i < bunifuCustomDataGrid4.Rows.Count; i++)
            {
                table.WidthPercentage = 100;

                for (int k = 0; k < bunifuCustomDataGrid4.Columns.Count; k++)
                {
                    if (bunifuCustomDataGrid4[k, i].Value != null)
                    {

                        table.AddCell(new Phrase(bunifuCustomDataGrid4[k, i].Value.ToString()));
                    }

                }


            }

            //add out table
            doc.Add(table);

            doc.Close();

            System.Diagnostics.Process.Start(@"VendorQuotation.pdf");

        }

        private void bunifuImageButton4_MouseHover(object sender, EventArgs e)
        {
            ToolTip n = new ToolTip();
            n.SetToolTip(bunifuImageButton4, "Generate pdf");
        }
    }
}



