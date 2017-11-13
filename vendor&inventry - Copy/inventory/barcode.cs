using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarcodeLib;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using BarcodeLib.Symbologies;
using System.Diagnostics;

namespace inventory
{
    public partial class barcode : Form
    {
        public barcode(string b,string n,string it)
        {
            InitializeComponent();
            bccode.Text = b;
            bcname.Text = n;
            brcdid.Text = it;
            
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            BarcodeLib.Barcode barcode = new BarcodeLib.Barcode()
            {
                IncludeLabel = true,
                Alignment = AlignmentPositions.CENTER,
                Width = 390,
                Height = 150,
                RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                BackColor = Color.White,
                ForeColor = Color.Black,
            };

            puyt.Image = barcode.Encode(TYPE.CODE128B, bccode.Text);
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            try
            {

                puyt.Image.Save(@"b" + bccode.Text + ".jpg", ImageFormat.Jpeg);
                MessageBox.Show("Barcode successfully saved in Barcodes folder", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            try
            {

                puyt.Image.Save(@"b" + bccode.Text + ".jpg", ImageFormat.Jpeg);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ProcessStartInfo psi = new ProcessStartInfo(@"b" + bccode.Text + ".jpg");
            psi.Verb = "print";
            Process.Start(psi);
        }
    }
}
