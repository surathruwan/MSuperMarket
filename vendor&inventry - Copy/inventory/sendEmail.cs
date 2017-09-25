using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Net.Mail;

namespace madushaTemp
{
    public partial class sendEmail : Form
    {
        public sendEmail()
        {
            InitializeComponent();
            password.PasswordChar = '*';
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
           
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {

        }

        private void subject_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void body_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void to_TextChanged(object sender, EventArgs e)
        {

        }

        private void from_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void smtp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void sendEmail_Load(object sender, EventArgs e)
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
        private void sent_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(from.Text) || String.IsNullOrWhiteSpace(to.Text) || String.IsNullOrWhiteSpace(password.Text) || String.IsNullOrWhiteSpace(subject.Text) )
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                bunifuCircleProgressbar1.Show();
                try
                {
                    MailMessage mail = new MailMessage(from.Text, to.Text, subject.Text, body.Text);
                    SmtpClient client = new SmtpClient(smtp.Text);
                    // SmtpClient client = new SmtpClient("smtp.gmail.com",587);
                    if (smtp.Text == "smtp.gmail.com")
                        client.Port = 587;
                    else if (smtp.Text == "smtp.mail.yahoo.com")
                        client.Port = 587;
                    //  client.Credentials = new System.Net.NetworkCredential(username.Text, password.Text);
                    client.Credentials = new System.Net.NetworkCredential(from.Text, password.Text);
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

                //  }
            }
        }
    }
}
