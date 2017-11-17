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
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace inventory
{
    public partial class report : Form
    {
        public report()
        {
            InitializeComponent();
            fillYear(y1);
            fillYear(y2);
            fillYear(y3);
            fillmonth(m1);
            fillmonth(m2);
            fillmonth(m3);
            m1.Text = "Select Month";
            m2.Text = "Select Month";
            m3.Text = "Select Month";
            y1.Text = "Select year";
            y2.Text = "Select year";
            y3.Text = "Select year";
            hidetb.Visible = false;

        }
        public void pdfReport(string name, string query, string fname)
        {
            string con = "datasource=localhost;port=3306;username=root";
            MySqlConnection dbcon = new MySqlConnection(con);


            MySqlCommand cm = new MySqlCommand(query, dbcon);
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

            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@fname, FileMode.Create));
            doc.Open();


            //Add border to page
            PdfContentByte content = w.DirectContent;
            iTextSharp.text.Rectangle rectangle = new iTextSharp.text.Rectangle(doc.PageSize);
            rectangle.Left += doc.LeftMargin - 5;
            rectangle.Right -= doc.RightMargin - 5;
            rectangle.Top -= doc.TopMargin - 22;
            rectangle.Bottom += doc.BottomMargin - 5;
            content.SetColorStroke(BaseColor.BLACK);
            content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
            content.Stroke();




            //BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN,BaseFont.CP1252,BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 30, BaseColor.BLACK);
            Paragraph prg = new Paragraph();
            prg.Alignment = Element.ALIGN_CENTER;
            prg.Add(new Chunk(name, font5));
            doc.Add(prg);

            //image 
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("msmsIcon1.png");
            logo.ScaleAbsolute(50f, 50f);
            doc.Add(logo);

            //line separator
            Paragraph pd = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2.0f, 100.0f, BaseColor.MAGENTA, Element.ALIGN_CENTER, 2.0f)));
            doc.Add(pd);


            //Authors
            iTextSharp.text.Font font15 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.BLACK);
            Paragraph prg1 = new Paragraph();
            prg1.Alignment = Element.ALIGN_RIGHT;
            Paragraph prg2 = new Paragraph();
            prg2.Alignment = Element.ALIGN_RIGHT;
            prg1.Add(new Chunk("Prepared By: Upali Kariyawasam", font15));
            prg2.Add(new Chunk("Prepared Date: " + DateTime.Now.ToShortDateString(), font15));
            doc.Add(prg1);
            doc.Add(prg2);


            //line separator
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2.0f, 100.0f, BaseColor.MAGENTA, Element.ALIGN_CENTER, 9.0f)));
            doc.Add(p);

            PdfPTable table = new PdfPTable(hidetb.Columns.Count);

            //add headers from gridview to table
            iTextSharp.text.Font fonth = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.BLACK);



            for (int j = 0; j < hidetb.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.CYAN;
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

            System.Diagnostics.Process.Start(@fname);


        }
        public void chartLoadProLosBar()
        {
            chart1.Series["Count"].Points.Clear();
            chart1.Visible = true;

            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select count(*) as 'count',Category from supermarket.item group by Category ;", conn);
            MySqlDataReader myR;
            try
            {
                conn.Open();
                myR = cmd.ExecuteReader();

                while (myR.Read())
                {
                    this.chart1.Series["Count"].Points.AddXY(myR.GetString("Category"), myR.GetInt32("Count"));
                }


            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }




        }

        public void chartLoadProLosPie()
        {
            chart2.Series["Count"].Points.Clear();
            chart1.Visible = true;
            
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
            MySqlCommand cmd = new MySqlCommand("select count(*) as 'count',Category from supermarket.item group by Category ;", conn);
            MySqlDataReader myR;
            try
            {
                conn.Open();
                myR = cmd.ExecuteReader();

                while (myR.Read())
                {
                    this.chart2.Series["Count"].Points.AddXY(myR.GetString("Category"), myR.GetInt32("count"));
                }


            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }



        }
        public void fillYear(ComboBox item)
        {
            for (int year = 2015; year <= DateTime.UtcNow.Year; year++)
            {
               item.Items.Add(year);
            }

           
           
        }
        public void fillmonth(ComboBox item)
        {
            var americanCulture = new CultureInfo("en-US");
            item.Items.AddRange(americanCulture.DateTimeFormat.MonthNames);



        }
        
        private void groupBox2_Enter(object sender, EventArgs e)
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

        private void report_Load(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            if (cmbchart.SelectedIndex == 0)
            {
                chart2.Visible = false;
                chart1.Visible = true;
                chartLoadProLosBar();


            }
            else if (cmbchart.SelectedIndex == 1)
            {
                
                chart1.Visible = false;
                chart2.Visible = true;

                chartLoadProLosPie();

            }

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (y1.Text == "Select year" || m1.Text == "Select Month") {

                MessageBox.Show("Please Select a year and a month", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else { 


            string x = y1.Text + "-" + (m1.SelectedIndex + 1).ToString("00");
      
            pdfReport(y1.Text + " " + m1.Text + " Stock Report", "select * from supermarket.stock where Date like '" + x + "%'", "yrep1.pdf");
        }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            if (y1.Text == "Select year")
            {

                MessageBox.Show("Please Select a year", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pdfReport(y1.Text+" Stock Report", "select * from supermarket.stock where Date like '" + y1.Text + "%'", "yrep1.pdf");

            }
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            if (y3.Text == "Select year")
            {

                MessageBox.Show("Please Select a year", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pdfReport(y3.Text+" Good Transfer Report", "select * from supermarket.transfer where date like '" + y3.Text + "%'", "yreps.pdf");

            }
        }

        private void m1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void bunifuThinButton27_Click(object sender, EventArgs e)
        {
            if (y3.Text == "Select year" || m3.Text == "Select Month")
            {

                MessageBox.Show("Please Select a year and a month", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {


                string x = y3.Text + "-" + (m3.SelectedIndex + 1).ToString("00");

                pdfReport(y3.Text + " " + m3.Text + " Good Transfer Report", "select * from supermarket.transfer where date like '" + x + "%'", "yrep1.pdf");
            }
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            if (y2.Text == "Select year" || m2.Text == "Select Month")
            {

                MessageBox.Show("Please Select a year and a month", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {


                string x = y2.Text + "-" + (m2.SelectedIndex + 1).ToString("00");

                pdfReport(y2.Text + " " + m2.Text + " Returned Goods Report", "select * from supermarket.returneditem where sdate like '" + x + "%'", "yrep1.pdf");
            }
        }

        private void bunifuThinButton22_Click_1(object sender, EventArgs e)
        {
            if (y2.Text == "Select year")
            {

                MessageBox.Show("Please Select a year", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pdfReport(y2.Text + " Retrned Goods Report", "select * from supermarket.returneditem where sdate like '" + y2.Text + "%'", "yreps.pdf");

            }
        }

        private void bunifuThinButton28_Click(object sender, EventArgs e)
        {
            defreport d = new defreport();
            d.Show();
        }

        private void bunifuThinButton29_Click(object sender, EventArgs e)
        {
            ilevelrep l = new ilevelrep();
            l.Show();
        }
    }
}
