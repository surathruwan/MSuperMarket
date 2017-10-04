using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace inventory
{
    
    class SalesMethod
    {

        
        //auto complete text boxes
        public void ItemsAutoComplete()
        {
            POS ps = new POS();
            ps.txtDescription.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ps.txtDescription.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=madusha");
            string sqlquery = "SELECT Item_name from item ";
            MySqlCommand cmd = new MySqlCommand(sqlquery, conn);
            MySqlDataReader msReader;

            try
            {
                conn.Open();
                msReader = cmd.ExecuteReader(); ;

                while (msReader.Read())
                {
                    string sname = msReader.GetString(0);
                    coll.Add(sname);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

            ps.txtDescription.AutoCompleteCustomSource = coll;

        }

    }
}
