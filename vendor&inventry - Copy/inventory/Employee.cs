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
using MySql.Data;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Net.Mail;


namespace madushaTemp
{
    public partial class Employee : Form
    {
        //private object start_time;

        public Employee()
        {
            InitializeComponent();

     

            loadtable();
            loadtable2();
            loadsalary();
        }
        DataTable dbdataset;
        String gender;
        Boolean cat = true;
        int i = 1050;
        
        int n = 001;
        //validation

        public void Regexp(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {
           
            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {
           
                pc.Image = inventory.Properties.Resources.invalid;
               
                //leaving form
              
                n.SetToolTip(pictureBox18, "Please include only Integer values");
              
                //employee management
                n.SetToolTip(pictureBox11, "Please include your full name");
                n.SetToolTip(pictureBox12, "Please include your name with initial ");
                n.SetToolTip(pictureBox13, "Please include your address");
                n.SetToolTip(pictureBox8, "Please include only integer values with correct length in your phone");
                n.SetToolTip(pictureBox8, "Please include only integer values with correct length in your phone");
                n.SetToolTip(pictureBox10, "Please include correct values in your NIC");
                n.SetToolTip(pictureBox1, "Please include @ , . in your email.");
                //salary
                n.SetToolTip(pictureBox21, "Please include  employee id");
                n.SetToolTip(pictureBox22, "Please include position");
                cat = false;

            }
            else
            {
                pc.Image = null;

            }

        }



        public void loadtable()
        {
            // string x = DateTime.Now.ToString("yyyy-mm-dd");
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);
            DateTime time = DateTime.Now;
            string formatD = "yyyy-MM-dd";

            string TimeValue = time.ToString(formatD);

            MySqlCommand cm = new MySqlCommand("select d.Empid as 'Employee Id',d.full_name as 'Employee Name',a.arrival as 'Arrival Time',a.end_time as 'leaving Time'  from supermarket.employee_details d,supermarket.attendance a where d.empid=a.empid and date='" + TimeValue + "';", dbcon);

            try
            {

                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cm;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dbdataset;
                gr.DataSource = bsource;
                sda.Update(dbdataset);




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void loadtable2()
        {
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);
            MySqlCommand cm = new MySqlCommand("select Empid as 'Employee Id',full_name as 'Employee Name',Email as 'email',NIC,position,phone as 'Phone'  from supermarket.employee_details ;", dbcon);
           


            try
            {

                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cm;
                dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dbdataset;
                grr.DataSource = bsource;
                sda.Update(dbdataset);




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


       

         public void loadsalary()
    {
        string con = "datasource=localhost;port=3306;username=root";
        MySqlConnection dbcon = new MySqlConnection(con);
        MySqlCommand cm = new MySqlCommand("Select Empid as 'Employee Id',full_name as 'Employee Name',position as 'Position',phone as 'Phone',address as 'Address',email as 'Email' from supermarket.employee_details ; ", dbcon);



        try
        {
            MySqlDataAdapter sda = new MySqlDataAdapter();
            sda.SelectCommand = cm;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            BindingSource bsource = new BindingSource();

            bsource.DataSource = dbdataset;
            bunifuCustomDataGrid3.DataSource = bsource;
            sda.Update(dbdataset);




        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }







        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox3_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label69_Click(object sender, EventArgs e)
        {

        }

        private void label61_Click(object sender, EventArgs e)
        {

        }

        private void bunifuDropdown1_onItemSelected(object sender, EventArgs e)
        {

        }

        private void bunifuDropdown3_onItemSelected(object sender, EventArgs e)
        {

        }

        private void label68_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuDropdown2_onItemSelected_1(object sender, EventArgs e)
        {

        }

        private void label64_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btniconfirm_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(aeid.Text) )
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {

                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);
                //string query = ("insert into supermarket.attendance(empid,sdate,,edate,nodates,reason,approve,sdate) values('" + this.id.Text + "' ,'" + this.name.Text + "', '" + this.start.Text + "' ,'" + this.end.Text + "' , '" + this.dates.Text + "' ,'" + this.reason.Text + "' ,'" + this.approve.Text + '")", dbcon);
                MySqlCommand cm = new MySqlCommand("insert into supermarket.attendance(empid,arrival,date) values('" + this.aeid.Text + "','" + this.astart.Text + "','" + this.ddd.Text + "')", dbcon);
                MySqlDataReader r;
                try
                {
                    dbcon.Open();
                    r = cm.ExecuteReader();
                    MessageBox.Show("Inserted successfully!");
                    {

                    }
                    loadtable();
                    dbcon.Close();
                    aeid.Text = "";
                    ddd.Text = "";
                    barcode.Text = "";
                    astart.Text = "";






                }
                catch (Exception)
                {
                    MessageBox.Show("Employee already present");
                }
            }
        }

        private void aend_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton211_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void aeid_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel10_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel125_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel127_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel129_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel18_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel20_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel17_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel12_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel9_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel8_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel7_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Male";
        }

