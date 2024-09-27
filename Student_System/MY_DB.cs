using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Student_System
{

    /*
     * in this class we will create the connection btw our app and mysql db
     * we need to add the mysql connector to the project
     * we need to create the databse
      */
    class MY_DB
    {
        //the connection
        MySqlConnection con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=student_db");

        //create a fuction to get the connection
        public MySqlConnection getConnection
        {
            get
            {
                return con;
            }
        }

        //create a functon to open the connection
        public void openConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }


        //create a functon to close the connection
        public void closeConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

    }
}
