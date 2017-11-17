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
using System.IO;
using System.Text.RegularExpressions;
using WinFormCharpWebCam;

namespace madushaTemp
{
    public partial class debtors : Form
    {
        int a;
        int b;
        int c;
        public debtors()
        {
            InitializeComponent();
            dobpicker.MaxDate = DateTime.Now;
            tableLoadCustomer();

            webcam = new WebCam();
            webcam.InitializeWebCam(ref picface);
            webcam.Start();

            ToolTip n = new ToolTip();
            n.SetToolTip(pictureBox1, "Capture Image");

            ToolTip n1 = new ToolTip();
            n1.SetToolTip(pictureBox3, "Stop Webcam");

            ToolTip n2 = new ToolTip();
            n2.SetToolTip(pictureBox4, "Save Image");
        }
        String picLoc1;
        WebCam webcam;


        public string nic
        { get; set; }
        public string fname
        { get; set; }
        public string nwi
        { get; set; }
        public string addr
        { get; set; }
        public string ema
        { get; set; }
        public string telephone
        { get; set; }
        public string dob
        { get; set; }
        public string job
        { get; set; }
        public string city
        { get; set; }

        public void Regexp2(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {


            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {

                pc.Image = inventory.Properties.Resources.invalid;
                n.SetToolTip(picerror, "NIC should have 9 digits followed with v or x");
                a = -99;

            }
            else
            {
                pc.Image = null;
                a = 0;

            }

        }

        public void Regexp3(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {

            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {

                pc.Image = inventory.Properties.Resources.invalid;
                n.SetToolTip(picemail, "Email Should have @ , . Symbols");
                b = -99;

            }
            else
            {
                pc.Image = null;
                b = 0;

            }

        }
        public void Regexp4(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {

            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {

                pc.Image = inventory.Properties.Resources.invalid;
                n.SetToolTip(picphon, "Phone number should have exactly 10 digits");
                c = -99;


            }
            else
            {
                pc.Image = null;
                c = 0;

            }

        }


        public void tableLoadCustomer()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select nic as NIC,fullname as Full_Name,nwinitials as Name_with_initials,dob as DOB,phone as Phone,address as Address,email as Email,city as City,job as Job from supermarket.customer ;", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable tab = new DataTable();
                sda.Fill(tab);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = tab;
                tablcust.DataSource = bsource;
                sda.Update(tab);
                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }




        }



        private void debtors_Load(object sender, EventArgs e)
        {
             search("");


            //webcam = new WebCam();
            //webcam.InitializeWebCam(ref picface);
            //webcam.Start();


        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            webcam.Stop();
            this.Close();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            Regexp3(@"^([\w]+)@([\w]+)\.([\w]+)$", txtema, picemail, "email");
            Regexp4(@"^\d{10}$", txtphone, picphon, "phone2");
            Regexp2(@"^\d{9}(x|v)$", txtnic, picerror, "NIC");

            if (String.IsNullOrEmpty(txtnic.Text) || String.IsNullOrWhiteSpace(txtfname.Text) || String.IsNullOrWhiteSpace(txtnwi.Text) || String.IsNullOrWhiteSpace(txtaddress.Text) || String.IsNullOrWhiteSpace(txtema.Text) || String.IsNullOrWhiteSpace(txtphone.Text) || String.IsNullOrWhiteSpace(txtjob.Text) || String.IsNullOrWhiteSpace(txtcity.Text)||String.IsNullOrEmpty(lblpathpicnicf.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (a == -99 || b == -99 || c == -99)
            { }

            else if (a == 0 && b == 0 && c == 0)
            {
                    

                    byte[] imagebtf = null;
                    FileStream fstream = new FileStream(this.lblpathpicnicf.Text, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fstream);
                    imagebtf = br.ReadBytes((int)fstream.Length);


                    nic = this.txtnic.Text;
                    fname = this.txtfname.Text;
                    nwi = this.txtnwi.Text;
                    addr = this.txtaddress.Text;
                    ema = this.txtema.Text;
                    telephone = this.txtphone.Text;
                    dob = this.dobpicker.Text;
                    job = this.txtjob.Text;
                    city = this.txtcity.Text;

                    string constring = "datasource=localhost;port=3306;username=root";
                    string query = "insert into supermarket.customer(nic,fullname,nwinitials,address,email,phone,dob,job,city,nicimage,faceimage)  values('" + nic + "','" + fname + "','" + nwi + "','" + addr + "','" + ema + "','" + telephone + "','" + dob + "','" + job + "','" + city + "',@IMGF,@IMGF);";
                    MySqlConnection con = new MySqlConnection(constring);

                    MySqlCommand cmd = new MySqlCommand(query, con);

                    MySqlDataReader myR;



                    try
                    {
                        con.Open();
                        cmd.Parameters.Add(new MySqlParameter("@IMGF", imagebtf));

                        myR = cmd.ExecuteReader();
                        MessageBox.Show("Customer Added Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        while (myR.Read()) { }
                        tableLoadCustomer();
                        con.Close();
                        txtnic.Text = "";
                        txtfname.Text = "";
                        txtnwi.Text = "";
                        txtaddress.Text = "";
                        txtema.Text = "";
                        txtphone.Text = "";
                        txtjob.Text = "";
                        txtcity.Text = "";
                    }

                    catch (Exception)
                    {

                        MessageBox.Show("Customer Already Exists !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                
            }
        }

        private void tablcust_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);
            MySqlCommand cm = new MySqlCommand("Select nicimage,faceimage from supermarket.customer where nic='" + txtnic.Text + "' ", dbcon);
            dbcon.Open();
            DataSet d = new DataSet();
            MySqlDataReader sdr = cm.ExecuteReader();

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.tablcust.Rows[e.RowIndex];
                txtnic.Text = row.Cells["NIC"].Value.ToString();
                txtfname.Text = row.Cells["Full_Name"].Value.ToString();
                txtnwi.Text = row.Cells["Name_with_initials"].Value.ToString();
                txtaddress.Text = row.Cells["Address"].Value.ToString();
                txtema.Text = row.Cells["Email"].Value.ToString();
                txtphone.Text = row.Cells["Phone"].Value.ToString();
                dobpicker.Text = row.Cells["DOB"].Value.ToString();
                txtjob.Text = row.Cells["Job"].Value.ToString();
                txtcity.Text = row.Cells["City"].Value.ToString();


                while (sdr.Read())
                {
                    byte[] img = (byte[])(sdr["nicimage"]);
                    if (img == null)
                    {
                        nicpic.Image = null;
                    }
                    else
                    {
                        nicpic.SizeMode = PictureBoxSizeMode.StretchImage;
                        MemoryStream mstream = new MemoryStream(img);
                        nicpic.Image = System.Drawing.Image.FromStream(mstream);
                    }
                }
            }

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (txtnic.Text != "")
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update supermarket.customer set fullname='" + this.txtfname.Text + "' , nwinitials='" + this.txtnwi.Text + "', address='" + this.txtaddress.Text + "', nwinitials='" + this.txtnwi.Text + "', email='" + this.txtema.Text + "', phone='" + this.txtphone.Text + "', dob='" + this.dobpicker.Text + "', job='" + this.txtjob.Text + "', city='" + this.txtcity.Text + "' where nic='" + this.txtnic.Text + "'", conn);

                    cmd.ExecuteNonQuery();



                    MessageBox.Show("Data Updated Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    tableLoadCustomer();
                    txtnic.Text = "";
                    txtfname.Text = "";
                    txtnwi.Text = "";
                    txtaddress.Text = "";
                    txtema.Text = "";
                    txtphone.Text = "";
                    dobpicker.Text = "";
                    txtjob.Text = "";
                    txtcity.Text = "";
                    conn.Close();
                }
                catch (Exception r)
                {

                    MessageBox.Show(r.Message);
                }
            }
            else { MessageBox.Show("Please Select a Record to be Updated !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (txtnic.Text != "")
            {
                if (DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    try
                    {
                        MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("delete from supermarket.customer where nic='" + this.txtnic.Text + "'", conn);

                        cmd.ExecuteNonQuery();



                        MessageBox.Show("Deleted Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tableLoadCustomer();
                        txtnic.Text = "";
                        txtfname.Text = "";
                        txtnwi.Text = "";
                        txtaddress.Text = "";
                        txtema.Text = "";
                        txtphone.Text = "";
                        dobpicker.Text = "";
                        txtjob.Text = "";
                        txtcity.Text = "";
                        nicpic.Image = null;
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
        private void txtphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void uploadNic_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|JPEG Files(*.jpeg)|*.jpeg|All Files(*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                picLoc1 = dlg.FileName.ToString();
                lblpathpicnicf.Text = picLoc1;
                lblup.Text = "Uploaded";
                Image image = Bitmap.FromFile(dlg.FileName);
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            nicpic.Image = picface.Image;
        }


        public void search(string valueTo)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");

            MySqlCommand cmd = new MySqlCommand("select nic as NIC,fullname as Full_Name,nwinitials as Name_with_initials,dob as DOB,phone as Phone,address as Address,email as Email,city as City,job as Job from supermarket.customer where nic like '" + valueTo + "%' ", conn);

            MySqlDataAdapter sda = new MySqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable dbt = new DataTable();
            sda.Fill(dbt);
            BindingSource bS = new BindingSource();

            bS.DataSource = dbt;
            tablcust.DataSource = bS;


        }

        private void nicsearchcust_Click(object sender, EventArgs e)
        {
            string valueTo = txtsearchcust.Text.ToString();
            search(valueTo);
        }

        private void bunifuThinButton29_Click(object sender, EventArgs e)
        {
            tableLoadCustomer();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            webcam.Stop();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            nicpic.Image = picface.Image;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            webcam.Stop();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            Helper.SaveImageCapture(nicpic.Image);
        }
    }
}
