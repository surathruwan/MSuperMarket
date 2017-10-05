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
using System.IO;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
namespace madushaTemp
{

    public partial class repair : Form
    {
        string itemcode;
        string i;
        string id;
        string picPath;
        string cname;
        string cid = "";
        string item_id;
        string item_name;
        string cust_id;
        int a,b,c,d,h;
        //ruchira
        MySqlConnection myConn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        public repair()
        {


            InitializeComponent();
            //loadcustItem();
            loaddatagridneedtosendtemp();
            loadneedtosend();
            loadpanel2sendtooriginal();
            loadsendwith();
            loadtableselecteditem();
            loadtableFullSpareParts();
            loadsendmessage();
            BindData1();

            txtt3from.Text = "ruchidhana@gmail.com";
            tableFullSpareParts.RowTemplate.Height = 80;


            try
            {

                MySqlDataAdapter mda = new MySqlDataAdapter("SELECT MAX(invoice_id) AS invoice_id FROM supermarket.payment ", myConn);
                DataTable dt = new DataTable();
                mda.Fill(dt);
                String id;

                foreach (DataRow dr in dt.Rows)
                {
                    id = dr["invoice_id"].ToString();


                    id = id + 1;
                    txtt4invoiceno.Text = "IN100" + id;

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        public repair(string name)
        {
            InitializeComponent();
            txtt1name.Text = name;
        }



        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox3_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseClick_1(object sender, MouseEventArgs e)
        {

        }

        private void listBox1_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void txtsupplier_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtsupplier_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void bunifuCustomLabel16_Click(object sender, EventArgs e)
        {

        }

        private void txtextraitems_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuCustomLabel32_Click(object sender, EventArgs e)
        {

        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        public void ProgresBar()
        {
            for (int i = 0; i <= 100; i++)
            {
                bunifuCircleProgressbar1.Value = i;
                bunifuCircleProgressbar1.Update();

            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbprepare_onItemSelected(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
           
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        public void Regexp1(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {
            ToolTip buttonToolTip = new ToolTip();
            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {
                pc.Image = inventory.Properties.Resources.invalid;
                n.SetToolTip(pc, s);
                a = -99;
            }
            else
            {
                pc.Image = null;
                a = 0;
            }
        }

        public void Regexp2(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {
            ToolTip buttonToolTip = new ToolTip();
            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {
                pc.Image = inventory.Properties.Resources.invalid;
                n.SetToolTip(pc, s);
                b = -99;
            }
            else
            {
                pc.Image = null;
                b = 0;
            }
        }

        public void Regexp3(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {
            ToolTip buttonToolTip = new ToolTip();
            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {
                pc.Image = inventory.Properties.Resources.invalid;
                n.SetToolTip(pc, s);
                c = -99;
            }
            else
            {
                pc.Image = null;
                c = 0;
            }
        }
        public void Regexp4(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {
            ToolTip buttonToolTip = new ToolTip();
            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {
                pc.Image = inventory.Properties.Resources.invalid;
                n.SetToolTip(pc, s);
                d = -99;
            }
            else
            {
                pc.Image = null;
                d = 0;
            }
        }

        public void Regexp5(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {
            ToolTip buttonToolTip = new ToolTip();
            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {
                pc.Image = inventory.Properties.Resources.invalid;
                n.SetToolTip(pc, s);
                h = -99;
            }
            else
            {
                pc.Image = null;
                h = 0;
            }
        }
        private void bunifuThinButton28_Click(object sender, EventArgs e)
        {
            Regexp1(@"^[0-9]{9}[vVxX]$", txtt1nic, picnic, "Please enter a valid nic");
            Regexp2(@"^([\w]+)@([\w]+)\.([\w]+)$", txtt1email, picemail, "Please enter a valid email address");
            Regexp3(@"^[0-9]{10}$", txtt1mobileno1, picmobno1, "phone no should only 10digit and numeric");
            Regexp4(@"^[0-9]{10}$", txtt1mobileno2, picmobno2, "phone no should only 10digit and numeric");
            Regexp5(@"^[0-9]{10}$", txtt1landno, picland, "phone no should only 10digit and numeric");

            if (String.IsNullOrEmpty(txtt1address.Text) || String.IsNullOrWhiteSpace(txtt1email.Text) || String.IsNullOrWhiteSpace(txtt1landno.Text) || String.IsNullOrWhiteSpace(txtt1mobileno1.Text) || String.IsNullOrWhiteSpace(txtt1mobileno2.Text) || String.IsNullOrWhiteSpace(txtt1name.Text) || String.IsNullOrWhiteSpace(txtt1nic.Text))
                {
                    MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (picPath == null)
                {
                    MessageBox.Show("Image is Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(a==-99|| b == -99|| c == -99|| d == -99|| h==-99)
            {

                
            }

            else if(a ==0 && b ==0 && c == 0&& d == 0 && h ==0)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to add the item?", "Add item", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    byte[] imageBt = null;
                    try
                    {

                        FileStream fstream = new FileStream(picPath, FileMode.Open, FileAccess.Read);

                        BinaryReader br = new BinaryReader(fstream);
                        imageBt = br.ReadBytes((int)fstream.Length);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }



                    myConn.Open();
                    MySqlCommand cm = new MySqlCommand("INSERT INTO supermarket.customerrepair(`nic`, `name`, `address`, `phoneNo1`, `phoneNo2`, `landNo`, `email`, `image`) VALUES ('" + txtt1nic.Text + "','" + txtt1name.Text + "','" + txtt1address.Text + "','" + txtt1mobileno1.Text + "','" + txtt1mobileno2.Text + "','" + txtt1landno.Text + "','" + txtt1email.Text + "','" + imageBt + "')", myConn);
                    cm.ExecuteNonQuery();
                    myConn.Close();
                    MessageBox.Show("Data Inserted Successfully!");
                }
               
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show("Not Added!");
                    }

                

            }
        }

        private void custitemdatagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string itemcode;
            itemcode = datagridcustitem.SelectedCells[0].Value.ToString();
            string itemname = datagridcustitem.SelectedCells[1].Value.ToString();
            txtt1itemcode.Text = itemcode;
            txtt1itemname.Text = itemname;



            try
            {

                myConn.Open();
                MySqlDataAdapter cmd = new MySqlDataAdapter("select * from supermarket.stock  where Item_code  = '" + itemcode + "%'", myConn);
                DataTable dt = new DataTable();
                cmd.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    txtt1suppliername.Text = dr["Sname"].ToString();


                }

                MySqlDataAdapter cmd1 = new MySqlDataAdapter("select Brand from supermarket.item  where  Item_code= '" + itemcode + "%'", myConn);
                DataTable dt1 = new DataTable();
                cmd1.Fill(dt1);

                foreach (DataRow dr in dt1.Rows)
                {

                    txtt1brandname.Text = dr["Brand"].ToString();


                }
                myConn.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuCustomLabel16_Click_1(object sender, EventArgs e)
        {

        }

        public bool checkradio()
        {
            bool value = true;

            if (radiot1Button1.Checked == true)
            {
                value = true;
                return value;
            }
            else if (radiot1Button2.Checked == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void bunifuThinButton21_Click_1(object sender, EventArgs e)
        {
            if (checkradio() == false || String.IsNullOrEmpty(txtt1itemcode.Text) || String.IsNullOrWhiteSpace(txtt1brandname.Text) || String.IsNullOrWhiteSpace(txtt1noofdays.Text) || String.IsNullOrWhiteSpace(txtt1itemname.Text) || String.IsNullOrWhiteSpace(txtt1suppliername.Text) || String.IsNullOrWhiteSpace(txtt1advance.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to add the item?", "Add item", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    string date = DateTime.Now.ToString("M/d/yyyy");




                    string warrenty;
                    if (radiot1Button1.Checked == true)
                    {
                        warrenty = "yes";
                    }
                    else
                    {
                        warrenty = "no";
                    }

                    myConn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO supermarket.needtosendtemp (`id`,date,`cust_id`, `item_code`, `item_name`, `brand_name`, `warranty`, `repairing_days`, `details`, `suuplier_name`, `salesman_name`,`Advance`) VALUES(null,'" + date + "','" + txtt1nic.Text + "','" + txtt1itemcode.Text + "', '" + txtt1itemname.Text + "', '" + txtt1brandname.Text + "', '" + warrenty + "', '" + txtt1noofdays.Value + "', '" + txtt1detail.Text + "', '" + txtt1suppliername.Text + "', '" + cbt1employee.Text + "','" + txtt1advance.Text + "')", myConn);
                    MySqlDataReader myR;


                    myR = cmd.ExecuteReader();
                    MessageBox.Show("Added Successfully!");
                    while (myR.Read()) { }

                    myConn.Close();

                    loaddatagridneedtosendtemp();

                }
                else
                {
                    MessageBox.Show("Not Addded!");

                }
            }
        }
        private void bunifuCards2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void datagridreciveditems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string i;
            i = (datagridneedtosendtemp.SelectedCells[0].Value.ToString());
            id = (datagridneedtosendtemp.SelectedCells[0].Value.ToString());
            myConn.Open();

            MySqlDataAdapter cmd = new MySqlDataAdapter("select * from supermarket.needtosendtemp  where  id= " + i + "", myConn);
            DataTable dt = new DataTable();
            cmd.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {

                txtt1itemcode.Text = dr["item_code"].ToString();
                txtt1itemname.Text = dr["item_name"].ToString();
                txtt1brandname.Text = dr["brand_name"].ToString();

                String warranty = dr["warranty"].ToString();
                //String warranty_end = dr["warranty_end"].ToString();
                //string warranty_start = dr["warranty_end"].ToString();
                txtt1detail.Text = dr["details"].ToString();

                txtt1suppliername.Text = dr["suuplier_name"].ToString();
                // cbemployee.selectedValue = dr["salesman_name"].ToString();

            }

            myConn.Close();
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {


            try
            {
                myConn.Open();
                MySqlDataAdapter cmd = new MySqlDataAdapter("select * from supermarket.needtosendoriginal  where  id= " + i + "", myConn);

                DataTable dt = new DataTable();
                cmd.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    string id = dr["id"].ToString();
                    string itemcode = dr["item_code"].ToString();
                    string date = dr["date"].ToString();
                    string custid = dr["cust_id"].ToString();
                    string itemname = dr["item_name"].ToString();
                    string brandname = dr["brand_name"].ToString();

                    string warranty = dr["warranty"].ToString();
                    //String warranty_end = dr["warranty_end"].ToString();
                    //string warranty_start = dr["warranty_end"].ToString();
                    string details = dr["details"].ToString();
                    string Advance = dr["Advance"].ToString();
                    string suppliername = dr["suuplier_name"].ToString();
                    string repairing_days = dr["repairing_days"].ToString();
                    string details1 = dr["details"].ToString();
                    string salesman_name = dr["salesman_name"].ToString();


                    MySqlCommand cmd1 = new MySqlCommand("INSERT INTO supermarket.needtosend (`id`,`date`,`cust_id`, `item_code`, `item_name`, `brand_name`, `warranty`, `repairing_days`, `details`, `suuplier_name`, `salesman_name`,`Advance`) VALUES ('" + id + "','" + date + "','" + custid + "', '" + itemcode + "', '" + itemname + "', '" + brandname + "', '" + warranty + "', '" + repairing_days + "', '" + details1 + "', '" + suppliername + "', '" + salesman_name + "','" + Advance + "')", myConn);
                    cmd1.ExecuteNonQuery();


            
            MySqlCommand cmd2 = new MySqlCommand("UPDATE supermarket.needtosendoriginal SET status='send to' WHERE id='" + i + "'", myConn);
            cmd2.ExecuteNonQuery();

            MySqlCommand cmd3 = new MySqlCommand("UPDATE supermarket.needtosend SET `status`='send to' WHERE id='" + i + "'", myConn);
            cmd3.ExecuteNonQuery();

            myConn.Close();

            loadpanel2sendtooriginal();
            loadneedtosend();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void bunifuCustomDataGrid4_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            i = (datagridpanel2sendtooriginal.SelectedCells[0].Value.ToString());
           
            btnright.Show();
        }
        public void loadcustItem()
        {
            string nic = txtt1nic.Text;
            //string query1 = "select p.iname,p.icode from supermarket.permanentorder p,supermarket.instcust in,supermarket.customer c where p.orderid=in.Invoice_No and c.nic=in.CustID and in.CustID='" + nic+ "' ";
            //string query1 = "select i.warrenty,i.date,p.iname,p.icode from supermarket.installments i,supermarket.permanentorder p,supermarket.instcust in,supermarket.customer c where i.orderid=p.orderid and p.orderid=in.Invoice_No and c.nic=in.CustID and c.nic='"+nic+"' ";
            string query1 = "select p.icode,p.iname,i.Date_Due,i.Installments as Warrenty_Years from supermarket.instcust i,supermarket.permanentorder p where p.orderid=i.Invoice_No and i.CustID='" + txtt1nic.Text + "' ";
            MySqlDataAdapter cmd = new MySqlDataAdapter(query1, myConn);
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            datagridcustitem.DataSource = dt;

        }
        public void loadtableFullSpareParts()
        {
            string query = "select item_code,Item_name,Brand,Rprice as Price,Description,image from supermarket.item where Category='Spare Parts'";
            MySqlDataAdapter cmd = new MySqlDataAdapter(query, myConn);
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            tableFullSpareParts.DataSource = dt;

        }
        public void loaddatagridneedtosendtemp()
        {

            string query = "select id as ID ,item_code as 'Item Code', item_name as 'Item Name',brand_name as 'Brand Name'  from supermarket.needtosendtemp";
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, myConn);
            DataTable dt1 = new DataTable();
            cmd1.Fill(dt1);
            datagridneedtosendtemp.DataSource = dt1;

        }

        public void loadpanel2sendtooriginal()
        {

            string query = "select id as ID,Date,item_code as 'Item Code',warranty as 'Warranty' ,suuplier_name as 'Supplier' from supermarket.needtosendoriginal where status='pending'";
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, myConn);
            DataTable dt1 = new DataTable();
            cmd1.Fill(dt1);
            datagridpanel2sendtooriginal.DataSource = dt1;

        }


        public void loadneedtosend()
        {
            string query = "select id as ID,Date,item_code as 'Item Code',warranty as 'Warranty' ,suuplier_name as 'Supplier'  from supermarket.needtosend where status='send to'";
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, myConn);
            DataTable dt1 = new DataTable();
            cmd1.Fill(dt1);
            datagridpanel2sendto.DataSource = dt1;

        }

        public void loadsendwith()
        {
            string query = "SELECT `id`, `cust_id`, `cus_date`, `sent_date`, `item_code`, `item_name`, `brand_name`, `warranty`, `repairing_days`, `suuplier_name`, `salesman_name`, `sendwith`,`Advance` from supermarket.sendwith";
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, myConn);
            DataTable dt1 = new DataTable();
            cmd1.Fill(dt1);
            bunifuCustomDataGrid5.DataSource = dt1;
            tablepanel3statuschanging.DataSource = dt1;
        }

        public void loadsendmessage()
        {
            string query = "select * from supermarket.sendmessage";
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, myConn);
            DataTable dt1 = new DataTable();
            cmd1.Fill(dt1);
            tablet3messagesend.DataSource = dt1;

        }

        public void loadtableselecteditem()
        {
            string query = "select  item_id, name, price, qty,amount from supermarket.selected_spartitem";
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, myConn);
            DataTable dt1 = new DataTable();
            cmd1.Fill(dt1);
            tableselecteditem.DataSource = dt1;

        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {

            if ((datagridpanel2sendto.Rows.Count!=1))
            { 
                DialogResult dialogResult = MessageBox.Show("Do you want to add the items?", "Add item", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                myConn.Open();
                MySqlDataAdapter cmd = new MySqlDataAdapter("select * from supermarket.needtosend", myConn);

                DataTable dt = new DataTable();
                cmd.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    string id = dr["id"].ToString();
                    string custid = dr["cust_id"].ToString();
                    string cus_date = dr["Date"].ToString();

                    string itemcode = dr["item_code"].ToString();
                    string itemname = dr["item_name"].ToString();
                    string brandname = dr["brand_name"].ToString();

                    string warranty = dr["warranty"].ToString();
                    //String warranty_end = dr["warranty_end"].ToString();
                    //string warranty_start = dr["warranty_end"].ToString();
                    string details = dr["details"].ToString();

                    string suppliername = dr["suuplier_name"].ToString();
                    string repairing_days = dr["repairing_days"].ToString();
                    string details1 = dr["details"].ToString();
                    string salesman_name = dr["salesman_name"].ToString();
                    string Advance = dr["Advance"].ToString();

                    MySqlCommand cmd1 = new MySqlCommand("INSERT INTO supermarket.sendwith ( `cust_id`,`cus_date`,`sent_date`,`item_code`, `item_name`, `brand_name`, `warranty`, `repairing_days`, `details`, `suuplier_name`, `salesman_name`,`sendwith`,`status`,`Advance`) VALUES ( '" + custid + "','" + cus_date + "','" + txtt2sentdate.Text + "','" + itemcode + "', '" + itemname + "', '" + brandname + "', '" + warranty + "', '" + repairing_days + "', '" + details1 + "', '" + suppliername + "', '" + salesman_name + "','" + cbt2employee.Text + "','pending','" + Advance + "')", myConn);
                    cmd1.ExecuteNonQuery();


                }
                MySqlCommand cmd2 = new MySqlCommand("delete from supermarket.needtosend", myConn);
                cmd2.ExecuteNonQuery();
                myConn.Close();
                loadneedtosend();
                loadsendwith();
            }
            else
            {
                MessageBox.Show("Not Addded!");

            }
        }
            else
            {
                MessageBox.Show("Table cannot Be Empty! ");
            }
        }

        private void bunifuThinButton216_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to complete?", "Complete Adding", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                myConn.Open();
                MySqlDataAdapter cmd = new MySqlDataAdapter("select * from supermarket.needtosendtemp", myConn);

                DataTable dt = new DataTable();
                cmd.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    string id = dr["id"].ToString();
                    string itemcode = dr["item_code"].ToString();
                    string date = dr["date"].ToString();
                    string cust_id = dr["cust_id"].ToString();
                    string itemname = dr["item_name"].ToString();
                    string brandname = dr["brand_name"].ToString();

                    string warranty = dr["warranty"].ToString();

                    //String warranty_end = dr["warranty_end"].ToString();
                    //string warranty_start = dr["warranty_end"].ToString();
                    string details = dr["details"].ToString();

                    string suppliername = dr["suuplier_name"].ToString();
                    string repairing_days = dr["repairing_days"].ToString();
                    string details1 = dr["details"].ToString();
                    string salesman_name = dr["salesman_name"].ToString();
                    string Advance = dr["Advance"].ToString();


                    MySqlCommand cmd1 = new MySqlCommand("INSERT INTO supermarket.needtosendoriginal ( `Date`,`cust_id`,`item_code`, `item_name`, `brand_name`, `warranty`, `repairing_days`, `details`, `suuplier_name`, `salesman_name`,`status`,`Advance`) VALUES ('" + date + "','" + cust_id + "','" + itemcode + "', '" + itemname + "', '" + brandname + "', '" + warranty + "', '" + repairing_days + "', '" + details1 + "', '" + suppliername + "', '" + salesman_name + "','pending','" + Advance + "')", myConn);
                    cmd1.ExecuteNonQuery();
                   

                }
                MySqlCommand cmd2 = new MySqlCommand("delete from supermarket.needtosendtemp", myConn);
                cmd2.ExecuteNonQuery();
                myConn.Close();
                loaddatagridneedtosendtemp();
                loadpanel2sendtooriginal();
            }
        }

        private void bunifuThinButton210_Click(object sender, EventArgs e)
        {

            string i;
            i = (bunifuCustomDataGrid5.SelectedCells[0].Value.ToString());
            DialogResult dialogResult = MessageBox.Show("Do you want to update this item?", "Update item", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {





                myConn.Open();
                MySqlCommand cmd1 = new MySqlCommand("UPDATE supermarket.sendwith SET `sendwith`='" + cbt2employee.Text + "',`sent_date`='" + txtt2sentdate.Text + "' WHERE id='" + i + "'", myConn);
                cmd1.ExecuteNonQuery();

                MessageBox.Show("Updated successfully!!");
                myConn.Close();
                loadsendwith();



            }
            else
            {
               
            
                    MessageBox.Show("Not Updated!");

            }
        }

        private void bunifuThinButton22_Click_1(object sender, EventArgs e)
        {

            if (checkradio() == false || String.IsNullOrEmpty(txtt1itemcode.Text) || String.IsNullOrWhiteSpace(txtt1brandname.Text) || String.IsNullOrWhiteSpace(txtt1noofdays.Text) || String.IsNullOrWhiteSpace(txtt1itemname.Text) || String.IsNullOrWhiteSpace(txtt1suppliername.Text) || String.IsNullOrWhiteSpace(txtt1advance.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to update this item?", "Update item", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string warrenty;
                    if (radiot1Button1.Checked == true)
                    {
                        warrenty = "yes";
                    }
                    else
                    {
                        warrenty = "no";
                    }
                    myConn.Open();
                    MySqlCommand cmd1 = new MySqlCommand("UPDATE supermarket.needtosendtemp SET `item_code`='" + txtt1itemcode.Text + "',`item_name`='" + txtt1itemname.Text + "',`brand_name`='" + txtt1brandname.Text + "',`warranty`='" + warrenty + "',`repairing_days`='" + txtt1noofdays.Value + "',`details`='" + txtt1detail.Text + "',`suuplier_name`='" + txtt1suppliername.Text + "',`salesman_name`='" + cbt2employee.SelectedItem + "' WHERE id='" + id + "'", myConn);
                    cmd1.ExecuteNonQuery();

                    MessageBox.Show("Updated successfully!!");
                    myConn.Close();
                    loaddatagridneedtosendtemp();
                }
                else
                {
                    MessageBox.Show("Update Cancelled");
                }
            }
        }
        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Do you want to Delete the item?", "Delete item", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                myConn.Open();
                MySqlCommand cmd1 = new MySqlCommand("DELETE FROM supermarket.needtosendtemp WHERE id='" + id + "'", myConn);
                cmd1.ExecuteNonQuery();

                MessageBox.Show("one record Deleted successfully!!");
                myConn.Close();
                loaddatagridneedtosendtemp();
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Not Deleted!");
            }
        }

        private void bunifuThinButton217_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                picPath = dlg.FileName.ToString();
                //imgpath.Text = picPath;
                t1pic.ImageLocation = picPath;
                t1pic.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void bunifuThinButton29_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtt1nic.Text))
            {
                MessageBox.Show("NIC Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to Delete the customer?", "Delete customer", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    myConn.Open();
                    MySqlCommand cmd1 = new MySqlCommand("DELETE FROM supermarket. WHERE nic='" + txtt1nic.Text + "'", myConn);
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show(id.ToString());
                    MessageBox.Show("one record Deleted successfully!!");
                    myConn.Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Not Deleted!");
                }
            }
        }

        private void bunifuThinButton27_Click_1(object sender, EventArgs e)
        {
            Regexp1(@"^[0-9]{9}[vVxX]$", txtt1nic, picnic, "Please enter a valid nic");
            Regexp2(@"^([\w]+)@([\w]+)\.([\w]+)$", txtt1email, picemail, "Please enter a valid email address");
            Regexp3(@"^[0-9]{10}$", txtt1mobileno1, picmobno1, "phone no should only 10digit and numeric");
            Regexp4(@"^[0-9]{10}$", txtt1mobileno2, picmobno2, "phone no should only 10digit and numeric");
            Regexp5(@"^[0-9]{10}$", txtt1landno, picland, "phone no should only 10digit and numeric");
            DialogResult dialogResult = MessageBox.Show("Do you want to update this customer?", "Update customer", MessageBoxButtons.YesNo);
            if (String.IsNullOrEmpty(txtt1address.Text) || String.IsNullOrWhiteSpace(txtt1email.Text) || String.IsNullOrWhiteSpace(txtt1landno.Text) || String.IsNullOrWhiteSpace(txtt1mobileno1.Text) || String.IsNullOrWhiteSpace(txtt1mobileno2.Text) || String.IsNullOrWhiteSpace(txtt1name.Text) || String.IsNullOrWhiteSpace(txtt1nic.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (dialogResult == DialogResult.Yes)
                {

                    if (String.IsNullOrEmpty(txtt1address.Text) || String.IsNullOrWhiteSpace(txtt1email.Text) || String.IsNullOrWhiteSpace(txtt1landno.Text) || String.IsNullOrWhiteSpace(txtt1mobileno1.Text) || String.IsNullOrWhiteSpace(txtt1mobileno2.Text) || String.IsNullOrWhiteSpace(txtt1name.Text) || String.IsNullOrWhiteSpace(txtt1nic.Text))
                    {
                        MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (picPath == null)
                    {
                        MessageBox.Show("Image is Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    else
                    {


                        byte[] imageBt = null;
                        try
                        {

                            FileStream fstream = new FileStream(picPath, FileMode.Open, FileAccess.Read);

                            BinaryReader br = new BinaryReader(fstream);
                            imageBt = br.ReadBytes((int)fstream.Length);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        myConn.Open();
                        MySqlCommand cmd1 = new MySqlCommand("UPDATE supermarket. SET `name`='" + txtt1name.Text + "',`address`='" + txtt1address.Text + "',`phoneNo1`='" + txtt1mobileno1.Text + "',`phoneNo2`='" + txtt1mobileno2.Text + "',`landNo`='" + txtt1landno.Text + "',`email`='" + txtt1email.Text + "',`image`='" + imageBt + "' WHeERE nic='" + txtt1nic.Text + "'", myConn);
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show(id.ToString());
                        MessageBox.Show("Updated successfully!!");
                        myConn.Close();

                    }
                }
                else
                {
                    MessageBox.Show("Customer Update Cancelled");
                }

            }
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtsuppliername_KeyUp(object sender, KeyEventArgs e)
        {
            listBoxt11.Items.Clear();
            string query = "select * from supermarket.vendor  where name like '%" + txtt1suppliername.Text + "%'";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, myConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);


            listBoxt11.Visible = true;
            foreach (DataRow item in dt.Rows)
            {
                listBoxt11.Items.Add(item["name"].ToString());

            }


        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            txtt1suppliername.Text = listBoxt11.SelectedItem.ToString();
            listBoxt11.Visible = false;
        }

        private void txtsuppliername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                listBoxt11.Focus();
                listBoxt11.SelectedIndex = 0;
            }
        }

        private void bunifuThinButton219_Click(object sender, EventArgs e)
        {

            myConn.Open();

            MySqlCommand cmd = new MySqlCommand("update supermarket.needtosendoriginal set status='pending' WHERE id='" + i + "'", myConn);
            cmd.ExecuteNonQuery();

            MySqlCommand cmd1 = new MySqlCommand("DELETE FROM supermarket.needtosend WHERE id='" + i + "'", myConn);
            cmd1.ExecuteNonQuery();
            myConn.Close();

            loadneedtosend();
            loadpanel2sendtooriginal();
        }

        private void datagridpanel2sendto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void datagridpanel2sendto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            i = (datagridpanel2sendto.SelectedCells[0].Value.ToString());
            btnleft.Show();

        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            if (!(txtt3search.Text.Length == 0))
            {

                int typeno = cbt3searchby.SelectedIndex;
                String type = "";
                String key = txtt3search.Text;


                switch (typeno)
                {
                    case 0:
                        type = "";
                        break;

                    case 1:
                        type = "id";
                        break;

                    case 2:
                        type = "item_name";
                        break;
                    case 3:
                        type = "brand_name";
                        break;



                }


                try
                {
                    MySqlDataAdapter cm = new MySqlDataAdapter("Select *  from supermarket.sendwith where " + type + " like '" + key + "%'", myConn);
                    DataTable set = new DataTable();
                    cm.Fill(set);


                    tablepanel3statuschanging.DataSource = set;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (txtt3search.Text == "")
            {
                loadsendwith();
            }
        }

        private void tablepanel3statuschanging_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cust_id = (tablepanel3statuschanging.SelectedCells[1].Value.ToString());


            MySqlDataAdapter cm = new MySqlDataAdapter("Select *  from supermarket.customer where  nic= '" + cust_id + "'", myConn);
            DataTable set = new DataTable();
            cm.Fill(set);

            foreach (DataRow dr in set.Rows)
            {

                cname = dr["fullname"].ToString();
                string cphoneNo1 = dr["phone"].ToString();
                string cemail = dr["email"].ToString();
                string cphoneNo2 = dr["phone"].ToString();


                txtt3toemail.Text = cemail;
                txtt3mobileno.Text = cphoneNo1;


            }

            MySqlDataAdapter cm1 = new MySqlDataAdapter("Select *  from supermarket.sendwith where  cust_id= '" + cust_id + "'", myConn);
            DataTable set1 = new DataTable();
            cm1.Fill(set1);

            foreach (DataRow dr1 in set1.Rows)
            {

                string itemname = dr1["item_name"].ToString();
                string itemcode = dr1["item_code"].ToString();



                txtt3emailbody.Text = "Dear valued customer this is to inform that " + itemname + " you bought has suucessfully repaired.please come and collect item.Thankyou.Madhusha supermarket ";
                txtt3messagebody.Text = "Dear valued  this is to inform that " + itemname + " you bought has suucessfully repaired.please come and collect item.Thankyou.Madhusha supermarket ";

            }

        }

        private void bunifuThinButton211_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to add the item?", "Add item", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                itemcode = (tablepanel3statuschanging.SelectedCells[4].Value.ToString());
                item_name = (tablepanel3statuschanging.SelectedCells[5].Value.ToString());
                myConn.Open();
                string status = "";
                string sendby = "";
                if (radiot3call.Checked == true)
                {
                    sendby = "call";
                }
                else if (radiot3message.Checked == true)
                {
                    sendby = "message";
                }


                if (radior3pending.Checked == true)
                {
                    status = "pending";
                }
                else if (radiot3repaired.Checked == true)
                {
                    status = "repaired";
                }


                string date = DateTime.Now.ToString("M/d/yyyy");
                MySqlCommand cmd1 = new MySqlCommand("INSERT INTO supermarket.sendmessage(`id`, `date`, `customer_id`, `customer_name`, `item_code`, `item_name`, `emaployee`, `cost`, `note`, `send_by`, `status`) VALUES (null,'" + date + "','" + cust_id + "','" + cname + "','" + itemcode + "','" + item_name + "','" + txtt3smtp.Text + "','" + txtt3cost.Text + "','" + txtt3note.Text + "','" + sendby + "','" + status + "')", myConn);
                cmd1.ExecuteNonQuery();
                MessageBox.Show("yes");
                loadsendmessage();
                myConn.Close();
            }
            else
            {
                MessageBox.Show("Not Addded!");

            }
        }
        private void tableFullSpareParts_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            item_id = (tableFullSpareParts.SelectedCells[0].Value.ToString());
           

            item_name = (tableFullSpareParts.SelectedCells[1].Value.ToString());

            string item_price = (tableFullSpareParts.SelectedCells[3].Value.ToString());

            txtt4ItemName.Text = item_name;
            txtt4price.Text = item_price;

        }

        private void bunifuThinButton212_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to add the item?", "Add item", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                decimal price = Convert.ToDecimal(txtt4price.Text);
                decimal qty = Convert.ToDecimal(txtt4qty.Text);
                decimal tot = price * qty;
                myConn.Open();
                MySqlCommand cmd1 = new MySqlCommand("INSERT INTO supermarket.selected_spartitem(`id`, `item_id`, `name`, `price`, `qty`,`amount`) VALUES (null,'" + item_id + "','" + txtt4ItemName.Text + "','" + txtt4price.Text + "','" + txtt4qty.Value + "','" + tot + "')", myConn);
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Added Successfully");
                loadtableselecteditem();


                string query = "select sum(amount) as tot from supermarket.selected_spartitem";
                MySqlCommand cmd = new MySqlCommand(query, myConn);
                cmd.ExecuteNonQuery();

                MySqlDataReader myR;


                myR = cmd.ExecuteReader();

                if (myR.Read())
                {

                    txtt4saprepartscharge.Text = myR["tot"].ToString();


                }

                myConn.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Not Added!");
            }
        }

