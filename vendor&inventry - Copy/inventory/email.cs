using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace madushaTemp
{
    public partial class email : Form
    {
        int b,a,c;
        public email()
        {
            InitializeComponent();
            cmbsmtp.SelectedIndex = 0;
            progressbarMail.Visible = false;

        }
        public void Regexp3(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {

            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {

                pc.Image = inventory.Properties.Resources.invalid;
                n.SetToolTip(p1, "Email Should have @ , . Symbols");
                b = -99;

            }
            else
            {
                pc.Image = null;
                b = 0;

            }

        }
        public void Regexp2(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {

            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {

                pc.Image = inventory.Properties.Resources.invalid;
                n.SetToolTip(p2, "Email Should have @ , . Symbols");
               c = -99;

            }
            else
            {
                pc.Image = null;
                c = 0;

            }

        }
        public void Regexp1(string re, Bunifu.Framework.UI.BunifuMetroTextbox tb, PictureBox pc, string s)
        {

            ToolTip n = new ToolTip();
            Regex regex = new Regex(re);
            if (!regex.IsMatch(tb.Text))
            {

                pc.Image = inventory.Properties.Resources.invalid;
                n.SetToolTip(p1, "Email Should have @ , . Symbols");
                a = -99;

            }
            else
            {
                pc.Image = null;
                a = 0;

            }

        }
        public void progressBar()
        {
            for (int i=0;i<=100;i++)
            {
                progressbarMail.Value = i;
                progressbarMail.Update();
            }


        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void minimizeButton_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnattach_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string picPath = dlg.FileName.ToString();
                txtatt.Text = picPath;

            }
        }

        private void btnmailsend_Click(object sender, EventArgs e)
        {
            Regexp3(@"^([\w]+)@([\w]+)\.([\w]+)$", txtfrom, p3, "email");
            Regexp2(@"^([\w]+)@([\w]+)\.([\w]+)$", txtto, p2, "email");
            Regexp1(@"^([\w]+)@([\w]+)\.([\w]+)$", txtus, p1, "email");

            if (String.IsNullOrEmpty(txtfrom.Text) || String.IsNullOrWhiteSpace(txtto.Text) || String.IsNullOrWhiteSpace(txtsubject.Text) || String.IsNullOrWhiteSpace(txtmsg.Text) || String.IsNullOrWhiteSpace(txtus.Text) || String.IsNullOrWhiteSpace(txtpa.Text)||String.IsNullOrEmpty(txtatt.Text))
            {
                MessageBox.Show("One or More Fields are Empty ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (a == -99 || b == -99 || c == -99)
            { }
            else if (a == 0 && b == 0 && c == 0)
            {
                progressbarMail.Visible = true;
                MailMessage mail = new MailMessage(txtfrom.Text, txtto.Text, txtsubject.Text, txtmsg.Text);
                mail.Attachments.Add(new Attachment(txtatt.Text.ToString()));



                SmtpClient client = new SmtpClient(cmbsmtp.Text);
                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential(txtus.Text, txtpa.Text);
                client.EnableSsl = true;
                client.Send(mail);
                progressBar();
                MessageBox.Show("Mail Sent Successfully ", "Success", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
    }
}
