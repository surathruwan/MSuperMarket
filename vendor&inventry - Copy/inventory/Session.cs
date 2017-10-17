﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory
{
    class Session
    {
        private static String userName = null;
        private static String userType = null;
        private static int a = 0;
        public static bool isSessionStarted()
        {
            return false;
        }
        public static void startSession(String username)
        {
            if (!isSessionStarted())
            {
                userName = username;
                
                MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=supermarket");
                MySqlCommand cmd = new MySqlCommand("SELECT type from supermarket.users WHERE user = '" + username + "'", conn);
                MySqlDataReader dr;

                try
                {
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        userType = dr[0].ToString();
                    }
                    if (userType == "admin")
                    {

                        a = 1;

                    }
                    else if (userType == "cashier")
                    {
                        a = 2;
                    }
                    else { a = 3; }
                }
                catch (Exception o)
                {
                    
                    MessageBox.Show(o.Message);
                }



            }
            else { MessageBox.Show("Cannot find the user"); }
        }

        public static int getUser()
        {
            if (userType == "admin")
            { return a; }
            else if (userType == "cashier")
            {
                return a;
            }
            else { return a; }

        }

        public static string UserLabel()
        {
            if (userType == "admin")
            { return "admin"; }
            else if (userType == "cashier")
            {
                return "cashier";
            }
            else { return "doperator"; }

        }

        public static void windUpSession()
        {

            //Reset values
            userName = null;
            userType = null;

        }



    }
}
