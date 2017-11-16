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



namespace madushaTemp
{
    public partial class vehicle_availa : Form
    {
        public vehicle_availa()
        {
            InitializeComponent();
            LoadTableVehicleDetails();
            LoadTableVehicleAvailability();
            FillCombo1();
            bunifuThinButton27.Visible = false;

        }
        DataTable dbdataset;
        string status;
    // string available;

        private void vehicle_availa_Load(object sender, EventArgs e)
        {
            //cmddb.Connection = condb;
        }

      

        //vehicle detail form of save button
        private void bunifuThinButton211_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtvcode.Text) || String.IsNullOrWhiteSpace(txtvnum.Text) || String.IsNullOrWhiteSpace(txtdname.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }




            else
            {

                string st = "datasource=localhost;port=3306;username=root";
                string q = "insert into supermarket.vehicle_details(vehicle_code,vehicle_type,vehicle_no,driver_name,by_date,status) values ('" + this.txtvcode.Text + "','" + this.ctype.SelectedItem + "','" + this.txtvnum.Text + "','" + this.txtdname.Text + "','" + this.dateTime1.Text + "','" + status + "'); ";
                MySqlConnection condb = new MySqlConnection(st);
                MySqlCommand cmddb = new MySqlCommand(q, condb);
                MySqlDataReader myReader;

                try
                {
                    condb.Open();
                    myReader = cmddb.ExecuteReader();
                    MessageBox.Show("Data Inserted Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTableVehicleDetails();
                    while (myReader.Read())
                    {



                    }
                }

                catch (Exception)
                {

                    throw;
                }

            }



        }

        public void LoadTableVehicleDetails()
        {
            string st = "datasource=localhost;port=3306;username=root";
            MySqlConnection condb = new MySqlConnection(st);
            MySqlCommand cmddb = new MySqlCommand("select * from supermarket.vehicle_details ;", condb);

            try
            {
                MySqlDataAdapter sd = new MySqlDataAdapter();
                sd.SelectCommand = cmddb;
                dbdataset = new DataTable();
                sd.Fill(dbdataset);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dbdataset; 
                bunifuCustomDataGrid1.DataSource = bsource;
                sd.Update(dbdataset);
              
            }
            catch (Exception)
            {

                throw;
            }

        }


        public void LoadTableVehicleAvailability()
        {
            string st = "datasource=localhost;port=3306;username=root";
            MySqlConnection condb = new MySqlConnection(st);
            MySqlCommand cmddb = new MySqlCommand("select vehicle_code,vehicle_type,vehicle_no,by_date,status from supermarket.vehicle_details ;", condb);

            try
            {
                MySqlDataAdapter sd = new MySqlDataAdapter();
                sd.SelectCommand = cmddb;
                dbdataset = new DataTable();
                sd.Fill(dbdataset);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dbdataset;
                bunifuCustomDataGrid2.DataSource = bsource;
                sd.Update(dbdataset);


                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "update";
                bunifuCustomDataGrid2.Columns.Add(btn);


            }
            catch (Exception)
            {

                throw;
            }

        }




