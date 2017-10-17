using System;
using System.Data;

using MySql.Data.MySqlClient;
using System.IO;
using System.Text.RegularExpressions;
using BarcodeLib;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using BarcodeLib.Symbologies;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Printing;
using System.Windows.Forms;


namespace inventory
{
    public partial class main : Form
    {
        public int awh;
        public Boolean status = true;

        
     

        public main()
        {
            InitializeComponent();

            int v = Session.getUser();
            if (v == 1)
            {
                // btncconfirm.Visible = false;
                //button1.Enabled = false;
                // ((Control)this.tabPage1).Enabled = false;
                //tabPage1.Enabled = false;
                 tabControl1.TabPages.Remove(tabPage2);

            }
            else if (v == 2)
            {
                //button2.Enabled = false;
                tabControl1.TabPages.Remove(tabPage4);
                // ((Control)this.tabPage2).Enabled = false;
            }
            else
            {


            }


            loadtable();
            wrun();
            srun();
            setfloor();
            vendor();
            comboBox1.Text = "Category";
            subbox.Text = "Sub Category";
            fillcombocat();
            afloor.Text = "Select Floor";
            catcombo.Text = "Select Category";
            subcombo.Text = "Select Sub Category";
            ret.Columns[0].Width = 70;
            ret.Columns[2].Width = 70;
            acode.Visible = false;
            hidetb.Visible = false;


        }
      
       /* public void ConvertText()
        {
            try
            {
                string Date = DateTime.Now.ToLongTimeString();
                string Time = DateTime.Now.ToLongDateString();
                String Datestr = DateTime.Now.ToString("yyyy-M-dd-HH-mm-ss");
                string total = rettot.Text;
                //string cash = txtdpay.Text;
               // string balance = lblbal.Text;
                //string prepare = cmbprepare.Text;
                string path = System.IO.Path.Combine("C:\\Users\\Hp\\Desktop", "Madusha" + Datestr + ".txt");
                TextWriter writer = new StreamWriter(path);
                writer.WriteLine("\t \t MADUSHA SUPERMARKET \t \t");
                writer.WriteLine("--------------------------------------------------");
                writer.WriteLine("" + Date + "\t \t" + Time);
               // writer.WriteLine("Cashier: " + prepare);
                writer.WriteLine("");

                writer.WriteLine("\tItem Name\t Item Code  Price  Quantity ");
                for (int i = 0; i < ret.Rows.Count; i++)
                {
                    writer.Write("\t" + ret.Rows[i].Cells[1].Value.ToString() + "\t" + " " +
                    ret.Rows[i].Cells[2].Value.ToString() + "  " + "  " +
                    ret.Rows[i].Cells[3].Value.ToString() + "  " + "    " +
                    
                    ret.Rows[i].Cells[4].Value.ToString() + "   " + " ");
                    writer.WriteLine("");
                    writer.WriteLine("--------------------------------------------------");
                }
                writer.WriteLine("\t Total Amount \t \t \t" + total);
                //writer.WriteLine("\t Down Payment \t \t \t" + cash);
                //writer.WriteLine("\t Balance \t \t \t" + balance);

                writer.Close();
                MessageBox.Show("data Exported");
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        */
        void reset()
        {

            catcombo.Text = "Select Category";
            subcombo.Text = "Select Sub Category";
            abrand.Text = "";
            aname.Text = "";
            acode.Text = "";
            ades.Text = "";
            whpr.Text = "";
            rpr.Text = "";
            abcode.Text = "";
            awarrenty.Text = "";
            afloor.Text = "Select Floor";
            roq.Text = "";
            tqty.Text = "";
            ashelf.Text = "";
            issue.Text = "";
            psize.Text = "";
            aq1.Text = "";
            aq2.Text = "";
            aq3.Text = "";
            aq4.Text = "";
            ap1.Text = "";
            ap2.Text = "";
            ap3.Text = "";
            ap4.Text = "";
            pic.Image = Properties.Resources.defimg;
            imgpath.Text = "";



        }

        void vendor()
        {
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);
            MySqlCommand cm = new MySqlCommand("Select distinct fullname from supermarket.vendor", dbcon);
            MySqlDataReader r;

            try
            {
                dbcon.Open();
                r = cm.ExecuteReader();

                while (r.Read())
                {
                    string cat = r.GetString("fullname");

                    spname.Items.Add(cat);
                    dsname.Items.Add(cat);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void resetstock()
        {
            scode.Text = "";
            sname.Text = "";
            scatcombo.Text = "";
            ssubcombo.Text = "";
            sbrand.Text = "";
            srpr.Text = "";
            spname.Text = "";
            spid.Text = "";
            saqty.Text = "";
            snqty.Text = "";
            stqty.Text = "";
            sup.Text = "";
            sbcode.Text = "";

        }

        void resetret()
        {
            rcode.Text = "";
            rname.Text = "";
            rcombo.Text = "";
            rsubcombo.Text = "";
            rbrand.Text = "";
            rqty.Text = "";
            rrpr.Text = "";
            rded.Text = "";
            datei.Text = "";
            rbcode.Text = "";
        }
        void resetgud()
        {
            gcode.Text = "";
            gcat.Text = "";
            gscat.Text = "";
            gbrand.Text = "";
            gname.Text = "";
            gwh.Text = "";
            gamnt.Text = "";
            gby.Text = "";
            gsr.Text = "";
            gbcode.Text = "";

        }
        void resetdef()
        {
            dcode.Text = "";
            dname.Text = "";
            dcat.Text = "";
            dsub.Text = "";
            dbrand.Text = "";
            drp.Text = "";
            dsname.Text = "";
            dsid.Text = "";
            dq.Text = "";
            dreas.Text = "";
            ddate.Text = "";
            dbcode.Text = "";
        }
        void resetmiss()
        {
            tcode.Text = "";
            tname.Text = "";
            tsc.Text = "";
            tss.Text = "";
            tws.Text = "";
            tt.Text = "";
            tmq.Text = "";
            tbcode.Text = "";
        }
        void wrun()
        {
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);
            MySqlCommand cm = new MySqlCommand("Select Item_code as 'Item Code',Item_name as 'Item Name',wqty as 'Warehouse',sqty as 'Showroom',wqty+sqty as 'Total' from supermarket.item where wqty <= roqty", dbcon);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cm;
                DataTable set = new DataTable();
                sda.Fill(set);
                BindingSource s = new BindingSource();

                s.DataSource = set;
                wro.DataSource = s;
                sda.Update(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        void srun()
        {
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);
            MySqlCommand cm = new MySqlCommand("Select Item_code as 'Item Code',Item_name as 'Item Name',wqty as 'Warehouse',sqty as 'Showroom',wqty+sqty as 'Total' from supermarket.item where sqty <= tqty", dbcon);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cm;
                DataTable set = new DataTable();
                sda.Fill(set);
                BindingSource s = new BindingSource();

                s.DataSource = set;
                sro.DataSource = s;
                sda.Update(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        void setfloor()
        {

            try//f1
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);

                MySqlCommand cm = new MySqlCommand("Select sum(sqty*Rprice) as 'tot' from supermarket.Item where Floor=1", dbcon);

                dbcon.Open();
                DataSet d = new DataSet();
                MySqlDataReader sdr = cm.ExecuteReader();

                while (sdr.Read())
                {
                    f1.Text = sdr["tot"].ToString();



                }
                dbcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try//f2
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);

                MySqlCommand cm = new MySqlCommand("Select sum(sqty*Rprice) as 'tot' from supermarket.Item where Floor=2", dbcon);

                dbcon.Open();
                DataSet d = new DataSet();
                MySqlDataReader sdr = cm.ExecuteReader();

                while (sdr.Read())
                {
                    f2.Text = sdr["tot"].ToString();


                }
                dbcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try//f3
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);

                MySqlCommand cm = new MySqlCommand("Select sum(sqty*Rprice) as 'tot' from supermarket.Item where Floor=3", dbcon);

                dbcon.Open();
                DataSet d = new DataSet();
                MySqlDataReader sdr = cm.ExecuteReader();

                while (sdr.Read())
                {
                    f3.Text = sdr["tot"].ToString();


                }
                dbcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try//f4
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);

                MySqlCommand cm = new MySqlCommand("Select sum(sqty*Rprice) as 'tot' from supermarket.Item where Floor=4", dbcon);

                dbcon.Open();
                DataSet d = new DataSet();
                MySqlDataReader sdr = cm.ExecuteReader();

                while (sdr.Read())
                {
                    f4.Text = sdr["tot"].ToString();

                }
                dbcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try//w
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);

