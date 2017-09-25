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

namespace emailCheck
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



       
        private void Form1_Load(object sender, EventArgs e)
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

      

       

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void from_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void from_TextChanged(object sender, EventArgs e)
        {

        }

        private void smtp_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void subject_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || char.IsNumber(e.KeyChar));

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if ((!checkEmail(to.Text.ToString())) || (!checkEmail(from.Text.ToString())))
            {
                MessageBox.Show("Please re-check email addresses again");

            }
           

            else if ((from.Text.Length < 1) || (to.Text.Length < 1) || (subject.Text.Length < 1) || (password.Text.Length < 1) || (smtp.Text.Length < 1))
            {
                MessageBox.Show("Please fill all fields");
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
                    client.Credentials = new System.Net.NetworkCredential(from.Text, password.Text);
                    client.EnableSsl = true;

                    client.Send(mail);


                    ProgresBar();
                    MessageBox.Show("Mail Sent!", "Success", MessageBoxButtons.OK);
                    this.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Mailing failed,Please recheck details ");
                    
                    //MessageBox.Show(ee.Message);
                }

            }
        }
        public static bool checkEmail(String email)
        {
            bool IsValid = false;
            Regex r = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (r.IsMatch(email))
                IsValid = true;
            return IsValid;
        }
    }
}