        private void bunifuThinButton215_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to Update the item?", "Update item", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                decimal price = Convert.ToDecimal(txtt4price.Text);
                decimal qty = Convert.ToDecimal(txtt4qty.Text);
                decimal tot = price * qty;

                myConn.Open();
                MySqlCommand cmd1 = new MySqlCommand("UPDATE supermarket.selected_spartitem SET `price`='" + price + "',`qty`='" + qty + "',amount='" + tot + "' WHERE item_id='" + itemcode + "'", myConn);
                cmd1.ExecuteNonQuery();

                MessageBox.Show("Updated Successfully");

                loadtableselecteditem();
                myConn.Close();
                totset();
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Not Updated!");
            }
        }

        private void tableselecteditem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            itemcode = (tableselecteditem.SelectedCells[0].Value.ToString());
            string item_name = (tableselecteditem.SelectedCells[1].Value.ToString());


            string item_price = (tableselecteditem.SelectedCells[2].Value.ToString());

            string qty = (tableselecteditem.SelectedCells[3].Value.ToString());

            txtt4ItemName.Text = item_name;
            txtt4price.Text = item_price;
            txtt4qty.Text = qty;

        }

        private void bunifuThinButton214_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to delete the item?", "Delete item", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                myConn.Open();
                MySqlCommand cmd1 = new MySqlCommand("DELETE FROM supermarket.selected_spartitem  WHERE item_id='" + itemcode + "'", myConn);
                cmd1.ExecuteNonQuery();


                MessageBox.Show("Deleted  Successfully");




                myConn.Close();
                totset();

                loadtableselecteditem();
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Not Deleted!");
            }
        }

        public void totset()
        {
            myConn.Open();
            string query = "select sum(amount) as tot from supermarket.selected_spartitem";
            MySqlCommand cmd = new MySqlCommand(query, myConn);
            cmd.ExecuteNonQuery();

            MySqlDataReader myR;


            myR = cmd.ExecuteReader();

            if (myR.Read())
            {

                txtt4saprepartscharge.Text = myR["tot"].ToString();


            }
            myConn.Close();
        }

        private void bunifuThinButton218_Click(object sender, EventArgs e)
        {

            string price = txtt4price.Text;
            string qty = txtt4qty.Value.ToString();
            myConn.Open();

            string date = DateTime.Now.ToString("M/d/yyyy");

            MySqlCommand cmd1 = new MySqlCommand("INSERT INTO supermarket.payment(`invoice_id`, `Repair_id`, `item_code`, `cust_id`, `Date`, `service charge`, `spareparts_charge`, `Total`, `advance`, `balance`, `discount`, `grandtotal`, `payment_mode`) VALUES(null,'" + txtt4Rid.Text + "','" + txtt4itemcode.Text + "','" + txtt4cusID.Text + "','" + date + "','" + txtt4serviceCharge.Text + "','" + txtt4saprepartscharge.Text + "','" + txtt4total.Text + "','" + txtt4advance.Text + "','" + txtt4balance.Text + "','" + txtt4discount.Text + "','" + txtt4grandtotal.Text + "','" + txtt4pay_mod.SelectedItem + "')", myConn);
            cmd1.ExecuteNonQuery();

            MessageBox.Show("inserted Successfully");


            myConn.Close();

        }

        private void bunifuThinButton213_Click(object sender, EventArgs e)
        {

        }

        private void radiocall_CheckedChanged(object sender, EventArgs e)
        {
            btnadd.Show();
        }

        private void radiomessage_CheckedChanged(object sender, EventArgs e)
        {
            btnadd.Show();
        }

        private void radioemail_CheckedChanged(object sender, EventArgs e)
        {
            btnadd.Show();
        }



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtsuppliername_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void cbt3employee_onItemSelected(object sender, EventArgs e)
        {

        }

        private void cbt1employee_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbt1employee.Items.Clear();
            //try
            //{
            //myConn.Open();
            //MySqlCommand cm = new MySqlCommand("SELECT * FROM supermarket.employee_details ", myConn);
            //MySqlDataReader r;


            //    r = cm.ExecuteReader();

            //    while (r.Read())
            //    {
            //        string cat = r.GetString("i_name");

            //        cbt1employee.Items.Add(cat);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        public void BindData1()
        {

            cbt1employee.Text = "Select Employee";
            myConn.Open();
            string strCmd = "select * from supermarket.employee_details";
            MySqlCommand cmd = new MySqlCommand(strCmd, myConn);
            MySqlDataAdapter da = new MySqlDataAdapter(strCmd, myConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteNonQuery();
            myConn.Close();

            cbt1employee.DisplayMember = "i_name";
            cbt1employee.ValueMember = "i_name";
            cbt1employee.DataSource = ds.Tables[0];

            cbt2employee.DisplayMember = "i_name";
            cbt2employee.ValueMember = "i_name";
            cbt2employee.DataSource = ds.Tables[0];

            txtt3smtp.DisplayMember = "i_name";
            txtt3smtp.ValueMember = "i_name";
            txtt3smtp.DataSource = ds.Tables[0];



        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtt1suppliername.Text = listBoxt11.SelectedItem.ToString();
                listBoxt11.Visible = false;
            }
        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtnic_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtnic_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtnic_OnValueChanged(object sender, EventArgs e)
        {


            MySqlDataAdapter cm = new MySqlDataAdapter("Select *  from supermarket.customer where  nic like '" + txtt1nic.Text + "'", myConn);
            DataTable set = new DataTable();
            cm.Fill(set);

            foreach (DataRow dr in set.Rows)
            {

                cname = dr["fullname"].ToString();
                string cphoneNo1 = dr["phone"].ToString();
                string cemail = dr["email"].ToString();
                string cphoneNo2 = dr["phone"].ToString();
                string address1 = dr["address"].ToString();
                string phone = dr["phone"].ToString();


                txtt1email.Text = cemail;
                txtt1mobileno1.Text = cphoneNo1;
                txtt1mobileno2.Text = cphoneNo1;
                txtt1address.Text = address1;
                txtt1name.Text = cname;
                txtt1landno.Text = phone;

                byte[] images = ((byte[])dr[18]);



                if (images == null)
                {
                    t1pic.Image = null;
                }

                else
                {
                    MemoryStream mstream = new MemoryStream(images);
                    t1pic.Image = System.Drawing.Image.FromStream(mstream);
                }

            }
            loadcustItem();

        }

        private void groupBox13_Enter(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton27_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to Clear ?", "Clear data", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                txtt1nic.Text = "";
                txtt1name.Text = "";
                txtt1mobileno1.Text = "";
                txtt1mobileno2.Text = " ";
                txtt1landno.Text = " ";
                txtt1address.Text = " ";
                txtt1email.Text = " ";
                t1pic.Image = null;
            }


        }

        private void txtname_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton211_Click_1(object sender, EventArgs e)
        {
            txtt1nic.Text = "";
            txtt4Rid.Text = "";
            txtt4itemcode.Text = "";
            txtt4cusID.Text = "";
            txtt4serviceCharge.Text = "";
            txtt4saprepartscharge.Text = "";
            txtt4total.Text = "";
            txtt4advance.Text = "";
            txtt4balance.Text = "";
            txtt4discount.Text = "";
            txtt4grandtotal.Text = "";
            txtt4pay_mod.Text = "";


        }

        private void radiotry_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton213_Click_1(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton220_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to Clear ?", "Clear data", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                txtt1itemcode.Text = "";
                txtt1brandname.Text = "";
                cbt1employee.Text = "";
                txtt1noofdays.Text = " ";
                txtt1advance.Text = " ";
                txtt1itemname.Text = " ";
                radiot1Button1.Text = " ";
                radiot1Button2.Text = " ";
                txtt1detail.Text = " ";
                txtt1suppliername.Text = " ";
                listBoxt11.Visible = false;
            }

        }


        private void txtt1suppliername_Leave(object sender, EventArgs e)
        {

        }

        private void groupBox2_MouseCaptureChanged(object sender, EventArgs e)
        {
            listBoxt11.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@"eItemList.pdf", FileMode.Create));
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
            prg.Add(new Chunk("Item List", font5));
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

            PdfPTable table = new PdfPTable(bunifuCustomDataGrid5.Columns.Count);

            //add headers from gridview to table
            iTextSharp.text.Font fonth = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);



            for (int j = 0; j < bunifuCustomDataGrid5.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(bunifuCustomDataGrid5.Columns[j].HeaderText.ToUpper(), fonth));
                table.AddCell(cell);

            }

            //flag first row as header
            table.HeaderRows = 1;


            //add actual rows from grid to table
            for (int i = 0; i < bunifuCustomDataGrid5.Rows.Count-4; i++)
            {
                table.WidthPercentage = 100;

                for (int k = 0; k < bunifuCustomDataGrid5.Columns.Count; k++)
                {
                    if (bunifuCustomDataGrid5[k, i].Value != null)
                    {

                        table.AddCell(new Phrase(bunifuCustomDataGrid5[k, i].Value.ToString()));
                    }

                }


            }

            //add out table
            doc.Add(table);

            doc.Close();

            System.Diagnostics.Process.Start(@"eItemList.pdf");

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (!(txtt3search2.Text.Length == 0))
            {

                int typeno = cbt3searchby2.SelectedIndex;
                String type = "";
                String key = txtt3search2.Text;


                switch (typeno)
                {
                    case 0:
                        type = "";
                        break;

                    case 1:
                        type = "id";
                        break;

                    case 2:
                        type = "item_name";
                        break;
                    case 3:
                        type = "brand_name";
                        break;



                }


                try
                {
                    MySqlDataAdapter cm = new MySqlDataAdapter("Select *  from supermarket.	sendmessage where " + type + " like '" + key + "%'", myConn);
                    DataTable set = new DataTable();
                    cm.Fill(set);


                    tablet3messagesend.DataSource = set;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\Ruchira Dhananjaya\Desktop\ItemList.pdf", FileMode.Create));
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
            prg.Add(new Chunk("Item List", font5));
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

            PdfPTable table = new PdfPTable(tablet3messagesend.Columns.Count);

            //add headers from gridview to table
            iTextSharp.text.Font fonth = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);



            for (int j = 0; j < tablet3messagesend.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(tablet3messagesend.Columns[j].HeaderText.ToUpper(), fonth));
                table.AddCell(cell);

            }

            //flag first row as header
            table.HeaderRows = 1;


            //add actual rows from grid to table
            for (int i = 0; i < tablet3messagesend.Rows.Count; i++)
            {
                table.WidthPercentage = 100;

                for (int k = 0; k < tablet3messagesend.Columns.Count; k++)
                {
                    if (tablet3messagesend[k, i].Value != null)
                    {

                        table.AddCell(new Phrase(tablet3messagesend[k, i].Value.ToString()));
                    }

                }


            }

            //add out table
            doc.Add(table);

            doc.Close();

            System.Diagnostics.Process.Start(@"C:\\Users\\Ruchira Dhananjaya\\Desktop\\ItemList.pdf");

        }

        private void groupBox14_Enter(object sender, EventArgs e)
        {

        }

        private void txtt4Rid_OnValueChanged(object sender, EventArgs e)
        {
            MySqlDataAdapter cm = new MySqlDataAdapter("Select *  from supermarket.sendwith where  id='" + txtt4Rid.Text + "'", myConn);
            DataTable set = new DataTable();
            cm.Fill(set);

            foreach (DataRow dr in set.Rows)
            {


                txtt4itemcode.Text = dr["item_code"].ToString();
                txtt4ItemName.Text = dr["item_name"].ToString();
                txtt4cusID.Text = dr["cust_id"].ToString();
                txtt4advance.Text = dr["Advance"].ToString();







            }
        }

        private void bunifuThinButton213_Click_2(object sender, EventArgs e)
        {
            myConn.Open();
            string query = "select sum(amount) as tot from supermarket.selected_spartitem";
            MySqlCommand cmd = new MySqlCommand(query, myConn);
            cmd.ExecuteNonQuery();
            MySqlDataReader myR;


            myR = cmd.ExecuteReader();

            if (myR.Read())
            {

                txtt4saprepartscharge.Text = myR["tot"].ToString();


            }
            myConn.Close();
        }

        private void btnnicsearch_Click(object sender, EventArgs e)
        {
            if (!(txtt3search.Text.Length == 0))
            {

                int typeno = cbt3searchby.SelectedIndex;
                String type = "";
                String key = txtt3search.Text;


                switch (typeno)
                {
                    case 0:
                        type = "";
                        break;

                    case 1:
                        type = "id";
                        break;

                    case 2:
                        type = "item_name";
                        break;
                    case 3:
                        type = "brand_name";
                        break;



                }


                try
                {
                    MySqlDataAdapter cm = new MySqlDataAdapter("Select *  from supermarket.sendwith where " + type + " like '" + key + "%'", myConn);
                    DataTable set = new DataTable();
                    cm.Fill(set);


                    tablepanel3statuschanging.DataSource = set;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            string i;
            i = (bunifuCustomDataGrid5.SelectedCells[0].Value.ToString());
            DialogResult dialogResult = MessageBox.Show("Do you want to Delete this item?", "Delete item", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {





                myConn.Open();
                MySqlCommand cmd1 = new MySqlCommand("DELETE FROM supermarket.sendwith  WHERE id='" + i + "'", myConn);
                cmd1.ExecuteNonQuery();

                MessageBox.Show("Deleted successfully!!");
                myConn.Close();
                loadsendwith();



            }
            else
            {


                MessageBox.Show("Not Updated!");

            }
        }

        private void txtt4total_OnValueChanged(object sender, EventArgs e)
        {
            string tot = txtt4total.Text.ToString();
            string advance = txtt4advance.Text.ToString();

            double tot1 = double.Parse(tot);
            double adv = double.Parse(advance);

            double subtot = tot1 - adv;

            txtt4balance.Text = subtot.ToString();
        }

        private void txtt4saprepartscharge_OnValueChanged(object sender, EventArgs e)
        {
            string servicecharge = txtt4serviceCharge.Text.ToString();
            string spareparts = txtt4saprepartscharge.Text.ToString();

            double ser = Convert.ToDouble(servicecharge);
            double spa = Convert.ToDouble(spareparts);

            double tot = ser + spa;

            txtt4total.Text = tot.ToString();
        }

        private void txtt4serviceCharge_OnValueChanged(object sender, EventArgs e)
        {
            
        }

        private void txtt4balance_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtt4advance_OnValueChanged(object sender, EventArgs e)
        {
           
        }

        private void txtt4discount_OnValueChanged(object sender, EventArgs e)
        {
            string balance = txtt4balance.Text.ToString();
            string  discount= txtt4discount.Text.ToString();

            double bal = double.Parse(balance);
            double dis = double.Parse(discount);

            double grandtot = bal - dis;

            txtt4grandtotal.Text = grandtot.ToString();
        }

        private void txtt1mobileno1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtt1mobileno1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtt1mobileno2_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtt1mobileno2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtt1landno_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void bunifuThinButton221_Click(object sender, EventArgs e)
        {


            bunifuCircleProgressbar1.Show();
            try
            {
                MailMessage mail = new MailMessage(txtt3from.Text, txtt3password.Text, txtt3subject.Text, txtt3emailbody.Text);
                SmtpClient client = new SmtpClient(txtt3smtp.Text);
                // SmtpClient client = new SmtpClient("smtp.gmail.com",587);
                if (txtt3smtp.Text == "smtp.gmail.com")
                    client.Port = 587;
                else if (txtt3smtp.Text == "smtp.mail.yahoo.com")
                    client.Port = 587;
                //  client.Credentials = new System.Net.NetworkCredential(username.Text, password.Text);
                client.Credentials = new System.Net.NetworkCredential(txtt3from.Text, txtt3password.Text);
                client.EnableSsl = true;

                client.Send(mail);


                ProgresBar();
                MessageBox.Show("Mail Sent!", "Success", MessageBoxButtons.OK);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Mailing failed");
                this.Close();
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (!(txtt2search.Text.Length == 0))
            {

                int typeno = comboBox2.SelectedIndex;
                string type = "";
                string key = txtt2search.Text;


                switch (typeno)
                {
                    case 0:
                        type = "";
                        break;

                    case 1:
                        type = "id";
                        break;

                    case 2:
                        type = "item_name";
                        break;
                    case 3:
                        type = "brand_name";
                        break;



                }


                try
                {
                    MySqlDataAdapter cm = new MySqlDataAdapter("Select *  from supermarket.sendwith where " + type + " like '" + key + "%'", myConn);
                    DataTable set = new DataTable();
                    cm.Fill(set);


                    bunifuCustomDataGrid5.DataSource = set;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void txtt1itemcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
            if(txtt1itemcode.Text.Length>=6)
            {
                e.Handled = true;

            }
        }

        private void tablet3messagesend_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string i;
            i = (tablet3messagesend.SelectedCells[0].Value.ToString());
           
            myConn.Open();

            MySqlDataAdapter cmd = new MySqlDataAdapter("select * from supermarket.sendmessage  where  id= " + i + "", myConn);
            DataTable dt = new DataTable();
            cmd.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {

                string sendby = dr["send_by"].ToString();
                string status = dr["status"].ToString();

                if (status == "pending")
                {
                    radior3pending.Checked = true;
                }

                else if(status == "repaired")
                {
                    radiot3repaired.Checked = true;

                }

                if (sendby == "call")
                {
                    radiot3call.Checked = true;
                }

                else if (sendby == "message")
                {
                    radiot3message.Checked = true;

                }
                else if (sendby == "email")
                {
                    radiot3email.Checked = true;

                }

                txtt3note.Text = dr["note"].ToString();
                txtt3cost.Text = dr["cost"].ToString();
                
                txtt3smtp.Text = dr["emaployee"].ToString();
                //String warranty_end = dr["warranty_end"].ToString();
                //string warranty_start = dr["warranty_end"].ToString();
                

               // txtt1suppliername.Text = dr["suuplier_name"].ToString();
                // cbemployee.selectedValue = dr["salesman_name"].ToString();

            }

            myConn.Close();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            string i;
            i = (tablet3messagesend.SelectedCells[1].Value.ToString());
            DialogResult dialogResult = MessageBox.Show("Do you want to update this item?", "Update item", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                string status="";
                if (radiot3repaired.Checked == true)
                {
                    status = "repaired";
                }
                else if(radior3pending.Checked==true)
                {
                    status = "pending";
                }


                string sendby="";
                if (radiot3call.Checked == true)
                {
                    sendby = "call";
                }
                else if (radiot3message.Checked == true)
                {
                    sendby = "message";
                }
                else if (radiot3email.Checked == true)
                {
                    sendby = "email";
                }



                myConn.Open();
                MySqlCommand cmd1 = new MySqlCommand("UPDATE supermarket.sendmessage SET `emaployee`='"+txtt3smtp+"',`cost`='"+ txtt3cost .Text+ "',`note`='"+ txtt3note .Text+ "',`send_by`='"+sendby+"',`status`='"+status+"' WHERE id='"+i+"'", myConn);
                cmd1.ExecuteNonQuery();

                MessageBox.Show("Updated successfully!!");
                myConn.Close();
                
                loadsendmessage();
            }
            else
            {
                MessageBox.Show("Deleted Cancelled");
            }
        }

        private void bunifuThinButton24_Click_1(object sender, EventArgs e)
        {
            string i;
            i = (tablet3messagesend.SelectedCells[0].Value.ToString());
            DialogResult dialogResult = MessageBox.Show("Do you want to Deleted this item?", "Deleted item", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {




                myConn.Open();
                MySqlCommand cmd1 = new MySqlCommand("DELETE FROM supermarket.sendmessage WHERE id='" + i + "'", myConn);
                cmd1.ExecuteNonQuery();

                MessageBox.Show("Deleted successfully!!");
                myConn.Close();
                loadsendmessage();
              
            }
            else
            {
                MessageBox.Show("Deleted Cancelled");
            }
        }

        private void bunifuCustomDataGrid5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bunifuThinButton210.Visible = true;
            bunifuThinButton25.Visible = true;

            string i;
            i = (bunifuCustomDataGrid5.SelectedCells[0].Value.ToString());

            MySqlDataAdapter cm = new MySqlDataAdapter("Select *  from supermarket.sendwith where  id='" + i + "'", myConn);
            DataTable set = new DataTable();
            cm.Fill(set);

            foreach (DataRow dr in set.Rows)
            {


                txtt2sentdate.Text = dr["sent_date"].ToString();
                cbt2employee.Text = dr["sendwith"].ToString();
                







            }
        }

        private void txtt1advance_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtt1advance_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtt3mobileno_TextChanged(object sender, EventArgs e)
        {

        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }
    }
}


    