                MySqlCommand cm = new MySqlCommand("Select sum(wqty*Rprice) as 'tot' from supermarket.Item", dbcon);

                dbcon.Open();
                DataSet d = new DataSet();
                MySqlDataReader sdr = cm.ExecuteReader();

                while (sdr.Read())
                {
                    f5.Text = sdr["tot"].ToString();


                }
                dbcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //calc Total
            try
            { double total = Double.Parse(f1.Text) + Double.Parse(f2.Text) + Double.Parse(f3.Text) + Double.Parse(f4.Text) + Double.Parse(f5.Text);
                all.Text = total.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        void resetview()
        {

            brand.Text = "";
            q1.Text = "";
            q2.Text = "";
            q3.Text = "";
            q4.Text = "";
            p1.Text = "";
            p2.Text = "";
            p3.Text = "";
            p4.Text = "";
            pictureBox1.Image = Properties.Resources.defimg;
            wprice.Text = "";
            description.Text = "";
            warrenty.Text = "";
            roqty.Text = "";
            floor.Text = "";
            Shelf.Text = "";
            freeissue.Text = "";
            pack.Text = "";
            createdD.Text = "";
            modifiedD.Text = "";
            lastPD.Text = "";
            lastSD.Text = "";
            name.Text = "";
            code.Text = "";
            code.Text = "";
            name.Text = "";
            category.Text = "";
            sub.Text = "";
            rprice.Text = "";
            bcode.Text = "";
            brcd.Text = "";


        }



        void fillcombocat()
        {
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);
            MySqlCommand cm = new MySqlCommand("Select distinct name from supermarket.category", dbcon);
            MySqlDataReader r;

            try
            {
                dbcon.Open();
                r = cm.ExecuteReader();

                while (r.Read())
                {
                    string cat = r.GetString("name");

                    comboBox1.Items.Add(cat);
                    catcombo.Items.Add(cat);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        public void Regexp(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {
            ToolTip buttonToolTip = new ToolTip();
            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {

                pc.Image = Properties.Resources.invalid;
                n.SetToolTip(pc, s);
                status = false;


            }
            else
            {
                pc.Image = null;

            }

        }
        void loadtable()
        {
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);

            if (comboBox1.SelectedIndex == -1)
            {

                MySqlCommand cm = new MySqlCommand("Select Item_code as 'Item Code',Item_name as 'Item',Category,Sub_category as 'Sub Category',Rprice as 'Price' from supermarket.item", dbcon);

                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cm;
                    DataTable set = new DataTable();
                    sda.Fill(set);
                    BindingSource s = new BindingSource();

                    s.DataSource = set;
                    gr.DataSource = s;
                    sda.Update(set);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                if (subbox.SelectedIndex == -1)
                {
                    MySqlCommand cm = new MySqlCommand("Select Item_code as 'Item Code',Item_name as 'Item',Category,Sub_category as 'Sub Category',Rprice as 'Price' from supermarket.item where Category='" + comboBox1.Text + "'", dbcon);

                    try
                    {
                        MySqlDataAdapter sda = new MySqlDataAdapter();
                        sda.SelectCommand = cm;
                        DataTable set = new DataTable();
                        sda.Fill(set);
                        BindingSource s = new BindingSource();

                        s.DataSource = set;
                        gr.DataSource = s;
                        sda.Update(set);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MySqlCommand cm = new MySqlCommand("Select Item_code as 'Item Code',Item_name as 'Item',Category,Sub_category as 'Sub Category',Rprice as 'Price' from supermarket.item where Category='" + comboBox1.Text + "' and Sub_category='" + subbox.Text + "'", dbcon);

                    try
                    {
                        MySqlDataAdapter sda = new MySqlDataAdapter();
                        sda.SelectCommand = cm;
                        DataTable set = new DataTable();
                        sda.Fill(set);
                        BindingSource s = new BindingSource();

                        s.DataSource = set;
                        gr.DataSource = s;
                        sda.Update(set);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }


            }





        }
        private void main_Load(object sender, EventArgs e)
        {


        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            subbox.Text = "Sub Category";
            loadtable();
            subbox.Items.Clear();
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);
            MySqlCommand cm = new MySqlCommand("Select sub from supermarket.category where name='" + comboBox1.Text + "'", dbcon);
            MySqlDataReader r;
            try
            {
                dbcon.Open();
                r = cm.ExecuteReader();

                while (r.Read())
                {
                    string cat = r.GetString("sub");

                    subbox.Items.Add(cat);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void subbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadtable();
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            comboBox1.Text = "Category";
            subbox.Text = "Sub Category";
            if (!(icode.text.Length == 0))
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);

                MySqlCommand cm = new MySqlCommand("Select Item_code as 'Item Code',Item_name as 'Item',Category,Sub_category as 'Sub Category',Rprice as 'Price' from supermarket.item where Item_code like '" + icode.text + "%'", dbcon);

                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cm;
                    DataTable set = new DataTable();
                    sda.Fill(set);
                    BindingSource s = new BindingSource();

                    s.DataSource = set;
                    gr.DataSource = s;
                    sda.Update(set);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

       
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            subbox.Items.Clear();
            fillcombocat();

            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);
            MySqlCommand cm = new MySqlCommand("Select Item_code as 'Item Code',Item_name as 'Item',Category,Sub_category as 'Sub Category',Rprice as 'Price' from supermarket.item", dbcon);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cm;
                DataTable set = new DataTable();
                sda.Fill(set);
                BindingSource s = new BindingSource();

                s.DataSource = set;
                gr.DataSource = s;
                sda.Update(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            fillcombocat();
            comboBox1.Text = "Select Category";
            subbox.Text = "Select Sub Category";
            icode.text = "";
            bcode.text = "";

        }




        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel25_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox4_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void gr_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //helloooooooo
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gr.Rows[e.RowIndex];
                code.Text = row.Cells["Item Code"].Value.ToString();
                name.Text = row.Cells["Item"].Value.ToString();
                category.Text = row.Cells["Category"].Value.ToString();
                sub.Text = row.Cells["Sub Category"].Value.ToString();
                rprice.Text = row.Cells["Price"].Value.ToString();
                try
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);

                    MySqlCommand cm = new MySqlCommand("Select distinct Barcode,Brand,image,Wprice,Description,Warrenty,wqty,sqty,roqty,Floor,shelf,freeIssue,packSize,createdD,modifiedD,modifiedBy,lastPD,lastSD from supermarket.Item where Item_code='" + code.Text + "'", dbcon);
                    dbcon.Open();
                    DataSet d = new DataSet();
                    MySqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        brcd.Text = sdr["Barcode"].ToString();
                        brand.Text = sdr["Brand"].ToString();

                        byte[] img = (byte[])(sdr["image"]);
                        if (img == null)
                        {
                            pictureBox1.Image = null;
                        }
                        else
                        {
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                            MemoryStream mstream = new MemoryStream(img);
                            pictureBox1.Image = System.Drawing.Image.FromStream(mstream);
                        }
                        wprice.Text = sdr["Wprice"].ToString();
                        description.Text = sdr["Description"].ToString();
                        warrenty.Text = sdr["Warrenty"].ToString();
                        int w = Int32.Parse(sdr["wqty"].ToString());
                        int s = Int32.Parse(sdr["sqty"].ToString());
                        roqty.Text = sdr["roqty"].ToString();
                        floor.Text = sdr["Floor"].ToString();
                        Shelf.Text = sdr["shelf"].ToString();
                        freeissue.Text = sdr["freeIssue"].ToString();
                        pack.Text = sdr["packSize"].ToString();
                        createdD.Text = sdr["createdD"].ToString();
                        modifiedD.Text = sdr["modifiedD"].ToString();
                        modifiedBy.Text = sdr["modifiedBy"].ToString();
                        lastPD.Text = sdr["lastPD"].ToString();
                        lastSD.Text = sdr["lastSD"].ToString();

                        qty.Text = (w + s).ToString();
                        wh.Text = w.ToString();
                        sr.Text = s.ToString();
                    }
                    dbcon.Close();


                    //stock table

                    //sgr.Width = 1800;

                    MySqlCommand cmm = new MySqlCommand("Select Date,Sname as 'Supplier Name',Sid as 'Supplier ID',Price as 'Unit Price',qty as 'Quantity' from supermarket.stock where Item_code='" + code.Text + "'", dbcon);

                    try
                    {
                        MySqlDataAdapter sda = new MySqlDataAdapter();
                        sda.SelectCommand = cmm;
                        DataTable set = new DataTable();
                        sda.Fill(set);
                        BindingSource s = new BindingSource();
                        s.DataSource = set;
                        sgr.DataSource = s;
                        sda.Update(set);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    // sdr.Close();
                    //price levels

                    MySqlCommand cmd = new MySqlCommand("Select q1,p1,q2,p2,q3,p3,q4,p4 from supermarket.pricelevel where Item_code='" + code.Text + "'", dbcon);
                    dbcon.Open();
                    try
                    {
                        DataSet d2 = new DataSet();
                        MySqlDataReader sdr2 = cmd.ExecuteReader();
                        while (sdr2.Read())
                        {
                            q1.Text = sdr2["q1"].ToString();
                            p1.Text = sdr2["p1"].ToString();
                            q2.Text = sdr2["q2"].ToString();
                            p2.Text = sdr2["p2"].ToString();
                            q3.Text = sdr2["q3"].ToString();
                            p3.Text = sdr2["p3"].ToString();
                            q4.Text = sdr2["q4"].ToString();
                            p4.Text = sdr2["p4"].ToString();


                        }
                        dbcon.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void bunifuCustomLabel108_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTileButton1_Click_1(object sender, EventArgs e)
        {
            newcatsub f1 = new newcatsub();

            f1.Show();
        }

        private void gb3_Enter(object sender, EventArgs e)
        {

        }

        private void bunifuCards8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuCustomLabel125_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton23_Click_1(object sender, EventArgs e)
        {
            upitem F2 = new upitem(code.Text);
            F2.Show();
        }

        private void bunifuMetroTextbox35_OnValueChanged(object sender, EventArgs e)
        {
            q4.Enabled = false;
        }

        private void bunifuCustomLabel162_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            report f1 = new report();
            f1.Show();
        }

        private void catcombo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            subcombo.Text = "Sub Category";
            loadtable();
            subcombo.Items.Clear();
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);
            MySqlCommand cm = new MySqlCommand("Select sub from supermarket.category where name='" + catcombo.Text + "'", dbcon);
            MySqlDataReader r;
            try
            {
                dbcon.Open();
                r = cm.ExecuteReader();

                while (r.Read())
                {
                    string cat = r.GetString("sub");

                    subcombo.Items.Add(cat);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void groupBox14_Enter(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton29_Click(object sender, EventArgs e)
        {

            status = true;
            Regexp("^[0-9]+$", abcode, bce, "Only Numeric values allowed");
            Regexp("^[0-9]+$", tqty, tqe, "Only Numeric values allowed");
            Regexp("^[0-9]+$", roq, roe, "Only Numeric values allowed");
            Regexp("^[0-9]+$", psize, pse, "Only Numeric values allowed");
            Regexp("^[0-9]+$", rpr, rpe, "Only Numeric values allowed");
            Regexp("^[0-9]+$", whpr, wpe, "Only Numeric values allowed");
            Regexp("^[0-9]+$", aq1, q1e, "Only Numeric values allowed");
            Regexp("^[0-9]+$", aq2, q2e, "Only Numeric values allowed");
            Regexp("^[0-9]+$", aq3, q3e, "Only Numeric values allowed");
            Regexp("^[0-9]+$", aq4, q4e, "Only Numeric values allowed");
            Regexp("^[0-9]+$", ap1, p1e, "Only Numeric values allowed");
            Regexp("^[0-9]+$", ap2, p2e, "Only Numeric values allowed");
            Regexp("^[0-9]+$", ap3, p3e, "Only Numeric values allowed");
            Regexp("^[0-9]+$", ap4, p4e, "Only Numeric values allowed");

            if (status == true)
            {

                string upd, dist;
                if (up.Checked == true)
                {
                    upd = "yes";
                }
                else
                {
                    upd = "no";
                }
                if (dis.Checked == true)
                {
                    dist = "yes";
                }
                else
                {
                    dist = "no";
                }

                DialogResult dialogResult = MessageBox.Show("Do you want to insert the item?", "Add item", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    byte[] imageBt = null;
                    try
                    {

                        FileStream fstream = new FileStream(this.imgpath.Text, FileMode.Open, FileAccess.Read);

                        BinaryReader br = new BinaryReader(fstream);
                        imageBt = br.ReadBytes((int)fstream.Length);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    // string a = null;
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);
                    MySqlCommand cm = new MySqlCommand("insert into supermarket.item(Item_name,Barcode,Category,Sub_category,Brand,Wprice,Rprice,Description,Warrenty,image,wqty,sqty,roqty,tqty,Floor,shelf,freeIssue,packSize,createdD,modifiedD,modifiedBy,lastPD,lastSD,updatable,discount) values('" + aname.Text + "','" + abcode.Text + "','" + catcombo.Text + "','" + subcombo.Text + "','" + abrand.Text + "','" + whpr.Text + "','" + rpr.Text + "','" + ades.Text + "','" + awarrenty.Text + "',@IMG,0,0,'" + roq.Text + "','" + tqty.Text + "','" + afloor.Text + "','" + ashelf.Text + "','" + issue.Text + "','" + psize.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + "','" + "Upali Kariyawasam" + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + "','" + upd + "','" + dist + "')", dbcon);
                    MySqlDataReader r;
                    try
                    {
                        dbcon.Open();
                        cm.Parameters.Add(new MySqlParameter("@IMG", imageBt));
                        r = cm.ExecuteReader();
                        MessageBox.Show("Inserted successfully!");
                        while (r.Read())
                        {

                        }
                        loadtable();

                        dbcon.Close();

                    }
                    catch (Exception ex)
                    {
                         MessageBox.Show(ex.Message);
                    }
                    try
                    {
                        MySqlCommand cmdx = new MySqlCommand("Select Item_code from supermarket.item where Item_name='" + aname.Text + "' and Brand='" + abrand.Text + "'", dbcon);
                    dbcon.Open();
                   
                        DataSet d2 = new DataSet();
                        MySqlDataReader sdr2 = cmdx.ExecuteReader();
                        while (sdr2.Read())
                        {
                            acode.Text = sdr2["Item_code"].ToString();


                        }
                        sdr2.Close();
                        dbcon.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    //add price levels



                    MySqlCommand cmd = new MySqlCommand("insert into supermarket.pricelevel values('" + acode.Text + "','" + abcode.Text + "','" + aq1.Text + "','" + ap1.Text + "','" + aq2.Text + "','" + ap2.Text + "','" + aq3.Text + "','" + ap3.Text + "','" + aq4.Text + "','" + ap4.Text + "')", dbcon);
                    MySqlDataReader r1;
                    try
                    {
                        dbcon.Open();
                        r1 = cmd.ExecuteReader();
                        while (r1.Read())
                        {

                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    loadtable();
                    reset();

                }

                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Not Inserted!");

                }
            }

        }

        private void bunifuThinButton211_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Do you want to reset the fields?", "Reset", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                reset();
            }
            else if (dialogResult == DialogResult.No)
            {

            }

        }

        private void bunifuMetroTextbox61_OnValueChanged(object sender, EventArgs e)
        {
            if (!(scode.Text.Length == 0))
            {

                try
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);

                    MySqlCommand cm = new MySqlCommand("Select Barcode,Item_name,Category,Sub_category,Brand,Rprice,wqty,sqty from supermarket.Item where Item_code='" + scode.Text + "'", dbcon);

                    dbcon.Open();
                    DataSet d = new DataSet();
                    MySqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        sbcode.Text = sdr["Barcode"].ToString();
                        sname.Text = sdr["Item_name"].ToString();
                        scatcombo.Text = sdr["Category"].ToString();
                        ssubcombo.Text = sdr["Sub_category"].ToString();
                        sbrand.Text = sdr["Brand"].ToString();
                        srpr.Text = sdr["Rprice"].ToString();
                        int wh = Convert.ToInt32(sdr["wqty"].ToString());
                        int sh = Convert.ToInt32(sdr["sqty"].ToString());
                        saqty.Text = (wh + sh).ToString();

                    }
                    dbcon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void snqty_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                int nq = Convert.ToInt32(snqty.Text);
                int aq = Convert.ToInt32(saqty.Text);
                int tot = nq + aq;
                stqty.Text = tot.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuThinButton215_Click(object sender, EventArgs e)
        {
            status = true;
            Regexp("^[0-9]+$", sbcode, sbcodee, "Only Numeric values allowed");
            Regexp("^[0-9]+$", srpr, srpre, "Only Numeric values allowed");
            Regexp("^[0-9]+$", saqty, saqtye, "Only Numeric values allowed");
            Regexp("^[0-9]+$", snqty, snqtye, "Only Numeric values allowed");
            Regexp("^[0-9]+$", stqty, stqtye, "Only Numeric values allowed");
            Regexp("^[0-9]+$", sup, supe, "Only Numeric values allowed");

            if (status == true)
            {

                DialogResult dialogResult = MessageBox.Show("Do you want to update this Stock?", "Update", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    try
                    {
                        string con = "datasource=localhost;port=3306;username=root";
                        MySqlConnection dbcon = new MySqlConnection(con);

                        MySqlCommand cm = new MySqlCommand("Select wqty from supermarket.Item where Item_code='" + scode.Text + "'", dbcon);

                        dbcon.Open();
                        DataSet d = new DataSet();
                        MySqlDataReader ssdr = cm.ExecuteReader();

                        while (ssdr.Read())
                        {

                            awh = Convert.ToInt32(ssdr["wqty"].ToString());


                        }
                        int nst = awh + Convert.ToInt32(snqty.Text);
                        ssdr.Close();

                        MySqlCommand cmd = new MySqlCommand("Update supermarket.Item set wqty=wqty+'" + Convert.ToInt32(snqty.Text) + "' where Item_code='" + scode.Text + "'", dbcon);
                        MySqlDataReader r;
                        r = cmd.ExecuteReader();
                        MessageBox.Show("Updated successfully!");
                        r.Close();

                        MySqlCommand cmd2 = new MySqlCommand("insert into supermarket.stock values('" + scode.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + "','" + spname.Text + "','" + spid.Text + "','" + sup.Text + "','" + snqty.Text + "')", dbcon);
                        MySqlDataReader r2;
                        r2 = cmd2.ExecuteReader();
                        loadtable();
                        dbcon.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                  //  resetstock();
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Stock Update Cancelled");
                }
            }
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string picPath = dlg.FileName.ToString();
                imgpath.Text = picPath;
                pic.ImageLocation = picPath;
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void bunifuThinButton22_Click_2(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Do you want to Delete the item?", "Delete item", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);
                MySqlCommand cm = new MySqlCommand("Delete from supermarket.item where Item_code='" + code.Text + "'", dbcon);
                MySqlDataReader r;
                try
                {
                    dbcon.Open();
                    r = cm.ExecuteReader();
                    MessageBox.Show("Deleted successfully!");
                    loadtable();
                    resetview();
                    while (r.Read())
                    {

                    }
                    resetview();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Not Deleted!");
            }

        }

        private void gr_CellContentClick_1(object sender, KeyEventArgs e)
        {

        }

        private void abrand_Enter(object sender, EventArgs e)
        {

        }

        private void ap4_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton212_Click(object sender, EventArgs e)
        {


        }

        private void bunifuThinButton223_Click(object sender, EventArgs e)
        {
            try {
                int q = Convert.ToInt32(rqty.Text);
                double p = Convert.ToDouble(rrpr.Text);
                double tp = (q * p) - Convert.ToDouble(rded.Text);
           
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(this.ret, rcode.Text, datei.Text, rqty.Text, rrpr.Text, rded.Text, tp.ToString());
            this.ret.Rows.Add(row);

            decimal Total = 0;

            for (int i = 0; i < ret.Rows.Count; i++)
            {
                Total += Convert.ToDecimal(ret.Rows[i].Cells["tot"].Value);
            }

            rettot.Text = Total.ToString("#.00");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            resetret();
        }

        private void bunifuThinButton213_Click(object sender, EventArgs e)
        {
            this.ret.Rows.Clear();
            int x = 0;
            rettot.Text = x.ToString("0.00");
        }

        private void rcode_OnValueChanged(object sender, EventArgs e)
        {
            if (!(rcode.Text.Length == 0))
            {

                try
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);

                    MySqlCommand cm = new MySqlCommand("Select Barcode,Item_name,Category,Sub_category,Brand,Rprice,wqty,sqty from supermarket.Item where Item_code='" + rcode.Text + "'", dbcon);

                    dbcon.Open();
                    DataSet d = new DataSet();
                    MySqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        rbcode.Text = sdr["Barcode"].ToString();
                        rname.Text = sdr["Item_name"].ToString();
                        rcombo.Text = sdr["Category"].ToString();
                        rsubcombo.Text = sdr["Sub_category"].ToString();
                        rbrand.Text = sdr["Brand"].ToString();
                        rrpr.Text = sdr["Rprice"].ToString();

                    }
                    dbcon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }

        }

        private void bunifuThinButton214_Click(object sender, EventArgs e)
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
                        for (int i = 0; i < ret.Rows.Count; i++)
                        {
                            StrQuery = "INSERT INTO supermarket.returneditem(Item_code,sdate,qty,price,ded,tot) VALUES(" + ret.Rows[i].Cells["cd"].Value + ",'" + ret.Rows[i].Cells["sd"].Value + "'," + ret.Rows[i].Cells["qt"].Value + "," + ret.Rows[i].Cells["pr"].Value + "," + ret.Rows[i].Cells["dedu"].Value + "," + ret.Rows[i].Cells["tot"].Value + ")";
                            comm.CommandText = StrQuery;
                            comm.ExecuteNonQuery();

                        }
                        MessageBox.Show("Success");
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.Message);
                    }
                }
            }

                 MySqlConnection dbcon = new MySqlConnection(con);

                 MySqlCommand cmd = new MySqlCommand("insert into supermarket.retreceipt(date,total) values('" + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + "','" + rettot.Text +  "')", dbcon);
                 MySqlDataReader r1;
                 try
                 {
                     dbcon.Open();
                     r1 = cmd.ExecuteReader();
                     while (r1.Read())
                     {

                     }


                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }

               // ConvertText();
                MessageBox.Show("Done");
                resetret();
                this.ret.Rows.Clear();
                int x = 0;
                rettot.Text = x.ToString("0.00");




        }

        private void ret_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuThinButton225_Click(object sender, EventArgs e)
        {
            wrun();
        }

        private void bunifuThinButton226_Click(object sender, EventArgs e)
        {
            srun();
        }

        private void bunifuThinButton227_Click(object sender, EventArgs e)
        {
            setfloor();
        }

        private void f1_OnValueChanged(object sender, EventArgs e)
        {
            f1.Enabled = false;
        }

        private void f2_OnValueChanged(object sender, EventArgs e)
        {
            f2.Enabled = false;
        }

        private void f3_OnValueChanged(object sender, EventArgs e)
        {
            f3.Enabled = false;
        }

        private void f4_OnValueChanged(object sender, EventArgs e)
        {
            f4.Enabled = false;
        }

        private void f5_OnValueChanged(object sender, EventArgs e)
        {
            f5.Enabled = false;
        }

        private void all_OnValueChanged(object sender, EventArgs e)
        {
            all.Enabled = false;
        }

        private void rrpr_OnValueChanged(object sender, EventArgs e)
        {
            rrpr.Enabled = false;
        }

        private void q1_OnValueChanged(object sender, EventArgs e)
        {
            q1.Enabled = false;
        }


        private void p1_OnValueChanged(object sender, EventArgs e)
        {
            p1.Enabled = false;
        }


        private void q2_OnValueChanged(object sender, EventArgs e)
        {
            q2.Enabled = false;
        }

        private void p2_OnValueChanged(object sender, EventArgs e)
        {
            p2.Enabled = false;
        }

        private void q3_OnValueChanged(object sender, EventArgs e)
        {
            q3.Enabled = false;
        }

        private void p3_OnValueChanged(object sender, EventArgs e)
        {
            p3.Enabled = false;
        }

        private void p4_OnValueChanged(object sender, EventArgs e)
        {
            p4.Enabled = false;
        }

        private void wro_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void wro_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void bunifuMetroTextbox40_OnValueChanged(object sender, EventArgs e)
        {
            if (!(gcode.Text.Length == 0))
            {

                try
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);

                    MySqlCommand cm = new MySqlCommand("Select Barcode,Item_name,Category,Sub_category,Brand,wqty,sqty from supermarket.Item where Item_code='" + gcode.Text + "'", dbcon);

                    dbcon.Open();
                    DataSet d = new DataSet();
                    MySqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        gbcode.Text = sdr["Barcode"].ToString();
                        gname.Text = sdr["Item_name"].ToString();
                        gcat.Text = sdr["Category"].ToString();
                        gscat.Text = sdr["Sub_category"].ToString();
                        gbrand.Text = sdr["Brand"].ToString();
                        gwh.Text = sdr["wqty"].ToString();
                        gsr.Text = sdr["sqty"].ToString();

                    }
                    dbcon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        
        private void gcat_OnValueChanged(object sender, EventArgs e)
        {
            gcat.Enabled = false;
        }

        private void gscat_OnValueChanged(object sender, EventArgs e)
        {
            gscat.Enabled = false;
        }

        private void gbrand_OnValueChanged(object sender, EventArgs e)
        {
            gbrand.Enabled = false;
        }

        private void gname_OnValueChanged(object sender, EventArgs e)
        {
            gname.Enabled = false;
        }

        private void gwh_OnValueChanged(object sender, EventArgs e)
        {
            gwh.Enabled = false;
        }

        private void gsr_OnValueChanged(object sender, EventArgs e)
        {
            gsr.Enabled = false;
        }

        private void bunifuThinButton219_Click(object sender, EventArgs e)
        {
            try
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);
                dbcon.Open();
                MySqlCommand cmd2 = new MySqlCommand("insert into supermarket.transfer values('" + gcode.Text + "','" + gamnt.Text + "','" + gby.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + "')", dbcon);
                MySqlDataReader r2;
                r2 = cmd2.ExecuteReader();
                MessageBox.Show("Transfered successfully!");

                r2.Close();

                MySqlCommand cmd = new MySqlCommand("Update supermarket.Item set wqty=wqty-'" + Convert.ToInt32(gamnt.Text) + "' where Item_code='" + gcode.Text + "'", dbcon);
                MySqlDataReader r;
                r = cmd.ExecuteReader();


                r.Close();

                MySqlCommand cmd3 = new MySqlCommand("Update supermarket.Item set sqty=sqty+'" + Convert.ToInt32(gamnt.Text) + "' where Item_code='" + gcode.Text + "'", dbcon);
                MySqlDataReader rs;
                rs = cmd3.ExecuteReader();

                dbcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            resetgud();


        }

        private void dcode_OnValueChanged(object sender, EventArgs e)
        {
            if (!(dcode.Text.Length == 0))
            {

                try
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);

                    MySqlCommand cm = new MySqlCommand("Select Barcode,Item_name,Category,Sub_category,Brand,Rprice,wqty,sqty from supermarket.Item where Item_code='" + dcode.Text + "'", dbcon);

                    dbcon.Open();
                    DataSet d = new DataSet();
                    MySqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        dbcode.Text = sdr["Barcode"].ToString();
                        dname.Text = sdr["Item_name"].ToString();
                        dcat.Text = sdr["Category"].ToString();
                        dsub.Text = sdr["Sub_category"].ToString();
                        dbrand.Text = sdr["Brand"].ToString();
                        drp.Text = sdr["Rprice"].ToString();

                    }
                    dbcon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void dbcode_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void dname_OnValueChanged(object sender, EventArgs e)
        {
            dname.Enabled = false;
        }

        private void dcat_OnValueChanged(object sender, EventArgs e)
        {
            dcat.Enabled = false;
        }

        private void dsub_OnValueChanged(object sender, EventArgs e)
        {
            dsub.Enabled = false;
        }

        private void dbrand_OnValueChanged(object sender, EventArgs e)
        {
            dbrand.Enabled = false;
        }

        private void drp_OnValueChanged(object sender, EventArgs e)
        {
            drp.Enabled = false;
        }

        private void tcode_OnValueChanged(object sender, EventArgs e)
        {
            if (!(tcode.Text.Length == 0))
            {

                try
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);

                    MySqlCommand cm = new MySqlCommand("Select Barcode,Item_name from supermarket.Item where Item_code='" + tcode.Text + "'", dbcon);

                    dbcon.Open();
                    DataSet d = new DataSet();
                    MySqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        tbcode.Text = sdr["Barcode"].ToString();
                        tname.Text = sdr["Item_name"].ToString();

                    }
                    dbcon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void bunifuThinButton221_Click(object sender, EventArgs e)
        {
            try
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);

                MySqlCommand cm = new MySqlCommand("Select wqty,sqty from supermarket.Item where Item_code='" + tcode.Text + "'", dbcon);

                dbcon.Open();
                DataSet d = new DataSet();
                MySqlDataReader sdr = cm.ExecuteReader();

                while (sdr.Read())
                {
                    tws.Text = sdr["wqty"].ToString();
                    int a = Convert.ToInt32(sdr["wqty"].ToString());
                    tss.Text = sdr["sqty"].ToString();
                    int v = Convert.ToInt32(sdr["sqty"].ToString());
                    tt.Text = (a + v).ToString();
                }
                dbcon.Close();
                tmq.Text = (Convert.ToInt32(tss.Text) - Convert.ToInt32(tsc.Text)).ToString();
                if (Convert.ToInt32(tsc.Text) == Convert.ToInt32(tss.Text))
                {
                    MessageBox.Show("No Missing Items found");
                }
                else if (Convert.ToInt32(tsc.Text) < Convert.ToInt32(tss.Text))
                {
                    MessageBox.Show((Convert.ToInt32(tss.Text) - Convert.ToInt32(tsc.Text)) + " Items Missing");
                }
                else
                {
                    MessageBox.Show("Invalid Showroom count");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuThinButton217_Click(object sender, EventArgs e)
        {

            try
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);
                dbcon.Open();
                MySqlCommand cmd2 = new MySqlCommand("insert into supermarket.defItem values('" + dcode.Text + "','" + dbcode.Text + "','" + dsid.Text + "','" + dq.Text + "','" + dreas.Text + "','" + ddate.Text + "')", dbcon);
                MySqlDataReader r2;
                r2 = cmd2.ExecuteReader();
                MessageBox.Show("Inserted successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            resetdef();




        }

        private void bunifuThinButton216_Click(object sender, EventArgs e)
        {
            resetstock();
        }

        private void bunifuThinButton224_Click(object sender, EventArgs e)
        {
            resetret();
        }

        private void bunifuThinButton220_Click(object sender, EventArgs e)
        {
           resetgud();
        }

        private void bunifuThinButton218_Click(object sender, EventArgs e)
        {
            resetdef();
        }

        private void bunifuThinButton222_Click(object sender, EventArgs e)
        {
            resetmiss();
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {

        }

        private void scatcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            scatcombo.Enabled = false;
        }

        private void spname_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void Barcode_Click(object sender, EventArgs e)
        {
           barcode gy = new barcode(brcd.Text, name.Text,code.Text);
           gy.Show();
           

        }

        private void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {
            
        }

        private void bcode_OnTextChange(object sender, EventArgs e)
        {

        }

        private void gbcode_OnValueChanged(object sender, EventArgs e)
        {
            gbcode.Enabled = false;
        }

        private void bcode_OnTextChange_1(object sender, EventArgs e)
        {
            comboBox1.Text = "Category";
            subbox.Text = "Sub Category";
            if (!(bcode.text.Length == 0))
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);

                MySqlCommand cm = new MySqlCommand("Select Item_code as 'Item Code', Item_name as 'Item',Category,Sub_category as 'Sub Category',Rprice as 'Price' from supermarket.item where Barcode='" + bcode.text + "'", dbcon);

                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cm;
                    DataTable set = new DataTable();
                    sda.Fill(set);
                    BindingSource s = new BindingSource();

                    s.DataSource = set;
                    gr.DataSource = s;
                    sda.Update(set);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            else
            {
                loadtable();
            }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@"ItemList.pdf", FileMode.Create));
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

