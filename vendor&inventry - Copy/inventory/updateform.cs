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
namespace inventory
{
    public partial class updateform : Form
    {
        public updateform()
        {
            InitializeComponent();
        }

        private void code_TextChanged(object sender, EventArgs e)
        {
            if (!(code.Text.Length == 0))
            {

                try
                {
                    string con = "datasource=localhost;port=3306;username=root";
                    MySqlConnection dbcon = new MySqlConnection(con);
                    MySqlDataAdapter a = new MySqlDataAdapter();
                    a.SelectCommand = new MySqlCommand("Select Barcode,Item_name,Category,Sub_category,Brand,Rprice,Stock from madusha.Item where Item_code='" + code.Text + "'", dbcon);

                    MySqlCommand cm = new MySqlCommand("Select Barcode,Item_name,Category,Sub_category,Brand,Rprice,Stock from madusha.Item where Item_code='" + code.Text + "'", dbcon);
                    MySqlCommandBuilder cb = new MySqlCommandBuilder(a);
                    dbcon.Open();
                    DataSet d = new DataSet();
                    MySqlDataReader sdr = cm.ExecuteReader();

                    while (sdr.Read())
                    {
                        bcode.Text= sdr["Barcode"].ToString();
                        name.Text = sdr["Item_name"].ToString();
                        catcombo.Text = sdr["Category"].ToString();
                        subcombo.Text = sdr["Sub_category"].ToString();
                        brand.Text = sdr["Brand"].ToString();
                        rprice.Text = sdr["Rprice"].ToString();
                        aqty.Text = sdr["Stock"].ToString();
                        

                    }
                    dbcon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void bunifuCustomTextbox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void nqty_TextChanged(object sender, EventArgs e)
        {
            try { int nq = Convert.ToInt32(nqty.Text);
                int aq = Convert.ToInt32(aqty.Text);
                int tot = nq + aq;
                tqty.Text = tot.ToString(); }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