        private void bunifuMetroTextbox14_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomDataGrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Employee_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'supermarketDataSet.employee_details' table. You can move, or remove it, as needed.
            //this.employee_detailsTableAdapter.Fill(this.supermarketDataSet.employee_details);

        }

        private void bunifuThinButton22_Click_1(object sender, EventArgs e)
        {
            LeaveForm f2 = new LeaveForm();
            f2.ShowDialog();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

       

        private void email_Click(object sender, EventArgs e)
        {
          
        }

        private void bunifuMetroTextbox7_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox11_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Female";
        }

        private void reports_Click(object sender, EventArgs e)
        {

        }

     

        private void gr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            aeid.Enabled = false;     
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {

        }

        private void srefresh_Click(object sender, EventArgs e)
        {

        }

        private void SendMail_Click(object sender, EventArgs e)
        {

        }

        private void Eadd_Click(object sender, EventArgs e)
        {
            int g= 10010;
            g++;
            i++;
            if (String.IsNullOrEmpty(fname.Text) || String.IsNullOrWhiteSpace(iname.Text) || String.IsNullOrWhiteSpace(position.Text) || String.IsNullOrWhiteSpace(phone.Text) || String.IsNullOrWhiteSpace(NIC.Text) || String.IsNullOrWhiteSpace(address.Text) || String.IsNullOrWhiteSpace(mail.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else { 
            cat = true;
         
            Regexp(@"[0-9]{9}[v|V]$", NIC ,pictureBox10, "NIC");
            Regexp(@"^[0-9]{10}$", phone, pictureBox8, "phone");
            Regexp(@"^([\w]+)@([\w]+)\.([\w]+)$", mail, pictureBox1, "Mail");


                if (cat == true)
                {

                    byte[] imageBt = null;
                    FileStream fstream = new FileStream(this.textBox_image_path.Text, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fstream);
                    imageBt = br.ReadBytes((int)fstream.Length);

                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);
                    MySqlCommand cm = new MySqlCommand("insert into supermarket.employee_details(Empid,full_name,i_name,gender,position,phone,DOB,sdate,NIC,address,email,image,barcode) values('" + i + "','" + this.fname.Text + "' ,'" + this.iname.Text + "'  ,'" + gender + "','" + this.position.Text + "' ,'" + this.phone.Text + "','" + this.DOB.Text + "' ,'" + this.startdate.Text + "' ,'" + this.NIC.Text + "' ,'" + this.address.Text + "' ,'" + this.mail.Text + "',@IMG,'" + this.barcode.Text + "'  )", dbcon);
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


                        loadtable2();

                        dbcon.Close();
                        eid.Text = "";
                        fname.Text = "";
                        iname.Text = "";
                        phone.Text = "";
                        NIC.Text = "";
                        address.Text = "";
                        mail.Text = "";
                        position.Text = "";
                        pictureBox2.Image = null;



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void eid_OnValueChanged(object sender, EventArgs e)
        {

        }

       


        private void grr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.grr.Rows[e.RowIndex];

                eid.Text = row.Cells["Employee Id"].Value.ToString();
                fname.Text = row.Cells["Employee Name"].Value.ToString();
               


                try
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);

                    MySqlCommand cm = new MySqlCommand("Select Empid,full_name,i_name,gender,position,phone,DOB,sdate,NIC,address,email,image from supermarket.employee_details where empid='" + eid.Text + "'", dbcon);

                    dbcon.Open();
                    DataSet d = new DataSet();
                    MySqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {


                       
                        eid.Text = sdr["Empid"].ToString();
                        fname.Text = sdr["full_name"].ToString();
                        iname.Text = sdr["i_name"].ToString();
                        //int w = Int32.Parse(sdr["wqty"].ToString());
                        //int s = Int32.Parse(sdr["sqty"].ToString());
                        //gender.Text = sdr["gender"].ToString();
                        position.Text = sdr["position"].ToString();
                        phone.Text = sdr["phone"].ToString();
                        DOB.Text = sdr["DOB"].ToString();
                        startdate.Text = sdr["sdate"].ToString();
                        NIC.Text = sdr["NIC"].ToString();
                        address.Text = sdr["address"].ToString();
                        mail.Text = sdr["email"].ToString();

                        byte[] image = (byte[])(sdr["image"]);
                        if (image == null)
                        {
                            pictureBox2.Image = null;
                        }
                        else
                        {
                            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                            MemoryStream mstream = new MemoryStream(image);
                            pictureBox2.Image = System.Drawing.Image.FromStream(mstream);
                        }


                        
                    }
                    dbcon.Close();





                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        

     

     

        private void att_Click(object sender, EventArgs e)
        {
            EditAttendance f2 = new EditAttendance();
            f2.Show();
        }

        private void ma_Click(object sender, EventArgs e)
        {
            sendEmail f2 = new sendEmail();
            f2.Show();
        }

        private void leave_Click(object sender, EventArgs e)
        {
            LeaveForm f2 = new LeaveForm();
            f2.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void month_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void panel11_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void groupBox20_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void bunifuCustomDataGrid3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.bunifuCustomDataGrid3.Rows[e.RowIndex];

                empid.Text = row.Cells["Employee Id"].Value.ToString();
                ppp.Text = row.Cells["Position"].Value.ToString();
               


                try
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);

                     MySqlCommand cm = new MySqlCommand("Select s.type,s.overtime,s.bonus,s.basic,s.epf,s.workinghrs,s.loan,s.whrs,s.ohrs from supermarket.employee_details e,supermarket.salary s where e.Empid=s.empid and s.empid='" + empid.Text + "'", dbcon);

                   // MySqlCommand cm = new MySqlCommand("Select * from supermarket.employee_details e,supermarket.salary s where e.Empid=s.Empid'" + eid.Text + "'", dbcon);

                    dbcon.Open();
                    DataSet d = new DataSet();
                    MySqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {

                        //if (position=="permanent") {



                        //}
                        
                        //empid.Text = sdr["Empid"].ToString();
                        hh.Text = sdr["workinghrs"].ToString();
                       type.Text = sdr["type"].ToString();
                        
                        oo.Text = sdr["overtime"].ToString();
                        bb.Text = sdr["basic"].ToString();
                        bbb.Text = sdr["bonus"].ToString();
                        ee.Text = sdr["epf"].ToString();
                        ll.Text = sdr["loan"].ToString();
                        whrs.Text = sdr["whrs"].ToString();
                        ohrs.Text = sdr["ohrs"].ToString();


                    }
                    dbcon.Close();





                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void bunifuMetroTextbox21_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void iid_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void position_onItemSelected(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void image_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dig = new OpenFileDialog();
            dig.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";

            if (dig.ShowDialog() == DialogResult.OK)
            {
                string picloc = dig.FileName.ToString();
                textBox_image_path.Text = picloc;
                pictureBox2.ImageLocation = picloc;


            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NIC_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void phone_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void refresh_Click(object sender, EventArgs e)
        {
            loadtable2();
        }

        private void eid_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click_1(object sender, EventArgs e)
        {
            loadtable2();

            eid.Text = "";
            fname.Text = "";
            iname.Text = "";
            position.Text = "";
            phone.Text = "";
            NIC.Text = "";
            address.Text = "";
            mail.Text = "";
            pictureBox2.Image = null;


        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(fname.Text) || String.IsNullOrWhiteSpace(iname.Text) || String.IsNullOrWhiteSpace(position.Text) || String.IsNullOrWhiteSpace(phone.Text) || String.IsNullOrWhiteSpace(NIC.Text) || String.IsNullOrWhiteSpace(address.Text) || String.IsNullOrWhiteSpace(mail.Text))
            {
                MessageBox.Show("Please Select Raw First!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                byte[] imageBt = null;
                FileStream fstream = new FileStream(this.textBox_image_path.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fstream);
                imageBt = br.ReadBytes((int)fstream.Length);

                string constring = "datasource=localhost;port=3306;username=root";
                string query = "update supermarket.employee_details set full_name='" + this.fname.Text + "' ,i_name='" + this.iname.Text + "'  ,position='" + this.position.Text + "' ,phone='" + this.phone.Text + "',DOB='" + this.DOB.Text + "' ,sdate='" + this.startdate.Text + "' ,NIC='" + this.NIC.Text + "' ,address='" + this.address.Text + "' ,email='" + this.mail.Text + "',image=@IMG where Empid='" + this.eid.Text + "'; ";
                MySqlConnection dbcon = new MySqlConnection(constring);
                MySqlCommand cmdb = new MySqlCommand(query, dbcon);
                MySqlDataReader reader;



                try
                {
                    dbcon.Open();
                    cmdb.Parameters.Add(new MySqlParameter("@IMG", imageBt));

                    reader = cmdb.ExecuteReader();
                    MessageBox.Show("Updated successfully!");
                    while (reader.Read())
                    {

                    }

                    loadtable2();
                    dbcon.Close();


                    eid.Text = "";
                    fname.Text = "";
                    iname.Text = "";
                    phone.Text = "";
                    NIC.Text = "";
                    address.Text = "";
                    mail.Text = "";
                    position.Text = "";
                    pictureBox2.Image = null;


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(id.Text) || String.IsNullOrWhiteSpace(name.Text) || String.IsNullOrWhiteSpace(reason.Text) || String.IsNullOrWhiteSpace(dates.Text) || String.IsNullOrWhiteSpace(approve.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           


         else
            {
                cat = true;
                Regexp(@"^[0-9][0-9]$", dates, pictureBox18, "nodates");

                if (cat == true)
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);
                    //string query = ("insert into supermarket.attendance(empid,sdate,,edate,nodates,reason,approve,sdate) values('" + this.id.Text + "' ,'" + this.name.Text + "', '" + this.start.Text + "' ,'" + this.end.Text + "' , '" + this.dates.Text + "' ,'" + this.reason.Text + "' ,'" + this.approve.Text + '")", dbcon);
                    MySqlCommand cm = new MySqlCommand("insert into supermarket.leaving(empid,name,sdate,edate,nodates,reason,approve) values('" + this.id.Text + "','" + this.name.Text + "','" + this.start.Text + "','" + this.end.Text + "','" + this.dates.Text + "','" + this.reason.Text + "','" + this.approve.Text + "')", dbcon);
                    MySqlDataReader r;





                    try
                    {
                        dbcon.Open();
                        r = cm.ExecuteReader();
                        MessageBox.Show("Inserted successfully!");



                        while (r.Read())
                        {

                        }




                        dbcon.Close();
                        id.Text = "";
                        name.Text = "";
                        reason.Text = "";
                        dates.Text = "";
                        approve.Text = "";









                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            

        }

        private void load_Click(object sender, EventArgs e)
        {

            

                string constring = "datasource=localhost;port=3306;username=root";

                string query = "select * from supermarket.salary where month='" + mmm.Text + "' ;";
                MySqlConnection dbcon = new MySqlConnection(constring);
                MySqlCommand cmdb = new MySqlCommand(query, dbcon);
                MySqlDataReader reader;



                try
                {
                    dbcon.Open();


                    reader = cmdb.ExecuteReader();

                    while (reader.Read())
                    {
                        this.chart1.Series["Working Hours"].Points.AddXY(reader.GetInt32("empid"), reader.GetInt32("whrs"));
                    }


                  



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            

        }

        private void SendMail_Click_1(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(fname.Text))
            {
                MessageBox.Show("Please select Raw First!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string constring = "datasource=localhost;port=3306;username=root";
                string query = "delete from supermarket.employee_details where Empid='" + this.eid.Text + "'; ";
                MySqlConnection dbcon = new MySqlConnection(constring);
                MySqlCommand cmdb = new MySqlCommand(query, dbcon);
                MySqlDataReader reader;



                try
                {
                    dbcon.Open();


                    reader = cmdb.ExecuteReader();
                    MessageBox.Show("Deleted successfully!");
                    while (reader.Read())
                    {

                    }


                    dbcon.Close();
                    eid.Text = "";
                    fname.Text = "";
                    iname.Text = "";
                    phone.Text = "";
                    NIC.Text = "";
                    address.Text = "";
                    mail.Text = "";
                    position.Text = "";
                    pictureBox2.Image = null;





                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void per_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void add_Click(object sender, EventArgs e)
        {
            n++;
            if (String.IsNullOrEmpty(empid.Text) || String.IsNullOrWhiteSpace(ppp.Text) )
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }





            else
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);
                // MySqlCommand cm = new MySqlCommand("insert into supermarket.salary(empid,position,type,overtime,workinghrs,bonus,basic,epf,loan,sid) values('" + this.empid.Text + "' ,'" + this.ppp.Text + "' ,'" + this.type.Text + "' ,'" + this.oo.Text + "','" + this.hh.Text + "' ,'" + this.bbb.Text + "' ,'" + this.bb.Text + "' ,'" + this.ee.Text + "' ,'" + this.ll.Text + "','" + n + "',whrs='" + this.whrs.Text + "',ohrs='" + this.ohrs.Text + "'  )", dbcon);
                MySqlCommand cm = new MySqlCommand("insert into supermarket.salary(empid,position,type,overtime,workinghrs,bonus,basic,epf,loan,sid,whrs,ohrs,salary) values('" + this.empid.Text + "' ,'" + this.ppp.Text + "' ,'" + this.type.Text + "' ,'" + this.oo.Text + "','" + this.hh.Text + "' ,'" + this.bbb.Text + "' ,'" + this.bb.Text + "' ,'" + this.ee.Text + "' ,'" + this.ll.Text + "','" + n + "',whrs='" + this.whrs.Text + "',ohrs='" + this.ohrs.Text + "',salary='" + this.sss.Text + "'  )", dbcon);

                MySqlDataReader r;



                try
                {
                    dbcon.Open();


                    r = cm.ExecuteReader();
                    MessageBox.Show("Inserted successfully!");



                    while (r.Read())
                    {

                    }


                    loadtable2();

                    dbcon.Close();
                    empid.Text = "";
                    ppp.Text = "";
                    type.Text = "";
                    oo.Text = "";
                    hh.Text = "";
                    bbb.Text = "";
                    bb.Text = "";
                    ee.Text = "";
                    //month.Text = "";
                    ll.Text = "";
                    whrs.Text = "";
                    ohrs.Text = "";



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void nn_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void update_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(empid.Text) || String.IsNullOrWhiteSpace(ppp.Text))
            {
                MessageBox.Show("Please select raw first ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string constring = "datasource=localhost;port=3306;username=root";
                string query = "update supermarket.salary set position='" + this.ppp.Text + "' ,type='" + this.type.Text + "'  ,overtime='" + this.oo.Text + "' ,workinghrs='" + this.hh.Text + "',bonus='" + this.bbb.Text + "' ,basic='" + this.bb.Text + "' ,epf='" + this.ee.Text + "' ,loan='" + this.ll.Text + "',whrs='" + this.whrs.Text + "',ohrs='" + this.ohrs.Text + "',salary='" + this.sss.Text + "'  where empid='" + this.empid.Text + "'; ";
                MySqlConnection dbcon = new MySqlConnection(constring);
                MySqlCommand cmdb = new MySqlCommand(query, dbcon);
                MySqlDataReader reader;



                try
                {
                    dbcon.Open();


                    reader = cmdb.ExecuteReader();
                    MessageBox.Show("Updated successfully!");
                    while (reader.Read())
                    {

                    }

                    loadtable2();
                    dbcon.Close();
                    empid.Text = "";
                    ppp.Text = "";
                    type.Text = "";
                    oo.Text = "";
                    hh.Text = "";
                    bbb.Text = "";
                    bb.Text = "";
                    ee.Text = "";
                    //month.Text = "";
                    ll.Text = "";
                    whrs.Text = "";
                    ohrs.Text = "";



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Deleta_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(empid.Text) || String.IsNullOrWhiteSpace(ppp.Text))
            {
                MessageBox.Show("Please select Raw First!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string constring = "datasource=localhost;port=3306;username=root";
                string query = "delete from supermarket.salary where empid='" + this.empid.Text + "'; ";
                MySqlConnection dbcon = new MySqlConnection(constring);
                MySqlCommand cmdb = new MySqlCommand(query, dbcon);
                MySqlDataReader reader;



                try
                {
                    dbcon.Open();


                    reader = cmdb.ExecuteReader();
                    MessageBox.Show("Deleted successfully!");
                    while (reader.Read())
                    {

                    }


                    dbcon.Close();
                    empid.Text = "";
                    ppp.Text = "";
                    type.Text = "";
                    oo.Text = "";
                    hh.Text = "";
                    bbb.Text = "";
                    bb.Text = "";
                    ee.Text = "";
                    //month.Text = "";
                    ll.Text = "";
                    whrs.Text = "";
                    ohrs.Text = "";





                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bunifuMetroTextbox1_OnValueChanged_1(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

    

        private void pictureBox16_Click(object sender, EventArgs e)
        {

        }

        private void id_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void name_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void approve_OnValueChanged(object sender, EventArgs e)
        {

        }

   

        private void groupBox17_Enter(object sender, EventArgs e)
        {
           
        }

        private void search_OnValueChanged(object sender, EventArgs e)
        {
           
            
            if (!(search.Text.Length == 0))
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);

                MySqlCommand cm = new MySqlCommand("select Empid as 'Employee Id',full_name as 'Employee Name',Email as 'email',NIC  from supermarket.employee_details  where empid='" + search.Text + "'", dbcon);

                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cm;
                    DataTable set = new DataTable();
                    sda.Fill(set);
                    BindingSource s = new BindingSource();

                    s.DataSource = set;
                    grr.DataSource = s;
                    sda.Update(set);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            else
            {
                loadtable2();
            }
            //DataView DV = new DataView(dbdataset);
            //DV.RowFilter = string.Format("full_name LIKE '%{0}%'", search.Text);
            //grr.DataSource = DV;

        }

        private void fname_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            //if (!(bunifuTextbox1.Text.Length == 0))
            //{
            //    string con = "datasource=localhost;port=3306;username=root";
            //    MySqlConnection dbcon = new MySqlConnection(con);

            //    MySqlCommand cm = new MySqlCommand("Select Empid as 'Employee Id',full_name as 'Employee Name',position as 'Position',phone as 'Phone',address as 'Address',email as 'Email' from supermarket.employee_details   where Empid='" + bunifuTextbox1.Text + "'", dbcon);

            //    try
            //    {
            //        MySqlDataAdapter sda = new MySqlDataAdapter();
            //        sda.SelectCommand = cm;
            //        DataTable set = new DataTable();
            //        sda.Fill(set);
            //        BindingSource s = new BindingSource();

            //        s.DataSource = set;
            //        bunifuCustomDataGrid3.DataSource = s;
            //        sda.Update(set);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }


            //}
            //else
            //{
            //    loadsalary();
            //}
        }

        private void calc_Click(object sender, EventArgs e)
        {
            double hourrate = Convert.ToDouble(this.hh.Text);
            double basic = Convert.ToDouble(this.bb.Text);
            double over = Convert.ToDouble(this.oo.Text);
            double bonus = Convert.ToDouble(this.bbb.Text);
            double epf = Convert.ToDouble(this.ee.Text);
            double loan = Convert.ToDouble(this.ll.Text);
            double whr = Convert.ToDouble(this.whrs.Text);
            double ohr = Convert.ToDouble(this.ohrs.Text);


            if (type.SelectedItem.ToString() == "permenant")
            {
                double salary =(basic+(over*ohr))-(loan+(epf*basic));
                sss.Text = Convert.ToString(salary);
            }
            else if (type.SelectedItem.ToString() == "contract")
            {
                double salary = ((hourrate*whr-8) + (over * ohr)) - (loan + (epf * basic));
                sss.Text = Convert.ToString(salary);
            }
        }

        private void uu_Click(object sender, EventArgs e)
        {
            //string constring = "datasource=localhost;port=3306;username=root";
            //string query = "update supermarket.leaving set empid='" + this.id.Text + "' ,name='" + this.name.Text + "'  ,sdate='" + this.start.Text + "' ,edate='" + this.end.Text + "',nodates='" + this.dates.Text + "' ,approve='" + this.approve.Text + "' ,reason='" + this.reason.Text + "'  where empid='" + this.id.Text + "'; ";
            //MySqlConnection dbcon = new MySqlConnection(constring);
            //MySqlCommand cmdb = new MySqlCommand(query, dbcon);
            //MySqlDataReader reader;



            //try
            //{
            //    dbcon.Open();
                

            //    reader = cmdb.ExecuteReader();
            //    MessageBox.Show("Updated successfully!");
            //    while (reader.Read())
            //    {

            //    }

               
            //    dbcon.Close();
            //    id.Text = "";
            //    name.Text = "";
            //    reason.Text = "";
            //    dates.Text = "";
            //    approve.Text = "";





            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void DOB_ValueChanged(object sender, EventArgs e)
        {
          DOB.MaxDate = DateTime.Now;
        }

        private void startdate_ValueChanged(object sender, EventArgs e)
        {
            //startdate.MinDate = DateTime.Now;
        }

        private void start_ValueChanged(object sender, EventArgs e)
        {
            start.MinDate = DateTime.Now;
        }

        private void end_ValueChanged(object sender, EventArgs e)
        {
            end.MinDate = DateTime.Now;
        }

        private void refresh_Click_1(object sender, EventArgs e)
        {
            empid.Text = "";
            ppp.Text = "";
            type.Text = "";
            oo.Text = "";
            hh.Text = "";
            bbb.Text = "";
            bb.Text = "";
            ee.Text = "";
            //month.Text = "";
            ll.Text = "";
            whrs.Text = "";
            ohrs.Text = "";

        }

        private void bunifuMetroTextbox1_OnValueChanged_2(object sender, EventArgs e)
        {
            if (!(bunifuMetroTextbox1.Text.Length == 0))
            {
                string con = "datasource=localhost;port=3306;username=root";
                MySqlConnection dbcon = new MySqlConnection(con);

                MySqlCommand cm = new MySqlCommand("Select Empid as 'Employee Id',full_name as 'Employee Name',position as 'Position',phone as 'Phone',address as 'Address',email as 'Email' from supermarket.employee_details   where Empid='" + bunifuMetroTextbox1.Text + "'", dbcon);

                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cm;
                    DataTable set = new DataTable();
                    sda.Fill(set);
                    BindingSource s = new BindingSource();

                    s.DataSource = set;
                    bunifuCustomDataGrid3.DataSource = s;
                    sda.Update(set);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            else
            {
                loadsalary();
            }
        }

        private void barcode_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void barcode_OnValueChanged_1(object sender, EventArgs e)
        {
             
            DateTime time = DateTime.Now;
            string formatD = "yyyy-MM-dd";

            string TimeValue = time.ToString(formatD);
            if (!(barcode.Text.Length == 0))
            {

                try
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);

                    MySqlCommand cm = new MySqlCommand("Select Empid from supermarket.employee_details   where Empid='" + barcode.Text + "'||barcode='" + barcode.Text + "'", dbcon);
                    //MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                    //MySqlCommand cmd = new MySqlCommand("select count(*) from supermarket.attendance where empid ='" + barcode.Text + "' ;", conn);
                    dbcon.Open();
                    DataSet d = new DataSet();
                    MySqlDataReader sdr = cm.ExecuteReader();

                    //for validation
                   // conn.Open();
                    //MySqlDataAdapter sda = new MySqlDataAdapter();
                    //sda.SelectCommand = cmd;

                    //int i = Convert.ToInt32(cmd.ExecuteScalar());

                    //if (i == 1)
                    //{

                        while (sdr.Read())
                        {
                            DateTime today = DateTime.Today;
                            aeid.Text = sdr["Empid"].ToString();

                            astart.Text = DateTime.Now.ToString("hh:mm tt");
                            // aend.Text = sdr["end_time"].ToString();
                            ddd.Text = DateTime.Now.ToString("yyyy-MM-dd");


                        }
                        dbcon.Close();
                   // }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            //if (!(barcode.Text.Length == 0))
            //{
            //    string con = "datasource=localhost;port=3306;username=root";
            //    MySqlConnection dbcon = new MySqlConnection(con);

            //    MySqlCommand cm = new MySqlCommand("Select* from supermarket.attendance   where empid='" + barcode.Text + "'", dbcon);
            //   // MySqlCommand cm1 = new MySqlCommand("update supermarket.salary set arrival = '" + this.astart.Text + "', end_time = '" + this.aend.Text + "'    where empid = '" + this.aeid.Text + "'   where empid='" + barcode.Text + "'", dbcon);

            //    try
            //    {
            //        MySqlDataAdapter sda = new MySqlDataAdapter();
            //        sda.SelectCommand = cm;
            //        DataTable set = new DataTable();
            //        sda.Fill(set);
            //        BindingSource s = new BindingSource();

            //        s.DataSource = set;
            //        bunifuCustomDataGrid3.DataSource = s;
            //        sda.Update(set);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }


            //}
            //else
            //{
            //    loadsalary();
            //}
        }

        private void eee_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(aeid.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //string x = DateTime.Now.ToString("yyyy-mm-dd");
                string constring = "datasource=localhost;port=3306;username=root";
                string query = "update supermarket.attendance set end_time='" + this.astart.Text + "' where empid='" + this.barcode.Text + "'and date='" + this.ddd.Text + "' ; ";
                MySqlConnection dbcon = new MySqlConnection(constring);
                MySqlCommand cmdb = new MySqlCommand(query, dbcon);
                MySqlDataReader reader;



                try
                {
                    dbcon.Open();


                    reader = cmdb.ExecuteReader();
                    MessageBox.Show("Updated successfully!");
                    while (reader.Read())
                    {

                    }
                    loadtable();
                    aeid.Text = "";
                    ddd.Text = "";
                    barcode.Text = "";
                    astart.Text = "";




                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
