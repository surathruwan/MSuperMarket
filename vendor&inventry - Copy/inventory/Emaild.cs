using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Web;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace madushaTemp
{
    public partial class Emaild : Form
    {
        public Emaild()
        {
            InitializeComponent();
            txtpasswd.PasswordChar = '*';
            
        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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




        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;//minimize
        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            this.Close();//close
        }

        private void btnsend_Click(object sender, EventArgs e)
        {





            if ((txtfrom.Text.Length < 1) || (txtto.Text.Length < 1) || (txtsubject.Text.Length < 1) || (txtpasswd.Text.Length < 1) || (cmbsmtp.Text.Length < 1))
            {
                MessageBox.Show("Please fill all fields");
            }




            else
            {


                bunifuCircleProgressbar1.Show();

                try
                {
                    MailMessage mail = new MailMessage(txtfrom.Text, txtto.Text, txtsubject.Text, txtbody.Text);
                    //mail.Attachments.Add(new Attachment(label7.Text.ToString()));
                    SmtpClient client = new SmtpClient(cmbsmtp.Text);
                    // SmtpClient client = new SmtpClient("smtp.gmail.com",587);
                    if (cmbsmtp.Text == "smtp.gmail.com")
                        client.Port = 587;
                    else if (cmbsmtp.Text == "smtp.mail.yahoo.com")
                        client.Port = 587;
                    //  client.Credentials = new System.Net.NetworkCredential(username.Text, password.Text);
                    client.Credentials = new System.Net.NetworkCredential(txtfrom.Text, txtpasswd.Text);
                    client.EnableSsl = true;

                    client.Send(mail);


                    ProgresBar();
                    MessageBox.Show("Mail Sent!", "Success", MessageBoxButtons.OK);
                    this.Close();
                }
                catch (Exception ee)
                {

                    //  throw;
                    MessageBox.Show(ee.Message);
                    //this.Close();
                }



            }
            




        }
 
    }
}