        public void FillCombo1()
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
                    ctype.Items.Add(stype);

                }


            }
            catch (Exception)
            {

                throw;
            }



        }





        private void txtdname_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void ctype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
     
      



        //get row by inserting vehicle id only
        private void txtvid_OnValueChanged(object sender, EventArgs e)
        {


            DataView DV = new DataView(dbdataset);
            DV.RowFilter = string.Format("vehicle_no LIKE '%{0}%'", txtvid.Text);
            bunifuCustomDataGrid1.DataSource = DV;

        }

        private void bunifuCustomLabel6_Click(object sender, EventArgs e)
        {

        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //to get the values of table into textboxes
        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.bunifuCustomDataGrid1.Rows[e.RowIndex];


                txtvcode.Text = row.Cells["vehicle_code"].Value.ToString();
                ctype.Text = row.Cells["vehicle_type"].Value.ToString();
                txtvnum.Text = row.Cells["vehicle_no"].Value.ToString();
                txtdname.Text = row.Cells["driver_name"].Value.ToString();
                dateTime1.Text = row.Cells["by_date"].Value.ToString();

                status = row.Cells["status"].Value.ToString();
                if (status == "active")
                {
                    radioact.Checked = true;

                }
                else
                    radioinact.Checked = true;



            }


        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            txtvcode.Text ="";
            ctype.Text = "";
            txtvnum.Text = "";
            txtdname.Text = "";
            dateTime1.Text = "";
            radioact.Checked = false;
            radioinact.Checked = false;






                    }


      


        //delete a selected row by ticking chekbox
        private void bunifuThinButton25_Click_1(object sender, EventArgs e)
        {
            //    foreach (DataGridViewRow item in this.bunifuCustomDataGrid1.SelectedRows)
            //    {
            //        bunifuCustomDataGrid1.Rows.RemoveAt(item.Index);



            //    }
            //    MessageBox.Show("Succesfully Deleted Selected Rows!!");


            //foreach (DataGridViewRow dataRow in bunifuCustomDataGrid1.Rows)
            //{
            //    string st = "datasource=localhost;port=3306;username=root";
            //    MySqlConnection condb = new MySqlConnection(st);



            //    if (bool.Parse(dataRow.Cells[0].Value.ToString()))
            //    {
            //        label2.Text = dataRow.Cells[0].RowIndex.ToString();
            //        MySqlDataAdapter y = new MySqlDataAdapter("delete from supermarket.vehicle_details where vehicle_code= '" + dataRow.Cells[1].Value.ToString() + "' ", condb);
            //        DataTable z = new DataTable();
            //        y.Fill(z);

            //    }

            //}

            //MessageBox.Show("Succesfully Deleted Selected Rows!!");





        }






        //vehicle details view data button with checkbox
        private void bunifuThinButton28_Click(object sender, EventArgs e)
        {
            string st = "datasource=localhost;port=3306;username=root";
            MySqlConnection condb = new MySqlConnection(st);
   
            MySqlDataAdapter x = new MySqlDataAdapter("select * from supermarket.vehicle_details", condb);
            DataTable ss = new DataTable();
            x.Fill(ss);
            bunifuCustomDataGrid1.Rows.Clear();
            foreach (DataRow data in ss.Rows)
            {
                int a = bunifuCustomDataGrid1.Rows.Add();
                bunifuCustomDataGrid1.Rows[a].Cells[0].Value = "false";
                bunifuCustomDataGrid1.Rows[a].Cells[1].Value = data["vehicle_code"].ToString();
                bunifuCustomDataGrid1.Rows[a].Cells[2].Value = data["vehicle_type"].ToString();
                bunifuCustomDataGrid1.Rows[a].Cells[3].Value = data["vehicle_no"].ToString();
                bunifuCustomDataGrid1.Rows[a].Cells[4].Value = data["driver_name"].ToString();
                bunifuCustomDataGrid1.Rows[a].Cells[5].Value = data["by_date"].ToString();
                bunifuCustomDataGrid1.Rows[a].Cells[6].Value = data["status"].ToString();



            }
           

        }





        private void bunifuThinButton23_Click_1(object sender, EventArgs e)
        {

            if (DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("delete from supermarket.vehicle_details where vehicle_code='" + this.txtvcode.Text + "'", conn);

                    cmd.ExecuteNonQuery();



                    MessageBox.Show("Deleted Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 
                    LoadTableVehicleDetails();

                    conn.Close();
                }
                catch (Exception r)
                {

                    MessageBox.Show(r.Message);
                }
            }

        }

        private void bunifuCustomDataGrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == bunifuCustomDataGrid2.Columns["Column6"].Index && e.RowIndex >= 0)
            //{
            //    //Console.WriteLine("Button on row {0} clicked", e.RowIndex);
            //    MessageBox.Show("dsfhjdhgfgjhjhgrfghjkk");



            //}
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.bunifuCustomDataGrid1.Rows[e.RowIndex];


                txtvcode.Text = row.Cells["vehicle_code"].Value.ToString();
                ctype.Text = row.Cells["vehicle_type"].Value.ToString();
                txtvnum.Text = row.Cells["vehicle_no"].Value.ToString();
                txtdname.Text = row.Cells["driver_name"].Value.ToString();
                dateTime1.Text = row.Cells["by_date"].Value.ToString();

                status = row.Cells["status"].Value.ToString();
                if (status == "active")
                {
                    radioact.Checked = true;

                }
                else
                    radioinact.Checked = true;



            }

        }

        private void bunifuThinButton24_Click_1(object sender, EventArgs e)
        {
            string st = "datasource=localhost;port=3306;username=root";
            MySqlConnection condb = new MySqlConnection(st);

            MySqlDataAdapter x = new MySqlDataAdapter("select * from supermarket.vehicle_details", condb);
            DataTable ss = new DataTable();
            x.Fill(ss);
            bunifuCustomDataGrid2.Rows.Clear();
            foreach (DataRow data in ss.Rows)
            {
                int a = bunifuCustomDataGrid2.Rows.Add();
                bunifuCustomDataGrid2.Rows[a].Cells[0].Value = "false";
                bunifuCustomDataGrid2.Rows[a].Cells[1].Value = data["vehicle_code"].ToString();
                bunifuCustomDataGrid2.Rows[a].Cells[2].Value = data["vehicle_type"].ToString();
                bunifuCustomDataGrid2.Rows[a].Cells[3].Value = data["vehicle_no"].ToString();
                bunifuCustomDataGrid2.Rows[a].Cells[4].Value = data["driver_name"].ToString();
                bunifuCustomDataGrid2.Rows[a].Cells[5].Value = data["by_date"].ToString();
                bunifuCustomDataGrid2.Rows[a].Cells[6].Value = data["status"].ToString();



            }
        }

        private void radioact_CheckedChanged(object sender, EventArgs e)
        {

            status = "active";
        }

        private void radioinact_CheckedChanged(object sender, EventArgs e)
        {

            status = "inactive";
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            try
            {
                string st = "datasource=localhost;port=3306;username=root";

                MySqlConnection condb = new MySqlConnection(st);
                string q = "select * from supermarket.vehicle_details where vehicle_no like '%" + textvehino.Text + "%' and by_date  like '%" + dateTimePicker2.Text + "%'";
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
            catch (Exception)
            {

                throw;
            }


        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("update supermarket.vehicle_details set vehicle_code='" + this.txtvcode.Text + "' , vehicle_type='" + this.ctype.Text + "', vehicle_no='" + this.txtvnum.Text + "', driver_name='" + this.txtdname.Text + "', by_date='" + this.dateTime1.Text + "', status='" +status+ "'where vehicle_code='" + this.txtvcode.Text + "'", conn);

                cmd.ExecuteNonQuery();



                MessageBox.Show("Data Updated Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTableVehicleDetails();
                conn.Close();
            }
            catch (Exception r)
            {

                MessageBox.Show(r.Message);
            }
        }

        private void bunifuThinButton27_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("delete from supermarket.vehicle_details where vehicle_code='" + this.txtvcode.Text + "'", conn);

                    cmd.ExecuteNonQuery();



                    MessageBox.Show("Deleted Successfully !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTableVehicleAvailability();

                    conn.Close();
                }
                catch (Exception r)
                {

                    MessageBox.Show(r.Message);
                }
            }
        }

        //private void Column6_Click(object sender, EventArgs e)
        //{
        //    email e1 = new email();
        //    e1.Show();







        //}


    }
}