            PdfPTable table = new PdfPTable(gr.Columns.Count);

            //add headers from gridview to table
            iTextSharp.text.Font fonth = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);



            for (int j = 0; j < gr.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(gr.Columns[j].HeaderText.ToUpper(), fonth));
                table.AddCell(cell);

            }

            //flag first row as header
            table.HeaderRows = 1;


            //add actual rows from grid to table
            for (int i = 0; i < gr.Rows.Count; i++)
            {
                table.WidthPercentage = 100;

                for (int k = 0; k < gr.Columns.Count; k++)
                {
                    if (gr[k, i].Value != null)
                    {

                        table.AddCell(new Phrase(gr[k, i].Value.ToString()));
                    }

                }


            }

            //add out table
            doc.Add(table);

            doc.Close();

            System.Diagnostics.Process.Start(@"ItemList.pdf");


        }

        private void sbcode_OnValueChanged(object sender, EventArgs e)
        {
            if (!(sbcode.Text.Length == 0))
            {

                    if (sbcode.Text.Length == 13)
                    {
                    sbcodee.Image = null;
                    }
                    else
                    {
                    sbcodee.Image = Properties.Resources.invalid;
                        ToolTip buttonToolTip = new ToolTip();
                        ToolTip n = new ToolTip();
                        n.SetToolTip(sbcodee, "Barcode ID should have 13 digits");
                    }
             
            }
            if (!(sbcode.Text.Length == 0))
            {

                try
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);

                    MySqlCommand cm = new MySqlCommand("Select Item_code from supermarket.Item where Barcode='" + sbcode.Text + "'", dbcon);

                    dbcon.Open();
                    DataSet d = new DataSet();
                    MySqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        scode.Text = sdr["Item_code"].ToString();
                        
                    }
                    dbcon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void rbcode_OnValueChanged(object sender, EventArgs e)
        {
            if (rbcode.Text.Length == 13 || rbcode.Text.Length == 0)
            {
                rbi.Image = null;
                ToolTip buttonToolTip = new ToolTip();
                ToolTip n = new ToolTip();
                n.SetToolTip(rbi, "");
            }
            else
            {
                rbi.Image = Properties.Resources.invalid;
                ToolTip buttonToolTip = new ToolTip();
                ToolTip n = new ToolTip();
                n.SetToolTip(rbi, "Barcode ID should have 13 digits");
            }
            if (!(rbcode.Text.Length == 0))
            {

                try
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);

                    MySqlCommand cm = new MySqlCommand("Select Item_code from supermarket.Item where Barcode='" + rbcode.Text + "'", dbcon);

                    dbcon.Open();
                    DataSet d = new DataSet();
                    MySqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        rcode.Text = sdr["Item_code"].ToString();

                    }
                    dbcon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void gbcode_OnValueChanged_1(object sender, EventArgs e)
        {


            if (gbcode.Text.Length == 13 || rbcode.Text.Length == 0)
            {
                gbc.Image = null;
                ToolTip buttonToolTip = new ToolTip();
                ToolTip n = new ToolTip();
                n.SetToolTip(gbc, "");
            }
            else
            {
                gbc.Image = Properties.Resources.invalid;
                ToolTip buttonToolTip = new ToolTip();
                ToolTip n = new ToolTip();
                n.SetToolTip(gbc, "Barcode ID should have 13 digits");
            }
            if (!(gbcode.Text.Length == 0))
            {

                try
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);

                    MySqlCommand cm = new MySqlCommand("Select Item_code from supermarket.Item where Barcode='" + gbcode.Text + "'", dbcon);

                    dbcon.Open();
                    DataSet d = new DataSet();
                    MySqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        gcode.Text = sdr["Item_code"].ToString();

                    }
                    dbcon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void dbcode_OnValueChanged_1(object sender, EventArgs e)
        {
            if (dbcode.Text.Length == 13 || rbcode.Text.Length == 0)
            {
                dbc.Image = null;
                ToolTip buttonToolTip = new ToolTip();
                ToolTip n = new ToolTip();
                n.SetToolTip(dbc, "");
            }
            else
            {
                dbc.Image = Properties.Resources.invalid;
                ToolTip buttonToolTip = new ToolTip();
                ToolTip n = new ToolTip();
                n.SetToolTip(dbc, "Barcode ID should have 13 digits");
            }
            if (!(dbcode.Text.Length == 0))
            {

                try
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);

                    MySqlCommand cm = new MySqlCommand("Select Item_code from supermarket.Item where Barcode='" + dbcode.Text + "'", dbcon);

                    dbcon.Open();
                    DataSet d = new DataSet();
                    MySqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        dcode.Text = sdr["Item_code"].ToString();

                    }
                    dbcon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void tbcode_OnValueChanged(object sender, EventArgs e)
        {

            if (tbcode.Text.Length == 13 || rbcode.Text.Length == 0)
            {
                tbc.Image = null;
                ToolTip buttonToolTip = new ToolTip();
                ToolTip n = new ToolTip();
                n.SetToolTip(tbc, "");
            }
            else
            {
                tbc.Image = Properties.Resources.invalid;
                ToolTip buttonToolTip = new ToolTip();
                ToolTip n = new ToolTip();
                n.SetToolTip(tbc, "Barcode ID should have 13 digits");
            }
            if (!(tbcode.Text.Length == 0))
            {

                try
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);

                    MySqlCommand cm = new MySqlCommand("Select Item_code from supermarket.Item where Barcode='" + tbcode.Text + "'", dbcon);

                    dbcon.Open();
                    DataSet d = new DataSet();
                    MySqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        tcode.Text = sdr["Item_code"].ToString();

                    }
                    dbcon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void abcode_OnValueChanged(object sender, EventArgs e)
        {
            if (!(abcode.Text.Length == 0))
            {

                int count = 0;
                try
                {
                    if (abcode.Text.Length == 13)
                    {
                        string con = "datasource=localhost;port=3306;username=root";
                        MySqlConnection dbcon = new MySqlConnection(con);

                        MySqlCommand cm = new MySqlCommand("Select * from supermarket.item where Barcode='" + abcode.Text + "'", dbcon);

                        dbcon.Open();
                        DataSet d = new DataSet();
                        MySqlDataReader sdr = cm.ExecuteReader();

                        while (sdr.Read())
                        {
                            count = count + 1;

                        }
                        dbcon.Close();

                        if (!(count == 0))
                        {
                            bce.Image = Properties.Resources.invalid;
                            ToolTip buttonToolTip = new ToolTip();
                            ToolTip x = new ToolTip();
                            x.SetToolTip(bce, "Barcode ID already Exists");
                        }
                        else
                        {
                            bce.Image = null;
                            ToolTip buttonToolTip = new ToolTip();
                            ToolTip n = new ToolTip();
                            n.SetToolTip(bce, "");
                        }
                    }
                    else
                    {
                        bce.Image = Properties.Resources.invalid;
                        ToolTip buttonToolTip = new ToolTip();
                        ToolTip n = new ToolTip();
                        n.SetToolTip(bce, "Barcode ID should have 13 digits");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void abcode_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void roq_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);


            MySqlCommand cm = new MySqlCommand("Select Item_code as 'code',Item_name as 'Item',sqty as 'Quantity',Floor,Rprice as 'Price',(Rprice*sqty) as 'Total' from supermarket.Item where Floor=1 and sqty>0", dbcon);
            try
            {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cm;
                    DataTable set = new DataTable();
                    sda.Fill(set);
                    BindingSource s = new BindingSource();

                    s.DataSource = set;
                    hidetb.DataSource = s;
                    sda.Update(set);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@"1floor.pdf", FileMode.Create));
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

            PdfPTable table = new PdfPTable(hidetb.Columns.Count);

            //add headers from gridview to table
            iTextSharp.text.Font fonth = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);



            for (int j = 0; j < hidetb.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(hidetb.Columns[j].HeaderText.ToUpper(), fonth));
                table.AddCell(cell);

            }

            //flag first row as header
            table.HeaderRows = 1;


            //add actual rows from grid to table
            for (int i = 0; i < hidetb.Rows.Count; i++)
            {
                table.WidthPercentage = 100;

                for (int k = 0; k < hidetb.Columns.Count; k++)
                {
                    if (hidetb[k, i].Value != null)
                    {

                        table.AddCell(new Phrase(hidetb[k, i].Value.ToString()));
                    }

                }


            }

            //add out table
            doc.Add(table);

            doc.Close();

            System.Diagnostics.Process.Start(@"1floor.pdf");


        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);


            MySqlCommand cm = new MySqlCommand("Select Item_code as 'code',Item_name as 'Item',sqty as 'Quantity',Floor,Rprice as 'Price',(Rprice*sqty) as 'Total' from supermarket.Item where Floor=2 and sqty>0", dbcon);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cm;
                DataTable set = new DataTable();
                sda.Fill(set);
                BindingSource s = new BindingSource();

                s.DataSource = set;
                hidetb.DataSource = s;
                sda.Update(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@"floor.pdf", FileMode.Create));
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

            PdfPTable table = new PdfPTable(hidetb.Columns.Count);

            //add headers from gridview to table
            iTextSharp.text.Font fonth = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);



            for (int j = 0; j < hidetb.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(hidetb.Columns[j].HeaderText.ToUpper(), fonth));
                table.AddCell(cell);

            }

            //flag first row as header
            table.HeaderRows = 1;


            //add actual rows from grid to table
            for (int i = 0; i < hidetb.Rows.Count; i++)
            {
                table.WidthPercentage = 100;

                for (int k = 0; k < hidetb.Columns.Count; k++)
                {
                    if (hidetb[k, i].Value != null)
                    {

                        table.AddCell(new Phrase(hidetb[k, i].Value.ToString()));
                    }

                }


            }

            //add out table
            doc.Add(table);

            doc.Close();

            System.Diagnostics.Process.Start(@"1floor.pdf");


        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);


            MySqlCommand cm = new MySqlCommand("Select Item_code as 'code',Item_name as 'Item',sqty as 'Quantity',Floor,Rprice as 'Price',(Rprice*sqty) as 'Total' from supermarket.Item where Floor=3 and sqty>0", dbcon);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cm;
                DataTable set = new DataTable();
                sda.Fill(set);
                BindingSource s = new BindingSource();

                s.DataSource = set;
                hidetb.DataSource = s;
                sda.Update(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@"1floor.pdf", FileMode.Create));
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

            PdfPTable table = new PdfPTable(hidetb.Columns.Count);

            //add headers from gridview to table
            iTextSharp.text.Font fonth = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);



            for (int j = 0; j < hidetb.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(hidetb.Columns[j].HeaderText.ToUpper(), fonth));
                table.AddCell(cell);

            }

            //flag first row as header
            table.HeaderRows = 1;


            //add actual rows from grid to table
            for (int i = 0; i < hidetb.Rows.Count; i++)
            {
                table.WidthPercentage = 100;

                for (int k = 0; k < hidetb.Columns.Count; k++)
                {
                    if (hidetb[k, i].Value != null)
                    {

                        table.AddCell(new Phrase(hidetb[k, i].Value.ToString()));
                    }

                }


            }

            //add out table
            doc.Add(table);

            doc.Close();

            System.Diagnostics.Process.Start(@"floor.pdf");


        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);


            MySqlCommand cm = new MySqlCommand("Select Item_code as 'code',Item_name as 'Item',sqty as 'Quantity',Floor,Rprice as 'Price',(Rprice*sqty) as 'Total' from supermarket.Item where Floor=4 and sqty>0", dbcon);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cm;
                DataTable set = new DataTable();
                sda.Fill(set);
                BindingSource s = new BindingSource();

                s.DataSource = set;
                hidetb.DataSource = s;
                sda.Update(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@"floor.pdf", FileMode.Create));
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

            PdfPTable table = new PdfPTable(hidetb.Columns.Count);

            //add headers from gridview to table
            iTextSharp.text.Font fonth = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);



            for (int j = 0; j < hidetb.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(hidetb.Columns[j].HeaderText.ToUpper(), fonth));
                table.AddCell(cell);

            }

            //flag first row as header
            table.HeaderRows = 1;


            //add actual rows from grid to table
            for (int i = 0; i < hidetb.Rows.Count; i++)
            {
                table.WidthPercentage = 100;

                for (int k = 0; k < hidetb.Columns.Count; k++)
                {
                    if (hidetb[k, i].Value != null)
                    {

                        table.AddCell(new Phrase(hidetb[k, i].Value.ToString()));
                    }

                }


            }

            //add out table
            doc.Add(table);

            doc.Close();

            System.Diagnostics.Process.Start(@"floor.pdf");


        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);


            MySqlCommand cm = new MySqlCommand("Select Item_code as 'code',Item_name as 'Item',wqty as 'Quantity',Rprice as 'Price',(Rprice*wqty) as 'Total' from supermarket.Item where wqty>0", dbcon);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cm;
                DataTable set = new DataTable();
                sda.Fill(set);
                BindingSource s = new BindingSource();

                s.DataSource = set;
                hidetb.DataSource = s;
                sda.Update(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@"floor.pdf", FileMode.Create));
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

            PdfPTable table = new PdfPTable(hidetb.Columns.Count);

            //add headers from gridview to table
            iTextSharp.text.Font fonth = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);



            for (int j = 0; j < hidetb.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(hidetb.Columns[j].HeaderText.ToUpper(), fonth));
                table.AddCell(cell);

            }

            //flag first row as header
            table.HeaderRows = 1;


            //add actual rows from grid to table
            for (int i = 0; i < hidetb.Rows.Count; i++)
            {
                table.WidthPercentage = 100;

                for (int k = 0; k < hidetb.Columns.Count; k++)
                {
                    if (hidetb[k, i].Value != null)
                    {

                        table.AddCell(new Phrase(hidetb[k, i].Value.ToString()));
                    }

                }


            }

            //add out table
            doc.Add(table);

            doc.Close();

            System.Diagnostics.Process.Start(@"floor.pdf");


        }

        private void abrand_KeyPress_1(object sender, KeyPressEventArgs e)
        {

        }

        private void abcode_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void tqty_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void tqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void roq_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void afloor_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void psize_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void rpr_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void whpr_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void aq1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void ap1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void aq2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void ap2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void aq3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void ap3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void aq4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void ap4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void scode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void sbcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void srpr_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void saqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void snqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void stqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void sup_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void rcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void rbcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void rqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void rrpr_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void rded_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void gcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void gbcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void gamnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void dcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void dbcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void dq_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void tcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void tbcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void tsc_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tss_OnValueChanged(object sender, EventArgs e)
        {
            tss.Enabled = false;
        }

        private void tws_OnValueChanged(object sender, EventArgs e)
        {
            tws.Enabled = false;
        }

        private void tt_OnValueChanged(object sender, EventArgs e)
        {
            tt.Enabled = false;
        }

        private void tmq_OnValueChanged(object sender, EventArgs e)
        {
            tmq.Enabled = false;
        }

        private void aname_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void sname_OnValueChanged(object sender, EventArgs e)
        {
            sname.Enabled = false;
        }

        private void ssubcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ssubcombo.Enabled = false;
        }

        private void sbrand_OnValueChanged(object sender, EventArgs e)
        {
            sbrand.Enabled = false;
        }

        private void srpr_OnValueChanged(object sender, EventArgs e)
        {
            srpr.Enabled = false;
        }

        private void saqty_OnValueChanged(object sender, EventArgs e)
        {
            saqty.Enabled = false;
        }

        private void stqty_OnValueChanged(object sender, EventArgs e)
        {
            stqty.Enabled = false;
        }

        private void rname_OnValueChanged(object sender, EventArgs e)
        {
            rname.Enabled = false;
        }

        private void rcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            rcombo.Enabled = false;
        }

        private void rsubcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            rsubcombo.Enabled = false;
        }

        private void rbrand_OnValueChanged(object sender, EventArgs e)
        {
            rbrand.Enabled = false;
        }
    }
}
