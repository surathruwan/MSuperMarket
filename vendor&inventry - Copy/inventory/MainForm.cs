using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace inventory
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
           
            timer1.Start();
            panel4.Controls.Clear();
            madushaTemp.TestForm VM = new madushaTemp.TestForm();
            VM.TopLevel = false;
            VM.AutoScroll = true;
            panel4.Controls.Add(VM);
            VM.Show();
            string i = login.getUsername();
            string u = Session.UserLabel();
            lblname.Text = i;
        }

        //load form to panel
        private void loadForm(Form form)
        {
            panel4.Controls.Clear();
            form.TopLevel = false;
            form.AutoScroll = true;
            panel4.Controls.Add(form);
            form.Show();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            madushaTemp.TestForm tForm = new madushaTemp.TestForm();
            tForm.TopLevel = false;
            tForm.AutoScroll = true;
            panel4.Controls.Add(tForm);
            tForm.Show();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minMaxButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
           loadForm(new POS());
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            main tForm = new main();
            tForm.TopLevel = false;
            tForm.AutoScroll = true;
            panel4.Controls.Add(tForm);
            tForm.Show();
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
           // loadForm(new Loyalty());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.timeLabel.Text = dateTime.ToString();
            //this.timeLabel.Text = dateTime.Day.ToString() + "-" + dateTime.Month.ToString() + "-" + dateTime.Year.ToString() + "  " + dateTime.TimeOfDay.ToString().Substring(0,dateTime.TimeOfDay.ToString().Length-8);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            madushaTemp.Employee VM = new madushaTemp.Employee();
            VM.TopLevel = false;
            VM.AutoScroll = true;
            panel4.Controls.Add(VM);
            VM.Show();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            inventory.repair VM = new inventory.repair();
            VM.TopLevel = false;
            VM.AutoScroll = true;
            panel4.Controls.Add(VM);
            VM.Show();
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            vendor_management.Vendor VM = new vendor_management.Vendor();
            VM.TopLevel = false;
            VM.AutoScroll = true;
            panel4.Controls.Add(VM);
            VM.Show();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            madushaTemp.Form2 VM = new madushaTemp.Form2();
            VM.TopLevel = false;
            VM.AutoScroll = true;
            panel4.Controls.Add(VM);
            VM.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            try
            {
                Session.windUpSession();
                panel4.Controls.Clear();
                login l = new login();
                l.Show();
                this.Hide();
            }
            catch (Exception c)
            {

                MessageBox.Show(c.Message);
            }
            
                }


    }
}
